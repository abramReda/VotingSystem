using VotingSystem.core.Models;

namespace VotingSystem.core
{
    public interface IVotingPollFactory
    {
        VotingPoll CreatePoll(VotingPollCreationRequest request);
    }
}
