using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace IntroMvc.Filter
{
    public class AddHeaderActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // context.Result = new ContentResult() { Content = "Hello from the filter." };
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // context.HttpContext.Response.Headers.Add("X-Debug", context.HttpContext.User?.Identity?.Name ?? "Unauthorized!");
        }
    }
}
