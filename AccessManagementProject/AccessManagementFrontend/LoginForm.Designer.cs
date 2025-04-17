namespace AccessManagementFrontend
{
    partial class LoginPage
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
            this.Username_txt = new System.Windows.Forms.TextBox();
            this.Password_txt = new System.Windows.Forms.TextBox();
            this.LoginUsername = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.LoginBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.ForgotPasswordbtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Username_txt
            // 
            this.Username_txt.Location = new System.Drawing.Point(235, 109);
            this.Username_txt.Name = "Username_txt";
            this.Username_txt.Size = new System.Drawing.Size(297, 22);
            this.Username_txt.TabIndex = 0;
            // 
            // Password_txt
            // 
            this.Password_txt.Location = new System.Drawing.Point(235, 156);
            this.Password_txt.Name = "Password_txt";
            this.Password_txt.Size = new System.Drawing.Size(297, 22);
            this.Password_txt.TabIndex = 1;
            // 
            // LoginUsername
            // 
            this.LoginUsername.AutoSize = true;
            this.LoginUsername.Location = new System.Drawing.Point(145, 115);
            this.LoginUsername.Name = "LoginUsername";
            this.LoginUsername.Size = new System.Drawing.Size(70, 16);
            this.LoginUsername.TabIndex = 2;
            this.LoginUsername.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(145, 162);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // LoginBtn
            // 
            this.LoginBtn.Location = new System.Drawing.Point(457, 218);
            this.LoginBtn.Name = "LoginBtn";
            this.LoginBtn.Size = new System.Drawing.Size(75, 23);
            this.LoginBtn.TabIndex = 4;
            this.LoginBtn.Text = "Login";
            this.LoginBtn.UseVisualStyleBackColor = true;
            this.LoginBtn.Click += new System.EventHandler(this.LoginBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.Location = new System.Drawing.Point(235, 218);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 5;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // ForgotPasswordbtn
            // 
            this.ForgotPasswordbtn.Location = new System.Drawing.Point(301, 292);
            this.ForgotPasswordbtn.Name = "ForgotPasswordbtn";
            this.ForgotPasswordbtn.Size = new System.Drawing.Size(155, 23);
            this.ForgotPasswordbtn.TabIndex = 6;
            this.ForgotPasswordbtn.Text = "forgot password";
            this.ForgotPasswordbtn.UseVisualStyleBackColor = true;
            this.ForgotPasswordbtn.Click += new System.EventHandler(this.ForgotPasswordbtn_Click);
            // 
            // LoginPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ForgotPasswordbtn);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.LoginBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.LoginUsername);
            this.Controls.Add(this.Password_txt);
            this.Controls.Add(this.Username_txt);
            this.Name = "LoginPage";
            this.Text = "Login Page";
            this.Load += new System.EventHandler(this.LoginPage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Username_txt;
        private System.Windows.Forms.TextBox Password_txt;
        private System.Windows.Forms.Label LoginUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LoginBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button ForgotPasswordbtn;
    }
}

