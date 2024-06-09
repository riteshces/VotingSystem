using VotingSystem.Core;
using VotingSystem.Core.Contracts;

namespace VotingSystem.Application
{
    public class StatisticsInteractor : IStatisticsInteractor
    {
        private IVotingSystemPersistance _persistance;
        private IVotingCounterManager _counterManager;

        public StatisticsInteractor(IVotingSystemPersistance votingSystemPersistance, IVotingCounterManager votingCounterManager)
        {
            _persistance = votingSystemPersistance;
            _counterManager = votingCounterManager;
        }

        public async Task<PollStatistics> GetStatistics(string pollId)
        {
            var poll = _persistance.GetPollAsync(pollId).Result;
            var statistics = _counterManager.GetVotingPercentage(poll.Counters);
            _counterManager.ResolveExcess(statistics);
            return new PollStatistics
            {
                PollId = poll.Id,
                Title = poll.Title,
                Description = poll.Description,
                Counters = statistics
            };
        }
    }
}
