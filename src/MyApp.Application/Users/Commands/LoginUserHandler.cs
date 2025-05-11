using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MyApp.Application.Interfaces;
using MyApp.Core.Entities;
using System.Security.Cryptography;
using System.Text.Json;

namespace MyApp.Application.Users.Commands
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public LoginUserHandler(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
                throw new UnauthorizedAccessException("Invalid credentials");
            if (!user.IsEmailVerified)
            throw new UnauthorizedAccessException("Email not verified. Please check your inbox.");

            var jwtSection = _configuration.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSection["Key"]!);
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expiresIn = int.Parse(jwtSection["ExpiresInMinutes"]!);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expiresIn),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // Generate Refresh Token
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // 7 days validity

            await _userRepository.UpdateUserAsync(user); // You’ll add this if not done

            return JsonSerializer.Serialize(new
            {
                accessToken = tokenHandler.WriteToken(token),
                refreshToken
            });
        }

        // This method can be used to generate a refresh token
        private string GenerateRefreshToken()
        {
            var randomBytes = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
                return Convert.ToBase64String(randomBytes);
            }
        }
    }
}