using AutoFixture;
using System.Diagnostics.Metrics;
using System.Reflection;

namespace VotingSystem.Core.Test
{
    public class VotingCounterManagerTests
    {
        public readonly VotingCounter _votingCounter;
        public readonly VotingCounterManager _counterManager;

        public const string CounterId = "1";
        public const string CounterName = "Counter Name";
        public VotingCounter _counter = new VotingCounter { Id = CounterId, VotingOptionName = CounterName, VoteCount = 5 };
        public VotingCounterManagerTests()
        {
            _votingCounter = new VotingCounter();
            _counterManager = new VotingCounterManager();
        }

        [Fact]
        public void VotingCounter_Should_Check_Has_Voting_Option_Name()
        {
            //Arrange
            string optionName = "yes";

            //Act
            _votingCounter.VotingOptionName = optionName;

            //Assert
            _votingCounter.VotingOptionName.Should().BeEquivalentTo(optionName);
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void VotingCounter_Should_Return_Voting_Option_Name_With_Count(string optionName, int voteCount)
        {
            //Act
            _votingCounter.VotingOptionName = optionName;
            _votingCounter.VoteCount = voteCount;

            //Assert
            _votingCounter.VotingOptionName.Should().NotBeNullOrEmpty().And.BeEquivalentTo(optionName);
            _votingCounter.VoteCount.Should().Be(voteCount);
        }

        [Theory]
        [InlineData(5, 10, 50)]
        [InlineData(1, 3, 33.33)]
        [InlineData(2, 3, 66.66)]
        public void GetVotingPercentage_Should_Return_Percentage_Of_Voting_Option_Count_Based_On_Total_Count(int count, int totalCount, double expectedPercentage)
        {
            //Arrange
            _counter.VoteCount = count;
            var counter = new VotingCounter { VoteCount = totalCount - count };

            //Act
            var result = new VotingCounterManager().GetVotingPercentage(new[] { _counter, counter }).First();

            //Assert
            result.VoteCount.Should().Be(count).And.BePositive();
            result.VotingPercentage.Should().Be(expectedPercentage).And.BePositive();
        }

        [Fact]
        public void Resolve_Access_Should_Not_Add_Access_When_All_Counter_Are_Equal()
        {
            //Arrange
            var expectedResult = 33.33;
            var modiCounter = new CounterStatistics { VoteCount = 1, VotingPercentage = 33.33 };
            var oppositionCounter = new CounterStatistics { VoteCount = 1, VotingPercentage = 33.33 };
            var initialCounter = new CounterStatistics { VoteCount = 1, VotingPercentage = 33.33 };
            var counters = new List<CounterStatistics> { modiCounter, oppositionCounter, initialCounter };

            //Act
            _counterManager.ResolveExcess(counters);

            //Assert
            expectedResult.Should().Be(modiCounter.VotingPercentage);
            expectedResult.Should().Be(oppositionCounter.VotingPercentage);
            expectedResult.Should().Be(initialCounter.VotingPercentage);
        }

        [Theory]
        [InlineData(66.66, 66.67, 33.33)]
        [InlineData(66.65, 66.67, 33.33)]
        public void Resolve_Access_Should_Add_Access_To_Max_Percentage_When_All_Counter_Are_Not_Equal(double initial, double expected, double lowest)
        {
            //Arrange
            var initialCounter = new CounterStatistics { VotingPercentage = initial };
            var oppositionCounter = new CounterStatistics { VotingPercentage = lowest };
            var counters = new List<CounterStatistics> { initialCounter, oppositionCounter };

            //Act
            _counterManager.ResolveExcess(counters);

            //Assert
            expected.Should().Be(initialCounter.VotingPercentage);
            lowest.Should().Be(oppositionCounter.VotingPercentage);
        }

        [Theory]
        [InlineData(11.11, 11.12, 44.44)]
        [InlineData(11.10, 11.12, 44.44)]
        public void Resolve_Access_Should_Add_Access_To_Min_Percentage_When_Have_More_Than_One_Highest_Counter(double initial, double expected, double highest)
        {
            //Arrange
            var modiCounter = new CounterStatistics { VotingPercentage = highest };
            var oppositionCounter = new CounterStatistics { VotingPercentage = highest };
            var initialCounter = new CounterStatistics { VotingPercentage = initial };
            var counters = new List<CounterStatistics> { modiCounter, oppositionCounter, initialCounter };

            //Act
            _counterManager.ResolveExcess(counters);

            //Assert
            highest.Should().Be(modiCounter.VotingPercentage);
            highest.Should().Be(oppositionCounter.VotingPercentage);
            expected.Should().Be(initialCounter.VotingPercentage);
        }

        [Fact]
        public void Resolve_Access_Should_Not_Add_Access_If_Total_Pecentage_Is_100()
        {
            //Arrange
            var expectedResult80 = 80d;
            var expectedResult20 = 20d;
            var counterWithVotingPercentage80 = new CounterStatistics { VoteCount = 4, VotingPercentage = 80 };
            var counterWithVotingPercentage20 = new CounterStatistics { VoteCount = 1, VotingPercentage = 20 };
            var counters = new List<CounterStatistics> { counterWithVotingPercentage80, counterWithVotingPercentage20 };

            //Act
            _counterManager.ResolveExcess(counters);

            //Assert
            expectedResult80.Should().Be(counterWithVotingPercentage80.VotingPercentage);
            expectedResult20.Should().Be(counterWithVotingPercentage20.VotingPercentage);
        }
        public static IEnumerable<object[]> TestData =>
         new[]
         {
            new object[] { new Fixture().Create<string>(), new Fixture().Create<int>() },
            new object[] { new Fixture().Create<string>(), new Fixture().Create<int>() }
         };
    }


}