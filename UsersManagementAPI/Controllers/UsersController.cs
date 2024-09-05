using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersManagementAPI.Data;
using UsersManagementAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace UsersManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                if (_context.Users.Any(u => u.email == user.email))
                {
                    return Conflict(new { message = "Email already in use." });
                }

                // Hash the password before saving
                user.password = _passwordHasher.HashPassword(user, user.password);

                // Save user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetUserById), new { id = user.id }, user);
            }
            catch (Exception ex)
            {
                // Log exception details
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while creating the user.", details = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            try
            {
                return await _context.Users.ToListAsync();
            }
            catch (Exception ex)
            {
                // Log exception details
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching users.", details = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }
                return user;
            }
            catch (Exception ex)
            {
                // Log exception details
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while fetching the user.", details = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, User user)
        {
            if (id != user.id)
            {
                return BadRequest(new { message = "User ID mismatch." });
            }

            try
            {
                // Check if the email is already exist
                if (_context.Users.Any(u => u.email == user.email && u.id != id))
                {
                    return Conflict(new { message = "Email already in use by another user." });
                }

                var existingUser = await _context.Users.FindAsync(id);
                if (existingUser == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                // Update the user details
                existingUser.name = user.name;
                existingUser.email = user.email;

                // If a new password is provided, hash it and update it
                if (!string.IsNullOrEmpty(user.password))
                {
                    existingUser.password = _passwordHasher.HashPassword(existingUser, user.password);
                }

                existingUser.updatedAt = DateTime.UtcNow;

                _context.Entry(existingUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound(new { message = "User not found." });
                }
                throw; // Rethrow exception after checking existence
            }
            catch (Exception ex)
            {
                // Log exception details
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while updating the user.", details = ex.Message });
            }
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception details
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while deleting the user.", details = ex.Message });
            }
        }

        private bool UserExists(int id) => _context.Users.Any(e => e.id == id);
    }
}
