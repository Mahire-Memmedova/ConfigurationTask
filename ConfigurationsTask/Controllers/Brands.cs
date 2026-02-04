using AutoMapper;
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
        private readonly IMapper _mapper;
        public BrandsController(ConfugurationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetBrandDto>>> GetAllBrands()
        {
            var brands = await _context.Brands.ToListAsync();
            return Ok(_mapper.Map<List<GetBrandDto>>(brands));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (brand == null)
                return NotFound();

            return Ok(_mapper.Map<GetBrandDto>(brand));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto brandDto)
        {
          
            await _context.Brands.AddAsync(_mapper.Map<Brand>(brandDto));
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBrand(int id, UpdateBrandDto update)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (brand == null)
                return NotFound();

            _mapper.Map(update, brand); 
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var deleted = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if (deleted == null)
                return NotFound();
                _context.Brands.Remove(deleted);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}