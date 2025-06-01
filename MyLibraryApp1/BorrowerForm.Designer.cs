//namespace MyLibraryApp1
//{
//    partial class BorrowerForm
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.txtName = new System.Windows.Forms.TextBox();
//            this.txtemail = new System.Windows.Forms.TextBox();
//            this.txtPhone = new System.Windows.Forms.TextBox();
//            this.btnSave = new System.Windows.Forms.Button();
//            this.btnCancel = new System.Windows.Forms.Button();
//            this.lblName = new System.Windows.Forms.Label();
//            this.lblEmail = new System.Windows.Forms.Label();
//            this.lblPhone = new System.Windows.Forms.Label();
//            this.lblID = new System.Windows.Forms.Label();
//            this.txtID = new System.Windows.Forms.TextBox();
//            this.SuspendLayout();
//            // 
//            // txtName
//            // 
//            this.txtName.Location = new System.Drawing.Point(203, 66);
//            this.txtName.Name = "txtName";
//            this.txtName.Size = new System.Drawing.Size(165, 22);
//            this.txtName.TabIndex = 0;
//            // 
//            // txtemail
//            // 
//            this.txtemail.Location = new System.Drawing.Point(203, 145);
//            this.txtemail.Name = "txtemail";
//            this.txtemail.Size = new System.Drawing.Size(165, 22);
//            this.txtemail.TabIndex = 1;
//            // 
//            // txtPhone
//            // 
//            this.txtPhone.Location = new System.Drawing.Point(203, 231);
//            this.txtPhone.Name = "txtPhone";
//            this.txtPhone.Size = new System.Drawing.Size(165, 22);
//            this.txtPhone.TabIndex = 2;
//            // 
//            // btnSave
//            // 
//            this.btnSave.Location = new System.Drawing.Point(127, 361);
//            this.btnSave.Name = "btnSave";
//            this.btnSave.Size = new System.Drawing.Size(98, 46);
//            this.btnSave.TabIndex = 3;
//            this.btnSave.Text = "Save";
//            this.btnSave.UseVisualStyleBackColor = true;
//            this.btnSave.Click += new System.EventHandler(this.btnSave_Click_1);
//            // 
//            // btnCancel
//            // 
//            this.btnCancel.Location = new System.Drawing.Point(363, 360);
//            this.btnCancel.Name = "btnCancel";
//            this.btnCancel.Size = new System.Drawing.Size(98, 47);
//            this.btnCancel.TabIndex = 4;
//            this.btnCancel.Text = "Cancel";
//            this.btnCancel.UseVisualStyleBackColor = true;
//            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
//            // 
//            // lblName
//            // 
//            this.lblName.AutoSize = true;
//            this.lblName.Location = new System.Drawing.Point(55, 72);
//            this.lblName.Name = "lblName";
//            this.lblName.Size = new System.Drawing.Size(44, 16);
//            this.lblName.TabIndex = 5;
//            this.lblName.Text = "Name";
//            this.lblName.Click += new System.EventHandler(this.lblName_Click);
//            // 
//            // lblEmail
//            // 
//            this.lblEmail.AutoSize = true;
//            this.lblEmail.Location = new System.Drawing.Point(55, 151);
//            this.lblEmail.Name = "lblEmail";
//            this.lblEmail.Size = new System.Drawing.Size(41, 16);
//            this.lblEmail.TabIndex = 6;
//            this.lblEmail.Text = "Email";
//            // 
//            // lblPhone
//            // 
//            this.lblPhone.AutoSize = true;
//            this.lblPhone.Location = new System.Drawing.Point(55, 237);
//            this.lblPhone.Name = "lblPhone";
//            this.lblPhone.Size = new System.Drawing.Size(46, 16);
//            this.lblPhone.TabIndex = 7;
//            this.lblPhone.Text = "Phone";
//            // 
//            // lblID
//            // 
//            this.lblID.AutoSize = true;
//            this.lblID.Enabled = false;
//            this.lblID.Location = new System.Drawing.Point(55, 302);
//            this.lblID.Name = "lblID";
//            this.lblID.Size = new System.Drawing.Size(20, 16);
//            this.lblID.TabIndex = 8;
//            this.lblID.Text = "ID";
//            // 
//            // txtID
//            // 
//            this.txtID.Location = new System.Drawing.Point(203, 296);
//            this.txtID.Name = "txtID";
//            this.txtID.ReadOnly = true;
//            this.txtID.Size = new System.Drawing.Size(165, 22);
//            this.txtID.TabIndex = 9;
//            // 
//            // BorrowerForm
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(562, 541);
//            this.Controls.Add(this.txtID);
//            this.Controls.Add(this.lblID);
//            this.Controls.Add(this.lblPhone);
//            this.Controls.Add(this.lblEmail);
//            this.Controls.Add(this.lblName);
//            this.Controls.Add(this.btnCancel);
//            this.Controls.Add(this.btnSave);
//            this.Controls.Add(this.txtPhone);
//            this.Controls.Add(this.txtemail);
//            this.Controls.Add(this.txtName);
//            this.Name = "BorrowerForm";
//            this.Text = "BorrowerForm";
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        private System.Windows.Forms.TextBox txtName;
//        private System.Windows.Forms.TextBox txtemail;
//        private System.Windows.Forms.TextBox txtPhone;
//        private System.Windows.Forms.Button btnSave;
//        private System.Windows.Forms.Button btnCancel;
//        private System.Windows.Forms.Label lblName;
//        private System.Windows.Forms.Label lblEmail;
//        private System.Windows.Forms.Label lblPhone;
//        private System.Windows.Forms.Label lblID;
//        private System.Windows.Forms.TextBox txtID;
//    }
//}