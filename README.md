# 👨‍🏫🤖 ProfGPT: Virtual Reality Education with AI-powered Virtual Teacher 🧠

## Project Overview
ProfGPT is a virtual reality (VR) educational application developed for Oculus Quest 2. It offers an immersive learning experience for users to explore and experiment with electrical circuits in a virtual lab environment. The highlight of the application is ProfGPT, an AI-powered virtual teacher who guides users through the learning process using natural language interaction (English or French).

## Features

- 🔌 **Interactive Electrical Circuits Lab**: Experiment with a wide range of electrical components to build and explore circuits in a realistic 3D setting.
- 🤖 **AI-powered Virtual Instructor**: ProfGPT guides users with spoken explanations, utilizing ChatGPT, Whisper, and AWS Polly to answer questions.
- 🎮 **Immersive VR Learning**: Designed for Oculus Quest 2, offering an engaging and hands-on learning experience in virtual reality.
- 🗣️ **Natural Language Assistance**: Voice queries are seamlessly processed via Whisper, ChatGPT, and AWS Polly for a smooth interactive experience.

## Architecture
The application architecture is divided into two key components:
1. **Virtual Lab Table**: A space where users can interact with various electrical components to build circuits.
2. **ProfGPT**: An intelligent virtual teacher built using APIs for speech recognition, natural language processing, and text-to-speech services.

The architecture leverages several APIs:
- **Whisper** (speech-to-text)
- **ChatGPT** (AI conversational responses)
- **AWS Polly** (text-to-speech)

![Architecture Diagram](profgpt-architecture.png)
  
