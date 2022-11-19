using GpProject206.Domain;
using GpProject206.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public async Task<ActionResult> CreateProduct([FromBody] Product item)
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
        public async Task<ActionResult> UpdateProduct(string id, [FromBody] Product item)
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
        public async Task<ActionResult> DeleteProduct(string id)
        {
            if (!await _menu.IsExist(id)) return BadRequest("No such product.");

            await _menu.Remove(id);
            return Ok();
        }

        [HttpPost("Promo")]
        public async Task<ActionResult> InsertPromo([FromBody] Promotion item)
        {
            var result = await _promo.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("Promo/All")]
        public async Task<ActionResult> ReadAllPromo()
        {
            var result = await _promo.ReadAll();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("Promo/{id}")]
        public async Task<ActionResult> UpdatePromo(string id, [FromBody] Promotion item)
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
        public async Task<ActionResult> DeletePromo(string id, [FromBody] Promotion item)
        {
            item.SetId(id);
            if (!await _promo.IsExist(id)) return BadRequest("No such promotion.");

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
        public async Task<ActionResult> InsertCat([FromBody] ProductCategory item)
        {
            var result = await _cat.Create(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("Cat/{id}")]
        public async Task<ActionResult> UpdateCat(string id, [FromBody] ProductCategory item)
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
        public async Task<ActionResult> DeleteCat(string id)
        {
            if (!await _cat.IsExist(id)) return BadRequest("No such product.");
            if (await _menu.ReadKey(nameof(Product.CategoryId), id) != null) return BadRequest("Can't delet if there is product under this category.");

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
        public async Task<ActionResult> DeleteMember(string id)
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