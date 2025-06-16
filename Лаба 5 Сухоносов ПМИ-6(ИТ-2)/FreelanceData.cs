using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aspose.Cells;

namespace Lab5
{
    /// <summary>
    /// Управление данными фриланс-сервиса: исполнителями, услугами и заказами
    /// </summary>
    public class FreelanceData
    {
        private const int PerformerCodeColumn = 0;
        private const int PerformerAgeColumn = 1;
        private const int PerformerCitizenshipColumn = 2;

        private const int ServiceCodeColumn = 0;
        private const int ServiceNameColumn = 1;

        private const int OrderCodeColumn = 0;
        private const int OrderServiceCodeColumn = 1;
        private const int OrderPerformerCodeColumn = 2;
        private const int OrderCostColumn = 3;

        private readonly List<Performer> _performers;
        private readonly List<Service> _services;
        private readonly List<Order> _orders;
        private readonly string _logPath;
        private readonly string _dataBasePath;

        /// <summary>
        /// Инициализирует новый экземпляр класса FreelanceData
        /// </summary>
        /// <param name="logPath">Путь к файлу лога</param>
        /// <param name="dataBasePath">Путь к файлу базы данных</param>
        /// <exception cref="ArgumentNullException">Если пути не указаны</exception>
        public FreelanceData(string logPath, string dataBasePath)
        {
            if (string.IsNullOrEmpty(logPath))
                throw new ArgumentNullException(nameof(logPath));
            if (string.IsNullOrEmpty(dataBasePath))
                throw new ArgumentNullException(nameof(dataBasePath));

            _performers = new List<Performer>();
            _services = new List<Service>();
            _orders = new List<Order>();
            _logPath = logPath;
            _dataBasePath = dataBasePath;
        }

        /// <summary>
        /// Чтение данных из базы данных Excel
        /// </summary>
        /// <exception cref="FileNotFoundException">Если файл базы данных не найден</exception>
        /// <exception cref="InvalidOperationException">При ошибках чтения данных</exception>
        public void ReadDatabase()
        {
            if (!File.Exists(_dataBasePath))
            {
                LogAction($"Файл базы данных не найден: {_dataBasePath}");
                throw new FileNotFoundException(_dataBasePath);
            }

            try
            {
                using (var workbook = new Workbook(_dataBasePath))
                {
                    ReadPerformers(workbook);
                    ReadServices(workbook);
                    ReadOrders(workbook);
                }
            }
            catch (Exception ex)
            {
                LogAction($"Ошибка при чтении файла: {ex.Message}");
                throw new InvalidOperationException("Ошибка чтения базы данных", ex);
            }
        }

        private void ReadPerformers(Workbook workbook)
        {
            var performersSheet = GetWorksheetOrThrow(workbook, "Исполнители");

            for (int i = 1; i <= performersSheet.Cells.MaxDataRow + 1; i++)
            {
                try
                {
                    if (performersSheet.Cells[i, PerformerCodeColumn].Value != null &&
                        int.TryParse(performersSheet.Cells[i, PerformerCodeColumn].Value.ToString(),
                        out int code) &&
                        performersSheet.Cells[i, PerformerAgeColumn].Value != null &&
                        int.TryParse(performersSheet.Cells[i, PerformerAgeColumn].Value.ToString(),
                        out int age))
                    {
                        string citizenship = performersSheet.Cells[i, PerformerCitizenshipColumn]
                            .Value?.ToString() ?? string.Empty;
                        _performers.Add(new Performer(code, age, citizenship));
                    }
                    else
                    {
                        LogAction($"Ошибка при разборе данных исполнителя в строке {i}");
                    }
                }
                catch (Exception ex)
                {
                    LogAction($"Ошибка при обработке строки {i} исполнителей: {ex.Message}");
                }
            }
        }

        private void ReadServices(Workbook workbook)
        {
            var servicesSheet = GetWorksheetOrThrow(workbook, "Услуги");

            for (int i = 1; i <= servicesSheet.Cells.MaxDataRow + 1; i++)
            {
                try
                {
                    if (servicesSheet.Cells[i, ServiceCodeColumn].Value != null &&
                        int.TryParse(servicesSheet.Cells[i, ServiceCodeColumn].Value.ToString(),
                        out int code))
                    {
                        string name = servicesSheet.Cells[i, ServiceNameColumn].Value?.ToString() 
                            ?? string.Empty;
                        _services.Add(new Service(code, name));
                    }
                    else
                    {
                        LogAction($"Ошибка при разборе данных услуги в строке {i}");
                    }
                }
                catch (Exception ex)
                {
                    LogAction($"Ошибка при обработке строки {i} услуг: {ex.Message}");
                }
            }
        }

        private void ReadOrders(Workbook workbook)
        {
            var ordersSheet = GetWorksheetOrThrow(workbook, "Заказы");

            for (int i = 1; i <= ordersSheet.Cells.MaxDataRow + 1; i++)
            {
                try
                {
                    if (ordersSheet.Cells[i, OrderCodeColumn].Value != null &&
                        int.TryParse(ordersSheet.Cells[i, OrderCodeColumn].Value.ToString(),
                        out int orderCode) &&
                        ordersSheet.Cells[i, OrderServiceCodeColumn].Value != null &&
                        int.TryParse(ordersSheet.Cells[i, OrderServiceCodeColumn].Value.ToString(),
                        out int serviceCode) &&
                        ordersSheet.Cells[i, OrderPerformerCodeColumn].Value != null &&
                        int.TryParse(ordersSheet.Cells[i, OrderPerformerCodeColumn].
                        Value.ToString(),
                        out int performerCode) &&
                        ordersSheet.Cells[i, OrderCostColumn].Value != null &&
                        decimal.TryParse(ordersSheet.Cells[i, OrderCostColumn].Value.ToString()
                            .Replace("р.", "").Replace(" ", ""), out decimal cost))
                    {
                        _orders.Add(new Order(orderCode, serviceCode, performerCode, cost));
                    }
                    else
                    {
                        LogAction($"Ошибка при разборе данных заказа в строке {i}");
                    }
                }
                catch (Exception ex)
                {
                    LogAction($"Ошибка при обработке строки {i} заказов: {ex.Message}");
                }
            }
        }

        private Worksheet GetWorksheetOrThrow(Workbook workbook, string sheetName)
        {
            var sheet = workbook.Worksheets[sheetName];
            if (sheet == null)
            {
                var message = $"Лист '{sheetName}' не найден";
                LogAction(message);
                throw new InvalidOperationException(message);
            }
            return sheet;
        }

        /// <summary>
        /// Вывод всех данных в консоль
        /// </summary>
        public void ViewDatabase()
        {
            Console.WriteLine("Исполнители:");
            _performers.ForEach(p => Console.WriteLine(p));

            Console.WriteLine("\nУслуги:");
            _services.ForEach(s => Console.WriteLine(s));

            Console.WriteLine("\nЗаказы:");
            _orders.ForEach(o => Console.WriteLine(o));
        }

        /// <summary>
        /// Удаление элемента из указанной таблицы
        /// </summary>
        /// <param name="table">Название таблицы (исполнители, услуги, заказы)</param>
        /// <param name="code">Код элемента для удаления</param>
        /// <exception cref="ArgumentException">При неверном названии таблицы</exception>
        public void DeleteElement(string table, int code)
        {
            if (string.IsNullOrEmpty(table))
                throw new ArgumentNullException(nameof(table));

            switch (table.ToLower())
            {
                case "исполнители":
                    _performers.RemoveAll(p => p.Code == code);
                    break;
                case "услуги":
                    _services.RemoveAll(s => s.Code == code);
                    break;
                case "заказы":
                    _orders.RemoveAll(o => o.OrderCode == code);
                    break;
                default:
                    var message = $"Неверное название таблицы: {table}";
                    LogAction(message);
                    throw new ArgumentException(message, nameof(table));
            }

            SaveDatabase();
        }

        /// <summary>
        /// Обновление элемента в указанной таблице
        /// </summary>
        /// <param name="table">Название таблицы</param>
        /// <param name="code">Код элемента</param>
        /// <param name="newValue">Новое значение</param>
        /// <exception cref="ArgumentException">При неверных аргументах</exception>
        public void UpdateElement(string table, int code, string newValue)
        {
            if (string.IsNullOrEmpty(table))
                throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrEmpty(newValue))
                throw new ArgumentNullException(nameof(newValue));

            switch (table.ToLower())
            {
                case "исполнители":
                    var performer = _performers.FirstOrDefault(p => p.Code == code);
                    if (performer != null)
                    {
                        if (int.TryParse(newValue, out int newAge))
                        {
                            performer.Age = newAge;
                        }
                        else
                        {
                            throw new ArgumentException("Новое значение возраста должно быть " +
                                "числом", nameof(newValue));
                        }
                    }
                    break;
                case "услуги":
                    var service = _services.FirstOrDefault(s => s.Code == code);
                    if (service != null)
                    {
                        service.Name = newValue;
                    }
                    break;
                case "заказы":
                    var order = _orders.FirstOrDefault(o => o.OrderCode == code);
                    if (order != null)
                    {
                        if (decimal.TryParse(newValue, out decimal newCost))
                        {
                            order.Cost = newCost;
                        }
                        else
                        {
                            throw new ArgumentException("Новое значение стоимости должно быть" +
                                " числом", nameof(newValue));
                        }
                    }
                    break;
                default:
                    var message = $"Неверное название таблицы: {table}";
                    LogAction(message);
                    throw new ArgumentException(message, nameof(table));
            }

            SaveDatabase();
        }

        /// <summary>
        /// Добавление нового элемента в указанную таблицу
        /// </summary>
        /// <param name="table">Название таблицы</param>
        /// <param name="data">Данные в формате CSV</param>
        /// <exception cref="ArgumentException">При неверных аргументах</exception>
        public void AddElement(string table, string data)
        {
            if (string.IsNullOrEmpty(table))
                throw new ArgumentNullException(nameof(table));
            if (string.IsNullOrEmpty(data))
                throw new ArgumentNullException(nameof(data));

            try
            {
                switch (table.ToLower())
                {
                    case "исполнители":
                        var performerData = data.Split(',');
                        if (performerData.Length != 3)
                            throw new ArgumentException("Неверный формат данных для исполнителя",
                                nameof(data));

                        if (!int.TryParse(performerData[0], out int code) || 
                            !int.TryParse(performerData[1], out int age))
                            throw new ArgumentException("Неверный формат числовых данных " +
                                "для исполнителя", nameof(data));

                        _performers.Add(new Performer(code, age, performerData[2]));
                        break;
                    case "услуги":
                        var serviceData = data.Split(',');
                        if (serviceData.Length != 2)
                            throw new ArgumentException("Неверный формат данных для услуги",
                                nameof(data));

                        if (!int.TryParse(serviceData[0], out code))
                            throw new ArgumentException("Неверный формат кода услуги",
                                nameof(data));

                        _services.Add(new Service(code, serviceData[1]));
                        break;
                    case "заказы":
                        var orderData = data.Split(',');
                        if (orderData.Length != 4)
                            throw new ArgumentException("Неверный формат данных для заказа",
                                nameof(data));

                        if (!int.TryParse(orderData[0], out int orderCode) ||
                            !int.TryParse(orderData[1], out int serviceCode) ||
                            !int.TryParse(orderData[2], out int performerCode) ||
                            !decimal.TryParse(orderData[3], out decimal cost))
                            throw new ArgumentException("Неверный формат числовых " +
                                "данных для заказа", nameof(data));

                        _orders.Add(new Order(orderCode, serviceCode, performerCode, cost));
                        break;
                    default:
                        var message = $"Неверное название таблицы: {table}";
                        LogAction(message);
                        throw new ArgumentException(message, nameof(table));
                }

                SaveDatabase();
            }
            catch (Exception ex)
            {
                LogAction($"Ошибка при добавлении элемента: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Запрос 1: Заказы стоимостью более 1,900,000
        /// </summary>
        /// <returns>Перечисление заказов</returns>
        public IEnumerable<Order> Query1()
        {
            return _orders.Where(o => o.Cost > 1900000);
        }

        /// <summary>
        /// Запрос 2: Заказы для услуги "Frontend-программист"
        /// </summary>
        /// <returns>Перечисление заказов</returns>
        public IEnumerable<Order> Query2()
        {
            return from o in _orders
                   join s in _services on o.ServiceCode equals s.Code
                   where s.Name == "Frontend-программист"
                   select o;
        }

        /// <summary>
        /// Запрос 3: Заказы исполнителей из Республики Корея
        /// </summary>
        /// <returns>Анонимные объекты с информацией о заказах</returns>
        public IEnumerable<dynamic> Query3()
        {
            return from o in _orders
                   join s in _services on o.ServiceCode equals s.Code
                   join p in _performers on o.PerformerCode equals p.Code
                   where p.Citizenship == "Республика Корея"
                   select new
                   {
                       o.OrderCode,
                       ServiceName = s.Name,
                       PerformerCode = p.Code,
                       o.Cost
                   };
        }

        /// <summary>
        /// Запрос 4: Общая стоимость заказов фронтенд-программистов 30-35 лет
        /// </summary>
        /// <returns>Сумма стоимости</returns>
        public decimal Query4()
        {
            return (from o in _orders
                    join s in _services on o.ServiceCode equals s.Code
                    join p in _performers on o.PerformerCode equals p.Code
                    where p.Age >= 30 && p.Age <= 35 && s.Name == "Frontend-программист"
                    select o.Cost).Sum();
        }

        /// <summary>
        /// Логирование действия в файл
        /// </summary>
        /// <param name="action">Действие для логирования</param>
        public void LogAction(string action)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(_logPath) ?? string.Empty);
                File.AppendAllText(_logPath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: " +
                    $"{action}{Environment.NewLine}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при записи в лог: {ex.Message}");
            }
        }

        /// <summary>
        /// Сохранение данных в базу данных Excel
        /// </summary>
        private void SaveDatabase()
        {
            try
            {
                using (var workbook = new Workbook())
                {
                    var performersSheet = workbook.Worksheets.Add("Исполнители");
                    var servicesSheet = workbook.Worksheets.Add("Услуги");
                    var ordersSheet = workbook.Worksheets.Add("Заказы");

                    performersSheet.Cells[0, PerformerCodeColumn].PutValue("Код");
                    performersSheet.Cells[0, PerformerAgeColumn].PutValue("Возраст");
                    performersSheet.Cells[0, PerformerCitizenshipColumn].PutValue("Гражданство");

                    for (int i = 0; i < _performers.Count; i++)
                    {
                        performersSheet.Cells[i + 1, PerformerCodeColumn]
                            .PutValue(_performers[i].Code);
                        performersSheet.Cells[i + 1, PerformerAgeColumn]
                            .PutValue(_performers[i].Age);
                        performersSheet.Cells[i + 1, PerformerCitizenshipColumn]
                            .PutValue(_performers[i].Citizenship);
                    }

                    servicesSheet.Cells[0, ServiceCodeColumn].PutValue("Код");
                    servicesSheet.Cells[0, ServiceNameColumn].PutValue("Название");

                    for (int i = 0; i < _services.Count; i++)
                    {
                        servicesSheet.Cells[i + 1, ServiceCodeColumn].PutValue(_services[i].Code);
                        servicesSheet.Cells[i + 1, ServiceNameColumn].PutValue(_services[i].Name);
                    }

                    ordersSheet.Cells[0, OrderCodeColumn].PutValue("Код заказа");
                    ordersSheet.Cells[0, OrderServiceCodeColumn].PutValue("Код услуги");
                    ordersSheet.Cells[0, OrderPerformerCodeColumn].PutValue("Код исполнителя");
                    ordersSheet.Cells[0, OrderCostColumn].PutValue("Стоимость");

                    for (int i = 0; i < _orders.Count; i++)
                    {
                        ordersSheet.Cells[i + 1, OrderCodeColumn]
                            .PutValue(_orders[i].OrderCode);
                        ordersSheet.Cells[i + 1, OrderServiceCodeColumn]
                            .PutValue(_orders[i].ServiceCode);
                        ordersSheet.Cells[i + 1, OrderPerformerCodeColumn]
                            .PutValue(_orders[i].PerformerCode);
                        ordersSheet.Cells[i + 1, OrderCostColumn]
                            .PutValue(_orders[i].Cost);
                    }

                    workbook.Save(_dataBasePath);
                }
            }
            catch (Exception ex)
            {
                LogAction($"Ошибка при сохранении файла: {ex.Message}");
                throw new InvalidOperationException("Ошибка сохранения базы данных", ex);
            }
        }
    }
}