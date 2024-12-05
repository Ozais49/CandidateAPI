using SigmaCandidate.Model;

namespace SigmaCandidate.Repository
{
    public interface ICandidateRepository
    {
        //Task<Candidate> GetCandidateById(int id);
        Task<Candidate> GetCandidateByEmailAsync(string email);
        Task CreateCandidateAsync(Candidate candidate);
        void UpdateCandidate(Candidate candidate);
        Task<int> SaveChangesAsync();

    }
}
