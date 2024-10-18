using System;

public class MyClass
{
    private int First, Second, Third;

    public MyClass(int first, int second, int third)
    {
        First = first;
        Second = second;
        Third = third;
    }

    public MyClass(MyClass other)
    {
        First = other.First;
        Second = other.Second;
        Third = other.Third;
    }

    public int MinLast()
    {
        int GetLastDigit(int number) => Math.Abs(number) % 10;
        return Math.Min(Math.Min(GetLastDigit(First), GetLastDigit(Second)), GetLastDigit(Third));
    }

    public override string ToString()
    {
        return $"First: {First}, Second: {Second}, Third: {Third}";
    }
}
public class StringClass : MyClass
{
    private int Str1, Str2, Str3;

    public StringClass(int first, int second, int third, int z, int y, int x)
        : base(first, second, third)
    {
        Str1 = x;
        Str2 = y;
        Str3 = z;
    }

    public StringClass(StringClass other)
        : base(other)
    {
        Str1 = other.Str1;
        Str2 = other.Str2;
        Str3 = other.Str3;
    }

    public int TotalLength()
    {
        return (int)Math.Sqrt(Str1 * Str1 + Str2 * Str2 + Str3 * Str3);
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Str1: {Str1}, Str2: {Str2}, Str3: {Str3}";
    }
}
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Введите три целых числа:");
        if (int.TryParse(Console.ReadLine(), out int x) &&
            int.TryParse(Console.ReadLine(), out int y) &&
            int.TryParse(Console.ReadLine(), out int z))
        {
            TestMyClass(x, y, z);
            TestStringClass(x, y, z);
        }
        else
        {
            Console.WriteLine("Введены неверные данные. Пожалуйста, введите три целых числа.");
        }
        Console.ReadKey();
    }

    static void TestMyClass(int x, int y, int z)
    {
        Console.WriteLine("\nТестирование основного класса MyClass:");
        MyClass myClass = new MyClass(x, y, z);
        Console.WriteLine(myClass);
        Console.WriteLine($"Минимальная последняя цифра = {myClass.MinLast()}");

        MyClass myClassCopy = new MyClass(myClass);
        Console.WriteLine(myClassCopy);
    }

    static void TestStringClass(int x, int y, int z)
    {
        Console.WriteLine("\nТестирование дочернего класса StringClass:");
        
        StringClass stringClass = new StringClass(x, y, z, z, y, x);
        Console.WriteLine(stringClass);
        Console.WriteLine($"Общая длина строк: {stringClass.TotalLength()}");

        StringClass stringClassCopy = new StringClass(stringClass);
        Console.WriteLine(stringClassCopy);
    }
}
