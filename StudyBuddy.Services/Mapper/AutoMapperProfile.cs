using AutoMapper;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Models;
using StudyBuddy.Domain.Pagination;
using StudyBuddy.Repositories.Entities;

namespace StudyBuddy.Services.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ConfigureMappings();
        }

        private void ConfigureMappings()
        {
            #region UserMaps

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>().ForMember(d => d.RoleName, o => o.MapFrom(s => s.Role.RoleName));
            CreateMap<UserDto, UserOutput>()/*.ForMember(d => d.FirstName, opt => opt.MapFrom(src => src.Split(',')[0])))*/.ReverseMap();
            CreateMap<UsersPaginatedOutput, PaginationOutput<UserDto>>().ReverseMap();

            CreateMap<UserPutRequest, UserDto>();
            CreateMap<UserSignUp, UserDto>();

            CreateMap<UserDto, UserDto>()
                .ForMember(d => d.PasswordHash, o => o.Condition(src => src.PasswordHash != default))
                .ForMember(d => d.Salt, o => o.Condition(src => src.Salt != default));

            #endregion UserMaps

            #region RoleMaps

            CreateMap<RoleDto, Role>();
            CreateMap<Role, RoleDto>();

            #endregion RoleMaps
        }
    }

}
