using HRRecruitmentSystem.Data;
using HRRecruitmentSystem.Services;
using Microsoft.EntityFrameworkCore;

namespace HRRecruitmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Настройка контекста базы данных с SQLite
            builder.Services.AddDbContext<RecruitmentDbContext>(options =>
                options.UseSqlite("Data Source=recruitment.db"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Регистрация сервиса подбора кадров
            builder.Services.AddScoped<RecruitmentService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            // Создание базы данных и применение миграций при первом запуске
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RecruitmentDbContext>();
                dbContext.Database.EnsureCreated(); // Создание базы данных, если она ещё не существует
                // Применяем миграции при старте приложения
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}