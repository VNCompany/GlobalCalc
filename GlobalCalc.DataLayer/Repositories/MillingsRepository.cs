using Dapper;

using GlobalCalc.DataLayer.Entities;

namespace GlobalCalc.DataLayer.Repositories;

public class MillingsRepository
{
    private readonly DataContext _c;

    public MillingsRepository(DataContext context) { _c = context; }

    public IEnumerable<Milling> GetAll() =>
        _c.Connection.Query<Milling>("SELECT * FROM Millings;");
}
