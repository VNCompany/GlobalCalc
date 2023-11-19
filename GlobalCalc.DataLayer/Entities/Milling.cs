namespace GlobalCalc.DataLayer.Entities;

public class Milling
{
    public int Type { get; set; }
    public int ProfileType { get; set; }
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
}
