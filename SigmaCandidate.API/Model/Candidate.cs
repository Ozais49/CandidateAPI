using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigmaCandidate.Model
{
    public class Candidate
    {
        //[Key]
        //public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Key]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }
        

        public string? CallTimeInterval { get; set; }

        //[Url]
        public string? LinkedInUrl { get; set; }

        //[Url]
        public string? GitHubUrl { get; set; }

        public string Comment { get; set; }
    }
}
