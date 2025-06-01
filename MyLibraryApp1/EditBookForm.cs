using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyLibraryApp1
{
    public partial class EditBookForm : Form
    {
        private int bookId;
        private TextBox txtTitle;
        private TextBox txtAuthor;
        private TextBox txtYear;
        private TextBox txtAvailable;
        private Button btnSave;
        private Button btnCancel;

        public EditBookForm(int bookId, string title, string author, int year, int availableCopies)
        {
            this.bookId = bookId;
            InitializeComponent();

            // Pre-fill values
            txtTitle.Text = title;
            txtAuthor.Text = author;
            txtYear.Text = year.ToString();
            txtAvailable.Text = availableCopies.ToString();
        }

        private void InitializeComponent()
        {
            this.Text = "Edit Book";
            this.Size = new Size(400, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(34, 40, 49);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10F);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            var mainPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(30, 20, 30, 20),
                BackColor = this.BackColor,
            };
            this.Controls.Add(mainPanel);

            var lblTitle = new Label() { Text = "Title", AutoSize = true };
            txtTitle = CreateModernTextBox();

            var lblAuthor = new Label() { Text = "Author", AutoSize = true };
            txtAuthor = CreateModernTextBox();

            var lblYear = new Label() { Text = "Year", AutoSize = true };
            txtYear = CreateModernTextBox();

            var lblAvailable = new Label() { Text = "Available Copies", AutoSize = true };
            txtAvailable = CreateModernTextBox();

            btnSave = new Button()
            {
                Text = "Save",
                BackColor = Color.FromArgb(0, 150, 136),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 40),
                Cursor = Cursors.Hand,
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += btnSave_Click;
            btnSave.MouseEnter += (s, e) => btnSave.BackColor = Color.FromArgb(0, 180, 166);
            btnSave.MouseLeave += (s, e) => btnSave.BackColor = Color.FromArgb(0, 150, 136);

            btnCancel = new Button()
            {
                Text = "Cancel",
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 40),
                Cursor = Cursors.Hand,
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += btnCancel_Click;
            btnCancel.MouseEnter += (s, e) => btnCancel.BackColor = Color.FromArgb(229, 57, 53);
            btnCancel.MouseLeave += (s, e) => btnCancel.BackColor = Color.FromArgb(244, 67, 54);

            var tblLayout = new TableLayoutPanel()
            {
                RowCount = 5,
                ColumnCount = 2,
                Dock = DockStyle.Top,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Padding = new Padding(0, 0, 0, 20),
            };
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
            tblLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));

            tblLayout.Controls.Add(lblTitle, 0, 0);
            tblLayout.Controls.Add(txtTitle, 1, 0);

            tblLayout.Controls.Add(lblAuthor, 0, 1);
            tblLayout.Controls.Add(txtAuthor, 1, 1);

            tblLayout.Controls.Add(lblYear, 0, 2);
            tblLayout.Controls.Add(txtYear, 1, 2);

            tblLayout.Controls.Add(lblAvailable, 0, 3);
            tblLayout.Controls.Add(txtAvailable, 1, 3);

            var btnPanel = new FlowLayoutPanel()
            {
                FlowDirection = FlowDirection.RightToLeft,
                AutoSize = true,
                Dock = DockStyle.Fill,
            };
            btnPanel.Controls.Add(btnSave);
            btnPanel.Controls.Add(btnCancel);
            tblLayout.Controls.Add(btnPanel, 1, 4);

            mainPanel.Controls.Add(tblLayout);

            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtTitle, "Enter the book's title");
            toolTip.SetToolTip(txtAuthor, "Enter the author's name");
            toolTip.SetToolTip(txtYear, "Enter the year of publication");
            toolTip.SetToolTip(txtAvailable, "Enter available copies count");
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
            if (string.IsNullOrWhiteSpace(txtTitle.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) ||
                string.IsNullOrWhiteSpace(txtYear.Text) ||
                string.IsNullOrWhiteSpace(txtAvailable.Text))
            {
                MessageBox.Show("Please fill in all fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtYear.Text, out int year) || year < 0)
            {
                MessageBox.Show("Please enter a valid year.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtAvailable.Text, out int available) || available < 0)
            {
                MessageBox.Show("Please enter a valid number of available copies.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string dbPath = Path.Combine(Application.StartupPath, "MyLibrary.db");
                string connectionString = $"Data Source={dbPath};Version=3;";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    string updateQuery = "UPDATE Books SET Title = @Title, Author = @Author, Year = @Year, AvailableCopies = @Available WHERE BookID = @BookID";

                    using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Title", txtTitle.Text);
                        cmd.Parameters.AddWithValue("@Author", txtAuthor.Text);
                        cmd.Parameters.AddWithValue("@Year", year);
                        cmd.Parameters.AddWithValue("@Available", available);
                        cmd.Parameters.AddWithValue("@BookID", bookId);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Book updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
