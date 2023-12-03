using Microsoft.AspNetCore.Mvc;

using GlobalCalc.Models;
using GlobalCalc.DataLayer;
using GlobalCalc.Web.BL;

namespace GlobalCalc.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : Controller
{
    private readonly DataContext _db;

    public ApiController(DataContext db)
    {
        _db = db;
    }

    [HttpGet("getData")]
    public FacadeData GetData()
    {
        var data = new FacadeData
        {
            WorkPrice = 450,
            Screws = Array.Empty<Screw>(),
            Millings = Array.Empty<Milling>()
        };
        var profilesBL = new ProfilesBL(_db);
        data.Profiles = profilesBL.GetProfiles().ToArray();

        return data;
    }

    [HttpGet("getImages")]
    public IEnumerable<RemoteImageFile> GetImages()
    {
        var dirInfo = new DirectoryInfo("wwwroot/content/");
        return dirInfo.GetFiles().Select(f => new RemoteImageFile(f.Name, f.LastWriteTime));
    }

    [HttpGet("test")]
    public object Test() => new BL.ProfilesBL(_db).GetProfiles();
}
