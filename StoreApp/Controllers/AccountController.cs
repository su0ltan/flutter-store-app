using DataAccess.Data.DTOs.Users;
using DataAccess.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager1;
        private readonly IConfiguration configuration;

        public AccountController(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager1 = userManager;
            this.configuration = configuration;
        }

        [HttpPost("[action]")]

        public async Task<IActionResult> Register(dtoNewUser user)
        {

            if (ModelState.IsValid)
            {
                AppUser appUser = new()
                {
                    UserName = user.userName,
                    PhoneNumber = user.phoneNumber
                };

                IdentityResult result = await _userManager1.CreateAsync(appUser, user.password);
                if (result.Succeeded)
                {
                    return Ok("success");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);

                    }
                }
            }
            return BadRequest(ModelState);
        }



        [HttpPost("[action]")]
        public async Task<IActionResult> login(dtoLogin login)
        {
            if (ModelState.IsValid)
            {
                AppUser? user = await _userManager1.FindByNameAsync(login.userName);

                if(user != null)
                {
                    if (await _userManager1.CheckPasswordAsync(user, login.password))
                    {
                        var claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        var roles = await _userManager1.GetRolesAsync(user);

                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                        }

                        //signing credetianls
                        var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
                        var sc = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            claims: claims, 
                            issuer: configuration["JWT:Issuer"], 
                           /* audience: configuration["JWT:Audience"], */
                            expires: DateTime.Now.AddDays(1), 
                            signingCredentials: sc);

                        var _token = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };




                        return Ok(_token);

                    }
                    else
                    {

                        return Unauthorized();

                    }


                }
                else
                {
                    ModelState.AddModelError("", "User Name is invalid");
                }

            }
            return BadRequest(ModelState);

        }



    }
}
