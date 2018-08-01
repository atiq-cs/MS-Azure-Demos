# Microsoft Azure Cognitive Services - Speech API Demo
## Speech to Text Demo
This is second demo of Microsoft Azure. However, this is more of a Cognitive
Services demo.

First, we need to create a cognitive services API resource in Azure Portal. If
we choose a wrong API we get an error, at a later stage, when we build/run the
application, like below,

    $ dotnet run
    Say something...
    Recognition status: Canceled
    There was an error, reason: WebSocket Upgrade failed with an authentication
    error (401). Please check the subscription key or the authorization token,
    and the region name.

An example of wrong API, in this context would be to select the Speech API
during creating a resource in the cloud portal that is used to identify
user voice and intents. Hence, it's important not to confuse with "Speech
Recognition (preview)" API. Instruction can be found at Azure Docs -
[Create a Speech resource in Azure][1] to select the right API as well.

Once we created the cognitive servies API resource for Speech (preview), we
create a console project,

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

I have slightly changed the original code to include the method under
appropriate class. Also, we avoid the Wait call, which, if mistaken can create
deadlocks. However, this require marking our main method as async. Hence,
enabling latest language features is required.

To be able to change main method to `async` we have to change lang version,

    <PropertyGroup>
      ...
      <LangVersion>Latest</LangVersion>
    </PropertyGroup>

## Documentation
Documentation link below is useful because there are a number of documentation
available for different frameworks such as old .net, Bing. This page will
provide documentation resources as well along with issues with screenshots,
time to time.

## Summary of API
Here is a brief overview,
Microsoft.CognitiveServices.Speech Namespace contains 17 classes. SpeechFactory
is used for speech to text recognition.

To authenticate SpeechFactory has 3 ways,

 - FromSubscription
 - FromAuthorizationToken
 - FromEndpoint

class SpeechFactory has following methods to instatiate an action,

 - CreateIntent* (6 overrides)
 - CreateSpeechRecognizer* (9 overrides)
 - CreateTranslationRecognizer* (6 overrides)

5 Properties,

 - AuthorizationToken	
 - EndpointURL	
 - Parameters	
 - Region	
 - SubscriptionKey	

2 types of results,
 - SpeechRecognitionResult
 - DetailedSpeechRecognitionResult

There is 10 seconds cut off for short duration recognition.

## Epilogue
As we can see we can perform speech to text recognition for short duration with
the API call demonstrated above. Similarly, long running recognition can
be performed using `StartContinuousRecognitionAsync`. API reference also shows
the capacity of doing it from streams which make live streaming and dictation
possible.

As the ASR stats showed, Microsoft and Google are pretty close in error rates
of word recognition, IBM seems to be lagging behind a bit.

This Speech API and Google cloud Speech API both support streaming and
recognition from audio files. However, supported audio formats are different.

If you are interested in Google Cloud Speech API demo using .net core please
refer to my previous demo on google cloud speech API which peacefully resides
at, [my github - .net core google cloud speech][3]

## References
 - Azure Docs - [Create a Speech resource in Azure][1]
 - Cognitive Services Documentation root for .net - [Microsoft.CognitiveServices.Speech Namespace][2]


  [1]: https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/get-started#create-a-speech-resource-in-azure
  [2]: https://docs.microsoft.com/en-us/dotnet/api/microsoft.cognitiveservices.speech
  [3]: https://github.com/atiq-cs/net-core-google-cloud/tree/master/speech
