using AutoMapper;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.RepositoryInterfaces;
using StudyBuddy.Repositories.Entities;

namespace StudyBuddy.Repositories.Repositories
{
    public class SchoolRepository : BaseRepository<School, SchoolDto>, ISchoolRepository
    {
        public SchoolRepository(StudyBuddyContext context, IMapper mapper) : base(context, mapper)
        {

        }
    }
}
