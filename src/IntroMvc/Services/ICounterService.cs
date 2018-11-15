using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntroMvc.Services
{
    public class CounterService
    {
        private static int count = 0;

        public CounterService()
        {
            count++;
        }

        public int GetCount()
        {
            return count;
        }
    }
}
