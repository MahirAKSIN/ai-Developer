using System.Net.Http.Headers;

class Program
{
    const long MaxFileSizeBytes = 25 * 1024 * 1024;

    static async Task Main(string[] args)
    {
        string apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY")
            ?? throw new InvalidOperationException("OPENAI_API_KEY ortam degiskeni tanimli degil.");
        string audioFilePath = Path.Combine(AppContext.BaseDirectory, "audio1.mp3");

        if (!File.Exists(audioFilePath))
        {
            Console.WriteLine($"Ses dosyasi bulunamadi: {audioFilePath}");
            return;
        }

        var fileInfo = new FileInfo(audioFilePath);
        double sizeMb = fileInfo.Length / (1024.0 * 1024.0);

        if (fileInfo.Length > MaxFileSizeBytes)
        {
            Console.WriteLine($"Dosya cok buyuk: {sizeMb:F1} MB (Whisper limiti: 25 MB)");
            return;
        }

        Console.WriteLine($"Dosya boyutu: {sizeMb:F1} MB - transkripsiyon basliyor...");

        using var client = new HttpClient { Timeout = TimeSpan.FromMinutes(10) };
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

        using var form = new MultipartFormDataContent();
        await using var fileStream = File.OpenRead(audioFilePath);

        var audioContent = new StreamContent(fileStream);
        audioContent.Headers.ContentType = MediaTypeHeaderValue.Parse("audio/mpeg");

        form.Add(audioContent, "file", Path.GetFileName(audioFilePath));
        form.Add(new StringContent("whisper-1"), "model");

        try
        {
            var response = await client.PostAsync("https://api.openai.com/v1/audio/transcriptions", form);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Transkripsiyon:");
                Console.WriteLine(result);
            }
            else
            {
                Console.WriteLine($"Hata: {response.StatusCode}");
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine($"Baglanti hatasi: {ex.Message}");
        }
    }
}
