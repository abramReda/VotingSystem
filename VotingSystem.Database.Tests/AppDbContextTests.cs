using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using VotingSystem.core.Models;
using VotingSystem.Database.Tests.InferaStructure;
using Xunit;

namespace VotingSystem.Database.Tests
{
    public class AppDbContextTests
    {
        [Fact]
        public async Task SaveCounterToDb()
        {
            var counter = new Counter() { Name = "Counter Name" };
            using (var ctx = AppContextFactory.CreateContext(nameof(SaveCounterToDb)))
            {
                ctx.Counters.Add(counter);
                ctx.SaveChanges();
            }

            using (var ctx = AppContextFactory.CreateContext(nameof(SaveCounterToDb)))
            {
                var storedCounter = await ctx.Counters.FirstAsync();

                Assert.Equal(counter.Name, storedCounter.Name);
            }
        }

        [Fact]
        public async Task SaveVotingPoll()
        {
            var poll = new VotingPoll
            {
                Title = "title for voting Poll",
                Description = "Description for voting Poll"
            };
            poll.Counters.Add(new Counter { Name = "counter 1" });
            poll.Counters.Add(new Counter { Name = "counter 2" });
            poll.Counters.Add(new Counter { Name = "counter 3" });

            using (var ctx = AppContextFactory.CreateContext(nameof(SaveCounterToDb)))
            {
                ctx.VotingPolls.Add(poll);
                ctx.SaveChanges();
            }

            using (var ctx = AppContextFactory.CreateContext(nameof(SaveCounterToDb)))
            {
                var savedPoll = await ctx.VotingPolls.Include(p=>p.Counters).SingleAsync();
                Assert.Equal(savedPoll.Title, poll.Title);
                Assert.Equal(savedPoll.Description, poll.Description);
                Assert.Equal(poll.Counters.Select(x => x.Name), savedPoll.Counters.Select(x => x.Name));
            }

        }


        
    }
}
