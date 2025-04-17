using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AccessManagementFrontend
{
    public partial class CreateUser : Form
    {
        public CreateUser()
        {
            InitializeComponent();
        }
        //SqlConnection conn = new SqlConnection(@"Data Source=PPADGHANE-57L8B;Initial Catalog=iamsystem10;Integrated Security=True");
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Retrieve input values from text boxes
            int id = Convert.ToInt32(idtext.Text);
            string Name = nametxt.Text;
            string username = usernametxt.Text;
            string password = paswordtxt.Text;
            string role = roletxt.Text;
            string email = emailtxt.Text;

            // Use a clean API URL (no query parameters)
            string apiurl = "http://localhost:5000/api/User/api/createUser/";

            var client = HttpClientManager.Client;

            // Remove any existing header values (to avoid duplicates)
            if (client.DefaultRequestHeaders.Contains("X-Name"))
                client.DefaultRequestHeaders.Remove("X-Name");
            if (client.DefaultRequestHeaders.Contains("X-Username"))
                client.DefaultRequestHeaders.Remove("X-Username");
            if (client.DefaultRequestHeaders.Contains("X-Id"))
                client.DefaultRequestHeaders.Remove("X-Id");
            if (client.DefaultRequestHeaders.Contains("X-Password"))
                client.DefaultRequestHeaders.Remove("X-Password");
            if (client.DefaultRequestHeaders.Contains("X-Role"))
                client.DefaultRequestHeaders.Remove("X-Role");
            if (client.DefaultRequestHeaders.Contains("X-Email"))
                client.DefaultRequestHeaders.Remove("X-Email");

            // Add the credentials as headers
            client.DefaultRequestHeaders.Add("X-Name", Name);
            client.DefaultRequestHeaders.Add("X-Username", username);
            client.DefaultRequestHeaders.Add("X-Id", id.ToString());
            client.DefaultRequestHeaders.Add("X-Password", password);
            client.DefaultRequestHeaders.Add("X-Role", role);
            client.DefaultRequestHeaders.Add("X-Email", email);

            // Send the POST request without a body (null content)
            HttpResponseMessage res = await client.PostAsync(apiurl, null);

            if (res.IsSuccessStatusCode)
            {
                MessageBox.Show("User created Successfully");
                this.Hide();
                UserDashboard dd = new UserDashboard();
                dd.Show();
            }
            else
            {
                string responseBody = await res.Content.ReadAsStringAsync();
                MessageBox.Show($"Error: {res.StatusCode}\n{responseBody}");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDashboard dd=new UserDashboard();
            dd.Show();
        }
    }
}
