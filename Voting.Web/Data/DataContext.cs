namespace Voting.Web.Data
{
    using Microsoft.EntityFrameworkCore;
    using Entities;

    public class DataContext : DbContext
    {        
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
    }
}
