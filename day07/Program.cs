using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

// another YIKES solution
// TODO: rewrite this piece of garbo

namespace day07
{
    class Program
    {
        static string PartOne()
        {
            var requirements = new SortedDictionary<char, HashSet<char>>();
            using (var sr = new StreamReader("input.txt"))
            {
                foreach (var line in Input(sr))
                {
                    var tokens = line.Split(" ");
                    var step = tokens[7][0];
                    var prereq = tokens[1][0];

                    // add chars to dict that have no prereq.
                    if (!requirements.TryGetValue(prereq, out var _))
                        requirements.Add(prereq, new HashSet<char>());

                    if (requirements.TryGetValue(step, out var list))
                    {
                        list.Add(prereq);
                        requirements[step] = list;
                    }
                    else
                    {
                        requirements.Add(step, new HashSet<char> { prereq });
                    }
                }
            }

            string result = "";

            var firstStep = requirements.Select(i => i)
                                        .Where(i => i.Value.Count == 0)
                                        .First();
            result += firstStep.Key;
            requirements.Remove(firstStep.Key);

            var completedSteps = new List<char> { firstStep.Key };
            while (requirements.Count != 0)
            {
                foreach (var step in requirements)
                {
                    var completeableStep = true;
                    foreach (var preStep in step.Value)
                    {
                        if (!completedSteps.Contains(preStep))
                        {
                            completeableStep = false;
                            break;
                        }
                    }

                    if (!completeableStep)
                        continue;

                    result += step.Key;
                    completedSteps.Add(step.Key);
                    requirements.Remove(step.Key);
                    break;
                }
            }

            return result;
        }

        static int PartTwo()
        {
            var requirements = new SortedDictionary<char, HashSet<char>>();
            using (var sr = new StreamReader("input.txt"))
            {
                foreach (var line in Input(sr))
                {
                    var tokens = line.Split(" ");
                    var step = tokens[7][0];
                    var prereq = tokens[1][0];

                    // add chars to dict that have no prereq.
                    if (!requirements.TryGetValue(prereq, out var _))
                        requirements.Add(prereq, new HashSet<char>());

                    if (requirements.TryGetValue(step, out var list))
                    {
                        list.Add(prereq);
                        requirements[step] = list;
                    }
                    else
                    {
                        requirements.Add(step, new HashSet<char> { prereq });
                    }
                }
            }

            int elapsedTime = -1; // 0th second counts as first second
            var jobCompletionTime = 60;
            var workers = 5;

            var jobs = new Dictionary<char, int>();
            while (requirements.Count != 0)
            {   // get availableWorkers and pass time
                var availableWorkers = workers - jobs.Count;
                if (availableWorkers < workers)
                    elapsedTime += 1;

                // get completeable steps BEFORE modifying our jobs
                // otherwise older jobs might get pushed back due to alphabetical ordering
                var completeableSteps = requirements.Select(i => i)
                                                    .Where(i => !jobs.Any(i2 => i.Key == i2.Key))
                                                    .Where(i => i.Value.Count == 0)
                                                    .OrderBy(i => i.Key);

                // check jobs for completion
                foreach (var job in jobs.ToArray())
                {
                    if (job.Value > 0)
                    {
                        if (--jobs[job.Key] == 0)
                        {
                            jobs.Remove(job.Key);
                            requirements.Remove(job.Key);
                            requirements.ToList().ForEach(i => i.Value.Remove(job.Key));
                        }
                    }
                }

                // add jobs if possible
                if (completeableSteps.Count() > 0)
                {
                    if (completeableSteps.Count() <= availableWorkers)
                    {
                        foreach (var step in completeableSteps)
                            jobs.Add(step.Key, jobCompletionTime + step.Key - 'A' + 1);
                    }
                    else if (completeableSteps.Count() > availableWorkers)
                    {
                        for (var i = 0; i < availableWorkers; i++)
                        {
                            var step = completeableSteps.ElementAt(i);

                            jobs.Add(step.Key, jobCompletionTime + step.Key - 'A' + 1);
                        }
                    }
                }
            }

            return elapsedTime;
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
