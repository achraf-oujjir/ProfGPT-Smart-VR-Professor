using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using UnityEngine.Networking;


namespace OpenAI
{
    public class ProfGPT : MonoBehaviour
    {
        //for Record
        [SerializeField] private Button recordButton;
        [SerializeField] private Button stopButton;
        [SerializeField] private Text message;
        [SerializeField] private Dropdown dropdown;

        private readonly string fileName = "output.wav";
        private readonly int duration = 30;

        private AudioClip clip;
        private bool isRecording;
        private readonly OpenAIApi openai = new OpenAIApi();


        //for GPT
        private List<ChatMessage> messages = new List<ChatMessage>();
        private readonly string startingPrompt = "Act as a professor in a classroom and reply to the questions. Don't break character. Don't ever mention that you are an AI model.";


        //for Polly
        [SerializeField] private AudioSource audioSource;
        private readonly BasicAWSCredentials credentials = new BasicAWSCredentials(accessKey: "", secretKey: "");
        private AmazonPollyClient client;



        // Start is called before the first frame update
        private void Start()
        {

            //for Record
            foreach (var device in Microphone.devices)
            {
                dropdown.options.Add(new Dropdown.OptionData(device));
            }

            recordButton.onClick.AddListener(StartRecording);
            stopButton.onClick.AddListener(EndRecording);
            dropdown.onValueChanged.AddListener(ChangeMicrophone);

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            dropdown.SetValueWithoutNotify(index);

            //for Polly
            client = new AmazonPollyClient(credentials, RegionEndpoint.EUCentral1);

            Debug.Log("Start has finished!\n");
        }

        private void ChangeMicrophone(int index)
        {
            PlayerPrefs.SetInt("user-mic-device-index", index);
            Debug.Log("ChangeMicrophone Done.\n");
        }

        private void StartRecording()
        {
            isRecording = true;
            recordButton.enabled = false;
            stopButton.enabled = true;

            var index = PlayerPrefs.GetInt("user-mic-device-index");
            clip = Microphone.Start(dropdown.options[index].text, false, duration, 44100);
            Debug.Log("Started recording\n");

        }

        private void EndRecording()
        {
            isRecording = false;
            Debug.Log("Recording has ended, it will be saved, transcribed and sent.\n");
            SaveTranscribeSend();
        }

        private async void SaveTranscribeSend()
        {
            //message.text = "Transcripting...";

            Microphone.End(null);
            byte[] data = SaveWav.Save(fileName, clip);

            Debug.Log("1) Audio saved\n");

            var req = new CreateAudioTranscriptionsRequest
            {
                FileData = new FileData() { Data = data, Name = "audio.wav" },
                Model = "whisper-1",
                Language = "en"
            };
            var res = await openai.CreateAudioTranscription(req);
            Debug.Log("2) Request sent to Whisper\n");
            Debug.Log("This is res.Text: " + res.Text);

            message.text = res.Text;

            Debug.Log("3) Audio transcribed: "+ message.text + "\n");//Added by me

            recordButton.enabled = true;
            stopButton.enabled = false;
            isRecording = false;

            Debug.Log("Launching to GPT\n");

            //for sending to gpt
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = message.text
            };

            if (messages.Count == 0) newMessage.Content = startingPrompt + "\n" + message.text;
            messages.Add(newMessage);

            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-3.5-turbo-0301",
                Messages = messages
            });

            Debug.Log("Got response from GPT !!\n");
            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var gptMsg = completionResponse.Choices[0].Message;
                Debug.Log(completionResponse.Choices[0]);
                gptMsg.Content = gptMsg.Content.Trim();

                messages.Add(gptMsg);
                message.text = gptMsg.Content;
                Debug.Log("The returned message is: " + gptMsg.Content);
                Debug.Log("The GPT response will be sent to Polly!");

                SaySpeech(gptMsg.Content);


            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }
        }
    


        private async void SaySpeech(string gptResponse)
        {
            //SpeechRequest
            var request = new SynthesizeSpeechRequest()
            {
                Text = gptResponse,
                Engine = Engine.Neural,
                VoiceId = VoiceId.Ayanda,
                OutputFormat = OutputFormat.Mp3
            };

            var response = await client.SynthesizeSpeechAsync(request);
            WriteIntoFile(response.AudioStream);

            using (var www = UnityWebRequestMultimedia.GetAudioClip($"{Application.persistentDataPath}/audio.mp3", AudioType.MPEG))
            {
                var op = www.SendWebRequest();
                while (!op.isDone) await Task.Yield();

                var clip = DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.Play();

            }

        }


        private void WriteIntoFile(Stream stream)
        {
            using (var fileStream = new FileStream(path: $"{Application.persistentDataPath}/audio.mp3", FileMode.Create))
            {
                byte[] buffer = new byte[8 * 1024];
                int bytesRead;

                while ((bytesRead = stream.Read(buffer, offset: 0, count: buffer.Length)) > 0)
                {
                    fileStream.Write(buffer, offset: 0, count: bytesRead);
                }

            }

        }


    }

}
