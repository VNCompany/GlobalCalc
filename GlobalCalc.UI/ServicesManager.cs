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

    public IApiClient Api =>
        _apiClientService ??= new Test.ApiClientTest();
    
    
    private AppService? _appService;

    public AppService App =>
        _appService ??= new AppService(this);


    private ImagesService? _imagesService;

    public ImagesService Images =>
        _imagesService ??= new ImagesService(this);
    
    
    private ServicesManager() { }

        
    public void Dispose()
    {
#if !DEBUG
        _configService?.Dispose();
        _appService?.Dispose();
#endif
    }
}
