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
    public partial class Form1 : Form
    {

       
        private string defaulthost = "localhost";
        private Postgres_Connection pc = new Postgres_Connection();
        private string defaultport = "5432";
        private string defaultuser = "postgres";
        static ImageList imgList  = new ImageList();
        private NpgsqlConnection conn = null;
        public Form1()
        {
            InitializeComponent();
           
            imgList.Images.Add("Hserver",
               Image.FromFile("C:\\Users\\Carlos Varela\\Documents\\Visual Studio 2013\\Projects\\Postgres_Manager\\HomeServer.png"));
            imgList.Images.Add("server",
               Image.FromFile("C:\\Users\\Carlos Varela\\Documents\\Visual Studio 2013\\Projects\\Postgres_Manager\\server.png"));
            imgList.Images.Add("server",
              Image.FromFile("C:\\Users\\Carlos Varela\\Documents\\Visual Studio 2013\\Projects\\Postgres_Manager\\folder.png"));
            imgList.Images.Add("server",
             Image.FromFile("C:\\Users\\Carlos Varela\\Documents\\Visual Studio 2013\\Projects\\Postgres_Manager\\Login.png"));
            treeView1.ImageList = imgList;
            treeView1.Nodes.Add("Servers:");
            treeView1.Nodes[0].ImageIndex = 0;
            treeView1.Nodes[0].SelectedImageIndex = 0;
            dataGridView1.Visible = false;
            load_Servers(defaulthost, defaultport, "postgres", "cgvm02", "postgres");
            richTextBox1.Enabled = false;
        }



       

        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           
               string selectedNodeText = e.Node.Text;
            if(e.Node.Parent!=null){
                if (e.Node.Parent.Text == "Servers:")
                {
                    login lg = new login(selectedNodeText, defaulthost);
                    lg.ShowDialog();
                    show_components(lg.ReturnServer(), defaultport,lg.ReturnUser(), lg.ReturnPass(), "postgres");
                }
                if (e.Node.Parent.Text == "Databases:")
                {
                    login lg = new login(selectedNodeText, defaulthost);
                    lg.ShowDialog();
                    NpgsqlConnection conn =pc.connect_db(lg.ReturnServer(), defaultport, lg.ReturnUser(), lg.ReturnPass(), selectedNodeText);
                  if(conn ==null)
                      treeView1.CollapseAll();
                    Tables_show(conn);
                    Triggers_show(conn);
                }
            }
        }

        private void Databases_Show(NpgsqlConnection conn)
        {
            try
            {

                treeView1.Nodes[0].Nodes[0].Nodes.Add("Databases:", "Databases:");
                treeView1.Nodes[0].Nodes[0].Nodes[0].ImageIndex = 1;
                treeView1.Nodes[0].Nodes[0].Nodes[0].SelectedImageIndex = 1;
                DataTable dt = pc.exec_Sql("select * from pg_database", conn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Add(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString());
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[i].ImageIndex = 1;
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[i].SelectedImageIndex = 1;
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[i].Nodes.Add("Functions:", "Functions:");
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[i].Nodes.Add("Sequences:", "Sequences:");
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[i].Nodes.Add("Tables:", "Tables:");
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[i].Nodes.Add("Triggers:", "Triggers:");
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[i].Nodes.Add("Views:", "Views:");
                }
                
                conn.Close();
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.ToString());

            }


        }

        private void load_Servers(string server, string port, string user, string password, string database)
        {
            try
            {
                conn = pc.connect_db(server, port, user, password, database);

                treeView1.Nodes[0].Nodes.Add("postgres(localhost:" + port + ")");

                conn.Close();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString());

            }
        }
        private void Tables_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;
                if (conn.Database != "postgres")
                {
                     dt = pc.exec_Sql("select tablename from pg_tables where schemaname != 'pg_catalog' and schemaname != 'information_schema' ", conn);
                    
                }
                else
                {
                    dt = pc.exec_Sql("select tablename from pg_tables", conn);
                }
                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].Nodes.Add(dt.Rows[i][0].ToString(),
                            dt.Rows[i][0].ToString());
                       
                            columns =
                                pc.exec_Sql(
                                    "SELECT column_name, data_type FROM information_schema.columns WHERE table_name=" + "'" + dt.Rows[i][0].ToString() + "'", conn);

                        for (int j = 0; j < columns.Rows.Count; j++)
                        {
                            treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].Nodes[i].Nodes.Add(columns.Rows[j][0].ToString(),
                            columns.Rows[j][0].ToString());
                        }
                        columns.Clear();
                    }
                    

                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.ToString());

            }

        }
        private void Triggers_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;


                dt = pc.exec_Sql("select tgname from pg_trigger", conn);
                
                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].Nodes.Add(dt.Rows[i][0].ToString());


                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.ToString());

            }

        }
        private void Tablespaces_Show(NpgsqlConnection conn)
        {
            try
            {

                treeView1.Nodes[0].Nodes[0].Nodes.Add("Tablespaces:", "Tablespaces:");
                treeView1.Nodes[0].Nodes[0].Nodes[1].ImageIndex = 2;
                treeView1.Nodes[0].Nodes[0].Nodes[1].SelectedImageIndex = 2;
                DataTable dt = pc.exec_Sql("select * from pg_tablespace", conn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    treeView1.Nodes[0].Nodes[0].Nodes[1].Nodes.Add(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString());
                    treeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[i].ImageIndex = 2;
                    treeView1.Nodes[0].Nodes[0].Nodes[1].Nodes[i].SelectedImageIndex = 2;
                }
                conn.Close();
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.ToString());

            }


        }
        private void Users_Show(NpgsqlConnection conn)
        {
            try
            {

                treeView1.Nodes[0].Nodes[0].Nodes.Add("Login Roles:", "Login Roles:");
                treeView1.Nodes[0].Nodes[0].Nodes[2].ImageIndex = 3;
                treeView1.Nodes[0].Nodes[0].Nodes[2].SelectedImageIndex = 3;
                DataTable dt = pc.exec_Sql("select * from pg_user", conn);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    treeView1.Nodes[0].Nodes[0].Nodes[2].Nodes.Add(dt.Rows[i][0].ToString(), dt.Rows[i][0].ToString());
                    treeView1.Nodes[0].Nodes[0].Nodes[2].Nodes[i].ImageIndex = 3;
                    treeView1.Nodes[0].Nodes[0].Nodes[2].Nodes[i].SelectedImageIndex = 3;
                }
                conn.Close();
            }
            catch (Exception msg)   
            {

                MessageBox.Show(msg.ToString());

            }


        }
        private void show_components(string server, string port, string user, string password, string database)
        {
               conn = pc.connect_db(server, port, user, password, database);
                if (conn != null)
                {
                    Databases_Show(conn);
                    conn.Open();
                    Tables_show(conn);
                    Tablespaces_Show(conn);
                    conn.Open();
                    Users_Show(conn);


                }
                else
                {
                    treeView1.CollapseAll();
                }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string value = "";
            try
            {

                MessageBox.Show(conn.Database);
                if (richTextBox1.SelectedText.Length > 1)
                    value = richTextBox1.SelectedText;
                else
                    value = richTextBox1.Text;

                DataTable dt = pc.exec_Sql(value, conn);
                dataGridView1.Visible = true;
                dataGridView1.ReadOnly = true;
                dataGridView1.BackgroundColor = Color.White;
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {

                richTextBox2.Text = ex.Message;
            }
        }

        private void richTextBox1_keypressed(Object sender, KeyEventArgs e)
        {
            string value = "";
          if (e.KeyCode == Keys.F5)
            {

                try
                {

                    MessageBox.Show(conn.Database);
                    if (richTextBox1.SelectedText.Length > 1)
                        value = richTextBox1.SelectedText;
                    else
                        value = richTextBox1.Text;
                        
                    DataTable dt = pc.exec_Sql(value, conn);
                    dataGridView1.Visible = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.BackgroundColor = Color.White;
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    
                    richTextBox2.Text = ex.Message;
                }
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTable ct = new CreateTable(conn);
            ct.ShowDialog();
        }

        private void createTriggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trigger_Create tc = new Trigger_Create(conn, richTextBox1);
            tc.ShowDialog();
        }
    }
}
