namespace GlobalCalc.Models;

public class ProfileColor
{
    public int Id { get; set; }
    public string Article { get; set; } = null!;
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public override string ToString() => $"{Article} {Name}";
}