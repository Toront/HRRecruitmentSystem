using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace HRRecruitmentSystem.Models
{
    public class Vacancy
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }

        [InverseProperty("Vacancies")]
        public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
    }
}
