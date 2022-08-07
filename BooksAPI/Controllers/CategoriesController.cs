using BooksAPI.DAL;
using BooksAPI.DTOs;
using BooksAPI.DTOs.Category;
using BooksAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c=>c.Id == id);
            if (category is null) return NotFound();
            CategoryGetDto categoryGetDto = new CategoryGetDto
            {
                Id = category.Id,
                Name = category.Name
            };
            return Ok(categoryGetDto);
        }


        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Categories.AsQueryable();
            ListDto<CategoryListItemDto> listDto = new ListDto<CategoryListItemDto>
            {
                ListItemDtos = query.Select(c=> new CategoryListItemDto
                {
                    Id = c.Id,
                    Name = c.Name
                }).Skip((page-1)*4).Take(4).ToList(),
                Count = query.Count()

            };
            return Ok(listDto);
        }



        [HttpPost("create")]
        public async Task<IActionResult> Create(CategoryPostDto categoryPostDto)
        {
            if (_context.Categories.Any(c => c.Name == categoryPostDto.Name)) return BadRequest();
            Category category = new Category
            {
                Name = categoryPostDto.Name
            };
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new { id = category.Id, categ = categoryPostDto });
        }



        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, CategoryPostDto categoryPostDto)
        {
            if(id == 0) return BadRequest();
            if(_context.Categories.Any(c=>c.Name == categoryPostDto.Name)) return BadRequest();
            Category current = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (current == null) return NotFound();
            _context.Entry(current).CurrentValues.SetValues(categoryPostDto);
            await _context.SaveChangesAsync();
            return StatusCode(200,new {category = categoryPostDto});
        }
    }
}
