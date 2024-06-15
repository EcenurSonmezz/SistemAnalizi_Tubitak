using AutoMapper;
using KBYS.BusinessLogic.Command.Foods;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Dto;
using KBYS.Helper;
using KBYS.Repository.Foods;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.Foods
{
    public class GetByIdFoodQueryHandler : IRequestHandler<GetByIdFoodQuery, ServiceResponse<FoodDto>>
    {
        private readonly IFoodRepository _foodRepository;
        private readonly IUnitOfWork<KBYSDbContext> _ouw;
        private readonly IMapper _mapper;
        private readonly ILogger<GetByIdFoodQueryHandler> _logger;

        public GetByIdFoodQueryHandler(
            IFoodRepository foodRepository,
            IUnitOfWork<KBYSDbContext> ouw,
            IMapper mapper,
            ILogger<GetByIdFoodQueryHandler> logger)
        {
            _foodRepository = foodRepository;
            _ouw = ouw;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<FoodDto>> Handle(GetByIdFoodQuery request, CancellationToken cancellationToken)
        {
            var entity = await _foodRepository.GetFoodWithNutritionalValues(request.Id);
            if (entity != null)
            {
                var entityDto = _mapper.Map<FoodDto>(entity);
                return ServiceResponse<FoodDto>.ReturnResultWith200(entityDto);
            }
            else
            {
                return ServiceResponse<FoodDto>.Return404();
            }
        }
    }
}
