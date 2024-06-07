using MongoDB.Driver;
using System;
using VotingSystem.Application;
using VotingSystem.Core.Models;

namespace VotingSystem.Database
{
    public class VotingSystemPersistance : IVotingSystemPersistance
    {
        private readonly AppDbContext _appDbContext;
        public VotingSystemPersistance(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<VotingPoll> GetPollAsync(string pollId)
        {
            var filter = Builders<VotingPoll>.Filter.Eq(v => v.Id, pollId);
            var votingPoll = await _appDbContext.VotingPolls.FindAsync(filter).Result.FirstOrDefaultAsync();
            return votingPoll;
        }

        public async Task<Vote> SaveVoteAsync(Vote vote)
        {
            await _appDbContext.Votes.InsertOneAsync(vote);
            return vote;
        }

        public async Task<VotingPoll> SaveVotingPollAsync(VotingPoll votingPoll)
        {
            await _appDbContext.VotingPolls.InsertOneAsync(votingPoll);
            return votingPoll;
        }

        public async Task<bool> VoteExists(Vote vote)
        {
            var filter = Builders<Vote>.Filter.Eq(v => v.UserId, vote.UserId);
            var votesExists = await _appDbContext.Votes.FindAsync(filter).Result.ToListAsync();
            return votesExists.Any();
        }
    }
}
