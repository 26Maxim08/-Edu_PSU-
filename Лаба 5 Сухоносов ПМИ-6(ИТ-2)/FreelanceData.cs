using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;
using Aspose.Cells.Drawing;

public class FreelanceData
{
    private List<Performer> performers;
    private List<Service> services;
    private List<Order> orders;
    private string logFilePath;
    private string databaseFilePath;

    public FreelanceData(string logFilePath, string databaseFilePath)
    {
        performers = new List<Performer>();
        services = new List<Service>();
        orders = new List<Order>();
        this.logFilePath = logFilePath;
        this.databaseFilePath = databaseFilePath;
    }

    public void ReadDatabase()
    {
        try
        {
            Workbook workbook = new Workbook(databaseFilePath);

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
            for (int i = 1; i <= performersSheet.Cells.MaxDataRow + 1; i++)
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
            for (int i = 1; i <= servicesSheet.Cells.MaxDataRow + 1; i++)
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
            for (int i = 1; i <= ordersSheet.Cells.MaxDataRow + 1; i++)
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
        SaveDatabase();
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
        SaveDatabase();
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
                if (int.TryParse(serviceData[0], out code))
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
        SaveDatabase();
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

    private void SaveDatabase()
    {
        try
        {
            Workbook workbook = new Workbook(databaseFilePath);

            // Очистка листов
            Worksheet performersSheet = workbook.Worksheets["Исполнители"];
            performersSheet.Cells.Clear();

            Worksheet servicesSheet = workbook.Worksheets["Услуги"];
            servicesSheet.Cells.Clear();

            Worksheet ordersSheet = workbook.Worksheets["Заказы"];
            ordersSheet.Cells.Clear();

            // Запись исполнителей
            for (int i = 0; i < performers.Count; i++)
            {
                performersSheet.Cells[i, 0].PutValue(performers[i].Code);
                performersSheet.Cells[i, 1].PutValue(performers[i].Age);
                performersSheet.Cells[i, 2].PutValue(performers[i].Citizenship);
            }

            // Запись услуг
            for (int i = 0; i < services.Count; i++)
            {
                servicesSheet.Cells[i, 0].PutValue(services[i].Code);
                servicesSheet.Cells[i, 1].PutValue(services[i].Name);
            }

            // Запись заказов
            for (int i = 0; i < orders.Count; i++)
            {
                ordersSheet.Cells[i, 0].PutValue(orders[i].OrderCode);
                ordersSheet.Cells[i, 1].PutValue(orders[i].ServiceCode);
                ordersSheet.Cells[i, 2].PutValue(orders[i].PerformerCode);
                ordersSheet.Cells[i, 3].PutValue(orders[i].Cost);
            }

            workbook.Save(databaseFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при сохранении файла: {ex.Message}");
            LogAction($"Ошибка при сохранении файла: {ex.Message}");
        }
    }
}
