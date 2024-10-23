using System.ComponentModel.DataAnnotations;

namespace HRRecruitmentSystem.Models
{
    public class HR
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
