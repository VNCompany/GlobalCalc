using Dapper;

using GlobalCalc.DataLayer.Entities;

namespace GlobalCalc.DataLayer.Repositories;

public class ProfilePositionsRepository
{
    private readonly DataContext _c;

    public ProfilePositionsRepository(DataContext context) { _c = context; }

    public IEnumerable<ProfilePosition> GetAll()
        => _c.Connection.Query<ProfilePosition, Profile, ProfileColor, ProfilePosition>(
            "SELECT pp.*, p.*, pc.* FROM ProfilePositions pp " +
            "INNER JOIN Profiles p ON p.Id = pp.ProfileId " +
            "INNER JOIN ProfileColors pc ON pc.Id = pp.ColorId;"
            , map: (profilePosition, profile, profileColor) =>
            {
                profilePosition.Profile = profile;
                profilePosition.Color = profileColor;
                return profilePosition;
            });
}
