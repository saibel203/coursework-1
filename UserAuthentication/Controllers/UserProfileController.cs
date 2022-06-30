using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UserAuthentication.Models;
using UserAuthentication.Utils;
using System.Net.Http.Headers;

namespace UserAuthentication.Controllers;

[ApiController]
[Route("api/{controller}")]
public class UserProfileController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;

    public UserProfileController(UserManager<AppUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    
    [HttpGet]
    [Authorize] // /api/UserProfile
    public async Task<IActionResult> GetUserAccount()
    {
        string userId = User.Claims.First(x => x.Type == "UserID").Value;
        AppUser user = await _userManager.FindByIdAsync(userId);
        
        IList<string> role = await _userManager.GetRolesAsync(user);
        
        return Ok(new
        {
            user.UserName,
            user.Email,
            user.Name,
            user.LastName,
            user.Description,
            user.ImagePath,
            role
        });
    }

    [HttpPatch]
    [Authorize]
    [Route("Edit")] // /api/UserProfile/Edit
    public async Task<IActionResult> UserEdit([FromBody] JsonPatchDocument<AppUser> model)
    {
        string userId = User.Claims.First(x => x.Type == "UserID").Value;
        AppUser user = await _userManager.FindByIdAsync(userId);
        IList<string> role = await _userManager.GetRolesAsync(user);
        
        model.ApplyTo(user, ModelState);
        await _context.SaveChangesAsync();
        
        return Ok(new
        {
            user.UserName,
            user.Email,
            user.Name,
            user.LastName,
            user.Description,
            user.ImagePath,
            role
        });
    }

    [HttpPost]
    [DisableRequestSizeLimit]
    [Authorize]
    [Route("Upload")] // /api/UserProfile/Upload
    public async Task<IActionResult> UploadImage()
    {
        try
        {
            IFormCollection formCollection = await Request.ReadFormAsync();
            IFormFile file = formCollection.Files.First();
            string folderName = Path.Combine("Resources", "Images");
            string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (file.Length > 0)
            {
                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName!.Trim('"');
                string fullPath = Path.Combine(pathToSave, fileName);
                string dbPath = Path.Combine(folderName, fileName);

                await using FileStream stream = new(fullPath, FileMode.Create);
                await file.CopyToAsync(stream);

                return Ok(new { dbPath });
            }
            return BadRequest();
            
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Ошибка сервера {e.Message}.");
        }
    }

    [HttpGet]
    [Authorize]
    [Route("GetImage")] // /api/UserProfile/GetImage
    public async Task<IActionResult> GetImageProfile()
    {
        string userId = User.Claims.First(x => x.Type == "UserID").Value;
        AppUser user = await _userManager.FindByIdAsync(userId);
        
        string fileName = Path.GetFileName(user.ImagePath!);
        return Ok(new Image
        {
            Path = $"https://localhost:7269/Resources/Images/{fileName}"
        });
    }
}
