using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Лаба_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();

            char Begin;
            byte Ex = 0;
            int x, y, z; // Инициализация переменных
            string s;
            int[] arr,arr2;
            Console.Write("Введите Y/N (N - закончить выполнение программы, Y - продолжить): ");
            Begin = Console.ReadKey().KeyChar;
            Console.WriteLine();

            do
            {
                Console.Write("Выберите номер задачи (1-20): ");
                if (!byte.TryParse(Console.ReadLine(), out Ex))
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите число от 1 до 20.");
                    continue;
                }

                switch (Ex)
                {
                    case 1:
                        {
                            // Задание 1 (1.2)
                            Console.Write("Введите число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (x > 9 || x < -9)
                            {
                                Console.WriteLine("Сумма последних двух символов = " + pr.sumLastNums(x));
                            }
                            else
                            {
                                Console.WriteLine("В числе меньше 2 символов!!");
                            }
                        }
                        break;
                    case 2:
                        {
                            // Задание 2 (1.4)
                            Console.Write("Введите число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (pr.isPositive(x))
                            {
                                Console.WriteLine("Число положительное");
                            }
                            else
                            {
                                Console.WriteLine("Число отрицательное");
                            }
                        }
                        break;
                    case 3:
                        {
                            // Задание 3 (1.6)
                            Console.Write("Введите символ: ");
                            char S = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                            Console.WriteLine("Результат: " + pr.isUpperCase(S));
                        }
                        break;
                    case 4:
                        {
                            // Задание 4 (1.8)
                            Console.Write("Введите x: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.Write("Введите y: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.WriteLine("Результат: " + pr.isDivisor(x, y));
                        }
                        break;
                    case 5:
                        {
                            // Задача 5 (1.10)
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            for (int i = 0; i < 5; i++)
                            {
                                Console.Write("Введите второе число: ");
                                if (!int.TryParse(Console.ReadLine(), out y))
                                {
                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                    break;
                                }
                                Console.Write(x + " + " + y);
                                x = pr.lastNumSum(x, y);
                                Console.WriteLine(" = " + x);
                            }
                            Console.WriteLine("Итог: " + x);
                        }
                        break;
                    case 6:
                        {
                            // Задание 6 (2.2)
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.WriteLine("x / y = " + pr.safeDiv(x, y));
                        }
                        break;
                    case 7:
                        {
                            // Задание 7 (2.4)
                            Console.Write("Введите первое число: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.Write("Введите второе число: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.WriteLine("x = " + x + "  y = " + y);
                            Console.WriteLine("Результат: " + pr.makeDecision(x, y));
                        }
                        break;
                    case 8:
                        {
                            // Задание 8 (2.6)
                            Console.Write("Введите x: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.Write("Введите y: ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.Write("Введите z: ");
                            if (!int.TryParse(Console.ReadLine(), out z))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.WriteLine("Результат: " + pr.sum3(x, y, z));
                        }
                        break;
                    case 9:
                        {
                            // Задание 9 (2.8)
                            Console.Write("Введите x: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.WriteLine("Результат: " + pr.age(x));
                        }
                        break;
                    case 10:
                        {
                            // Задание 10 (2.10)
                            Console.Write("Введите x: ");
                            s = Console.ReadLine();
                            Console.WriteLine("x = " + s);
                            pr.printDays(s);
                        }
                        break;
                    case 11:
                        {
                            // Задание 11 (3.2)
                            Console.Write("Введите x (x < 0): ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (x >= 0)
                            {
                                Console.WriteLine("x < 0");
                            }
                            else
                            {
                                Console.WriteLine("x = " + x);
                                Console.WriteLine("Результат: " + pr.reverseListNums(x));
                            }
                        }
                        break;
                    case 12:
                        {
                            // Задание 12 (3.4)
                            Console.Write("Введите x: ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            Console.Write("Введите y (y < 0): ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (y >= 0)
                            {
                                Console.WriteLine("y < 0");
                            }
                            else
                            {
                                Console.WriteLine("x = " + x);
                                Console.WriteLine("y = " + y);
                                Console.WriteLine("Результат: " + pr.pow(x, y));
                            }
                        }
                        break;
                    case 13:
                        {
                            // Задание 13 (3.6)
                            Console.Write("Введите x (-9 < x > 9): ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (x > 9 || x < -9)
                            {
                                Console.WriteLine("x = " + x);
                                Console.WriteLine("Результат: " + pr.equalNum(x));
                            }
                            else
                            {
                                Console.WriteLine("x однозначное число!!!");
                            }
                        }
                        break;
                    case 14:
                        {
                            // Задание 14 (3.8)
                            Console.Write("Введите x (x > 0): ");
                            if (!int.TryParse(Console.ReadLine(), out x))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (x > 0)
                            {
                                Console.WriteLine("x = " + x);
                                Console.WriteLine("Результат: ");
                                pr.leftTriangle(x);
                            }
                            else
                            {
                                Console.WriteLine("x < 0");
                            }
                        }
                        break;
                    case 15:
                        {
                            // Задание 15 (3.10)
                            pr.guessGame();
                        }
                        break;
                    case 16:
                        {
                            // Задание 16 (4.2)
                            Console.Write("Введите размер массива(> 0): ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (y > 0)
                            {
                                arr = pr.CreateArr(y);
                                Console.Write("arr = [ ");
                                pr.PrintArr(arr);
                                Console.WriteLine("]");
                                Console.WriteLine("Введите x: ");
                                if (!int.TryParse(Console.ReadLine(), out x))
                                {
                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                    break;
                                }
                                else
                                {
                                    z = pr.findLast(arr, x);
                                    Console.WriteLine("Результат: " + z);
                                }
                            }
                            else
                            {
                                Console.WriteLine("размер массива < 0");
                            }
                        }
                        break;
                    case 17:
                        {
                            // Задание 17 (4.4)
                            Console.Write("Введите размер массива(> 0): ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (y > 0)
                            {
                                arr = pr.CreateArr(y);
                                Console.Write("arr = [ ");
                                pr.PrintArr(arr);
                                Console.WriteLine("]");
                                Console.WriteLine("Введите x: ");
                                if (!int.TryParse(Console.ReadLine(), out x))
                                {
                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Введите pos: ");
                                    if (!int.TryParse(Console.ReadLine(), out z) || z<0)
                                    {
                                        Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                        break;
                                    }
                                    else
                                    {
                                        arr = pr.Add(arr, x, z);
                                        Console.Write("arr = [ ");
                                        pr.PrintArr(arr);
                                        Console.WriteLine("]");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("размер массива < 0");
                            }
                        }
                        break;
                    case 18:
                        {
                            // Задание 18 (4.6)
                            Console.Write("Введите размер массива(> 0): ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (y > 0)
                            {
                                arr = pr.CreateArr(y);
                                Console.Write("arr = [ ");
                                pr.PrintArr(arr);
                                Console.WriteLine("]");
                                pr.Reverse(arr);
                                Console.Write("Результат: arr = [ ");
                                pr.PrintArr(arr);
                                Console.WriteLine("]");
                            }
                            else
                            {
                                Console.WriteLine("размер массива < 0");
                            }
                        }
                        break;
                    case 19:
                        {
                            // Задание 19 (4.8)
                            Console.Write("Введите размер первого массива(> 0): ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (y > 0)
                            {
                                arr = pr.CreateArr(y);
                                Console.Write("arr1 = [ ");
                                pr.PrintArr(arr);
                                Console.WriteLine("]");
                                Console.Write("Введите размер второго массива(> 0): ");
                                if (!int.TryParse(Console.ReadLine(), out y))
                                {
                                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                    break;
                                }
                                else
                                {
                                    arr2 = pr.CreateArr(y);
                                    Console.Write("arr2 = [ ");
                                    pr.PrintArr(arr2);
                                    Console.WriteLine("]");
                                    arr = pr.Concat(arr, arr2);
                                    Console.Write("New_arr = [ ");
                                    pr.PrintArr(arr);
                                    Console.WriteLine("]");
                                }
                            }
                            else
                            {
                                Console.WriteLine("размер массива < 0");
                            }
                        }
                        break;
                    case 20:
                        {
                            // Задание 20 (4.10)
                            Console.Write("Введите размер массива(> 0): ");
                            if (!int.TryParse(Console.ReadLine(), out y))
                            {
                                Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число.");
                                break;
                            }
                            if (y > 0)
                            {
                                arr = pr.CreateArr(y);
                                Console.Write("arr = [ ");
                                pr.PrintArr(arr);
                                Console.WriteLine("]");
                                arr=pr.DeleteNegative(arr);
                                Console.Write("Результат: arr = [ ");
                                pr.PrintArr(arr);
                                Console.WriteLine("]");
                            }
                            else
                            {
                                Console.WriteLine("размер массива < 0");
                            }
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("нет такой задачи!!!");
                        }
                        break;
                }
                
                Console.Write("Y/N: ");
                Begin = char.Parse(Console.ReadLine());

            } while (Begin == 'Y');

            Console.ReadKey();
        }

        public int sumLastNums(int x) // Задание 1 (1.2)
        {
            x = Math.Abs(x);
            int x1 = x % 10;
            int x2 = x / 10 % 10;
            return x1 + x2;
        }

        public bool isPositive(int x) // Задание 2 (1.4)
        {
            if (x >= 0) return true;
            else return false;
           
        }

        public bool isUpperCase(char x) // Задание 3 (1.6)
        {
            if (x >= 'A' && x <= 'Z') return true;
            else return false;
        }

        public bool isDivisor(int a, int b) // Задание 4 (1.8)
        {
            if (a==0 || b==0) return false;
            if ((a % b == 0) || (b % a == 0)) return true;
            else return false;
        }

        public int lastNumSum(int a, int b) // Задание 5 (1.10)
        {
            return a % 10 + b % 10;
        }

        public double safeDiv(int x, int y) // Задание 6 (2.2)
        {
            if (y == 0) return 0;
            return (double)x / y;
        }

        public string makeDecision(int x, int y) // Задание 7 (2.4)
        {
            if (x == y) return x + " == " + y;
            else if (x > y) return x + " > " + y;
            else return x + " < " + y;
        }

        public bool sum3(int x, int y, int z) // Задание 8 (2.6)
        {
            return (x + y == z) || (y + z == x) || (z + x == y);
        }

        public string age(int x) // Задание 9 (2.8)
        {
            if (x % 10 == 1 && x % 100 != 11) return x + " год";
            else if ((x % 10 == 2 || x % 10 == 3 || x % 10 == 4) && (x % 100 != 12 && x % 100 != 13 && x % 100 != 14)) return x + " года";
            else return x + " лет";
        }

        public void printDays(string x) // Задание 10 (2.10)
        {
            switch (x.ToLower())
            {
                case "понедельник":
                    Console.WriteLine("понедельник");
                    goto case "вторник";
                case "вторник":
                    Console.WriteLine("вторник");
                    goto case "среда";
                case "среда":
                    Console.WriteLine("среда");
                    goto case "четверг";
                case "четверг":
                    Console.WriteLine("четверг");
                    goto case "пятница";
                case "пятница":
                    Console.WriteLine("пятница");
                    goto case "суббота";
                case "суббота":
                    Console.WriteLine("суббота");
                    goto case "воскресенье";
                case "воскресенье":
                    Console.WriteLine("воскресенье");
                    break;
                default:
                    Console.WriteLine("это не день недели");
                    break;
            }
        }

        public string reverseListNums(int x) // Задание 11 (3.2)
        {
            StringBuilder result = new StringBuilder();
            for (int i = x; i >= 0; i--)
            {
                result.Append(i);
                if (i != 0)
                {
                    result.Append(" ");
                }
            }
            return result.ToString();
        }

        public int pow(int x, int y) // Задание 12 (3.4)
        {
            int result = 1;
            for (int i = 0; i < y; i++)
            {
                result *= x;
            }
            return result;
        }

        public bool equalNum(int x) // Задание 13 (3.6)
        {
            if (x < 0)
            {
                x = -x; // Обрабатываем отрицательные числа
            }

            int firstDigit = x % 10; // Получаем последнюю цифру числа
            x /= 10; // Удаляем последнюю цифру

            while (x > 0)
            {
                int currentDigit = x % 10; // Получаем текущую последнюю цифру
                if (currentDigit != firstDigit)
                {
                    return false;
                }
                x /= 10; // Удаляем последнюю цифру
            }

            return true;
        }

        public void leftTriangle(int x) // Задание 14 (3.8)
        {
            for (int i = 1; i <= x; i++)
            {
                for (int j = 1; j <= i; j++)
                {
                    Console.Write("*");
                }
                Console.WriteLine();
            }
        }

        public void guessGame() // Задание 15 (3.10)
        {
            Random random = new Random();
            int targetNumber = random.Next(0, 10); // Генерируем случайное число от 0 до 9
            int attempts = 0;
            int userGuess;

            Console.WriteLine("Введите число от 0 до 9:");

            do
            {
                attempts++;
                if (!int.TryParse(Console.ReadLine(), out userGuess))
                {
                    Console.WriteLine("Некорректный ввод. Пожалуйста, введите целое число от 0 до 9.");
                    continue;
                }

                if (userGuess == targetNumber)
                {
                    Console.WriteLine("Вы угадали!");
                    Console.WriteLine("Вы отгадали число за " + attempts + " попытки");
                }
                else
                {
                    Console.WriteLine("Вы не угадали, введите число от 0 до 9:");
                }
            } while (userGuess != targetNumber);
        }

        public int findLast(int[] arr, int x)// Задание 16 (4.2)
        {
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                if (arr[i] == x)
                {
                    return i;
                }
            }
            return -1;
        }

        public int[] Add(int[] arr, int x, int pos)//Задание 17 (4.4)
        {
            // Создание нового массива на один элемент больше
            int[] newArr = new int[arr.Length + 1];

            // Копирование элементов до позиции pos
            for (int i = 0; i < pos; i++)
            {
                newArr[i] = arr[i];
            }

            // Вставка элемента x на позицию pos
            newArr[pos] = x;

            // Копирование оставшихся элементов после позиции pos
            for (int i = pos; i < arr.Length; i++)
            {
                newArr[i + 1] = arr[i];
            }

            return newArr;
        }

        public void Reverse(int[] arr)//ЗАдание 18 (4.6)
        {
            int left = 0;
            int right = arr.Length - 1;

            while (left < right)
            {
                // Меняем местами элементы на позициях left и right
                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;

                // Перемещаем указатели к центру массива
                left++;
                right--;
            }
        }

        public int[] Concat(int[] arr1, int[] arr2)// Задание 19 (4.8)
        {
            // Создание нового массива, который будет содержать элементы обоих массивов
            int[] result = new int[arr1.Length + arr2.Length];

            // Копирование элементов из первого массива в новый массив
            for (int i = 0; i < arr1.Length; i++)
            {
                result[i] = arr1[i];
            }

            // Копирование элементов из второго массива в новый массив
            for (int i = 0; i < arr2.Length; i++)
            {
                result[arr1.Length + i] = arr2[i];
            }

            return result;
        }

        public int[] DeleteNegative(int[] arr)//Задача 20 (4.10)
        {
            // Подсчет количества неотрицательных элементов
            int count = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] >= 0)
                {
                    count++;
                }
            }

            // Создание нового массива для неотрицательных элементов
            int[] result = new int[count];

            // Копирование неотрицательных элементов в новый массив
            int index = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] >= 0)
                {
                    result[index++] = arr[i];
                }
            }

            return result;
        }






        public int[] CreateArr(int x)// заполнение массива 
        {
            Random random = new Random();
            int[] arr = new int[x];

            for (int i = 0; i < x; i++)
            {
                arr[i] = random.Next(-20, 21);//рандом от -20 до 20
            }

            return arr;
        }

        public void PrintArr(int[] arr)//вывод массива
        {
            foreach (int item in arr)
            {
                Console.Write(item + " ");
            }
        }
    }
}
