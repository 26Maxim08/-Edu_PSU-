public class Performer
{
    public int Code { get; set; }
    public int Age { get; set; }
    public string Citizenship { get; set; }

    public Performer(int code, int age, string citizenship)
    {
        Code = code;
        Age = age;
        Citizenship = citizenship;
    }

    public override string ToString()
    {
        return $"Код: {Code}, Возраст: {Age}, Гражданство: {Citizenship}";
    }
}

