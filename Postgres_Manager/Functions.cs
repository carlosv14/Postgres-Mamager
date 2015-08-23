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
using ScintillaNET;


namespace Postgres_Manager
{
    public partial class Functions : Form
    {
     
       
        private string DDL = null;
        private Scintilla RT;
        public Functions( Scintilla RT)
        {
            InitializeComponent();
            this.RT = RT;
            

            dataGridView1.Columns.Add("Nombre","Nombre");
            dataGridView1.Columns.Add("Tipo","Tipo");
        }

        private void Functions_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DDL += "CREATE OR REPLACE FUNCTION ";
            DDL += textBox1.Text;
            DDL += "( ";
            for (int i = 0; i < dataGridView1.Rows.Count-1; i++)
            {
                DDL += dataGridView1.Rows[i].Cells["Nombre"].Value.ToString();
                DDL += " ";
                DDL += dataGridView1.Rows[i].Cells["Tipo"].Value.ToString();
                if(i< dataGridView1.Rows.Count-2)
                    DDL += ", ";
            }
            DDL += " )";
            DDL += " RETURNS ";
            DDL += comboBox1.GetItemText(comboBox1.SelectedItem);
            DDL += " AS $$ \n";
            DDL += "BEGIN\n";
            DDL += "END\n";
            DDL += "$$ LANGUAGE plpgsql;";
            RT.Text = DDL;
            RT.Enabled = true;
            this.Close();
        }
    }
}
