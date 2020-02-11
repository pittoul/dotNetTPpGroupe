using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TPLibrairieEnGroupe.Models;
using TPLibrairiev03.Abstraction;

namespace TPLibrairieEnGroupe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleRepository articleRepository;

        public HomeController(ILogger<HomeController> logger, IArticleRepository articleRepository)
        {
            _logger = logger;
            this.articleRepository = articleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult VueTest()
        {
            var unArticle = articleRepository.FindById(4);

            return View(unArticle);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
