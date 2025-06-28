using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartMeetingRoomApi.Data;
using SmartMeetingRoomApi.Dtos;
using SmartMeetingRoomApi.Models;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using System.IdentityModel.Tokens.Jwt;
using SmartMeetingRoomApi.services;

namespace SmartMeetingRoomApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(SmartMeetingRoomApiDbContext _context,IAuthService authService) : ControllerBase
    {
        //private readonly SmartMeetingRoomApiDbContext _context;

        //public UserController(SmartMeetingRoomApiDbContext context)
        //{
        //    _context = context;
        //}

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _context.Users
                //.Include(u => u.ScheduledMeetings)
                //.Include(u => u.Notifications)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    UserName = u.UserName,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    Role = u.Role,

                })
                .ToListAsync();

            return Ok(users);

            

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var u = await _context.Users.FindAsync(id);
            if (u == null) return NotFound();

            return Ok(new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName,
                Email = u.Email,
                IsActive = u.IsActive,
                Role = u.Role
            });
        }

        [HttpPost("Create User")]
        public async Task<ActionResult<User>> CreateUser(CreateUserDto dto)
        {
            var user =await authService.CreateUser(dto);
            if(user is null) 
                return BadRequest("User with this email already exists.");

            return Ok(user);

            //    var user = new User
            //    {
            //        FirstName = dto.FirstName,
            //        LastName = dto.LastName,
            //        Email = dto.Email,
            //        PasswordHash = dto.Password,
            //        Role = dto.Role,
            //        IsActive = true,
            //        CreatedAt = DateTime.UtcNow,
            //        UserName = dto.FirstName + " " + dto.LastName
            //    };
            //    user.PasswordHash = user.PasswordHash = new PasswordHasher<User>()
            //        .HashPassword(user, dto.Password);

            //    _context.Users.Add(user);
            //    await _context.SaveChangesAsync();

            //    return CreatedAtAction(nameof(GetUser), new { id = user.Id }, new UserDto
            //    {
            //        Id = user.Id,
            //        FirstName = user.FirstName,
            //        LastName = user.LastName,
            //        Email = user.Email,
            //        IsActive = user.IsActive,
            //        Role = user.Role,
            //        UserName = dto.FirstName + " " + dto.LastName
            //    });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserLoginDto dto)
        {

            var token = await authService.Login(dto);
            if (token is null) 
                return Unauthorized("Invalid email or password.");
            return Ok(token);
            //    var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == dto.Email);
            //    if (user == null) return Unauthorized("User not Found");
            //    var passwordHasher = new PasswordHasher<User>();
            //    //var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            //    if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password) == PasswordVerificationResult.Failed)
            //        return Unauthorized("Wrong Password");

            //    var token = CreateToken(user);
            //    return Ok(token);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UpdateUserDto dto)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();
            if (dto.Email != null) user.Email = dto.Email;
            if (dto.Password != null) user.PasswordHash = dto.Password; // hashing logic
            if (dto.Role != null) user.Role = dto.Role;
            if (dto.IsActive.HasValue) user.IsActive = dto.IsActive.Value;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        
    }
}