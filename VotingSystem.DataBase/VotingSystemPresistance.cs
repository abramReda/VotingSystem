using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VotingSystem.core.Models;

namespace VotingSystem.Database
{
    public class VotingSystemPresistance : IVotingSystemPresistance
    {
        private AppDbContext _ctx;

        public VotingSystemPresistance(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> IsVoteExistAsync(Vote vote)
        {
            var v = await _ctx.Votes.FirstOrDefaultAsync(v=>v.UserId == vote.UserId && v.CounterId==v.CounterId);
            return v != null;
        }

        public async Task SaveVoteAsync(Vote vote)
        {
            _ctx.Votes.Add(vote);
            await _ctx.SaveChangesAsync();
        }

        public async Task SaveVotingPollAsync(VotingPoll poll)
        {
            _ctx.VotingPolls.Add(poll);
            await _ctx.SaveChangesAsync();
        }

       
    }
}
