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
        public async Task CreateCandidate(Candidate candidate)
        {
           await _appDbContext.Candidates.AddAsync(candidate);
        }

        public async Task<Candidate> GetCandidateByEmail(string email)
        {
            return await _appDbContext.Candidates.FirstOrDefaultAsync(x=>x.Email == email);
        }

        //public async Task<Candidate> GetCandidateById(int id)
        //{
        //    return await _appDbContext.Candidates.FirstOrDefaultAsync(x => x.Id == id);
        //}

        public void UpdateCandidate(Candidate candidate)
        {
            _appDbContext.Candidates.Update(candidate);
        }
        public async Task<int> SaveChanges()
        {
           return  await _appDbContext.SaveChangesAsync();
        }
    }
}
