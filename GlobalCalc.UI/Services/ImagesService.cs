using System;
using System.IO;

using GlobalCalc.UI.Helpers;

namespace GlobalCalc.UI.Services;

internal class ImagesService
{
    private readonly ServicesManager _services;
    
    public ImagesService(ServicesManager services)
    {
        _services = services;
    }

    public void Initialize()
    {
        if (!_services.Config.UseImages)
            return;
        
        try
        {
            foreach (var img in _services.Api.GetImages())
            {
                FileInfo fi = new FileInfo(Path.Combine(AppService.DataPath, img.Name));
                if (!fi.Exists || fi.LastWriteTime < img.LastWriteTime)
                {
                    System.Diagnostics.Trace.WriteLine($"Load image: {img.Name}");
                    using Stream loadStream = _services.Api.GetImage(img.Name);
                    ImagesHelper.LoadImageFromStream(loadStream, fi.FullName);
                }
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Trace.WriteLine($"ImageService error: {ex.Message}");
        }
    }
    
    public string? GetImageSource(int profileId, bool preview) =>
        _services.Config.UseImages
            ? Path.Combine(AppService.DataPath, $"{profileId}{(preview ? "-preview" : null)}.jpg")
            : null;
}