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
            WorkPrice = _db.Prices.GetPrice(DataLayer.Types.PriceType.WorkPrice),
            Screws = _db.Screws.GetAll().Select(_ => new ScrewMapper().ToModel(_)).ToArray(),
            Millings = _db.Millings.GetAll().Select(_ => new MillingMapper().ToModel(_)).ToArray(),
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
}
