using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroMvc.Services;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IntroMvc.TagHelpers
{
    [HtmlTargetElement("h1", Attributes = "asp-name")]
    [HtmlTargetElement("h2", Attributes = "asp-name")]
    public class GreetingHeadingTagHelper : TagHelper
    {
        private readonly IGreetingProvider greetingProvider;

        // asp-name
        public string AspName { get; set; }

        public GreetingHeadingTagHelper(IGreetingProvider greetingProvider)
        {
            this.greetingProvider = greetingProvider;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.Content.SetContent($"{this.greetingProvider.GetGreeting()}, {this.AspName}");
        }
    }
}
