using BankSystem.Application.Interfaces;
using BankSystem.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankSystem.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// This endpoint authenticate a user, if the user is not registered then is created
        /// </summary>
        /// <param name="username">User name</param>
        /// <param name="password">Password</param>
        /// <returns>A Service Result for User Entity</returns>
        [HttpPost]
        [Route("Authenticate")]
        [ProducesResponseType(typeof(ServiceResult<UserEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticationRequest req) 
        {
            return Ok(await _userService.AuthenticateUser(req.UserName, req.Password));
        }
        /// <summary>
        /// Returns a service result with an user data
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{userId:int}")]
        [ProducesResponseType(typeof(ServiceResult<UserEntity>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(JsonErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetUserData(int userId) 
        {
            return Ok(await _userService.GetUserData(userId));
        }
    }
}
