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
using ScintillaNET;


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

            richTextBox1.StyleResetDefault();
            richTextBox1.Styles[Style.Default].Font = "Consolas";
            richTextBox1.Styles[Style.Default].Size = 10;
            richTextBox1.StyleClearAll();
            richTextBox1.Styles[Style.Cpp.Default].ForeColor = Color.Silver;
            richTextBox1.Styles[Style.Cpp.Comment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            richTextBox1.Styles[Style.Cpp.CommentLine].ForeColor = Color.FromArgb(0, 128, 0); // Green
            richTextBox1.Styles[Style.Cpp.CommentLineDoc].ForeColor = Color.FromArgb(128, 128, 128); // Gray
            richTextBox1.Styles[Style.Cpp.Number].ForeColor = Color.Red;
            richTextBox1.Styles[Style.Cpp.Word].ForeColor = Color.Blue;
            richTextBox1.Styles[Style.Cpp.Word2].ForeColor = Color.Fuchsia;
            richTextBox1.Styles[Style.Cpp.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            richTextBox1.Styles[Style.Cpp.Character].ForeColor = Color.FromArgb(163, 21, 21); // Red
            richTextBox1.Styles[Style.Cpp.Verbatim].ForeColor = Color.FromArgb(163, 21, 21); // Red
            richTextBox1.Styles[Style.Cpp.Operator].ForeColor = Color.Silver;
            richTextBox1.Styles[Style.Cpp.Preprocessor].ForeColor = Color.Purple;
            richTextBox1.Lexer = Lexer.Cpp;
            richTextBox1.Margins[0].Width = 16;

            richTextBox1.SetKeywords(0, "abort absolute access action add admin after aggregate all also alter always analyse analyze and any array as asc assertion assignment asymmetric at attribute authorization backward before begin between bigint binary bit boolean both by cache called cascade cascaded case cast catalog chain char character characteristics check checkpoint class close cluster coalesce collate collation column comment comments commit committed concurrently configuration connection constraint constraints content continue conversion copy cost create cross csv current current_catalog current_date current_role current_schema current_time current_timestamp current_user cursor cycle data database day deallocate dec decimal declare default defaults deferrable deferred definer delete delimiter delimiters desc dictionary disable discard distinct do document domain double drop each else enable encoding encrypted end enum escape event except exclude excluding exclusive execute exists explain extension external extract false family fetch filter first float following for force foreign forward freeze from full function functions global grant granted greatest group handler having header hold hour identity if ilike immediate immutable implicit in including increment index indexes inherit inherits initially inline inner inout input insensitive insert instead int integer intersect interval into invoker is isnull isolation join key label language large last lateral lc_collate lc_ctype leading leakproof least left level like limit listen load local localtime localtimestamp location lock mapping match materialized maxvalue minute minvalue mode month move name names national natural nchar next no none not nothing notify notnull nowait null nullif nulls numeric object of off offset oids on only operator option options or order ordinality out outer over overlaps overlay owned owner parser partial partition passing password placing plans position preceding precision prepare prepared preserve primary prior privileges procedural procedure program quote range read real reassign recheck recursive ref references refresh reindex relative release rename repeatable replace replica reset restart restrict returning returns revoke right role rollback row rows rule savepoint schema scroll search second security select sequence sequences serializable server session session_user set setof share show similar simple smallint snapshot some stable standalone start statement statistics stdin stdout storage strict strip substring symmetric sysid system table tables tablespace temp template temporary text then time timestamp to trailing transaction treat trigger trim true truncate trusted type types unbounded uncommitted unencrypted union unique unknown unlisten unlogged until user using vacuum valid validate validator value values varchar variadic varying verbose version view views volatile when where whitespace window with within without work wrapper write xml xmlattributes xmlconcat xmlelement xmlexists xmlforest xmlparse xmlpi xmlroot xmlserialize year yes zone ABORT ABSOLUTE ACCESS ACTION ADD ADMIN AFTER AGGREGATE ALL ALSO ALTER ALWAYS ANALYSE ANALYZE AND ANY ARRAY AS ASC ASSERTION ASSIGNMENT ASYMMETRIC AT ATTRIBUTE AUTHORIZATION BACKWARD BEFORE BEGIN BETWEEN BIGINT BINARY BIT BOOLEAN BOTH BY CACHE CALLED CASCADE CASCADED CASE CAST CATALOG CHAIN CHAR CHARACTER CHARACTERISTICS CHECK CHECKPOINT CLASS CLOSE CLUSTER COALESCE COLLATE COLLATION COLUMN COMMENT COMMENTS COMMIT COMMITTED CONCURRENTLY CONFIGURATION CONNECTION CONSTRAINT CONSTRAINTS CONTENT CONTINUE CONVERSION COPY COST CREATE CROSS CSV CURRENT CURRENT_CATALOG CURRENT_DATE CURRENT_ROLE CURRENT_SCHEMA CURRENT_TIME CURRENT_TIMESTAMP CURRENT_USER CURSOR CYCLE DATA DATABASE DAY DEALLOCATE DEC DECIMAL DECLARE DEFAULT DEFAULTS DEFERRABLE DEFERRED DEFINER DELETE DELIMITER DELIMITERS DESC DICTIONARY DISABLE DISCARD DISTINCT DO DOCUMENT DOMAIN DOUBLE DROP EACH ELSE ENABLE ENCODING ENCRYPTED END ENUM ESCAPE EVENT EXCEPT EXCLUDE EXCLUDING EXCLUSIVE EXECUTE EXISTS EXPLAIN EXTENSION EXTERNAL EXTRACT FALSE FAMILY FETCH FILTER FIRST FLOAT FOLLOWING FOR FORCE FOREIGN FORWARD FREEZE FROM FULL FUNCTION FUNCTIONS GLOBAL GRANT GRANTED GREATEST GROUP HANDLER HAVING HEADER HOLD HOUR IDENTITY IF ILIKE IMMEDIATE IMMUTABLE IMPLICIT IN INCLUDING INCREMENT INDEX INDEXES INHERIT INHERITS INITIALLY INLINE INNER INOUT INPUT INSENSITIVE INSERT INSTEAD INT INTEGER INTERSECT INTERVAL INTO INVOKER IS ISNULL ISOLATION JOIN KEY LABEL LANGUAGE LARGE LAST LATERAL LC_COLLATE LC_CTYPE LEADING LEAKPROOF LEAST LEFT LEVEL LIKE LIMIT LISTEN LOAD LOCAL LOCALTIME LOCALTIMESTAMP LOCATION LOCK MAPPING MATCH MATERIALIZED MAXVALUE MINUTE MINVALUE MODE MONTH MOVE NAME NAMES NATIONAL NATURAL NCHAR NEXT NO NONE NOT NOTHING NOTIFY NOTNULL NOWAIT NULL NULLIF NULLS NUMERIC OBJECT OF OFF OFFSET OIDS ON ONLY OPERATOR OPTION OPTIONS OR ORDER ORDINALITY OUT OUTER OVER OVERLAPS OVERLAY OWNED OWNER PARSER PARTIAL PARTITION PASSING PASSWORD PLACING PLANS POSITION PRECEDING PRECISION PREPARE PREPARED PRESERVE PRIMARY PRIOR PRIVILEGES PROCEDURAL PROCEDURE PROGRAM QUOTE RANGE READ REAL REASSIGN RECHECK RECURSIVE REF REFERENCES REFRESH REINDEX RELATIVE RELEASE RENAME REPEATABLE REPLACE REPLICA RESET RESTART RESTRICT RETURNING RETURNS REVOKE RIGHT ROLE ROLLBACK ROW ROWS RULE SAVEPOINT SCHEMA SCROLL SEARCH SECOND SECURITY SELECT SEQUENCE SEQUENCES SERIALIZABLE SERVER SESSION SESSION_USER SET SETOF SHARE SHOW SIMILAR SIMPLE SMALLINT SNAPSHOT SOME STABLE STANDALONE START STATEMENT STATISTICS STDIN STDOUT STORAGE STRICT STRIP SUBSTRING SYMMETRIC SYSID SYSTEM TABLE TABLES TABLESPACE TEMP TEMPLATE TEMPORARY TEXT THEN TIME TIMESTAMP TO TRAILING TRANSACTION TREAT TRIGGER TRIM TRUE TRUNCATE TRUSTED TYPE TYPES UNBOUNDED UNCOMMITTED UNENCRYPTED UNION UNIQUE UNKNOWN UNLISTEN UNLOGGED UNTIL USER USING VACUUM VALID VALIDATE VALIDATOR VALUE VALUES VARCHAR VARIADIC VARYING VERBOSE VERSION VIEW VIEWS VOLATILE WHEN WHERE WHITESPACE WINDOW WITH WITHIN WITHOUT WORK WRAPPER WRITE XML XMLATTRIBUTES XMLCONCAT XMLELEMENT XMLEXISTS XMLFOREST XMLPARSE XMLPI XMLROOT XMLSERIALIZE YEAR YES ZONE");
            richTextBox1.SetKeywords(1, "update UPDATE");
           
            firstLoad();
            richTextBox1.Enabled = false;
        }

        private void UpdateLineNumbers(int startingAtLine)
        {
            
            for (int i = startingAtLine; i < richTextBox1.Lines.Count; i++)
            {
                richTextBox1.Lines[i].MarginStyle = Style.LineNumber;
                richTextBox1.Lines[i].MarginText = i.ToString();
            }
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
            imgList.Images.Add("Table",
           Properties.Resources.table);
            imgList.Images.Add("Fx",
          Properties.Resources.fx);
            imgList.Images.Add("sp",
         Properties.Resources.sp);
            imgList.Images.Add("tgr",
      Properties.Resources.tgr);
            imgList.Images.Add("view",
     Properties.Resources.view);
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
                    seq_show(conn);
                }
            }
        }

        private void Functions_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;


                dt = pc.exec_Sql("select * from pg_proc where pronamespace = 2200", conn, this.richTextBox2);


                
                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].ImageIndex = 6;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].SelectedImageIndex = 6;
                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].Nodes.Add(dt.Rows[i][0].ToString());
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].Nodes[i].ImageIndex = 6;
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[0].Nodes[i].SelectedImageIndex = 6;
                    }



                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {


                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();

            }
        }

        private void Databases_Show(NpgsqlConnection conn)
        {
            try
            {

                treeView1.Nodes[0].Nodes[0].Nodes.Add("Databases:", "Databases:");
                treeView1.Nodes[0].Nodes[0].Nodes[0].ImageIndex = 1;
                treeView1.Nodes[0].Nodes[0].Nodes[0].SelectedImageIndex = 1;
                DataTable dt = pc.exec_Sql("select * from pg_database", conn,this.richTextBox2);
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


                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();
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

                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();


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
                     dt = pc.exec_Sql("select tablename from pg_tables where schemaname != 'pg_catalog' and schemaname != 'information_schema' ", conn, this.richTextBox2);
                    
                }
                else
                {
                    dt = pc.exec_Sql("select tablename from pg_tables", conn, this.richTextBox2);
                }
                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;

                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].ImageIndex = 4;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].SelectedImageIndex = 4;
                
                 
               
                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].GetNodeCount(false) == 0)
                    {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].Nodes.Add(dt.Rows[i][0].ToString(),
                            dt.Rows[i][0].ToString());
                       
                            columns =
                                pc.exec_Sql(
                                    "SELECT column_name, data_type FROM information_schema.columns WHERE table_name=" + "'" + dt.Rows[i][0].ToString() + "'", conn, this.richTextBox2);

                        for (int j = 0; j < columns.Rows.Count; j++)
                        {
                            treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].Nodes[i].Nodes.Add(columns.Rows[j][0].ToString(),
                            columns.Rows[j][0].ToString());
                        }
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].Nodes[i].ImageIndex = 4;
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[2].Nodes[i].SelectedImageIndex = 4;
                        columns.Clear();
                    }
                    

                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {


                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();
            }

        }
        private void Triggers_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;


                dt = pc.exec_Sql("select tgname from pg_trigger", conn, this.richTextBox2);
                
                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].ImageIndex = 7;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].SelectedImageIndex = 7;

                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].Nodes.Add(dt.Rows[i][0].ToString());
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].Nodes[i].ImageIndex = 7;
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[3].Nodes[i].SelectedImageIndex = 7;

                    }
                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {

                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();
              
            }

           

        }
        void view_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;


                dt = pc.exec_Sql("select table_name from INFORMATION_SCHEMA.views WHERE table_schema = ANY (current_schemas(false))", conn, this.richTextBox2);

                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].ImageIndex = 8;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].SelectedImageIndex = 8;
                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].Nodes.Add(dt.Rows[i][0].ToString());
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].Nodes[i].ImageIndex = 8;
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[4].Nodes[i].SelectedImageIndex = 8;

                    }

                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {


                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();

            }
        }
        void seq_show(NpgsqlConnection conn)
        {
            try
            {
                DataTable dt = null;
                DataTable columns = null;


                dt = pc.exec_Sql("SELECT c.relname FROM pg_class c WHERE c.relkind = 'S';", conn,this.richTextBox2);

                this.conn = conn;
                int index =
                    treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes.Find(conn.Database, true).First().Index;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[1].ImageIndex = 5;
                treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[1].SelectedImageIndex = 5;

                if (treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[1].GetNodeCount(false) == 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[1].Nodes.Add(dt.Rows[i][0].ToString());
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[1].Nodes[i].ImageIndex = 5;
                        treeView1.Nodes[0].Nodes[0].Nodes[0].Nodes[index].Nodes[1].Nodes[i].SelectedImageIndex = 5;
                    }


                }

                treeView1.Enabled = true;
            }
            catch (Exception msg)
            {


                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();

            }
        }
        private void Tablespaces_Show(NpgsqlConnection conn)
        {
            try
            {

                treeView1.Nodes[0].Nodes[0].Nodes.Add("Tablespaces:", "Tablespaces:");
                treeView1.Nodes[0].Nodes[0].Nodes[1].ImageIndex = 2;
                treeView1.Nodes[0].Nodes[0].Nodes[1].SelectedImageIndex = 2;
                DataTable dt = pc.exec_Sql("select * from pg_tablespace", conn, this.richTextBox2);
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


                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();

            }


        }
        private void Users_Show(NpgsqlConnection conn)
        {
            try
            {

                treeView1.Nodes[0].Nodes[0].Nodes.Add("Login Roles:", "Login Roles:");
                treeView1.Nodes[0].Nodes[0].Nodes[2].ImageIndex = 3;
                treeView1.Nodes[0].Nodes[0].Nodes[2].SelectedImageIndex = 3;
                DataTable dt = pc.exec_Sql("select * from pg_user", conn, this.richTextBox2);
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

                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + msg.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength = msg.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();

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

                DataTable dt = pc.exec_Sql(value, conn, this.richTextBox2);
                dataGridView1.Visible = true;
                dataGridView1.ReadOnly = true;
                dataGridView1.BackgroundColor = Color.White;
                dataGridView1.DataSource = dt;
                this.tabControl1.SelectedIndex = 1;

            }
            catch (Exception ex)
            {


                int length = richTextBox2.TextLength;

                richTextBox2.AppendText(DateTime.Now + " " + ex.Message + "\n");

                richTextBox2.SelectionStart = length;
                int flength =ex.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();
                this.tabControl1.SelectedIndex = 0;
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
                        
                    DataTable dt = pc.exec_Sql(value, conn, this.richTextBox2);
                    dataGridView1.Visible = true;
                    dataGridView1.ReadOnly = true;
                    dataGridView1.BackgroundColor = Color.White;
                    dataGridView1.DataSource = dt;
                    this.tabControl1.SelectedIndex = 1;

                }
                catch (Exception ex)
                {


                    int length = richTextBox2.TextLength;

                    richTextBox2.AppendText(DateTime.Now + " " + ex.Message + "\n");

                    richTextBox2.SelectionStart = length;
                    int flength = ex.Message.Length + DateTime.Now.ToString().Length + 1;
                    richTextBox2.SelectionLength = flength;
                    richTextBox2.SelectionColor = Color.Red;
                    richTextBox2.ScrollToCaret();
                    this.tabControl1.SelectedIndex = 0;
                }
            }
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void createTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTable ct = new CreateTable(conn,richTextBox2);
            ct.ShowDialog();
           NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            seq_show(conn);
            Functions_show(conn);

            }

        private void createTriggerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Trigger_Create tc = new Trigger_Create(conn, richTextBox1,richTextBox2);
            tc.ShowDialog();
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            view_show(conn);
            Functions_show(conn);
            seq_show(conn);
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
            seq_show(conn);
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void sequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Sequence s = new Sequence(conn,richTextBox2);
            s.ShowDialog();
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            view_show(conn);
            Functions_show(conn);
            seq_show(conn);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                

            string selectedNodeText = e.Node.Text;
            if (e.Node.Parent != null)
            {
                if (e.Node.Parent.Text == "Functions:")
                {
                    
                   richTextBox1.Text= pc.exec_Sql(
                        "SELECT pg_get_functiondef(p.oid) FROM   pg_proc p JOIN   pg_namespace n ON n.oid = p.pronamespace WHERE  p.proname ILIKE "+"'"+e.Node.Text+"'", conn, this.richTextBox2).Rows[0][0].ToString();
                }
                if (e.Node.Parent.Text == "Sequences:")
                {
                   

                }
                if (e.Node.Parent.Text == "Tables:")
                {
                    EditTable et = new EditTable(conn,e.Node.Text,richTextBox2);
                    et.ShowDialog();
                }
                if (e.Node.Parent.Text == "Triggers:")
                {
                    DataTable dt = null;
                    richTextBox1.Clear();
                    string ddl = "CREATE ";

                    dt = pc.exec_Sql(
                            "SELECT event_object_table, trigger_name, event_manipulation, action_statement, action_timing , action_condition FROM information_schema.triggers where trigger_name ="+ "'" + e.Node.Text + "'", conn, this.richTextBox2);
                    ddl += dt.Rows[0][1].ToString();
                    ddl += "\n";
                    ddl += dt.Rows[0][4].ToString() + " ";
                    ddl += dt.Rows[0][2].ToString() + " ON ";
                    ddl += dt.Rows[0][0].ToString() + " \n";
                    ddl += " FOR EACH ROW \n";
                    ddl+= dt.Rows[0][5].ToString() + "\n";
                    ddl += dt.Rows[0][3].ToString() + ";\n";
                    richTextBox1.Text = ddl;
                }
                if (e.Node.Parent.Text == "Views:")
                {
                  richTextBox1.Clear();
                    string ddl = "CREATE OR REPLACE ";
                    DataTable dt = null;
                    richTextBox1.Clear();
                    dt = pc.exec_Sql(
                            "select * from pg_views  where schemaname != 'pg_catalog' and schemaname != 'information_schema' and viewname=" +"'"+e.Node.Text+"'", conn, this.richTextBox2);

                    ddl += dt.Rows[0][1].ToString();
                    ddl += " AS \n";
                    ddl += dt.Rows[0][3].ToString();
                    richTextBox1.Text = ddl;
                }
            }

            }
            catch (Exception ex)
            {
                int length = richTextBox2.TextLength; 
                if(ex.Message== "There is no row at position 0")
                richTextBox2.AppendText("Error en Base de Datos"+"\n");
                else
                {
                    richTextBox2.AppendText(ex.Message + "\n");
                }
                richTextBox2.SelectionStart = length;
                int flength = ex.Message.Length + DateTime.Now.ToString().Length + 1;
                richTextBox2.SelectionLength = flength;
                richTextBox2.SelectionColor = Color.Red;
                richTextBox2.ScrollToCaret();



             
            }

        }

        private void createViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View v = new View(conn,richTextBox2);
            v.ShowDialog();
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(conn.ConnectionString);
            treeView1.Nodes.Clear();
            firstLoad();
            show_components(builder.Host, builder.Port.ToString(), builder.UserName, System.Text.Encoding.UTF8.GetString(builder.Password), conn.Database);
            Triggers_show(conn);
            Tables_show(conn);
            view_show(conn);
            Functions_show(conn);
            seq_show(conn);
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_Insert(object sender, ModificationEventArgs e)
        {
            if (e.LinesAdded != 0)
                UpdateLineNumbers(richTextBox1.LineFromPosition(e.Position));
        }

        private void richTextBox1_Delete(object sender, ModificationEventArgs e)
        {
            if (e.LinesAdded != 0)
                UpdateLineNumbers(richTextBox1.LineFromPosition(e.Position));
        }
    }
}
