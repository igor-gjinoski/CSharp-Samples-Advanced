
namespace SortedList
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }

    public class SortedList<T> where T : IComparable<T>
    {
        internal Node<T> Root { get; set; }

        public int Size { get; set; }

        public SortedList(IEnumerable<T> values) : this(values.ToArray())
        {
        }

        public SortedList(params T[] values)
        {
            foreach (T item in values)
            {
                Insert(item);
                Size++;
            }
        }

        /// <summary>
        /// Adding new elements using Iterations
        /// </summary>
        public void Add(T value)
        {
            Node<T> newNode = new Node<T>();
            newNode.Value = value;

            if (Root == null)
            {
                Root = newNode;
                return;
            }

            Node<T> focusNode = Root;
            Node<T> parent;

            while (true)
            {
                parent = focusNode;

                if (value.CompareTo(focusNode.Value) < 0)
                {
                    focusNode = focusNode.Left;

                    if (focusNode == null)
                    {
                        parent.Left = newNode;
                        return;
                    }
                }
                else
                {
                    focusNode = focusNode.Right;

                    if (focusNode == null)
                    {
                        parent.Right = newNode;
                        return;
                    }
                }
                // All Done
            }
        }

        /// <summary>
        /// Adding new elements using Recursion
        /// </summary>
        public void Insert(T value)
        {
            if (Root == null)
            {
                Root = new Node<T>() { Value = value };
                return;
            }

            Root = _Ins(Root, value);

            static Node<T> _Ins(Node<T> focusNode, T value)
            {
                if (focusNode == null)
                {
                    focusNode = new Node<T>();
                    focusNode.Value = value;
                    return focusNode;
                }

                if (value.CompareTo(focusNode.Value) > 0)
                {
                    focusNode.Right = _Ins(focusNode.Right, value);
                }
                else focusNode.Left = _Ins(focusNode.Left, value);

                return focusNode;
            }
        }
    }

    internal class Node<T> where T : IComparable<T>
    {
        public T Value { get; set; }

        public Node<T> Left { get; set; }

        public Node<T> Right { get; set; }

        public Node() : this(default(T))
        {
        }
        public Node(T value)
            => Value = value;
    }
}
