using System;

public class Dog : IMeowable
{
    private int meowCount;

    public Dog()
    {
        meowCount = 0;
    }

    public void Meow()
    {
        Console.WriteLine("Dog: мяу!");
        meowCount++;
    }

    public int GetMeowCount()
    {
        return meowCount;
    }
}
