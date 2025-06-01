using System;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyLibraryApp1
{
    public partial class MainForm : Form
    {
        private string dbPath;

        public MainForm()
        {
            InitializeComponent();

            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;

            dbPath = Path.Combine(Application.StartupPath, "MyLibrary.db");
        }

        private SQLiteConnection GetConnection()
        {
            return new SQLiteConnection($"Data Source={dbPath};Version=3;");
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadBooks();
            LoadBorrowers();
        }

        public void LoadBooks()
        {
            try
            {
                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    string selectQuery = "SELECT BookID, Title, Author, Year, AvailableCopies FROM Books";

                    using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn))
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvBooks.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading books: " + ex.Message);
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBookForm addForm = new AddBookForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadBooks();
            }
        }

        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBooks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a book to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                object bookIdObj = dgvBooks.SelectedRows[0].Cells["BookID"].Value;
                if (bookIdObj == null || !int.TryParse(bookIdObj.ToString(), out int bookId))
                {
                    MessageBox.Show("Selected book ID is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                DialogResult result = MessageBox.Show("Are you sure you want to delete this book?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Books WHERE BookID = @BookID";

                    using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BookID", bookId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Book deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBooks();
            }
            catch (SQLiteException sqlEx)
            {
                MessageBox.Show("Database error deleting book: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error deleting book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBooks.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a book to edit.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dgvBooks.SelectedRows[0];

                if (!int.TryParse(row.Cells["BookID"].Value?.ToString(), out int bookId))
                {
                    MessageBox.Show("Selected book ID is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string title = row.Cells["Title"].Value?.ToString() ?? "";
                string author = row.Cells["Author"].Value?.ToString() ?? "";

                if (!int.TryParse(row.Cells["Year"].Value?.ToString(), out int year))
                {
                    MessageBox.Show("Book year is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!int.TryParse(row.Cells["AvailableCopies"].Value?.ToString(), out int available))
                {
                    MessageBox.Show("Available copies count is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                EditBookForm editForm = new EditBookForm(bookId, title, author, year, available);
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    LoadBooks();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error editing book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LoadBorrowers()
        {
            try
            {
                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    string selectQuery = "SELECT BorrowerID, Name, Phone, Email FROM Borrowers";

                    using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, conn))
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        dgvBorrowers.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading borrowers: " + ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            BorrowerForm borrowerForm = new BorrowerForm();
            if (borrowerForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SQLiteConnection conn = GetConnection())
                    {
                        conn.Open();
                        string insertQuery = "INSERT INTO Borrowers (Name, Email, Phone) VALUES (@Name, @Email, @Phone)";

                        using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", borrowerForm.BorrowerName);
                            cmd.Parameters.AddWithValue("@Email", borrowerForm.BorrowerEmail);
                            cmd.Parameters.AddWithValue("@Phone", borrowerForm.BorrowerPhone);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Borrower added successfully.");
                    LoadBorrowers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding borrower: " + ex.Message);
                }
            }
        }

        private void btnEditBorrowers_Click(object sender, EventArgs e)
        {
            if (dgvBorrowers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a borrower to edit.");
                return;
            }

            DataGridViewRow row = dgvBorrowers.SelectedRows[0];
            int borrowerId = Convert.ToInt32(row.Cells["BorrowerID"].Value);
            string name = row.Cells["Name"].Value.ToString();
            string email = row.Cells["Email"].Value.ToString();
            string phone = row.Cells["Phone"].Value.ToString();

            BorrowerForm borrowerForm = new BorrowerForm(borrowerId, name, email, phone);
            if (borrowerForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (SQLiteConnection conn = GetConnection())
                    {
                        conn.Open();
                        string updateQuery = "UPDATE Borrowers SET Name = @Name, Email = @Email, Phone = @Phone WHERE BorrowerID = @BorrowerID";

                        using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@Name", borrowerForm.BorrowerName);
                            cmd.Parameters.AddWithValue("@Email", borrowerForm.BorrowerEmail);
                            cmd.Parameters.AddWithValue("@Phone", borrowerForm.BorrowerPhone);
                            cmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Borrower updated successfully.");
                    LoadBorrowers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating borrower: " + ex.Message);
                }
            }
        }

        private void btnDeleteBorrowers_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBorrowers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a borrower to delete.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataGridViewRow row = dgvBorrowers.SelectedRows[0];

                if (!int.TryParse(row.Cells["BorrowerID"].Value?.ToString(), out int borrowerId))
                {
                    MessageBox.Show("Selected borrower ID is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var result = MessageBox.Show("Are you sure you want to delete this borrower?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result != DialogResult.Yes)
                    return;

                using (SQLiteConnection conn = GetConnection())
                {
                    conn.Open();
                    string deleteQuery = "DELETE FROM Borrowers WHERE BorrowerID = @BorrowerID";

                    using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@BorrowerID", borrowerId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Borrower deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBorrowers();
            }
            catch (SQLiteException sqlEx)
            {
                MessageBox.Show("Database error deleting borrower: " + sqlEx.Message, "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error deleting borrower: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIssueBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBorrowers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a borrower first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                object borrowerIdObj = dgvBorrowers.SelectedRows[0].Cells["BorrowerID"].Value;
                if (borrowerIdObj == null || !int.TryParse(borrowerIdObj.ToString(), out int borrowerID))
                {
                    MessageBox.Show("Selected borrower ID is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (IssueBookForm issueForm = new IssueBookForm(this))
                {
                    issueForm.ShowDialog();
                }

                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error issuing book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturnBook_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBorrowers.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Please select a borrower first.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                object borrowerIdObj = dgvBorrowers.SelectedRows[0].Cells["BorrowerID"].Value;
                if (borrowerIdObj == null || !int.TryParse(borrowerIdObj.ToString(), out int borrowerId))
                {
                    MessageBox.Show("Selected borrower ID is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (ReturnBookForm returnForm = new ReturnBookForm(borrowerId))
                {
                    returnForm.ShowDialog();
                }

                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unexpected error returning book: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
