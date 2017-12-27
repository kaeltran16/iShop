using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.Controller
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        public IActionResult Index()
        {
            return View();
        }

//        public IActionResult Error()
//        {
//            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
//            return View();
//        }
    }
}