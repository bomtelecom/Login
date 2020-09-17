using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Login.Entities
{
    public class UserContext : DbContext
    {
        private IConfiguration configuration { get; }
        public UserContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set;}
    }
}
