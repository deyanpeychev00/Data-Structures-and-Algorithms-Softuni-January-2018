using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{

    private List<T> list;

    public FirstLastList()
    {
        this.list = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.list.Count;
        }
    }

    public void Add(T element)
    {
        this.list.Add(element);
    }

    public void Clear()
    {
        this.list.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        CheckRange(count);

        for (int i = 0; i < count; i++)
        {
            yield return this.list[i];
        }
    }

    public IEnumerable<T> Last(int count)
    {
        CheckRange(count);

        for (int i = this.list.Count-1 ; i >= this.list.Count - count; i--)
        {
            yield return this.list[i];
        }
    }

    public IEnumerable<T> Max(int count)
    {
        var newList = performInsertionSort(this.list.ToArray());

        Array.Sort(newList, (a,b) => a.CompareTo(b));

        Array.Reverse(newList);

        CheckRange(count);

        for (int i = 0; i < count; i++)
        {
            yield return newList[i];
        }
    }

    public IEnumerable<T> Min(int count)
    {
        this.list.Sort();
        CheckRange(count);

        for (int i = 0; i < count; i++)
        {
            yield return this.list[i];
        }
    }

    public int RemoveAll(T element)
    {
        int countRemoved = 0;
        for (int i = 0; i < this.list.Count; i++)
        {
            if(this.list[i].CompareTo(element) == 0)
            {
                this.list.RemoveAt(i);
                countRemoved++;
                i--;
            }
        }

        return countRemoved;
    }

    private void CheckRange(int count)
    {
        if (this.list.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    static T[] performInsertionSort(T[] inputarray)
    {
        for (int i = 0; i < inputarray.Length - 1; i++)
        {
            int j = i + 1;

            while (j > 0)
            {
                if (inputarray[j - 1].CompareTo(inputarray[j]) > 0)
                {
                    T temp = inputarray[j - 1];
                    inputarray[j - 1] = inputarray[j];
                    inputarray[j] = temp;

                }
                j--;
            }
        }
        return inputarray;
    }
}
