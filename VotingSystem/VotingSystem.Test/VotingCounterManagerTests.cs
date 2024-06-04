namespace VotingSystem.Test
{
    public class VotingCounterManagerTests
    {
        public readonly VotingCounter _votingCounter;
        public readonly CounterManager _counterManager;
        public VotingCounterManagerTests()
        {
            _votingCounter = new VotingCounter();
            _counterManager = new CounterManager();
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
        [InlineData(2, 3, 66.67)]
        public void GetVotingPercentage_Should_Return_Percentage_Of_Voting_Option_Count_Based_On_Total_Count(int count, int totalCount, double expectedPercentage)
        {
            //Arrange
            _votingCounter.VoteCount = count;

            //Act
            var result = _counterManager.GetVotingPercentage(_votingCounter, totalCount);

            //Assert
            result.VoteCount.Should().Be(count).And.BePositive();
            result.VotingPercentage.Should().Be(expectedPercentage).And.BePositive();
        }

        [Fact]
        public void Resolve_Access_Should_Not_Add_Access_When_All_Counter_Are_Equal()
        {
            //Arrange
            var expectedResult = 33.33;
            var counter1 = new VotingCounter { VoteCount = 1, VotingPercentage = 33.33 };
            var counter2 = new VotingCounter { VoteCount = 1, VotingPercentage = 33.33 };
            var counter3 = new VotingCounter { VoteCount = 1, VotingPercentage = 33.33 };
            var counters = new List<VotingCounter> { counter1, counter2, counter3 };

            //Act
            _counterManager.ResolveAccess(counters);

            //Assert
            expectedResult.Should().Be(counter1.VotingPercentage);
            expectedResult.Should().Be(counter2.VotingPercentage);
            expectedResult.Should().Be(counter3.VotingPercentage);
        }

        [Theory]
        [InlineData(66.66, 66.67, 33.33)]
        [InlineData(66.65, 66.67, 33.33)]
        public void Resolve_Access_Should_Add_Access_To_Max_Percentage_When_All_Counter_Are_Not_Equal(double initial, double expected, double lowest)
        {
            //Arrange
            var counter1 = new VotingCounter { VotingPercentage = initial };
            var counter2 = new VotingCounter { VotingPercentage = lowest };
            var counters = new List<VotingCounter> { counter1, counter2 };

            //Act
            _counterManager.ResolveAccess(counters);

            //Assert
            expected.Should().Be(counter1.VotingPercentage);
            lowest.Should().Be(counter2.VotingPercentage);
        }

        [Theory]
        [InlineData(11.11, 11.12, 44.44)]
        [InlineData(11.10, 11.12, 44.44)]
        public void Resolve_Access_Should_Add_Access_To_Min_Percentage_When_Have_More_Than_One_Highest_Counter(double initial, double expected, double highest)
        {
            //Arrange
            var counter1 = new VotingCounter { VotingPercentage = highest };
            var counter2 = new VotingCounter { VotingPercentage = highest };
            var counter3 = new VotingCounter { VotingPercentage = initial };
            var counters = new List<VotingCounter> { counter1, counter2, counter3 };

            //Act
            _counterManager.ResolveAccess(counters);

            //Assert
            highest.Should().Be(counter1.VotingPercentage);
            highest.Should().Be(counter2.VotingPercentage);
            expected.Should().Be(counter3.VotingPercentage);
        }

        [Fact]
        public void Resolve_Access_Should_Not_Add_Access_If_Total_Pecentage_Is_100()
        {
            //Arrange
            var expectedResult1 = 80d;
            var expectedResult2 = 20d;
            var counter1 = new VotingCounter { VoteCount = 4, VotingPercentage = 80 };
            var counter2 = new VotingCounter { VoteCount = 1, VotingPercentage = 20 };
            var counters = new List<VotingCounter> { counter1, counter2 };

            //Act
            _counterManager.ResolveAccess(counters);

            //Assert
            expectedResult1.Should().Be(counter1.VotingPercentage);
            expectedResult2.Should().Be(counter2.VotingPercentage);
        }

        public static readonly IEnumerable<object[]> TestData = new[]
        {
            new object[] {"Yes", 5},
            new object[] {"No", 5}
        };
    }

    
}