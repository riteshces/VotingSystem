

using Microsoft.AspNetCore.Http.HttpResults;
using VotingSystem.API.Tests.Fixtures;
using VotingSystem.Core.Models;

namespace VotingSystem.API.Tests
{
    public class PollControllerTests
    {
        private readonly Mock<IVotingPollFactory> _pollFactoryMock;
        private readonly Mock<IVotingSystemPersistance> _persistanceMock;
        private readonly Mock<IVotingCounterManager> _votingCounterManagerMock;
        private readonly Mock<IStatisticsInteractor> _statisticsInteractor;
        private PollController _pollController;
        public PollControllerTests()
        {
            _pollFactoryMock = new Mock<IVotingPollFactory>();
            _persistanceMock = new Mock<IVotingSystemPersistance>();
            _votingCounterManagerMock = new Mock<IVotingCounterManager>();
            _statisticsInteractor = new Mock<IStatisticsInteractor>();
            _pollController = new PollController(_pollFactoryMock.Object, _persistanceMock.Object, _votingCounterManagerMock.Object, _statisticsInteractor.Object);
        }

        [Fact]
        public async Task Create_VotingPollRequestIsValid_ReturnsOkResult()
        {
            // Arrange
            var request = PollFixtures.GetVotingPollRequest();
            var votingPoll = PollFixtures.GetVotingPoll();
            _pollFactoryMock.Setup(pf => pf.Create(request)).Returns(votingPoll);
            _persistanceMock.Setup(m => m.SaveVotingPollAsync(votingPoll)).Returns(Task.FromResult(votingPoll));

            // Act
            var result = await _pollController.Create(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task Create_VotingPollRequestIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var request = new VotingPollRequest { Title = "test", Description = "testdescription" };

            // Act
            var result = await _pollController.Create(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetVotingPoll_PollIdIsValid_ReturnsOkResult()
        {
            // Arrange
            var votingPoll = PollFixtures.GetVotingPoll();
            var pollId = votingPoll.Id;
            _persistanceMock.Setup(p => p.GetPollAsync(pollId)).ReturnsAsync(votingPoll);

            // Act
            var result = await _pollController.GetVotingPoll(pollId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task GetVotingPoll_PollIdIsInvalid_ReturnsBadRequestResult()
        {
            // Arrange
            var pollId = "1";
            _persistanceMock.Setup(p => p.GetPollAsync(pollId)).ReturnsAsync((VotingPoll)null);

            // Act
            var result = await _pollController.GetVotingPoll(pollId);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task GetStatistics_ValidPollId_ReturnsOkResult()
        {
            // Arrange
            var pollId = "123456789012345678901234";
            var statistics = new PollStatistics
            {
                PollId = pollId,
                Title = "test",
                Description = "description"
            };
            _statisticsInteractor.Setup(p => p.GetStatistics(pollId)).ReturnsAsync(statistics);

            // Act
            var result = await _pollController.GetStatistics(pollId);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

    }
}