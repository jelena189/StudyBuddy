using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Pagination;
using StudyBuddy.Domain.ServiceInterfaces;

namespace StudyBuddy.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TDto, TService> : Controller
        where TDto : BaseDto
        where TService : IBaseService<TDto>
    {
        protected readonly TService service;

        public BaseController(TService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Deletes an entity
        /// </summary>
        /// <param name="id">Id of entity to delete</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public virtual async Task<IActionResult> DeleteEntityAsync(Guid id)
        {
            await service.DeleteAsync(id);
            return NoContent();
        }

        /// <summary>
        /// Finds entity
        /// </summary>
        /// <param name="id">Id of the entity to read</param>
        /// <returns>JSON object containing the resource</returns>
        [HttpGet("{id}")]
        [ActionName(nameof(GetEntityAsync))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public virtual async Task<IActionResult> GetEntityAsync(Guid id)
        {
            return Ok(await service.GetByIdAsync(id));
        }

        /// <summary>
        /// Gets paginated list of entities
        /// </summary>
        /// <param name="paginator">Page number and number of entities per page</param>
        /// <returns>Paginated list of entities</returns>
        [HttpGet]
        public virtual async Task<IActionResult> GetEntitiesAsync([FromQuery] PaginationInput paginator)
        {
            return Ok(await service.GetEntitiesAsync(paginator));
        }
    }
}