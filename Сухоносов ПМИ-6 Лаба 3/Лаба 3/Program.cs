using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;

public class ArrayClass
{
    private int[,] array;
    private static Random random = new Random();


    public ArrayClass(int n, int m)
    {
        array = new int[n, m];
    }

    public int GetElement(int row, int col)
    {
        return array[row, col];
    }

    public void SetElement(int row, int col, int value)
    {
        array[row, col] = value;
    }

    public int GetLength(int dimension)
    {
        return array.GetLength(dimension);
    }

    // Конструктор для заполнения массива данными, вводимыми с клавиатуры
    public ArrayClass(int n, int m,double x)
    {
        if (x == 0)
        {
            array = new int[n, m];
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Введите элемент [{i}][{j}]: ");
                    array[i, j] = int.Parse(Console.ReadLine());
                }
            }
        }
        else
        {
            this.array = new int[n, m];
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                   this.array[i, j] = random.Next(10);
                }
            }
        }
    }

    // Конструктор для заполнения массива четырехзначными случайными числами, составленными из четных цифр
    public ArrayClass(int n,double x)
    {
        array = new int[n, n];
        if (x == 0)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    array[i, j] = GenerateRandom();
                }
            }
        }
        else
        {
            for (int i=0; i< n; i += 2) {
                for (int j = 0; j+i < n; j++)
                {
                    array[j, i + j] = j + 1;
                }
            }
            for (int i = 1; i < n; i += 2)
            {
                for (int j = 0; j + i < n; j++)
                {
                    array[j, i + j] = n-i-j;
                }
            }

        }
    }


    // Метод для генерации четырехзначного случайного числа, составленного из четных цифр
    private int GenerateRandom()
    {
        int number = random.Next(1, 5) * 2; // Генерирует случайное четное число от 2 до 8;

        for (int i = 0; i < 3; i++)
        {
            int digit = random.Next(0, 5) * 2; // Генерирует случайное четное число от 0 до 8
            number = number * 10 + digit;
        }
        return number;
    }

    //2 задания 
    // метод для нахождения пути
    public int ShortestPath()
    {
        int min = int.MaxValue, max = int.MinValue;
        int minRow = 0, minCol = 0, maxRow = 0, maxCol = 0;

        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] < min)
                {
                    min = array[i, j];
                    minRow = i;
                    minCol = j;
                }
                if (array[i, j] > max)
                {
                    max = array[i, j];
                    maxRow = i;
                    maxCol = j;
                }
            }
        }

        return Math.Max(Math.Abs(maxRow - minRow), Math.Abs(maxCol - minCol));
    }


    //3 Задание 
    // метод для транспонирования матрицы
    private void TransposeMatrix()
    {
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        int[,] transposedMatrix = new int[cols, rows];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                transposedMatrix[j, i] = array[i, j];
            }
        }

        array=transposedMatrix;
    }

    // метод для подсчёта 
    private void Calculate(ArrayClass A, ArrayClass B, ArrayClass C)
    {
        int n = array.GetLength(0);
        int[,] result = new int[n, n];
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                result[i,j]= B.GetElement(i,j) + C.GetElement(i, j);
                result[i, j] = A.GetElement(i, j) - result[i, j];
            }
        }
        array = result;
    }

    // Метод для вывода массива на экран
    public void PrintArray()
    {
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                Console.Write("{0,6}",array[i, j]);
            }
            Console.WriteLine();
        }
    }


    public static void Main(string[] args)
    {
        // 1 задание 
        // Пример использования первого конструктора
        int n, m;
        Console.WriteLine("Введите n(>0):");
        if (!int.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (n > 0)
        {
            Console.WriteLine("Введите m(>0):");
            if (!int.TryParse(Console.ReadLine(), out m))
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
            }
            if (m > 0)
            {
                ArrayClass array1 = new ArrayClass(n, m,0.0);
                Console.WriteLine("1-ый Массив, заполненный с клавиатуры:");
                array1.PrintArray();
            }
            else
            {
                Console.WriteLine("m < 0");
            }
        }
        else
        {
            Console.WriteLine("n < 0");
        }

        // Пример использования второго конструктора
        Console.WriteLine("Введите n(>0):");
        if (!int.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (n > 0)
        {
            ArrayClass array2 = new ArrayClass(n, 0.0);
            Console.WriteLine("2-ой Массив, заполненный случайными четырехзначными числами из четных цифр:");
            array2.PrintArray();
        }
        else
        {
            Console.WriteLine("размер массива < 0");
        }
        

        // Пример использования третьего конструктора
        Console.WriteLine("Введите n(>0):");
        if (!int.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (n > 0)
        {
            ArrayClass array3 = new ArrayClass(n, 0.1);
            Console.WriteLine("3-ий Массив, заполненный для произвольного n по примеру");
            array3.PrintArray();
        }
        else
        {
            Console.WriteLine("размер массива < 0");
        }


        // 2 задание 
        Console.WriteLine("Элементами массива:");
        ArrayClass array4 = new ArrayClass(n, 0.0);
        array4 .PrintArray();
        Console.WriteLine("Самый короткий путь между максимальным и минимальным элементами массива:");
        Console.WriteLine(array4.ShortestPath());
        Console.WriteLine();

        // 3 задание 

        Console.WriteLine("Посчитаем Ат-(В+Ст)");
        Console.WriteLine("Введите размерность n x n");
        Console.WriteLine("Введите n(>0):");
        if (!int.TryParse(Console.ReadLine(), out n))
        {
            Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
        }
        if (n > 0)
        {
            ArrayClass A = new ArrayClass(n, n,0.1);
            ArrayClass B = new ArrayClass(n, n,0.1);
            ArrayClass C = new ArrayClass(n, n,0.1);
            ArrayClass result = new ArrayClass(n, n);
            Console.WriteLine("Матрица A");
            A.PrintArray();
            Console.WriteLine("Матрица B");
            B.PrintArray();
            Console.WriteLine("Матрица С");
            C.PrintArray();
            //Вызовы метадов транспонирования
            A.TransposeMatrix();
            Console.WriteLine("Матрица Aт");
            A.PrintArray();
            C.TransposeMatrix();
            Console.WriteLine("Матрица Ст");
            C.PrintArray();
            //Вызов метода подсчёта матриц
            Console.WriteLine(" Ат - (В + Ст) = ");
            result.Calculate(A, B, C);
            result.PrintArray();
        }
        else
        {
            Console.WriteLine("n < 0");
        }
        ToyProgram.ToyMain();
    }
}


[Serializable]
public class Toy
{
    public string Name { get; set; }
    public decimal Cost { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
}

public class ToyProgram
{
    private const string FileName = "toys.dat";
    private const string TextFileName = "numbers.txt";
    private static Random random = new Random();

    public static void ToyMain()
    {
        //4 задание 
        Console.WriteLine("Введите значение k:");
        if (int.TryParse(Console.ReadLine(), out int k))
        {
            Console.WriteLine("Введите количество случайных чисел для заполнения исходного файла:");
            if (int.TryParse(Console.ReadLine(), out int count))
            {
                CreateSourceFile("1.txt", count);
                FilterAndWriteToTargetFile("1.txt", "2.txt", k);
                Console.WriteLine("Операция завершена успешно.");
            }
            else
            {
                Console.WriteLine("Некорректное значение количества чисел. Пожалуйста, введите целое число.");
            }
        }
        else
        {
            Console.WriteLine("Некорректное значение k. Пожалуйста, введите целое число.");
        }


        //5 задание 
        if (!File.Exists(FileName))
        {
            CreateFile();
        }
        Console.WriteLine("Список игрушек:");
        PrintToys();
        Console.WriteLine("Максимальная цена на игрушку: " + GetMostCost());

        //6 задание 
        // Заполняем файл случайными числами
        FileRandom("numbers(6).txt", 10);

        // Читаем содержимое файла и сохраняем его в список
        List<int> numbers = new List<int>();
        using (StreamReader reader = new StreamReader("numbers(6).txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line.Trim(), out int number))
                {
                    numbers.Add(number);
                }
            }
        }

        // Находим сумму элементов, которые равны своему индексу
        int sumOfElements = 0;
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] == i)
            {
                sumOfElements += numbers[i];
            }
        }

        // Выводим полученную сумму
        Console.WriteLine("Сумма элементов, равных своему индексу: " + sumOfElements);

        //7 задание 
        // Заполнение текстового файла случайными данными
        FillTextRandomData();

        // Вычисление произведения элементов, которые кратны заданному числу k
        Console.Write("Введите число k: ");
        k = int.Parse(Console.ReadLine());
        Console.WriteLine("Произведение элементов, кратных " + k + ": " + CalculateMultiples(k));

        //8 задание 
        // Заполнение текстового файла случайными данными
        TextRandomData();
        Console.WriteLine("Создание рандомной строки в файле");
        // Переписывание строк без русских букв в другой файл
        FilterAndWriteToFile();
        Console.WriteLine("Переписывание его без русских букв.");
        Console.WriteLine("Успешно!");

        Console.ReadKey();
    }


    //4 задание 

    public static void CreateSourceFile(string fileName, int count)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Create)))
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int number = random.Next(1, 100); // Генерация случайных чисел от 1 до 99
                writer.Write(number);
            }
        }
    }

    public static void FilterAndWriteToTargetFile(string sourceFileName, string targetFileName, int k)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(sourceFileName, FileMode.Open)))
        using (BinaryWriter writer = new BinaryWriter(File.Open(targetFileName, FileMode.Create)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                if (number % k == 0)
                {
                    writer.Write(number);
                }
            }
        }
    }

    private static void CreateFile()
    {
        int numberOfToys = random.Next(5, 15); // Генерация случайного количества игрушек от 5 до 14
        Toy[] toys = new Toy[numberOfToys];

        for (int i = 0; i < numberOfToys; i++)
        {
            toys[i] = new Toy
            {
                Name = $"Constructor{i + 1}",
                Cost = random.Next(50, 500), // Генерация случайной стоимости от 50 до 499
                MinAge = random.Next(1, 5), // Генерация случайного минимального возраста от 1 до 4
                MaxAge = random.Next(6, 10) // Генерация случайного максимального возраста от 6 до 9
            };
        }

        using (FileStream stream = new FileStream(FileName, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, toys); // Сериализация массива объектов Toy в файл
        }
    }
    
    //5 задание 
    private static decimal GetMostCost()
    {
        Toy[] toys;
        using (FileStream stream = new FileStream(FileName, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            toys = (Toy[])formatter.Deserialize(stream); // Десериализация массива объектов Toy из файла
        }

        decimal maxCost = 0;
        foreach (Toy toy in toys)
        {
            if (toy.Name.Contains("Constructor") && toy.Cost > maxCost)
            {
                maxCost = toy.Cost;
            }
        }

        return maxCost;
    }

    private static void PrintToys()
    {
        Toy[] toys;
        using (FileStream stream = new FileStream(FileName, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            toys = (Toy[])formatter.Deserialize(stream); // Десериализация массива объектов Toy из файла
        }

        foreach (Toy toy in toys)
        {
            Console.WriteLine($"Name: {toy.Name}, Cost: {toy.Cost}, MinAge: {toy.MinAge}, MaxAge: {toy.MaxAge}");
        }
    }


    //6 задание 

    // Метод для заполнения файла случайными числами
    public static void FileRandom(string filename, int count)
    {
        Random random = new Random();
        using (StreamWriter writer = new StreamWriter(filename))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(random.Next(0, 11)+" ");
            }
        }
    }

    //7 задание 
    private static void FillTextRandomData()
    {
        int numberOfLines = random.Next(5, 15); // Генерация случайного количества строк от 5 до 14
        using (StreamWriter writer = new StreamWriter(TextFileName))
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                int numberOfNumbers = random.Next(3, 10); // Генерация случайного количества чисел в строке от 3 до 9
                int[] numbers = new int[numberOfNumbers];
                for (int j = 0; j < numberOfNumbers; j++)
                {
                    numbers[j] = random.Next(1, 100); // Генерация случайного числа от 1 до 99
                }
                writer.WriteLine(string.Join(" ", numbers));
            }
        }
    }

    private static long CalculateMultiples(int k)
    {
        long product = 1;
        bool found = false;

        using (StreamReader reader = new StreamReader(TextFileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                int[] numbers = line.Split(' ').Select(int.Parse).ToArray();
                foreach (int number in numbers)
                {
                    if (number % k == 0)
                    {
                        product *= number;
                        found = true;
                    }
                }
            }
        }

        return found ? product : 0;
    }

    //8 задание 
    private static void TextRandomData()
    {
        int numberOfLines = random.Next(5, 15); // Генерация случайного количества строк от 5 до 14
        using (StreamWriter writer = new StreamWriter("input.txt"))
        {
            for (int i = 0; i < numberOfLines; i++)
            {
                int length = random.Next(5, 20); // Генерация случайной длины строки от 5 до 19
                string line = GenerateRandomString(length);
                writer.WriteLine(line);
            }
        }
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФЧЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюяABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        char[] stringChars = new char[length];
        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }
        return new string(stringChars);
    }

    private static void FilterAndWriteToFile()
    {
        using (StreamReader reader = new StreamReader("input.txt"))
        using (StreamWriter writer = new StreamWriter("output.txt"))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (!line.Any(c => c >= 'А' && c <= 'я'))
                {
                    writer.WriteLine(line);
                }
            }
        }
    }

}

