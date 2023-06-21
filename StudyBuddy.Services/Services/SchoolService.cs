using AutoMapper;
using FluentValidation;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.RepositoryInterfaces;
using StudyBuddy.Domain.ServiceInterfaces;
using StudyBuddy.Repositories.Entities;

namespace StudyBuddy.Services.Services
{
    public class SchoolService : BaseService<School, SchoolDto, ISchoolRepository>, ISchoolService
    {
        private readonly ISchoolRepository schoolRepository;

        public SchoolService(ISchoolRepository schoolRepository,
            IMapper mapper,
            IHttpContextWrapper httpContextWrapper) : base(schoolRepository, mapper, httpContextWrapper)
        {
            this.schoolRepository = schoolRepository;
        }
    }
}
