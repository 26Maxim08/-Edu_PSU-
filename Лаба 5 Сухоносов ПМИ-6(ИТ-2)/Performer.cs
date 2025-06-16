using System;

/// <summary>
/// Класс, представляющий исполнителя
/// </summary>
public class Performer
{
    public int Code { get; }
    public int Age { get; set; }
    public string Citizenship { get; }

    public Performer(int code, int age, string citizenship)
    {
        Code = code;
        Age = age;
        Citizenship = citizenship ?? throw new ArgumentNullException(nameof(citizenship));
    }

    public override string ToString() => $"Код: {Code}, Возраст: {Age}, Гражданство: {Citizenship}";
}
