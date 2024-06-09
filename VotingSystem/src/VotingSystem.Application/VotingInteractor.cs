using VotingSystem.Application.Contracts;
using VotingSystem.Core.Models;

namespace VotingSystem.Application
{
    public class VotingInteractor:IVotingInteractor
    {
        private readonly IVotingSystemPersistance _persistance;

        public VotingInteractor(IVotingSystemPersistance persistance)
        {
            _persistance = persistance;
        }

        public async Task<Vote> Vote(Vote vote)
        {
            if (!await _persistance.VoteExists(vote))
            {
                return await _persistance.SaveVoteAsync(vote);
            }
            else
            {
                throw new InvalidOperationException("Vote already exists");
            }
        }
    }
}
