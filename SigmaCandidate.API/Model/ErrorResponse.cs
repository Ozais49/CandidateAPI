namespace SigmaCandidate.Model
{
    public class ErrorResponse
    {
        public ErrorResponse()
        {
            Errors = new();
        }
        public List<string> Errors { get; set; }

    }
}
