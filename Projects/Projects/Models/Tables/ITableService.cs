using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects.Models.Tables
{
    interface ITableService<T> where T:class
    {
        //Select для импорта в DataGridView
        dynamic Select();
        bool Insert(T obj);
        void Delete(int[] id);
        void Update(T obj);
    }
}
