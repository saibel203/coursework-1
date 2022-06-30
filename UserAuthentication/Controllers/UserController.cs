using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using UserAuthentication.Models;
using UserAuthentication.Utils;

namespace UserAuthentication.Controllers;

[ApiController]
[Route("api/{controller}")]
public class UserController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    // private readonly SignInManager<AppUser> _signInManager;
    // private readonly AppDbContext _db;

    public UserController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
        // _signInManager = signInManager;
        // _db = db;
    }

    [HttpPost]
    [Route("Register")] // /api/User/Register
    public async Task<IActionResult> Register([FromBody] UserModel model)
    {
        AppUser userTest = await _userManager.FindByNameAsync(model.UserName);
        model.Role = "User"; 
        
        if (userTest is not null) 
            return BadRequest(new Response { Status = "400: Bad request.", Message = "Пользователь уже существует." });
        
        AppUser user = new()
        {
            UserName = model.UserName,
            Email = model.Email
        };
        
        IdentityResult result = await _userManager.CreateAsync(user, model.Password);
        
        if (!result.Succeeded)
            return BadRequest(new Response { Status = "400: Bad request.", Message = "Ошибка ввода данных. Пароль должен содержать хотя бы 1 цифру и 1 символ латинского алфавита." });
        await _userManager.AddToRoleAsync(user, "User");
        return Ok(result);
    }

    [HttpPost]
    [Route("Login")] // /api/User/Login
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        AppUser user = await _userManager.FindByNameAsync(model.UserName);

        if (user is null)
            return BadRequest(new Response { Status = "404: Bad request.", Message = "Пользователь с таким логином не найден." });
        if (await _userManager.CheckPasswordAsync(user, model.Password))
        {
            IList<string> role = await _userManager.GetRolesAsync(user);
            IdentityOptions options = new();

            List<Claim> claims = new()
            {
                new("UserID", user.Id),
                new(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault()!)
            };
            
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(ApplicationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);
            string? token = tokenHandler.WriteToken(securityToken);

            return Ok(new { token });   
        }
        return BadRequest(new Response { Status = "404: Bad request.", Message = "Ошибка пароля пользователя." });
    }
}
