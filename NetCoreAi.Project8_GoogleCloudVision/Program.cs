using Google.Cloud.Vision.V1;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Resim yolunu giriniz : ");
        string imagePath = Console.ReadLine();

        Console.WriteLine();

        string creadetialPath = "Api key gelecek";

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATON_CREDENTIALS", creadetialPath);

        try
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);
            Console.WriteLine("Resimdeki Metin");
            Console.WriteLine();

            foreach (var annotation in response)
            {
                if (!string.IsNullOrEmpty(annotation.Description))

                {
                    Console.WriteLine(annotation.Description);
                }
            }
        }
        catch (Exception ex)
        {

            Console.WriteLine($"Bir hata olustu ${ex.Message}");
        }



    }
}