using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using System.Diagnostics.Metrics;

namespace VotingSystem.Application.Test
{
    public class VotingInteractorTests
    {
        private readonly Mock<IVotingSystemPersistance> _mockPersistance;
        private readonly VotingInteractor _votingInteractor;
        private readonly Vote _vote = new Vote() { UserId = "1", CounterId = "1" };
        public VotingInteractorTests()
        {
            _mockPersistance = new Mock<IVotingSystemPersistance>();
            _votingInteractor = new VotingInteractor(_mockPersistance.Object);
        }

        [Fact]
        public async Task Vote_Persist_Vote_When_User_Hasnt_Voted()
        {
            //Act
            _mockPersistance.Setup(x => x.VoteExists(_vote)).ReturnsAsync(false);
            await _votingInteractor.Vote(_vote);

            //Assert
            _mockPersistance.Verify(x => x.SaveVoteAsync(_vote), Times.Once);
        }

        [Fact]
        public async Task Vote_Doesnt_Persist_Vote_When_User_Already_VotedAsync()
        {
            //Act
            _mockPersistance.Setup(x => x.VoteExists(_vote)).ReturnsAsync(true);
            await _votingInteractor.Vote(_vote);

            //Assert
            _mockPersistance.Verify(x => x.SaveVoteAsync(_vote), Times.Never);
        }
    }
}
