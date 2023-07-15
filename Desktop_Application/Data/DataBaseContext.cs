using Desktop_Application.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop_Application.Data
{
    public class DataBaseContext : DbContext
    {
        private readonly string _path = @"D:\Semester 3\Indivual project\Individual_GUI_Project\user.db";

        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={_path}")
                          .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information)
                          .EnableSensitiveDataLogging();
        }

        public DbSet<Student> Students { get; set; }

    }
}

