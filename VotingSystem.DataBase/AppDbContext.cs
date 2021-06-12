using Microsoft.EntityFrameworkCore;
using VotingSystem.core.Models;

namespace VotingSystem.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Counter> Counters { get; set; }
        public DbSet<VotingPoll> VotingPolls { get; set; }
    }
}
