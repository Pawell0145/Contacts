using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Lab_7_9_Contacts
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=contacts.db");
        }
    }

    public class Person
    {
        public int Id { get; set; } // Primary Key for EF Core
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public int Phone { get; set; }
    }
}
