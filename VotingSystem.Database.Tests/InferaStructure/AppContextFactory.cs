using Microsoft.EntityFrameworkCore;

namespace VotingSystem.Database.Tests.InferaStructure
{
    public class AppContextFactory
    {
        public  static AppDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                          .UseInMemoryDatabase(dbName)
                          .Options;

            return new AppDbContext(options);
        }
    }
}
