using AutoMapper;
using KBYS.BusinessLogic.Command.Allergies;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Helper;
using KBYS.Repository.Allergies;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.Alergies
{
    public class DeleteAllergyCommandHandler : IRequestHandler<DeleteAllergyCommand, ServiceResponse<bool>>
    {
        private readonly IAllergiesRepository _allergyRepository;
        private readonly IUnitOfWork<KBYSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<DeleteAllergyCommandHandler> _logger;

        public DeleteAllergyCommandHandler(
            IAllergiesRepository allergyRepository,
            IMapper mapper,
            IUnitOfWork<KBYSDbContext> uow,
            ILogger<DeleteAllergyCommandHandler> logger
        )
        {
            _allergyRepository = allergyRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(DeleteAllergyCommand request, CancellationToken cancellationToken)
        {
            var entity = await _allergyRepository.FindAsync(request.Id);

            if (entity == null)
            {
                return ServiceResponse<bool>.Return404("Allergy not found");
            }

            _allergyRepository.Remove(entity);

            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<bool>.Return500();
            }

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }

}
