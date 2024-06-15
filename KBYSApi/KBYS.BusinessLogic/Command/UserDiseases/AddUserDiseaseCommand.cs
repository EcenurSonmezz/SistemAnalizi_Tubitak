using KBYS.Entities.Dto;
using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.UserDiseases
{
    public class AddUserDiseaseCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid UserId { get; set; }
        public List<int> DiseaseIds { get; set; }
    }
}
