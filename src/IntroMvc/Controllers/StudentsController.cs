using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IntroMvc.ModelBinders;
using IntroMvc.ValidationAttributes;
using IntroMvc.ViewModels.Students;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace IntroMvc.Controllers
{
    public class StudentsController : BaseController
    {
        private readonly IHostingEnvironment environment;

        public StudentsController(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public IActionResult Create()
        {
            var model = new StudentBindingModel
            {
                Name = new FullName { FirstName = "Pesho" },
                Type = StudentType.Fake,
            };
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm]
            // [Bind("Bio", "Name")]
            StudentBindingModel model,

            [FromHeader]
            string connection,
            
            [FromServices]
            ILogger<StudentsController> logger,
            
            [ModelBinder(typeof(GetAllHeadersModelBinder))]
            string allMyHeaders)
        {
            if (this.ModelState.IsValid)
            {
                // TODO: Persist
                var fileName = this.environment.WebRootPath + "/files/" + "1.jpg";
                using (var fileStream = new FileStream(fileName, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                return this.Json(model);
            }
            else
            {
                return this.View(model);
            }
        }
    }
}
