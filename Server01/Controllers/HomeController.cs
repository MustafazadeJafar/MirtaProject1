using Microsoft.AspNetCore.Mvc;
using Server01.Models;
using System.Diagnostics;

namespace Server01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            object gg;
            using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "_templates/index.html")))
            {
                gg = sr.ReadToEnd().HtmlToCshtml();
                //using (StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "/Views/Home/Index.cshtml"))
                //{
                //    sw.Write(gg);
                //    sw.Close();
                //    //sw.Dispose();
                //}
                sr.Close();
            }

            return View(gg);
        }

        public IActionResult Lobby()
        {
            return View();
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
    }
}
