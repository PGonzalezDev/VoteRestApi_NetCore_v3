using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Service.Interfaces;

namespace WebApplication.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportServices _service;

        public ReportController(IReportServices service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<ActionResult<ReportDto>> GetReport([FromHeader] Guid adminId, [FromHeader] DateTime period)
        {
            ReportDto report = null;

            try
            {
                report = await _service.GetReport(adminId, period);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }

            return Ok(report);
        }
    }
}
