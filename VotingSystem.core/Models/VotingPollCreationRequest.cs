namespace VotingSystem.core.Models
{
    public class VotingPollCreationRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string[] CounterNames { get; set; }
    }
}
