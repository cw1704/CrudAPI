using CrudAPI.Domain;
using CrudAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CrudAPI.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class OrderController : Controller 
    {
        private readonly OrderService _service;

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Order data)
        {
            var result = await _service.MakeOrder(data);
            return BadRequest();
        }
    }
}