namespace GlobalCalc.Models;

public class Screw
{
    public string Color { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public override string ToString() => $"{Color} ({Description})";
}