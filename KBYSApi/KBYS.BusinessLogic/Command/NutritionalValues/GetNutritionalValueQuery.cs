using KBYS.Entities.Dto;
using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.NutritionalValues
{
    public class GetNutritionalValueQuery : IRequest<ServiceResponse<NutritionalValueDto>>
    {
        public int FoodId { get; set; }
    }
}
