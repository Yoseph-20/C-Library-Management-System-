using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyLibraryApp1
{
    public partial class IssueBookForm : Form
    {
        private string connectionString;
        private MainForm mainForm;

        private ComboBox cmbBorrowers;
        private ComboBox cmbBooks;
        private DateTimePicker dtpDueDate;
        private Button btnIssue;

        public IssueBookForm(MainForm form)
        {
            mainForm = form;
            // Use dynamic path for DB
            string dbPath = Path.Combine(Application.StartupPath, "MyLibrary.db");
            connectionString = $"Data Source={dbPath};Version=3;";

            InitializeComponent();
            LoadBorrowers();
            LoadBooks();
        }

        private void InitializeComponent()
        {
            this.cmbBorrowers = new ComboBox();
            this.cmbBooks = new ComboBox();
            this.dtpDueDate = new DateTimePicker();
            this.btnIssue = new Button();

            this.SuspendLayout();

            // Form settings
            this.ClientSize = new Size(300, 250);
            this.Name = "IssueBookForm";
            this.Text = "Issue Book";
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // ComboBox: Borrowers
            cmbBorrowers.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBorrowers.Location = new Point(30, 30);
            cmbBorrowers.Size = new Size(220, 25);

            // ComboBox: Books
            cmbBooks.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbBooks.Location = new Point(30, 70);
            cmbBooks.Size = new Size(220, 25);

            // DateTimePicker: Due Date
            dtpDueDate.Location = new Point(30, 110);
            dtpDueDate.Size = new Size(220, 25);
            dtpDueDate.MinDate = DateTime.Now.AddDays(1);

            // Button: Issue Book
            btnIssue.Text = "Issue Book";
            btnIssue.Location = new Point(30, 160);
            btnIssue.Size = new Size(100, 35);
            btnIssue.BackColor = Color.FromArgb(50, 150, 250);
            btnIssue.FlatStyle = FlatStyle.Flat;
            btnIssue.Click += btnIssue_Click;

            // Add controls to the form
            this.Controls.Add(cmbBorrowers);
            this.Controls.Add(cmbBooks);
            this.Controls.Add(dtpDueDate);
            this.Controls.Add(btnIssue);

            this.ResumeLayout(false);
        }

        private void LoadBorrowers()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    var da = new SQLiteDataAdapter("SELECT BorrowerID, Name FROM Borrowers", conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    cmbBorrowers.DataSource = dt;
                    cmbBorrowers.DisplayMember = "Name";
                    cmbBorrowers.ValueMember = "BorrowerID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load borrowers.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBooks()
        {
            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    var da = new SQLiteDataAdapter("SELECT BookID, Title FROM Books WHERE AvailableCopies > 0", conn);
                    var dt = new DataTable();
                    da.Fill(dt);

                    cmbBooks.DataSource = dt;
                    cmbBooks.DisplayMember = "Title";
                    cmbBooks.ValueMember = "BookID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load books.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (cmbBorrowers.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a borrower.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbBooks.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a book.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int borrowerId = Convert.ToInt32(cmbBorrowers.SelectedValue);
            int bookId = Convert.ToInt32(cmbBooks.SelectedValue);
            DateTime issueDate = DateTime.Now;
            DateTime dueDate = dtpDueDate.Value;

            if (dueDate <= issueDate)
            {
                MessageBox.Show("Due date must be after today.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    // Start transaction
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Insert into IssuedBooks
                        var insertCmd = new SQLiteCommand(
                            "INSERT INTO IssuedBooks (BookID, BorrowerID, IssueDate, DueDate) VALUES (@BookID, @BorrowerID, @IssueDate, @DueDate)",
                            conn, transaction);
                        insertCmd.Parameters.AddWithValue("@BookID", bookId);
                        insertCmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                        insertCmd.Parameters.AddWithValue("@IssueDate", issueDate);
                        insertCmd.Parameters.AddWithValue("@DueDate", dueDate);
                        insertCmd.ExecuteNonQuery();

                        // Update Books
                        var updateCmd = new SQLiteCommand(
                            "UPDATE Books SET AvailableCopies = AvailableCopies - 1 WHERE BookID = @BookID",
                            conn, transaction);
                        updateCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }

                MessageBox.Show("Book issued successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                mainForm.LoadBooks(); // Refresh books in main form
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error issuing book.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
