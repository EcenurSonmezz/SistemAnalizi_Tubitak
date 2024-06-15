using KBYS.Entities.Dto;
using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.Allergies
{
    public class DeleteAllergyCommand : IRequest<ServiceResponse<bool>>
    {
        public Guid Id { get; set; }
    }
}
