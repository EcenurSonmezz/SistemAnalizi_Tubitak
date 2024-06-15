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
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ServiceResponse<UserDto>>
    {
        private readonly IUnitOfWork<KBYSDbContext> _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AddUserCommandHandler(IUnitOfWork<KBYSDbContext> unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Email = request.Email,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    IsEmailConfirmed = false,
                    EmailConfirmationToken = Guid.NewGuid().ToString(),
                };

                user.PasswordHash = PasswordHasher.HashPassword(request.Password, out string salt);
                user.Salt = salt;

                await _userRepository.AddAsync(user);
                await _unitOfWork.Complete();

                var userDto = _mapper.Map<UserDto>(user);

                // Email gönderme işlemi burada yapılabilir

                return ServiceResponse<UserDto>.ReturnResultWith201(userDto);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while adding a new user.");
                return ServiceResponse<UserDto>.ReturnException(ex);
            }
        }
    }
}
