using GlobalCalc.Models;

namespace GlobalCalc.Client;

public interface IApiClient
{
    FacadeData GetData();

    List<RemoteImageFile> GetImages();

    Stream GetImage(string file);
}