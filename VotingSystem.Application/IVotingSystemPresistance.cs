using VotingSystem.core.Models;

namespace VotingSystem.Application
{
    public interface IVotingSystemPresistance
    {
        void SaveVotingPoll(VotingPoll poll);
    }
}
