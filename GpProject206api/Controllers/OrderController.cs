﻿using GpProject206.Domain;
using GpProject206.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace GpProject206.Controllers
{
    [ResponseHeader("Access-Control-Allow-Origin", "*")]
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
            Promotion? promo = await _promo.ReadKey(nameof(Promotion.Code), data.PromotionCode);
            if (!string.IsNullOrEmpty(data.PromotionCode))
            {
                if (promo == null)
                    return BadRequest(new ErrorResponse("No such promotion code."));

                if (promo.IsEnded)
                    return BadRequest(new ErrorResponse("This promotion is already finished."));
            }

            if (!string.IsNullOrEmpty(data.MemberId))
                if (!await _member.IsExist(data.MemberId))
                    return BadRequest(new ErrorResponse("Invalid member ID."));

            if (data.Items.Count < 1 | data.Items.Any(x => !_product.IsExist(x.ProductId).Result) | data.Items.Any(x=>x.Qty<1))
                return BadRequest(new ErrorResponse("Invalid items."));

            
            /*
            
            var listed = await _product.ReadListed(data.Items.Select(x => x.ProductId).ToList());
            var sub_totals = data.Items.Select(x => listed.First(y => y.Id == x.ProductId).Price * x.Qty);
            var p_total = sub_totals.Sum();
            var total = p_total;

            if (promo != null)
            {
                total = Math.Max(0, total - promo.DirectDeduction);
                total = Math.Max(0, Math.Round(total * promo.PercentageDiscount / 100.00, 2));
            }

            if (data.TotalPrice != total)
                return BadRequest(new ErrorResponse("Product price updated. Please try again."));
            */

            Order result = await _order.Create(data);
            if (result != null)
            {
                if (promo != null)
                {
                    promo.AddAppliedOrders(result.Id);
                    await _promo.Update(promo);
                }
                return Ok(result);
            }

            return BadRequest(new ErrorResponse("Unknown"));
        }
    }
}