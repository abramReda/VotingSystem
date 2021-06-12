using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.core;
using VotingSystem.core.Models;
using VotingSystem.Database;
using Xunit;

namespace VotingSystem.Application.Tests
{
    public class VotingInteractorTests
    {
        private readonly Vote _vote = new Vote() { UserId = "user Guid",CounterId = 1};
        private readonly Mock<IVotingSystemPresistance> _mockPresistance = new Mock<IVotingSystemPresistance>();
        private readonly VotingInteractor _interactor;
        public VotingInteractorTests()
        {
            _interactor = new VotingInteractor(_mockPresistance.Object);
        }
        [Fact]
        public async Task Vote_PersistsVoteForFirstTime()
        {
            _mockPresistance.Setup(v => v.IsVoteExistAsync(_vote)).Returns(Task.FromResult(false));

            await _interactor.Vote(_vote);
            
            _mockPresistance.Verify(c => c.SaveVoteAsync(_vote));
        }

        

        [Fact]
        public async Task Vote_DoesNotPersistsVoteWhenUserHasVotedAlredy()
        {
            _mockPresistance.Setup(v => v.IsVoteExistAsync(_vote)).Returns(Task.FromResult(true));
           
            await _interactor.Vote(_vote);

            _mockPresistance.Verify(c => c.SaveVoteAsync(_vote),Times.Never);
        }

    }

    public class VotingInteractor
    {
        private readonly IVotingSystemPresistance _presistance;

        public VotingInteractor(IVotingSystemPresistance Presistance)
        {
            _presistance = Presistance;
        }

        public async Task Vote(Vote vote)
        {
            if(!await _presistance.IsVoteExistAsync(vote))
               await _presistance.SaveVoteAsync(vote);
        }
    }
}