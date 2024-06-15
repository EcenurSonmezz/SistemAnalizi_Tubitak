using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.UserAllergies
{
    public class AddUserAllergyCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid UserId { get; set; }
        public List<Guid> AllergyIds { get; set; }
    }
}
