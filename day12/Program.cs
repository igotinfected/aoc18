using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace dayXX
{
    class Program
    {

        class Pot
        {
            public bool Plant { get; set; }
            public int Id { get; set; }

            public Pot(int id, bool plant)
            {
                this.Id = id;
                this.Plant = plant;
            }
        }

        static bool NextGeneration(List<Pot> pots, Dictionary<string, char> patterns, int potIndex)
        {
            string pattern = "";

            for (var i = -2; i < 3; i++)
                pattern += pots[potIndex + i].Plant ? '#' : '.';

            char value;
            if (patterns.TryGetValue(pattern, out value))
                return value == '#';
            else
                return false;
        }

        static int PartOne()
        {
            string initialState;
            var patterns = new Dictionary<string, char>();
            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr).GetEnumerator();
                input.MoveNext();

                initialState = input.Current.Split(" ")[2];
                input.MoveNext();

                while (input.MoveNext())
                {
                    var tokenized = input.Current.Split(" ");
                    patterns[tokenized[0]] = tokenized[2][0];
                }
            }

            var pots = new List<Pot>();

            for (var i = 0; i < initialState.Length; i++)
                pots.Add(initialState[i] == '#' ? new Pot(i, true) : new Pot(i, false));

            var generations = 0;
            while (generations < 20)
            {
                // pad left side for pattern matching
                var leftMostPlant = pots.FindIndex(i => i.Plant);
                while (leftMostPlant++ < 6)
                    pots.Insert(0, new Pot(pots.First().Id - 1, false));

                // pad right side for pattern matching
                var rightMostPlant = pots.FindLastIndex(i => i.Plant);
                while (rightMostPlant > pots.Count - 6)
                    pots.Add(new Pot(pots.Last().Id + 1, false));

                var newPots = new List<Pot>();
                foreach (var pot in pots.GetRange(2, pots.Count - 4))
                    newPots.Add(new Pot(pot.Id, NextGeneration(pots, patterns, pots.IndexOf(pot))));

                pots = newPots;
                generations++;
            }

            return pots.Where(p => p.Plant).Sum(p => p.Id);
        }

        static long PartTwo()
        {   // at some point the sum stabilises, and keeps adding 65.
            // pick a decent amount of iterations (I took 1999), check the difference
            // between 2000s sum and 1999s sum, multiply 5B - 1999 by that and add to 2000s sum!
            return 130891 + (50000000000 - 1999) * 65;
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
