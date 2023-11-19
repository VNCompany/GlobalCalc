using System.Runtime.CompilerServices;

namespace GlobalCalc.Operations;

public static class FacadeCalculator
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static double GetPerimeter(Size size) => (size.Width + size.Height) * 2;

    public static CalculatorResult CalculateFacadePrice(
        decimal workPrice
        , Size mmSize
        , decimal profilePrice
        , decimal? sealPrice
        , decimal cornerPrice
        , decimal screwPrice
        , decimal millingPrice
        , int holesCount
    )
    {
        var result = new CalculatorResult
        {
            WorkPrice = workPrice,
            ProfileSize = SpecialMath.MillimetersToMeters(mmSize)
        };
        result.Perimeter = GetPerimeter(result.ProfileSize);
        result.FacadePrice = profilePrice * (decimal)result.Perimeter;
        result.SealPrice = sealPrice == null ? 0 : sealPrice.Value * (decimal)result.Perimeter;
        result.CornersPrice = cornerPrice * 4;
        result.ScrewsPrice = screwPrice * 8;
        result.MillingPrice = millingPrice * holesCount;
        result.TotalPrice = result.WorkPrice
                            + result.FacadePrice
                            + result.SealPrice
                            + result.CornersPrice
                            + result.ScrewsPrice
                            + result.MillingPrice;
        return result;
    }
}