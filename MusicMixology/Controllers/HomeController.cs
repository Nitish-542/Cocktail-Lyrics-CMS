using Microsoft.AspNetCore.Mvc;
using MusicMixology.Models;
using System.Diagnostics;

namespace MusicMixology.Controllers
{
    /// <summary>
    /// Controller responsible for handling general home-related requests.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        /// <summary>
        /// Constructor to inject the logger dependency.
        /// </summary>
        /// <param name="logger">Logger instance for HomeController</param>
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles the request to the home/index page.
        /// </summary>
        /// <returns>The Index view</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Handles the request to the privacy policy page.
        /// </summary>
        /// <returns>The Privacy view</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Handles errors and displays the error view.
        /// </summary>
        /// <returns>Error view with error details</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
