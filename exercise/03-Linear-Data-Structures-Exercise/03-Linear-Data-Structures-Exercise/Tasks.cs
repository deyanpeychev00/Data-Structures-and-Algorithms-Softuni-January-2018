using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Linear_Data_Structures_Exercise
{
    class Tasks
    {
        static void Main()
        {
            // SumAndAverage();
            // SortWords();
            // LongestSubsequence();
            // RemoveOddOccurences();
            // CountOfOccurences();
            // DistanceInLabyrinth();
        }

        public static void SumAndAverage()
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList<int>();
            Console.WriteLine($"Sum={input.Sum()}; Average={input.Average():f2}");
        }

        public static void SortWords()
        {
            var input = Console.ReadLine().Split(' ').ToList<string>().OrderBy(x => x);
            Console.WriteLine(String.Join(" ", input));
        }

        public static void LongestSubsequence()
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToArray();
            var subsequencesDict = new Dictionary<int, int>();

            for (int i = 0; i < input.Length - 1; i++)
            {
                if(input[i] == input[i + 1])
                {
                    if (!subsequencesDict.ContainsKey(input[i + 1]))
                    {
                        subsequencesDict.Add(input[i + 1], 2);
                    }
                    else
                    {
                        subsequencesDict[input[i + 1]]++;
                    }
                }
            }
            if(subsequencesDict.Count == 0)
            {
                Console.WriteLine(input[0]);
            }
            else
            {
                var firstKey = 0;
                foreach (var kvp in subsequencesDict.OrderByDescending(x => x.Value))
                {
                    firstKey = kvp.Key;
                    break;
                }
                var number = subsequencesDict[firstKey];

                var subsequence = new List<int>();
                for (int i = 0; i < number; i++)
                {
                    subsequence.Add(firstKey);
                }

                Console.WriteLine(String.Join(" ", subsequence));
            }

        }

        public static void RemoveOddOccurences()
            {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList<int>();
            var numbersToRemove = new List<int>();

            for (int i = 0; i < input.Count; i++)
            {
                var currentNumber = input[i];
                var currentNumCounter = 0;
                for (int j = 0; j < input.Count; j++)
                {
                    if(input[j] == currentNumber)
                    {
                        currentNumCounter++;
                    }
                }
                if(currentNumCounter % 2 != 0)
                {
                    if (!numbersToRemove.Contains(currentNumber))
                    {
                        numbersToRemove.Add(currentNumber);
                    }
                   
                }
            }
            for (int i = 0; i < numbersToRemove.Count; i++)
            {
                input.RemoveAll(num => num == numbersToRemove[i]);
            }

            Console.WriteLine(String.Join(" ", input));
        }

        public static void CountOfOccurences()
        {
            var input = Console.ReadLine().Split().Select(int.Parse).ToList<int>();
            var occurences = new Dictionary<int, int>();

            foreach (var item in input)
            {
                if (!occurences.ContainsKey(item))
                {
                    occurences.Add(item, 1);
                }
                else
                {
                    occurences[item]++;
                }
            }
            foreach (var kvp in occurences.OrderBy(element => element.Key))
            {
                Console.WriteLine($"{kvp.Key} -> {kvp.Value} times");
            }
        }

        public static void DistanceInLabyrinth()
        {
            int[,] matrix;
            int matrixSize;

            matrixSize = int.Parse(Console.ReadLine());

            int starX = 0;
            int starY = 0;

            matrix = new int[matrixSize, matrixSize];

            // Fill matrix
            for (int i = 0; i < matrixSize; i++)
            {
                string input = Console.ReadLine();
                for (int j = 0; j < input.Length; j++)
                {
                    if (input[j].Equals('*'))
                    {
                        matrix[i, j] = -1;
                        starX = i;
                        starY = j;
                    }
                    else if (input[j].Equals('x'))
                    {
                        matrix[i, j] = -2;
                    }
                    else
                    {
                        matrix[i, j] = 0;
                    }
                }
            }

            // Main
            fill(starX, starY, 0);

            // Print Matrix
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    if (matrix[i, j] == -1)
                    {
                        Console.Write('*');
                    }
                    else if (matrix[i, j] == -2)
                    {
                        Console.Write('x');
                    }
                    else if (matrix[i, j] == 0)
                    {
                        Console.Write('u');
                    }
                    else
                    {
                        Console.Write(matrix[i, j]);
                    }
                }

                Console.WriteLine();
            }

            void fill(int x, int y, int d)
            {
                if (matrix[x, y] == 0 || d == 0 || matrix[x, y] > d)
                {
                    if (d > 0)
                    {
                        matrix[x, y] = d;
                    }

                    if (x < matrixSize - 1) fill(x + 1, y, d + 1);
                    if (x > 0) fill(x - 1, y, d + 1);
                    if (y < matrixSize - 1) fill(x, y + 1, d + 1);
                    if (y > 0) fill(x, y - 1, d + 1);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
