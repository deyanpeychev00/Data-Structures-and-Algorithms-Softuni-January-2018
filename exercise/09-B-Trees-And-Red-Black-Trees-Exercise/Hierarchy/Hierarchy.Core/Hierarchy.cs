namespace Hierarchy.Core
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private Node Root { get; set; }
        private Dictionary<T, Node> NodesByValue { get; set; }

        private class Node
        {
            public T Value { get; set; }
            public Node Parent { get; set; }
            public List<Node> Children { get; set; }
            public bool IsRoot { get; }

            public Node(T value, Node parent = null, bool isRoot = false)
            {
                this.Value = value;
                this.Children = new List<Node>();
                this.Parent = parent;
                this.IsRoot = isRoot;
            }
        }

        public Hierarchy(T root)
        {
            this.Root = new Node(root, null, true);
            this.NodesByValue = new Dictionary<T, Node>
            {
                { root, this.Root }
            };
        }

        public int Count
        {
            get
            {
                return this.NodesByValue.Count;
            }
        }

        public void Add(T parent, T child)
        {
            // Validate parent existance in heriarchy values dictionary
            if (!this.NodesByValue.ContainsKey(parent) || this.NodesByValue.ContainsKey(child))
            {
                throw new ArgumentException();
            }

            Node parentNode = this.NodesByValue[parent];
            Node childNode = new Node(child, parentNode);

            // Add child to parentNode children list
            parentNode.Children.Add(childNode);


            // Add child to hierarchy values dictionary
            if (!this.NodesByValue.ContainsKey(child))
            {
                this.NodesByValue.Add(child, childNode);
            }

        }

        public void Remove(T element)
        {
            if (!this.NodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }
            if (this.NodesByValue[element].IsRoot)
            {
                throw new InvalidOperationException();
            }

            Node current = this.NodesByValue[element];

            /* 
             * attach children to the current element's parent
             * and change their parent to current element's parent
            */

            foreach (var currentChild in current.Children)
            {
                currentChild.Parent = current.Parent;
                current.Parent.Children.Add(currentChild);
            }

            // remove current parent from children and from hierarchy nodes values dict
            current.Parent.Children.Remove(current);
            this.NodesByValue.Remove(element);

        }

        public IEnumerable<T> GetChildren(T item)
        {
            if (!this.NodesByValue.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            return this.NodesByValue[item].Children.Select(x => x.Value);
        }

        public T GetParent(T item)
        {
            if (!this.NodesByValue.ContainsKey(item))
            {
                throw new ArgumentException();
            }

            return this.NodesByValue[item].Parent != null ? this.NodesByValue[item].Parent.Value : default(T);
        }

        public bool Contains(T value)
        {
            return this.NodesByValue.ContainsKey(value);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            return this.NodesByValue.Keys.ToList().Intersect(other.NodesByValue.Keys.ToList());
        }

        public IEnumerator<T> GetEnumerator()
        {
            Queue<Node> values = new Queue<Node>();
            Node current = this.Root;
            values.Enqueue(current);

            while (values.Count > 0)
            {
                current = values.Dequeue();
                yield return current.Value;

                foreach (var child in current.Children)
                {
                    values.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}