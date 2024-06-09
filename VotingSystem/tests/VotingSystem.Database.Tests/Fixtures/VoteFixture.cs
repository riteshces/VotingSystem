using AutoFixture;
using VotingSystem.Core.Models;

namespace VotingSystem.Database.Tests.Fixtures
{
    public class VoteFixture
    {
        public static Vote GetVotes()
        {
            Fixture fixture = new Fixture();
            return new Vote
            {
                UserId = fixture.Create<string>().Replace("-",""),
                CounterId = fixture.Create<string>().Replace("-", "").Substring(0, 24)
            };
        }
    }
}
