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

namespace AccessManagementFrontend
{
    public partial class DeleteUserForm : Form
    {
        public DeleteUserForm()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Get the username from the text box.
            string username = usernametxt.Text;

            // Use the correct API URL.
            string url = "http://localhost:5000/api/User/api/deleteuser";

            // Get your HttpClient instance.
            var client = HttpClientManager.Client;

            // Remove any previously added header to avoid duplication.
            if (client.DefaultRequestHeaders.Contains("X-Username"))
                client.DefaultRequestHeaders.Remove("X-Username");

            // Add the username as a custom header.
            client.DefaultRequestHeaders.Add("X-Username", username);

            // Send the POST request with no body.
            HttpResponseMessage res = await client.PostAsync(url, null);

            if (res.IsSuccessStatusCode)
            {
                string responseString = await res.Content.ReadAsStringAsync();
                MessageBox.Show(responseString);
                usernametxt.Text = "";
            }
            else
            {
                string responseString = await res.Content.ReadAsStringAsync();
                MessageBox.Show($"Error: {res.StatusCode}\n{responseString}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserDashboard dd = new UserDashboard();
            this.Hide();
            dd.Show();
        }
    }
}
