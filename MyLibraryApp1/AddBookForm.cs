using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyLibraryApp1
{
    public partial class AddBookForm : Form
    {
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtYear;
        private TextBox txtAvailable;
        private Button btnSave;
        private Button btnCancel;

        public AddBookForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // Form settings
            this.Text = "Add New Book";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(34, 40, 49);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Main panel for padding
            var mainPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 20, 30, 20),
                BackColor = this.BackColor,
            };
            this.Controls.Add(mainPanel);

            // Title Label & TextBox
            var lblTitle = new Label() { Text = "Title", ForeColor = Color.White, AutoSize = true };
            txtTitle = CreateModernTextBox();

            // Author Label & TextBox
            var lblAuthor = new Label() { Text = "Author", ForeColor = Color.White, AutoSize = true };
            txtAuthor = CreateModernTextBox();

            // Year Label & TextBox
            var lblYear = new Label() { Text = "Year", ForeColor = Color.White, AutoSize = true };
            txtYear = CreateModernTextBox();

            // Available Copies Label & TextBox
            var lblAvailable = new Label() { Text = "Available Copies", ForeColor = Color.White, AutoSize = true };
            txtAvailable = CreateModernTextBox();

            // Save Button
            btnSave = new Button()
            {
                Text = "Save",
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 40),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Right,
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = Color.FromArgb(0, 180, 166);
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = Color.FromArgb(0, 150, 136);

            // Cancel Button
            btnCancel = new Button()
            {
                Text = "Cancel",
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 40),
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Left,
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += btnCancel_Click;
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(229, 57, 53);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.FromArgb(244, 67, 54);

            // Layout with TableLayoutPanel
            var tblLayout = new TableLayoutPanel()
            {
                RowCount = 5,
                ColumnCount = 2,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 0, 0, 20),
            };
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            tblLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));

            // Add controls to layout
            tblLayout.Controls.Add(lblTitle, 0, 0);
            tblLayout.Controls.Add(txtTitle, 1, 0);

            tblLayout.Controls.Add(lblAuthor, 0, 1);
            tblLayout.Controls.Add(txtAuthor, 1, 1);

            tblLayout.Controls.Add(lblYear, 0, 2);
            tblLayout.Controls.Add(txtYear, 1, 2);

            tblLayout.Controls.Add(lblAvailable, 0, 3);
            tblLayout.Controls.Add(txtAvailable, 1, 3);

            // Buttons panel
            var btnPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Fill,
                AutoSize = true,
                Padding = new Padding(0),
                Margin = new Padding(0),
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnCancel);

            tblLayout.Controls.Add(btnPanel, 1, 4);

            mainPanel.Controls.Add(tblLayout);

            // Tooltips
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtTitle, "Enter the book's title");
            toolTip.SetToolTip(txtAuthor, "Enter the author name");
            toolTip.SetToolTip(txtYear, "Enter the publication year (numeric)");
            toolTip.SetToolTip(txtAvailable, "Enter how many copies are available");
        }

        private TextBox CreateModernTextBox()
        {
            return new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(50, 50, 50),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10F),
                Margin = new Padding(0, 5, 0, 5),
                Width = 200,
            };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Trimmed input values
            string title = txtTitle.Text.Trim();
            string author = txtAuthor.Text.Trim();
            string yearText = txtYear.Text.Trim();
            string availableText = txtAvailable.Text.Trim();

            // Input Validation
            if (string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Title cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(author))
            {
                MessageBox.Show("Author cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(yearText, out int year) || year <= 0)
            {
                MessageBox.Show("Please enter a valid publication year (positive number).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(availableText, out int available) || available < 0)
            {
                MessageBox.Show("Available copies must be a valid non-negative number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Database Operation
            try
            {
                string dbPath = Path.Combine(Application.StartupPath, "MyLibrary.db");
                string connectionString = $"Data Source={dbPath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string insertQuery = @"
                INSERT INTO Books (Title, Author, Year, AvailableCopies)
                VALUES (@Title, @Author, @Year, @Available)
            ";

                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", title);
                        cmd.Parameters.AddWithValue("@Author", author);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@Available", available);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Book added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while saving the book:\n" + ex.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
