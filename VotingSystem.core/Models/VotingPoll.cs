using System.Collections.Generic;

namespace VotingSystem.core.Models
{
    public class VotingPoll
    {
        public int Id { get; set; }

        public List<Counter> Counters { get; }
        public string Title { get; set; }
        public string Description { get; set; }
        public VotingPoll()
        {
            Counters = new List<Counter>();
        }

    }
}
