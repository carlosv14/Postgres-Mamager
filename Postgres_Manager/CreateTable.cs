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
    public partial class CreateTable : Form
    {

        NpgsqlConnection conn = null;
        Postgres_Connection pc = null;
        private RichTextBox t;
       
        public CreateTable(NpgsqlConnection conn,RichTextBox t)
        {
            InitializeComponent();
            this.conn = conn;
            this.t = t;
            this.pc = new Postgres_Connection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
              comboBox1.DataSource = pc.exec_Sql("select * from pg_user", conn,t);
              comboBox1.DisplayMember = "usename";
              comboBox2.DataSource = pc.exec_Sql("select * from pg_tablespace", conn,t); ;
              comboBox2.DisplayMember = "spcname";

              DataGridViewCheckBoxColumn pk = new DataGridViewCheckBoxColumn();
              pk.HeaderText = "PK";
              pk.Name = "PK";
 
              dataGridView1.Columns.Add(pk);
              dataGridView1.Columns.Add("NAME", "NAME");
              DataGridViewComboBoxColumn type = new DataGridViewComboBoxColumn();
              type.HeaderText = "DATA TYPE";
              type.Name= "type";
              type.DataSource = pc.exec_Sql("SELECT  typname from pg_catalog.pg_type t WHERE t.typrelid = 0 AND pg_catalog.pg_type_is_visible(t.oid)", conn,t);
              type.DisplayMember = "typname";
              dataGridView1.Columns.Add(type);
              dataGridView1.Columns.Add("LENGTH", "LENGTH");
              DataGridViewCheckBoxColumn nn = new DataGridViewCheckBoxColumn();
              nn.HeaderText = "NOT NULL";
              nn.Name = "NN";
             
              dataGridView1.Columns.Add(nn);

              
                  
           
        }

        private void CreateTable_Load(object sender, EventArgs e)
        {
          
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> pks = new List<string>();
            if (tabControl1.SelectedIndex==2 && dataGridView1.Rows[0].Cells[1].Value!=null)
            {
                richTextBox1.Clear();
                richTextBox1.Text += "CREATE TABLE " + textBox1.Text + "( \n";
               
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    richTextBox1.Text += dataGridView1.Rows[i].Cells[1].Value.ToString();
                    richTextBox1.Text += " "+dataGridView1.Rows[i].Cells[2].Value.ToString();
                    richTextBox1.Text += "(";
                    richTextBox1.Text += dataGridView1.Rows[i].Cells[3].Value.ToString();
                    richTextBox1.Text += ")";
                    if (dataGridView1.Rows[i].Cells["NN"].Value != null && (Boolean)dataGridView1.Rows[i].Cells["NN"].Value)
                        richTextBox1.Text += " NOT NULL, \n";
                    else
                        richTextBox1.Text += ",\n";
                    if (dataGridView1.Rows[i].Cells["PK"].Value!=null && (Boolean)dataGridView1.Rows[i].Cells["PK"].Value)
                    {
                        pks.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());
                       
                    }
                }
                richTextBox1.Text += " PRIMARY KEY(";
                for (int i = 0; i < pks.Count; i++)
                {
                   
                    richTextBox1.Text += pks.ElementAt(i);
                    if(i<pks.Count-1)
                    richTextBox1.Text += ", ";
                    
                }
                richTextBox1.Text += ") \n";
                richTextBox1.Text += ") \n";

                richTextBox1.Text += " WITH (OIDS = FALSE);";
               
                    
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> pks = new List<string>();
            if (tabControl1.SelectedIndex == 1 && dataGridView1.Rows[0].Cells[1].Value != null)
            {
                richTextBox1.Clear();
                richTextBox1.Text += "CREATE TABLE " + textBox1.Text + "( \n";

                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    richTextBox1.Text += dataGridView1.Rows[i].Cells[1].Value.ToString();
                    richTextBox1.Text += " " + dataGridView1.Rows[i].Cells[2].Value.ToString();
                    richTextBox1.Text += "(";
                    richTextBox1.Text += dataGridView1.Rows[i].Cells[3].Value.ToString();
                    richTextBox1.Text += ")";
                    if (dataGridView1.Rows[i].Cells["NN"].Value != null && (Boolean)dataGridView1.Rows[i].Cells["NN"].Value)
                        richTextBox1.Text += " NOT NULL, \n";
                    else
                        richTextBox1.Text += ",\n";
                    if (dataGridView1.Rows[i].Cells["PK"].Value != null && (Boolean)dataGridView1.Rows[i].Cells["PK"].Value)
                    {
                        pks.Add(dataGridView1.Rows[i].Cells[1].Value.ToString());

                    }
                }
                richTextBox1.Text += " PRIMARY KEY(";
                for (int i = 0; i < pks.Count; i++)
                {

                    richTextBox1.Text += pks.ElementAt(i);
                    if (i < pks.Count - 1)
                        richTextBox1.Text += ", ";

                }
                richTextBox1.Text += ") \n";
                richTextBox1.Text += ") \n";

                richTextBox1.Text += " WITH (OIDS = FALSE);";


            }
            pc.exec_Sql(richTextBox1.Text, conn,t);
            this.Close();
        }
    }
}
