using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VotesRestApi.Core.Models;
using VotesRestApi.Service.Context;
using VotesRestApi.Service.Helper;

namespace WebApplication1.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUserDbSet()
        {
            var result = await _context.UserDbSet.ToListAsync();

            if (!result.Any())
            {
                await MockUsers();
            }

            return await _context.UserDbSet.ToListAsync();
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
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

        // POST: api/users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            user.Id = Guid.NewGuid();
            user.Pass = CryptographyHelper.Encrypt(user.Pass);

            _context.UserDbSet.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
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

        #region MockUsers
        private async Task MockUsers()
        {
            _context.UserDbSet.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Mail = "admin@admin.com",
                Pass = CryptographyHelper.Encrypt("Admin"),
                UserRoleValue = 0
            });
            _context.UserDbSet.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Manu Ginobili",
                Mail = "manu@ginobili.com",
                Pass = CryptographyHelper.Encrypt("Manu20."),
                UserRoleValue = 1
            });
            _context.UserDbSet.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Chapu Nocioni",
                Mail = "chapu@nocioni.com",
                Pass = CryptographyHelper.Encrypt("Chapu13."),
                UserRoleValue = 1
            });
            _context.UserDbSet.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Luis Scola",
                Mail = "luifa@scola.com",
                Pass = CryptographyHelper.Encrypt("Luifa4."),
                UserRoleValue = 1
            });
            _context.UserDbSet.Add(new User()
            {
                Id = Guid.NewGuid(),
                Name = "Facu Campazzo",
                Mail = "facu@campazzo.com",
                Pass = CryptographyHelper.Encrypt("Facu7."),
                UserRoleValue = 1
            });

            await _context.SaveChangesAsync();
        } 
        #endregion

        private bool UserExists(Guid id)
        {
            return _context.UserDbSet.Any(e => e.Id == id);
        }
    }
}
