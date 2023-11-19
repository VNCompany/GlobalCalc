namespace GlobalCalc.DataLayer.Entities;

public class Profile
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int Type { get; set; }
    public decimal SealPrice { get; set; } 
    public decimal CornerPrice { get; set; } 
}