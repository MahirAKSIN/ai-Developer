using NetCoreAi.Project3._1_RapidApi.ViewModels;
using Newtonsoft.Json;

var client = new HttpClient();

List<ApieriesViewModels> apieriesViewModels = new List<ApieriesViewModels>();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get,
    RequestUri = new Uri("https://imdb-top-1000-movies-series.p.rapidapi.com/list/1"),
    Headers =
    {
        { "x-rapidapi-key", "f1659e6c36msh29d48e118befa8bp1236fdjsnf104f73c4025" },
        { "x-rapidapi-host", "imdb-top-1000-movies-series.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    var apiResponse = JsonConvert.DeserializeObject<ApieriesResponse>(body);
    apieriesViewModels = apiResponse?.result ?? new List<ApieriesViewModels>();
    foreach (var item in apieriesViewModels)
    {
        Console.WriteLine(item.rank + " " + item.Series_Title + " " + item.Released_Year + " " + item.IMDB_Rating);
    }
}
Console.ReadLine();
