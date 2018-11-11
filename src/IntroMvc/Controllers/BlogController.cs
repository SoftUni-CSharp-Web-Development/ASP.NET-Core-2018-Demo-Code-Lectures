using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IntroMvc.Controllers
{
    public class BlogController : BaseController
    {
        public IActionResult ByDate(int year, int month, int day)
        {
            return this.Content($"{year}/{month}/{day}");
        }
    }
}
