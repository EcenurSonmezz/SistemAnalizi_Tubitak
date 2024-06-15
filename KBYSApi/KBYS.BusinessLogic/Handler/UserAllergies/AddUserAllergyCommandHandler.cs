using AutoMapper;
using KBYS.BusinessLogic.Command.UserAllergies;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Entities;
using KBYS.Helper;
using KBYS.Repository.UserAllergies;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.UserAllergies
{
    public class AddUserAllergyCommandHandler : IRequestHandler<AddUserAllergyCommand, ServiceResponse<bool>>
    {
        private readonly IUserAllergiesRepository _userAllergiesRepository;
        private readonly IUnitOfWork<KBYSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserAllergyCommandHandler> _logger;

        public AddUserAllergyCommandHandler(
            IUserAllergiesRepository userAllergiesRepository,
            IMapper mapper,
            IUnitOfWork<KBYSDbContext> uow,
            ILogger<AddUserAllergyCommandHandler> logger
        )
        {
            _userAllergiesRepository = userAllergiesRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(AddUserAllergyCommand request, CancellationToken cancellationToken)
        {
            foreach (var allergyId in request.AllergyIds)
            {
                var entity = new UserAllergy
                {
                    UserId = request.UserId,
                    AllergyId = allergyId
                };

                await _userAllergiesRepository.AddAsync(entity);
            }

            try
            {
                if (await _uow.SaveAsync() <= 0)
                {
                    _logger.LogError("Failed to save the user allergies to the database.");
                    return ServiceResponse<bool>.Return500("Failed to save the user allergies to the database.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the user allergies to the database.");
                return ServiceResponse<bool>.Return500(ex.Message);
            }

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }

}
