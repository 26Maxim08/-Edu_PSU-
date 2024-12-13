using System;

public class Fraction : IFraction
{
    public int Numerator { get; private set; }
    public int Denominator { get; private set; }

    public Fraction(int numerator, int denominator)
    {
        if (denominator < 0)
        {
            throw new ArgumentException("Знаменатель не может быть отрицательным.");
        }

        Numerator = numerator;
        Denominator = denominator;
        Simplify();
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }

    public double GetRealValue()
    {
        return (double)Numerator / Denominator;
    }

    public void SetNumerator(int numerator)
    {
        Numerator = numerator;
        Simplify();
    }

    public void SetDenominator(int denominator)
    {
        if (denominator < 0)
        {
            throw new ArgumentException("Знаменатель не может быть отрицательным.");
        }

        Denominator = denominator;
        Simplify();
    }

    public static Fraction operator +(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.Numerator * f2.Denominator + f2.Numerator * f1.Denominator;
        int newDenominator = f1.Denominator * f2.Denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator -(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.Numerator * f2.Denominator - f2.Numerator * f1.Denominator;
        int newDenominator = f1.Denominator * f2.Denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator *(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.Numerator * f2.Numerator;
        int newDenominator = f1.Denominator * f2.Denominator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator /(Fraction f1, Fraction f2)
    {
        int newNumerator = f1.Numerator * f2.Denominator;
        int newDenominator = f1.Denominator * f2.Numerator;
        return new Fraction(newNumerator, newDenominator);
    }

    public static Fraction operator +(Fraction f, int n)
    {
        return new Fraction(f.Numerator + n * f.Denominator, f.Denominator);
    }

    public static Fraction operator -(Fraction f, int n)
    {
        return new Fraction(f.Numerator - n * f.Denominator, f.Denominator);
    }

    public static Fraction operator *(Fraction f, int n)
    {
        return new Fraction(f.Numerator * n, f.Denominator);
    }

    public static Fraction operator /(Fraction f, int n)
    {
        return new Fraction(f.Numerator, f.Denominator * n);
    }

    private void Simplify()
    {
        int gcd = GCD(Numerator, Denominator);
        Numerator /= gcd;
        Denominator /= gcd;
    }

    private int GCD(int a, int b)
    {
        return b == 0 ? a : GCD(b, a % b);
    }

    public object Clone()
    {
        return new Fraction(Numerator, Denominator);
    }

    public int CompareTo(Fraction other)
    {
        if (other == null) return 1;

        double thisValue = GetRealValue();
        double otherValue = other.GetRealValue();

        if (thisValue > otherValue) return 1;
        if (thisValue < otherValue) return -1;
        return 0;
    }
}
