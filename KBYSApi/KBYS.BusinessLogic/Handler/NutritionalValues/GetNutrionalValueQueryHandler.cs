using AutoMapper;
using KBYS.BusinessLogic.Command.Foods;
using KBYS.BusinessLogic.Command.NutritionalValues;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Dto;
using KBYS.Entities.Entities;
using KBYS.Helper;
using KBYS.Repository.NutritionalValues;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.NutritionalValues
{
    public class GetNutrionalValueQueryHandler : IRequestHandler<GetNutritionalValueQuery, ServiceResponse<NutritionalValueDto>>
    {
        protected readonly INutritionalValueRepository _nutritionalValueRepository;
        protected readonly IUnitOfWork<KBYSDbContext> _uow;
        protected readonly IMapper _mapper;
        protected readonly ILogger<GetNutrionalValueQueryHandler> _logger;


        public GetNutrionalValueQueryHandler(
            INutritionalValueRepository nutritionalValueRepository,
            IUnitOfWork<KBYSDbContext> uow, 
            IMapper mapper, 
            ILogger<GetNutrionalValueQueryHandler> logger)
        {
            _nutritionalValueRepository = nutritionalValueRepository;
            _uow = uow;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ServiceResponse<NutritionalValueDto>> Handle(GetNutritionalValueQuery request, CancellationToken cancellationToken)
        {
            var entity = await _nutritionalValueRepository.FindByFirstAsync(a=>a.FoodId==request.FoodId);
            if (entity != null)
            { 
                var entityDto = _mapper.Map<NutritionalValueDto>(entity);
                return ServiceResponse<NutritionalValueDto>.ReturnResultWith200(entityDto);
            }
            else
            {
                return ServiceResponse<NutritionalValueDto>.Return404();
            }
        }
    }
}
