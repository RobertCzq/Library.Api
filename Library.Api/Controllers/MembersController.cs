using Library.Api.Infrastructure.Models;
using Library.Api.Models;
using Library.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromServices] IMembersService membersService, int id)
        {
            var member = await membersService.Get(id);
            if (member != null)
            {
                return Ok(member);
            }

            return NotFound();
        }


        [HttpPost]
        public async Task<ActionResult> AddBook([FromServices] IMembersService membersService,
            [FromBody] MemberInputModel member)
        {
            var added = await membersService.Add(new Member()
            {
                Name = member.Name,
                JoinedDate = member.JoinedDate
            });
            if (added)
            {
                return Created("", member);
            }

            return NoContent();
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromServices] IMembersService membersService,
            int id,
            [FromBody] MemberInputModel member)
        {
            var updated = await membersService.Update(id, new Member()
            {
                Name = member.Name,
                JoinedDate = member.JoinedDate
            });

            if (updated)
            {
                return Ok(member);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromServices] IMembersService membersService, int id)
        {
            var deleted = await membersService.Delete(id);

            if (deleted)
            {
                return Ok(id);
            }

            return NoContent();
        }
    }
}
