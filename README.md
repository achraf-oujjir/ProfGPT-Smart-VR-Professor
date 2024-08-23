# 👨‍🏫🤖 ProfGPT: Virtual Reality Education with AI-powered Virtual Teacher 🧠

## 🎯 Project Overview
ProfGPT is a virtual reality (VR) educational application developed for Oculus Quest 2. It offers an immersive learning experience for users to explore and experiment with electrical circuits in a virtual lab environment. The highlight of the application is ProfGPT, an AI-powered virtual teacher who guides users through the learning process using natural language interaction (English or French).

## 🚀 Features

- 🔌 **Interactive Electrical Circuits Lab**: Experiment with a wide range of electrical components to build and explore circuits in a realistic 3D setting.
- 🤖 **AI-powered Virtual Instructor**: ProfGPT guides users with spoken explanations, utilizing ChatGPT, Whisper, and AWS Polly to answer questions.
- 🎮 **Immersive VR Learning**: Designed for Oculus Quest 2, offering an engaging and hands-on learning experience in virtual reality.
- 🗣️ **Natural Language Assistance**: Voice queries are seamlessly processed via Whisper, ChatGPT, and AWS Polly for a smooth interactive experience.

## 🏗️ Architecture
The application architecture is divided into two key components:
1. **Virtual Lab Table**: A space where users can interact with various electrical components to build circuits.
2. **ProfGPT**: An intelligent virtual teacher built using APIs for speech recognition, natural language processing, and text-to-speech services.

The architecture leverages several APIs:
- **Whisper** (speech-to-text)
- **ChatGPT** (AI conversational responses)
- **AWS Polly** (text-to-speech)

![Architecture Diagram](profgpt-architecture.png)

  ## Implementation
ProfGPT uses a modular architecture where each component is responsible for specific interactions:

1. **Speech Recognition**: The user records their voice, which is sent to Whisper by OpenAI for transcription.
2. **AI Interaction**: The transcribed text is sent to ChatGPT to generate a response.
3. **Speech Synthesis**: The AI's response is converted back into speech using AWS Polly and played for the user.


### 🔑 Key Technologies:
- Unity for the VR environment
- OpenAI's Whisper and ChatGPT for voice interaction
- AWS Polly for text-to-speech synthesis

## 🔧 APIs & Services Used
- **Whisper**: Converts audio input to text using state-of-the-art speech recognition.
- **AWS Polly**: Provides natural-sounding text-to-speech conversion to generate responses from ProfGPT.
- **ChatGPT**: AI-powered responses to user queries, allowing natural language interaction.

## 📦 Plugins & Packages
- **ReadyPlayerMe**: Used to create the ProfGPT avatar.
- **OpenAI-Unity**: For integrating OpenAI models (Whisper and ChatGPT) with Unity.
- **XR Interaction Toolkit**: Facilitates interactions within the VR environment.
- **Mixamo**: Provides animations for ProfGPT’s avatar.

## 🧪 Virtual Lab Table
The virtual lab table contains various components for users to build electrical circuits. Users can interact with components like:
- 💡 Bulbs
- 🔋 Batteries
- 🌞 Solar Panels

These components can be picked up and placed on the lab table to form functioning circuits.

![Lab Table](path_to_lab_table_image.png)

## 🤖 ProfGPT: Avatar & Configuration
ProfGPT is designed using the ReadyPlayerMe platform, and the avatar is integrated with Unity via the ReadyPlayerMe SDK. The avatar can:
- Hear user queries using Whisper.
- Respond to users using ChatGPT and AWS Polly.

The following scripts are essential:
- `ProfFrGPT.cs`: Manages interactions between the user and ProfGPT, including sending audio to APIs and handling responses.
- `ProfUI.cs`: Manages the GUI for recording voice input and displaying ProfGPT’s responses.

![ProfGPT Avatar](path_to_avatar_image.png)

## 📸 Media



## 📚 References

- [Faraday Virtual Lab Table](https://github.com/Schackasawa/faraday) - Credit to the creator for providing the virtual lab table asset used in this project.
- [OpenAI-Unity GitHub Repo](https://github.com/srcnalt/OpenAI-Unity) - Credit to the developer for making it easier to integrate OpenAI's models with Unity.
- [Sgt3v YouTube Channel](https://www.youtube.com/@sgt3v) - Special thanks to Sgt3v for providing invaluable tutorials on developing smart VR NPCs powered by GPT. Check out his channel for amazing content!


## ⚠️ Notice

**This project is incomplete.** The repository contains all the files I could retrieve from the original project. Due to the age of the project, some assets and code might be missing or outdated. Please take this into consideration when reviewing or attempting to use the code.

