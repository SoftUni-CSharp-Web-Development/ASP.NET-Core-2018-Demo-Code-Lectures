using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntroMvc.Data;

namespace IntroMvc.Services
{
    public interface IUsersService
    {
        int Count();
    }

    class UsersService : IUsersService
    {
        private readonly ApplicationDbContext db;

        public UsersService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int Count()
        {
            return this.db.Users.Count();
        }
    }
}
