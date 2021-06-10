using System;
using System.Linq;
using VotingSystem.core.Models;
using Xunit;

namespace VotingSystem.core.tests
{
    public class VotingPollFactoryTest
    {
        private readonly VotingPollFactory _factory = new VotingPollFactory();
        private readonly VotingPollCreationRequest _request = new VotingPollCreationRequest
        {
            Title = "poll title",
            Description= "description for the poll",
            CounterNames = new[] { "name of counter 1", "name of counter 2" }
        };
        [Fact]
        public void create_ThrowswhenLessThanTowCounterName()
        {
            _request.CounterNames = new string[] { "single name" };
            Assert.Throws<ArgumentException>(() => _factory.CreatePoll(_request));
            _request.CounterNames = new string[] { };
            Assert.Throws<ArgumentException>(() => _factory.CreatePoll(_request));

        }

        [Fact]
        public void create_AddCounterToPollForeachName()
        {
            VotingPoll poll = _factory.CreatePoll(_request);
            var createdCounterNamees = poll.Counters.Select(c => c.Name);
            foreach (var name in _request.CounterNames)
                Assert.Contains(name,createdCounterNamees);
        }
        [Fact]
        public void create_AddTitleToPoll()
        {
            VotingPoll poll = _factory.CreatePoll(_request);
            Assert.Equal(_request.Title, poll.Title);
        }
        [Fact]
        public void Create_AddDescriptionToPoll()
        {
            VotingPoll poll = _factory.CreatePoll(_request);
            Assert.Equal(_request.Description, poll.Description);
        }
    }
}
