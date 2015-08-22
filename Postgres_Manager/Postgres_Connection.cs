using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, conn);
                ds.Reset();
                da.Fill(ds);
                if (ds.Tables.Count > 0)
                    dt = ds.Tables[0];
                int length = t.TextLength;
                string msg = "Succesfull Query!";
                t.AppendText(DateTime.Now + " "+msg + "\n");

                t.SelectionStart = length;
                int flength = msg.Length + DateTime.Now.ToString().Length + 1;
                t.SelectionLength = flength;
                t.SelectionColor = Color.Green;
                t.ScrollToCaret();
                return dt;
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
            return new DataTable();
        }



        
    }
}