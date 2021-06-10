using VotingSystem.core;
using VotingSystem.core.Models;

namespace VotingSystem.Application
{
    public class VotingPollInteractor
    {
        private readonly IVotingPollFactory _factory;
        private readonly IVotingSystemPresistance _presistance;

        public VotingPollInteractor(IVotingPollFactory factory, IVotingSystemPresistance presistance)
        {
            _factory = factory;
            _presistance = presistance;
        }

        public VotingPoll CreateVotingPoll(VotingPollCreationRequest request)
        {
            var poll = _factory.CreatePoll(request);
            _presistance.SaveVotingPoll(poll);
            return poll;
        }
    }
}
