using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Postgres_Manager
{
    public partial class login : Form
    {
        private string password = "";
        private string server = "";
        private string user = "";
        public login(string user, string server)
        {
            InitializeComponent();
            this.server = server;
        }

        
        public string ReturnPass()
        {
            return password;
        }
        public string ReturnServer()
        {
            return server;
        }
        public string ReturnUser()
        {
            return user;
        }
        private void login_Load(object sender, EventArgs e)
        {
            textBox1.Text = server;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            password =  textBox3.Text;
            user = textBox2.Text;
            
            this.Close();
        }

    }
}
