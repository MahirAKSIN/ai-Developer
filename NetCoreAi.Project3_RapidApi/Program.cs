using NetCoreAi.Project3_RapidApi.ViewModels;
using Newtonsoft.Json;

var client = new HttpClient();
List<ApiSeriesModels> apiSeriesModels = new List<ApiSeriesModels>();

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
    var apiResponse = JsonConvert.DeserializeObject<ApiSeriesResponse>(body);
    apiSeriesModels = apiResponse?.result ?? new List<ApiSeriesModels>();
    foreach (var apiSeries in apiSeriesModels)
    {
        Console.WriteLine(apiSeries.rank + "" + apiSeries.Series_Title + "" + apiSeries.Released_Year + "" + apiSeries.IMDB_Rating);

    }

}
Console.ReadLine();