namespace VotingSystem.Test
{
    public class CounterManager
    {
        public VotingCounter GetVotingPercentage(VotingCounter votingCounter, int Total)
        {
            votingCounter.VotingPercentage = RoundUp((votingCounter.VoteCount * 100.0) / Total);
            return votingCounter;
        }
        public void ResolveAccess(List<VotingCounter> counters)
        {
            var total = counters.Sum(c => c.VotingPercentage);
            if (total == 100) return;
            var excess = 100 - total;
            var highestPercentage = counters.Max(c => c.VotingPercentage);
            var highestCounters = counters.Where(c => c.VotingPercentage == highestPercentage).ToList();
            if (highestCounters.Count == 1)
            {
                highestCounters.First().VotingPercentage += excess;
            }
            else if (highestCounters.Count < counters.Count)
            {
                var lowestPercentage = counters.Min(c => c.VotingPercentage);
                var lowestCounter = counters.First(c => c.VotingPercentage == lowestPercentage);
                double num = lowestCounter.VotingPercentage += excess;
                lowestCounter.VotingPercentage = RoundUp(num);
            }
        }
        double RoundUp(double num) => Math.Round(num, 2);
    }
}