namespace GlobalCalc.DataLayer.Entities;

public class ProfilePosition
{
    public Profile Profile { get; set; } = null!;
    public ProfileColor Color { get; set; } = null!;
    public decimal Price { get; set; }
}
