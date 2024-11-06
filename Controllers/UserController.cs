using Microsoft.AspNetCore.Mvc;
using DotnetBackend.Models;
using DotnetBackend.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotnetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<List<User>>> Get() =>
            await _userService.GetUsersAsync();

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetById(string id) =>
            await _userService.GetUserByIdAsync(id);

        [HttpPost]
        public async Task<IActionResult> Create(User newUser)
        {
            await _userService.CreateUserAsync(newUser);
            return CreatedAtAction(nameof(GetById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, User updatedUser)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user is null)
                return NotFound();

            updatedUser.Id = user.Id;

            await _userService.UpdateUserAsync(id, updatedUser);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user is null)
                return NotFound();

            await _userService.DeleteUserAsync(id);

            return NoContent();
        }
    }
}