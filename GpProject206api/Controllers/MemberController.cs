﻿using GpProject206.Domain;
using GpProject206.Services;
using GpProject206.Settings;
using DnsClient;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;
using Member = GpProject206.Domain.Member;

namespace GpProject206.Controllers
{
    [ResponseHeader("Access-Control-Allow-Origin", "*")]
    [Route("[controller]")]
    [Controller]
    public class MemberController : Controller
    {
        private readonly MemberService _member;
        private readonly OrderService _order;
        public MemberController(MemberService member, OrderService order)
        {
            _member = member;
            _order = order;
        }

        [HttpPost("New")]
        public async Task<ActionResult> SignUp([FromBody] Member item)
        {
            var check = await _member.ReadKey(nameof(Member.Email), item.Email);
            if (check != null)
                return BadRequest(new ErrorResponse("This email is already in use."));

            var result = await _member.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new ErrorResponse("Nothing from database"));
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login([FromBody] LoginObject item)
        {
            var result = await _member.ReadKey(nameof(Member.Email), item.Email);
            if (result.VerifyPassword(item.Password))
            {
                return Ok(result);
            }
            return NotFound(new ErrorResponse("Nothing from database"));
        }

        [HttpGet("Pofile/{id}")]
        public async Task<ActionResult> Profile(string id)
        {
            var result = await _member.ReadId(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new ErrorResponse("Nothing from database"));
        }

        [HttpPost("Update/{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] MemberUpdateObject item)
        {
            Member member = await _member.ReadId(id);

            if(!member.VerifyPassword(item.CurrentPassword)) return BadRequest(new ErrorResponse("Incorrect password"));

            member.Modify(item);
            var result = await _member.Update(member);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(new ErrorResponse("Nothing from database"));
        }

        [HttpGet("All")]
        public async Task<ActionResult> Member_ListAll()
        {
            var result = await _member.ReadAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new ErrorResponse("Nothing from database"));
        }

        [HttpGet("History/{id}")]
        public async Task<ActionResult> Member_ListHistory(string id)
        {
            var result = await _order.Filter(nameof(Order.MemberId), id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound(new ErrorResponse("Nothing from database"));
        }
    }
}