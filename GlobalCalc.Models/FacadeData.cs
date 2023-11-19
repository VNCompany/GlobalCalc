namespace GlobalCalc.Models;

public class FacadeData
{
    public decimal WorkPrice { get; set; }
    public Profile[] Profiles { get; set; } = null!;
    public Screw[] Screws { get; set; } = null!;
    public Milling[] Millings { get; set; } = null!;
}