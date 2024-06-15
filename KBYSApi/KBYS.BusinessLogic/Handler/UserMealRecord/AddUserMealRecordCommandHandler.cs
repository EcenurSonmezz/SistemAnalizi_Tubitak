using AutoMapper;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Entities;
using KBYS.Helper;
using KBYS.Repository.UserMealRecords;
using KBYS.BusinessLogic.Command.UserMealRecord;
using MediatR;
using Microsoft.Extensions.Logging;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.Entities.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.UserMealRecords
{
    public class AddUserMealRecordCommandHandler : IRequestHandler<AddUserMealRecordCommand, ServiceResponse<UserMealRecordDto>>
    {
        private readonly IUserMealRecordRepository _userMealRecordRepository;
        private readonly IUnitOfWork<KBYSDbContext> _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserMealRecordCommandHandler> _logger;

        public AddUserMealRecordCommandHandler(
            IUserMealRecordRepository userMealRecordRepository,
            IMapper mapper,
            IUnitOfWork<KBYSDbContext> unitOfWork,
            ILogger<AddUserMealRecordCommandHandler> logger)
        {
            _userMealRecordRepository = userMealRecordRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ServiceResponse<UserMealRecordDto>> Handle(AddUserMealRecordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userMealRecord = _mapper.Map<UserMealRecord>(request);
                userMealRecord.DateConsumed = DateTime.UtcNow;

                _userMealRecordRepository.Add(userMealRecord);

                if (await _unitOfWork.Complete() <= 0)
                {
                    return ServiceResponse<UserMealRecordDto>.Return500();
                }

                var userMealRecordDto = _mapper.Map<UserMealRecordDto>(userMealRecord);
                return ServiceResponse<UserMealRecordDto>.ReturnResultWith200(userMealRecordDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding user meal record.");
                return ServiceResponse<UserMealRecordDto>.Return500();
            }
        }
    }
}
