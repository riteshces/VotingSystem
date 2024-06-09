namespace VotingSystem.Core
{
    public class CounterStatistics
    {
        public string Id { get; set; }
        public string VotingOptionName { get; set; }
        public int VoteCount { get; set; }
        public double VotingPercentage { get; set; }
    }
}
