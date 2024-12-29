using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductProject.Api.Core;
using ProductProject.Api.Data;

namespace ProductProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<ProductEntity>>> GetProducts()
        {
            var products = await _context.ProductEntities.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductEntity>> GetProductById(int id)
        {
            var product = await _context.ProductEntities.FindAsync(id);
            if (product == null)
                return BadRequest("Geçersiz ürün id'si");
            return Ok();
        }
        [HttpPost]
        public async Task<ActionResult<List<ProductEntity>>> AddProduct(ProductEntity product)
        {
            _context.ProductEntities.Add(product);
            await _context.SaveChangesAsync();
            var products = await _context.ProductEntities.ToListAsync();
            return Ok(products);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<ProductEntity>>> UpdateProductById(ProductEntity request)
        {
            var product = await _context.ProductEntities.FindAsync(request.Id);
            if (product == null)
                return BadRequest($"Geçersiz Id : {request.Id}");
            product.Name = request.Name;
            product.Price = request.Price;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProductEntity>>> DeleteProductById(int id)
        {
            var product = await _context.ProductEntities.FindAsync(id);
            if (product == null)
                return BadRequest($"Geçersiz Id : {id}");

            _context.ProductEntities.Remove(product);
            await _context.SaveChangesAsync();
            return Ok($"{id} id'li ürün başarıyla silindi");
        }
    }
}
