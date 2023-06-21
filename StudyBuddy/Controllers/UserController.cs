using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Models;
using StudyBuddy.Domain.Pagination;
using StudyBuddy.Domain.Resources;
using StudyBuddy.Domain.ServiceInterfaces;

namespace StudyBuddy.WebApi.Controllers
{
    [Route("api/users")]
    [Authorize(Policy = RoleNames.Administrator)]
    [ApiController]
    public class UserController : BaseController<UserDto, IUserService>
    {
        public UserController(IUserService service) : base(service)
        {

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(UserPutRequest user, Guid id)
        {
            await service.UpdateUserAsync(user, id);
            return Ok(user);
        }

        [Authorize(Policy = RoleNames.UserAndAdministrator)]
        [HttpGet]
        public override async Task<IActionResult> GetEntitiesAsync([FromQuery] PaginationInput paginator)
        {
            var users = await service.GetUsersAsync(paginator);
            return Ok(users.Entities);
        }
    }
}
