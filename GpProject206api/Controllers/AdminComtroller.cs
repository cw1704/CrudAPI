using GpProject206.Domain;
using GpProject206.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GpProject206.Controllers
{
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

        [HttpGet("Member/All")]
        public async Task<ActionResult> Member_ListAll()
        {
            var result = await _member.ReadAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
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
    }
}