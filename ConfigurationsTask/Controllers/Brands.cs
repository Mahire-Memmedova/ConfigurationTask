using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ConfigurationsTask.DAL;
using ConfigurationsTask.DAL.Repositories.Abstract;
using ConfigurationsTask.Entities;
using ConfigurationsTask.Entities.Dtos.Brands;
using ConfigurationsTask.Entities.Dtos.Products;
using Microsoft.AspNetCore.Authorization;

namespace ConfigurationsTask.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IBrandRepository _repo;
       private readonly IMapper _mapper;
       
       public BrandsController(IBrandRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetBrandsAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandById(int? id)
        {
            return Ok(await _repo.GetBrandAsync(b=>b.Id == id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto create)
        {
            var brand = _mapper.Map<Brand>(create);
            await _repo.AddAsync(brand);
            return Ok(brand);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            var deleted =await _repo.GetBrandAsync(b=>b.Id == id);
            _repo.Remove(deleted);
            await _repo.SaveAsync();
            return Ok(deleted);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateBrand(int id,UpdateBrandDto update)
        {
            var uptaded =await _repo.GetBrandAsync(b=>b.Id == id);
            _mapper.Map(update,uptaded);
            await _repo.SaveAsync();
            return Ok(uptaded);
        }
    }
}