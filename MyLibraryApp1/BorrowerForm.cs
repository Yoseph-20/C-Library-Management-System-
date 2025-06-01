using System;
using System.Data.SQLite;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace MyLibraryApp1
{
    public partial class BorrowerForm : Form
    {
        private int borrowerId;

        private Label lblID, lblName, lblEmail, lblPhone;
        private TextBox txtID, txtName, txtemail, txtPhone;
        private Button btnSave, btnCancel;

        public string BorrowerName => txtName.Text.Trim();
        public string BorrowerEmail => txtemail.Text.Trim();
        public string BorrowerPhone => txtPhone.Text.Trim();

        public BorrowerForm()
        {
            InitializeComponent();
        }

        public BorrowerForm(int borrowerId, string name, string email, string phone)
        {
            InitializeComponent();
            this.borrowerId = borrowerId;

            txtID.Text = borrowerId.ToString();
            txtName.Text = name;
            txtemail.Text = email;
            txtPhone.Text = phone;
        }

        private void InitializeComponent()
        {
            this.Text = "Borrower Details";
            this.Size = new Size(350, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(30, 30, 30);
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10);

            lblID = new Label() { Text = "Borrower ID", Location = new Point(30, 20), AutoSize = true };
            txtID = new TextBox() { Location = new Point(30, 45), Size = new Size(260, 25), ReadOnly = true };

            lblName = new Label() { Text = "Name", Location = new Point(30, 80), AutoSize = true };
            txtName = new TextBox() { Location = new Point(30, 105), Size = new Size(260, 25) };

            lblEmail = new Label() { Text = "Email", Location = new Point(30, 140), AutoSize = true };
            txtemail = new TextBox() { Location = new Point(30, 165), Size = new Size(260, 25) };

            lblPhone = new Label() { Text = "Phone", Location = new Point(30, 200), AutoSize = true };
            txtPhone = new TextBox() { Location = new Point(30, 225), Size = new Size(260, 25) };

            btnSave = new Button()
            {
                Text = "Save",
                Location = new Point(30, 260),
                Size = new Size(100, 35),
                BackColor = Color.FromArgb(50, 150, 250),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSave.Click += btnSave_Click;

            btnCancel = new Button()
            {
                Text = "Cancel",
                Location = new Point(190, 260),
                Size = new Size(100, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancel.Click += btnCancel_Click;

            this.Controls.Add(lblID);
            this.Controls.Add(txtID);
            this.Controls.Add(lblName);
            this.Controls.Add(txtName);
            this.Controls.Add(lblEmail);
            this.Controls.Add(txtemail);
            this.Controls.Add(lblPhone);
            this.Controls.Add(txtPhone);
            this.Controls.Add(btnSave);
            this.Controls.Add(btnCancel);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string name = txtName.Text.Trim();
                string email = txtemail.Text.Trim();
                string phone = txtPhone.Text.Trim();

                if (string.IsNullOrWhiteSpace(name))
                {
                    MessageBox.Show("Name cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }
                if (name.Length < 2 || name.Length > 100)
                {
                    MessageBox.Show("Name must be between 2 and 100 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtemail.Focus();
                    return;
                }
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtemail.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("Phone number cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }
                if (!Regex.IsMatch(phone, @"^\d{7,15}$"))
                {
                    MessageBox.Show("Please enter a valid phone number (7–15 digits, numbers only).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPhone.Focus();
                    return;
                }

                // If all validations passed
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                // Log or handle unexpected exceptions here
                MessageBox.Show("An unexpected error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                // Use System.Net.Mail.MailAddress for more reliable email validation
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //private bool IsValidEmail(string email)
        //{
        //    try
        //    {
        //        return Regex.IsMatch(email,
        //            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        //            RegexOptions.IgnoreCase);
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
