using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

class Program
{
    static async Task Main(string[] args)
    {
        string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
            ?? throw new InvalidOperationException("OPENAI_API_KEY ortam degiskeni tanimli degil.");
        Console.WriteLine("Prompt girin:");
        string? prompt = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(prompt))
        {
            Console.WriteLine("Prompt bos olamaz.");
            return;
        }

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "gpt-image-1",
            prompt,
            n = 1,
            size = "1024x1024",
            quality = "medium"
        };

        string jsonBody = JsonConvert.SerializeObject(requestBody);
        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("https://api.openai.com/v1/images/generations", content);
        string responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(responseString);
            return;
        }

        var json = JObject.Parse(responseString);
        string? b64 = json["data"]?[0]?["b64_json"]?.ToString();
        string? url = json["data"]?[0]?["url"]?.ToString();

        if (!string.IsNullOrEmpty(b64))
        {
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "generated.png");
            await File.WriteAllBytesAsync(outputPath, Convert.FromBase64String(b64));
            Console.WriteLine($"Gorsel kaydedildi: {outputPath}");
        }
        else if (!string.IsNullOrEmpty(url))
        {
            Console.WriteLine($"Gorsel URL: {url}");
        }
        else
        {
            Console.WriteLine(responseString);
        }

        Console.ReadLine();
    }
    
}
