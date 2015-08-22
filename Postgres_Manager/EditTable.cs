using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Postgres_Manager
{
    public partial class EditTable : Form
    {
        private Postgres_Connection pc;
        private NpgsqlConnection conn;
        private string selectedTable;
        private RichTextBox t;
        public EditTable(NpgsqlConnection conn, string selectedTable,RichTextBox t)
        {
            InitializeComponent();
            this.conn = conn;
            this.t = t;
            this.selectedTable = selectedTable;
            this.pc = new Postgres_Connection();
            DataGridViewCheckBoxColumn pk = new DataGridViewCheckBoxColumn();
            pk.HeaderText = "PK";
            pk.Name = "PK";

            dataGridView1.Columns.Add(pk);
            DataTable columns = null;
            columns =  pc.exec_Sql(
                                   "SELECT column_name, data_type FROM information_schema.columns WHERE table_name=" + "'" + selectedTable + "'", conn,t);

            dataGridView1.DataSource = columns;

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
    
            if (tabControl1.SelectedIndex == 0)
            {
                DataTable columns = null;
                columns = pc.exec_Sql(
                                       "SELECT column_name, data_type FROM information_schema.columns WHERE table_name=" + "'" + selectedTable + "'", conn,t);

                dataGridView1.DataSource = columns;

            }
            if (tabControl1.SelectedIndex == 1)
            {
                DataTable columns = null;
                columns = pc.exec_Sql(
                                       "SELECT *  from "
                                        + selectedTable 
                                        , conn,t);

                dataGridView2.DataSource = columns;

            }
           
           
            if (tabControl1.SelectedIndex == 1)
            {
                string ddl = "CREATE TABLE ";
                ddl += selectedTable + " \n";
                ddl += "(\n";
                DataTable columns = null;
                columns = pc.exec_Sql(
                                       " SELECT column_name, data_type, is_nullable,character_maximum_length FROM information_schema.columns WHERE table_name =" + "'" + selectedTable + "'", conn,t);
                for (int i = 0; i < columns.Rows.Count; i++)
                {
                    ddl += columns.Rows[i][0].ToString()+ " ";
                    ddl += columns.Rows[i][1].ToString() + " (";
                    ddl += columns.Rows[i][3].ToString() + ")";
                    if (columns.Rows[i][2].ToString() == "NO")
                    {
                        ddl += " NOT NULL";
                    }
                    if (i < columns.Rows.Count - 1)
                        ddl += ", \n";
                }
                columns.Clear();
                columns =
                    pc.exec_Sql(
                        "SELECT a.attname FROM pg_index i JOIN pg_attribute a ON a.attrelid = i.indrelid AND a.attnum = ANY(i.indkey) WHERE  i.indrelid ="+"'"+selectedTable +"'"+"::regclass AND i.indisprimary; ",
                        conn,t);
                if (columns.Rows.Count > 0)
                {
                    ddl += ",\n";
                    ddl += "PRIMARY KEY(";
                    for (int i = 0; i < columns.Rows.Count; i++)
                    {
                        ddl+= columns.Rows[i][0].ToString();
                        if (i < columns.Rows.Count - 1)
                            ddl += ", ";
                    }
                    ddl += ") \n";
                    ddl += ");";
                }
                richTextBox1.Clear();
                richTextBox1.Text = ddl;

            }
        }

        private void EditTable_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
