

namespace VotingSystem.Database.Tests
{
    public class VotingSystemPersistanceTests
    {
        private readonly Mock<IMongoClient> _mockClient;
        public VotingSystemPersistanceTests()
        {
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public async void SaveVotingPollAsync_PersistsVotingPoll()
        {
            //Arrange
            var votingPoll = VotingPollFixture.GetVotingPoll();
            AppDbContext appDbContext = new AppDbContext(_mockClient.Object);

            //Act
            IVotingSystemPersistance votingSystemPersistance = new VotingSystemPersistance(appDbContext);
            votingSystemPersistance.SaveVotingPollAsync(votingPoll);
            var filter = Builders<VotingPoll>.Filter.Eq(v => v.Id, votingPoll.Id);
            var savedVotingPoll = await appDbContext.VotingPolls.FindAsync(filter).Result.ToListAsync();

            //Assert
            savedVotingPoll.Should().NotBeNull();
            savedVotingPoll.Should().HaveCount(1);
            savedVotingPoll[0].Title.Should().Be(votingPoll.Title);
            savedVotingPoll[0].Description.Should().Be(votingPoll.Description);
        }


        [Fact]
        public async void SaveVoteAsync_PersistsVote()
        {
            //Arrange
            var vote = VoteFixture.GetVotes();
            AppDbContext appDbContext = new AppDbContext(_mockClient.Object);

            //Act
            IVotingSystemPersistance votingSystemPersistance = new VotingSystemPersistance(appDbContext);
            await votingSystemPersistance.SaveVoteAsync(vote);
            var filter = Builders<Vote>.Filter.Eq(v => v.Id, vote.Id);
            var savedVote = await appDbContext.Votes.FindAsync(filter).Result.ToListAsync();

            //Assert
            savedVote.Should().NotBeNull();
            savedVote.Should().HaveCount(1);
            savedVote[0].CounterId.Should().Be(vote.CounterId);
            savedVote[0].UserId.Should().Be(vote.UserId);
        }


        [Fact]
        public async void VoteExists_Return_False_When_No_Vote_Submitted_By_User()
        {
            //Arrange
            var vote = VoteFixture.GetVotes();
            AppDbContext appDbContext = new AppDbContext(_mockClient.Object);

            //Act
            IVotingSystemPersistance votingSystemPersistance = new VotingSystemPersistance(appDbContext);
            bool result = await votingSystemPersistance.VoteExists(vote);


            //Assert
            result.Should().Be(false);
        }

        [Fact]
        public async void VoteExists_Return_True_When_Vote_Submitted_By_User()
        {
            //Arrange
            var vote = VoteFixture.GetVotes();
            AppDbContext appDbContext = new AppDbContext(_mockClient.Object);
            IVotingSystemPersistance votingSystemPersistance = new VotingSystemPersistance(appDbContext);
            await votingSystemPersistance.SaveVoteAsync(vote);

            //Act
            bool result = await votingSystemPersistance.VoteExists(vote);


            //Assert
            result.Should().Be(true);
        }

    }
}
