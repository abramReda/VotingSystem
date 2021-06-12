using System.Threading.Tasks;
using VotingSystem.core;
using VotingSystem.core.Models;
using VotingSystem.Database;

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

        public async Task<VotingPoll> CreateVotingPollAsync(VotingPollCreationRequest request)
        {
            var poll = _factory.CreatePoll(request);
            await _presistance.SaveVotingPollAsync(poll);
            return poll;
        }
    }
}
