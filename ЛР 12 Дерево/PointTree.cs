using ClassLibrary2;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ЛР_12_Дерево
{
    public class PointTree<T>
    {
        public T Data { get; set; }
        public PointTree<T> Right { get; set; }
        public PointTree<T>? Left { get; set; }

        public PointTree()
        {
            this.Data = Activator.CreateInstance<T>();
            this.Left = null;
            this.Right = null;
        }
        public PointTree(T data)
        {
            this.Data = data;
            this.Left = null;
            this.Right = null;
        }
        public override string ToString()
        {
            return Data == null ? "" : Data.ToString();
        }
        public override int GetHashCode()
        {
            return Data == null ? 0 : Data.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            PointTree<T> other = (PointTree<T>)obj;
  
            if (EqualityComparer<T>.Default.Equals(Data, other.Data))
            {
                bool leftEquals = (Left == null && other.Left == null) || (Left != null && Left.Equals(other.Left));

                bool rightEquals = (Right == null && other.Right == null) || (Right != null && Right.Equals(other.Right));

                return leftEquals && rightEquals;
            }

            return false;
        }
        public string GetKey()
        {
            return Data.ToString();
        }
    }
}
