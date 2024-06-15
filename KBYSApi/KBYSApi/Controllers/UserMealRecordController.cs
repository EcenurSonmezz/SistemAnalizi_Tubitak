using MediatR;
using Microsoft.AspNetCore.Mvc;
using KBYS.BusinessLogic.Command.UserMealRecord;
using KBYS.Helper;
using KBYS.Entities.Dto;
using System;
using System.Threading.Tasks;

namespace KBYS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMealRecordController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserMealRecordController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddUserMealRecord([FromBody] AddUserMealRecordCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }

            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("today/{userId}")]
        public async Task<IActionResult> GetTodayUserMeals(Guid userId)
        {
            try
            {
                if (userId == Guid.Empty)
                {
                    return BadRequest("User ID is required.");
                }

                var query = new GetTodayUserMealsQuery { UserId = userId };
                var response = await _mediator.Send(query);

                if (!response.Success)
                {
                    return StatusCode(response.StatusCode, response.Errors);
                }

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
