using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using UmbracoDemo.Dtos.User;

namespace UmbracoDemo.Controllers.API
{
    [Route("api/User")]
    [ApiController]
    public class UserManagementController : UmbracoApiController
    {
        [HttpPost("Register")]
        public IActionResult Register(UserToAddDto userToAddDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();


            return Ok();
        }

        [HttpPost("Login")]
        public IActionResult Login(UserTOLogin userTOLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            return Ok();
        }
    }
}
