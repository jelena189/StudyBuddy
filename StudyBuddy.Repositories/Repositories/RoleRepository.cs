using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.RepositoryInterfaces;
using StudyBuddy.Repositories.Entities;

namespace StudyBuddy.Repositories.Repositories
{
    public class RoleRepository : BaseRepository<Role, RoleDto>, IRoleRepository
    {
        public RoleRepository(StudyBuddyContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<RoleDto> GetRoleByName(string roleName)
        {
            var entity = await Entities.AsNoTracking().FirstOrDefaultAsync(r => r.RoleName == roleName);
            return Mapper.Map<RoleDto>(entity);
        }
    }

}
