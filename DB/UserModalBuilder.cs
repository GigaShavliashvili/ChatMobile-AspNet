using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatmobile.entites;
using Microsoft.EntityFrameworkCore;

namespace chatmobile.DB
{
    public class UserModalBuilder
    {
        public static void CreateUserModalBuilder(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.UserName).IsUnique();
        }
    }
}