using Dapper;

using GlobalCalc.DataLayer.Entities;

namespace GlobalCalc.DataLayer.Repositories;

public class ProfilePositionsRepository
{
    private readonly DataContext _c;

    public ProfilePositionsRepository(DataContext context) { _c = context; }

    public IEnumerable<ProfilePosition> GetAll()
    {
        var profiles = _c.Profiles.GetAll().ToList();
        var colors = _c.ProfileColors.GetAll().ToList();
        var profilePositions = _c.Connection.Query("SELECT * FROM ProfilePositions;");
        return from pos in profilePositions
               select new ProfilePosition
               {
                   Profile = profiles.Single(p => p.Id == pos.ProfileId),
                   Color = colors.Single(c => c.Id == pos.ColorId),
                   Price = pos.Price,
               };
    }

    public void Create(int profileId, int colorId, decimal price)
    {
        _c.Connection.Execute("INSERT INTO " +
            "ProfilePositions (ProfileId, ColorId, Price) " +
            "VALUES (@ProfileId, @ColorId, @Price);"
            , new { ProfileId = profileId, ColorId = colorId, Price = price });
    }

    public void Delete(int profileId, int colorId)
    {
        _c.Connection.Execute("DELETE FROM ProfilePositions " +
            "WHERE ProfileId=@ProfileId AND ColorId=@ColorId;"
            , new { ProfileId = profileId, ColorId = colorId });
    }
}
