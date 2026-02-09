using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ConfigurationsTask.Entities.Auth;
using ConfigurationsTask.Entities.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TokenOptions = Microsoft.AspNetCore.Identity.TokenOptions;

namespace ConfigurationsTask.Controllers.Auth;
[ApiController]
[Route("api/[controller]/[action]")]

public class AuthController:ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IMapper _mapper;
    private readonly IConfiguration _config;
    private readonly TokenOption _tokenOption;

    public AuthController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, IConfiguration config)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mapper = mapper;
        _config = config;
        _tokenOption = _config.GetSection("TokenOptions").Get<TokenOption>();
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



    [HttpPost]
    public async Task<IActionResult> Login(LoginDto login)
    {
        var user = await _userManager.FindByNameAsync(login.UserName);
        if (user is null)
        {
            return NotFound();
        }

        if (!await _userManager.CheckPasswordAsync(user, login.Password))
        {
            return  Unauthorized();
        }

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOption.SecurityKey));

        SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        JwtHeader header = new JwtHeader(signingCredentials);

        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("FirstName", user.FirstName),
        };
        foreach (var role in await _userManager.GetRolesAsync(user))
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        
        JwtPayload payload = new JwtPayload(issuer: _tokenOption.Issuer,audience:_tokenOption.Audience,claims:claims,notBefore:DateTime.Now,expires:DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration));
        
        JwtSecurityToken securityToken = new JwtSecurityToken(header, payload);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        string myToken = handler.WriteToken(securityToken);

        return Ok(new
        {
            Token = myToken,
            Expires = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration).Ticks
        });
    }
}