using HRRecruitmentSystem.Data;
using HRRecruitmentSystem.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace HRRecruitmentSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            try
            {
                logger.Debug("Initiating application...");

                var builder = WebApplication.CreateBuilder(args);

                // ��������� ��������� ���� ������ � SQLite
                builder.Services.AddDbContext<RecruitmentDbContext>(options =>
                    options.UseSqlite("Data Source=recruitment.db"));

                builder.Services.AddControllers();
                builder.Services.AddEndpointsApiExplorer();

                // ��������� Swagger
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Title = "HR Recruitment System API",
                        Version = "v1",
                        Description = "API ��� ���������� ��������� ������� ������ � HR Recruitment System.",
                        Contact = new OpenApiContact
                        {
                            Name = "Frolov Sergei",
                            Url = new Uri("https://github.com/Toront"),
                            Email = "email@example.com"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT License",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                    // ���������Assembly � �������� ���� � XML-������������
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });

                // ����������� ��������
                builder.Services.AddScoped<RecruitmentService>();
                builder.Services.AddTransient<LogService>();

                var app = builder.Build();

                // ��������� HTTP-���������
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
                    dbContext.Database.EnsureCreated();
                    dbContext.Database.Migrate();
                }

                app.Run();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Application stopped because of exception.");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
        }
    }
}