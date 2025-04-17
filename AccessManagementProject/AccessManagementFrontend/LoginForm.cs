using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MySqlX.XDevAPI;

namespace AccessManagementFrontend
{
    public partial class LoginPage : Form
    {

       
        public LoginPage()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void LoginBtn_Click(object sender, EventArgs e)
        {
            string username = Username_txt.Text;
            string password = Password_txt.Text;
            sharedata_username.username = username;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.");
                return;
            }

            // Set the API URL (no query parameters, no body)
            string apiUrl = "http://localhost:5000/api/User/api/login";

            // Get your HttpClient (assuming HttpClientManager.Client is already defined)
            var client = HttpClientManager.Client;

            // Remove any existing header values (to avoid duplicate header errors)
            if (client.DefaultRequestHeaders.Contains("X-Username"))
                client.DefaultRequestHeaders.Remove("X-Username");
            if (client.DefaultRequestHeaders.Contains("X-Password"))
                client.DefaultRequestHeaders.Remove("X-Password");

            // Add the credentials as headers.
            client.DefaultRequestHeaders.Add("X-Username", username);
            client.DefaultRequestHeaders.Add("X-Password", password);

            // Send the POST request without a body (null content)
            HttpResponseMessage response = await client.PostAsync(apiUrl, null);

            if (response.IsSuccessStatusCode)
            {
                // Read and show the response message.
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responseBody);

                // After a successful login, call another API endpoint (if needed)
                string url = "http://localhost:5000/api/User/api/getcurrentuser";
                HttpResponseMessage res = await client.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    string content1 = await res.Content.ReadAsStringAsync();
                    dynamic response1 = JsonConvert.DeserializeObject(content1);
                    dynamic user = response1.user;

                    // Update shared user info
                    UserInfo.name = user.name;
                    UserInfo.username = user.username;
                    UserInfo.role = user.role;
                }

                // Open the user dashboard
                UserDashboard dd = new UserDashboard();
                this.Hide();
                dd.Show();
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Error: {response.StatusCode}\n{responseBody}");
            }


        }

        private async void LoginPage_Load(object sender, EventArgs e)
        {
           

        }

        private void ForgotPasswordbtn_Click(object sender, EventArgs e)
        {
            SendMail ss = new SendMail();
            this.Hide();
            ss.Show();
        }
    }
    public static class sharedata_username
    {
        public static string username { get; set; }
    }

}
