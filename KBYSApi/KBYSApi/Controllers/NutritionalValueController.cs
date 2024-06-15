using KBYS.BusinessLogic.Command.NutritionalValues;
using KBYS.Entities.Dto;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KBYSApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionalValueController : BaseController
    {
        IMediator _mediator;

        NutritionalValueController(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json", "application/xml", Type = typeof(NutritionalValueDto))]
        public async Task<IActionResult> Get(int FoodId)
        {
            var result = await _mediator.Send(new GetNutritionalValueQuery { FoodId = FoodId});

            return ReturnFormattedResponse(result);
        }
    }
}
