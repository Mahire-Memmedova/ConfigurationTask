using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ConfigurationsTask.DAL;
using ConfigurationsTask.Entities;
using ConfigurationsTask.Entities.Dtos.Products;

namespace ConfigurationsTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ConfugurationDbContext _context;
        public ProductsController(ConfugurationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _context.Products.ToListAsync());
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Products.FirstOrDefaultAsync(p => p.Id == id));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
        {
            Product product = new Product()
            {
                Name = productDto.Name,
                Desc = productDto.Desc,
                Price = productDto.Price,
                BrandId =  productDto.BrandId,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            _context.Products.Remove(deleted);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id,UpdateProductDto dto)
        {
            var updated = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            updated.Name = dto.Name;
            updated.Desc = dto.Desc;
            updated.Price = dto.Price;
            updated.BrandId = dto.BrandId;
            _context.Products.Update(updated);
            await  _context.SaveChangesAsync();
            return Ok();
        }
    }
}

