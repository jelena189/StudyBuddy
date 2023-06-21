using StudyBuddy.Domain.Dtos;

namespace StudyBuddy.Domain.RepositoryInterfaces
{
    public interface IRoleRepository : IBaseRepository<RoleDto>
    {
        public Task<RoleDto> GetRoleByName(string roleName);
    }

}
