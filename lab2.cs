using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;

namespace lab2
{

    class Multitude
    {
        public List<string> multitude = new List<string>();

        public static bool IsItFloat(string i)
        {
            try
            {
                float a = float.Parse(i);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool IsItThere(string i, List<string> multitude)
        {
            if (multitude.Count == 0)
            {
                return true;
            }
            else
            {
                foreach (string element in multitude)
                {
                    if (float.Parse(i) == float.Parse(element))
                        return false;
                }
                return true;
            }
        }

        public static Multitude operator ++(Multitude element)//добавление элемента в множество
        {
            Console.WriteLine("введите добавляемый элемент");
            string i = Console.ReadLine();
            if (IsItFloat(i))
            {
                if (IsItThere(i, element.multitude))
                    element.multitude.Add(i);
            }
            return element;
        }

        public static Multitude operator --(Multitude element)//удаление элемента из множества
        {
            Console.WriteLine("введите удаляемый элемент");
            string i = Console.ReadLine();
            if (IsItFloat(i))
                element.multitude.Remove(i);
            return element;
        }

        public static Multitude operator +(Multitude element1, Multitude element2)
        {
            Multitude crossing = new Multitude();
            foreach (string i in element1.multitude)
                crossing.multitude.Add(i);

            foreach (string i in element2.multitude)
            {
                bool flag = crossing.multitude.Contains(i);
                if (!flag)
                    crossing.multitude.Add(i);
            }
            return crossing;
        }

        public static Multitude operator *(Multitude element1, Multitude element2)
        {
            Multitude union = new Multitude();

            foreach (string i in element2.multitude)
            {
                if (element1.multitude.Contains(i))
                    union.multitude.Add(i);
            }
            return union;
        }

        public static void WriteArray(Multitude print)
        {
            foreach (string i in print.multitude)
            {
                Console.Write(i + " | ");
            }
            Console.WriteLine();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Multitude first = new Multitude();
            Multitude second = new Multitude();

            bool flag = true;
            while (true)
            {
                Console.WriteLine("---------------------------------------------\nЧто вы хотите сделать с множеством: Добавить элемент в первое множество (напишите \"1\")\n" +
    "Удалить элемент из первого множества (напишите \"2\"), Вывести на экран все первое множество (напишите \"3\")\n" +
    "Добавить элемент во второе множество (напишите \"4\"), Удалить элемент из второго множества (напишите \"5\")\n" +
    "Вывести на экран все второе множество (напишите \"6\"), Вывести пересечение множеств (напишите \"7\")\n" +
    "Вывести объединение множеств (напишите \"8\"), Выйти (напишите \"0\")\n---------------------------------------------");
                string choose = Console.ReadLine();
                if (choose == "1")
                    first++;
                else if (choose == "2")
                    first--;
                else if (choose == "3")
                    Multitude.WriteArray(first);
                else if (choose == "4")
                    second++;
                else if (choose == "5")
                    second--;
                else if (choose == "6")
                    Multitude.WriteArray(second);
                else if (choose == "7")
                {
                    Multitude.WriteArray(first * second);
                }
                else if (choose == "8")
                {
                    Multitude.WriteArray(first + second);
                }
                else if (choose == "0")
                    break;
                else
                    Console.WriteLine("Неверный ввод, попробуйте еще раз");
            }
        }

    }

}
