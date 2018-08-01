# Microsoft Azure Cognitive Services - Speech API Demo
## Speech to Text Demo
This is second demo of Microsoft Azure. However, this is more of a Cognitive
Services demo.

First, we need to create a cognitive services API resource in Azure Portal. If
we choose a wrong API we get an error like below, at a later stage, when we
build/run,

    $ dotnet run
    Say something...
    Recognition status: Canceled
    There was an error, reason: WebSocket Upgrade failed with an authentication
    error (401). Please check the subscription key or the authorization token,
    and the region name.

An example of wrong API, in this context would be to select the Speech API
during creating a resource in the cloud portal that is used to identify
user voice and intents.  Please don't confuse with "Speech Recognition
(previous)" API. Instruction can be found at Azure Docs -
[Create a Speech resource in Azure][1] to select the right API.

To create a console project we do,

    $ dotnet new console -n P2_SpeechRecognition
    The template "Console Application" was created successfully.

    Processing post-creation actions...
    Running 'dotnet restore' on P2_SpeechRecognition\P2_SpeechRecognition.csproj...
      Restoring packages for D:\P2_SpeechRecognition\P2_SpeechRecognition.csproj...
      Generating MSBuild file D:\P2_SpeechRecognition\obj\P2_SpeechRecognition.csproj.nuget.g.props.
      Generating MSBuild file D:\P2_SpeechRecognition\obj\P2_SpeechRecognition.csproj.nuget.g.targets.
      Restore completed in 263.86 ms for D:\P2_SpeechRecognition\P2_SpeechRecognition.csproj.

    Restore succeeded.

We rename `Program.cs` to `Speech.cs`,

    $ Move-Item Program.cs Speech.cs
    $ cd P2_SpeechRecognition

We can run it with code,

    $ code .

Or Visual Studio,    

    $ start devenv .

It is important to get the correct package for it: `Microsoft.CognitiveServices.Speech`.

We add/install this package,
    
    $ dotnet add package Microsoft.CognitiveServices.Speech
      Writing C:\Users\atiqc\AppData\Local\Temp\tmpAD20.tmp
    info : Adding PackageReference for package 'Microsoft.CognitiveServices.Speech' into project 'D:\P2_SpeechRecognition\P2_SpeechRecognition.csproj'.
    log  : Restoring packages for D:\P2_SpeechRecognition\P2_SpeechRecognition.csproj...
    info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.cognitiveservices.speech/index.json
    info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.cognitiveservices.speech/index.json 321ms
    info :   GET https://api.nuget.org/v3-flatcontainer/microsoft.cognitiveservices.speech/0.5.0/microsoft.cognitiveservices.speech.0.5.0.nupkg
    info :   OK https://api.nuget.org/v3-flatcontainer/microsoft.cognitiveservices.speech/0.5.0/microsoft.cognitiveservices.speech.0.5.0.nupkg 270ms
    log  : Installing Microsoft.CognitiveServices.Speech 0.5.0.
    info : Package 'Microsoft.CognitiveServices.Speech' is compatible with all the specified frameworks in project 'D:\P2_SpeechRecognition\P2_SpeechRecognition.csproj'.
    info : PackageReference for package 'Microsoft.CognitiveServices.Speech' version '0.5.0' added to file 'D:\P2_SpeechRecognition\P2_SpeechRecognition.csproj'.

Then, we build/run the console application and we say something for speech
recognition when the console asks to do so,

    $ dotnet run
    Say something...
    We recognized: A cat in the window.
    Please press a key to continue.

## Summary for API
Here is a brief overview,

## References
 - Azure Docs - [Create a Speech resource in Azure][1]
 - Cognitive Services Documentation root for .net - [Microsoft.CognitiveServices.Speech Namespace][2]


  [1]: https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/get-started#create-a-speech-resource-in-azure
  [2]: https://docs.microsoft.com/en-us/dotnet/api/microsoft.cognitiveservices.speech
