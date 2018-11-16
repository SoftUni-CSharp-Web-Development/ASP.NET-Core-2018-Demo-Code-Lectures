using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace IntroMvc.ModelBinders
{
    public class GetAllHeadersModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var stringBuilder = new StringBuilder();
            foreach (var header in bindingContext.HttpContext.Request.Headers)
            {
                stringBuilder.AppendLine(header.ToString());
            }

            bindingContext.Result = ModelBindingResult.Success(stringBuilder.ToString());

            return Task.CompletedTask;
        }
    }
}
