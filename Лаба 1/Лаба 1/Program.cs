using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Лаба_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Program pr = new Program();

            char Begin;
            byte Ex = 0;
            int x, y,z;//инциализация переменных
            Console.Write("Введите Y/N(N-закончить выполнение программы,Y-продолжить): ");
            Begin = Console.ReadKey().KeyChar;Console.WriteLine(); 
            
            
            do
            {
                Console.Write("Выбирете номер задачи(1-20): ");
                Ex=byte.Parse(Console.ReadLine());
                switch (Ex) {
                    case 1:
                        {
                            //задание 1(1.2)
                            Console.Write("Введите число: ");
                            x = int.Parse(Console.ReadLine());
                            if (x > 9 || x < -9)
                            {
                                int EX1 = pr.sumLastNums(x);
                                Console.WriteLine("Сумма последних двух символов = " + EX1);
                            }
                            else
                            {
                                Console.WriteLine("В числе меньше 2 символов!!");
                            }
                        }
                        break;
                    case 2:
                        {
                            //задание 2(1.4)
                            Console.Write("Введите число: ");
                            x = int.Parse(Console.ReadLine());
                            bool EX2 = pr.isPositive(x);
                            if (EX2 == true) Console.WriteLine("Число положительное");
                            else Console.WriteLine("Число отрицательное");
                        }
                        break;
                    case 3:
                        {
                            //Задание 3(1.6)
                            Console.Write("Введите символ: ");
                            char S = Console.ReadKey().KeyChar;
                            Console.WriteLine();
                            bool Ex3 = pr.isUpperCase(S);
                            Console.WriteLine("Результат: " + Ex3);
                        }
                        break;
                    case 4:
                        {
                            //задание 4(1.8)
                            Console.Write("Введите x: ");
                            x = int.Parse(Console.ReadLine());
                            Console.Write("Введите y: ");
                            y = int.Parse(Console.ReadLine());
                            bool Ex4 = pr.isDivisor(x, y);
                            Console.WriteLine("Результат: " + Ex4);
                        }
                        break;
                    case 5:
                        {
                            //задача 5(1.10)
                            Console.Write("Введите первое число: ");
                            x = int.TryParse(Console.ReadLine());
                            for (int i = 0; i < 5; i++)
                            {
                                Console.Write("Введите второе число: ");
                                y = int.Parse(Console.ReadLine());
                                Console.Write(x + " + " + y);
                                x = pr.lastNumSum(x, y);
                                Console.WriteLine(" = " + x);
                            }
                            Console.WriteLine("Итог: " + x);
                        }
                        break;
                    case 6:
                        {
                            //задание 6(2.2)
                            Console.Write("Введите первое число: ");
                            x = int.Parse(Console.ReadLine());
                            Console.Write("Введите второе число: ");
                            y = int.Parse(Console.ReadLine());
                            double Ex6= pr.safeDiv(x, y);
                            Console.WriteLine("x / y = "+Ex6);
                        }
                        break;
                    case 7:
                        {
                            //задание 7(2.4)
                            Console.Write("Введите первое число: ");
                            x = int.Parse(Console.ReadLine());
                            Console.Write("Введите второе число: ");
                            y = int.Parse(Console.ReadLine());
                            string Ex7 = pr.makeDecision(x, y);
                            Console.WriteLine("x = " + x + "  y = " + y);
                            Console.WriteLine("Результат:"+Ex7);
                        }
                        break;
                    case 8:
                        {
                            // задание 8(2.6)
                            Console.Write("Введите x: ");
                            x = int.Parse(Console.ReadLine());
                            Console.Write("Введите y: ");
                            y = int.Parse(Console.ReadLine());
                            Console.Write("Введите z: ");
                            z = int.Parse(Console.ReadLine());
                            bool Ex8= pr.sum3(x,y, z);
                            Console.WriteLine("Результат: " + Ex8);
                        }
                        break;




                }
                Console.Write("Y/N: ");
                Begin=char.Parse(Console.ReadLine());

            } while (Begin=='Y');

            

            


           

            
            
            

            






            
            Console.Write("Введите a: ");
            x=int.Parse(Console.ReadLine());
            Console.Write("Введите b: ");
            y=int.Parse(Console.ReadLine());
            Console.Write("Введите num: ");
            z=int.Parse(Console.ReadLine());
            bool Ex9=pr.isInRange(x,y,z);


            Console.ReadKey();
        }

        public int sumLastNums(int x)//задание 1(1.2)
        {
            int x1 = Math.Abs(x) % 10;
            int x2 = Math.Abs(x) / 10 % 10;
            return x1 + x2;
        }

        public bool isPositive(int x)//задание 2(1.4)
        {
            if (x > 0) return true;
            else return false;
        }

        public bool isUpperCase(char x)//задание 3(1.6)
        {
            if (x>='A' && x<='Z') return true;
            else return false;
        }

        public bool isDivisor(int a, int b)//задание 4(1.8)
        {
            if ((a%b == 0) ||(b%a == 0)) return true;
            return false;
        }

        public int lastNumSum(int a, int b)//задание 5(1.10)
        {
            return a%10 + b%10;
        }

        public double safeDiv(int x, int y)//задание 6(2.2)
        { 
            if(y==0) return 0;
            else return (double)x / y;
        }

        public String makeDecision(int x, int y)//задание 7(2.4)
        {
            if(x==y) return x.ToString()+ " == "+y.ToString();
            else  if(x>y)return x.ToString()+" > "+y.ToString();
                  else return x.ToString()+" < "+y.ToString();
        }

        public bool sum3(int x, int y, int z)
        {
            if (x+y==z || y+z==x || z+x==y) return true;
            else return false;
        }//задание 8(2.6)








        public bool isInRange(int a, int b, int num)
        {
            if ((a <= num && b >= num) || (a >= num && b <= num)) return true;
            else return false;
        }
    }
}
