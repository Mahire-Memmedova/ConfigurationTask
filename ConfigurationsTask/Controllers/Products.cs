using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ConfigurationsTask.DAL;
using ConfigurationsTask.DAL.Repositories.Abstract;
using ConfigurationsTask.Entities;
using ConfigurationsTask.Entities.Dtos.Products;

namespace ConfigurationsTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetAll()
        {
            var products = await _repo.GetAllAsync();

            var dto = _mapper.Map<List<GetProductDto>>(products);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<ActionResult<List<GetProductDto>>> GetPaged(int page = 1, int size = 10)
        {
            var products = await _repo.GetAllPaginateAsync(page, size);

            if (products == null || !products.Any())
                return NotFound("Bu səhifədə məhsul tapılmadı.");
            
            var dto = _mapper.Map<List<GetProductDto>>(products);
    
            return Ok(dto);
        }        
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _repo.GetAsync(p => p.Id == id);
            if (product == null) return NotFound();
            var dto = _mapper.Map<GetProductDto>(product);
            return Ok(dto);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            if (product == null) return NotFound();
            await _repo.AddAsync(product);
            await _repo.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = await _repo.GetAsync(p => p.Id == id);
            _repo.Remove(deleted);
            await _repo.SaveAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDto dto)
        {
            var product = await _repo.GetAsync(p => p.Id == id);
            if (product == null)
                return NotFound();

            _mapper.Map(dto, product);  
            _repo.Update(product);
            await _repo.SaveAsync();

            return Ok(_mapper.Map<GetProductDto>(product));
        }

    }
}

