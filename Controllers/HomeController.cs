using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using final_BS.Models;
using final_BS.Data.Models;
using Microsoft.EntityFrameworkCore;
using final_BS.Data;

namespace final_BS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private BookContext _context = new BookContext();


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(string sortOrder, string searchString,  int sz=3, int page=1)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AuthorSortParm"] = sortOrder == "Author" ? "author_desc" : "Author";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;

            var books = from s in _context.Books
                           select s;   

            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => s.Title.Contains(searchString)
                                       || s.Author.Contains(searchString)
                                       || s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    books = books.OrderByDescending(s => s.Title);
                    break;
                case "Author":
                    books = books.OrderBy(s => s.Author);
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author);
                    break;
                case "Price":
                    books = books.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    books = books.OrderByDescending(s => s.Price);
                    break;
                default:
                    books = books.OrderBy(s => s.Title);
                    break;
            }
            var count = books.Count();
            List<Book> items = books.Skip((page - 1) * sz).Take(sz).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, sz);
            /* IndexViewModel viewModel = new IndexViewModel
             {
                 PageViewModel = pageViewModel,
                 Books = (IEnumerable<Book>)items,
                 pageSize = sz
             };*/
            List<Book> items2 =  items;

            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Books = items2,
                pageSize = sz
            };
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public ActionResult Message(string s)
        {
            ViewBag.message = s;
            return View();
        }
    }
}
