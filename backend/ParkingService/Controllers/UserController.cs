using Microsoft.AspNetCore.Mvc;
using ParkingService.Models;
using ParkingService.Services;
using System.Threading.Tasks;

namespace ParkingService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{mail}")]
        public async Task<IActionResult> GetById(string mail)
        {
            var user = await _userService.GetByIdAsync(mail);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            var newUser = await _userService.AddAsync(user);
            return CreatedAtAction(nameof(GetById), new { id = newUser.id }, newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, User user)
        {
            if (id != user.id) return BadRequest();
            var updatedUser = await _userService.UpdateAsync(user);
            if (updatedUser == null) return NotFound();
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _userService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
