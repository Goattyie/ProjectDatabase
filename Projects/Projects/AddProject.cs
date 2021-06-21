using ProjectDatabase.Models;
using ProjectDatabase.Models.Tables;
using Projects.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projects
{
    public partial class AddProject : Form
    {
        public AddProject()
        {
            InitializeComponent();
        }
        public void LoadCountries() 
        {
            var list = new CountryService().Select();
            lookUpEdit2.Properties.DataSource = list;
        }
        public AddProject(Project project)
        {
            InitializeComponent();
            this.project = project;
            textBox1.Text = project.Name;
            dateTimePicker1.Value = DateTime.Parse(project.DateCreate);
            using (var connection = new SqlDataContext())
            {
                var id_list = (from p in connection.Projects where p.Id == project.Id select p.CountryId);
                int id = id_list.Single();
                var item = connection.Countries.Find(id);
                lookUpEdit2.Properties.DisplayMember = item.Name;
                lookUpEdit2.Properties.ValueMember = item.Id.ToString();
            }
            Edit = true;
        }
        bool Edit = false;
        Project project;
        public event Action<object, EventArgs> Success;
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0 || dateTimePicker1.Value.Date > DateTime.Now.Date || lookUpEdit2.EditValue == null)
            {
                MessageBox.Show("Укажите данные правильно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Если все нормально
            var country = new Country();
            if (!Edit)//Если добавляем новую запись
            {
                project = new Project(textBox1.Text, dateTimePicker1.Value.Date.ToString().Split(' ')[0], (int)lookUpEdit2.EditValue);
                if (new ProjectService().Insert(project))
                {
                    textBox1.Text = null;
                    dateTimePicker1.Value = DateTime.Now.Date;
                    Success?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                project.Name = textBox1.Text;
                project.DateCreate = dateTimePicker1.Value.Date.ToString().Split(' ')[0];
                project.CountryId = (int)lookUpEdit2.EditValue;
                new ProjectService().Update(project);
                Success?.Invoke(this, new EventArgs());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AddCountry().ShowDialog(this);
            LoadCountries();
        }

        private void AddProject_Load(object sender, EventArgs e)
        {
            LoadCountries();
            lookUpEdit2.Properties.DisplayMember = "Название";
            lookUpEdit2.Properties.ValueMember = "Id";
        }
    }
}
