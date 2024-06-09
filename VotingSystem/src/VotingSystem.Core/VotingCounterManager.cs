using System.Diagnostics.Metrics;
using VotingSystem.Core.Contracts;
using VotingSystem.Core.Models;

namespace VotingSystem.Core
{
    public class VotingCounterManager : IVotingCounterManager
    {
        public List<CounterStatistics> GetVotingPercentage(ICollection<VotingCounter> counters)
        {
            var totalCount = counters.Sum(x => x.VoteCount);
            return counters.Select(x => new CounterStatistics
            {
                Id = x.Id,
                VotingOptionName = x.VotingOptionName,
                VoteCount = x.VoteCount,
                VotingPercentage = totalCount > 0 ? Math.Round((x.VoteCount * 100.0 / totalCount), 2, MidpointRounding.ToZero) : 0
            }).ToList();
        }
        public void ResolveExcess(List<CounterStatistics> counters)
        {
            var total = counters.Sum(c => c.VotingPercentage);
            if (total == 100) return;
            var excess = 100 - total;
            var highestCounters = counters.Where(c => c.VotingPercentage == counters.Max(c => c.VotingPercentage)).ToList();
            if (highestCounters.Count == 1)
            {
                highestCounters.First().VotingPercentage += excess;
            }
            else if (highestCounters.Count < counters.Count)
            {
                var lowestCounter = counters.OrderBy(c => c.VotingPercentage).First();
                lowestCounter.VotingPercentage = RoundUp(lowestCounter.VotingPercentage + excess);
            }
        }
        double RoundUp(double num) => Math.Round(num, 2);
    }
}