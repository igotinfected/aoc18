using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day10
{
    class Point
    {
        public int X;
        public int Y;
        int Dx;
        int Dy;

        public Point(int X, int Y, int Dx, int Dy)
        {
            this.X = X;
            this.Y = Y;
            this.Dx = Dx;
            this.Dy = Dy;
        }

        public void Move()
        {
            X += Dx;
            Y += Dy;
        }
    }
    class Program
    {
        static int PartOne()
        {
            var points = new List<Point>();
            using (var sr = new StreamReader("test.txt"))
            {
                var input = Input(sr);

                foreach (var line in input)
                {
                    var position = line.Split('<')[1].Split('>')[0].Split(',');
                    var velocity = line.Split('<')[2].Split('>')[0].Split(',');

                    var x = int.Parse(position[0].Trim());
                    var y = int.Parse(position[1].Trim());
                    var dx = int.Parse(velocity[0].Trim());
                    var dy = int.Parse(velocity[1].Trim());

                    points.Add(new Point(x, y, dx, dy));
                }

                var minX = points.Min(p => p.X);
                var minY = points.Min(p => p.Y);
                var maxX = points.Max(p => p.X);
                var maxY = points.Max(p => p.Y);

                while (true)
                {
                    var pointsCopy = new List<Point>(points);

                    points.ForEach(p => p.Move());

                    var newMinX = points.Min(p => p.X);
                    var newMinY = points.Min(p => p.Y);
                    var newMaxX = points.Max(p => p.X);
                    var newMaxY = points.Max(p => p.Y);

                    if (newMaxX - newMaxY > maxX - minX || newMaxY - newMinY > maxY - minY)
                    {
                        for (var i = minY; i <= maxY; i++)
                        {
                            for (var j = minX; j <= maxX; j++)
                            {
                                Console.Write(pointsCopy.Any(p => p.X == j && p.Y == i) ? "#" : ".");
                            }
                            Console.WriteLine();
                        }
                        Console.ReadLine();
                    }

                    minX = newMinX;
                    minY = newMinY;
                    maxX = newMaxX;
                    maxY = newMaxY;
                }
            }


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
