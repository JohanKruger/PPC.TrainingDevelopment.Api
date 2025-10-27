using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PPC.TrainingDevelopment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TrainingPsMasterController : ControllerBase
    {
        private readonly TrainingPsMasterService _service;

        public TrainingPsMasterController()
        {
            _service = new TrainingPsMasterService();
        }

        [HttpGet]
        public async Task<ActionResult<List<TrainingPsMaster>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{personnelNumber}")]
        public async Task<ActionResult<TrainingPsMaster>> GetByPersonnelNumber(string personnelNumber)
        {
            var result = await _service.GetByPersonnelNumberAsync(personnelNumber);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
