using VotingSystem.Core.Models;

namespace VotingSystem.Core.Contracts
{
    public interface IVotingCounterManager
    {
        List<CounterStatistics> GetVotingPercentage(ICollection<VotingCounter> counters);
        void ResolveExcess(List<CounterStatistics> counters);
    }
}
