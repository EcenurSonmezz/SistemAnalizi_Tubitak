using AutoMapper;
using KBYS.BusinessLogic.Command.Users;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Dto;
using KBYS.Helper;
using KBYS.Repository.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace KBYS.BusinessLogic.Handler.Users
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ServiceResponse<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<KBYSDbContext> _unitOfWork;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(
            IUserRepository userRepository,
            IMapper mapper,
            IUnitOfWork<KBYSDbContext> unitOfWork,
            ILogger<GetUserByIdQueryHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ServiceResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _userRepository
                .FindBy(u => u.Id == request.Id)
                .Include(u => u.UserAllergies)
                    .ThenInclude(ua => ua.Allergy)
                .Include(u => u.UserDiseases)
                    .ThenInclude(ud => ud.Disease)
                .FirstOrDefaultAsync();

            if (entity != null)
            {
                var entityDto = _mapper.Map<UserDto>(entity);
                return ServiceResponse<UserDto>.ReturnResultWith200(entityDto);
            }
            else
            {
                return ServiceResponse<UserDto>.Return404();
            }
        }
    }
}
