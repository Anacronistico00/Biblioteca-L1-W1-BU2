using Biblioteca_L1_W1_BU2.Data;
using Biblioteca_L1_W1_BU2.Models;
using Biblioteca_L1_W1_BU2.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca_L1_W1_BU2.Services
{
    public class BookService
    {
        private readonly BibliotecaEfCoreDbContext _context;

        public BookService(BibliotecaEfCoreDbContext context)
        {
            _context = context;
        }

        public async Task<BookListViewModel> GetBooksAsync()
        {
            try
            {
                var bookList = new BookListViewModel();
                bookList.Books = await _context.Books.ToListAsync();
                return bookList;
            } catch
            {
                return new BookListViewModel() { Books = new List<Book>() };
            }
        }
    }
}