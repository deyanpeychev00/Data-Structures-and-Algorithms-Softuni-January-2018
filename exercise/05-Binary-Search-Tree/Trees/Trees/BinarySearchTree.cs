using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{

    private Node Root;

    private class Node
    {
        public T Value { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(T value)
        {
            this.Value = value;
            this.Left = this.Right = null;
        }

    }

    public BinarySearchTree()
    {
        this.Root = null;
    }

    private BinarySearchTree(Node node)
    {
        this.Duplicate(node);
    }

    private void Duplicate(Node node)
    {
        if(node == null)
        {
            return;
        }
        else
        {
            this.Insert(node.Value);
            this.Duplicate(node.Left);
            this.Duplicate(node.Right);
        }
    }
   
    public void Insert(T value)
    {
        if(this.Root == null)
        {
            this.Root = new Node(value);
        }
        else
        {
            Node current = this.Root;
            Node parent = null;

            while(current != null)
            {
                if(current.Value.CompareTo(value) > 0)
                {
                    parent = current;
                    current = current.Left;
                }
                else if (current.Value.CompareTo(value) < 0)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    return;
                }
            }

            if(parent.Value.CompareTo(value) > 0)
            {
                parent.Left = new Node(value);
            }
            else if (parent.Value.CompareTo(value) < 0)
            {
                parent.Right = new Node(value);
            }
            else
            {
                return;
            }
        }
    }

    public bool Contains(T value)
    {
        if (this.Root == null)
        {
            return false;
        }
        else
        {
            Node current = this.Root;

            while (current != null)
            {
                if(current.Value.CompareTo(value) == 0)
                {
                    return true;
                }
                else
                {
                    if (current.Value.CompareTo(value) > 0)
                    {
                        current = current.Left;
                    }
                    else if (current.Value.CompareTo(value) < 0)
                    {
                        current = current.Right;
                    }
                }
            }

            return false;
        }
    }

    public void DeleteMin()
    {
        if(this.Root == null)
        {
            return;
        }
        if(this.Root != null && (this.Root.Left == null && this.Root.Left == null))
        {
            this.Root = null;
        }
        else
        {
            Node current = this.Root;
            Node parent = null;
            while(current != null)
            {
                if(current.Left != null)
                {
                    parent = current;
                    current = current.Left;
                }
                else if(current.Right != null)
                {
                    parent = current;
                    current = current.Right;
                }
                else
                {
                    break;
                }
             
            }
            if(parent.Left != null)
            {
                parent.Left = null;
            }else if(parent.Right != null)
            {
                parent.Right = null;
            }
            else
            {
                parent = null;
            }
         
        }
    }

    public BinarySearchTree<T> Search(T value)
    {
        if (this.Root == null)
        {
            return null;
        }
        else
        {
            Node current = this.Root;

            while (current != null)
            {
                if (current.Value.CompareTo(value) == 0)
                {
                    return new BinarySearchTree<T>(current);
                }
                else
                {
                    if (current.Value.CompareTo(value) > 0)
                    {
                        current = current.Left;
                    }
                    else if (current.Value.CompareTo(value) < 0)
                    {
                        current = current.Right;
                    }
                }
            }

            return null;
        }
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        Queue<T> output = new Queue<T>();

        this.Range(output, startRange, endRange, this.Root);

        return output;
    }

    private void Range(Queue<T> output, T startRange, T endRange, Node root)
    {
        if(root == null)
        {
            return;
        }
        else
        {
            int higherValueRange = endRange.CompareTo(root.Value);
            int lowerValueRange = startRange.CompareTo(root.Value);

            if(lowerValueRange < 0)
            {
                this.Range(output, startRange, endRange, root.Left);
            }
            if(lowerValueRange <= 0 && higherValueRange >= 0)
            {
                output.Enqueue(root.Value);
            }
            if(higherValueRange > 0)
            {
                this.Range(output, startRange, endRange, root.Right);
            }
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.Root, action); 
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if(node == null)
        {
            return;
        }
        if(node.Left != null)
        {
            this.EachInOrder(node.Left, action);
        }
        action(node.Value);
        if(node.Right != null)
        {
            this.EachInOrder(node.Right, action);
        }
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        
    }
}
