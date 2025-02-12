using ContactsManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ContactsManager.Controllers
{
    public class HomeController : Controller
    {
        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }

        
    }
}
