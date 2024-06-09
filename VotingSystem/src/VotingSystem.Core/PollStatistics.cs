using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Core
{
    public class PollStatistics
    {
        public string PollId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<CounterStatistics> Counters { get; set; }
    }
}
