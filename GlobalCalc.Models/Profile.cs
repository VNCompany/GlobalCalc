namespace GlobalCalc.Models;

public enum ProfileType
{
    Wide = 1,
    Narrow = 2
}

public class Profile
{
    public int Id { get; set; }
    public ProfileType Type { get; set; }
    public string Name { get; set; } = null!;
    public decimal SealPrice { get; set; }
    public decimal CornerPrice { get; set; }
    public ProfileColor[] Colors { get; set; } = null!;
}
