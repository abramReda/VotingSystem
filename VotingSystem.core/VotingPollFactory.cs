using System;
using VotingSystem.core.Models;

namespace VotingSystem.core
{
    public class VotingPollFactory : IVotingPollFactory
    {
       
        public VotingPoll CreatePoll(VotingPollCreationRequest request)
        {
            if(request.CounterNames.Length<2)throw new ArgumentException();

            var poll = new VotingPoll() {
                Title = request.Title,
                Description = request.Description
            };
            foreach (var name in request.CounterNames)
                poll.Counters.Add(new Counter { Name = name, Count = 0 });
            return poll;
        }
    }
}
