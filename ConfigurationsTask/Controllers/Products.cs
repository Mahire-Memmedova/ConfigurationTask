using AutoMapper;
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
        private readonly IMapper _mapper;
        public ProductsController(ConfugurationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<GetProductDto>> GetAllProducts()
        {
            var products = await _context.Products.Include(p=>p.Brand).Select(p=>new GetProductDto()
            {
                BrandName =  p.Brand.Name,
                Desc = p.Desc,
                Price = p.Price,
                Name =  p.Name,
                BrandId =  p.BrandId
            }).ToListAsync();
            return Ok(products);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();
            return Ok(_mapper.Map<GetProductDto>(product));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
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
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            _mapper.Map(dto, product);  
            await _context.SaveChangesAsync();

            return Ok(product);
        }

    }
}

