using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyLibraryApp1
{
    public partial class ReturnBookForm : Form
    {
        private string connectionString;
        private int borrowerId;

        private DataGridView dgvIssuedBooks;
        private Button btnReturn;

        public ReturnBookForm(int borrowerId)
        {
            this.borrowerId = borrowerId;
            // Use dynamic path for DB
            string dbPath = Path.Combine(Application.StartupPath, "MyLibrary.db");
            connectionString = $"Data Source={dbPath};Version=3;";

            InitializeComponent();
            LoadIssuedBooks();
        }

        private void InitializeComponent()
        {
            this.dgvIssuedBooks = new DataGridView();
            this.btnReturn = new Button();

            // Form settings
            this.Text = "Return Book";
            this.Size = new Size(500, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // DataGridView
            dgvIssuedBooks.Location = new Point(20, 20);
            dgvIssuedBooks.Size = new Size(440, 250);
            dgvIssuedBooks.ReadOnly = true;
            dgvIssuedBooks.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvIssuedBooks.BackgroundColor = Color.FromArgb(45, 45, 45);
            dgvIssuedBooks.DefaultCellStyle.BackColor = Color.FromArgb(40, 40, 40);
            dgvIssuedBooks.DefaultCellStyle.ForeColor = Color.White;
            dgvIssuedBooks.DefaultCellStyle.SelectionBackColor = Color.FromArgb(70, 130, 180);
            dgvIssuedBooks.DefaultCellStyle.SelectionForeColor = Color.White;

            // Return button
            btnReturn.Text = "Return Book";
            btnReturn.Size = new Size(120, 35);
            btnReturn.Location = new Point(20, 290);
            btnReturn.BackColor = Color.FromArgb(50, 150, 250);
            btnReturn.FlatStyle = FlatStyle.Flat;
            btnReturn.Click += btnReturn_Click;

            // Add controls
            this.Controls.Add(dgvIssuedBooks);
            this.Controls.Add(btnReturn);
        }

        private void LoadIssuedBooks()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter(@"
                        SELECT i.IssueID, b.Title AS BookTitle, i.DueDate 
                        FROM IssuedBooks i
                        JOIN Books b ON i.BookID = b.BookID
                        WHERE i.BorrowerID = @BorrowerID", conn);
                    da.SelectCommand.Parameters.AddWithValue("@BorrowerID", borrowerId);

                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvIssuedBooks.DataSource = dt;

                    dgvIssuedBooks.Columns["IssueID"].Visible = false;
                    dgvIssuedBooks.Columns["BookTitle"].HeaderText = "Book Title";
                    dgvIssuedBooks.Columns["DueDate"].HeaderText = "Due Date";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to load issued books.\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvIssuedBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a record to return.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Safer parsing of IssueID
            var issueIdObj = dgvIssuedBooks.SelectedRows[0].Cells["IssueID"].Value;
            if (issueIdObj == null || !int.TryParse(issueIdObj.ToString(), out int issueId))
            {
                MessageBox.Show("Selected record's IssueID is invalid.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    using (var transaction = conn.BeginTransaction())
                    {
                        // Get BookID safely
                        var getBookIdCmd = new SQLiteCommand("SELECT BookID FROM IssuedBooks WHERE IssueID = @IssueID", conn, transaction);
                        getBookIdCmd.Parameters.AddWithValue("@IssueID", issueId);
                        object bookIdObj = getBookIdCmd.ExecuteScalar();

                        if (bookIdObj == null || !int.TryParse(bookIdObj.ToString(), out int bookId))
                        {
                            MessageBox.Show("Failed to find the book associated with this issue record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            transaction.Rollback();
                            return;
                        }

                        // Delete from IssuedBooks
                        var deleteCmd = new SQLiteCommand("DELETE FROM IssuedBooks WHERE IssueID = @IssueID", conn, transaction);
                        deleteCmd.Parameters.AddWithValue("@IssueID", issueId);
                        int rowsDeleted = deleteCmd.ExecuteNonQuery();

                        if (rowsDeleted == 0)
                        {
                            MessageBox.Show("Failed to delete the issue record. It may have already been returned.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            transaction.Rollback();
                            return;
                        }

                        // Update AvailableCopies
                        var updateCmd = new SQLiteCommand("UPDATE Books SET AvailableCopies = AvailableCopies + 1 WHERE BookID = @BookID", conn, transaction);
                        updateCmd.Parameters.AddWithValue("@BookID", bookId);
                        updateCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                }

                MessageBox.Show("Book returned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadIssuedBooks(); // Refresh the table
            }
            catch (SQLiteException sqlEx)
            {
                MessageBox.Show("Database error returning book:\n" + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error returning book:\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
