using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Model;

namespace SigmaCandidate.Data
{
    public class AppDataContext:DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options):base (options)
        {
            
        }
        public DbSet<Candidate> Candidates { get; set; }
    }
}
