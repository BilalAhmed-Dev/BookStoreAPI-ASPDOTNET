
using AutoMapper;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.Book;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class BooksController : ControllerBase
    {
        private readonly BookstoreContext _context;
        private readonly IMapper mapper;


        public BooksController(BookstoreContext context)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: /api/Books/getBooks
        [HttpGet("getBooks")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return mapper.Map<List<BookDTO>>(books);
        }


        // GET: api/Books/getBook
        [HttpGet("getBook")]
        [Authorize]
        public async Task<ActionResult<BookDTO>> GetBook([FromQuery(Name = "bookId")] int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return mapper.Map<BookDTO>(book);
        }

        // GET: api/Books/search
        // GET: api/Books/search
        [HttpGet("search")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<BookDTO>>> SearchBooks(
            [FromQuery] string? title,
            [FromQuery] string? author,
            [FromQuery] string? genre)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(title))
                query = query.Where(b => b.Title.ToLower().Contains(title.ToLower()));

            if (!string.IsNullOrEmpty(author))
                query = query.Where(b => b.Author.ToLower().Contains(author.ToLower()));

            if (!string.IsNullOrEmpty(genre))
                query = query.Where(b => b.Genre.ToLower().Contains(genre.ToLower()));

            var results = await query.ToListAsync();
            return mapper.Map<List<BookDTO>>(results);
        }


        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("editbook/{id}")]
        [Authorize]
        public async Task<IActionResult> PutBook(int id, BookDTO bookDTO)
        {
            if (id != bookDTO.BookId)
            {
                return BadRequest();
            }

            _context.Entry(bookDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BookDTO>> PostBook(BookDTO bookDTO)
        {
            var book = mapper.Map<Book>(bookDTO);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.BookId }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.BookId == id);
        }
    }
}
