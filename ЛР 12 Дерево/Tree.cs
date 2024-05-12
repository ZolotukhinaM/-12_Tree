using ClassLibrary2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР_12_Дерево
{
    public class Tree<T> where T : IInit, IComparable, new()
    {
       public PointTree<T>? root = null;
        public int count = 0;

        public int Count => count;

        public Tree(int length)
        {
            count = length;
            root = MakeTree(length, root);
        }

        public void PrintTree()
        {
            Show(root);
        }

        public void Show(PointTree<T>? point, int space = 5)
        {
            if (point != null)
            {
                Show(point.Right, space + 5); // Рекурсивно вызываем сначала правое поддерево
                for (int i = 0; i < space; i++)
                {
                    Console.Write(" ");
                }
                Console.WriteLine(point.Data);
                Show(point.Left, space + 5); // Затем рекурсивно вызываем левое поддерево
            }
            else
            {
                Console.WriteLine("Дерево пусто!");
            }
        }

        PointTree<T>? MakeTree(int length, PointTree<T>? point)
        {
            if (length <= 0)
            {
                return null;
            }

            T data = new T();
            data.RandomInit();
            PointTree<T> newItem = new PointTree<T>(data);

            int nl = length / 2;
            int nr = length - nl - 1;
            newItem.Right = MakeTree(nl, newItem.Right);
            newItem.Left = MakeTree(nr, newItem.Left);

            return newItem;
        }

        public void AddPoint(T data)
        {
            PointTree<T>? point = root;
            PointTree<T>? current = null;
            bool isExist = false;

            while (point != null && !isExist)
            {
                current = point;
                if (point.Data.CompareTo(data) == 0) { isExist = true; }
                else
                {
                    if (point.Data.CompareTo(data) < 0) { point = point.Right; }
                    else { point = point.Left; }
                }
            }

            if (isExist) { return; }

            PointTree<T> newPoint = new PointTree<T>(data);
            if (current == null)
                root = newPoint;
            else if (current.Data.CompareTo(data) < 0)
                current.Right = newPoint;
            else
                current.Left = newPoint;

            count++;
        }
        public int TreeHeight()
        {
            return Height(root);
        }

        int Height(PointTree<T>? point)
        {
            if (point == null)
            {
                return 0;
            }
            else
            {
                int leftHeight =Height(point.Left);
                int rightHeight = Height(point.Right);
                return Math.Max(leftHeight, rightHeight) + 1;
            }
        }
        public void TransformToArray(PointTree<T>? point, T[] array, ref int current)
        {
            if (point != null)
            {
                TransformToArray(point.Left, array, ref current);
                array[current] = point.Data;
                current++;
                TransformToArray(point.Right, array, ref current);
            }
        }

        public void TransformToFindTree()
        {
            if (count == 0)
            {
                root = null;
                return;
            }

            T[] array = new T[count];
            int current = 0;
            TransformToArray(root, array, ref current);

            root = new PointTree<T>(array[0]);
            count = 0;
            for (int i = 1; i < array.Length; i++)
            {
                AddPoint(array[i]);
            }
        }
        public void DeleteByKey(T key)
        {
            root = Delete(root, key);
        }

        public PointTree<T>? Delete(PointTree<T>? point, T key)
        {
            if (point == null)
            {
                return point;
            }

            if (key.CompareTo(point.Data) < 0)
            {
                point.Left = Delete(point.Left, key);
            }
            else if (key.CompareTo(point.Data) > 0)
            {
                point.Right = Delete(point.Right, key);
            }
            else
            {
                if (point.Left == null)
                {
                    return point.Right;
                }
                else if (point.Right == null)
                {
                    return point.Left;
                }

                point.Data = MinValue(point.Right);
                point.Right = Delete(point.Right, point.Data);
            }

            return point;
        }
        private T MinValue(PointTree<T> point)
        {
            T minv = point.Data;
            while (point.Left != null)
            {
                minv = point.Left.Data;
                point = point.Left;
            }
            return minv;
        }
        public void RemoveTree()
        {
            root = null;
            count = 0;
        }
    }
}
