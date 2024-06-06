using VotingSystem.Core.Models;

namespace VotingSystem.Core
{
    public class VotingCounterManager
    {
        public VotingCounter GetVotingPercentage(VotingCounter votingCounter, int total)
        {
            votingCounter.VotingPercentage = Math.Round((votingCounter.VoteCount * 100.0) / total, 2, MidpointRounding.ToZero);
            return votingCounter;
        }
        public void ResolveAccess(List<VotingCounter> counters)
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