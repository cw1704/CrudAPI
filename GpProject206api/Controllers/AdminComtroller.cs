using GpProject206.Domain;
using GpProject206.Services;
using Microsoft.AspNetCore.Mvc;

namespace GpProject206.Controllers
{
    [ResponseHeader("Access-Control-Allow-Origin", "*")]
    [ResponseHeader("Access-Control-Allow-Methods", "GET,HEAD,PUT,PATCH,POST,DELETE")]
    [ResponseHeader("Access-Control-Allow-Credentials", "true")]
    [ResponseHeader("Access-Control-Allow-Headers", "Content-Type, Accept")]
    [Route("[controller]")]
    [Controller]
    public class AdminController : Controller
    {
        private readonly ProductService _menu;
        private readonly PromotionService _promo;
        private readonly MemberService _member;
        private readonly CategoryService _cat;

        public AdminController(ProductService service, PromotionService promo, MemberService member, CategoryService cat)
        {
            _menu = service;
            _promo = promo;
            _member = member;
            _cat = cat;
        }

        [HttpPost("Product/New")]
        public async Task<ActionResult> Product_Create([FromBody] Product item)
        {
            if(!await _cat.IsExist(item.CategoryId)) return BadRequest("No such category.");

            var result = await _menu.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("Product/{id}")]
        public async Task<ActionResult> Product_Update(string id, [FromBody] Product item)
        {
            item.SetId(id);
            if (!await _menu.IsExist(id)) return BadRequest("No such product.");
            if (!await _cat.IsExist(item.CategoryId)) return BadRequest("No such category.");

            var result = await _menu.Update(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("Product/{id}")]
        public async Task<ActionResult> Product_Delete(string id)
        {
            if (!await _menu.IsExist(id)) return BadRequest("No such product.");

            await _menu.Remove(id);
            return Ok();
        }

        [HttpPost("Promo")]
        public async Task<ActionResult> Promo_Insert([FromBody] Promotion item)
        {
            if (await _menu.IsExist(nameof(Promotion.Code), item.Code)) return BadRequest("No such product.");

            var result = await _promo.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("Promo/All")]
        public async Task<ActionResult> Promo_ReadAll()
        {
            var result = await _promo.ReadAll();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("Promo/{id}")]
        public async Task<ActionResult> Promo_Update(string id, [FromBody] Promotion item)
        {
            item.SetId(id);
            if (!await _promo.IsExist(id)) return BadRequest("No such promotion.");

            var result = await _promo.Update(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("Promo/{id}")]
        public async Task<ActionResult> Promo_Delete(string id)
        {
            Promotion item = await _promo.ReadId(id);
            if (item == null) return BadRequest("No such promotion.");

            item.CountLimit = -1;
            item.MarkEnded();
            var result = await _promo.Update(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("Cat")]
        public async Task<ActionResult> Cat_Insert([FromBody] ProductCategory item)
        {
            var result = await _cat.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("Cat/{id}")]
        public async Task<ActionResult> Cat_Update(string id, [FromBody] ProductCategory item)
        {
            item.SetId(id);
            if (!await _cat.IsExist(id)) return BadRequest("No such promotion.");

            var result = await _cat.Update(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("Cat/{id}")]
        public async Task<ActionResult> Cat_Delete(string id)
        {
            if (!await _cat.IsExist(id)) return BadRequest("No such product.");
            if (await _menu.IsExist(nameof(Product.CategoryId), id)) return BadRequest("Can't delet if there is product under this category.");

            await _cat.Remove(id);
            return Ok();
        }

        /*[HttpPut("Member/{id}")]
        public async Task<ActionResult> UpdateMember(string id, [FromBody] Member item)
        {
            Member member = await _member.ReadId(id);

            if (null == member) return BadRequest("No such member");

            member.Modify(item);
            var result = await _member.Update(member);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(item);
        }*/

        [HttpDelete("Member/{id}")]
        public async Task<ActionResult> Member_Delete(string id)
        {
            var result = await _member.ReadId(id);
            if (result != null)
            {
                await _member.Remove(id);
            }
            return NotFound();

        }
    }
}