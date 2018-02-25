using System;
using System.Collections.Generic;

public class IntervalTree
{
    private class Node
    {
        internal Interval Interval;
        internal double MaxEndpoint;
        internal Node RightInterval;
        internal Node LeftInterval;

        public Node(Interval interval)
        {
            this.Interval = interval;
            this.MaxEndpoint = interval.Hi;
        }
    }

    private Node root;

    public void Insert(double lo, double hi)
    {
        this.root = this.Insert(this.root, lo, hi);
    }

    public void EachInOrder(Action<Interval> action)
    {
        EachInOrder(this.root, action);
    }

    public Interval SearchAny(double lo, double hi)
    {
        Node currentInterval = this.root;
        while(currentInterval != null && !currentInterval.Interval.Intersects(lo, hi))
        {
            if(currentInterval.LeftInterval != null && currentInterval.LeftInterval.MaxEndpoint > lo)
            {
                currentInterval = currentInterval.LeftInterval;
            }
            else
            {
                currentInterval = currentInterval.RightInterval;
            }
        }

        return currentInterval?.Interval;
    }

    public IEnumerable<Interval> SearchAll(double lo, double hi)
    {
        List<Interval> resultList = new List<Interval>();
        this.SearchAll(this.root, resultList, new Interval(lo, hi));
        return resultList;
    }

    private void SearchAll(Node node, List<Interval> intervalsList, Interval interval)
    {
        if(node == null)
        {
            return;
        }

        if (node.LeftInterval != null && node.LeftInterval.MaxEndpoint > interval.Lo)
        {
            this.SearchAll(node.LeftInterval, intervalsList, interval);
        }
        if (node.Interval.Intersects(interval.Lo, interval.Hi))
        {
            intervalsList.Add(node.Interval);
        }
        if(node.RightInterval != null && node.RightInterval.Interval.Lo < interval.Hi)
        {
            this.SearchAll(node.RightInterval, intervalsList, interval);
        }
    }

    private void EachInOrder(Node node, Action<Interval> action)
    {
        if (node == null)
        {
            return;
        }

        EachInOrder(node.LeftInterval, action);
        action(node.Interval);
        EachInOrder(node.RightInterval, action);
    }

    private Node Insert(Node node, double lo, double hi)
    {
        if (node == null)
        {
            return new Node(new Interval(lo, hi));
        }

        int cmp = lo.CompareTo(node.Interval.Lo);
        if (cmp < 0)
        {
            node.LeftInterval = Insert(node.LeftInterval, lo, hi);
        }
        else if (cmp > 0)
        {
            node.RightInterval = Insert(node.RightInterval, lo, hi);
        }
        UpdateMaxEndpoint(node);
        return node;
    }

    private void UpdateMaxEndpoint(Node node)
    {
        var maxChild = GetMax(node.LeftInterval, node.RightInterval);
        node.MaxEndpoint = GetMax(node, maxChild).MaxEndpoint;
    }

    private Node GetMax(Node left, Node right)
    {
        if(left == null)
        {
            return right;
        }
        if(right == null)
        {
            return left;
        }

        return left.MaxEndpoint > right.MaxEndpoint ? left : right;
    }
}
