using System.IO;

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
        
        foreach (var img in _services.Api.GetImages())
        {
            FileInfo fi = new FileInfo(Path.Combine(AppService.DataPath, img.Key));
            if (!fi.Exists || fi.LastWriteTime < img.Value)
            {
                using (Stream imageStream = _services.Api.GetImage(img.Key))
                {
                    using (FileStream fs = new FileStream(fi.FullName, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buf = new byte[1024];
                        int count;
                        while ((count = imageStream.Read(buf, 0, buf.Length)) != 0)
                            fs.Write(buf, 0, count);
                        
                        fs.Flush();
                    }
                }
            }
        }
    }
    
    public string? GetImageSource(int profileId, bool preview) =>
        _services.Config.UseImages
            ? Path.Combine(AppService.DataPath, $"{profileId}{(preview ? "-preview" : null)}.jpg")
            : null;
}