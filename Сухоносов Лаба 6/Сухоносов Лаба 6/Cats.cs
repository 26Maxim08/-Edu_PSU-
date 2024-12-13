using System;
using System.Linq;

public class Cat : IMeowable
{
    public string Name { get; private set; }
    private int meowCount;

    public Cat(string name)
    {
        Name = name;
        meowCount = 0;
    }

    public override string ToString()
    {
        return $"кот: {Name}";
    }

    public void Meow()
    {
        Console.WriteLine($"{Name}: мяу!");
        meowCount++;
    }

    // Метод для мяуканья кота N раз
    public void Meow(int N)
    {
        if (N <= 0)
        {
            throw new ArgumentException("Количество мяуканий должно быть положительным числом.");
        }
        string meows = string.Join("-", Enumerable.Repeat("мяу", N));
        Console.WriteLine($"{Name}: {meows}!");
        meowCount += N;
    }

    public int GetMeowCount()
    {
        return meowCount;
    }
}
    