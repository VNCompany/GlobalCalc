using Microsoft.Data.Sqlite;

using GlobalCalc.DataLayer.Repositories;

namespace GlobalCalc.DataLayer;

public class DataContext : IDisposable
{
    private readonly SqliteConnection _conn = null!;

    public SqliteConnection Connection => _conn;

    // ProfilesRepository
    private ProfilesRepository? _profiles;
    public ProfilesRepository Profiles =>
        _profiles ??= new ProfilesRepository(this);

    // ProfileColorsRepository
    private ProfileColorsRepository? _profileColors;
    public ProfileColorsRepository ProfileColors =>
        _profileColors ??= new ProfileColorsRepository(this);

    // ProfilePositionsRepository
    private ProfilePositionsRepository? _profilePositions;
    public ProfilePositionsRepository ProfilePositions =>
        _profilePositions ??= new ProfilePositionsRepository(this);

    // ScrewsRepository
    private ScrewsRepository? _screws;
    public ScrewsRepository Screws =>
        _screws ??= new ScrewsRepository(this);

    // MillingsRepository
    public MillingsRepository? _millings;
    public MillingsRepository Millings =>
        _millings ??= new MillingsRepository(this);

    // PricesRepository
    public PricesRepository? _prices;
    public PricesRepository Prices =>
        _prices ??= new PricesRepository(this);

    public DataContext()
    {
        bool needInit = !File.Exists("db.sqlite");
        _conn = new SqliteConnection("Filename=db.sqlite");
        _conn.Open();
        if (needInit) InitializeDb();
    }

    public void Dispose()
    {
        _conn.Dispose();
    }

    private void InitializeDb()
    {
        using (var cmd = _conn.CreateCommand())
        {
            cmd.CommandText = Resources.InitDatabaseQueryString;
            cmd.ExecuteNonQuery();
        }
    }
}
