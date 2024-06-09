using System.ComponentModel.DataAnnotations;

namespace VotingSystem.Core.Models
{
    public class VotingPollRequest
    {
        [Required(ErrorMessage = "Please provide title.")]
        public string Title { get; init; }
        [Required(ErrorMessage = "Please provide description.")]
        public string Description { get; init; }
        [Required(ErrorMessage = "Please provide voting option names.")]
        public string[] Names { get; init; }
    }
}
