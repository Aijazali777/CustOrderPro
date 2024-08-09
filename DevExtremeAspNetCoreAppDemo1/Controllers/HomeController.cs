using CustOrderPro.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustOrderPro.Controllers
{
    public class HomeController : Controller
    {
        #region Home ActionMethods
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Contact()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
        #endregion
    }
}
