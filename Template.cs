using System;
using System.IO;
using System.Collections.Generic;

namespace dayXX
{
    class Program
    {
        static int PartOne()
        {
            return 0;
        }

        static int PartTwo()
        {
            return 0;
        }

        static IEnumerable<String> Input(StreamReader input)
        {
            string line;
            while ((line = input.ReadLine()) != null)
                yield return line;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("result 1: " + PartOne());
            Console.WriteLine("result 2: " + PartTwo());
        }
    }
}

