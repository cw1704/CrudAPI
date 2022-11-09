using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CrudAPI.Domain;
using CrudAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CrudAPI.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class MenuController : Controller
    {                
        private readonly MenuService _service;
        private readonly PromotionService _promo;
        
        public MenuController(MenuService service, PromotionService promo)
        {
            _service = service;
            _promo = promo;
        }

        // ============================================================================ GET ===========

        [HttpGet]
        public async Task<ActionResult> GetMenuFull()
        {
            var result = await _service.GetFullMenu();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("Cat")]
        public async Task<ActionResult> GetCategories()
        {
            var result = await _service.GetCategories();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("Cat/{cat}")]
        public async Task<ActionResult> GetMenuCat(string cat)
        {
            var result = await _service.GetFullMenu();
            if (result != null)
            {
                return Ok(result.Where(x => x.Category == cat));
            }
            return NotFound();
        }

        [HttpGet("Promo")]
        public async Task<ActionResult> GetPromo()
        {
            var result = await _promo.Read();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            var result = await _service.GetById(id);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        // ============================================================================ POST ===========

        [HttpPost("Item")]
        public async Task<ActionResult> InsertProduct([FromBody]ProductItem item)
        {   
            var result = await _service.InsertProduct(item);
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

        [HttpPost("Cat")]
        public async Task<ActionResult> InsertCat([FromBody] ProductCategory item)
        {
            var result = await _service.InsertCategory(item);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
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
        }
        */
    }
}