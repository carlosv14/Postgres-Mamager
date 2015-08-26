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
    public partial class Database : Form
    {
        Postgres_Connection pc = new Postgres_Connection();
        NpgsqlConnection conn = null;
        private RichTextBox rt;
        public string DDL;
        public Database(NpgsqlConnection conn, RichTextBox rt)
        {

            InitializeComponent();
            DDL = "";
            this.conn = conn;
            this.rt = rt;
        }

        private void Database_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = pc.exec_Sql("select * from pg_tablespace", conn, rt); ;
            comboBox1.DisplayMember = "spcname";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DDL = "CREATE DATABASE " + textBox1.Text + " TABLESPACE " + comboBox1.GetItemText(comboBox1.SelectedItem);
            pc.exec_Sql(DDL
               , conn,
                rt);
            this.Close();
        }
    }
}
