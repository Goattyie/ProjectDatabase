using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDatabase.Models.Tables
{
    public class Project
    {
        public int Id { get; set; }
        public string DateCreate { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; }
        public Project(string name, string date, int country_id)
        {
            Name = name;
            DateCreate = date;
            CountryId = country_id;
        }
        public Project(int id, string name, string country, string date)
        {
            Id = id;
            Name = name;
            DateCreate = date;
            Country = new Country(country);
        }
        public Project() { }
    }
}
