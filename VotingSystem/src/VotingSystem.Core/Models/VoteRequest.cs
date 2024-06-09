using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotingSystem.Core.Models
{
    public class VoteRequest
    {
        [Required(ErrorMessage ="Please provide user id.")]
        [Length(1,24,ErrorMessage ="User id should not be empty.")]
        public string UserId { get; init; }

        [Required(ErrorMessage = "Please provide poll id.")]
        [Length(24, 24, ErrorMessage = "Please enter correct poll id.")]
        public string PollId { get; init; }

        [Required(ErrorMessage = "Please provide counter id.")]
        [Length(24, 24, ErrorMessage = "Please enter correct counter id.")]
        public string CounterId { get; init; }
    }
}
