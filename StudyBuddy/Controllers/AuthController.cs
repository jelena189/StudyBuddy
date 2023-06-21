using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudyBuddy.Domain.Models;
using StudyBuddy.Domain.ServiceInterfaces;

namespace StudyBuddy.WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly ISecurityService securityService;
        private readonly IUserService usersService;
        public AuthController(ISecurityService securityService, IUserService userService)
        {
            this.securityService = securityService;
            this.usersService = userService;
        }

        /// <summary>
        /// Log in
        /// </summary>
        /// <param name="userlogin"> user credentials </param>
        /// <returns>JWT token</returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserLogin userlogin)
        {
            var user = await usersService.LoginAsync(userlogin);
            return Ok(securityService.GenerateJwtToken(user.Id, user.RoleName));
        }

        /// <summary>
        /// Create an account
        /// </summary>
        /// <param name="userSignUp"> user info </param>
        /// <returns>No content 204 response if valid</returns>
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync(UserSignUp userSignUp)
        {
            await usersService.InsertUserAsync(userSignUp);
            return NoContent();
        }
    }
}