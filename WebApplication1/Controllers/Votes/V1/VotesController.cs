using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VotesRestApi.Core.DTOs;
using VotesRestApi.Service.Interfaces;
using WebApplication.Requests;

namespace WebApplication1.Controllers
{
    [Route("api/votes")]
    [ApiController]
    public class VotesController : ControllerBase
    {
        private readonly IVoteService _service;

        public VotesController(IVoteService service)
        {
            _service = service;
        }

        // GET: api/votes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetVoteResultDto>>> GetAllVotes()
        {
            var votes = _service.GetAll();

            return Ok(votes);
        }

        // GET: api/votes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GetVoteResultDto>> GetVote(Guid id)
        {
            var vote = _service.GetByIdAsync(id);
        
            if (vote == null)
            {
                return NotFound();
            }
        
            return Ok(vote);
        }

        // POST: api/votes
        [HttpPost]
        public async Task<ActionResult<Guid>> PostVote(CreateVoteRequest request)
        {
            Guid? id = await _service.AddAsync(request.ToDto());

            if(!id.HasValue)
            {
                return UnprocessableEntity();
            }
        
            return CreatedAtAction("PostVote", id.Value);
        }

        // PUT: api/votes/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser([FromRoute] Guid id, [FromBody] UpdateVoteRequest request)
        {
            bool isValid = await _service.UpdateAsync(request.ToDto(id));

            if (!isValid)
            {
                return UnprocessableEntity();
            }

            return NoContent();
        }

        // DELETE: api/votes/{id}
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
