using KBYS.BusinessLogic.Command.UserAllergies;
using KBYS.Helper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KBYSApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserAllergiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserAllergiesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AddUserAllergyCommand addUserAllergyCommand)
        {
            var result = await _mediator.Send(addUserAllergyCommand);
            return StatusCode(result.StatusCode, result);
        }

        private IActionResult ReturnFormattedResponse<T>(ServiceResponse<T> response)
        {
            if (!response.Success)
            {
                return StatusCode(response.StatusCode, response.Errors);
            }
            return StatusCode(response.StatusCode, response.Data);
        }
    }
}
