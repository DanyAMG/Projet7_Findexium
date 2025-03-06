using Microsoft.AspNetCore.Mvc;
using P7CreateRestApi.Models;
using Microsoft.AspNetCore.Identity;
using P7CreateRestApi.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Dot.Net.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public LoginController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerModel.Username,
                Email = registerModel.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerModel.Password);

            if (identityResult.Succeeded)
            {
                if (registerModel.Roles != null && registerModel.Roles.Any())
                {
                   identityResult =  await userManager.AddToRolesAsync(identityUser, registerModel.Roles);

                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered. Please login");
                    }
                }
            }
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            //TODO: implement the UserManager from Identity to validate User and return a security token.
            var user = await userManager.FindByEmailAsync(loginModel.Username);

            if (user != null)
            {
                var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginModel.Password);

                if (checkPasswordResult)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                        return Ok(jwtToken);
                    }

                    
                    
                }
            }

            return BadRequest("Username or password incorrect");
        }            
    }
}