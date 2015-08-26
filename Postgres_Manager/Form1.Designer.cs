namespace Postgres_Manager
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.createTableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createTriggerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createFunctionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sequenceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createIndexToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new ScintillaNET.Scintilla();
            this.createTablespaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(0, 27);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(202, 400);
            this.treeView1.TabIndex = 0;
            this.treeView1.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView1_AfterSelect);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(708, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.createTableToolStripMenuItem,
            this.createTriggerToolStripMenuItem,
            this.createFunctionToolStripMenuItem,
            this.sequenceToolStripMenuItem,
            this.createViewToolStripMenuItem,
            this.createIndexToolStripMenuItem,
            this.createTablespaceToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(169, 22);
            this.toolStripMenuItem1.Text = "Create Database";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // createTableToolStripMenuItem
            // 
            this.createTableToolStripMenuItem.Name = "createTableToolStripMenuItem";
            this.createTableToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.createTableToolStripMenuItem.Text = "Create Table";
            this.createTableToolStripMenuItem.Click += new System.EventHandler(this.createTableToolStripMenuItem_Click);
            // 
            // createTriggerToolStripMenuItem
            // 
            this.createTriggerToolStripMenuItem.Name = "createTriggerToolStripMenuItem";
            this.createTriggerToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.createTriggerToolStripMenuItem.Text = "Create Trigger";
            this.createTriggerToolStripMenuItem.Click += new System.EventHandler(this.createTriggerToolStripMenuItem_Click);
            // 
            // createFunctionToolStripMenuItem
            // 
            this.createFunctionToolStripMenuItem.Name = "createFunctionToolStripMenuItem";
            this.createFunctionToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.createFunctionToolStripMenuItem.Text = "Create Function";
            this.createFunctionToolStripMenuItem.Click += new System.EventHandler(this.createFunctionToolStripMenuItem_Click);
            // 
            // sequenceToolStripMenuItem
            // 
            this.sequenceToolStripMenuItem.Name = "sequenceToolStripMenuItem";
            this.sequenceToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.sequenceToolStripMenuItem.Text = "Create Sequence";
            this.sequenceToolStripMenuItem.Click += new System.EventHandler(this.sequenceToolStripMenuItem_Click);
            // 
            // createViewToolStripMenuItem
            // 
            this.createViewToolStripMenuItem.Name = "createViewToolStripMenuItem";
            this.createViewToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.createViewToolStripMenuItem.Text = "Create View";
            this.createViewToolStripMenuItem.Click += new System.EventHandler(this.createViewToolStripMenuItem_Click);
            // 
            // createIndexToolStripMenuItem
            // 
            this.createIndexToolStripMenuItem.Name = "createIndexToolStripMenuItem";
            this.createIndexToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.createIndexToolStripMenuItem.Text = "Create Index";
            this.createIndexToolStripMenuItem.Click += new System.EventHandler(this.createIndexToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Location = new System.Drawing.Point(229, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(31, 24);
            this.button1.TabIndex = 3;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(673, 108);
            this.dataGridView1.TabIndex = 5;
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(0, 3);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.ReadOnly = true;
            this.richTextBox2.Size = new System.Drawing.Size(676, 102);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            this.richTextBox2.TextChanged += new System.EventHandler(this.richTextBox2_TextChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 433);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(684, 137);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(676, 111);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Console";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dataGridView1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(676, 111);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Output";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(229, 27);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(444, 396);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.UseTabs = false;
            this.richTextBox1.Delete += new System.EventHandler<ScintillaNET.ModificationEventArgs>(this.richTextBox1_Delete);
            this.richTextBox1.Insert += new System.EventHandler<ScintillaNET.ModificationEventArgs>(this.richTextBox1_Insert);
            // 
            // createTablespaceToolStripMenuItem
            // 
            this.createTablespaceToolStripMenuItem.Name = "createTablespaceToolStripMenuItem";
            this.createTablespaceToolStripMenuItem.Size = new System.Drawing.Size(169, 22);
            this.createTablespaceToolStripMenuItem.Text = "Create Tablespace";
            this.createTablespaceToolStripMenuItem.Click += new System.EventHandler(this.createTablespaceToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(708, 573);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ToolStripMenuItem createTableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTriggerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createFunctionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sequenceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createViewToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private ScintillaNET.Scintilla richTextBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem createIndexToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createTablespaceToolStripMenuItem;
    }
}

