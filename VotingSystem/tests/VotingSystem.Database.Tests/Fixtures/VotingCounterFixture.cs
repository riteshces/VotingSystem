using AutoFixture;
using VotingSystem.Core.Models;

namespace VotingSystem.Database.Tests.Fixtures
{
    public static class VotingCounterFixture
    {
        public static VotingCounter GetVotingCounter()
        {
            Fixture fixture = new Fixture();
            return new VotingCounter
            {
                VoteCount = fixture.Create<int>(),
                VotingOptionName = fixture.Create<string>().Replace("-", ""),
                VotingPercentage = fixture.Create<int>(),
            };
        }

        public static IEnumerable<VotingCounter> GetVotingCounters()
        {
            Fixture fixture = new Fixture();
            IEnumerable<VotingCounter> votingCounters = fixture.CreateMany<VotingCounter>(10).ToList();
            return votingCounters;
        }
    }
}
