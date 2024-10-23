using HRRecruitmentSystem.Data;
using HRRecruitmentSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitmentSystem.Services
{
    public class RecruitmentService
    {
        private readonly RecruitmentDbContext _context;

        public RecruitmentService(RecruitmentDbContext context)
        {
            _context = context;
        }

        public void AddVacancy(Vacancy vacancy)
        {
            if (_context.Vacancies.Any(v => v.Id == vacancy.Id))
            {
                throw new InvalidOperationException("Вакансия с таким ID уже существует.");
            }
            _context.Vacancies.Add(vacancy);
            _context.SaveChanges();
        }

        public void SubmitResume(int vacancyId, Candidate candidate)
        {
            var vacancy = _context.Vacancies.Include(v => v.Candidates).FirstOrDefault(v => v.Id == vacancyId);
            if (vacancy == null)
            {
                throw new InvalidOperationException("Вакансия не найдена.");
            }

            vacancy.Candidates.Add(candidate);
            _context.Candidates.Add(candidate);
            _context.SaveChanges();
        }

        public void ConductInterview(int candidateId)
        {
            // Логика проведения собеседования
        }

        public void ReviewTestResults(int candidateId, bool isPassed)
        {
            var candidate = _context.Candidates.Find(candidateId);
            if (candidate == null)
            {
                throw new InvalidOperationException("Кандидат не найден.");
            }

            candidate.IsTestCompleted = isPassed;
            _context.SaveChanges();
        }
    }
}
