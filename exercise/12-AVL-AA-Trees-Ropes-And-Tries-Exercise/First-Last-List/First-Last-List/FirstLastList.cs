using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T> where T : IComparable<T>
{

    private bool isSortedUp;
    private bool isSortedDown;
    private List<T> list;

    public FirstLastList()
    {
        this.list = new List<T>();
        isSortedDown = isSortedUp = false;
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
        CheckRange(count);
        if (!isSortedDown)
        {
            this.list.Sort((a, b) => {
                return b.CompareTo(a);
            });

            isSortedDown = true;
        }
      

        for (int i = 0; i < count; i++)
        {
            yield return this.list[i];
        }
    }

    public IEnumerable<T> Min(int count)
    {
        CheckRange(count);
        if (!isSortedUp)
        {
            this.list.Sort();
            this.isSortedUp = true;
        }

  
        for (int i = 0; i < count; i++)
        {
            yield return this.list[i];
        }
    }

    public int RemoveAll(T element)
    {
        return this.list.RemoveAll(x => x.CompareTo(element) == 0);
    }

    private void CheckRange(int count)
    {
        if (this.list.Count < count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }
}
