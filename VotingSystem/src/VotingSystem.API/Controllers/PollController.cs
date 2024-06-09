using Microsoft.AspNetCore.Mvc;
using VotingSystem.Application;
using VotingSystem.Core;
using VotingSystem.Core.Contracts;
using VotingSystem.Core.Models;

namespace VotingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PollController : ControllerBase
    {
        private readonly IVotingSystemPersistance _persistance;
        private readonly IVotingPollFactory _pollFactory;
        private readonly IVotingCounterManager _votingCounterManager;
        private readonly IStatisticsInteractor _statisticsInteractor;
        public PollStatistics Statistics { get; set; }
        public PollController(IVotingPollFactory pollFactory, IVotingSystemPersistance persistance, IVotingCounterManager votingCounterManager, IStatisticsInteractor statisticsInteractor)
        {
            _pollFactory = pollFactory;
            _persistance = persistance;
            _votingCounterManager = votingCounterManager;
            _statisticsInteractor = statisticsInteractor;
        }

        [HttpPost]
        public async Task<IActionResult> Create(VotingPollRequest request)
        {
            VotingPoll votingPoll = _pollFactory.Create(request);
            var result = await _persistance.SaveVotingPollAsync(votingPoll);
            if(result!=null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetVotingPoll(string id)
        {
            var result = await _persistance.GetPollAsync(id);
            if(result == null)
            {
                return BadRequest("No poll found.");
            }
            return Ok(result);
        }

        [HttpGet("/GetStatistics/{pollId:length(24)}")]
        public async Task<IActionResult> GetStatistics(string pollId)
        {
            Statistics = await _statisticsInteractor.GetStatistics(pollId);
            if (Statistics == null)
            {
                return BadRequest("No statistics found.");
            }
            return Ok(Statistics);
        }
    }
}
