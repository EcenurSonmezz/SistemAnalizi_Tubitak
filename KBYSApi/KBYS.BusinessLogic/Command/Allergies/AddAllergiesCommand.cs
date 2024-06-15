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
    public class AddAllergyCommand : IRequest<ServiceResponse<AllergyDto>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
