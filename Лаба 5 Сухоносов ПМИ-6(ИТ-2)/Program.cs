using Lab5;
using System;
using System.IO;

class Program
{
    static void Main()
    {
        string dataBase = "LR5-var6.xls";
        string log = "log.txt";

        // Проверка существования файла базы данных
        if (!File.Exists(dataBase))
        {
            Console.WriteLine("Файл базы данных не найден. Пожалуйста, разместите файл" +
                " базы данных по указанному пути.");
            return;
        }

        // Создание лог-файла, если он не существует
        if (!File.Exists(log))
        {
            File.Create(log).Close();
        }

        try
        {
            FreelanceData data = new FreelanceData(log, dataBase);
            data.LogAction("Начало сеанса");

            data.ReadDatabase();
            data.LogAction("База данных загружена");

            while (true)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1. Просмотр базы данных");
                Console.WriteLine("2. Удаление элемента");
                Console.WriteLine("3. Корректировка элемента");
                Console.WriteLine("4. Добавление элемента");
                Console.WriteLine("5. Запрос 1 (Заказы > 1,900,000)");
                Console.WriteLine("6. Запрос 2 (Frontend-программисты)");
                Console.WriteLine("7. Запрос 3 (Исполнители из Кореи)");
                Console.WriteLine("8. Запрос 4 (Сумма заказов фронтендеров 30-35 лет)");
                Console.WriteLine("9. Выход");

                string choice = Console.ReadLine();
                data.LogAction($"Выбран пункт меню: {choice}");

                try
                {
                    switch (choice)
                    {
                        case "1":
                            data.ViewDatabase();
                            data.LogAction("База данных просмотрена");
                            break;
                        case "2":
                            Console.WriteLine("Введите таблицу (Исполнители, Услуги, Заказы) " +
                                "и код через запятую:");
                            string[] deleteData = Console.ReadLine().Split(',');
                            if (deleteData.Length == 2 && int.TryParse(deleteData[1].Trim(), 
                                out int code))
                            {
                                data.DeleteElement(deleteData[0].Trim(), code);
                                data.LogAction($"Элемент удален из таблицы {deleteData[0]}" +
                                    $" с кодом {code}");
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат ввода.");
                            }
                            break;
                        case "3":
                            Console.WriteLine("Введите таблицу (Исполнители, Услуги, Заказы), " +
                                "код и новое значение через запятую:");
                            string[] updateData = Console.ReadLine().Split(',');
                            if (updateData.Length == 3 && int.TryParse(updateData[1].Trim(),
                                out code))
                            {
                                data.UpdateElement(updateData[0].Trim(), code,
                                    updateData[2].Trim());
                                data.LogAction($"Элемент обновлен в таблице {updateData[0]}" +
                                    $" с кодом {code} на новое значение {updateData[2]}");
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат ввода.");
                            }
                            break;
                        case "4":
                            Console.WriteLine("Введите таблицу (Исполнители, Услуги, Заказы)" +
                                " и данные через запятую:");
                            Console.WriteLine("Формат для исполнителей: Код,Возраст,Гражданство");
                            Console.WriteLine("Формат для услуг: Код,Название");
                            Console.WriteLine("Формат для заказов: КодЗаказа,КодУслуги," +
                                "КодИсполнителя,Стоимость");
                            string[] addData = Console.ReadLine().Split(',');
                            if (addData.Length >= 2)
                            {
                                data.AddElement(addData[0].Trim(), string.Join(",", addData, 1,
                                    addData.Length - 1));
                                data.LogAction($"Элемент добавлен в таблицу {addData[0]} с " +
                                    $"данными {string.Join(",", addData, 1, addData.Length - 1)}");
                            }
                            else
                            {
                                Console.WriteLine("Неверный формат ввода.");
                            }
                            break;
                        case "5":
                            Console.WriteLine("Заказы стоимостью более 1,900,000:");
                            foreach (var order in data.Query1())
                            {
                                Console.WriteLine(order);
                            }
                            data.LogAction("Выполнен запрос 1");
                            break;
                        case "6":
                            Console.WriteLine("Заказы для услуги 'Frontend-программист':");
                            foreach (var order in data.Query2())
                            {
                                Console.WriteLine(order);
                            }
                            data.LogAction("Выполнен запрос 2");
                            break;
                        case "7":
                            Console.WriteLine("Заказы исполнителей из Республики Корея:");
                            foreach (var item in data.Query3())
                            {
                                Console.WriteLine($"Код заказа: {item.OrderCode}," +
                                    $" Название услуги: {item.ServiceName}, " +
                                    $"Код исполнителя: {item.PerformerCode}," +
                                    $" Стоимость: {item.Cost}");
                            }
                            data.LogAction("Выполнен запрос 3");
                            break;
                        case "8":
                            Console.WriteLine($"Общая стоимость заказов: {data.Query4()}");
                            data.LogAction("Выполнен запрос 4");
                            break;
                        case "9":
                            data.LogAction("Конец сеанса");
                            return;
                        default:
                            Console.WriteLine("Неверный выбор. Попробуйте снова.");
                            data.LogAction("Неверный выбор");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    data.LogAction($"Ошибка при выполнении операции: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критическая ошибка: {ex.Message}");
            File.AppendAllText(log, $"{DateTime.Now}: Критическая ошибка: {ex.Message}\n");
        }
    }
}