public class Service
{
    public int Code { get; set; }
    public string Name { get; set; }

    public Service(int code, string name)
    {
        Code = code;
        Name = name;
    }

    public override string ToString()
    {
        return $"Код: {Code}, Название: {Name}";
    }
}
