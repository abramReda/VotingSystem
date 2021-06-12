using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.core.Models;
using VotingSystem.Database.Tests.InferaStructure;
using Xunit;

namespace VotingSystem.Database.Tests
{
    public class VotingSystemPersistanceTests
    {
        [Fact]
        public async Task presistVotingPoll()
        {

            var poll = new VotingPoll
            {
                Title = "title for voting Poll",
                Description = "Description for voting Poll"
            };
            poll.Counters.Add(new Counter { Name = "counter 1" });
            poll.Counters.Add(new Counter { Name = "counter 2" });
            poll.Counters.Add(new Counter { Name = "counter 3" });

            await using (var ctx = AppContextFactory.CreateContext(nameof(presistVotingPoll)))
            {
                IVotingSystemPresistance presistance = new VotingSystemPresistance(ctx);
                await presistance.SaveVotingPollAsync(poll);
            }
            using (var ctx = AppContextFactory.CreateContext(nameof(presistVotingPoll)))
            {
                var savedPoll = await ctx.VotingPolls
                    .Include(p=>p.Counters)
                    .SingleAsync();
                Assert.Equal(savedPoll.Title, poll.Title);
                Assert.Equal(savedPoll.Description, poll.Description);
                Assert.Equal(poll.Counters.Select(x => x.Name), savedPoll.Counters.Select(x => x.Name));
            }

        }

        [Fact]
        public async Task PersistVote()
        {
            var vote = new Vote() { UserId = "Guid for user", CounterId = 1 };
           await using (var ctx = AppContextFactory.CreateContext(nameof(PersistVote)))
            {
                var votingSystem = new VotingSystemPresistance(ctx);
                await votingSystem.SaveVoteAsync(vote);
            }

            using(var ctx = AppContextFactory.CreateContext(nameof(PersistVote)))
            {
                var savedVote = ctx.Votes.Single();
                Assert.Equal(savedVote.UserId, vote.UserId);
                Assert.Equal(savedVote.CounterId, vote.CounterId);
            }
        }

        [Fact]
        public async Task IsVoteExist_ReturnFasleIfVoteNotExist()
        {
            var vote = new Vote() { CounterId = 0, UserId = "guid for user" };
            await using (var ctx = AppContextFactory.CreateContext(nameof(IsVoteExist_ReturnFasleIfVoteNotExist)))
            {
                var repo = new VotingSystemPresistance(ctx);
                Assert.False(await repo.IsVoteExistAsync(vote));
            }
        }

        [Fact]
        public async Task IsVoteExist_ReturnTrueIfVoteExist()
        {
            var vote = new Vote() { CounterId = 0, UserId = "guid for user" };
            await using (var ctx = AppContextFactory.CreateContext(nameof(IsVoteExist_ReturnTrueIfVoteExist)))
            {
                var repo = new VotingSystemPresistance(ctx);
                Assert.False(await repo.IsVoteExistAsync(vote));
                await repo.SaveVoteAsync(vote);
            }

            await using (var ctx = AppContextFactory.CreateContext(nameof(IsVoteExist_ReturnTrueIfVoteExist)))
            {
                var repo = new VotingSystemPresistance(ctx);
                Assert.True(await repo.IsVoteExistAsync(vote));
            }


        }
    }
}
