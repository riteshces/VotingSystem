namespace VotingSystem.Core.Models
{
    public class VotingPollRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] Names { get; set; }
    }
}
