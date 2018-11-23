using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IntroMvc.Controllers
{
    public class TempDataDemoController : BaseController
    {
        public IActionResult Form()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Form(string data)
        {
            this.TempData["ThankYouMessage"] = $"Thank you for submitting: \"{data}\"";
            return this.RedirectToAction("ThankYou");
        }

        public IActionResult ThankYou()
        {
            this.ViewData["ThankYouMessage"] = this.TempData["ThankYouMessage"];
            return this.View();
        }
    }
}
