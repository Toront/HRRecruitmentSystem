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

        public RecruitmentController(RecruitmentService recruitmentService)
        {
            _recruitmentService = recruitmentService;
        }

        [HttpPost("vacancy")]
        public IActionResult AddVacancy([FromBody] Vacancy vacancy)
        {
            try
            {
                _recruitmentService.AddVacancy(vacancy);
                return CreatedAtAction(nameof(AddVacancy), vacancy);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("resume/{vacancyId}")]
        public IActionResult SubmitResume(int vacancyId, [FromBody] Candidate candidate)
        {
            try
            {
                _recruitmentService.SubmitResume(vacancyId, candidate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("interview/{candidateId}")]
        public IActionResult ConductInterview(int candidateId)
        {
            _recruitmentService.ConductInterview(candidateId);
            return Ok();
        }

        [HttpPost("review/{candidateId}")]
        public IActionResult ReviewTestResults(int candidateId, bool isPassed)
        {
            _recruitmentService.ReviewTestResults(candidateId, isPassed);
            return Ok();
        }
    }
}
