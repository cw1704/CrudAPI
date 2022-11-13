using GpProject206.Domain;
using GpProject206.Services;
using GpProject206.Settings;
using DnsClient;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace GpProject206.Controllers
{
    [ResponseHeader("Access-Control-Allow-Origin", "*")]
    [Route("[controller]")]
    [Controller]
    public class MemberController : Controller
    {
        private readonly MemberService _service;
        public MemberController(MemberService member)
        {
            _service = member;
        }


        [HttpPost("New")]
        public async Task<ActionResult> SignUp([FromBody] Member item)
        {
            var check = await _service.ReadKey(nameof(Member.Email), item.Email);
            if (check != null)
                return BadRequest("This email is already in use.");

            var result = await _service.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginObject item)
        {

            var result = await _service.ReadKey(nameof(Member.Email), item.Email);
            if (result.VerifyPassword(item.Password))
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("Pofile/{id}")]
        public async Task<ActionResult> Profile(string id)
        {

            var result = await _service.ReadId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpPost("Update/{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] MemberUpdateObject item)
        {
            Member member = await _service.ReadId(id);

            if(!member.VerifyPassword(item.CurrentPassword)) return BadRequest("Incorrect password");

            member.Update(item);
            var result = await _service.Update(member);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}