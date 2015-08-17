using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Postgres_Manager
{
    public partial class Functions : Form
    {
        NpgsqlConnection conn = null;
        Postgres_Connection pc = null;
        public Functions(NpgsqlConnection conn)
        {
            InitializeComponent();
            DataGridViewColumn nombre = new DataGridViewColumn();
            nombre.HeaderText="Nombre";
            nombre.Name = "NOMBRE";
            DataGridViewColumn type = new DataGridViewColumn();
            type.HeaderText = "DATA TYPE";
            type.Name = "type";
            dataGridView1.Columns.Add(nombre);
            dataGridView1.Columns.Add(type);
        }

        private void Functions_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
