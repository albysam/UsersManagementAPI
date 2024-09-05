using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using UsersManagementAPI.Models;

namespace UsersManagementAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
        {
        }


        public DbSet<User> Users { get; set; }
    }
}