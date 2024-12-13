using System;

public class Program
{
    public static void Main()
    {
        // Задание 1: Кот
        Console.WriteLine("Задание 1 (Мау):");
        Cat barsik = new Cat("Барсик");
        barsik.Meow();
        barsik.Meow(3);

        Cat murzik = new Cat("Мурзик");
        Dog rex = new Dog();

        MeowHelper.MakeMeow(barsik, murzik, rex);

        Console.WriteLine($"Барсик мяукнул {barsik.GetMeowCount()} раз.");
        Console.WriteLine($"Мурзик мяукнул {murzik.GetMeowCount()} раз.");
        Console.WriteLine($"Рекс мяукнул {rex.GetMeowCount()} раз. Чтооооооо? Собака мяукает?");

        // Задание 2: Дроби
        Console.WriteLine("Задание 2 (дроби):");
        Fraction f1 = new Fraction(1, 3);
        Fraction f2 = new Fraction(2, 3);
        Fraction f3 = new Fraction(1, 2);

        Console.WriteLine("Операции с дробями:");
        Console.WriteLine($"{f1} + {f2} = {f1 + f2}");
        Console.WriteLine($"{f1} - {f2} = {f1 - f2}");
        Console.WriteLine($"{f1} * {f2} = {f1 * f2}");
        Console.WriteLine($"{f1} / {f2} = {f1 / f2}");

        Console.WriteLine("Операции дробей с числами:");
        Console.WriteLine($"{f1} + 2 = {f1 + 2}");
        Console.WriteLine($"{f1} - 2 = {f1 - 2}");
        Console.WriteLine($"{f1} * 2 = {f1 * 2}");
        Console.WriteLine($"{f1} / 2 = {f1 / 2}");

        // Пример использования метода умножения
        Console.WriteLine("Результаты выполнея умножения:");
        Fraction multiplicationResult = f1.Multiply(f2);
        Console.WriteLine($"{f1} * {f2} = {multiplicationResult}");

        Console.WriteLine("Подсчёт f1.sum(f2).div(f3).minus(5):");
        Fraction result = f1.Sum(f2).Div(f3).Minus(5);
        Console.WriteLine($"{f1} + {f2} / {f3} - 5 = {result}");

        // Сравнение дробей
        Console.WriteLine("Сравнение дробей:");
        Fraction f4 = new Fraction(2, 4);
        Fraction f5 = new Fraction(2, 6);
        Console.WriteLine($"{f1} == {f4} : {f1.CompareTo(f4) == 0}");
        Console.WriteLine($"{f1} == {f5} : {f1.CompareTo(f5) == 0}");

        // Клонирование дроби
        Fraction clonedFraction = (Fraction)f1.Clone();
        Console.WriteLine($"Клонированная дробь: {clonedFraction}");

        // Пример использования интерфейса IFraction с кэшированием
        IFraction fraction = new CachedFraction(new Fraction(3, 4));
        Console.WriteLine($"Вещественное значение дроби: {fraction.GetRealValue()}");
        fraction.SetNumerator(5);
        fraction.SetDenominator(6);
        Console.WriteLine($"Обновленное вещественное значение дроби: {fraction.GetRealValue()}");
    }
}
