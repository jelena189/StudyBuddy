using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Models;
using StudyBuddy.Domain.Pagination;

namespace StudyBuddy.Domain.ServiceInterfaces
{
    public interface IUserService : IBaseService<UserDto>
    {
        Task<Guid> InsertUserAsync(UserSignUp userSignUp);

        public Task<int> UpdateUserAsync(UserPutRequest userModel, Guid id);

        Task<UserDto> LoginAsync(UserLogin userLogin);

        Task<UsersPaginatedOutput> GetUsersAsync(PaginationInput paginator);
    }

}
