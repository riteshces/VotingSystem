using VotingSystem.Core.Models;

namespace VotingSystem.Application
{
    public interface IVotingSystemPersistance
    {
        Task<VotingPoll> GetPollAsync(string pollId);
        Task<Vote> SaveVoteAsync(Vote vote);
        Task<VotingPoll> SaveVotingPollAsync(VotingPoll votingPoll);
        Task<bool> VoteExists(Vote vote);
    }
}
