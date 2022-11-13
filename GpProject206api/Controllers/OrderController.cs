using GpProject206.Domain;
using GpProject206.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject206.Controllers
{
    [ResponseHeader("Access-Control-Allow-Origin", "http://localhost:3000")]
    [Route("[controller]")]
    [Controller]
    public class OrderController : Controller 
    {
        private readonly OrderService _order;
        private readonly PromotionService _promo;
        private readonly MemberService _member;
        private readonly ProductService _product;
        public OrderController(OrderService o, PromotionService promo, MemberService member, ProductService menu)
        {
            _order = o;
            _promo = promo;
            _member = member;
            _product = menu;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Order data)
        {
            var promo = await _promo.ReadKey("Code", data.PromotionCode);
            if (!string.IsNullOrEmpty(data.PromotionCode))
                if (promo == null)
                    return BadRequest("Promotion code invalid.");

            var promoed = await _order.ReadByPromotions(new[] { data.PromotionCode });
            if (promo.CountLimit <= promoed.Count)
                return BadRequest("This promotion is already finished.");

            if (!string.IsNullOrEmpty(data.MemberId))
                if (!await _member.IsExist(data.MemberId))
                    return BadRequest("Member id invalid.");

            if (data.Items.Count < 1 | data.Items.Any(x => !_product.IsExist(x.ProductId).Result) | data.Items.Any(x=>x.Qty<1))
                return BadRequest("Invalid items.");

            var listed = await _product.ReadListed(data.Items.Select(x => x.ProductId).ToList());
            var sub_totals = data.Items.Select(x => listed.First(y => y.Id == x.ProductId).Price * x.Qty);
            var total = sub_totals.Sum();
            if (data.TotalPrice != total)
                return BadRequest("Product price updated. Please try again.");


            if (promoed.Count == promo.CountLimit - 1)
            {
                promo.MarkEnded();
                await _promo.Update(promo);
            }

            var result = await _order.Create(data);
            if (result != null)
                return Ok(result);
            return BadRequest();
        }
    }
}