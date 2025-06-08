using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Ciezarki.MVVM.Model
{
    internal class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
    

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User(1, "abc@eu", "123", "chuj"));
               
        }
    }
}
