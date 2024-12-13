public class CachedFraction : IFraction
{
    private readonly IFraction fraction;
    private double? cachedRealValue;

    public CachedFraction(IFraction fraction)
    {
        this.fraction = fraction;
        cachedRealValue = null;
    }

    public double GetRealValue()
    {
        if (!cachedRealValue.HasValue)
        {
            cachedRealValue = fraction.GetRealValue();
        }
        return cachedRealValue.Value;
    }

    public void SetNumerator(int numerator)
    {
        fraction.SetNumerator(numerator);
        cachedRealValue = null;
    }

    public void SetDenominator(int denominator)
    {
        fraction.SetDenominator(denominator);
        cachedRealValue = null;
    }
}
