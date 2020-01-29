
namespace LinkedLists
{
    using System;
    using System.Text;

    class Program
    {
        public static void Main()
        {
        }

        public class LinkedList<T> where T : IComparable<T>
        {
            public uint Count { get; set; }

            public LinkedListNode<T> Head { get; set; }

            public LinkedList()
            {
                Head = null;
            }

            public bool IsEmpty => Head == null;

            public bool Search(T value)
            {
                LinkedListNode<T> temp = !IsEmpty ? Head : null;
                if (temp == null)
                    return false;

                while (true)
                {
                    if (temp.NodeValue.CompareTo(value) == 0)
                        return true;

                    temp = temp._Next;

                    if (temp == null)
                        break;
                }
                return false;
            }

            public void AddFirst(T value)
            {
                LinkedListNode<T> node = new LinkedListNode<T>() { NodeValue = value };
                node._Next = Head;
                Head = node;
                Count++;
            }

            public void Remove(T value)
            {
                LinkedListNode<T> vertex = !IsEmpty ? Head : null;
                if (vertex == null)
                    return;

                if (vertex.NodeValue.CompareTo(value) == 0)
                {
                    Head = Head._Next;
                    return;
                }

                while (vertex != null)
                {
                    if (vertex._Next?.NodeValue.CompareTo(value) == 0)
                    {
                        LinkedListNode<T> delete = vertex._Next;
                        vertex._Next = delete._Next;
                        Count--;
                        break;
                    }
                    vertex = vertex._Next;
                }
            }

            public void RemoveAt(int index)
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                LinkedListNode<T> vertex = !IsEmpty ? Head : null;
                if (vertex == null)
                    return;

                if (index == 0)
                {
                    Head = Head._Next;
                    Count--;
                }

                for (int i = 0; i < index - 1; i++)
                {
                    vertex = vertex._Next;

                }
                LinkedListNode<T> delete = vertex._Next;
                vertex._Next = delete._Next;
                Count--;
            }

            public override string ToString()
            {
                LinkedListNode<T> currentLink = Head;
                StringBuilder builder = new StringBuilder();
                while (currentLink != null)
                {
                    builder.Append($"{currentLink.NodeValue} ");
                    currentLink = currentLink._Next;
                }
                return builder.ToString();
            }
        }

        public class LinkedListNode<T> where T : IComparable<T>
        {
            public LinkedListNode<T> _Next { get; set; }

            public T NodeValue { get; set; }
        }
    }
}
