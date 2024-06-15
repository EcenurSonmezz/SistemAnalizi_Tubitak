using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using KBYS.DataAcces.UnitOfWork;
using KBYS.Repository.Allergies;
using KBYS.Repository.Diseases;
using KBYS.Repository.Foods;
using KBYS.Repository.NutritionalValues;
using KBYS.Repository.UserAllergies;
using KBYS.Repository.UserDiseases;
using KBYS.Repository.UserMealRecords;
using KBYS.Repository.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace KBYSApi.Extensions
{
    public static class StartupExtensions
    {
        public static void ConfigureServices(this IServiceCollection services)
        {
            // AutoMapper configuration
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // MediatR configuration
            var assembly = AppDomain.CurrentDomain.Load("KBYS.BusinessLogic");
            services.AddMediatR(assembly);

            // FluentValidation configuration
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());

            // Add other services
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAllergiesRepository, AllergiesRepository>();
            services.AddScoped<IFoodRepository, FoodRepository>();
            services.AddScoped<IUserMealRecordRepository, UserMealRecordRepository>();
            services.AddScoped<INutritionalValueRepository, NutritionalValueRepository>();
            services.AddScoped<IUserAllergiesRepository, UserAllergiesRepository>();
            services.AddScoped<IUserDiseasesRepository, UserDiseasesRepository>();
            services.AddScoped<IDiseaseRepository, DiseaseRepository>();
        }
    }
}
