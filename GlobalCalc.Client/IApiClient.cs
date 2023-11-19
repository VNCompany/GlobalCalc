using GlobalCalc.Models;

namespace GlobalCalc.Client;

public interface IApiClient
{
    FacadeData GetData();

    Dictionary<string, DateTime> GetImages();

    Stream GetImage(string file);
}