using System;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.core.Models;

namespace VotingSystem.core
{
    public class CounterManager
    {
        public List<Counter> Counters { get; set; }

        public CounterManager(params Counter[] counter)
        {
            Counters = counter.ToList();
        }

        public bool ResolveExcess()
        {
            var totalPercent = Counters.Sum(counter => counter.Percent);
            if (totalPercent > 100.00) throw new Exception("totalPercent of counters accedded 100.00!!");
            if (totalPercent == 100.00) return true;

            var excess = 100.00 - totalPercent;

           var maxPercent = Counters.Max(Counter => Counter.Percent);

           var winnerCounter =  Counters.Where(c => c.Percent == maxPercent);
            if (winnerCounter.Count() == 1)
            {
                winnerCounter.First().Percent = Math.Round(maxPercent + excess, 2);
                return true;
            }

            var minPercent = Counters.Min(couter => couter.Percent);
            var lowestCount = Counters.Where(C => C.Percent == minPercent);
            if (lowestCount.Count() == 1 && maxPercent > (minPercent+excess))
            {
                lowestCount.First().Percent = Math.Round(minPercent + excess, 2);
                return true;
            }

            return false;
        }


        public Counter GetCounterStatistics(Counter counter,int totalCount)
        {
            counter.Percent = Math.Round(counter.Count * 100.0 / totalCount, 2);
            return counter;
        }

       
    }


}


