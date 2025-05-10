using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyApp.Application.DTOs;
using MyApp.Application.Interfaces;
using MyApp.Application.Users.Commands;
using MyApp.Application.Users.Queries;


namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator, IUserRepository userRepository)
        {
            _mediator = mediator;
            _userRepository = userRepository;
        }
        // This controller handles HTTP requests related to user operations.
        // It contains endpoints for user registration, login, and profile management.

        [AllowAnonymous]
        // This endpoint allows users to register by providing their name, email, and password.
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            // Call the application layer to handle the registration logic
            // var handler = new RegisterUserHandler(_userRepository);            
            var userId = await _mediator.Send(command);         

            return CreatedAtAction(nameof(GetById), new { id = userId }, new { Id = userId });  
        }

        [AllowAnonymous]
        // This endpoint allows users to log in by providing their email and password.
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            // Call the application layer to handle the login logic
            // var handler = new LoginUserHandler(_userRepository);
            var token = await _mediator.Send(command);
            return Ok(new { token });
        }

        [Authorize]
        [HttpPost("logout")]
        // This endpoint allows users to log out.
        public IActionResult Logout()
        {
            // Invalidate the user's session or token here if needed
            return Ok("Logged out successfully");
        }

        // This endpoint allows users to refresh their access token using a refresh token.
        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand command)
        {
            var newAccessToken = await _mediator.Send(command);
            return Ok(new { accessToken = newAccessToken });
        }

        [Authorize]
        // This endpoint allows users to secure any endpoint.
        [HttpGet("me")]
        public IActionResult GetMyInfo()
        {
            var email = User.Identity?.Name;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new { email, role });
        }


        [HttpGet("{id}")]
        // This endpoint retrieves a user by their ID.
        // It returns a DTO containing the user's details.      
        public async Task<IActionResult> GetById(Guid id)
        {
            // use IMediator to send the query to the appropriate handler
        // and return the result.
            // The handler will interact with the repository to fetch the user data.
            // If the user is not found, return a 404 Not Found response.
            // If the user is found, return a 200 OK response with the user DTO.
            var query = new GetUserByIdQuery { UserId = id };
            // Add validation logic here if needed
            /* var validator = new GetUserByIdQueryValidator();
            var validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            } */

            var userDto = await _mediator.Send(query);
            if (userDto == null)
            {
                return NotFound();
            }
            return Ok(userDto); // Return the user DTO as the response            
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            if (role == "Admin")
            {
                // Admin sees all users
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            }

            // Regular user only sees their own info
            var email = User.Identity?.Name;
            if (email == null) return Unauthorized();

            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return NotFound();

            var dto = new UserDto
            {
                Name = user.Name,
                Email = user.Email
            };

            return Ok(new[] { dto });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult GetAdminSecret()
        {
            // This endpoint is only accessible to users with the "Admin" role.
            return Ok("You are an Admin !");
        }
    }
    
}