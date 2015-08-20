using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Postgres_Manager
{
    public partial class Sequence : Form
    {
        private string DDL;
        private NpgsqlConnection conn;
        private Postgres_Connection PC;
        public Sequence(NpgsqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            comboBox1.Items.Add("CACHE");
            comboBox1.Items.Add("NO CACHE");
            PC = new Postgres_Connection();
        }

        private void Sequence_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DDL = "";
            DDL += "CREATE SEQUENCE ";
            DDL += textBox1.Text;
            DDL += " INCREMENT BY ";
            DDL += textBox2.Text;
            DDL += " \n";
            DDL += "MINVALUE ";
            DDL += textBox3.Text;
            DDL += " MAXVALUE ";
            DDL += textBox4.Text;
           // if (comboBox1.GetItemText(comboBox1.SelectedItem) == "CACHE")
            PC.exec_Sql(DDL, conn);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDL = "";
            DDL += " CREATE SEQUENCE ";
            DDL += textBox1.Text;
            DDL += " INCREMENT BY ";
            DDL += textBox2.Text;
            DDL += " \n";
            DDL += "MINVALUE ";
            DDL += textBox3.Text;
            DDL += " MAXVALUE ";
            DDL += textBox4.Text;
            richTextBox1.Text = DDL;
        }
    }
}
