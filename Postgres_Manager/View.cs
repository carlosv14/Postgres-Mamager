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
    public partial class View : Form
    {
        private NpgsqlConnection conn;
        private Postgres_Connection pc; 
        public View(NpgsqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
            this.pc = new Postgres_Connection();

        }

        private void View_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ddl;
            ddl = "CREATE VIEW ";
            ddl += textBox1.Text + " AS \n";
            ddl += richTextBox1.Text;
            pc.exec_Sql(ddl, conn);
            this.Close();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            richTextBox2.Clear();
            string ddl;
            ddl = "CREATE VIEW ";
            ddl += textBox1.Text + " AS \n";
            ddl += richTextBox1.Text;
            richTextBox2.Text = ddl;
        }
    }
}
