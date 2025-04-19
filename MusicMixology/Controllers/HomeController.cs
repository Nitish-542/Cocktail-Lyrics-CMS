using Microsoft.AspNetCore.Mvc;
using MusicMixology.Models;
using System.Diagnostics;

namespace MusicMixology.Controllers
{
    // Home controller for handling home page and error-related actions.
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Constructor for dependency injection of the logger.
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>View for the homepage.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the privacy policy page.
        /// </summary>
        /// <returns>View for the privacy page.</returns>
        public IActionResult Privacy()
        {
            return View();
        }

        /// <summary>
        /// Displays an error page when an error occurs.
        /// </summary>
        /// <returns>View displaying error details.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            // Includes request ID or trace identifier for debugging.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
