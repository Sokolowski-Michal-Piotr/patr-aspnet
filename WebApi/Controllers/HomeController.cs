using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return Redirect("swagger");
        }

        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
