using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Security;
using UmbracoDemo.Dtos.User;

namespace UmbracoDemo.Controllers.API
{
    [Route("api/User")]
    [ApiController]
    public class UserManagementController : UmbracoApiController
    {
        private readonly IMemberManager _memberManager;
        private readonly IMemberSignInManager _memberSignInManager;
        public UserManagementController(IMemberManager memberManager, IMemberSignInManager memberSignInManager)
        {
            _memberManager = memberManager;
            _memberSignInManager = memberSignInManager;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserToAddDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value?.Errors.Select(e => e.ErrorMessage)));

            if (string.IsNullOrEmpty(model.Username) && !string.IsNullOrEmpty(model.Email))
                model.Username = model.Email;

            //Create new member
            var identityUser = MemberIdentityUser.CreateNew(model.Username, model.Email, "portalMember", true, model.Name);
            IdentityResult identityResult = await _memberManager.CreateAsync(identityUser, model.Password);

            if (identityResult is not null && identityResult.Succeeded)
            {
                await _memberSignInManager.SignInAsync(identityUser, false);
                if (_memberManager.IsLoggedIn())
                    return Ok();
                else
                    return BadRequest("Authentication Failed");
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserTOLogin userTOLogin)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value?.Errors.Select(e => e.ErrorMessage)));

            //Find user by username
            MemberIdentityUser fetchedUser = await _memberManager.FindByNameAsync(userTOLogin.Username);
            if (fetchedUser is not null)
            {
                var result = await _memberSignInManager.PasswordSignInAsync(userTOLogin.Username, userTOLogin.Password, userTOLogin.RememberMe, false);
                if (result is not null && result.Succeeded)
                    return Ok("User Logged in sucessfully");
            }
            return BadRequest("Invalid UserName or Password");
        }

        [HttpPost("Logout/{userName}")]
        public async Task<IActionResult> Logout(string userName)
        {
            if (string.IsNullOrEmpty(userName))
                return BadRequest();

            //Find user by username
            MemberIdentityUser fetchedUser = await _memberManager.FindByNameAsync(userName);
            if (fetchedUser is not null)
            {
                var isLoggedIn = HttpContext.User.Identity?.IsAuthenticated ?? false;

                if (_memberManager.IsLoggedIn() && isLoggedIn)
                {
                    await _memberSignInManager.SignOutAsync();
                    return Ok();
                }
            }
            return BadRequest("Invalid User");
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(PasswordToChangeDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value?.Errors.Select(e => e.ErrorMessage)));

            //Find user by username
            MemberIdentityUser fetchedUser = await _memberManager.FindByNameAsync(model.Username);
            if (fetchedUser is not null)
            {
                IdentityResult result = await _memberManager.ChangePasswordAsync(fetchedUser, model.CurrentPassword, model.NewPassword);
                if (result is not null && result.Succeeded)
                    return Ok("Password updated sucessfully");
            }

            return BadRequest("Invalid User");
        }

        [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword(PasswordToResetDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(x => x.Value?.Errors.Select(e => e.ErrorMessage)));

            //Find user by username
            MemberIdentityUser fetchedUser = await _memberManager.FindByNameAsync(model.Username);
            if (fetchedUser is not null)
            {
                var generatedToken = await _memberManager.GeneratePasswordResetTokenAsync(fetchedUser);
                if (generatedToken is not null)
                {
                    IdentityResult result = await _memberManager.ResetPasswordAsync(fetchedUser, generatedToken, model.NewPassword);

                    if (result is not null && result.Succeeded)
                        return Ok("Password updated sucessfully");
                }
                else
                    return BadRequest("Failed");
            }

            return BadRequest("Invalid User");
        }


    }
}
