using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

/*
 * Note: this code is disgusting, I was pretty much just playing around with LINQ because I was
 *       too lazy to manually iterate over the guards... :D
 *       ngl this is probably the nasties thing I've ever written.
 */

namespace day04
{
    class Guard
    {
        public string id;
        public List<int> minutes;

        public Guard(string id)
        {
            this.id = id;
            minutes = new List<int>();
        }

        public void Sleep(int minute) { minutes.Add(minute); }
    }

    class Program
    {

        static int PartOne()
        {
            using (var sr = new StreamReader("input.txt"))
            {
                var orderedLogs = Input(sr).OrderBy(item => item).GetEnumerator();

                var guardDict = new Dictionary<string, Guard>();
                Guard activeGuard = null;

                // parse logs
                while (orderedLogs.MoveNext())
                {
                    var logItem = orderedLogs.Current;

                    if (logItem.Contains("#"))
                    {
                        // guard id
                        var startIndex = logItem.IndexOf('#') + 1;
                        var length = logItem.IndexOf('b') - startIndex - 1;
                        var id = logItem.Substring(startIndex, length);

                        if (guardDict.ContainsKey(id))
                        {
                            activeGuard = guardDict[id];
                        }
                        else
                        {
                            activeGuard = new Guard(id);
                            guardDict.Add(id, activeGuard);
                        }
                    }
                    else
                    {
                        var sleepStartItem = logItem;
                        var startIndex = sleepStartItem.IndexOf(':') + 1;
                        var sleepStart = int.Parse(sleepStartItem.Substring(startIndex, 2));

                        orderedLogs.MoveNext();
                        logItem = orderedLogs.Current;
                        var sleepEndItem = logItem;
                        var sleepEnd = int.Parse(sleepEndItem.Substring(startIndex, 2));

                        for (var minute = sleepStart; minute < sleepEnd; minute++)
                            activeGuard.Sleep(minute);
                    }
                }

                // order by sum of slept minutes, select first in that ordering
                var sleepyGuard = guardDict.OrderByDescending(kvp => kvp.Value.minutes.Sum())
                                           .Select(guard => guard.Value)
                                           .First();

                // group the numbers, order by count, select the first key (minute)
                var sleepyMinute = sleepyGuard.minutes.GroupBy(x => x)
                                                      .OrderByDescending(x => x.Count())
                                                      .Select(x => x.Key)
                                                      .First();

                return int.Parse(sleepyGuard.id) * sleepyMinute;
            }
        }

        static int PartTwo()
        {
            using (var sr = new StreamReader("input.txt"))
            {
                var orderedLogs = Input(sr).OrderBy(item => item).GetEnumerator();

                var guardDict = new Dictionary<string, Guard>();
                Guard activeGuard = null;

                // parse logs
                while (orderedLogs.MoveNext())
                {
                    var logItem = orderedLogs.Current;

                    if (logItem.Contains("#"))
                    {
                        // guard id
                        var startIndex = logItem.IndexOf('#') + 1;
                        var length = logItem.IndexOf('b') - startIndex - 1;
                        var id = logItem.Substring(startIndex, length);

                        if (guardDict.ContainsKey(id))
                        {
                            activeGuard = guardDict[id];
                        }
                        else
                        {
                            activeGuard = new Guard(id);
                            guardDict.Add(id, activeGuard);
                        }
                    }
                    else
                    {
                        var sleepStartItem = logItem;
                        var startIndex = sleepStartItem.IndexOf(':') + 1;
                        var sleepStart = int.Parse(sleepStartItem.Substring(startIndex, 2));

                        orderedLogs.MoveNext();
                        logItem = orderedLogs.Current;
                        var sleepEndItem = logItem;
                        var sleepEnd = int.Parse(sleepEndItem.Substring(startIndex, 2));

                        for (var minute = sleepStart; minute < sleepEnd; minute++)
                            activeGuard.Sleep(minute);
                    }
                }

                // order by biggest count of minutes, select the first one in that order
                // DefaultIfEmpty in case 'minutes' is empty
                var mostConsistentGuard = guardDict.OrderByDescending
                                                   (
                                                        kvp => kvp.Value.minutes.DefaultIfEmpty()
                                                                                .GroupBy(x => x)
                                                                                .Max(x => x.Count())
                                                   )
                                                   .Select(x => x.Value)
                                                   .First();

                //group the numbers, order by count, select the first key (minute)
                var mostSleptMinute = mostConsistentGuard.minutes.GroupBy(x => x)
                                                                 .OrderByDescending(x => x.Count())
                                                                 .Select(x => x.Key)
                                                                 .First();

                return int.Parse(mostConsistentGuard.id) * mostSleptMinute;
            }
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
