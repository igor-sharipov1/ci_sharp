using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks.Dataflow;

namespace lab1
{
    class Program
    {
        static void Main(string[] args)
        {     
            Console.WriteLine("Hello World!");
            Arrays first_array = new Arrays();
            Arrays second_array = new Arrays();

            bool array_select = true;

            while (array_select)
            {
                Console.WriteLine("--------------------------------------------\nРабота с первым массивом (введите \"1\"), работа со вторым массивом (введите \"2\"),\nработа с обоими массивами (введите \"3\"), выйти (введите \"exit\")\n--------------------------------------------");
                string array_number = Console.ReadLine();
                if ((array_number == "1") || (array_number == "2"))
                    Menu_of_one(array_choice(array_number, first_array, second_array));
                else if (array_number == "3")
                    Menu_of_both(first_array, second_array);
                else if (array_number == "exit")
                    array_select = false;
                else
                    Console.WriteLine("неверно введены данные, попробуйте еще раз");
            }


        }

        static Arrays array_choice(string array_number, Arrays first_array, Arrays second_array) 
        {
            if (array_number == "1")
                return first_array;
            else
                return second_array;
        }

        static Arrays Menu_of_one(Arrays any_array)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("--------------------------------------------\nЧто вы хотите сделать с массивом: \nдобавить элемент (введите \"1\"), вывести элемент по индексу (введите \"2\"), вывести весь массив (введите \"3\")\n" +
                    "начать работу с массивом элементов, начинающихся с <<введенное значение>> (введите \"4\"),\nвыйти в предыдущее меню (введите \"exit\")\n--------------------------------------------");
                string vote = Console.ReadLine();
                if (vote == "1")
                    Arrays.AddElement(any_array.array);
                else if (vote == "2")
                    Console.WriteLine(Arrays.FindElementByIndex(any_array.array));
                else if (vote == "3")
                    Arrays.WriteArray(any_array.array);
                else if (vote == "4")
                {
                    Arrays elements = Arrays.FindElementByBeginning(any_array.array);
                    if (elements.array.Count != 0)
                         Menu_of_one(elements);
                }
                else if (vote == "exit")
                    flag = false;
                else
                    Console.WriteLine("неверный ввод, попробуйте еще раз");
            }
            return any_array;
        }
        static void Menu_of_both(Arrays first_array, Arrays second_array)
        {
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("--------------------------------------------\nЧто вы хотите сделать с массивами: \nнайти объединение массивов (введите \"1\"), найти пересечение массивов (введите \"2\"), сцепить массивы поэлементно (введите \"3\")\n" +
                   "выйти в предыдущее меню (введите \"exit\")\n--------------------------------------------");
                string vote = Console.ReadLine();
                if (vote == "1")
                {
                    Console.WriteLine("выберите действие с массивом объединения");
                    Menu_of_one(Arrays.Union(first_array.array, second_array.array));
                }
                else if (vote == "2")
                {
                    Console.WriteLine("выберите действие с массивом пересечения");
                    Menu_of_one(Arrays.Crossing(first_array.array, second_array.array));
                }
                else if (vote == "3")
                {
                    Console.WriteLine("выберите действие с массивом поэлементного сцепления");
                    Menu_of_one(Arrays.PairCouple(first_array.array, second_array.array));
                }
                else if (vote == "exit")
                    flag = false;
                else
                    Console.WriteLine("неверный ввод");
            }
        }
    }



    class Arrays
    {
        public List<string> array = new List<string> ();//создание массива



        public static List<string> AddElement(List<string> array)//добавление элемента в массив
        {
            Console.WriteLine("Введите элемент");
            string element = Console.ReadLine();
            if (element.Length == 0) 
                Console.WriteLine("ничего не было введено!");
            else
                array.Add(element);

            return array;
        }

        public static void WriteArray(List<string> array)//вывод всех элементов массива через запятую
        {
          foreach(string i in array)
          {
             Console.Write(i + " | ");
          }
            Console.WriteLine();
        }


        static bool IndexControl(List<string> array, int index)//проверка корректности индекса
        {   
            if (index >= 0 && index < array.Count)
                return true;
            else
                return false;
        }

        public static string FindElementByIndex(List<string> array)  //обращение к отдельным строкам по индексам
        {
            Console.WriteLine("введите индекс");
            try
            {
                int i = Convert.ToInt32(Console.ReadLine());
                if (IndexControl(array, i))
                    return array[i];
                else
                    return "Индекс вне массива";
            }
            catch (FormatException)
            {
                return "Индекс должен быть числом";
            }
        }
        public static Arrays FindElementByBeginning(List<string> array) //нахождение элементов, начинающихся с введенной строки
        {
            Arrays element = new Arrays();
            Console.WriteLine("введите начало элемента");
            string beginning = Console.ReadLine();
            foreach (string i in array)
            {
                if (i.StartsWith(beginning))
                {
                    element.array.Add(i);
                }
            }
                       
            foreach(string el in element.array)//если элементов нет, то ничего не выведется
            {
                Console.Write(el + " | ");
            }
            Console.WriteLine();
            return element;            
        }

        public static Arrays Crossing (List<string> first_array, List<string> second_array)//пересечение массивов
        {
            IEnumerable<string> enter = first_array.Intersect(second_array);
            Arrays crossing = new Arrays();
            foreach (string i in enter)
            {
                Console.Write(i+" | ");
                crossing.array.Add(i);
            }
            Console.WriteLine();
            return crossing;
        }

        public static Arrays Union(List<string> first_array, List<string> second_array)//объединение массивов
        {
            IEnumerable<string> enter = first_array.Union(second_array);
            Arrays union = new Arrays();
            foreach (string i in enter)
            {
                Console.Write(i + " | ");
                union.array.Add(i);
            }
            Console.WriteLine();
            return union;
        }

        public static Arrays PairCouple (List<string> first_array, List<string> second_array)//поэлементное сцепление
        {
            Arrays couple = new Arrays();
            int max;
            if (first_array.Count > second_array.Count)
            {
                max = second_array.Count;
                for (int i = 0; i < max; i++)
                {
                    couple.array.Add(first_array[i] + second_array[i]);
                }
                while (max < first_array.Count)
                {
                    couple.array.Add(first_array[max]);
                    max++;
                }
            }
            else
            {
                max = first_array.Count;
                for (int i = 0; i < max; i++)
                {
                    couple.array.Add(first_array[i] + second_array[i]);
                }
                while (max < second_array.Count)
                {
                    couple.array.Add(second_array[max]);
                    max++;
                }
            }
            return couple;
        }
    }   
}
