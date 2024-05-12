using ClassLibrary2;
using System;
using System.Collections;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Net;

namespace ЛР_12_Дерево
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int answer;
            bool isConvertAnswer;
            Console.WriteLine("Здравствуйте!");
            Console.WriteLine("Введите длину дерева:");
            bool isConvert;
            int length;
            do
            {
                string buf = Console.ReadLine();
                isConvert = int.TryParse(buf, out length);
                if (!isConvert)
                {
                    Console.WriteLine($"Неправильно введено число. Введите длину дерева снова:");
                }
            } while (!isConvert);
            Tree<CelestialBody> tree = new Tree<CelestialBody>(length);
            Console.WriteLine("Дерево создано!");
            Console.WriteLine("Выберите действие");
            bool exit = false;
            do
            {
                Console.WriteLine("1. Напечатать сбалансированное бинарное дерево");
                Console.WriteLine("2. Поиск высоты дерева");
                Console.WriteLine("3. Преобразовать сбалансированное бинарное дерево в дерево поиска");
                Console.WriteLine("4. Удалить из дерева поиска элемент с заданным ключом");
                Console.WriteLine("5. Удалить дерево полностью");
                Console.WriteLine("6. Выйти");
                Console.WriteLine("Выберите действие:");
                do
                {
                    isConvertAnswer = int.TryParse(Console.ReadLine(), out answer);
                    if (!isConvertAnswer)
                        Console.WriteLine("Неправильно введено число. \n" + "Попробуйте ещё раз.");
                } while (!isConvertAnswer);
                switch (answer)
                {
                    case 1:
                        tree.PrintTree();
                        break;
                    case 2:
                        int height = tree.TreeHeight();
                        Console.WriteLine("Высота дерева: " + height);
                        break;
                    case 3:
                        Console.WriteLine("Дерево до преобразования:");
                        tree.PrintTree();

                        tree.TransformToFindTree();

                        Console.WriteLine("\nДерево после преобразования в дерево поиска:");
                        tree.PrintTree();
                        break;
                    case 4:
                        Console.WriteLine("Введите имя объекта, чтобы его удалить");
                        string name = Console.ReadLine();
                        Console.WriteLine("Введите массу объекта, чтобы его удалить");
                        int weight = int.Parse(Console.ReadLine());
                        Console.WriteLine("Введите радиус объекта, чтобы его удалить");
                        int radius = int.Parse(Console.ReadLine());
                        CelestialBody body = new CelestialBody(name, weight, radius);
                        tree.DeleteByKey(body);
                        break;
                    case 5:
                        tree.RemoveTree();
                        break;
                    case 6:
                        exit = true;
                        Console.WriteLine("Выход из программы.");
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }

            } while (!exit);
        }
    }
}



