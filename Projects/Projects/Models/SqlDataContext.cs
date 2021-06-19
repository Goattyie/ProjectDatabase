using System.Data.SQLite;
using ProjectDatabase.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDatabase.Models
{
    class SqlDataContext:DbContext
    {
        public SqlDataContext()
            : base(new SQLiteConnection($"Data Source ={System.IO.Directory.GetCurrentDirectory()}\\Project.db"), true)
        { }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Country> Countries { get; set; }
    }
}
