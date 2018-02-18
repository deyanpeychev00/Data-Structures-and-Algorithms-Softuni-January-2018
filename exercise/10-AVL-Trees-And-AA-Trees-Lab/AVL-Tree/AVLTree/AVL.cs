using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = this.Rebalance(node);

        return node;
    }

    private Node<T> Rebalance(Node<T> node)
    {
        node.Height = Math.Max(this.GetNodeHeight(node.Left), this.GetNodeHeight(node.Right)) + 1;

        // Balance AVL
        int balanceFactor = this.GetNodeHeight(node.Left) - this.GetNodeHeight(node.Right);

        // BF out of bounds case handling
        if (balanceFactor > 1)
        {
            int childBF = this.GetNodeHeight(node.Left.Left) - this.GetNodeHeight(node.Left.Right);
            if (childBF < 0)
            {
                node.Left = this.RotateLeft(node.Left);
            }
            node = this.RotateRight(node);
        }
        else if (balanceFactor < -1)
        {
            int childBF = this.GetNodeHeight(node.Right.Left) - this.GetNodeHeight(node.Right.Right);
            if (childBF > 0)
            {
                node.Right = this.RotateRight(node.Right);
            }
            node = this.RotateLeft(node);
        }

        return node;
    }

    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> newRoot = node.Left;
        node.Left = newRoot.Right;
        newRoot.Right = node;

        node.Height = Math.Max(this.GetNodeHeight(node.Left), this.GetNodeHeight(node.Right)) + 1;
        newRoot.Height = Math.Max(this.GetNodeHeight(newRoot.Left), this.GetNodeHeight(newRoot.Right)) + 1;

        return newRoot;
    }

    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> newRoot = node.Right;
        node.Right = newRoot.Left;
        newRoot.Left = node;

        node.Height = Math.Max(this.GetNodeHeight(node.Left), this.GetNodeHeight(node.Right)) + 1;
        newRoot.Height = Math.Max(this.GetNodeHeight(newRoot.Left), this.GetNodeHeight(newRoot.Right)) + 1;

        return newRoot;
    }

    private int GetNodeHeight(Node<T> node)
    {
        return node == null ? 0 : node.Height;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}
