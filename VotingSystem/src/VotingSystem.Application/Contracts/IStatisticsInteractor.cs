using VotingSystem.Core;

namespace VotingSystem.Application
{
    public interface IStatisticsInteractor
    {
        Task<PollStatistics> GetStatistics(string pollId);
    }
}
