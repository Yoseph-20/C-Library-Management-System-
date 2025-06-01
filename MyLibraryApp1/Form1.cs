using System;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyLibraryApp1
{
    public partial class Form1 : Form
    {
        private string dbPath;

        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;

            // Set database path to executable folder
            dbPath = Path.Combine(Application.StartupPath, "MyLibrary.db");

            EnsureDatabase(); // Create DB/tables on first run
        }

        private void EnsureDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile(dbPath);
                MessageBox.Show("Database created at:\n" + dbPath, "Info");
            }

            using (var conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
            {
                conn.Open();
                string query = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Username TEXT NOT NULL,
                        Password TEXT NOT NULL
                    );

                    INSERT INTO Users (Username, Password)
                    SELECT 'admin', '123'
                    WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'admin');

                    CREATE TABLE IF NOT EXISTS Books (
                        BookID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Title TEXT NOT NULL,
                        Author TEXT NOT NULL,
                        Year INTEGER NOT NULL,
                        AvailableCopies INTEGER NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS Borrowers (
                        BorrowerID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL,
                        Email TEXT NOT NULL,
                        Phone TEXT NOT NULL
                    );

                    CREATE TABLE IF NOT EXISTS IssuedBooks (
                        IssueID INTEGER PRIMARY KEY AUTOINCREMENT,
                        BookID INTEGER NOT NULL,
                        BorrowerID INTEGER NOT NULL,
                        IssueDate TEXT NOT NULL,
                        DueDate TEXT NOT NULL,
                        FOREIGN KEY (BookID) REFERENCES Books(BookID),
                        FOREIGN KEY (BorrowerID) REFERENCES Borrowers(BorrowerID)
                    );
                ";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Input Validation for Login
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter your username.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM Users WHERE Username=@username AND Password=@password";

                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (count == 1)
                        {
                            MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            MainForm main = new MainForm(); // Assuming you have this
                            main.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            // Input Validation for Registration
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Please enter a username.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter a password.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            if (username.Length < 3 || username.Length > 20)
            {
                MessageBox.Show("Username must be between 3 and 20 characters.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (password.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters long.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection($"Data Source={dbPath};Version=3;"))
                {
                    conn.Open();

                    string checkQuery = "SELECT COUNT(*) FROM Users WHERE Username = @username";
                    using (var checkCmd = new SQLiteCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@username", username);
                        int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (exists > 0)
                        {
                            MessageBox.Show("Username already exists.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    string insertQuery = "INSERT INTO Users (Username, Password) VALUES (@username, @password)";
                    using (var insertCmd = new SQLiteCommand(insertQuery, conn))
                    {
                        insertCmd.Parameters.AddWithValue("@username", username);
                        insertCmd.Parameters.AddWithValue("@password", password);
                        insertCmd.ExecuteNonQuery();
                    }

                    conn.Close();
                    MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Registration error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Label label, label2, lblTitle;
        private TextBox txtUsername, txtPassword;
        private Button btnLogin, btnRegister;

        private void InitializeComponent()
        {
            this.label = new Label();
            this.label2 = new Label();
            this.txtUsername = new TextBox();
            this.txtPassword = new TextBox();
            this.btnLogin = new Button();
            this.btnRegister = new Button();
            this.lblTitle = new Label();
            this.SuspendLayout();

            // Form1
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 500);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "MyLibrary - Login";

            // lblTitle
            this.lblTitle.Text = "Welcome to MyLibrary";
            this.lblTitle.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(0, 30);
            this.lblTitle.Size = new Size(800, 60);
            this.lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            this.Controls.Add(this.lblTitle);

            // label (Username)
            this.label.AutoSize = true;
            this.label.Location = new Point(150, 140);
            this.label.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.label.ForeColor = Color.White;
            this.label.Text = "Username:";
            this.Controls.Add(this.label);

            // txtUsername
            this.txtUsername.Location = new Point(150, 170);
            this.txtUsername.Size = new Size(500, 30);
            this.txtUsername.Font = new Font("Segoe UI", 11F);
            this.txtUsername.ForeColor = Color.White;
            this.txtUsername.BackColor = Color.FromArgb(45, 45, 48);
            this.txtUsername.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.txtUsername);

            // label2 (Password)
            this.label2.AutoSize = true;
            this.label2.Location = new Point(150, 230);
            this.label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.label2.ForeColor = Color.White;
            this.label2.Text = "Password:";
            this.Controls.Add(this.label2);

            // txtPassword
            this.txtPassword.Location = new Point(150, 260);
            this.txtPassword.Size = new Size(500, 30);
            this.txtPassword.Font = new Font("Segoe UI", 11F);
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.ForeColor = Color.White;
            this.txtPassword.BackColor = Color.FromArgb(45, 45, 48);
            this.txtPassword.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(this.txtPassword);

            // btnLogin
            this.btnLogin.Location = new Point(150, 330);
            this.btnLogin.Size = new Size(500, 45);
            this.btnLogin.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.btnLogin.Text = "Login";
            this.btnLogin.FlatStyle = FlatStyle.Flat;
            this.btnLogin.FlatAppearance.BorderSize = 0;
            this.btnLogin.BackColor = Color.FromArgb(0, 122, 204);
            this.btnLogin.ForeColor = Color.White;
            this.btnLogin.Cursor = Cursors.Hand;
            this.btnLogin.Click += new EventHandler(this.btnLogin_Click);
            this.Controls.Add(this.btnLogin);

            // btnRegister
            this.btnRegister.Location = new Point(150, 390);
            this.btnRegister.Size = new Size(500, 45);
            this.btnRegister.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            this.btnRegister.Text = "Register";
            this.btnRegister.FlatStyle = FlatStyle.Flat;
            this.btnRegister.FlatAppearance.BorderSize = 0;
            this.btnRegister.BackColor = Color.FromArgb(28, 151, 234);
            this.btnRegister.ForeColor = Color.White;
            this.btnRegister.Cursor = Cursors.Hand;
            this.btnRegister.Click += new EventHandler(this.btnRegister_Click);
            this.Controls.Add(this.btnRegister);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
