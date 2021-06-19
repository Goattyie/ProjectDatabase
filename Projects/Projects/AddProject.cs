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
                comboBox1.Text = id + "|" + project.Country.Name;
                comboBox1.Items.Add("Добавить");
                comboBox1.Items.Add(comboBox1.Text);
                comboBox1.SelectedIndex = 1;
            }
            Edit = true;
        }
        bool Edit = false;
        Project project;
        public event Action<object, EventArgs> Success;
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text.Length == 0 || dateTimePicker1.Value.Date > DateTime.Now.Date || comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Укажите данные правильно.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            //Если все нормально
            int countryId = int.Parse(comboBox1.SelectedItem.ToString().Split('|')[0]);//Из строки со странами вытаскиваем айди
            if (!Edit)//Если добавляем новую запись
            {
                project = new Project(textBox1.Text, dateTimePicker1.Value.Date.ToString().Split(' ')[0], countryId);
                if (new ProjectService().Insert(project))
                {
                    comboBox1.Text = null;
                    textBox1.Text = null;
                    dateTimePicker1.Value = DateTime.Now.Date;
                    Success?.Invoke(this, new EventArgs());
                }
            }
            else
            {
                project.Name = textBox1.Text;
                project.DateCreate = dateTimePicker1.Value.Date.ToString().Split(' ')[0];
                project.CountryId = countryId;
                new ProjectService().Update(project);
                Success?.Invoke(this, new EventArgs());
            }

        }
        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Добавить");
            //Добавление стран в комбо бокс
            using (var connection = new SqlDataContext())
            {
                foreach (Country item in connection.Countries)
                {
                    comboBox1.Items.Add(item.Id + "|" + item.Name);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                //добавление новой страны
                var addCountry = new AddCountry();
                addCountry.ShowDialog(this);
                comboBox1.Items.Clear(); // но это удалит все элементы..
            }
        }
    }
}
