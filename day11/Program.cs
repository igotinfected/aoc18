using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace day11
{
    class Program
    {
        static (int, int) PartOne()
        {
            var serialNumber = 3463;
            var grid = new int[300, 300];

            for (var y = 1; y <= 300; y++)
            {
                for (var x = 1; x <= 300; x++)
                {
                    var rackId = x + 10;
                    grid[x - 1, y - 1] = ((((y * rackId + serialNumber) * rackId) / 100) % 10) - 5;
                }
            }

            var maxSum = 0;
            var maxPos = (0, 0);
            for (var y = 0; y < 300 - 2; y++)
            {
                for (var x = 0; x < 300 - 2; x++)
                {
                    var sum = grid[x, y] + grid[x + 1, y] + grid[x + 2, y]
                            + grid[x, y + 1] + grid[x + 1, y + 1] + grid[x + 2, y + 1]
                            + grid[x, y + 2] + grid[x + 1, y + 2] + grid[x + 2, y + 2];
                    if (sum > maxSum)
                    {
                        maxSum = sum;
                        maxPos = (x + 1, y + 1);
                    }
                }
            }

            return maxPos;
        }

        static (int, int, int) PartTwo()
        {
            var serialNumber = 3463;
            var grid = new int[300, 300];

            for (var y = 1; y <= 300; y++)
            {
                for (var x = 1; x <= 300; x++)
                {
                    var rackId = x + 10;
                    grid[x - 1, y - 1] = ((((y * rackId + serialNumber) * rackId) / 100) % 10) - 5;
                }
            }

            var maxSum = 0;
            var pos = (0, 0, 0);
            for (var windowSize = 1; windowSize <= 300; windowSize++)
            {   // for each windowSize
                for (var y = 0; y < 300 - windowSize + 1; y++)
                {   // calculate the sums for each subwindows
                    for (var x = 0; x < 300 - windowSize + 1; x++)
                    {
                        var sum = 0;
                        for (var subY = 0; subY < windowSize; subY++)
                            for (var subX = 0; subX < windowSize; subX++)
                                sum += grid[x + subX, y + subY];

                        if (sum > maxSum)
                        {
                            maxSum = sum;
                            pos = (x + 1, y + 1, windowSize);
                            // soon as there's no more output you can assume that's the answer
                            Console.WriteLine(pos);
                        }
                    }
                }
            }

            return pos;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("result 1: " + PartOne());
            Console.WriteLine("result 2: " + PartTwo());
        }
    }
}
