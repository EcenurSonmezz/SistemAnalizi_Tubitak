using AutoMapper;
using KBYS.BusinessLogic.Command.Allergies;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Dto;
using KBYS.Entities.Entities;
using KBYS.Helper;
using KBYS.Repository.Allergies;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.Allergies
{
    public class AddAllergyCommandHandler : IRequestHandler<AddAllergyCommand, ServiceResponse<AllergyDto>>
    {
        private readonly IAllergiesRepository _allergyRepository;
        private readonly IUnitOfWork<KBYSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddAllergyCommandHandler> _logger;

        public AddAllergyCommandHandler(
            IAllergiesRepository allergyRepository,
            IMapper mapper,
            IUnitOfWork<KBYSDbContext> uow,
            ILogger<AddAllergyCommandHandler> logger
        )
        {
            _allergyRepository = allergyRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<AllergyDto>> Handle(AddAllergyCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Allergy>(request);
            entity.Id = Guid.NewGuid();

            await _allergyRepository.AddAsync(entity);

            try
            {
                if (await _uow.SaveAsync() <= 0)
                {
                    _logger.LogError("Failed to save the new allergy to the database.");
                    return ServiceResponse<AllergyDto>.Return500("Failed to save the new allergy to the database.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the new allergy to the database.");
                return ServiceResponse<AllergyDto>.Return500(ex.Message);
            }

            var entityDto = _mapper.Map<AllergyDto>(entity);
            return ServiceResponse<AllergyDto>.ReturnResultWith200(entityDto);
        }
    }
}
