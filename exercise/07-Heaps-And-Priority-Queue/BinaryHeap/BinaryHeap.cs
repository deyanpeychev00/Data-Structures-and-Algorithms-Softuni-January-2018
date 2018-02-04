using System;
using System.Collections.Generic;

public class BinaryHeap<T> where T : IComparable<T>
{
    private List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count
    {
        get
        {
            return this.heap.Count;
        }
    }

    public void Insert(T item)
    {
        heap.Add(item);
        this.HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int childIndex)
    {
        int parentIndex = (childIndex - 1) / 2;
        int compare = this.heap[parentIndex].CompareTo(this.heap[childIndex]);

        if (compare < 0)
        {
            this.Swap(parentIndex, childIndex);
            this.HeapifyUp(parentIndex);
        }

    }
    
    private void Swap(int parentIndex, int childIndex)
    {
        var temp = this.heap[parentIndex];
        this.heap[parentIndex] = this.heap[childIndex];
        this.heap[childIndex] = temp;
    }

    public T Peek()
    {
        if (this.heap.Count == 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        if (this.heap.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var maxHeapElement = this.heap[0];
        this.Swap(0, this.heap.Count - 1);
        this.heap.RemoveAt(this.heap.Count - 1);
        this.HeapifyDown(0);

        return maxHeapElement;
    }

    private void HeapifyDown(int rootIndex)
    {

        while(rootIndex < this.Count / 2)
        {
            int childIndex = (rootIndex * 2) + 1;
            if (childIndex + 1 < this.Count && ChildIsGreater(childIndex, childIndex + 1))
            {
                childIndex += 1;
            }

            int compare = this.heap[rootIndex].CompareTo(this.heap[childIndex]);

            if (compare < 0)
            {
                this.Swap(rootIndex, childIndex);
            }
            rootIndex = childIndex;
        }

    }

    private bool ChildIsGreater(int leftChildIndex, int rightChildIndex)
    {
        return this.heap[leftChildIndex].CompareTo(this.heap[rightChildIndex]) < 0;
    }
}
