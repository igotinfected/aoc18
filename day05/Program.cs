using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day05
{
    class Program
    {
        static int PartOne()
        {
            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr).GetEnumerator();
                var polymer = new Stack<int>();

                while (input.MoveNext())
                {
                    var unit = input.Current;

                    if (unit == '\n')
                        continue;

                    if (polymer.Count == 0)
                        polymer.Push(unit);
                    else if (Math.Abs(unit - polymer.Peek()) == 32)
                        polymer.Pop();
                    else
                        polymer.Push(unit);
                }

                return polymer.Count;
            }
        }

        static int PartTwo()
        {
            var lengths = new List<int>();
            for (var skipChar = 'A'; skipChar <= 'Z'; skipChar++)
            {
                using (var sr = new StreamReader("input.txt"))
                {
                    var input = Input(sr).GetEnumerator();
                    var polymer = new Stack<int>();

                    while (input.MoveNext())
                    {
                        var unit = input.Current;
                        
                        // skip if char is the one we want removed
                        if (unit == skipChar || unit == skipChar + 32 || unit == '\n')
                            continue;

                        if (polymer.Count == 0)
                            polymer.Push(unit);
                        else if (Math.Abs(unit - polymer.Peek()) == 32)
                            polymer.Pop();
                        else
                            polymer.Push(unit);
                    }

                    if (polymer.Count != 0)
                    {
                        lengths.Add(polymer.Count);
                        polymer.Clear();
                    }
                }
            }

            return lengths.Min(x => x);
        }

        static IEnumerable<int> Input(StreamReader input)
        {
            int character;
            while ((character = input.Read()) != -1)
                yield return character;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("result 1: " + PartOne());
            Console.WriteLine("result 2: " + PartTwo());
        }
    }
}

