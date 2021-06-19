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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var list = new ProjectService().Select();
            dataGridView1.DataSource = list;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            var addProject = new AddProject();
            addProject.Show();
            addProject.Success += Update;
        }
        private void Update(object sender, EventArgs e)
        {
            var list = new ProjectService().Select();
            dataGridView1.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;
            var project = new Project(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()), dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), 
                dataGridView1.SelectedRows[0].Cells[2].Value.ToString(), dataGridView1.SelectedRows[0].Cells[3].Value.ToString());

            var addProject = new AddProject(project);
            addProject.Show();
            addProject.Success += Update;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
                return;

            int[] idArray = new int[dataGridView1.SelectedRows.Count];
            for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                idArray[i] = int.Parse(dataGridView1.SelectedRows[i].Cells[0].Value.ToString());
            new ProjectService().Delete(idArray);
            Update(sender, new EventArgs());
        }
    }
}
