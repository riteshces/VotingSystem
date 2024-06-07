using VotingSystem.Core.Contracts;
using VotingSystem.Core.Models;

namespace VotingSystem.Application
{
    public class VotingPollInteractor
    {
        private readonly IVotingPollFactory _votingPollFactory;
        private readonly IVotingSystemPersistance _persistance;

        public VotingPollInteractor(IVotingPollFactory votingPollFactory, IVotingSystemPersistance persistance)
        {
            _votingPollFactory = votingPollFactory;
            _persistance = persistance;
        }
        public void CreateVotingPoll(VotingPollRequest request)
        {
            var poll = _votingPollFactory.Create(request);
            _persistance.SaveVotingPollAsync(poll);
        }
    }
}
