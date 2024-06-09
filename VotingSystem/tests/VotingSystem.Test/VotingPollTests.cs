namespace VotingSystem.Core.Test
{
    public class VotingPollTests
    {
        [Fact]
        public void Should_Zero_Counter_On_Initial()
        {
            //Act
            var poll = new VotingPoll();

            //Assert
            poll.Counters.Should().BeEmpty();
        }
    }
}
