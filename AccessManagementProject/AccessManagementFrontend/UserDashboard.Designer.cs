namespace AccessManagementFrontend
{
    partial class UserDashboard
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
            this.ChnagePasswordbtn = new System.Windows.Forms.Button();
            this.CreateUserbtn = new System.Windows.Forms.Button();
            this.ViewLogsbtn = new System.Windows.Forms.Button();
            this.profilebtn = new System.Windows.Forms.Button();
            this.ChangeUserPasswordBtn = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ChnagePasswordbtn
            // 
            this.ChnagePasswordbtn.Location = new System.Drawing.Point(60, 37);
            this.ChnagePasswordbtn.Name = "ChnagePasswordbtn";
            this.ChnagePasswordbtn.Size = new System.Drawing.Size(163, 94);
            this.ChnagePasswordbtn.TabIndex = 0;
            this.ChnagePasswordbtn.Text = "Change Your Password";
            this.ChnagePasswordbtn.UseVisualStyleBackColor = true;
            this.ChnagePasswordbtn.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // CreateUserbtn
            // 
            this.CreateUserbtn.Location = new System.Drawing.Point(60, 378);
            this.CreateUserbtn.Name = "CreateUserbtn";
            this.CreateUserbtn.Size = new System.Drawing.Size(163, 88);
            this.CreateUserbtn.TabIndex = 1;
            this.CreateUserbtn.Text = "Create New User";
            this.CreateUserbtn.UseVisualStyleBackColor = true;
            this.CreateUserbtn.Click += new System.EventHandler(this.CreateUserbtn_Click);
            // 
            // ViewLogsbtn
            // 
            this.ViewLogsbtn.Location = new System.Drawing.Point(60, 209);
            this.ViewLogsbtn.Name = "ViewLogsbtn";
            this.ViewLogsbtn.Size = new System.Drawing.Size(163, 94);
            this.ViewLogsbtn.TabIndex = 2;
            this.ViewLogsbtn.Text = "View Logs";
            this.ViewLogsbtn.UseVisualStyleBackColor = true;
            this.ViewLogsbtn.Click += new System.EventHandler(this.ViewLogsbtn_Click);
            // 
            // profilebtn
            // 
            this.profilebtn.Location = new System.Drawing.Point(381, 37);
            this.profilebtn.Name = "profilebtn";
            this.profilebtn.Size = new System.Drawing.Size(163, 94);
            this.profilebtn.TabIndex = 3;
            this.profilebtn.Text = "Profile";
            this.profilebtn.UseVisualStyleBackColor = true;
            this.profilebtn.Click += new System.EventHandler(this.profilebtn_Click);
            // 
            // ChangeUserPasswordBtn
            // 
            this.ChangeUserPasswordBtn.Location = new System.Drawing.Point(381, 209);
            this.ChangeUserPasswordBtn.Name = "ChangeUserPasswordBtn";
            this.ChangeUserPasswordBtn.Size = new System.Drawing.Size(163, 94);
            this.ChangeUserPasswordBtn.TabIndex = 4;
            this.ChangeUserPasswordBtn.Text = "Change User\'s Password";
            this.ChangeUserPasswordBtn.UseVisualStyleBackColor = true;
            this.ChangeUserPasswordBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(675, 209);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(163, 94);
            this.button2.TabIndex = 5;
            this.button2.Text = "Delete User";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(675, 45);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(163, 86);
            this.button3.TabIndex = 6;
            this.button3.Text = "Logout";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(381, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(163, 88);
            this.button1.TabIndex = 7;
            this.button1.Text = "Add security question";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // UserDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 505);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ChangeUserPasswordBtn);
            this.Controls.Add(this.profilebtn);
            this.Controls.Add(this.ViewLogsbtn);
            this.Controls.Add(this.CreateUserbtn);
            this.Controls.Add(this.ChnagePasswordbtn);
            this.Name = "UserDashboard";
            this.Text = "User Dashboard";
            this.Load += new System.EventHandler(this.UserDashboard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ChnagePasswordbtn;
        private System.Windows.Forms.Button CreateUserbtn;
        private System.Windows.Forms.Button ViewLogsbtn;
        private System.Windows.Forms.Button profilebtn;
        private System.Windows.Forms.Button ChangeUserPasswordBtn;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button1;
    }
}