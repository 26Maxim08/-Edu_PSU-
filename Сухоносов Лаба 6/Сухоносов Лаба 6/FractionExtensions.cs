public static class FractionExtensions
{
    public static Fraction Sum(this Fraction f1, Fraction f2)
    {
        return f1 + f2;
    }

    public static Fraction Div(this Fraction f1, Fraction f2)
    {
        return f1 / f2;
    }

    public static Fraction Minus(this Fraction f1, int n)
    {
        return f1 - n;
    }

    public static Fraction Multiply(this Fraction f1, Fraction f2)
    {
        return f1 * f2;
    }
}
