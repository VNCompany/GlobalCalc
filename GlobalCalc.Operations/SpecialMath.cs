namespace GlobalCalc.Operations;

public static class SpecialMath
{
    public static Size MillimetersToMeters(Size mmSize)
    {
        return new Size(
            Math.Ceiling(mmSize.Width / 10) / 100,
            Math.Ceiling(mmSize.Height / 10) / 100);
    }

    public static decimal RoundPrice(decimal price)
    {
        if (price == 0)
            return 0;
        
        price = Math.Ceiling(price);
        return Math.Ceiling(price / 5) * 5;
    }
}
