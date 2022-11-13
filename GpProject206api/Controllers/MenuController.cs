using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GpProject206.Domain;
using GpProject206.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GpProject206.Controllers
{
    [ResponseHeader("Access-Control-Allow-Origin", "http://localhost:3000")]
    [Route("[controller]")]
    [Controller]
    public class MenuController : Controller
    {                
        private readonly ProductService _product;
        private readonly PromotionService _promo;
        private readonly CategoryService _cat;
        
        public MenuController(ProductService service, PromotionService promo, CategoryService cat)
        {
            _product = service;
            _promo = promo;
            _cat = cat;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllProducts()
        {
            var result = await _product.ReadAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("Cat")]
        public async Task<ActionResult> GetAllCategories()
        {
            var result = await _cat.ReadAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("Cat/{id}")]
        public async Task<ActionResult> GetCat(string id)
        {
            var result = await _cat.ReadAll();
            if (result != null)
            {
                return Ok(result.Where(x => x.Id == id));
            }
            return NotFound();
        }

        [HttpGet("Promo")]
        public async Task<ActionResult> GetAllPromo()
        {
            var all = await _promo.ReadAll();
            var result = all.Where(x => !x.IsEnded);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


/*
        // ============================================================================ PUT ===========
        [HttpPut]
        public async Task<ActionResult> Update([FromBody] ProductItem item)
        {
            var result = await _service.Update(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }


        // ============================================================================ DEL ===========
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.Remove(id);
            return Ok();
        }*/
        
    }
}