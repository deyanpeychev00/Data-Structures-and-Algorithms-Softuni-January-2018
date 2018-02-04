using System;

public static class Heap<T> where T : IComparable<T>
{
    public static void Sort(T[] arr)
    {
        MakeHeap(arr);
        HeapSort(arr);
    }

    private static void HeapSort(T[] arr)
    {
        for (int i = arr.Length - 1; i > 0; i--)
        {
            Swap(arr, 0, i);
            HeapifyDown(arr, 0, i);

        }
    }

    private static void MakeHeap(T[] arr)
    {
        for( int i = arr.Length / 2; i >= 0; i--)
        {
             HeapifyDown(arr, i, arr.Length);
        }
    }

    private static void HeapifyDown(T[] array, int rootIndex, int arrLength)
    {

        while (rootIndex < arrLength / 2)
        {
            int childIndex = (rootIndex * 2) + 1;
            if (childIndex + 1 < arrLength && ChildIsGreater(array, childIndex, childIndex + 1))
            {
                childIndex += 1;
            }

            int compare = array[rootIndex].CompareTo(array[childIndex]);

            if (compare < 0)
            {
                Swap(array, rootIndex, childIndex);
            }
            rootIndex = childIndex;
        }

    }

    private static void Swap(T[] array, int parentIndex, int childIndex)
    {
        var temp = array[parentIndex];
        array[parentIndex] = array[childIndex];
        array[childIndex] = temp;
    }

    private static bool ChildIsGreater(T[] array, int leftChildIndex, int rightChildIndex)
    {
        return array[leftChildIndex].CompareTo(array[rightChildIndex]) < 0;
    }
}
