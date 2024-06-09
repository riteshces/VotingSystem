using VotingSystem.Core.Contracts;
using VotingSystem.Core.Models;

namespace VotingSystem.Core
{
    public class VotingPollFactory: IVotingPollFactory
    {
        public VotingPoll Create(VotingPollRequest request)
        {
            if (request.Names.Length < 2)
            {
                throw new ArgumentException("Create poll required atleaset two names.");
            }
            return new VotingPoll { 
                Title = request.Title, 
                Description = request.Description, 
                Counters = request.Names.Select(name => new VotingCounter { VotingOptionName = name }).ToList()
            };
        }
    }
}