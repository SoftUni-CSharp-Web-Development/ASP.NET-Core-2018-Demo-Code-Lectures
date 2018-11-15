using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace IntroMvc.Filter
{
    public class MyResourceFilter : IResourceFilter
    {
        private readonly ILogger<MyResourceFilter> logger;

        public MyResourceFilter(ILogger<MyResourceFilter> logger)
        {
            this.logger = logger;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            this.logger.LogInformation($"OnResourceExecuting: {context.HttpContext.Request.Path.Value}");
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            this.logger.LogInformation($"OnResourceExecuting: {context.Result.GetType().Name}");
        }
    }
}
