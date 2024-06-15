using KBYS.BusinessLogic.Command.Allergies;
using KBYS.Entities.Dto;
using KBYS.Helper;
using KBYS.Repository.Allergies;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace KBYSApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AllergiesController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IAllergiesRepository _allergyRepository;
        private readonly ILogger<AllergiesController> _logger;



        public AllergiesController(IMediator mediator, IAllergiesRepository allergyRepository, ILogger<AllergiesController> logger)
        {
            _mediator = mediator;
            _allergyRepository = allergyRepository;
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

                var allergies = await _allergyRepository.FindBy(a => a.Name.Contains(query)).ToListAsync();
                return Ok(allergies);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for allergies.");
                return StatusCode(500, "Internal server error");
            }
        }


        /// <summary>
        /// Add a new allergy.
        /// </summary>
        /// <param name="addAllergyCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(ServiceResponse<AllergyDto>))]
        public async Task<IActionResult> Add([FromBody] AddAllergyCommand addAllergyCommand)
        {
            var result = await _mediator.Send(addAllergyCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete an allergy by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(ServiceResponse<bool>))]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteAllergyCommand { Id = id });
            return ReturnFormattedResponse(result);
        }


    }
}
