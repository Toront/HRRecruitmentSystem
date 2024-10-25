using HRRecruitmentSystem.Models;
using HRRecruitmentSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HRRecruitmentSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecruitmentController : ControllerBase
    {
        private readonly RecruitmentService _recruitmentService;
        private readonly ILogger<RecruitmentController> _logger;

        public RecruitmentController(RecruitmentService recruitmentService, ILogger<RecruitmentController> logger)
        {
            _recruitmentService = recruitmentService;
            _logger = logger;
        }

        /// <summary>
        /// Добавляет новую вакансию в систему.
        /// </summary>
        /// <param name="vacancy">Детали вакансии.</param>
        /// <returns>Возвращает созданную вакансию.</returns>
        /// <response code="201">Вакансия успешно добавлена.</response>
        /// <response code="400">Ошибка при добавлении вакансии.</response>
        [HttpPost("vacancy")]
        public IActionResult AddVacancy([FromBody] Vacancy vacancy)
        {
            try
            {
                _recruitmentService.AddVacancy(vacancy);
                _logger.LogInformation("Вакансия '{Title}' успешно добавлена.", vacancy.Title);
                return CreatedAtAction(nameof(AddVacancy), vacancy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при добавлении вакансии '{Title}'.", vacancy.Title);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Позволяет кандидату подать резюме на вакансию.
        /// </summary>
        /// <param name="vacancyId">ID вакансии, на которую подается резюме.</param>
        /// <param name="candidate">Детали кандидата.</param>
        /// <returns>Возвращает статус операции.</returns>
        /// <response code="200">Резюме успешно подано.</response>
        /// <response code="400">Ошибка при подаче резюме.</response>
        [HttpPost("resume/{vacancyId}")]
        public IActionResult SubmitResume(int vacancyId, [FromBody] Candidate candidate)
        {
            try
            {
                _recruitmentService.SubmitResume(vacancyId, candidate);
                _logger.LogInformation("Кандидат '{Name}' подал резюме на вакансию ID: {VacancyId}.", candidate.Name, vacancyId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при подаче резюме кандидата '{Name}' на вакансию ID: {VacancyId}.", candidate.Name, vacancyId);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Проводит собеседование с кандидатом.
        /// </summary>
        /// <param name="candidateId">ID кандидата, с которым проводится собеседование.</param>
        /// <returns>Возвращает статус операции.</returns>
        /// <response code="200">Собеседование успешно проведено.</response>
        /// <response code="400">Ошибка при проведении собеседования.</response>
        [HttpPost("interview/{candidateId}")]
        public IActionResult ConductInterview(int candidateId)
        {
            try
            {
                _recruitmentService.ConductInterview(candidateId);
                _logger.LogInformation("Собеседование с кандидатом ID: {CandidateId} проведено.", candidateId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проведении собеседования с кандидатом ID: {CandidateId}.", candidateId);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Проверяет результаты тестирования кандидата.
        /// </summary>
        /// <param name="candidateId">ID кандидата.</param>
        /// <param name="isPassed">Флаг, указывающий, прошел ли кандидат тест.</param>
        /// <returns>Возвращает статус операции.</returns>
        /// <response code="200">Результаты теста успешно проверены.</response>
        /// <response code="400">Ошибка при проверке результатов теста.</response>
        [HttpPost("review/{candidateId}")]
        public IActionResult ReviewTestResults(int candidateId, bool isPassed)
        {
            try
            {
                _recruitmentService.ReviewTestResults(candidateId, isPassed);
                _logger.LogInformation("Результаты теста для кандидата ID: {CandidateId}: {Result}.", candidateId, isPassed ? "Успешно" : "Не удалось");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при проверке результатов теста для кандидата ID: {CandidateId}.", candidateId);
                return BadRequest(ex.Message);
            }
        }
    }
}
