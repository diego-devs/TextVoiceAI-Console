using System.Net;
using System.Security.Cryptography.X509Certificates;

public class Assistant 
{
    public static async Task RunAssistant() 
    {
        while (true) 
        {
            Console.WriteLine("Tú: ");
            string userMessage = await SpeechService.SpeechToText();

            Console.WriteLine("Assistant: ");
            string response = await OpenAIService.GetCompletion(userMessage);

            Console.WriteLine(response);

            await SpeechService.TextToSpeech(response);
        }
    }
}