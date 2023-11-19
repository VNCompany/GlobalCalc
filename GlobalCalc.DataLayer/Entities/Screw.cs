namespace GlobalCalc.DataLayer.Entities;

public class Screw
{
    public int Id { get; set; }
    public string Color { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
}
