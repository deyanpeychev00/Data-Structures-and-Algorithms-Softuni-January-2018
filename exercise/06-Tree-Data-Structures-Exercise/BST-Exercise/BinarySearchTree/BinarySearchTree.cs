using System;
using System.Collections.Generic;

public class BinarySearchTree<T> : IBinarySearchTree<T> where T : IComparable
{
    private Node root;
    private int ElementsCount;
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

        if (!this.ElementsList.Contains(node))
        {
            this.ElementsList.Add(node);
        }
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
        if (!this.Contains(element))
        {
            this.ElementsCount++;
        }
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
            return;
        }

        Node current = this.root;
        Node parent = null;
        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            RemoveNodeFromList(this.root);
            this.root = this.root.Right;
        }
        else
        {
            RemoveNodeFromList(parent.Left);
            parent.Left = current.Right;
        }
        this.ElementsCount--;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> queue = new Queue<T>();

        this.Range(this.root, queue, startRange, endRange);

        return queue;
    }

    public void Delete(T element)
    {
        PreorderTraversal(this.root, element);
        this.ElementsCount--;
        this.RemoveNodeFromList(new Node(element));
    }

    private void PreorderTraversal(Node Root, T element)
    {
        if (Root == null)
        {
            return;
        }
        Node currentLeft;
        Node currentRight;
        if (Root != null)
        {
            if (Root.Value.CompareTo(element) == 0)
            {

                currentLeft = Root.Left;
                currentRight = Root.Right;
                Root = null;
                TraverseSubTree(currentLeft);
                TraverseSubTree(currentRight);
                return;
            }
        }
        PreorderTraversal(Root.Left, element);
        PreorderTraversal(Root.Right, element);
    }

    private void TraverseSubTree(Node Root)
    {

        if (Root == null)
        {
            return;
        }
        if (Root != null)
        {
            this.Insert(Root.Value);
        }
        TraverseSubTree(Root.Left);
        TraverseSubTree(Root.Right);
    }

    public void DeleteMax()
    {
        if (this.root == null)
        {
            return;
        }

        Node current = this.root;
        Node parent = null;
        while (current.Right != null)
        {
            parent = current;
            current = current.Right;
        }

        if (parent == null)
        {
            RemoveNodeFromList(this.root);
            this.root = this.root.Left;
        }
        else
        {
            RemoveNodeFromList(parent.Right);
            parent.Right = current.Left;
        }
        this.ElementsCount--;
    }

    public int Count()
    {
        return this.ElementsCount;
    }

    public int Rank(T element)
    {
        for (int i = 0; i < this.ElementsList.Count; i++)
        {
            if (this.ElementsList[i].Value.CompareTo(element) == 0)
            {
                return i;
            }
        }

        return 0;
    }

    public T Select(int rank)
    {

        var sortedList = this.TransferListValues();
        if (rank < 0 || rank >= sortedList.Count)
        {
            throw new InvalidOperationException();
        }
        return sortedList[rank];
    }

    public T Ceiling(T element)
    {

        var sortedList = this.TransferListValues();
        var elementIndex = sortedList.IndexOf(element);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException();
        }

        if (elementIndex == sortedList.Count - 1)
        {
            return sortedList[sortedList.Count - 1];
        }
        else
        {
            return sortedList[elementIndex + 1];
        }
    }

    public T Floor(T element)
    {

        var sortedList = this.TransferListValues();
        var elementIndex = sortedList.IndexOf(element);

        if (elementIndex == -1)
        {
            throw new InvalidOperationException();
        }

        if (elementIndex == 0)
        {
            return sortedList[0];
        }
        else
        {
            return sortedList[elementIndex - 1];
        }
    }

    private void RemoveNodeFromList(Node node)
    {
        for (int i = 0; i < this.ElementsList.Count; i++)
        {
            if (this.ElementsList[i].Value.CompareTo(node.Value) == 0)
            {
                this.ElementsList.RemoveAt(i);
                break;
            }
        }
    }

    public void DisplayElementsList()
    {
        var output = new List<T>();
        foreach (var node in this.ElementsList)
        {
            output.Add(node.Value);
        }
        output.Sort();
        Console.WriteLine(String.Join(" ", output));
    }

    private List<T> TransferListValues()
    {
        var sortedList = new List<T>();
        foreach (var element in this.ElementsList)
        {
            sortedList.Add(element.Value);
        }
        sortedList.Sort();
        return sortedList;
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();

        bst.Insert(1);
        bst.Insert(3);
        bst.Insert(4);
        bst.Insert(5);
        bst.Insert(8);
        bst.Insert(9);
        bst.Insert(10);
        bst.Insert(37);
        bst.Insert(39);
        bst.Insert(45);
        bst.DisplayElementsList();

        // bst.DeleteMin();
        // Console.WriteLine("Delete MIN..");
        // bst.DisplayElementsList();
        // Console.WriteLine("Count: " + bst.Count());
        // Console.WriteLine();
        bst.DeleteMax();
        Console.WriteLine("Delete MAX..");
        // bst.DisplayElementsList();
        // Console.WriteLine("Count: " + bst.Count());
        // Console.WriteLine();

        Console.WriteLine("Adding already existing element..");
        bst.Insert(37); // element already exists, count doesn't increment
        // bst.EachInOrder(Console.WriteLine);
        Console.WriteLine("Count: " + bst.Count());
        Console.WriteLine();

        Console.WriteLine("Rank(8) -> Should return 4    | Returns: " + bst.Rank(8));
        Console.WriteLine();
        Console.WriteLine("Select(4) -> Should return 8  | Returns: " + bst.Select(4));
        Console.WriteLine();
        Console.WriteLine("Ceiling(4) -> Should return 5 | Returns: " + bst.Ceiling(4));
        Console.WriteLine();
        Console.WriteLine("Floor(5) -> Should return 4   | Returns: " + bst.Floor(5));
        Console.WriteLine();
        Console.WriteLine("Count: " + bst.Count());
        Console.WriteLine();
        bst.Delete(37);
        bst.DisplayElementsList();
        Console.WriteLine("Count: " + bst.Count());
        Console.WriteLine();
        bst.EachInOrder(Console.WriteLine);
    }
}