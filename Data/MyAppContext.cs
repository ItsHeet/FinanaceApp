using Microsoft.EntityFrameworkCore;
using MyApp.Models;

namespace MyApp.Data
{
    public class MyAppContext : DbContext
    {
        public MyAppContext(DbContextOptions<MyAppContext> options): base(options){}


            public DbSet<Item1> Items { get; set; }


            public DbSet<Student> Students { get; set; }

    }
}
    



    