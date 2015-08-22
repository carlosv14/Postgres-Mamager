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
    public partial class Trigger_Create : Form
    {
        private NpgsqlConnection conn = null;
        private Postgres_Connection pc = null;
        private string DDL;
        private RichTextBox rt = null;
        private RichTextBox t;
        public Trigger_Create(NpgsqlConnection conn,RichTextBox rt,RichTextBox t)
        {
            InitializeComponent();
            this.conn = conn;
            pc = new Postgres_Connection();
            this.t = t;
            this.rt = rt;
        }

        private void Trigger_Create_Load(object sender, EventArgs e)
        {
           
            comboBox3.Items.Add("BEFORE");
            comboBox3.Items.Add("AFTER");
            comboBox3.Items.Add("INSTEAD OF");

            try
            {
                DataTable dt = null;
                DataTable columns = null;
                if (conn.Database != "postgres")
                {
                    dt = pc.exec_Sql("select tablename from pg_tables where schemaname != 'pg_catalog' and schemaname != 'information_schema' ", conn,t);

                }
                else
                {
                    dt = pc.exec_Sql("select tablename from pg_tables", conn,t);
                }
                this.conn = conn;
                comboBox2.DataSource = dt;
                comboBox2.DisplayMember = "tablename";
            }
            catch (Exception msg)
            {

                MessageBox.Show(msg.ToString());

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contador = 0;
            DDL += "CREATE TRIGGER ";
            DDL += textBox1.Text + " \n";
            DDL += comboBox3.GetItemText(comboBox3.SelectedItem)+" ";
            
            if (checkBox1.Checked)
            {
                DDL+= checkBox1.Text;
                contador++;
            }
            if (checkBox2.Checked)
            {
                if (contador > 0)
                    DDL += " OR ";
                  DDL+= checkBox2.Text;
            }
            if (checkBox3.Checked)
            {
                if(contador>0)
                    DDL+=" OR ";
                  DDL+= checkBox3.Text;
            }
            if (checkBox4.Checked)
            {
                if(contador>0)
                    DDL+=" OR ";
                  DDL+= checkBox4.Text;
            }
            DDL += " ON " + comboBox2.GetItemText(comboBox2.SelectedItem) + "\n";
            DDL += " FOR EACH ROW \n";
            rt.Text = DDL;
            rt.Enabled = true;
            this.Close();
        }
    }
}
