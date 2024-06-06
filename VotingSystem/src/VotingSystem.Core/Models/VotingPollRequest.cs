namespace VotingSystem.Core.Models
{
    public class VotingPollRequest
    {
        public string Title { get; init; }
        public string Description { get; init; }
        public string[] Names { get; init; }
    }
}
