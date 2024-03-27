using System.Text.Json;

public static class OpenAIConfig 
{
    public static string? ApiKey = Environment.GetEnvironmentVariable("OPENAI_KEY"); // this should be a environment variable
    public static string ApiEndpoint = "https://api.openai.com/v1/chat/completions"; // this should be in appsettings.json
}

public class OpenAIService 
{
    public static async Task<string> GetCompletion(string prompt)
    {
       // Esto deberia ser usado con HttpClientFactory 
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {OpenAIConfig.ApiKey}");

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[]
            {
                new { role = "user", content = prompt }
            },
            temperature = 0.7 // Adjust as needed
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");

        var response = await client.PostAsync(OpenAIConfig.ApiEndpoint, content);
        if (response.IsSuccessStatusCode)
        {
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<OpenAIResponse>(jsonResponse);
            return responseData.choices[0].text.Trim();
        }
        else
        {
            var error = response.ReasonPhrase;
            Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
            return error;
        }
    }

}
public class OpenAIChoice
    {
        public string text { get; set; }
    }
public class OpenAIResponse
    {
        public OpenAIChoice[] choices { get; set; }
    }



