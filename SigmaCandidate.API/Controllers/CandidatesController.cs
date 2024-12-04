using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SigmaCandidate.Model;

namespace SigmaCandidate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        public CandidatesController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> UpsertCandidate([FromBody] Candidate candidate)
        {
            //here goes data validation and/or guard clauses as required.
            
            return Ok(candidate);
        }
    }
}
