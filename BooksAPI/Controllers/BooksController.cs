using BooksAPI.DAL;
using BooksAPI.DTOs;
using BooksAPI.DTOs.Book;
using BooksAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Book book = _context.Books.Include(c=>c.Category).ThenInclude(c=>c.Books).FirstOrDefault(b=>b.Id == id);
            if(book == null) return NotFound();
            BookGetDto bookGetDto = new BookGetDto
            {
                Id = book.Id,
                Name = book.Name,
                Author = book.Author,
                releaseDate = book.releaseDate,
                Pages = book.Pages,
                Category = new CategoryAndBookGetDto
                {
                    Id = book.Id,
                    Name = book.Category.Name,
                    BooksCount = book.Category.Books.Count
                }
            };
            return Ok(bookGetDto);
        }

        [HttpGet("")]
        public IActionResult GetAll(int page = 1)
        {
            var query = _context.Books.AsQueryable();
            List<Book> books = _context.Books.Skip((page-1)*4).Take(4).ToList();
            ListDto<BookListItemDto> bookListDto = new ListDto<BookListItemDto>
            {
                ListItemDtos = query.Include(c => c.Category).ThenInclude(c => c.Books).Select(b => new BookListItemDto
                {
                    Id = b.Id,
                    Name = b.Name,
                    Author = b.Author,
                    releaseDate = b.releaseDate,
                    Pages = b.Pages,
                    Category = new CategoryAndBookGetDto
                    {
                        Id = b.CategoryId,
                        Name = b.Category.Name,
                        BooksCount = b.Category.Books.Count
                    }

                }).ToList(),
                Count = books.Count
            };

            return Ok(bookListDto);


        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(BookPostDto postDto)
        {
            if(postDto is null) return BadRequest();
            if (!_context.Categories.Any(c => c.Id == postDto.CategoryId)) return BadRequest(); 
            Book book = new Book
            {
                Name = postDto.Name,
                Author = postDto.Author,
                releaseDate = postDto.releaseDate,
                Pages = postDto.Pages,
                CategoryId = postDto.CategoryId 
            };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status200OK, new {id = book.Id,book = postDto});
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, BookPostDto bookPostDto)
        {
            if(id == 0) return BadRequest();
            Book book = _context.Books.FirstOrDefault(b => b.Id == id);
            _context.Entry(book).CurrentValues.SetValues(bookPostDto);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0) return BadRequest();
            Book current = _context.Books.FirstOrDefault(b => b.Id == id);
            if (current == null) return NotFound();
            _context.Books.Remove(current);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
