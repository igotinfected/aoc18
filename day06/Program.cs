using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day06
{
    class Tile
    {
        public int X { get; set; }
        public int Y { get; set; }
        public List<Tile> Subtiles = new List<Tile>();
        public int TempDistance { get; set; }
        public bool IsInfinite { get; set; }
    }

    class Program
    {
        static int PartOne()
        {
            var locations = new List<Tile>();

            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr).GetEnumerator();

                while (input.MoveNext())
                {
                    var coordinates = input.Current.Split(',').Select(i => i.Trim()).ToArray();
                    var x = int.Parse(coordinates[0]);
                    var y = int.Parse(coordinates[1]);
                    locations.Add(new Tile { X = x, Y = y });
                }
            }

            var minXCoord = locations.Select(i => i).Min(i => i.X);
            var minYCoord = locations.Select(i => i).Min(i => i.Y);
            var maxXCoord = locations.Select(i => i).Max(i => i.X);
            var maxYCoord = locations.Select(i => i).Max(i => i.Y);

            // calculate area tiles
            for (var i = minXCoord; i <= maxXCoord; i++)
            {
                for (var j = minYCoord; j <= maxYCoord; j++)
                {
                    var tile = FindManhattanTile(locations, i, j);
                    if (tile != null)
                    {
                        tile.Subtiles.Add(new Tile { X = i, Y = j });
                        if (!tile.IsInfinite && i == minXCoord || j == minYCoord
                                             || i == maxXCoord || j == maxYCoord)
                            tile.IsInfinite = true;
                    }
                }
            }

            return locations.Where(i => i.IsInfinite == false).Max(i => i.Subtiles.Count);
        }

        static int PartTwo()
        {
            var locations = new List<Tile>();

            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr).GetEnumerator();

                while (input.MoveNext())
                {
                    var coordinates = input.Current.Split(',').Select(i => i.Trim()).ToArray();
                    var x = int.Parse(coordinates[0]);
                    var y = int.Parse(coordinates[1]);
                    locations.Add(new Tile { X = x, Y = y });
                }
            }

            var minXCoord = locations.Select(i => i).Min(i => i.X);
            var minYCoord = locations.Select(i => i).Min(i => i.Y);
            var maxXCoord = locations.Select(i => i).Max(i => i.X);
            var maxYCoord = locations.Select(i => i).Max(i => i.Y);

            var safeTiles = new List<Tile>();
            for (var i = minXCoord; i <= maxXCoord; i++)
            {
                for (var j = minYCoord; j <= maxYCoord; j++)
                {
                    var runningSum = 0;
                    foreach (var tile in locations)
                        runningSum += ManhattanDistance(i, j, tile.X, tile.Y);

                    if (runningSum < 10000)
                        safeTiles.Add(new Tile { X = i, Y = j });
                }
            }

            return safeTiles.Count;
        }

        static Tile FindManhattanTile(List<Tile> tiles, int x, int y)
        {
            foreach (var tile in tiles)
                tile.TempDistance = ManhattanDistance(tile.X, tile.Y, x, y);

            var orderedTiles = tiles.OrderBy(i => i.TempDistance);
            if (orderedTiles.First().TempDistance == orderedTiles.ElementAt(1).TempDistance)
                return null;    // same manhattan distance
            else
                return orderedTiles.First();
        }

        static int ManhattanDistance(int x1, int y1, int x2, int y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
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

