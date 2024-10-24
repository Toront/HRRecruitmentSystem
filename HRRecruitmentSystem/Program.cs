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

            // ��������� ��������� ���� ������ � SQLite
            builder.Services.AddDbContext<RecruitmentDbContext>(options =>
                options.UseSqlite("Data Source=recruitment.db"));

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // ����������� ������� ������� ������
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

            // �������� ���� ������ � ���������� �������� ��� ������ �������
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<RecruitmentDbContext>();
                dbContext.Database.EnsureCreated(); // �������� ���� ������, ���� ��� ��� �� ����������
                // ��������� �������� ��� ������ ����������
                dbContext.Database.Migrate();
            }

            app.Run();
        }
    }
}