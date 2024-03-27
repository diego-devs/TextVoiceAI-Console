using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

public static class SpeechConfiguration 
{
        // This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
        // Please create with command: setx SPEECH_KEY <key> and setx SPEECH_REGION westus
    public static string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
    public static string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");
}
public class SpeechService {
    public static async Task TextToSpeech(string text) 
    {
        var speechConfig = SpeechConfig.FromSubscription(SpeechConfiguration.speechKey, SpeechConfiguration.speechRegion);      

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = "es-MX-LucianoNeural"; 

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                if (text != null)
                {
                    var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                    OutputSpeechSynthesisResult(speechSynthesisResult, text);
                } 
                else
                {
                    return;
                }
            }
    }
     public static async Task TextToSpeech() {
            var speechConfig = SpeechConfig.FromSubscription(SpeechConfiguration.speechKey, SpeechConfiguration.speechRegion);      

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = "es-MX-LucianoNeural"; 

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Get text from the console and synthesize to the default speaker.
                Console.WriteLine("Enter some text that you want to speak >");
                string text = Console.ReadLine();
                if (text != null)
                {
                    var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                    OutputSpeechSynthesisResult(speechSynthesisResult, text);
                } 
                else
                {
                    return;
                }
            }
        }
        static void OutputSpeechSynthesisResult(SpeechSynthesisResult speechSynthesisResult, string text)
        {
            switch (speechSynthesisResult.Reason)
            {
                case ResultReason.SynthesizingAudioCompleted:
                    Console.WriteLine($"Speech synthesized for text: [{text}]");
                    break;
                case ResultReason.Canceled:
                    var cancellation = SpeechSynthesisCancellationDetails.FromResult(speechSynthesisResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
                default:
                    break;
            }
        }
        public static async Task<string> SpeechToText()
        {
            var speechConfig = SpeechConfig.FromSubscription(SpeechConfiguration.speechKey, SpeechConfiguration.speechRegion);
            speechConfig.SpeechRecognitionLanguage = "es-MX";

            using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
            using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            

            Console.WriteLine("Speak into your microphone.");
            var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();
            OutputSpeechRecognitionResult(speechRecognitionResult); // todo

            return speechRecognitionResult.Text;
        }
        static void OutputSpeechRecognitionResult(SpeechRecognitionResult speechRecognitionResult)
        {
            switch (speechRecognitionResult.Reason)
            {
                case ResultReason.RecognizedSpeech:
                    Console.WriteLine($"RECOGNIZED: Text={speechRecognitionResult.Text}");
                    break;
                case ResultReason.NoMatch:
                    Console.WriteLine($"NOMATCH: Speech could not be recognized.");
                    break;
                case ResultReason.Canceled:
                    var cancellation = CancellationDetails.FromResult(speechRecognitionResult);
                    Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                    if (cancellation.Reason == CancellationReason.Error)
                    {
                        Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                        Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                        Console.WriteLine($"CANCELED: Did you set the speech resource key and region values?");
                    }
                    break;
            }
        }
}
 
