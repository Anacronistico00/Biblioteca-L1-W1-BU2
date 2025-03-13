using Biblioteca_L1_W1_BU2.Data;
using Biblioteca_L1_W1_BU2.Models;
using Biblioteca_L1_W1_BU2.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<bool> SaveAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
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

        public async Task<bool> AddBookAsync(AddBookViewModel addBookViewModel)
        {
            try
            {
                var fileName = addBookViewModel.CoverPath.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Images", fileName);

                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await addBookViewModel.CoverPath.CopyToAsync(stream);
                }

                var webPath = Path.Combine("Uploads", "Images", fileName);

                var book = new Book()
                {
                    Title = addBookViewModel.Title,
                    Author = addBookViewModel.Author,
                    Genre = addBookViewModel.Genre,
                    Available = addBookViewModel.Available,
                    CoverUrl = webPath
                };
                _context.Books.Add(book);

                return await SaveAsync();
            }
            catch
            {
                return false;
            }
        }

        public async Task<BookDetailsViewModel> GetBookByIdAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return new BookDetailsViewModel()
                {
                    Id = Guid.Empty,
                    Title = string.Empty,
                    Author = string.Empty,
                    Genre = string.Empty,
                    Available = false,
                    CoverUrl = string.Empty
                };
            }

            var bookDetails = new BookDetailsViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Available = book.Available,
                CoverUrl = book.CoverUrl
            };

            return bookDetails;
        }

        public async Task<BookListViewModel> GetBookBySearchAsync(string searched)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searched))
                {
                    return await GetBooksAsync();
                }

                var bookList = new BookListViewModel
                {
                    Books = await _context.Books
                        .Where(b => b.Title.Contains(searched) ||
                                    b.Author.Contains(searched) ||
                                    b.Genre.Contains(searched))
                        .ToListAsync()
                };

                return bookList;
            }
            catch
            {
                return new BookListViewModel { Books = new List<Book>() };
            }
        }

        public async Task<EditBookViewModel> GetBookDetailsByIdAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return new EditBookViewModel()
                {
                    Id = Guid.Empty,
                    Title = string.Empty,
                    Author = string.Empty,
                    Genre = string.Empty,
                    Available = false,
                    CoverUrl = string.Empty
                };
            }

            var bookDetails = new EditBookViewModel()
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Available = book.Available,
                CoverUrl = book.CoverUrl
            };

            return bookDetails;
        }

        [HttpPost]
        public async Task<bool> UpdateBookWithPathAsync(EditBookViewModel edit, string oldPath)
        {
            string webPath;
            if (edit.CoverPath != null)
            {
                var fileName = edit.CoverPath.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Uploads", "Images", fileName);
                await using (var stream = new FileStream(path, FileMode.Create))
                {
                    await edit.CoverPath.CopyToAsync(stream);
                }

                webPath = Path.Combine("Uploads", "Images", fileName);
            }
            else
            {
                webPath = oldPath;
            }

                var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == edit.Id);

            if (book == null)
            {
                return false;
            }

            book.Title = edit.Title;
            book.Author = edit.Author;
            book.Genre = edit.Genre;
            book.Available = edit.Available;
            book.CoverUrl = webPath;

            return await SaveAsync();
        }
        public async Task UpdateBookAsync(BookDetailsViewModel book)
        {
            var bookToUpdate = await _context.Books.FirstOrDefaultAsync(b => b.Id == book.Id);
            if (bookToUpdate == null)
            {
                return;
            }
            bookToUpdate.Title = book.Title;
            bookToUpdate.Author = book.Author;
            bookToUpdate.Genre = book.Genre;
            bookToUpdate.Available = book.Available;
            bookToUpdate.CoverUrl = book.CoverUrl;
            await SaveAsync();
        }

        public async Task<bool> DeleteBookByIdAsync(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (book == null)
            {
                return false;
            }
            _context.Books.Remove(book);
            return await SaveAsync();
        }

    }
}