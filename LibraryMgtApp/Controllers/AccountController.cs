using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryMgtApp.Context.Models;
using LibraryMgtApp.Dto;
using LibraryMgtApp.Extensions;
using LibraryMgtApp.Extensions.Helpers;
using LibraryMgtApp.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryMgtApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<ApplicationIdentityRole> _roleManager;
        private readonly IUserMgmtService _userSrv;
        private readonly AppSettings _appSettings;
        public AccountController(
            UserManager<AppUser> userManager,
            RoleManager<ApplicationIdentityRole> roleManager,
            IOptions<AppSettings> appSettings,
            IUserMgmtService userSrv
            )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _userSrv = userSrv;
            _appSettings = appSettings.Value;
        }

        [HttpPost(nameof(Register))]
        public async Task<IActionResult> Register([FromBody]RegisterForDto model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userSrv.UserExists(model.Email))
                        ModelState.AddModelError("Email", "Email already taken");
                    if (await _userSrv.PhoneExists(model.PhoneNumber))
                        ModelState.AddModelError("PhoneNumber", "Phone Number already taken");

                    var user = AppUser.Create(model.FirstName, model.LastName, model.Email, model.Gender, model.PhoneNumber, model.NIN);

                    user.UserName = model.Email;
                    user.EmailConfirmed = true;
                    user.FullName = model.FirstName + " " + model.LastName;
                    user.Activated = true;
                    user.IsDisabled = false;
                    user.CreatedOnUtc = DateTime.Now.GetDateUtcNow();
                    user.LockoutEnabled = false;

                    var createResult = await _userManager.CreateAsync(user, model.Password);

                    if (!createResult.Succeeded)
                    {
                        return BadRequest(new { errorList = $"{createResult.Errors.FirstOrDefault().Description}" });
                    }
                    createResult = await _userManager.AddToRoleAsync(user, "USERS");

                    if (!createResult.Succeeded)
                    {
                        return BadRequest(new { errorList = $"{createResult.Errors.FirstOrDefault().Description}" });
                    }
                }
                //success message
                return Ok("Registration Successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost(nameof(Login))]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto formdata)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            var user = await _userSrv.GetUserByEmail(formdata.Email);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Secret));
            double tokenExpiryTime = Convert.ToDouble(_appSettings.ExpireTime);
            if (user != null && await _userManager.CheckPasswordAsync(user, formdata.Password))
            {
                // Then Check If Email Is confirmed
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                    response.Code = ApiResponseCodes.INVALID_REQUEST;
                    response.Description = $"{formdata.Email} has not confirmed email!";
                    return BadRequest(new
                    {
                        LoginError = "We sent you an Confirmation Email. Please Confirm Your Registration With Us To Log in."
                    });
                }
                var roles = await _userManager.GetRolesAsync(user);
                var fullname = user.FirstName + " " + user.LastName;
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, formdata.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, fullname),
                        new Claim("LoggedOn", DateTime.Now.ToString())
                     }),
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                    Issuer = _appSettings.Site,
                    Audience = _appSettings.Audience,
                    Expires = DateTime.UtcNow.AddMinutes(tokenExpiryTime)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);
                return Ok(new
                {
                    tokenString,
                    id = user.Id,
                    email = user.Email,
                    userRole = roles.FirstOrDefault(),
                    expiration = token.ValidTo
                });
            }
            response.Code = ApiResponseCodes.NOT_FOUND;
            response.Description = "Email / Password was not Found";
            return BadRequest(new
            {
                LoginError = "Invalid Email/Password was entered"
            });
        }
    }
}