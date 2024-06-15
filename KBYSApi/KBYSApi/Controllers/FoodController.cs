using Microsoft.AspNetCore.Mvc;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Entities;
using KBYS.Repository.Foods;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using KBYS.DataAcces.KBYSDbContexts;
using Microsoft.EntityFrameworkCore;
using KBYS.Entities.Dto;
using MediatR;
using KBYS.BusinessLogic.Command.Foods;
using KBYS.Api.Controllers;
using KBYSApi.Controllers; // Bunu ekleyin

namespace KBYS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : BaseController
    {
        private readonly IUnitOfWork<KBYSDbContext> _unitOfWork;
        private readonly IFoodRepository _foodRepository;
        private readonly ILogger<FoodController> _logger;
        private readonly IMediator _mediator;

        public FoodController(IUnitOfWork<KBYSDbContext> unitOfWork, IFoodRepository foodRepository, ILogger<FoodController> logger, IMediator mediator)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _foodRepository = foodRepository;
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

                var foods = await _foodRepository.FindBy(f => f.name.Contains(query)).ToListAsync();
                return Ok(foods);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for foods.");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(FoodDto))]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetByIdFoodQuery { Id = id });

            return ReturnFormattedResponse(result);
        }
    }
}
