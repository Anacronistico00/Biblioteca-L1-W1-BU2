using Biblioteca_L1_W1_BU2.Models;
using Biblioteca_L1_W1_BU2.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Biblioteca_L1_W1_BU2.Controllers
{
    public class PrestitiController : Controller
    {
        private readonly LoanService _loanService;
        private readonly BookService _bookService;

        public PrestitiController(LoanService loanService, BookService bookService)
        {
            _loanService = loanService;
            _bookService = bookService;
        }

        public async Task<IActionResult> Index()
        {
            var prestiti = await _loanService.GetLoansAsync();
            return View(prestiti);
        }

        public async Task<IActionResult> Returned()
        {
            var prestiti = await _loanService.GetReturnedAsync();
            return View(prestiti);
        }


        public async Task<IActionResult> Overdue()
        {
            var prestiti = await _loanService.GetOverDueAsync();

            return View(prestiti);
        }

        [HttpGet("/prestito/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var prestito = await _loanService.GetLoanByIdAsync(id);
            if (prestito == null)
            {
                return NotFound();
            }
            return View(prestito);
        }

        public async Task<IActionResult> Create()
        {
            var libri = await _bookService.GetBooksAsync();
            ViewBag.BookList = new SelectList(libri.Books.Where(b => b.Available), "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Prestito prestito)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _loanService.AddLoanAsync(prestito);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

             var libri = await _bookService.GetBooksAsync();
            ViewBag.BookList = new SelectList(libri.Books.Where(b => b.Available), "Id", "Title", prestito.BookId);
            return View(prestito);
        }

        [HttpGet("/edit/{id:guid}", Name = "Edit")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var prestito = await _loanService.GetLoanByIdAsync(id);
            if (prestito == null)
            {
                return NotFound();
            }
            var libri = await _bookService.GetBooksAsync();
            ViewBag.BookList = new SelectList(libri.Books.Where(b => b.Available), "Id", "Title", prestito.BookId);
            return View(prestito);
        }

        [HttpPost("/edit/{id:guid}/save", Name = "SaveEdit")]
        public async Task<IActionResult> Edit(Guid id, Prestito prestito)
        {
            if (id != prestito.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var oldLoan = await _loanService.GetLoanByIdAsync(id);

                    if (oldLoan != null && oldLoan.Book != null)
                    {
                        oldLoan.Book.Available = true;

                        await _bookService.UpdateAvailabilityAsync(oldLoan.Book);

                        var newBook = await _bookService.GetBookByIdAsync(prestito.BookId);
                        if (newBook != null)
                        {
                            newBook.Available = false;

                            await _bookService.UpdateBookAsync(newBook);
                        }
                    }

                    await _loanService.UpdateLoanAsync(prestito);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            var libri = await _bookService.GetBooksAsync();
            ViewBag.BookList = new SelectList(libri.Books.Where(b => b.Available), "Id", "Title", prestito.BookId);
            return View(prestito);
        }

        [HttpGet("/return/{id:guid}")]
        public async Task<IActionResult> Return(Guid id)
        {
            var prestito = await _loanService.GetLoanByIdAsync(id);
            if (prestito == null)
            {
                return NotFound();
            }
            return View(prestito);
        }

        [HttpGet("/prestito/return/{id:guid}/confirmed")]
        public async Task<IActionResult> ReturnConfirmed(Guid id)
        {
            try
            {
                await _loanService.ReturnLoanAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                var prestito = await _loanService.GetLoanByIdAsync(id);
                return View(prestito);
            }
        }
    }
}
