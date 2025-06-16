using System;

/// <summary>
/// Класс, представляющий услугу
/// </summary>
public class Service
{
    public int Code { get; }
    public string Name { get; set; }

    public Service(int code, string name)
    {
        Code = code;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public override string ToString() => $"Код: {Code}, Название: {Name}";
}
