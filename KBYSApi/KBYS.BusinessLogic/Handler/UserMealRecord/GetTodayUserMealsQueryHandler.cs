using AutoMapper;
using KBYS.BusinessLogic.Command.UserMealRecord;
using KBYS.Entities.Dto;
using KBYS.Entities.Entities;
using KBYS.Helper;
using KBYS.Repository.UserMealRecords;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.UserMealRecords
{
    public class GetTodayUserMealsQueryHandler : IRequestHandler<GetTodayUserMealsQuery, ServiceResponse<List<UserMealRecordDto>>>
    {
        private readonly IUserMealRecordRepository _userMealRecordRepository;
        private readonly IMapper _mapper;

        public GetTodayUserMealsQueryHandler(IUserMealRecordRepository userMealRecordRepository, IMapper mapper)
        {
            _userMealRecordRepository = userMealRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<UserMealRecordDto>>> Handle(GetTodayUserMealsQuery request, CancellationToken cancellationToken)
        {
            var userMealRecords = await _userMealRecordRepository.GetTodayUserMealsAsync(request.UserId);
            var userMealRecordDtos = _mapper.Map<List<UserMealRecordDto>>(userMealRecords);

            return ServiceResponse<List<UserMealRecordDto>>.ReturnResultWith200(userMealRecordDtos);
        }
    }
}
