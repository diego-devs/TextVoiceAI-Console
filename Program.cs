using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace TextVoiceAI
{
   
    public class Program
    {
        static async Task Main(string[] args)
        {
            await Assistant.RunAssistant();
            //await ProgramLoop();
        }

        static async Task ProgramLoop() 
        {
            Console.WriteLine("Select an option: ");
            Console.WriteLine("1. Text to audio");
            Console.WriteLine("2. Audio to text");
            int x = int.Parse(Console.ReadLine());

            switch (x)
            {
                case 1:
                    await SpeechService.TextToSpeech();
                    break;
                case 2:
                    await SpeechService.SpeechToText();
                    break;
                default:
                    Console.WriteLine("Not a valid option.");
                    break;
            }
            
        }
    
       


    }
    
}