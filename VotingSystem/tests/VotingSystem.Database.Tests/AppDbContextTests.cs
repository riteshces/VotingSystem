namespace VotingSystem.Database.Tests
{
    public class AppDbContextTests
    {
        private readonly Mock<IMongoClient> _mockClient;
        public AppDbContextTests()
        {
            _mockClient = new Mock<IMongoClient>();
        }

        [Fact]
        public async void SaveVotingCounter_Should_Save_Voting_Counter_To_Database()
        {
            //Arrange
            var votingCounter = VotingCounterFixture.GetVotingCounter();
            var dbContext = new AppDbContext(_mockClient.Object);

            //Act
            await dbContext.VotingCounters.InsertOneAsync(votingCounter);
            var filter = Builders<VotingCounter>.Filter.Eq(v => v.Id, votingCounter.Id);
            var savedVotingCounter = await dbContext.VotingCounters.FindAsync(filter).Result
                                                            .SingleAsync();

            //Assert
            savedVotingCounter.Should().NotBeNull();
            savedVotingCounter.Should().BeEquivalentTo(votingCounter, options => options.Excluding(c => c.Id));
        }

        [Fact]
        public async void SaveVotingPoll_Should_Save_Voting_Poll_To_Database()
        {
            //Arrange
            var votingPoll =VotingPollFixture.GetVotingPoll();
            var dbContext = new AppDbContext(_mockClient.Object);

            //Act
            await dbContext.VotingPolls.InsertOneAsync(votingPoll);
            var filter = Builders<VotingPoll>.Filter.Eq(v => v.Id, votingPoll.Id);
            var savedVotingPoll = await dbContext.VotingPolls.FindAsync(filter).Result
                                                            .SingleAsync();

            //Assert
            savedVotingPoll.Should().NotBeNull();
            savedVotingPoll.Should().BeEquivalentTo(votingPoll, options => options.Excluding(c => c.Id));
        }

        [Fact]
        public async void SaveVote_Should_Save_Vote_To_Database()
        {
            //Arrange
            var vote = VoteFixture.GetVotes();
            var dbContext = new AppDbContext(_mockClient.Object);

            //Act
            await dbContext.Votes.InsertOneAsync(vote);
            var filter = Builders<Vote>.Filter.Eq(v => v.Id, vote.Id);
            var savedVote = await dbContext.Votes.FindAsync(filter).Result
                                                            .SingleAsync();

            //Assert
            savedVote.Should().NotBeNull();
            savedVote.Should().BeEquivalentTo(vote, options => options.Excluding(c => c.Id));
        }
    }
}