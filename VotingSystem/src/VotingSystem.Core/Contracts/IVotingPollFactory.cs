using VotingSystem.Core.Models;

namespace VotingSystem.Core.Contracts
{
    public interface IVotingPollFactory
    {
        VotingPoll Create(VotingPollRequest request);
    }
}
