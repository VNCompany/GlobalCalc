using System.Net.Http.Json;
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

    public FacadeData GetData() => GetApiDataAsync<FacadeData>("getData").Result!;

    public List<RemoteImageFile> GetImages()
        => GetApiDataAsync<List<RemoteImageFile>>("getImages").Result!;

    public Stream GetImage(string file)
    {
        using HttpClient client = new HttpClient();
        return client.GetAsync(new Uri(_host, string.Concat("content/", file)))
            .Result.Content.ReadAsStream();
    }

    private async Task<T?> GetApiDataAsync<T>(string endpoint)
    {
        using HttpClient client = new HttpClient();
        return await client.GetFromJsonAsync<T>(new Uri(_host, $"{endpoint}")
            , new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}