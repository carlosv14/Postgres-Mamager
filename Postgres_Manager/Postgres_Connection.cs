using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Postgres_Manager
{
    class Postgres_Connection
    {
        public Postgres_Connection()
        {

        }
        public NpgsqlConnection connect_db(string server, string port, string user, string password, string database)
        {
            NpgsqlConnection conn = null;
            try
            {
                // PostgeSQL-style connection string
                string connstring = String.Format("Server={0};Port={1};" +
                                                  "User Id={2};Password={3};Database={4};", server, port, user, password, database);

                conn = new NpgsqlConnection(connstring);
                conn.Open();
                return conn;

            }

            catch (Exception msg)
            {
                // something went wrong, and you wanna know why
                MessageBox.Show("Invalid User or Password");
              
                conn.Close();
                return null;
            }
        }

        public DataTable exec_Sql(string sql, NpgsqlConnection conn,RichTextBox t)
        {
            DataTable dt = new DataTable();
            try
            {
                
               
              
                NpgsqlCommand cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader r;
               
                r = cmd.ExecuteReader();
                if (r.HasRows)
                {
                    dt.Load(r);
                    r.Close();
                }
                r.Close();
               
                int length = t.TextLength;
                string msg = "Succesfull Query!";
                t.AppendText(DateTime.Now + " "+msg + "\n");

                t.SelectionStart = length;
                int flength = msg.Length + DateTime.Now.ToString().Length + 1;
                t.SelectionLength = flength;
                t.SelectionColor = Color.Green;
                t.ScrollToCaret();
                
                
            }
            catch (Exception ex)
            {
                int length = t.TextLength;

               t.AppendText(DateTime.Now + " " + ex.Message + "\n");

                t.SelectionStart = length;
                int flength = ex.Message.Length + DateTime.Now.ToString().Length + 1;
                t.SelectionLength = flength;
                t.SelectionColor = Color.Red;
                t.ScrollToCaret();
               
            }


            return dt;
        }

        public void special_query(TabControl tabControl, string sql, NpgsqlConnection con, RichTextBox t)
        {
            try
            {
                NpgsqlCommand cmd2 = new NpgsqlCommand();
                cmd2.Connection = con;
                cmd2.CommandType = System.Data.CommandType.Text;
                cmd2.CommandText = sql;
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd2);

                DataSet ds = new DataSet();
                da.Fill(ds);
                if(tabControl.TabCount>2)
                for (int i = 2; i <tabControl.TabCount+1; i++)
                {
                    tabControl.TabPages.RemoveAt(i);
                }
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    TabPage tab = new TabPage("Query" + (i + 1));
                    tabControl.TabPages.Add(tab);
                    tabControl.SelectedTab = tab;
                    DataTable dt = new DataTable();
                    dt = ds.Tables[i];
                    DataGridView dg = new DataGridView();
                    dg.DataSource = dt;
                    dg.ReadOnly = true;
                    tabControl.SelectedTab.Controls.Add(dg);
                }
            }
            catch (Exception ex)
            {
                int length = t.TextLength;

                t.AppendText(DateTime.Now + " " + ex.Message + "\n");

                t.SelectionStart = length;
                int flength = ex.Message.Length + DateTime.Now.ToString().Length + 1;
                t.SelectionLength = flength;
                t.SelectionColor = Color.Red;
                t.ScrollToCaret();

            }
        }


    }
}