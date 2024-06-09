using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Database.Tests.Fixtures
{
    public static class VotingPollFixture
    {
        public static VotingPoll GetVotingPoll()
        {
            Fixture fixture = new Fixture();
            return new VotingPoll
            {
                Description = fixture.Create<string>().Replace("-", ""),
                Title = fixture.Create<string>().Replace("-", ""),
                //Counters = VotingCounterFixture.GetVotingCounters()
            };
        }
    }
}
