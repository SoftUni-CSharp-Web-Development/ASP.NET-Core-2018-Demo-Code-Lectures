using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using IntroMvc.Data;
using Microsoft.AspNetCore.Mvc;
using IntroMvc.Models;
using IntroMvc.Services;
using IntroMvc.ViewModels.Home;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace IntroMvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGreetingProvider configuration;
        private readonly IUsersService usersService;

        public HomeController(
            IGreetingProvider configuration,
            IUsersService usersService)
        {
            this.configuration = configuration;
            this.usersService = usersService;
        }

        public IActionResult GetByUsername(string username)
        {
            return this.Content(username);
        }

        public IActionResult Index(int id)
        {
            ViewBag.Name = "Niki";
            ViewData["HI"] = this.configuration.GetGreeting() + ", " + this.User.Identity.Name;
            ViewData["UsersCount"] = this.usersService.Count();
            var model = new IndexViewModel {Message = this.configuration.GetGreeting()};
            return this.View(model);
        }

        [HttpGet("[controller]/Get")]
        public IActionResult About(int number)
        {
            ViewData["Message"] = number;
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
