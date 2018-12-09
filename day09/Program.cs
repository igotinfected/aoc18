using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day09
{
    class Program
    {
        static void Print(List<int> circle)
        {
            foreach (var marble in circle)
                System.Console.Write(marble + " ");
            System.Console.WriteLine();
        }

        static int Mod(int x, int m)
        {
            int r = x % m;
            return r < 0 ? r + m : r;
        }

        static int PartOne()
        {
            int players;
            int lastMarble;
            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr);
                players = int.Parse(input[0]);
                lastMarble = int.Parse(input[6]);
            }

            var playerScore = new int[players];
            var circle = new List<int> {0};

            var currentMarbleIndex = 0;
            var currentPlayer = 0;
            var marble = 0;
            while (marble++ <= lastMarble)
            {
                currentPlayer = Mod(currentPlayer + 1, players);
                currentMarbleIndex = Mod(currentMarbleIndex + 2, circle.Count + 1);

                if (currentMarbleIndex == 0)
                    currentMarbleIndex++;

                if (marble % 23 == 0)
                {
                    playerScore[currentPlayer] += marble;

                    currentMarbleIndex = Mod(currentMarbleIndex - 7 - 2, circle.Count);
                    playerScore[currentPlayer] += circle[currentMarbleIndex];

                    circle.RemoveAt(currentMarbleIndex);
                }
                else
                {
                    circle.Insert(currentMarbleIndex, marble);
                }
            }

            return playerScore.Max();
        }

        // reimplement using LinkedList for speed 😂
        static long PartTwo()
        {
            int players;
            int lastMarble;
            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr);
                players = int.Parse(input[0]);
                lastMarble = int.Parse(input[6]);
            }

            var playerScore = new long[players];
            var circle = new LinkedList<long>();

            var currentMarble = circle.AddFirst(0);
            var currentPlayer = 0;
            var marble = 0;
            while (marble++ <= lastMarble * 100)
            {
                currentPlayer = Mod(currentPlayer + 1, players);

                if (marble % 23 == 0)
                {
                    playerScore[currentPlayer] += marble;

                    for (var i = 0; i < 7; i++)
                        currentMarble = currentMarble.Previous ?? circle.Last;

                    playerScore[currentPlayer] += currentMarble.Value;

                    var toRemove = currentMarble;
                    currentMarble = toRemove.Next;
                    circle.Remove(toRemove);
                }
                else
                {
                    currentMarble = circle.AddAfter(currentMarble.Next ?? circle.First, marble);
                }
            }

            return playerScore.Max();
        }

        static string[] Input(StreamReader input)
        {
            return input.ReadToEnd().Split(" ");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("result 1: " + PartOne());
            Console.WriteLine("result 2: " + PartTwo());
        }
    }
}
