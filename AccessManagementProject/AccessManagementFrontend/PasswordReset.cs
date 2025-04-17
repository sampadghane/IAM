using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AccessManagementFrontend
{
    public partial class PasswordReset : Form
    {
        string username = "";
        public PasswordReset(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string newPassword = newpasstxt.Text;
            var data = new { username, newPassword };
            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string url = $"http://localhost:5000/api/User/api/resetpassword?username={username}&newPassword={newPassword}";
            HttpResponseMessage response = await HttpClientManager.Client.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Your password has changed successfully");
                LoginPage dd = new LoginPage();
                this.Hide();
                dd.Show();
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Error: {responseBody}");
            }
        }
    }
}
