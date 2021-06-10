using System.Collections;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.core.Models;
using Xunit;

namespace VotingSystem.core.tests
{
    public class VotingPollTests
    {
        [Fact]
        public void ZeroCounterWhenCreated()
        {
            var poll = new VotingPoll();
            Assert.Empty(poll.Counters);
        }
    }
}
