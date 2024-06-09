using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using VotingSystem.Application;
using VotingSystem.Application.Contracts;
using VotingSystem.Core;
using VotingSystem.Core.Contracts;
using VotingSystem.Core.Models;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoteController : ControllerBase
    {
        private readonly IVotingSystemPersistance _persistance;
        private readonly IVotingInteractor _votingInteractor;
        
        public VoteController(IVotingSystemPersistance persistance, IVotingInteractor votingInteractor)
        {
            _persistance = persistance;
            _votingInteractor = votingInteractor;
        }

        [HttpPost]
        public async Task<IActionResult> AddVote(VoteRequest voteRequest)
        {
            Vote vote = new Vote() { CounterId = voteRequest.CounterId, UserId = voteRequest.UserId, PollId = voteRequest.PollId };
            var result = await _votingInteractor.Vote(vote);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


    }
}
