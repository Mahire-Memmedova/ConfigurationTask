using AutoMapper;
using ConfigurationsTask.Entities.Auth;
using ConfigurationsTask.Entities.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationsTask.Controllers.Auth;
[ApiController]
[Route("api/[controller],[action]")]

public class AuthController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;

    public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterDto register)
    {
        var user = _mapper.Map<AppUser>(register);
        var addedUser = await _userManager.CreateAsync(user, register.Password);
        if (!addedUser.Succeeded)
        {
            return BadRequest(new
            {
                Errors = addedUser.Errors,
                Code = 400,
            });
        }

        if (await _roleManager.RoleExistsAsync("User"))
        {
            return BadRequest("bu rol var");
        }

        var addedRole = await _roleManager.CreateAsync(new IdentityRole("User"));
        if (!addedRole.Succeeded)
        {
            return BadRequest(new
            {
                Errors = addedRole.Errors,
                Code = 400,
            });
        }
        await _userManager.AddToRoleAsync(user, "User");
      return Ok("User created");
    }
}