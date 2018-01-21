using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    public int Count { get; private set; }
    private ListNode<T> Head;
    private ListNode<T> Tail;

    public void AddFirst(T element)
    {
        if(this.Count == 0)
        {
            HandleEmptyList(element, "ADD");
        }
        else
        {
            var listHead = new ListNode<T>(element);
            listHead.Next = this.Head;
            this.Head.Prev = listHead;
            this.Head = listHead;
        }
        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            HandleEmptyList(element, "ADD");
        }
        else
        {
            var listHead = new ListNode<T>(element);
            listHead.Prev = this.Tail;
            this.Tail.Next = listHead;
            this.Tail = listHead;
        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        if(this.Count == 0)
        {
            HandleEmptyList(default(T), "REMOVE");
        }
        var element = this.Head.Value;
        if(this.Count == 1)
        {
            this.Head = this.Tail = this.Head.Prev = this.Head.Next = this.Tail.Prev = this.Tail.Next = null;
        }
        else
        {
            this.Head = this.Head.Next;
            this.Head.Prev = null;
        }

        this.Count--;
        return element;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            HandleEmptyList(default(T), "REMOVE");
        }
        var element = this.Tail.Value;
        if (this.Count == 1)
        {
            this.Head = this.Tail = this.Head.Prev = this.Head.Next = this.Tail.Prev = this.Tail.Next = null;
        }
        else
        {
            this.Tail = this.Tail.Prev;
            this.Tail.Next = null;
        }

        this.Count--;
        return element;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var headNode = this.Head;
        while(headNode != null)
        {
            yield return headNode.Value;
            headNode = headNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        var array = new T[this.Count];
        var listHead = this.Head;
        var index = 0;
        while(listHead != null)
        {
            array[index] = listHead.Value;
            listHead = listHead.Next;
            index++;
        }

        return array;
    }

    private void HandleEmptyList(T element, string v)
    {
        switch (v)
        {
            case "ADD":
                {
                    this.Head = this.Tail = new ListNode<T>(element);
                    break;
                }
            case "REMOVE":
                {
                    throw new InvalidOperationException("List is empty.");
                }
        }
    }

    public void ForEach(Action<T> action)
    {
        var currentNode = this.Head;
        while(currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.Next;
        }
    }

    private class ListNode<T>
    {
        public T Value { get; private set;}
        public ListNode<T> Next { get; set; }
        public ListNode<T> Prev { get; set; }

        public ListNode(T value)
        {
            this.Value = value;
            this.Prev = null;
            this.Prev = null;
        }
    }
}


class Example
{
    static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.AddLast(5);
        list.AddFirst(3);
        list.AddFirst(2);
        list.AddLast(10);
        Console.WriteLine("Count = {0}", list.Count);

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveFirst();
        list.RemoveLast();
        list.RemoveFirst();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");

        list.RemoveLast();

        list.ForEach(Console.WriteLine);
        Console.WriteLine("--------------------");
    }
}
