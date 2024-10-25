using HRRecruitmentSystem.Data;
using HRRecruitmentSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitmentSystem.Tests
{
    using HRRecruitmentSystem.Data;
    using HRRecruitmentSystem.Services;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class LogServiceTests
    {
        private RecruitmentDbContext _context;
        private LogService _logService;

        public LogServiceTests()
        {
            var options = new DbContextOptionsBuilder<RecruitmentDbContext>()
                .UseInMemoryDatabase(databaseName: "LogTestDatabase")
                .Options;

            _context = new RecruitmentDbContext(options);
            _logService = new LogService(_context);
        }

        [Fact]
        public async Task LogAsync_ShouldAddLogEntry()
        {
            await _logService.LogAsync("Information", "Test log entry", null);

            var logEntry = _context.Logs.FirstOrDefault();
            Assert.NotNull(logEntry);
            Assert.Equal("Test log entry", logEntry.Message);
        }

        [Fact]
        public async Task LogAsync_ShouldStoreCorrectLevel()
        {
            await _logService.LogAsync("Error", "Error log entry", "Test exception");

            var logEntry = _context.Logs.FirstOrDefault();
            Assert.NotNull(logEntry);
            Assert.Equal("Error", logEntry.Level);
        }
    }
}
