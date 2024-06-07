using VotingSystem.Core.Models;

namespace VotingSystem.Application
{
    public class VotingInteractor
    {
        private readonly IVotingSystemPersistance _persistance;

        public VotingInteractor(IVotingSystemPersistance persistance)
        {
            _persistance = persistance;
        }

        public async Task Vote(Vote vote)
        {
            if (!await _persistance.VoteExists(vote))
            {
                await _persistance.SaveVoteAsync(vote);
            }

        }
    }
}
