using LibraryApplication.Controllers.Dto;
using LibraryApplication.Models;
using LibraryApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public ActionResult<List<Book>> Get() =>
            _bookService.Get();

        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        [HttpGet("search/{searchTerm}")]
        public ActionResult<List<Book>> Search(string searchTerm) =>
            _bookService.Search(searchTerm);

        [HttpPost]
        public IActionResult Create(BookDTO newBook)
        {
            var book = new Book { Name = newBook.Name, Author = newBook.Author };

            _bookService.Create(book);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update(int id, BookDTO bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, book.Update(bookIn.Name, bookIn.Author));

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Remove(book.Id);

            return NoContent();
        }

        [HttpPost("{id}/loanTo/{user}")]
        public IActionResult Borrow(int id, string user)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            if (user == null)
            {
                return BadRequest();
            }

            book.LoanTo(user);

            _bookService.Update(id, book);

            return Ok();
        }

        [HttpPost("{id}/return")]
        public IActionResult Return(int id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            book.Return();

            _bookService.Update(id, book);

            return Ok();
        }

        [HttpGet("lastId")]
        public ActionResult<int> LastId()
        {
            return _bookService.LastId();
        }
    }
}
