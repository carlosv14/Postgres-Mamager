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
    public partial class CreateIndex : Form
    {
        private Postgres_Connection pc = new Postgres_Connection();
        private NpgsqlConnection conn = null;
        private RichTextBox rt = null;
        public CreateIndex(NpgsqlConnection conn, RichTextBox rt)
        {
            this.conn = conn;
            this.rt = rt;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pc.exec_Sql(richTextBox1.Text, conn, rt);
            this.Close();
        }

        private void CreateIndex_Load(object sender, EventArgs e)
        {
           
           
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                richTextBox1.Text += " " + textBox1.Text;
                richTextBox1.Text += " ON";
            }
        }
    }
}
