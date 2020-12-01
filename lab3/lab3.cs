using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace lab3
{

    public abstract class Figure
    {
        public double Size { get; set; } //обозначаем поля абстрактного класса
        public int Frame { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }


        public abstract double GetSize();
    }
    public class Square : Figure  //наследуем абстрактный класс в класс каждой отдельной фигуры
    {
        public override double GetSize()//переопределяем метод нахождения площади
        {
            Size = (Width + Frame) * (Height + Frame);
            return Size;
        }
    }

    public class Rectangle : Figure
        public override double GetSize()
        {
            Size = (Height + Frame) * (Width + Frame);
            return Size;
        }
    }

    public class Circle : Figure
    {
        public override double GetSize()
        {
            Size = (Width + Frame) * (Height + Frame) * 3.14;
            return Size;
        }
    }

    public class Ellipse : Figure
    {
 
        public override double GetSize()
        {
            Size = (Width + Frame) * (Height + Frame) * 3.14;
            return Size;
        }
    }

    class FigureComparer : IComparer<Figure> //класс для сравнения двух объектов
{
        public int Compare(Figure first, Figure second)
        {
            if (first.Size > second.Size)
                return 1;
            else if (first.Size < second.Size)
                return -1;
            else
            {
                if (first.Frame > second.Frame)
                    return 1;
                else if (first.Frame < second.Frame)
                    return -1;
                else
                    return 0;
            }
        }
    }

    [Serializable]//добавляем возможность для сериализации этого класса
public class GraphicEditor//графический редактор, включающий в себя список фигур и методы построения фигур в консоль
{
        public List<Figure> ListOfFigure = new List<Figure>();

        public void DrawEllipse(List<Figure> ListOfFigure, int number)
        {
            double a = ListOfFigure[number].Width;
            double b = ListOfFigure[number].Height;
            int roof = -1;
            double equation;
            double x;
            double y;
            if ((ListOfFigure[number].Width > Console.WindowWidth/2 - 1) || (ListOfFigure[number].Height > Console.WindowHeight/2 - 1))
            {
                Console.WriteLine("Фигура не вместится в консоль");
            }
            else
            {
                for (int i = 0; i < ListOfFigure[number].Width + 1; i++)
                {
                    for (int j = 0; j < ListOfFigure[number].Height - roof; j++)
                    {
                        x = i;
                        y = j;
                        equation = Math.Abs(((x * x) / (a * a)) + ((y * y) / (b * b)) - 1);
                        if (equation < 0.03)
                        {

                            Console.SetCursorPosition(Console.WindowWidth / 2 - 2 * i, Console.WindowHeight / 2 - j);
                            Console.Write("*");
                            Console.SetCursorPosition(Console.WindowWidth / 2 + 2 * i, Console.WindowHeight / 2 - j);
                            Console.Write("*");
                            Console.SetCursorPosition(Console.WindowWidth / 2 - 2 * i, Console.WindowHeight / 2 + j);
                            Console.Write("*");
                            Console.SetCursorPosition(Console.WindowWidth / 2 + 2 * i, Console.WindowHeight / 2 + j);
                            Console.Write("*");
                            roof++;
                        }
                    }

                }
                Console.SetCursorPosition(0, Console.WindowHeight);
            }
        }

        public void DrawRectangle(List<Figure> ListOfFigure, int number)
        {
            string top = "";
            string side = "* ";

            if ((ListOfFigure[number].Width > Console.WindowWidth) || (ListOfFigure[number].Height > Console.WindowHeight))
            {
                Console.WriteLine("Фигура не вместится в консоль");
            }
            else
            {
                for (int i = 0; i < ListOfFigure[number].Width; i++)
                {
                    top = top + "* ";
                }

                for (int i = 1; i < ListOfFigure[number].Width - 1; i++)
                {
                    side = side + "  ";
                }
                side = side + "*";

                Console.SetCursorPosition(Console.WindowWidth / 2 - ListOfFigure[number].Width + 1, Console.WindowHeight / 2 - ListOfFigure[number].Height / 2);
                Console.Write(top);
                for (int j = -ListOfFigure[number].Height / 2 + 1; j < ListOfFigure[number].Height / 2 - 1; j++)
                {
                    Console.WriteLine();
                    Console.SetCursorPosition(Console.WindowWidth / 2 - ListOfFigure[number].Width + 1, Console.WindowHeight / 2 + j);
                    Console.Write(side);
                }
                Console.SetCursorPosition(Console.WindowWidth / 2 - ListOfFigure[number].Width + 1, Console.WindowHeight / 2 + ListOfFigure[number].Height / 2 - 1);
                Console.Write(top);

                Console.SetCursorPosition(0, Console.WindowHeight);
            }
        }

        

    }
    public class Program 
    {

        public static bool IsItInt(string i)//проверка, является ли число целым и положительным
    {
            try
            {
                int a = Convert.ToInt32(i);
                if (a > 0)
                    return true;
                else
                    return false;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        static void Main(string[] args)
        {
            double AllSize = 0;
            int FigureCount = 0;
            GraphicEditor editor = new GraphicEditor();
            bool menu = true;
            FigureComparer comparer = new FigureComparer();
            while (menu)
            {
                Console.WriteLine("__________________________________________________\n" +
                    "введите 1, чтобы добавить эллипс | введите 2, чтобы добавить круг | \nвведите 3, чтобы добавить прямоугольник | введите 4, чтобы добавить квадрат\n" +
                    "введите 5, чтобы вывести на экран все фигуры | введите 6, чтобы вывести тип первых трех фигур | \nвведите 7, чтобы вывести две последние фигуры в консоль | введите 8, чтобы вывести среднюю площадь всех фигур" +
                    "\nвведите 0, чтобы выйти\n__________________________________________________");
                string choose = Console.ReadLine();
                if (choose == "1")
                {
                    Console.WriteLine("Введите верхнюю полуось эллипса");
                    string height_string = Console.ReadLine();
                    Console.WriteLine("Введите боковую полуось эллипса");
                    string width_string = Console.ReadLine();
                    Console.WriteLine("Введите ширину рамки");
                    string frame_string = Console.ReadLine();
                    if (IsItInt(width_string) && IsItInt(height_string) && IsItInt(frame_string))
                    {
                        editor.ListOfFigure.Add(new Ellipse {Height = Convert.ToInt32(height_string), Width = Convert.ToInt32(width_string), Frame = Convert.ToInt32(frame_string), Name = "Эллипс"});
                        editor.ListOfFigure[editor.ListOfFigure.Count - 1].GetSize();
                        AllSize += editor.ListOfFigure[editor.ListOfFigure.Count - 1].Size;
                        FigureCount++;
                    }
                    else
                        Console.WriteLine("Неверный ввод - число должно быть целым и положительным");                  
                }
                else if (choose == "2")
                {
                    Console.WriteLine("Введите радиус круга");
                    string height_string = Console.ReadLine();
                    Console.WriteLine("Введите ширину рамки");
                    string frame_string = Console.ReadLine();
                    if (IsItInt(height_string) && IsItInt(frame_string))
                    {
                        editor.ListOfFigure.Add(new Circle {Width = Convert.ToInt32(height_string), Height = Convert.ToInt32(height_string), Frame = Convert.ToInt32(frame_string) , Name = "Круг"});
                        editor.ListOfFigure[editor.ListOfFigure.Count - 1].GetSize();
                        AllSize += editor.ListOfFigure[editor.ListOfFigure.Count - 1].Size;
                        FigureCount++;
                    }
                    else
                        Console.WriteLine("Неверный ввод - число должно быть целым и положительным");                   
                }
                else if (choose == "3")
                {
                    Console.WriteLine("Введите высоту прямоугольника");
                    string height_string = Console.ReadLine();
                    Console.WriteLine("Введите ширину прямоугольника");
                    string width_string = Console.ReadLine();
                    Console.WriteLine("Введите ширину рамки");
                    string frame_string = Console.ReadLine();
                    if (IsItInt(width_string) && IsItInt(height_string) && IsItInt(frame_string))
                    {
                        editor.ListOfFigure.Add(new Rectangle { Height = Convert.ToInt32(height_string), Width = Convert.ToInt32(width_string), Frame = Convert.ToInt32(frame_string), Name = "Прямоугольник" });
                        editor.ListOfFigure[editor.ListOfFigure.Count - 1].GetSize();
                        AllSize += editor.ListOfFigure[editor.ListOfFigure.Count - 1].Size;
                        FigureCount++;
                    }
                    else
                        Console.WriteLine("Неверный ввод - число должно быть целым и положительным");                   
                }
                else if (choose == "4")
                {
                    Console.WriteLine("Введите длину квадрата");
                    string height_string = Console.ReadLine();
                    Console.WriteLine("Введите ширину рамки");
                    string frame_string = Console.ReadLine();
                    if (IsItInt(height_string) && IsItInt(frame_string))
                    {
                        editor.ListOfFigure.Add(new Square { Width = Convert.ToInt32(height_string), Height = Convert.ToInt32(height_string), Frame = Convert.ToInt32(frame_string), Name = "Квадрат" });
                        editor.ListOfFigure[editor.ListOfFigure.Count - 1].GetSize();
                        AllSize += editor.ListOfFigure[editor.ListOfFigure.Count - 1].Size;
                        FigureCount++;
                    }
                    else
                        Console.WriteLine("Неверный ввод - число должно быть целым и положительным");
                }
                else if (choose == "5")
                {
                    editor.ListOfFigure.Sort(comparer);
                    for (int i = 0; i < editor.ListOfFigure.Count; i++)
                    {
                        Console.WriteLine("Название фигуры: " + editor.ListOfFigure[i].Name + " Площадь фигуры: " + editor.ListOfFigure[i].Size + " Толщина рамки: " + editor.ListOfFigure[i].Frame);
                    }
                }
                else if (choose == "6")
                {
                    editor.ListOfFigure.Sort(comparer);
                    if (editor.ListOfFigure.Count >= 3)
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            Console.WriteLine(editor.ListOfFigure[i].Name);
                        }
                    }
                    else
                        Console.WriteLine("Количество фигур в списке меньше трех!");
                }
                else if (choose == "7")
                {
                    editor.ListOfFigure.Sort(comparer);
                    if (editor.ListOfFigure.Count >= 2)
                    {
                        for (int k = editor.ListOfFigure.Count - 2; k < editor.ListOfFigure.Count; k++)
                        {
                            if ((editor.ListOfFigure[k].Name == "Эллипс") || (editor.ListOfFigure[k].Name == "Круг"))
                            {
                                Console.Clear();
                                editor.DrawEllipse(editor.ListOfFigure, k);
                                Console.ReadKey();
                            }
                            else
                            {
                                Console.Clear();
                                editor.DrawRectangle(editor.ListOfFigure, k);
                                Console.ReadKey();
                            }
                        }
                    }
                    else
                        Console.WriteLine("Количество фигур в списке меньше двух!");
                }
                else if (choose == "8")
                {
                    Console.WriteLine(AllSize / FigureCount);
                }
                else if (choose == "0")
                    menu = false;              
            }
            var path1 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//ToCodeSerialization.xml";
            var path2 = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "//FromCodeSerialization.xml";
            XmlSerializer writer = new XmlSerializer(typeof(GraphicEditor));

            StreamReader file2 = new StreamReader(path1);//десериализация
        GraphicEditor DeserializedList = (GraphicEditor)writer.Deserialize(file2);
            file2.Close();
            Console.Write(DeserializedList.ListOfFigure[0].Name);

            FileStream file1 = File.Create(path2);//сериализация
        writer.Serialize(file1, editor);
            file1.Close();

            
        }
    }
}
