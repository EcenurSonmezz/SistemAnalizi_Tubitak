using AutoMapper;
using KBYS.BusinessLogic.Command.UserDiseases;
using KBYS.DataAcces.KBYSDbContexts;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Entities.Dto;
using KBYS.Entities.Entities;
using KBYS.Helper;
using KBYS.Repository.UserDiseases;
using KBYS.Repository.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KBYS.BusinessLogic.Handler.UserDiseases
{
    public class AddUserDiseaseCommandHandler : IRequestHandler<AddUserDiseaseCommand, ServiceResponse<bool>>
    {
        private readonly IUserDiseasesRepository _userDiseasesRepository;
        private readonly IUnitOfWork<KBYSDbContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserDiseaseCommandHandler> _logger;

        public AddUserDiseaseCommandHandler(
            IUserDiseasesRepository userDiseasesRepository,
            IMapper mapper,
            IUnitOfWork<KBYSDbContext> uow,
            ILogger<AddUserDiseaseCommandHandler> logger
        )
        {
            _userDiseasesRepository = userDiseasesRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }

        public async Task<ServiceResponse<bool>> Handle(AddUserDiseaseCommand request, CancellationToken cancellationToken)
        {
            foreach (var diseaseId in request.DiseaseIds)
            {
                var entity = new UserDisease
                {
                    UserId = request.UserId,
                    DiseaseId = diseaseId
                };

                await _userDiseasesRepository.AddAsync(entity);
            }

            try
            {
                if (await _uow.SaveAsync() <= 0)
                {
                    _logger.LogError("Failed to save the user diseases to the database.");
                    return ServiceResponse<bool>.Return500("Failed to save the user diseases to the database.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the user diseases to the database.");
                return ServiceResponse<bool>.Return500(ex.Message);
            }

            return ServiceResponse<bool>.ReturnResultWith200(true);
        }
    }

}

