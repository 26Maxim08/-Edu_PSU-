using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;
using Aspose.Cells.Drawing;

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

public class Order
{
    public int OrderCode { get; set; }
    public int ServiceCode { get; set; }
    public int PerformerCode { get; set; }
    public decimal Cost { get; set; }

    public Order(int orderCode, int serviceCode, int performerCode, decimal cost)
    {
        OrderCode = orderCode;
        ServiceCode = serviceCode;
        PerformerCode = performerCode;
        Cost = cost;
    }

    public override string ToString()
    {
        return $"Код заказа: {OrderCode}, Код услуги: {ServiceCode}, Код исполнителя: {PerformerCode}, Стоимость: {Cost}";
    }
}

public class FreelanceData
{
    private List<Performer> performers;
    private List<Service> services;
    private List<Order> orders;
    private string logFilePath;

    public FreelanceData(string logFilePath)
    {
        performers = new List<Performer>();
        services = new List<Service>();
        orders = new List<Order>();
        this.logFilePath = logFilePath;
    }

    public void ReadDatabase(string filePath)
    {
        try
        {
            Workbook workbook = new Workbook(filePath);

            // Проверка наличия листов
            Worksheet performersSheet = workbook.Worksheets["Исполнители"];
            if (performersSheet == null)
            {
                LogAction("Лист 'Исполнители' не найден.");
                return;
            }

            Worksheet servicesSheet = workbook.Worksheets["Услуги"];
            if (servicesSheet == null)
            {
                LogAction("Лист 'Услуги' не найден.");
                return;
            }

            Worksheet ordersSheet = workbook.Worksheets["Заказы"];
            if (ordersSheet == null)
            {
                LogAction("Лист 'Заказы' не найден.");
                return;
            }

            // Чтение исполнителей
            for (int i = 2; i <= performersSheet.Cells.MaxDataRow + 1; i++)
            {
                if (performersSheet.Cells[i, 0].Value != null && int.TryParse(performersSheet.Cells[i, 0].Value.ToString(), out int code) &&
                    performersSheet.Cells[i, 1].Value != null && int.TryParse(performersSheet.Cells[i, 1].Value.ToString(), out int age))
                {
                    string citizenship = performersSheet.Cells[i, 2].Value?.ToString() ?? string.Empty;
                    performers.Add(new Performer(code, age, citizenship));
                }
                else
                {
                    LogAction($"Ошибка при разборе данных исполнителя в строке {i}");
                }
            }

            // Чтение услуг
            for (int i = 2; i <= servicesSheet.Cells.MaxDataRow + 1; i++)
            {
                if (servicesSheet.Cells[i, 0].Value != null && int.TryParse(servicesSheet.Cells[i, 0].Value.ToString(), out int code))
                {
                    string name = servicesSheet.Cells[i, 1].Value?.ToString() ?? string.Empty;
                    services.Add(new Service(code, name));
                }
                else
                {
                    LogAction($"Ошибка при разборе данных услуги в строке {i}");
                }
            }

            // Чтение заказов
            for (int i = 2; i <= ordersSheet.Cells.MaxDataRow + 1; i++)
            {
                if (ordersSheet.Cells[i, 0].Value != null && int.TryParse(ordersSheet.Cells[i, 0].Value.ToString(), out int orderCode) &&
                    ordersSheet.Cells[i, 1].Value != null && int.TryParse(ordersSheet.Cells[i, 1].Value.ToString(), out int serviceCode) &&
                    ordersSheet.Cells[i, 2].Value != null && int.TryParse(ordersSheet.Cells[i, 2].Value.ToString(), out int performerCode) &&
                    ordersSheet.Cells[i, 3].Value != null && decimal.TryParse(ordersSheet.Cells[i, 3].Value.ToString().Replace("р.", "").Replace(" ", ""), out decimal cost))
                {
                    orders.Add(new Order(orderCode, serviceCode, performerCode, cost));
                }
                else
                {
                    LogAction($"Ошибка при разборе данных заказа в строке {i}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
            LogAction($"Ошибка при чтении файла: {ex.Message}");
        }
    }

    public void ViewDatabase()
    {
        Console.WriteLine("Исполнители:");
        foreach (var performer in performers)
        {
            Console.WriteLine(performer);
        }

        Console.WriteLine("Услуги:");
        foreach (var service in services)
        {
            Console.WriteLine(service);
        }

        Console.WriteLine("Заказы:");
        foreach (var order in orders)
        {
            Console.WriteLine(order);
        }
    }

    public void DeleteElement(string table, int code)
    {
        switch (table.ToLower())
        {
            case "исполнители":
                performers.RemoveAll(p => p.Code == code);
                break;
            case "услуги":
                services.RemoveAll(s => s.Code == code);
                break;
            case "заказы":
                orders.RemoveAll(o => o.OrderCode == code);
                break;
            default:
                Console.WriteLine("Неверное название таблицы.");
                break;
        }
    }

    public void UpdateElement(string table, int code, string newValue)
    {
        switch (table.ToLower())
        {
            case "исполнители":
                var performer = performers.FirstOrDefault(p => p.Code == code);
                if (performer != null && int.TryParse(newValue, out int newAge))
                {
                    performer.Age = newAge;
                }
                break;
            case "услуги":
                var service = services.FirstOrDefault(s => s.Code == code);
                if (service != null)
                {
                    service.Name = newValue;
                }
                break;
            case "заказы":
                var order = orders.FirstOrDefault(o => o.OrderCode == code);
                if (order != null && decimal.TryParse(newValue, out decimal newCost))
                {
                    order.Cost = newCost;
                }
                break;
            default:
                Console.WriteLine("Неверное название таблицы.");
                break;
        }
    }

    public void AddElement(string table, string data)
    {
        switch (table.ToLower())
        {
            case "исполнители":
                string[] performerData = data.Split(',');
                if (int.TryParse(performerData[0], out int code) &&
                    int.TryParse(performerData[1], out int age))
                {
                    performers.Add(new Performer(code, age, performerData[2]));
                }
                break;
            case "услуги":
                string[] serviceData = data.Split(',');
                if (int.TryParse(serviceData[0], out  code))
                {
                    services.Add(new Service(code, serviceData[1]));
                }
                break;
            case "заказы":
                string[] orderData = data.Split(',');
                if (int.TryParse(orderData[0], out int orderCode) &&
                    int.TryParse(orderData[1], out int serviceCode) &&
                    int.TryParse(orderData[2], out int performerCode) &&
                    decimal.TryParse(orderData[3], out decimal cost))
                {
                    orders.Add(new Order(orderCode, serviceCode, performerCode, cost));
                }
                break;
            default:
                Console.WriteLine("Неверное название таблицы.");
                break;
        }
    }

    public void Query1()
    {
        var result = from o in orders
                     where o.Cost > 1900000
                     select o;
        foreach (var o in result)
        {
            Console.WriteLine(o);
        }
    }

    public void Query2()
    {
        var result = from o in orders
                     join s in services on o.ServiceCode equals s.Code
                     where s.Name == "Frontend-программист"
                     select o;
        foreach (var o in result)
        {
            Console.WriteLine(o);
        }
    }

    public void Query3()
    {
        var result = from o in orders
                     join s in services on o.ServiceCode equals s.Code
                     join p in performers on o.PerformerCode equals p.Code
                     where p.Citizenship == "Республика Корея"
                     select new
                     {
                         OrderCode = o.OrderCode,
                         ServiceName = s.Name,
                         PerformerCode = p.Code,
                         Cost = o.Cost
                     };
        foreach (var item in result)
        {
            Console.WriteLine($"Код заказа: {item.OrderCode}, Название услуги: {item.ServiceName}, Код исполнителя: {item.PerformerCode}, Стоимость: {item.Cost}");
        }
    }


    public void Query4()
    {
        var result = from o in orders
                     join s in services on o.ServiceCode equals s.Code
                     join p in performers on o.PerformerCode equals p.Code
                     where p.Age >= 30 && p.Age <= 35 && s.Name == "Frontend-программист"
                     select o.Cost;
        Console.WriteLine($"Общая стоимость всех заказов, выполненных исполнителями в возрасте от 30 до 35 лет, которые являются фронтенд-программистами: {result.Sum()}");
    }


    public void LogAction(string action)
    {
        using (StreamWriter writer = new StreamWriter(logFilePath, true))
        {
            writer.WriteLine($"{DateTime.Now}: {action}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        string databaseFilePath = "LR5-var6.xls"; // Убедитесь, что путь к файлу базы данных правильный
        string logFilePath = "log.txt";

        // Проверка существования файла базы данных
        if (!File.Exists(databaseFilePath))
        {
            Console.WriteLine("Файл базы данных не найден. Пожалуйста, разместите файл базы данных по указанному пути.");
            return;
        }

        // Создание лог-файла, если он не существует
        if (!File.Exists(logFilePath))
        {
            File.Create(logFilePath).Close();
        }

        FreelanceData data = new FreelanceData(logFilePath);
        data.LogAction("Начало сеанса");

        data.ReadDatabase(databaseFilePath);
        data.LogAction("База данных загружена");

        while (true)
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Просмотр базы данных");
            Console.WriteLine("2. Удаление элемента");
            Console.WriteLine("3. Корректировка элемента");
            Console.WriteLine("4. Добавление элемента");
            Console.WriteLine("5. Запрос 1");
            Console.WriteLine("6. Запрос 2");
            Console.WriteLine("7. Запрос 3");
            Console.WriteLine("8. Запрос 4");
            Console.WriteLine("9. Выход");

            string choice = Console.ReadLine();
            data.LogAction($"Выбран пункт меню: {choice}");

            switch (choice)
            {
                case "1":
                    data.ViewDatabase();
                    data.LogAction("База данных просмотрена");
                    break;
                case "2":
                    Console.WriteLine("Введите таблицу (Исполнители, Услуги, Заказы) и код:");
                    string[] deleteData = Console.ReadLine().Split(',');
                    if (deleteData.Length == 2 && int.TryParse(deleteData[1], out int code))
                    {
                        data.DeleteElement(deleteData[0], code);
                        data.LogAction($"Элемент удален из таблицы {deleteData[0]} с кодом {code}");
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат ввода.");
                    }
                    break;
                case "3":
                    Console.WriteLine("Введите таблицу (Исполнители, Услуги, Заказы), код и новое значение:");
                    string[] updateData = Console.ReadLine().Split(',');
                    if (updateData.Length == 3 && int.TryParse(updateData[1], out code))
                    {
                        data.UpdateElement(updateData[0], code, updateData[2]);
                        data.LogAction($"Элемент обновлен в таблице {updateData[0]} с кодом {code} на новое значение {updateData[2]}");
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат ввода.");
                    }
                    break;
                case "4":
                    Console.WriteLine("Введите таблицу (Исполнители, Услуги, Заказы) и данные через запятую:");
                    string[] addData = Console.ReadLine().Split(',');
                    if (addData.Length >= 2)
                    {
                        data.AddElement(addData[0], string.Join(",", addData, 1, addData.Length - 1));
                        data.LogAction($"Элемент добавлен в таблицу {addData[0]} с данными {string.Join(",", addData, 1, addData.Length - 1)}");
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат ввода.");
                    }
                    break;
                case "5":
                    data.Query1();
                    data.LogAction("Выполнен запрос 1");
                    break;
                case "6":
                    data.Query2();
                    data.LogAction("Выполнен запрос 2");
                    break;
                case "7":
                    data.Query3();
                    data.LogAction("Выполнен запрос 3");
                    break;
                case "8":
                    data.Query4();
                    data.LogAction("Выполнен запрос 4");
                    break;
                case "9":
                    data.LogAction("Конец сеанса");
                    return;
                default:
                    data.LogAction("Неверный выбор");
                    break;
            }
        }
    }
}
