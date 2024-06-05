using HaberPortali.DTO;
using HaberPortali.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HaberPortali.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        
        private readonly NewsContext _context;

        public NewsController(NewsContext context)
        {
           _context = context;
        }

        // localhost:5000/api/products => GET
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _context.News.Where(i => i.IsActive).Select(p => 
            ProductToDTO(p))
            .ToListAsync();
            return Ok(products);
        }

        // localhost:5000/api/products/1 => GET
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
           if(id == null)
            {
                return NotFound();
            }

            var p = await _context
                .News.Where(i => i.NewsId == id)
                .Select(p => ProductToDTO(p))
                .FirstOrDefaultAsync();

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(News entity)
        {
            _context.News.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.NewsId }, entity);
        }

        // localhost:5000/api/products/5 => GET
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, News entity)
        {
            if (id != entity.NewsId)
            {
                return BadRequest();
            }

            var product = await _context.News.FirstOrDefaultAsync(i => i.NewsId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.NewsTitle = entity.NewsTitle;
            product.Date = entity.Date;
            product.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null) 
            { 
                return NotFound();
            }

            var product = await _context.News.FirstOrDefaultAsync(i => i.NewsId == id);

            if(product == null)
            {
                return NotFound();
            }

            _context.News.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            return NoContent();

        }


        private static NewsDTO ProductToDTO(News p)
        {
            var entity = new NewsDTO();
            if(p != null)
            {
                entity.NewsId = p.NewsId;
                entity.NewsTitle = p.NewsTitle;
                entity.Date = p.Date;
            }
            return entity;
        }
    }


}