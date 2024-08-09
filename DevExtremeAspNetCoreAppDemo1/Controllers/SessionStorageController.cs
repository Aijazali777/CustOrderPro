using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustOrderPro.Controllers
{
    public class SessionStorageController : Controller
    {
        public IActionResult LocalAndSessionStorage()
        {
            return View();
        }
    }
}
