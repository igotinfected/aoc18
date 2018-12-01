using System;
using System.IO;
using System.Collections.Generic;

namespace day01
{
    class Program
    {
        static int PartOne()
        {
            using (var sr = new StreamReader("input.txt"))
            {
                var frequency = 0;
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var change = Int32.Parse(line);
                    frequency += change;
                }

                return frequency;
            }
        }

        static HashSet<int> frequencies = new HashSet<int>();
        static int PartTwo(int frequency)
        {
            using (var sr = new StreamReader("input.txt"))
            {
                string line;
                while (true)
                {
                    line = sr.ReadLine();
                    var change = Int32.Parse(line);

                    if (frequencies.Add((int)frequency)) // if unique, continue
                        frequency += change;
                    else // found our result
                        break;

                    if (sr.Peek() == -1) // reset stream if we reach the end
                    {
                        frequency = PartTwo(frequency);
                        break;
                    }
                }

                return frequency;
            }
        }

        static void Main(string[] args)
        {
            string resultOne = "result 1: " + PartOne();
            string resultTwo = "result 2: " + PartTwo(0);

            Console.WriteLine(resultOne);
            Console.WriteLine(resultTwo);
        }
    }
}
