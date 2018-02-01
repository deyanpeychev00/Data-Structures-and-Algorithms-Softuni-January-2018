using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDS_Exercise
{
    class Tree<T>
    {
        public T Value { get; set; }
        public List<Tree<T>> Children { get; set; }
        public Tree<T> Parent { get; set; }

        public Tree( T value, params Tree<T>[] children)
        {
            this.Value = value;
            this.Children = new List<Tree<T>>(children);
        }

        public void PrintTree(int indent = 0)
        {
            Console.WriteLine($"{new string(' ', indent)}{this.Value}");
            foreach (var child in this.Children)
            {
                child.PrintTree(indent + 2);
            }
        }
    }

    

    class TDS_Solution
    {
        static Dictionary<int, Tree<int>> tree = new Dictionary<int, Tree<int>>();

        static void Main()
        {
            int nodesNum = int.Parse(Console.ReadLine());
            InitializeTreeDictionary(nodesNum);

            // FindRoot(tree);
            // Print(tree);
            // FindLeadNodes(tree);
            // FindMiddleNodes(tree);
            // AllPathsWithGivenSum(tree);
            // FindLongestPath(tree);
            // FindDeepestNode(tree);
            SubtreesWithGivenSum(tree);
        }

        private static void SubtreesWithGivenSum(Dictionary<int, Tree<int>> tree)
        {
            int givenSum = int.Parse(Console.ReadLine());
            var root = GetRoot(tree);
            Console.WriteLine($"Subtrees of sum {givenSum}:");
            SubtreeDFS(root, givenSum);
        }

        private static int SubtreeDFS(Tree<int> node, int sum)
        {
            int currentSum = node.Value;
            int subtreeSum = 0;

            foreach (var child in node.Children)
            {
                currentSum += child.Value;
                subtreeSum += SubtreeDFS(child, sum);
                
            }

            if(subtreeSum == sum || currentSum == sum)
            {
                List<int> subtree = new List<int>();
                ReturnSubtree(node, subtree);
                Console.WriteLine(string.Join(" ", subtree));
            }

            return currentSum;
        }

        private static void ReturnSubtree(Tree<int> node, List<int> subtree)
        {
            subtree.Add(node.Value);
            foreach (var child in node.Children)
            {
                ReturnSubtree(child, subtree);
            }
        }

        private static void FindDeepestNode(Dictionary<int, Tree<int>> tree)
        {
            var longestDepthElement = GetDeepestNodeWithPathLenght(tree);
            Console.WriteLine($"Deepest node: {longestDepthElement.Key.Value}");
        }

        private static KeyValuePair<Tree<int>, int> GetDeepestNodeWithPathLenght(Dictionary<int, Tree<int>> tree)
        {
            List<Tree<int>> leafs = tree.Values.Where(x => x.Children.Count == 0).ToList();
            Dictionary<Tree<int>, int> leafToRootDepth = new Dictionary<Tree<int>, int>();


            foreach (var leaf in leafs)
            {
                if (!leafToRootDepth.ContainsKey(leaf))
                {
                    leafToRootDepth.Add(leaf, 0);
                }
                int pathDepth = TraverseLeafToRoot(leaf);
                leafToRootDepth[leaf] = pathDepth;
            }

            var longestDepthElement = leafToRootDepth.OrderByDescending(x => x.Value).First();

            return longestDepthElement;
        }

        private static void FindLongestPath(Dictionary<int, Tree<int>> tree)
        {

            var longestDepthElement = GetDeepestNodeWithPathLenght(tree);
            Console.Write("Longest path: ");
            PrintPath(longestDepthElement.Key);
        }

        private static int TraverseLeafToRoot(Tree<int> leaf)
        {
            var pathDepth = 0;
            while(leaf != null)
            {
                leaf = leaf.Parent;
                pathDepth++;
            }
            return pathDepth;
        }

        private static void AllPathsWithGivenSum(Dictionary<int, Tree<int>> tree)
        {
            int sum = int.Parse(Console.ReadLine());
            List<Tree<int>> leafs = tree.Values.Where(x => x.Children.Count == 0).ToList();
            var root = GetRoot(tree);
            Console.WriteLine($"Paths of sum {sum}:");
            DFS(root, sum);
        }

        private static void DFS(Tree<int> node, int targetSum, int sum = 0)
        {
            sum += node.Value;

            if (sum == targetSum)
            {
                PrintPath(node);
            }

            foreach (var child in node.Children)
            {
                DFS(child, targetSum, sum);
            }
        }

        private static void PrintPath(Tree<int> node)
        {
            Stack<int> path = new Stack<int>();
            Tree<int> start = node;
            while(start != null)
            {
                path.Push(start.Value);
                start = start.Parent;
            }

            Console.WriteLine(String.Join(" ", path));
        }

        private static void FindMiddleNodes(Dictionary<int, Tree<int>> tree)
        {
            var middleNodes = new List<int>();
            foreach(var kvp in tree)
            {
                if(kvp.Value.Parent != null && kvp.Value.Children.Count > 0)
                {
                    middleNodes.Add(kvp.Value.Value);
                }
            }

            Console.WriteLine($"Middle nodes: {String.Join(" ", middleNodes.OrderBy(x => x))}");
        }

        private static void FindLeadNodes(Dictionary<int, Tree<int>> tree)
        {
            var leafNodes = new List<int>();
            foreach (var kvp in tree)
            {
                if(kvp.Value.Children.Count == 0)
                {
                    leafNodes.Add(kvp.Value.Value);
                }
            }
            Console.WriteLine($"Leaf nodes: {String.Join(" ", leafNodes.OrderBy(x => x))}");
        }

        private static void InitializeTreeDictionary(int nodesNum)
        {
            for (int i = 0; i < nodesNum - 1; i++)
            {
                int[] nodes = Console.ReadLine().Split().Select(int.Parse).ToArray();

                int parent = nodes[0];
                int child = nodes[1];

                if (!tree.ContainsKey(parent))
                {
                    tree.Add(parent, new Tree<int>(parent));
                }
                if (!tree.ContainsKey(child))
                {
                    tree.Add(child, new Tree<int>(child));
                }

                Tree<int> parentNode = tree[parent];
                Tree<int> childNode = tree[child];

                parentNode.Children.Add(childNode);
                childNode.Parent = parentNode;
            }
        }

        private static Tree<int> GetRoot(Dictionary<int, Tree<int>> tree)
        {
            foreach (var key in tree.Keys)
            {
                if (tree[key].Parent == null)
                {
                    return tree[key];
                }
            }
            return null;
        }

        private static void FindRoot(Dictionary<int, Tree<int>> tree)
        {
            foreach(var key in tree.Keys)
            {
           
                if(tree[key].Parent == null)
                {
                    Console.WriteLine($"Root node: {key}");
                    return;
                }
            }
        }

        private static void Print(Dictionary<int, Tree<int>> tree)
        {
            var root = GetRoot(tree);
            root.PrintTree();
        }
    }
}
