using ProjectDatabase.Models;
using ProjectDatabase.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projects.Models.Tables
{
    class ProjectService : ITableService<Project>
    {
        public void Delete(int[] id)
        {
            using (var connection = new SqlDataContext())
            {
                try
                {
                    List<Project> list = new List<Project>();
                    foreach (int index in id)
                    {
                        list.AddRange((from p in connection.Projects where p.Id == index select p));
                    }
                    connection.Projects.RemoveRange(list);
                    connection.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                MessageBox.Show("Записи удалены", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public bool Insert(Project obj)
        {
            using (var connection = new SqlDataContext())
            {
                try
                {
                    connection.Projects.Add(obj);
                    connection.SaveChanges();
                }
                catch(Exception ex)
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
                var list = (from p in connection.Projects select new { p.Id, Название = p.Name, Страна = p.Country.Name, Дата = p.DateCreate }).ToList();
                return (dynamic)list;
            }
        }

        public void Update(Project obj)
        {
            using (var connection = new SqlDataContext())
            {
                try
                {
                    var project = (from p in connection.Projects where p.Id == obj.Id select p);
                    var item = project.SingleOrDefault();
                    item.Id = obj.Id;
                    item.CountryId = obj.CountryId;
                    item.Name = obj.Name;
                    item.DateCreate = obj.DateCreate;
                    connection.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                MessageBox.Show("Запись добавлена", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
    }
}
