using System.ComponentModel.DataAnnotations;

namespace HRRecruitmentSystem.Models
{
    public class Candidate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsTestCompleted { get; set; }
        public bool IsHired { get; set; }

        public ICollection<Vacancy> Vacancies { get; set; } = new List<Vacancy>();
    }
}
