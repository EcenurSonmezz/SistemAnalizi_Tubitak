using KBYS.Entities.Dto;
using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.Foods
{
    public class GetByIdFoodQuery : IRequest<ServiceResponse<FoodDto>>
    {
        public int Id { get; set; }
    }
}
