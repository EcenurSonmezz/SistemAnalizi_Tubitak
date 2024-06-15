using KBYS.Entities.Dto;
using KBYS.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Command.Users
{
    public class LoginCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
