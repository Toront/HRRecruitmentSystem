using HRRecruitmentSystem.Data;
using HRRecruitmentSystem.Models;

namespace HRRecruitmentSystem.Services
{
    public class LogService
    {
        private readonly RecruitmentDbContext _context;

        public LogService(RecruitmentDbContext context)
        {
            _context = context;
        }

        public async Task LogAsync(string level, string message, string exception)
        {
            var logEntry = new LogEntry
            {
                Date = DateTime.Now,
                Level = level,
                Message = message,
                Exception = exception
            };

            await _context.Logs.AddAsync(logEntry);
            await _context.SaveChangesAsync();
        }
    }
}
