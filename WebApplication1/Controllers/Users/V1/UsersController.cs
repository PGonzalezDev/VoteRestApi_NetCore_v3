using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Core.Helper;
using VotesRestApi.Core.Models;
using VotesRestApi.Repositories.Configure;
using VotesRestApi.Service.DTOs;
using VotesRestApi.Service.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IUserService _service;

        public UsersController(ApplicationDBContext context, IUserService service)
        {
            _context = context;
            _service = service;
        }

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<GetUserDto>> GetAllUsers()
        {
            var users = _service.GetAll();

            return CreatedAtAction("GetAllUsers", users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.UserDbSet.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/users/{id}
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

        // POST: api/users]
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.Pass = CryptographyHelper.Encrypt(user.Pass);

            _context.UserDbSet.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUser", new { id = user.Id }, user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(Guid id)
        {
            var user = await _context.UserDbSet.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.UserDbSet.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(Guid id)
        {
            return _context.UserDbSet.Any(e => e.Id == id);
        }
    }
}
