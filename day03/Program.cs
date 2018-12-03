using System;
using System.IO;
using System.Collections.Generic;

namespace day03
{
    class Program
    {
        static int PartOne()
        {
            // lazy
            var fabric = new int[2000, 2000];

            var overlaps = 0;
            using (var sr = new StreamReader("input.txt"))
            {
                foreach (var line in Input(sr))
                {
                    // #123 @ 3,2: 5x4
                    string[] separators = {"@", " ", ",", ":", "x"};
                    var tokens =
                        line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

                    // get caim dim + pos
                    var leftPadding = int.Parse(tokens[1]);
                    var topPadding = int.Parse(tokens[2]);
                    var claimWidth = int.Parse(tokens[3]);
                    var claimHeight = int.Parse(tokens[4]);

                    // mark coordinates, detect overlaps ONCE
                    for (var i = leftPadding + 1; i <= leftPadding + claimWidth; i++)
                        for (var j = topPadding + 1; j <= topPadding + claimHeight; j++)
                            if (++fabric[i, j] == 2)
                                overlaps++;
                }
            }

            return overlaps;
        }

        static int PartTwo()
        {
            // lazy
            var fabric = new int[2000, 2000];
            var claimList = new List<int>();

            using (var sr = new StreamReader("input.txt"))
            {
                foreach (var line in Input(sr))
                {
                    string[] separators = {"#", "@", " ", ",", ":", "x"};
                    var tokens =
                        line.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);

                    var claimId = int.Parse(tokens[0]);
                    claimList.Add(claimId);

                    // get caim dim + pos
                    var leftPadding = int.Parse(tokens[1]);
                    var topPadding = int.Parse(tokens[2]);
                    var claimWidth = int.Parse(tokens[3]);
                    var claimHeight = int.Parse(tokens[4]);

                    // write ID
                    for (var i = leftPadding + 1; i <= leftPadding + claimWidth; i++)
                        for (var j = topPadding + 1; j <= topPadding + claimHeight; j++)
                        {
                            // if overlap, remove existing and new claim id
                            if (fabric[i, j] != 0)
                            {
                                claimList.Remove(fabric[i, j]);
                                claimList.Remove(claimId);
                            }
                            
                            fabric[i, j] = claimId;
                        }
                }
            }

            if (claimList.Count != 1)
                Console.WriteLine("wtf");

            return claimList[0];
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
