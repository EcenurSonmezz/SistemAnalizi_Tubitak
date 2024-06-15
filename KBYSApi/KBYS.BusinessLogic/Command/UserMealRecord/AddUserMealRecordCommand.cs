using KBYS.Entities.Dto;
using KBYS.Entities.Entities;
using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.UserMealRecord
{
    public class AddUserMealRecordCommand : IRequest<ServiceResponse<UserMealRecordDto>>
    {
        public Guid UserId { get; set; }
        public int FoodId { get; set; }
        public DateTime DateConsumed { get; set; }
    }
}
