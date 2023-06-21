using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Domain.Dtos;
using StudyBuddy.Domain.Resources;
using StudyBuddy.Domain.ServiceInterfaces;

namespace StudyBuddy.WebApi.Controllers
{
    [Route("api/schools")]
    [Authorize(Policy = RoleNames.Administrator)]
    [ApiController]
    public class SchoolController : BaseController<SchoolDto, ISchoolService>
    {
        public SchoolController(ISchoolService schoolService) : base(schoolService)
        {

        }

        /// <summary>
        /// Insert a new school
        /// </summary>
        /// <param name="model"> school info </param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateSchool(SchoolDto model)
        {
            await service.InsertAsync(model);
            return NoContent();
        }
    }
}
