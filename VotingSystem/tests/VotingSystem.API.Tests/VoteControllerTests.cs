

using VotingSystem.API.Tests.Fixtures;

namespace VotingSystem.API.Tests
{
    public class VoteControllerTests
    {
        [Fact]
        public async Task AddVote_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var persistanceMock = new Mock<IVotingSystemPersistance>();
            var votingInteractor = new Mock<IVotingInteractor>();
            var voteController = new VoteController(persistanceMock.Object, votingInteractor.Object);
            var voteRequest = VoteFixtures.GetVoteRequest();
            var vote = VoteFixtures.GetVote();
            var result = true;
            persistanceMock.Setup(m => m.SaveVoteAsync(vote)).Returns(Task.FromResult(vote));

            // Act
            var actionResult = await voteController.AddVote(voteRequest);

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>();
        }
    }
}