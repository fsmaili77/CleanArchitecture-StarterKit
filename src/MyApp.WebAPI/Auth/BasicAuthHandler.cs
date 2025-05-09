using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using MyApp.Application.Interfaces;
using System.Text.Encodings.Web;
using BCrypt.Net;

namespace MyApp.WebAPI.Auth
{
    public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepository;

        public BasicAuthHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IUserRepository userRepository)
            : base(options, logger, encoder, clock)
        {
            _userRepository = userRepository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Missing Authorization Header");

            try
            {
                var authHeaderRaw = Request.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(authHeaderRaw))
                    return AuthenticateResult.Fail("Missing Authorization Header");
                AuthenticationHeaderValue authHeader;

                try
                {
                    authHeader = AuthenticationHeaderValue.Parse(authHeaderRaw);
                }
                catch (FormatException)
                {
                    return AuthenticateResult.Fail("Invalid Authorization Header Format");
                }

                if (!authHeader.Scheme.Equals("Basic", StringComparison.OrdinalIgnoreCase))
                    return AuthenticateResult.Fail("Invalid Authorization Scheme");

              //  var authHeader = AuthenticationHeaderValue.Parse(authHeaderRaw);

                var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? "");
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);

                if (credentials.Length != 2)
                    return AuthenticateResult.Fail("Invalid Basic Auth Format");
                var email = credentials[0];
                var password = credentials[1];

                var user = await _userRepository.GetUserByEmailAsync(email);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))

                    return AuthenticateResult.Fail("Invalid credentials");

                var claims = new[] {
                    new Claim(ClaimTypes.Name, user.Email ?? string.Empty),
                    new Claim(ClaimTypes.Role, user.Role ?? "User")
                };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            catch
            {
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
        }
    }
}