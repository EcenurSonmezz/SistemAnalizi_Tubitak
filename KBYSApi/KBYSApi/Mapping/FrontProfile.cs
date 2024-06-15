using AutoMapper;
using KBYS.BusinessLogic.Command.Users;
using KBYS.Entities.Dto;
using KBYS.Entities.Entities;
using KBYS.BusinessLogic.Command.UserDiseases;
using KBYS.BusinessLogic.Command.Allergies;
using KBYS.BusinessLogic.Command.Foods;
using KBYS.BusinessLogic.Command.NutritionalValues;
using KBYS.BusinessLogic.Command.UserAllergies;
using KBYS.BusinessLogic.Command.UserMealRecord;

namespace KBYSApi.Mapping
{
    public class FrontProfile : Profile
    {
        public FrontProfile()
        {
            // User Mappings
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.UserAllergies.Select(ua => ua.Allergy)))
                .ForMember(dest => dest.Diseases, opt => opt.MapFrom(src => src.UserDiseases.Select(ud => ud.Disease)))
                .ReverseMap();
            CreateMap<AddUserCommand, User>();
            CreateMap<GetUserByIdQuery, User>();

            // Allergy Mappings
            CreateMap<Allergy, AllergyDto>().ReverseMap();
            CreateMap<AddAllergyCommand, Allergy>();
            CreateMap<DeleteAllergyCommand, Allergy>();

            // Disease Mappings
            CreateMap<Disease, DiseaseDto>().ReverseMap();

            // UserDisease Mappings
            CreateMap<UserDisease, UserDiseaseDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.DiseaseId, opt => opt.MapFrom(src => src.DiseaseId))
                .ReverseMap();
            CreateMap<AddUserDiseaseCommand, UserDisease>();

            // Other Mappings
            CreateMap<UserMealRecord, UserMealRecordDto>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Food, opt => opt.MapFrom(src => src.Food))
                .ReverseMap();
            CreateMap<AddUserMealRecordCommand, UserMealRecord>();
            CreateMap<GetTodayUserMealsQuery, UserMealRecord>();

            // Foods
            CreateMap<Food, FoodDto>()
                .ForMember(dest => dest.NutritionalValues, opt => opt.MapFrom(src => src.NutritionalValues))
                .ReverseMap();
            CreateMap<GetByIdFoodQuery, Food>().ReverseMap();

            // NutritionalValue
            CreateMap<NutritionalValue, NutritionalValueDto>().ReverseMap();
            CreateMap<GetNutritionalValueQuery, NutritionalValue>().ReverseMap();

            // UserAllergies
            CreateMap<UserAllergy, UserAllergyDto>().ReverseMap();
            CreateMap<AddUserAllergyCommand, UserAllergy>().ReverseMap();
        }
    }
}
