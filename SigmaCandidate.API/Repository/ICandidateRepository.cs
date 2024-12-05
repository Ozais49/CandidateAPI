using SigmaCandidate.Model;

namespace SigmaCandidate.Repository
{
    public interface ICandidateRepository
    {
        Task<Candidate> GetCandidateById(int id);
        Task<Candidate> GetCandidateByEmail(string email);
        Task CreateCandidate(Candidate candidate);
        void UpdateCandidate(Candidate candidate);
        Task<int> SaveChanges();

    }
}
