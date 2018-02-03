using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private Node root;
    private List<Node> ElementsList;

    private Node FindElement(T element)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(element) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(element) < 0)
            {
                current = current.Right;
            }
            else
            {
                break;
            }
        }

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null)
        {
            return;
        }

        this.Insert(node.Value);
        this.PreOrderCopy(node.Left);
        this.PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
        {
            node = new Node(element);
        }
        else if (element.CompareTo(node.Value) < 0)
        {
            node.Left = this.Insert(element, node.Left);
        }
        else if (element.CompareTo(node.Value) > 0)
        {
            node.Right = this.Insert(element, node.Right);
        }

        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null)
        {
            return;
        }

        int nodeInLowerRange = startRange.CompareTo(node.Value);
        int nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0)
        {
            this.Range(node.Left, queue, startRange, endRange);
        }
        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0)
        {
            queue.Enqueue(node.Value);
        }
        if (nodeInHigherRange > 0)
        {
            this.Range(node.Right, queue, startRange, endRange);
        }
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }

    private BinarySearchTree(Node node)
    {
        this.ElementsList = new List<Node>();
        this.PreOrderCopy(node);
    }

    public BinarySearchTree()
    {
           this.ElementsList = new List<Node>();
    }

    public void Insert(T element)
    {
        this.root = this.Insert(element, this.root);
    }

    public bool Contains(T element)
    {
        Node current = this.FindElement(element);

        return current != null;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    public BinarySearchTree<T> Search(T element)
    {
        Node current = this.FindElement(element);

        return new BinarySearchTree<T>(current);
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }
        this.root = this.DeleteMin(this.root);
    }

    private Node DeleteMin(Node node)
    {
        if (node.Left == null)
        {
            return node.Right;
        }

        node.Left = this.DeleteMin(node.Left);
        node.Count = 1 + this.Count(node.Right) + this.Count(node.Left);

        return node;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            throw new InvalidOperationException();
        }

        this.root = this.DeleteMax(this.root);
    }

    private Node DeleteMax(Node node)
    {
        if (node.Right == null)
        {
            return node.Left;
        }

        node.Right = this.DeleteMax(node.Right);
        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);

        return node;
    }

    public int Count()
    {
        return this.Count(this.root);
    }

    private int Count(Node node)
    {
        if (node == null)
        {
            return 0;
        }
        return node.Count;
    }

    public int Rank(T element)
    {
        return this.Rank(this.root, element);
    }

    private int Rank(Node node, T element)
    {
        if (node == null)
        {
            return 0;
        }

        int compare = node.Value.CompareTo(element);

        if (compare > 0)
        {
            return this.Rank(node.Left, element);
        }
        if (compare < 0)
        {
            return 1 + this.Count(node.Left) + this.Rank(node.Right, element);
        }

        return this.Count(node.Left);
    }

    public T Select(int rank)
    {
        if(rank < 0 || rank > this.Count())
        {
            throw new InvalidOperationException();
        }
        CopyTreeToList(this.root);

        List<T> TreeValues = GetTreeValues();
        // Console.WriteLine(String.Join(" ", TreeValues));

        return TreeValues[rank];
    }

    private List<T> GetTreeValues()
    {
        CopyTreeToList(this.root);
        List<T> values = new List<T>();
        foreach (var node in this.ElementsList)
        {
            values.Add(node.Value);
        }
        values.Sort();
        return values;
    }

    private void CopyTreeToList(Node root)
    {
        if(root == null)
        {
            return;
        }
        CopyTreeToList(root.Left);
        CopyTreeToList(root.Right);

        if (!this.ElementsList.Contains(root))
        {
            this.ElementsList.Add(root);
        }

    }

    public T Ceiling(T element)
    {
        CopyTreeToList(this.root);
        List<T> TreeValues = GetTreeValues();
        int elementIndex = TreeValues.IndexOf(element);

        if ( elementIndex == -1 || elementIndex+1 >= TreeValues.Count)
        {
            throw new InvalidOperationException();
        }

        return TreeValues[elementIndex + 1];
    }

    public T Floor(T element)
    {
        CopyTreeToList(this.root);
        List<T> TreeValues = GetTreeValues();
        int elementIndex = TreeValues.IndexOf(element);

        if (elementIndex == -1 || elementIndex - 1 < 0)
        {
            throw new InvalidOperationException();
        }

        return TreeValues[elementIndex -1];
    }

    public void Delete(T element)
    {
        if (this.Count(this.root) == 0 || !this.Contains(element))
        {
            throw new InvalidOperationException();
        }
        this.root = this.Delete(this.root, element);
    }

    private Node Delete(Node node,T element)
    {
        if (node == null)
        {
            return null;
        }
        int compare = node.Value.CompareTo(element);

        // Traverse tree until the targetted element is found
        if (compare > 0)
        {
            node.Left = this.Delete(node.Left, element);
        }
        else if(compare < 0)
        {
            node.Right = this.Delete(node.Right, element);
        }
        // Remove targetted element
        else
        {
            if(node.Left == null)
            {
                return node.Right;
            }
            if(node.Right == null)
            {
                return node.Left;
            }
            Node leftMost = this.FindSubtreeLeftmost(node.Right);
            node.Value = leftMost.Value;
            node.Right = this.Delete(node.Right, leftMost.Value);
        }

        node.Count = 1 + this.Count(node.Left) + this.Count(node.Right);
        return node;
    }

    private Node FindSubtreeLeftmost(Node node)
    {
        Node current = node;
        while(current.Left != null)
        {
            current = current.Left;
        }
       

        return current;
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }
        public int Count { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Insert(10);
        bst.Insert(5);
        bst.Insert(3);
        bst.Insert(1);
        bst.Insert(4);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);
        bst.Insert(6);

        Console.WriteLine(bst.Rank(8));
        Console.WriteLine();
        bst.DeleteMax();
        bst.EachInOrder(Console.WriteLine);
        Console.WriteLine();
        Console.WriteLine(bst.Select(4));
        Console.WriteLine();
        Console.WriteLine(bst.Ceiling(37));
        Console.WriteLine();
        Console.WriteLine(bst.Floor(3));
        Console.WriteLine();
        List<int> values = new List<int>();
        bst.EachInOrder(values.Add);
        Console.WriteLine(string.Join(" ", values));
        bst.Delete(5);

        List<int> values2 = new List<int>();
        bst.EachInOrder(values2.Add);
        Console.WriteLine(string.Join(" ", values2));


    }
}