using Microsoft.AspNetCore.Mvc;

namespace BlazorEcommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _db;

        public ProductController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProduct()
        {
            return Ok(await _db.Products.ToListAsync());
        }


    }
}
