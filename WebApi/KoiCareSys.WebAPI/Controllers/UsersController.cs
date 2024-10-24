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
            try
            {
                var result = await userService.GetById(id);
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

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, UpdateUserDTO user)
        {
            try
            {
                var result = await userService.GetById(id);
                if (result.Status < 0)
                {
                    return BadRequest();
                }
                var userUpdate = result.Data as User;
                var update = await userService.UpdateUser(userUpdate, user);
                if (update.Status > 0)
                {
                    return Ok(update.Data as User);
                }
                else { return NotFound(result.Message); }
            }
            catch
            {
                return BadRequest();
            }
        }

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



        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                var result = await userService.GetById(id);
                if (result.Status < 0)
                {
                    return BadRequest();
                }
                var delete = await userService.DeleteUser(id);
                if (delete.Status > 0)
                {
                    return Ok();
                }
                else { return NotFound(result.Message); }

            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
