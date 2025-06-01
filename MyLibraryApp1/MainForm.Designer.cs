namespace MyLibraryApp1
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnDeleteBook = new System.Windows.Forms.Button();
            this.btnEditBook = new System.Windows.Forms.Button();
            this.btnAddBook = new System.Windows.Forms.Button();
            this.dgvBooks = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnReturnBook = new System.Windows.Forms.Button();
            this.btnIssueBook = new System.Windows.Forms.Button();
            this.btnDeleteBorrowers = new System.Windows.Forms.Button();
            this.btnEditBorrowers = new System.Windows.Forms.Button();
            this.btnAddBorrowers = new System.Windows.Forms.Button();
            this.dgvBorrowers = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(860, 500);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.tabPage1.Controls.Add(this.btnDeleteBook);
            this.tabPage1.Controls.Add(this.btnEditBook);
            this.tabPage1.Controls.Add(this.btnAddBook);
            this.tabPage1.Controls.Add(this.dgvBooks);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(852, 473);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Books Management";
            // 
            // btnDeleteBook
            // 
            this.btnDeleteBook.BackColor = System.Drawing.Color.Transparent;
            this.btnDeleteBook.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnDeleteBook.Location = new System.Drawing.Point(25, 176);
            this.btnDeleteBook.Name = "btnDeleteBook";
            this.btnDeleteBook.Size = new System.Drawing.Size(152, 39);
            this.btnDeleteBook.TabIndex = 3;
            this.btnDeleteBook.Text = "Delete Book";
            this.btnDeleteBook.UseVisualStyleBackColor = false;
            this.btnDeleteBook.Click += new System.EventHandler(this.btnDeleteBook_Click);
            // 
            // btnEditBook
            // 
            this.btnEditBook.BackColor = System.Drawing.Color.Transparent;
            this.btnEditBook.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEditBook.Location = new System.Drawing.Point(25, 106);
            this.btnEditBook.Name = "btnEditBook";
            this.btnEditBook.Size = new System.Drawing.Size(152, 39);
            this.btnEditBook.TabIndex = 2;
            this.btnEditBook.Text = "Edit Book";
            this.btnEditBook.UseVisualStyleBackColor = false;
            this.btnEditBook.Click += new System.EventHandler(this.btnEditBook_Click);
            // 
            // btnAddBook
            // 
            this.btnAddBook.BackColor = System.Drawing.Color.Transparent;
            this.btnAddBook.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAddBook.Location = new System.Drawing.Point(25, 35);
            this.btnAddBook.Name = "btnAddBook";
            this.btnAddBook.Size = new System.Drawing.Size(152, 39);
            this.btnAddBook.TabIndex = 1;
            this.btnAddBook.Text = "Add Book";
            this.btnAddBook.UseVisualStyleBackColor = false;
            this.btnAddBook.Click += new System.EventHandler(this.btnAddBook_Click);
            // 
            // dgvBooks
            // 
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            this.dgvBooks.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvBooks.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.Menu;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvBooks.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.SystemColors.InfoText;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBooks.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvBooks.GridColor = System.Drawing.Color.White;
            this.dgvBooks.Location = new System.Drawing.Point(199, 0);
            this.dgvBooks.Name = "dgvBooks";
            this.dgvBooks.RowHeadersWidth = 51;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            this.dgvBooks.RowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvBooks.RowTemplate.Height = 24;
            this.dgvBooks.Size = new System.Drawing.Size(574, 400);
            this.dgvBooks.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.tabPage2.Controls.Add(this.btnReturnBook);
            this.tabPage2.Controls.Add(this.btnIssueBook);
            this.tabPage2.Controls.Add(this.btnDeleteBorrowers);
            this.tabPage2.Controls.Add(this.btnEditBorrowers);
            this.tabPage2.Controls.Add(this.btnAddBorrowers);
            this.tabPage2.Controls.Add(this.dgvBorrowers);
            this.tabPage2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(852, 473);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Borrowers Management";
            // 
            // btnReturnBook
            // 
            this.btnReturnBook.Location = new System.Drawing.Point(23, 364);
            this.btnReturnBook.Name = "btnReturnBook";
            this.btnReturnBook.Size = new System.Drawing.Size(152, 42);
            this.btnReturnBook.TabIndex = 5;
            this.btnReturnBook.Text = "Return Book";
            this.btnReturnBook.UseVisualStyleBackColor = true;
            this.btnReturnBook.Click += new System.EventHandler(this.btnReturnBook_Click);
            // 
            // btnIssueBook
            // 
            this.btnIssueBook.Location = new System.Drawing.Point(23, 283);
            this.btnIssueBook.Name = "btnIssueBook";
            this.btnIssueBook.Size = new System.Drawing.Size(152, 42);
            this.btnIssueBook.TabIndex = 4;
            this.btnIssueBook.Text = "Issue Book";
            this.btnIssueBook.UseVisualStyleBackColor = true;
            this.btnIssueBook.Click += new System.EventHandler(this.btnIssueBook_Click);
            // 
            // btnDeleteBorrowers
            // 
            this.btnDeleteBorrowers.Location = new System.Drawing.Point(23, 200);
            this.btnDeleteBorrowers.Name = "btnDeleteBorrowers";
            this.btnDeleteBorrowers.Size = new System.Drawing.Size(152, 46);
            this.btnDeleteBorrowers.TabIndex = 3;
            this.btnDeleteBorrowers.Text = "Delete Borrowers";
            this.btnDeleteBorrowers.UseVisualStyleBackColor = true;
            this.btnDeleteBorrowers.Click += new System.EventHandler(this.btnDeleteBorrowers_Click);
            // 
            // btnEditBorrowers
            // 
            this.btnEditBorrowers.BackColor = System.Drawing.Color.Transparent;
            this.btnEditBorrowers.ForeColor = System.Drawing.Color.Black;
            this.btnEditBorrowers.Location = new System.Drawing.Point(23, 120);
            this.btnEditBorrowers.Name = "btnEditBorrowers";
            this.btnEditBorrowers.Size = new System.Drawing.Size(152, 43);
            this.btnEditBorrowers.TabIndex = 2;
            this.btnEditBorrowers.Text = "Edit Borrowers";
            this.btnEditBorrowers.UseVisualStyleBackColor = false;
            this.btnEditBorrowers.Click += new System.EventHandler(this.btnEditBorrowers_Click);
            // 
            // btnAddBorrowers
            // 
            this.btnAddBorrowers.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAddBorrowers.Location = new System.Drawing.Point(23, 43);
            this.btnAddBorrowers.Name = "btnAddBorrowers";
            this.btnAddBorrowers.Size = new System.Drawing.Size(152, 42);
            this.btnAddBorrowers.TabIndex = 1;
            this.btnAddBorrowers.Text = "Add Borrowers";
            this.btnAddBorrowers.UseVisualStyleBackColor = true;
            this.btnAddBorrowers.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // dgvBorrowers
            // 
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            this.dgvBorrowers.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvBorrowers.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.dgvBorrowers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvBorrowers.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvBorrowers.Location = new System.Drawing.Point(200, 6);
            this.dgvBorrowers.Name = "dgvBorrowers";
            this.dgvBorrowers.RowHeadersWidth = 51;
            this.dgvBorrowers.RowTemplate.Height = 24;
            this.dgvBorrowers.Size = new System.Drawing.Size(644, 400);
            this.dgvBorrowers.TabIndex = 0;
            // 
            // MainForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.ClientSize = new System.Drawing.Size(860, 500);
            this.Controls.Add(this.tabControl1);
            this.Name = "MainForm";
            this.Text = "MyLibrary - Library Management";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBooks)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBorrowers)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgvBooks;
        private System.Windows.Forms.Button btnDeleteBook;
        private System.Windows.Forms.Button btnEditBook;
        private System.Windows.Forms.Button btnAddBook;
        private System.Windows.Forms.DataGridView dgvBorrowers;
        private System.Windows.Forms.Button btnDeleteBorrowers;
        private System.Windows.Forms.Button btnEditBorrowers;
        private System.Windows.Forms.Button btnAddBorrowers;
        private System.Windows.Forms.Button btnIssueBook;
        private System.Windows.Forms.Button btnReturnBook;
    }
}
