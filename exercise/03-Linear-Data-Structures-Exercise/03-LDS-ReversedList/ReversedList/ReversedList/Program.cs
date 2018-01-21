using System;
using System.Collections;
using System.Collections.Generic;

public class ReversedList<T> : IEnumerable<T>
{
    private T[] arr;

    public int Count { get; private set; }
    public int Capacity { get; private set; }

    public ReversedList(int capacity = 2)
    {
        this.arr = new T[capacity];
        this.Capacity = capacity;
        this.Count = 0;
    }

    public T this[int index]
    {

        get
        {
            CheckIndexRange(index);
            return this.arr[this.Count - 1 - index];
        }

        set
        {
            CheckIndexRange(index);
            this.arr[this.Count - 1 - index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count >= this.Capacity)
        {
            Array.Resize(ref this.arr, this.Capacity * 2);
            this.Capacity *= 2;
        }
        this.arr[this.Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        
        T item = this[index];
        this[index] = default(T);
        this.ShiftLeft(this.Count - 1 - index);
        this.Count--;
        return item;
    }

    private void CheckIndexRange(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count - 1; i++)
        {
            this.arr[i] = this.arr[i + 1];
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < this.Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

class Program
{
    static void Main(string[] args)
    {
    }
}
