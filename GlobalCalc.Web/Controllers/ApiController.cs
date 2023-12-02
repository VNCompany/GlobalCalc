using Microsoft.AspNetCore.Mvc;

using GlobalCalc.Models;
using GlobalCalc.DataLayer;
using GlobalCalc.Web.Extensions;

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
            Screws = _db.Screws.GetAll().Select(s => s.ToModel()).ToArray(),
            Millings = _db.Millings.GetAll().Select(m => m.ToModel()).ToArray(),
        };
        var profiles = from pp in _db.ProfilePositions.GetAll()
                group pp by pp.Profile into profileGroup
                select profileGroup.Key.ToModel(
                    from pos in profileGroup
                    select pos.Color.ToModel(pos.Price)
                );
        data.Profiles = profiles.ToArray();

        return data;
    }

    [HttpGet("getImages")]
    public IEnumerable<RemoteImageFile> GetImages()
    {
        var dirInfo = new DirectoryInfo("wwwroot/content/");
        return dirInfo.GetFiles().Select(f => new RemoteImageFile(f.Name, f.LastWriteTime));
    }
}
