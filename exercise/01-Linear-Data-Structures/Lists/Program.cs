using System;

public class Program
{
    public static void Main(string[] args)
    {
        var testList = new ArrayList<string>();
        testList.Add("foo");
        testList.Add("bar");

        for (int i = 0; i < testList.Count; i++)
        {
            Console.WriteLine($"testList[{i}]= {testList[i]}");
        }
    }
}
