namespace VotingSystem.Core.Models
{
    public class VotingPoll
    {
        public VotingPoll()
        {
            Counters = Enumerable.Empty<VotingCounter>();
        }
        public IEnumerable<VotingCounter> Counters { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
