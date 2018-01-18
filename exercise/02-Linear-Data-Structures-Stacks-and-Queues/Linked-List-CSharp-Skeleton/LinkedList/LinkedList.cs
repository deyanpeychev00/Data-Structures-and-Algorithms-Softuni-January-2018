using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    private Node Head;
    private Node Tail;

    public int Count { get; private set; }
 
    public LinkedList()
    {
        this.Head = null;
        this.Tail = null;
        this.Count = 0;
    }

    public void AddFirst(T item)
    {
        var addedNode = new Node(item);
        if (this.Count == 0)
        {
            handleEmptyList(addedNode, "ADD");
        }
        else
        {
            addedNode.Next = this.Head;
            this.Head = addedNode;
        }
        this.Count++;
    }

    public void AddLast(T item)
    {
        var addedNode = new Node(item);
        if (this.Count == 0)
        {
            handleEmptyList(addedNode, "ADD");
        }
        else
        {
            this.Tail.Next = addedNode;
            this.Tail = addedNode;
        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        T removedValue;

        if (this.Count == 0)
        {
            removedValue = default(T);
            handleEmptyList(null, "REMOVE");
        }
        else if (this.Count == 1)
        {
            removedValue = this.Head.Value;
            this.Head = null;
            this.Tail = null;
        }
        else
        {
            removedValue = this.Head.Value;
            this.Head = this.Head.Next;
        }
        this.Count--;

        return removedValue;
    }

    public T RemoveLast()
    {
        T removedValue;

        if (this.Count == 0)
        {
            removedValue = default(T);
            handleEmptyList(null, "REMOVE");
        }
        else if (this.Count == 1)
        {
            removedValue = this.Tail.Value;
            this.Head = null;
            this.Tail = null;
        }
        else
        {
            removedValue = this.Tail.Value;
            Node currentNodeElement = this.Head;

            while (currentNodeElement.Next != this.Tail)
            {
                currentNodeElement = currentNodeElement.Next;
            }

            this.Tail = currentNodeElement;
            currentNodeElement.Next = null;
        }

        this.Count--;
        return removedValue;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node currentNodeElement = this.Head;
        while (currentNodeElement != null)
        {
            yield return currentNodeElement.Value;
            currentNodeElement = currentNodeElement.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private class Node
    {
        public T Value { get; set; }
        public Node Next { get; set; }

        public Node(T value)
        {
            this.Value = value;
        }
    }

    private void handleEmptyList(Node addedNode, string action)
    {
        switch (action)
        {
            case "ADD":
                {
                    this.Head = addedNode;
                    this.Tail = addedNode;
                    break;
                }
            case "REMOVE":
                {
                    throw new InvalidOperationException();
                }
        }
   
    }
}
