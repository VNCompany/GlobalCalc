using Dapper;

using GlobalCalc.DataLayer.Entities;

namespace GlobalCalc.DataLayer.Repositories;

public class ProfileColorsRepository
{
    private readonly DataContext _c;

    public ProfileColorsRepository(DataContext context) { _c = context; }

    public IEnumerable<ProfileColor> GetAll() =>
        _c.Connection.Query<ProfileColor>("SELECT * FROM ProfileColors;");

    public ProfileColor Get(int id) =>
        _c.Connection.QueryFirst<ProfileColor>("SELECT * FROM ProfileColors WHERE Id=@Id;", new { Id = id });

}
