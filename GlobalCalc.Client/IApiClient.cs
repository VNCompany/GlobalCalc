using GlobalCalc.Shared;

namespace GlobalCalc.Client;

public interface IApiClient
{
    FacadeData GetData();

    List<RemoteImageFile> GetImages();

    Stream GetImage(string file);
}