using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day02
{
    class Program
    {
        static int PartOne()
        {
            using (var sr = new StreamReader("input.txt"))
            {
                var twice = 0;
                var thrice = 0;

                var words = Input(sr).GetEnumerator();
                while (words.MoveNext())
                {
                    var occurrences =
                        words.Current.GroupBy(ch => ch)
                                     .ToDictionary(ch => ch.Key, ch => ch.Count());

                    if (occurrences.ContainsValue(2))
                        twice++;

                    if (occurrences.ContainsValue(3))
                        thrice++;
                }

                return twice * thrice;
            }
        }

        static string PartTwo()
        {
            using (var sr = new StreamReader("input.txt"))
            {
                string closestMatchOne = "";
                string closestMatchTwo = "";
                int shortestDistance = int.MaxValue;
                var words = Input(sr).GetEnumerator();
                while (words.MoveNext())
                {
                    using (var srCopy = new StreamReader("inputCopy.txt"))
                    {
                        var compareTo = Input(srCopy).GetEnumerator();
                        while (compareTo.MoveNext())
                        {
                            if (words.Current == compareTo.Current)
                                continue;

                            int distance = HammingDistance(words.Current, compareTo.Current);

                            if (distance < shortestDistance)
                            {
                                closestMatchOne = words.Current;
                                closestMatchTwo = compareTo.Current;
                                shortestDistance = distance;
                            }
                        }
                    }
                }

                return string.Concat(closestMatchOne.Where((c, i) => c == closestMatchTwo[i]));
            }
        }

        static IEnumerable<String> Input(StreamReader input)
        {
            string line;
            while ((line = input.ReadLine()) != null)
                yield return line;
        }

        static int HammingDistance(string s, string t)
        {
            int distance = s.ToCharArray()
                            .Zip(t.ToCharArray(), (sch, tch) => new { sch, tch })
                            .Count(m => m.sch != m.tch);

            return distance;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("result 1: " + PartOne());
            Console.WriteLine("result 2: " + PartTwo());
        }
    }
}
