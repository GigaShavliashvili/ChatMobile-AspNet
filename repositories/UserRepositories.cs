using chatmobile.DB;
using chatmobile.entites;
using Microsoft.EntityFrameworkCore;

namespace chatmobile.repositories
{


    public class UserRepositories(DbContext context) : Repositories<User>(context)
    {
        public AppDbContext Context => (_context as AppDbContext)!;
    }
}