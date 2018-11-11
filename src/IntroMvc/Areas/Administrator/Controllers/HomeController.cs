using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroMvc.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace IntroMvc.Areas.Administrator.Controllers
{
    public class HomeController : AdministratorController
    {
        public IActionResult Index()
        {
            return View();
        }
    }

    [Area("Administrator")]
    public class AdministratorController : BaseController
    {
    }
}