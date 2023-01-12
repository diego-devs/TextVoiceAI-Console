using Microsoft.CognitiveServices.Speech;

namespace TextVoiceAI
{
    public static class AppConfiguration 
    {
            // This example requires environment variables named "SPEECH_KEY" and "SPEECH_REGION"
            // Please create with command: setx SPEECH_KEY <key> and setx SPEECH_REGION westus
        public static string speechKey = Environment.GetEnvironmentVariable("SPEECH_KEY");
        public static string speechRegion = Environment.GetEnvironmentVariable("SPEECH_REGION");
    }
    public class Program
    {
        
        static async Task Main(string[] args)
        {
            await ProgramLoop();
        }
        static async Task ProgramLoop() 
        {
            await SpeechText();
            System.Console.WriteLine("Try again y/n");
            var r = Console.ReadLine();
            if (r == "y" || r == "Y") 
            {
                await ProgramLoop();
            } else
            {
                Environment.Exit(0);
            }
        }
    
        static async Task SpeechText() {
            var speechConfig = SpeechConfig.FromSubscription(AppConfiguration.speechKey, AppConfiguration.speechRegion);      

            // The language of the voice that speaks.
            speechConfig.SpeechSynthesisVoiceName = "es-MX-LucianoNeural"; 

            using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
            {
                // Get text from the console and synthesize to the default speaker.
                Console.WriteLine("Enter some text that you want to speak >");
                string text = Console.ReadLine();

                var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                OutputSpeechSynthesisResult(speechSynthesisResult, text);
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

    
    }
    
}