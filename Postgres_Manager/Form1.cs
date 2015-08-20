using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
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
           
          firstLoad();
            richTextBox1.Enabled = false;
        }



        private void firstLoad()
        {
            imgList.Images.Add("Hserver",
               Properties.Resources.HomeServer);
            imgList.Images.Add("server",
              Properties.Resources.server1);
            imgList.Images.Add("folder",
              Properties.Resources.folder);
            imgList.Images.Add("login",
             Properties.Resources.Login);
            treeView1.ImageList = imgList;
            treeView1.Nodes.Add("Servers:");
            treeView1.Nodes[0].ImageIndex = 0;
            treeView1.Nodes[0].SelectedImageIndex = 0;
            dataGridView1.Visible = false;
            var pic = new Bitmap(Properties.Resources.play, new Size(button1.Width, button1.Height));
           
            button1.BackgroundImage = pic;
            load_Servers(defaulthost, defaultport, "postgres", "cgvm02", "postgres");
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
                    richTextBox1.Enabled = true;
                }
                if (e.Node.Parent.Text == "Databases:")
                {
                    login lg = new login(selectedNodeText, defaulthost);
                    lg.ShowDialog();
                    NpgsqlConnection conn =pc.connect_db(lg.ReturnServer(), defaultport, lg.ReturnUser(), lg.ReturnPass(), selectedNodeText);
                  if(conn ==null)
                      treeView1.CollapseAll();
                    Triggers_show(conn);
                    Tables_show(conn);
                    view_show(conn);
                    Functions_show(conn);
                }
            }
        }

        private void Functions_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;


                dt = pc.exec_Sql("select * from pg_proc where pronamespace = 2200", conn);

                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].Nodes.Add(dt.Rows[i][0].ToString());


                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.ToString());

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
        void view_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;


                dt = pc.exec_Sql("select table_name from INFORMATION_SCHEMA.views WHERE table_schema = ANY (current_schemas(false))", conn);

                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].Nodes.Add(dt.Rows[i][0].ToString());


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
           NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            }

        private void createTriggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trigger_Create tc = new Trigger_Create(conn, richTextBox1);
            tc.ShowDialog();
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            view_show(conn);
            Functions_show(conn);
        }

        private void createFunctionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Functions fn = new Functions(richTextBox1);
            fn.ShowDialog();
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            view_show(conn);
            Functions_show(conn);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sequence s = new Sequence(conn);
            s.ShowDialog();
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            view_show(conn);
            Functions_show(conn);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            string selectedNodeText = e.Node.Text;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Text == "Functions:")
                {
                    
                   richTextBox1.Text= pc.exec_Sql(
                        "SELECT pg_get_functiondef(p.oid) FROM   pg_proc p JOIN   pg_namespace n ON n.oid = p.pronamespace WHERE  p.proname ILIKE "+"'"+e.Node.Text+"'", conn).Rows[0][0].ToString();
                }
                if (e.Node.Parent.Text == "Sequences:")
                {
                   

                }
                if (e.Node.Parent.Text == "Tables:")
                {
                  
                }
                if (e.Node.Parent.Text == "Triggers:")
                {
                    DataTable dt = null;
                    richTextBox1.Clear();
                    string ddl = "CREATE ";

                    dt = pc.exec_Sql(
                            "SELECT event_object_table, trigger_name, event_manipulation, action_statement, action_timing , action_condition FROM information_schema.triggers where trigger_name ="+ "'" + e.Node.Text + "'", conn);
                    ddl += dt.Rows[0][1].ToString();
                    ddl += "\n";
                    ddl += dt.Rows[0][4].ToString() + " ";
                    ddl += dt.Rows[0][2].ToString() + " ON ";
                    ddl += dt.Rows[0][0].ToString() + " \n";
                    ddl += " FOR EACH ROW \n";
                    ddl+= dt.Rows[0][5].ToString() + "\n";
                    ddl += dt.Rows[0][3].ToString() + "\n";
                    richTextBox1.Text = ddl;
                }
                if (e.Node.Parent.Text == "Views:")
                {
                  richTextBox1.Clear();
                    string ddl = "CREATE OR REPLACE ";
                    DataTable dt = null;
                    richTextBox1.Clear();
                    dt = pc.exec_Sql(
                            "select * from pg_views  where schemaname != 'pg_catalog' and schemaname != 'information_schema' and viewname=" +"'"+e.Node.Text+"'", conn);

                    ddl += dt.Rows[0][1].ToString();
                    ddl += " AS \n";
                    ddl += dt.Rows[0][3].ToString();
                    richTextBox1.Text = ddl;
                }
            }
        }

        private void createViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View v = new View(conn);
            v.ShowDialog();
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            view_show(conn);
            Functions_show(conn);
        }
    }
}
