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
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace AccessManagementFrontend
{
    public partial class security_que : Form
    {
        public security_que()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Get the values from the UI
            string username = sharedata_username.username;
            string question = comboBox1.Text;
            string answer = txtAnswer.Text;

            // Correct API URL (no query parameters)
            string apiurl = "http://localhost:5000/api/User/question";

            // Get the HttpClient instance
            var client = HttpClientManager.Client;

            // Remove any existing header values to avoid duplicates
            foreach (var header in new string[] { "X-Username", "X-Question", "X-Answer" })
            {
                if (client.DefaultRequestHeaders.Contains(header))
                    client.DefaultRequestHeaders.Remove(header);
            }

            // Add the values as custom headers
            client.DefaultRequestHeaders.Add("X-Username", username);
            client.DefaultRequestHeaders.Add("X-Question", question);
            client.DefaultRequestHeaders.Add("X-Answer", answer);

            // Send the POST request without a body (null content)
            HttpResponseMessage response = await client.PostAsync(apiurl, null);

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Data Saved!");
                this.Close();
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show($"Error: {response.StatusCode}\n{responseBody}");
            }
        }
    }
}
