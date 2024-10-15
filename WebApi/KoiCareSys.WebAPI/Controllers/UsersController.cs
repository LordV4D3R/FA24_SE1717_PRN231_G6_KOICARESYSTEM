using Microsoft.AspNetCore.Mvc;
using KoiCareSys.Data.Models;
using KoiCareSys.Service.Service.Interface;
using KoiCareSys.Service.Service;
using KoiCareSys.Data.DTO;

namespace KoiCareSys.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }



        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers([FromQuery] string? search)
        {
            try
            {
                var result = await userService.GetAll(search);
                if (result.Status > 0)
                {
                    return Ok(result.Data as IEnumerable<User>);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var result = await userService.GetById(id);
            return Ok(result.Data);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
        */
        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] RegisterNewUserDTO request)
        {
            try
            {
                var profile = new RegisterNewUserDTO
                {
                    Email = request.Email,
                    Password = request.Password,
                    FullName = request.FullName,
                    PhoneNumber = request.PhoneNumber,
                    Role = request.Role,
                    Status = request.Status
                };
                var result = await userService.Create(profile);
                if (result.Status > 0)
                {
                    return Ok(result.Data as User);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }


        }

        /*

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
        */
    }
}
