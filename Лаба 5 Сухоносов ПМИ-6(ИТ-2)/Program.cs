using System;
using System.IO;

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

        FreelanceData data = new FreelanceData(logFilePath, databaseFilePath);
        data.LogAction("Начало сеанса");

        data.ReadDatabase();
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
