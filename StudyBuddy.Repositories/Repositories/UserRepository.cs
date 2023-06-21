using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.RepositoryInterfaces;
using StudyBuddy.Repositories.Entities;

namespace StudyBuddy.Repositories.Repositories
{
    public class UserRepository : BaseRepository<User, UserDto>, IUserRepository
    {
        public UserRepository(StudyBuddyContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await Entities.AsNoTracking().Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
            return Mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            var user = await Entities.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
            return Mapper.Map<UserDto>(user);
        }

        public bool IsEmailUnique(string email)
        {
            return !Entities.Any(u => u.Email == email);
        }

        public bool IsUsernameUnique(string username)
        {
            return !Entities.Any(u => u.Username == username);
        }
    }
}
