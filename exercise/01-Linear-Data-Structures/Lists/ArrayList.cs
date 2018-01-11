using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayList<T> : IEnumerable<T>
{
    private T[] arr;

    public int Count { get; set; }
    public int Capacity { get; set; }

    public ArrayList(int capacity = 2)
    {
        this.arr = new T[capacity];
        this.Capacity = capacity;
    }

    public T this[int index]
    {
        get
        {
            CheckIndexRange(index);
            return this.arr[index];
        }

        set
        {
            CheckIndexRange(index);
            this.arr[index] = value;
        }
    }

    

    public void Add(T item)
    {
        if(this.Count + 1 > this.Capacity)
        {
            this.Grow();
        }
        this.arr[this.Count] = item;
        this.Count++;
    }

    public T RemoveAt(int index)
    {
        T item = this[index];
        this[index] = default(T);
        this.ShiftLeft(index);
        if(this.Count - 1 < this.Capacity / 3)
        {
            this.Shrink(index);
        }
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

    private void Grow()
    {
        T[] newArr = new T[this.Capacity * 2];
        this.Capacity *= 2;
        Array.Copy(this.arr, newArr, this.Count);

        this.arr = newArr;
    }

    private void ShiftLeft(int index)
    {
        for (int i = index; i < this.Count - 1; i++)
        {
            this.arr[i] = this.arr[i + 1];
        }
    }

    private void Shrink(int index)
    {
        T[] shrinkedArray = new T[this.Capacity / 2];
        this.Capacity /= 2;
        Array.Copy(this.arr, shrinkedArray, this.Count);
        this.arr = shrinkedArray;
    }

    public IEnumerator<T> GetEnumerator()
    {
        foreach (var item in this.arr)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
