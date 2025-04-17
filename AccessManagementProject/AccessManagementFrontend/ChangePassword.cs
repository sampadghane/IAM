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
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AccessManagementFrontend
{
    public partial class ChangePassword : Form
    {
        

        public ChangePassword()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Get the new password from the textbox
            string newPass = password_txt.Text;

            // Correct API URL (without query parameters)
            string apiurl = "http://localhost:5000/api/User/api/changepassword";

            // Get the HttpClient instance
            var client = HttpClientManager.Client;

            // Remove any previously added header to avoid duplication
            if (client.DefaultRequestHeaders.Contains("X-NewPassword"))
                client.DefaultRequestHeaders.Remove("X-NewPassword");

            // Add the new password as a custom header
            client.DefaultRequestHeaders.Add("X-NewPassword", newPass);

            // Send the POST request with no body (null content)
            HttpResponseMessage res = await client.PostAsync(apiurl, null);

            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("Your password changed successfully");
                this.Hide();
                UserDashboard dd = new UserDashboard();
                dd.Show();
            }
            else
            {
                string responseBody = await res.Content.ReadAsStringAsync();
                MessageBox.Show($"Error: {res.StatusCode}\n{responseBody}");
                password_txt.Text = "";
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDashboard dd = new UserDashboard();
            dd.Show();
        }
    }
}
