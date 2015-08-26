using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Postgres_Manager
{
    public partial class CreateTablespaces : Form
    {
        string ddl = "";
        private RichTextBox rt = null;
        private NpgsqlConnection conn = null;
        private Postgres_Connection pc = new Postgres_Connection();
        public CreateTablespaces(NpgsqlConnection conn,RichTextBox rt)
        {
            
          
            InitializeComponent();
            this.conn = conn;
            this.rt = rt;
            ddl += "CREATE TABLESPACE ";
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ddl += textBox1.Text;
            ddl += " LOCATION ";
            SaveFileDialog pf = new SaveFileDialog();
            pf.FileName = textBox1.Text;
            if (pf.ShowDialog() == DialogResult.OK)
            {
                ddl += "'"+Path.GetDirectoryName(pf.FileName)+"'";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ddl += ";";
            pc.exec_Sql(ddl, conn,rt);
            this.Close();

        }

        private void CreateTablespaces_Load(object sender, EventArgs e)
        {

        }
    }
}
