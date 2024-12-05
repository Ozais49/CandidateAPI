using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Data;
using SigmaCandidate.Model;

namespace SigmaCandidate.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _appDbContext;
        public CandidateRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CreateCandidateAsync(Candidate candidate)
        {
            await _appDbContext.Candidates.AddAsync(candidate);
        }



        public async Task<Candidate> GetCandidateByEmailAsync(string email)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _appDbContext.Candidates
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Email == email);
#pragma warning restore CS8603 // Possible null reference return.
        }

        //public async Task<Candidate> GetCandidateById(int id)
        //{
        //    return await _appDbContext.Candidates.FirstOrDefaultAsync(x => x.Id == id);
        //}

        public void UpdateCandidate(Candidate candidate)
        {
            _appDbContext.Candidates.Update(candidate);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
