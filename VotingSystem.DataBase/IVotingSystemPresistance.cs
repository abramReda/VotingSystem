using System.Threading.Tasks;
using VotingSystem.core.Models;

namespace VotingSystem.Database
{
    public interface IVotingSystemPresistance
    {
        Task SaveVotingPollAsync(VotingPoll poll);
        Task SaveVoteAsync(Vote vote);
        Task<bool> IsVoteExistAsync(Vote vote);
    }
}
