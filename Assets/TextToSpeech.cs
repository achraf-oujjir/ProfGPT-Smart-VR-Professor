using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Polly;
using Amazon.Polly.Model;
using Amazon.Runtime;
using UnityEngine;
using UnityEngine.Networking;



public class TextToSpeech : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    // Start is called before the first frame update
    private async void Start()
    {

        //Client creation
        var credentials = new BasicAWSCredentials(accessKey: "", secretKey: "");
        var client = new AmazonPollyClient(credentials, RegionEndpoint.EUCentral1);




        //SpeechRequest
        var request = new SynthesizeSpeechRequest()
        {
            Text = "This is a pretty cool technology !!",
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

            while((bytesRead=stream.Read(buffer, offset:0, count:buffer.Length)) > 0)
            {
                fileStream.Write(buffer, offset: 0, count: bytesRead);
            }

        }

    }

   
}
