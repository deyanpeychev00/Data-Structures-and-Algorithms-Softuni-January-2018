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
            var input = Console.ReadLine().Split().Select(int.Parse).ToList<int>();
            var counterSubsequence = 0;
            var subsequenceNumber = input[0];

            for (int i = 0; i < input.Count; i++)
            {
                
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
    }
}
