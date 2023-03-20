using System;

class LinkedListNode<T>
{
    public T Value { get; private set; }
    public LinkedListNode<T> Next { get; internal set; }

    public LinkedListNode(T value)
    {
        Value = value;
        Next = null;
    }
}

class LinkedList<T>
{
    private LinkedListNode<T> head;

    public void Add(T value)
    {
        LinkedListNode<T> newNode = new LinkedListNode<T>(value);

        if (head == null)
        {
            head = newNode;
        }
        else
        {
            LinkedListNode<T> currentNode = head;
            while (currentNode.Next != null)
            {
                currentNode = currentNode.Next;
            }
            currentNode.Next = newNode;
        }
    }

    public void ForEach(Action<T> action)
    {
        LinkedListNode<T> currentNode = head;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.Next;
        }
    }

    public T Max()
    {
        T max = head.Value;
        LinkedListNode<T> currentNode = head.Next;
        while (currentNode != null)
        {
            if (Comparer<T>.Default.Compare(currentNode.Value, max) > 0)
            {
                max = currentNode.Value;
            }
            currentNode = currentNode.Next;
        }
        return max;
    }

    public T Min()
    {
        T min = head.Value;
        LinkedListNode<T> currentNode = head.Next;
        while (currentNode != null)
        {
            if (Comparer<T>.Default.Compare(currentNode.Value, min) < 0)
            {
                min = currentNode.Value;
            }
            currentNode = currentNode.Next;
        }
        return min;
    }

    public T Sum()
    {
        T sum = default(T);
        LinkedListNode<T> currentNode = head;
        while (currentNode != null)
        {
            sum = (dynamic)sum + currentNode.Value;
            currentNode = currentNode.Next;
        }
        return sum;
    }
}

class Program
{
    static void Main(string[] args)
    {
        LinkedList<int> list = new LinkedList<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);

        list.ForEach(x => Console.Write(x + " ")); // 打印链表元素
        Console.WriteLine();

        Console.WriteLine("Max: " + list.Max()); // 求最大值
        Console.WriteLine("Min: " + list.Min()); // 求最小值
        Console.WriteLine("Sum: " + list.Sum()); // 求和
    }
}
