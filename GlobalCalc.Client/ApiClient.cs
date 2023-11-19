using System.Text.Json;

using GlobalCalc.Models;

namespace GlobalCalc.Client;

public class ApiClient : IApiClient
{
    private readonly Uri _host;

    public ApiClient(string host)
    {
        _host = new Uri(host, UriKind.Absolute);
    }

    public FacadeData GetData()
    {
        using HttpClient client = new HttpClient();
        string content = client.GetAsync(new Uri(_host, "api/getData"))
            .Result.Content.ReadAsStringAsync().Result;
        return JsonSerializer.Deserialize<FacadeData>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
    }

    public Dictionary<string, DateTime> GetImages()
    {
        using HttpClient client = new HttpClient();
        string content = client.GetAsync(new Uri(_host, "api/getImages"))
            .Result.Content.ReadAsStringAsync().Result;
        var deserialized = JsonSerializer.Deserialize<IEnumerable<KeyValuePair<string, DateTime>>>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        return new Dictionary<string, DateTime>(deserialized);
    }

    public Stream GetImage(string file)
    {
        using HttpClient client = new HttpClient();
        return client.GetAsync(new Uri(_host, string.Concat("content/", file)))
            .Result.Content.ReadAsStream();
    }
}