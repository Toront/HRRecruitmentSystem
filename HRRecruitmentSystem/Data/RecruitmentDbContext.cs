using HRRecruitmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitmentSystem.Data
{
    public class RecruitmentDbContext : DbContext
    {
        public RecruitmentDbContext(DbContextOptions<RecruitmentDbContext> options) : base(options) { }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<HR> HrSpecialists { get; set; }
    }
}
