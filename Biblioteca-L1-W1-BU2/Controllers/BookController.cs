using Biblioteca_L1_W1_BU2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca_L1_W1_BU2.Controllers
{
    public class BookController : Controller
    {
        private readonly BookService _productService;

        public BookController(BookService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> Index()
        {   
            var productList = await _productService.GetBooksAsync();
            return View(productList);
        }
    }
}
