using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaCandidate.Model;
using SigmaCandidate.Repository;

namespace SigmaCandidate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        ICandidateRepository _candidateRepository;
        public CandidatesController(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> UpsertCandidate([FromBody] Candidate candidate)
        {
            //here goes data validation and/or guard clauses as required.
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new ErrorResponse { Errors = errors });
            }
            try
            {
                var existingCandidate = await _candidateRepository.GetCandidateByEmailAsync(candidate.Email);
                //if candidate is not null update the candidate, else insert. 
                if (existingCandidate != null)
                {
                    _candidateRepository.UpdateCandidate(candidate);
                }
                else
                {
                    await _candidateRepository.CreateCandidateAsync(candidate);
                }
                await _candidateRepository.SaveChangesAsync();
                return Ok(candidate);
            }
            catch (Exception ex)
            {
                //To-DO Improvement: Add a logger here to log the exception and assign a tracking id.
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
