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
        /// ��������� ����� �������� � �������.
        /// </summary>
        /// <param name="vacancy">������ ��������.</param>
        /// <returns>���������� ��������� ��������.</returns>
        /// <response code="201">�������� ������� ���������.</response>
        /// <response code="400">������ ��� ���������� ��������.</response>
        [HttpPost("vacancy")]
        public IActionResult AddVacancy([FromBody] Vacancy vacancy)
        {
            try
            {
                _recruitmentService.AddVacancy(vacancy);
                _logger.LogInformation("�������� '{Title}' ������� ���������.", vacancy.Title);
                return CreatedAtAction(nameof(AddVacancy), vacancy);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� ���������� �������� '{Title}'.", vacancy.Title);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// ��������� ��������� ������ ������ �� ��������.
        /// </summary>
        /// <param name="vacancyId">ID ��������, �� ������� �������� ������.</param>
        /// <param name="candidate">������ ���������.</param>
        /// <returns>���������� ������ ��������.</returns>
        /// <response code="200">������ ������� ������.</response>
        /// <response code="400">������ ��� ������ ������.</response>
        [HttpPost("resume/{vacancyId}")]
        public IActionResult SubmitResume(int vacancyId, [FromBody] Candidate candidate)
        {
            try
            {
                _recruitmentService.SubmitResume(vacancyId, candidate);
                _logger.LogInformation("�������� '{Name}' ����� ������ �� �������� ID: {VacancyId}.", candidate.Name, vacancyId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� ������ ������ ��������� '{Name}' �� �������� ID: {VacancyId}.", candidate.Name, vacancyId);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// �������� ������������� � ����������.
        /// </summary>
        /// <param name="candidateId">ID ���������, � ������� ���������� �������������.</param>
        /// <returns>���������� ������ ��������.</returns>
        /// <response code="200">������������� ������� ���������.</response>
        /// <response code="400">������ ��� ���������� �������������.</response>
        [HttpPost("interview/{candidateId}")]
        public IActionResult ConductInterview(int candidateId)
        {
            try
            {
                _recruitmentService.ConductInterview(candidateId);
                _logger.LogInformation("������������� � ���������� ID: {CandidateId} ���������.", candidateId);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� ���������� ������������� � ���������� ID: {CandidateId}.", candidateId);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// ��������� ���������� ������������ ���������.
        /// </summary>
        /// <param name="candidateId">ID ���������.</param>
        /// <param name="isPassed">����, �����������, ������ �� �������� ����.</param>
        /// <returns>���������� ������ ��������.</returns>
        /// <response code="200">���������� ����� ������� ���������.</response>
        /// <response code="400">������ ��� �������� ����������� �����.</response>
        [HttpPost("review/{candidateId}")]
        public IActionResult ReviewTestResults(int candidateId, bool isPassed)
        {
            try
            {
                _recruitmentService.ReviewTestResults(candidateId, isPassed);
                _logger.LogInformation("���������� ����� ��� ��������� ID: {CandidateId}: {Result}.", candidateId, isPassed ? "�������" : "�� �������");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "������ ��� �������� ����������� ����� ��� ��������� ID: {CandidateId}.", candidateId);
                return BadRequest(ex.Message);
            }
        }
    }
}
