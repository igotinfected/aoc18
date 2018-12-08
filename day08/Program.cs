using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

// Some inspiration taken from reddit users
// "keithnicholasnz" and "dylanfromwinnipeg"

namespace day08
{
    class Node
    {
        public int Children { get; set; }
        public List<Node> ChildNodes { get; set; } = new List<Node>();
        public List<int> Metadata { get; set; } = new List<int>();

        public int Value()
        {
            if (ChildNodes.Count == 0)
                return Metadata.Sum();
            else
                return Metadata.Where(index => index - 1 < ChildNodes.Count)
                               .Sum(index => ChildNodes[index - 1].Value());
        }

        public int MetadataSum()
        {
            var res = Metadata.Sum();
            ChildNodes.ForEach(c => res += c.MetadataSum());

            return res;
        }
    }

    class Program
    {
        public static Node ProcessInput(List<int> input)
        {
            var result = new Node();

            var children = input[0];
            var metadata = input[1];

            // remove parent data
            input.RemoveRange(0, 2);

            // add child data
            for (var i = 0; i < children; i++)
                result.ChildNodes.Add(ProcessInput(input));

            // add meta data
            for (var j = 0; j < metadata; j++, input.RemoveAt(0))
                result.Metadata.Add(input[0]);

            return result;
        }

        static int PartOne()
        {
            Node parent;
            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr);

                parent = ProcessInput(input);
            }

            return parent.MetadataSum();
        }

        static int PartTwo()
        {
            Node parent;

            using (var sr = new StreamReader("input.txt"))
            {
                var input = Input(sr);

                parent = ProcessInput(input);
            }

            return parent.Value();
        }

        static List<int> Input(StreamReader input)
        {
            return input.ReadToEnd().Split(" ").Select(i => int.Parse(i)).ToList();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("result 1: " + PartOne());
            Console.WriteLine("result 2: " + PartTwo());
        }
    }
}
