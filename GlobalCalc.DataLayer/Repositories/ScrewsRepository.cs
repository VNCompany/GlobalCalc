using Dapper;

using GlobalCalc.DataLayer.Entities;

namespace GlobalCalc.DataLayer.Repositories;

public class ScrewsRepository
{
    private readonly DataContext _c;

    public ScrewsRepository(DataContext context) { _c = context; }

    public IEnumerable<Screw> GetAll() =>
        _c.Connection.Query<Screw>("SELECT * FROM Screws;");

    public Screw Get(int id) =>
        _c.Connection.QueryFirst<Screw>("SELECT * FROM Screws WHERE Id=@Id;", new { Id = id });

    public void Create(Screw item)
    {
        _c.Connection.Execute("INSERT INTO " +
            "Screws (Id, Color, Description, Price) " +
            "VALUES (@Id, @Color, @Description, @Price);", item);
    }
}
