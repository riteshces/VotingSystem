using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Core.Models;

namespace VotingSystem.Application.Contracts
{
    public interface IVotingInteractor
    {
        Task<Vote> Vote(Vote vote);
    }
}
