using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using ProjectDatabase.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDatabase.Models
{
    class SqlDataContext:DbContext
    {
        public SqlDataContext()
        {
            Database.EnsureCreated();

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Project.db;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity < Country> ()
                .HasIndex(c => new { c.Name })
                .IsUnique(true);
        }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
