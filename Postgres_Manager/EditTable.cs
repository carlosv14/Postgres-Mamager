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
using ScintillaNET;

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

        }
        private void UpdateLineNumbers(int startingAtLine)
        {

            for (int i = startingAtLine; i < richTextBox1.Lines.Count; i++)
            {
                richTextBox1.Lines[i].MarginStyle = Style.LineNumber;
                richTextBox1.Lines[i].MarginText = i.ToString();
            }
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
