using Dapper;

using GlobalCalc.DataLayer.Entities;

namespace GlobalCalc.DataLayer.Repositories;

public class ProfilesRepository
{
    private readonly DataContext _c;

    public ProfilesRepository(DataContext context) {  _c = context; }

    public IEnumerable<Profile> GetAll() => 
        _c.Connection.Query<Profile>("SELECT * FROM Profiles;");

    public Profile Get(int id) =>
        _c.Connection.QueryFirst<Profile>("SELECT * FROM Profiles WHERE Id=@Id;", new { Id = id });

}
