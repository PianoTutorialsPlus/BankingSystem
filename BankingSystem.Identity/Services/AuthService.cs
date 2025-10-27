using BankingSystem.Application.Contracts.Identity;
using BankingSystem.Application.Exceptions;
using BankingSystem.Application.Models.Identity;
using BankingSystem.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankingSystem.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly JwtSettings jwtSettings;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.jwtSettings = jwtSettings.Value;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email)
            ?? throw new NotFoundException($"User with {request.Email} not found", request.Email);

        var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);

        if (!result.Succeeded)
            throw new BadRequestException($"Credentials for {request.Email} aren´t valid.");

        var jwtSecurityToken = await GenerateToken(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName
        };
        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            var stringBuilder = new StringBuilder();
            foreach (var err in result.Errors)
                stringBuilder.AppendFormat("{0}\n", err.Description);

            throw new BadRequestException($"{stringBuilder}");
        }

        await userManager.AddToRoleAsync(user, "Employee");
        return new RegistrationResponse { UserId = user.Id };
    }
    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {
        var userClaims = await userManager.GetClaimsAsync(user);
        var roles = await userManager.GetRolesAsync(user);

        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("uid", user.Id),
        }
        .Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.DurationInMinutes),
            signingCredentials: signingCredentials);

        return jwtSecurityToken;
    }
}
