using GlobalCalc.DataLayer;

namespace GlobalCalc.Web.BL;

internal class ProfilesBL
{
    private readonly DataContext _db;

    public ProfilesBL(DataContext db)
    {
        _db = db;
    }
}
