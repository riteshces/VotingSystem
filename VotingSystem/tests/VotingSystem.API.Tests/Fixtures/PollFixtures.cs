using AutoFixture;

namespace VotingSystem.API.Tests.Fixtures
{
    public static class PollFixtures
    {
        public static VotingPollRequest GetVotingPollRequest()
        {
            Fixture fixture = new Fixture();
            return fixture.Create<VotingPollRequest>();
        }

        public static VotingPoll GetVotingPoll()
        {
            Fixture fixture = new Fixture();
            return fixture.Create<VotingPoll>();
        }
    }
}
