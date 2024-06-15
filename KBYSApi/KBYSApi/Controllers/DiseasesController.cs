using KBYS.Repository.Diseases;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KBYSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiseasesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDiseaseRepository _diseaseRepository;
        private readonly ILogger<DiseasesController> _logger;



        public DiseasesController(IMediator mediator, IDiseaseRepository diseaseRepository, ILogger<DiseasesController> logger)
        {
            _mediator = mediator;
            _diseaseRepository = diseaseRepository  ;
            _logger = logger;
        }



        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string query)
        {


            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return BadRequest("Query parameter is required");
                }

                var allergies = await _diseaseRepository.FindBy(a => a.Name.Contains(query)).ToListAsync();
                return Ok(allergies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for allergies.");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
