using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using MediatR;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            // Call the application layer to handle the registration logic
            // var handler = new RegisterUserHandler(_userRepository);            
            var userId = await _mediator.Send(command);         

            return CreatedAtAction(nameof(GetById), new { id = userId }, new { Id = userId });           
            
            
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }
    }
    
}