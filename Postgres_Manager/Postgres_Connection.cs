using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
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

        public DataTable exec_Sql(string sql, NpgsqlConnection conn)
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
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            return new DataTable();
        }



        
    }
}