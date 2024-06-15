using AutoMapper;
using KBYS.BusinessLogic.Command.Users;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Dto;
using KBYS.Entities.Entities;
using KBYS.Helper;
using KBYS.Repository.Users;
using MediatR;
using Serilog;

namespace KBYS.BusinessLogic.Handler.Users
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ServiceResponse<UserDto>>
    {
        private readonly IUnitOfWork<KBYSDbContext> _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public LoginCommandHandler(IUnitOfWork<KBYSDbContext> unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.PasswordHash, user.Salt))
                {
                    return ServiceResponse<UserDto>.ReturnError("Invalid email or password");
                }

                var userDto = _mapper.Map<UserDto>(user);
                return ServiceResponse<UserDto>.ReturnResultWith200(userDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while logging in.");
                return ServiceResponse<UserDto>.ReturnException(ex);
            }
        }
    }
}
