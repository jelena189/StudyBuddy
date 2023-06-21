using StudyBuddy.Domain.Dtos;

namespace StudyBuddy.Domain.RepositoryInterfaces
{
    public interface IUserRepository : IBaseRepository<UserDto>
    {
        public Task<UserDto> GetUserByUsername(string username);

        public Task<UserDto> GetUserByEmail(string email);

        public bool IsEmailUnique(string email);

        public bool IsUsernameUnique(string username);
    }
}
