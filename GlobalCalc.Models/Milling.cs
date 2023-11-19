namespace GlobalCalc.Models;

public enum MillingType
{
    None = 0,
    WideProfile = 1,
    NarrowProfile = 2,
    Canopy = 3,
}

public class Milling
{
    public MillingType Type { get; set; }
    public ProfileType ProfileType { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}