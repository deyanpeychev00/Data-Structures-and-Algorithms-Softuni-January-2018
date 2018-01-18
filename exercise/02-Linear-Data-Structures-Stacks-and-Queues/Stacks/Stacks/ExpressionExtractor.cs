using System;
using System.Collections.Generic;

namespace Stacks
{
    class ExpressionExtractor
    {
        static void Main()
        {
            // Sample input:  1 + (2 - (2 + 3) * 4 / (3 + 1)) * 5

            string expression = Console.ReadLine();
            var bracketStack = new Stack<int>();

            for (int i = 0; i < expression.Length; i++)
            {
                if(expression[i] == '(')
                {
                    bracketStack.Push(i);
                }
                else if(expression[i] == ')')
                {
                    var bracketFrom = bracketStack.Pop();

                    Console.WriteLine(expression.Substring(bracketFrom, (i-bracketFrom)+1));
                }
            }
        }
    }
}
