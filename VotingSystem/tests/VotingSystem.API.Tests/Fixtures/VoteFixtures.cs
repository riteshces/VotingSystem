using AutoFixture;

namespace VotingSystem.API.Tests.Fixtures
{
    public static class VoteFixtures
    {
        public static VoteRequest GetVoteRequest()
        {
            Fixture fixture = new Fixture();
            return fixture.Create<VoteRequest>();
        }

        public static Vote GetVote()
        {
            Fixture fixture = new Fixture();
            return fixture.Create<Vote>();
        }
    }
}
