using System;

using GlobalCalc.UI.Services;
using GlobalCalc.Client;

namespace GlobalCalc.UI;

internal class ServicesManager : IDisposable
{
    public static readonly ServicesManager Services = new();

    private ConfigService? _configService;
    public ConfigService Config =>
        _configService ??= ConfigService.Load();


    private IApiClient? _apiClientService;
    public IApiClient Api
    {
        get
        {
#if DEBUG
            return _apiClientService ??= new Test.ApiClientTest();
#else
            return _apiClientService ??= new ApiClient(Config.Host);
#endif
        }
    }

    private AppService? _appService;
    public AppService App =>
        _appService ??= new AppService(this);


    private ImagesService? _imagesService;
    public ImagesService Images =>
        _imagesService ??= new ImagesService(this);


    public void Dispose()
    {
#if !DEBUG
        _appService?.Dispose();
#endif
    }
}
