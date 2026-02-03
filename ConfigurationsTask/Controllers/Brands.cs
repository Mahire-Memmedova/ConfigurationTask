using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ConfigurationsTask.DAL;
using ConfigurationsTask.Entities;
using ConfigurationsTask.Entities.Dtos.Brands;
using ConfigurationsTask.Entities.Dtos.Products;

namespace ConfigurationsTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly ConfugurationDbContext _context;
        public BrandsController(ConfugurationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
           return Ok(await _context.Brands.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Brands.SingleOrDefaultAsync(x => x.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto brandDto)
        {
            Brand brand = new Brand()
            {
                Name = brandDto.Name
            };
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(int id,UpdateBrandDto brandDto)
        {
            var updated = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            updated.Name = brandDto.Name;
            _context.Brands.Update(updated);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var deleted = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            _context.Brands.Remove(deleted);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}