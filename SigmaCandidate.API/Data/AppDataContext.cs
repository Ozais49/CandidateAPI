﻿using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Model;

namespace SigmaCandidate.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base (options)
        {
            
        }
        public DbSet<Candidate> Candidates { get; set; }
    }
}
