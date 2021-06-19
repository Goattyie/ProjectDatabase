using ProjectDatabase.Models;
using ProjectDatabase.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projects.Models.Tables
{
    class CountryService : ITableService<Country>
    {
        public void Delete(int[] id)
        {
            //Можно добавить функциональность
            throw new NotImplementedException();
        }

        public bool Insert(Country obj)
        {
            using (var connection = new SqlDataContext())
            {
                try
                {
                    connection.Countries.Add(obj);
                    connection.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                MessageBox.Show("Запись добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return true;
            }
        }

        public dynamic Select()
        {
            using (var connection = new SqlDataContext())
            {
                var list = (from c in connection.Countries select new { c.Id, Название = c.Name}).ToList();
                return (dynamic)list;
            }
        }

        public void Update(Country obj)
        {
            //Можно добавить функциональность
            throw new NotImplementedException();
        }
    }
}
