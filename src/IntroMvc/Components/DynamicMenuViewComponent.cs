using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace IntroMvc.Components
{
    [ViewComponent(Name = "menu")]
    public class DynamicMenuViewComponent : ViewComponent
    {
        private readonly IGreetingProvider greetingProvider;

        public DynamicMenuViewComponent(IGreetingProvider greetingProvider)
        {
            this.greetingProvider = greetingProvider;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var viewModel = new List<MenuItemViewModel>
            {
                new MenuItemViewModel {Url = "http://google.com", Title = "Google"},
                new MenuItemViewModel {Url = "http://facebook.com", Title = "Facebook"},
            };

            return this.View(viewModel);
        }
    }

    public class MenuItemViewModel
    {
        public string Title { get; set; }

        public string Url { get; set; }
    }
}
