using KBYS.Entities.Dto;
using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.UserMealRecord
{
    public class GetTodayUserMealsQuery : IRequest<ServiceResponse<List<UserMealRecordDto>>>
    {
        public Guid UserId { get; set; }
    }
}
