namespace VotingSystem.Application.Test
{
    public class VotingCounterInteractorTests
    {
        private readonly Mock<IVotingSystemPersistance> _mockPersistance = new Mock<IVotingSystemPersistance>();
        private readonly Mock<IVotingCounterManager> _mockCounterManager = new Mock<IVotingCounterManager>();

        [Fact]
        public void DisplaysPollCounting()
        {
            //Arrange
            var pollId = "1";
            var counter1 = new VotingCounter { VotingOptionName = "One", VoteCount = 2, VotingPercentage = 60 };
            var counter2 = new VotingCounter { VotingOptionName = "Two", VoteCount = 1, VotingPercentage = 40 };
            var counters = new List<VotingCounter> { counter1, counter2 };
            var counterStats1 = new CounterStatistics { VotingOptionName = "One", VoteCount = 2, VotingPercentage = 60 };
            var counterStats2 = new CounterStatistics { VotingOptionName = "Two", VoteCount = 1, VotingPercentage = 40 };
            var counterStats = new List<CounterStatistics> { counterStats1, counterStats2 };
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "desc",
                Counters = counters
            };
            _mockPersistance.Setup(x => x.GetPollAsync(pollId)).ReturnsAsync(poll);
            _mockCounterManager.Setup(x => x.GetVotingPercentage(poll.Counters)).Returns(counterStats);

            //Act
            var interactor = new StatisticsInteractor(
               _mockPersistance.Object,
               _mockCounterManager.Object);
            var pollStatistics = interactor.GetStatistics(pollId).Result;

            //Assert
            pollStatistics.Title.Should().BeEquivalentTo(poll.Title);
            pollStatistics.Description.Should().BeEquivalentTo(poll.Description);
            var resultCounterStats1 = pollStatistics.Counters[0];
            resultCounterStats1.Should().BeEquivalentTo(counterStats1);
            var resultCounterStats2 = pollStatistics.Counters[1];
            resultCounterStats2.Should().BeEquivalentTo(counterStats2);
            _mockCounterManager.Verify(x => x.ResolveExcess(counterStats), Times.Once);
        }
    }
}
