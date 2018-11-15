using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace IntroMvc.Services
{
    public interface IGreetingProvider
    {
        string GetGreeting();
    }

    public class GreetingProvider : IGreetingProvider
    {
        private readonly IConfiguration configuration;

        public GreetingProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public string GetGreeting()
        {
            return this.configuration["Greeting"];
        }
    }
}
