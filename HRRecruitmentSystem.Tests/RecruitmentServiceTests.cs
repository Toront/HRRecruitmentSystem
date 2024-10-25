using HRRecruitmentSystem.Data;
using HRRecruitmentSystem.Models;
using HRRecruitmentSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitmentSystem.Tests;

public class RecruitmentServiceTests
{
    private RecruitmentDbContext _context;
    private RecruitmentService _service;

    public RecruitmentServiceTests()
    {
        var options = new DbContextOptionsBuilder<RecruitmentDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new RecruitmentDbContext(options);
        _service = new RecruitmentService(_context);
    }

    [Fact]
    public void AddVacancy_ShouldAddVacancy()
    {
        var vacancy = new Vacancy { Id = 1, Title = "Software Engineer", Department = "IT" };
        _service.AddVacancy(vacancy);

        var result = _context.Vacancies.SingleOrDefault(v => v.Id == 1);
        Assert.NotNull(result);
        Assert.Equal("Software Engineer", result.Title);
    }

    [Fact]
    public void SubmitResume_ShouldAddCandidateToVacancy()
    {
        var vacancy = new Vacancy { Title = "Software Engineer", Department = "IT" };
        _context.Vacancies.Add(vacancy);
        _context.SaveChanges();

        var candidate = new Candidate { Name = "John Doe", Email = "john.doe@example.com" };
        _service.SubmitResume(1, candidate);

        var result = _context.Vacancies.Include(v => v.Candidates).FirstOrDefault(v => v.Id == 1);
        Assert.Contains(candidate, result.Candidates);
    }
}