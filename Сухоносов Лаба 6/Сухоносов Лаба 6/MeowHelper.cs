using System;

public static class MeowHelper
{
    public static void MakeMeow(params IMeowable[] meowables)   
    {
        foreach (var meowable in meowables)
        {
            meowable.Meow();
        }
    }
}
