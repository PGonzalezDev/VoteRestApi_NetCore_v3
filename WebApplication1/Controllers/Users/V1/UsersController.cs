using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Service.DTOs;
using VotesRestApi.Service.Interfaces;
using WebApplication;

namespace WebApplication1.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;

        public UsersController(IUserService service)
        {
            _service = service;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<GetUserResponse>> GetAllUsers()
        {
            var users = _service.GetAll();

            return Ok(users?.Select(x => new GetUserResponse(x)));
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserResponse>> GetUserById(Guid id)
        {
            var userDto = await _service.GetByIdAsync(id);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(new GetUserResponse(userDto));
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<Guid>> PostUser(CreateUserRequest request)
        {
            Guid id = await _service.AddAsync(request.ToDto());

            return CreatedAtAction("PostUser", id);
        }

        // PUT: api/users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
        {
            bool isValid = await _service.UpdateAsync(request.ToDto(id));

            if (!isValid)
            {
                return UnprocessableEntity();
            }

            return NoContent();
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            bool exist = await _service.RemoveAsync(id);
            
            if (!exist)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
