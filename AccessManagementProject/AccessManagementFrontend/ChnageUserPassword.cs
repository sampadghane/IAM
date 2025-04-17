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
    public partial class ChnageUserPassword : Form
    {
        public ChnageUserPassword()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username=usernametxt.Text;
            string password=passwordtxt.Text;

            var data = new { username, password };
            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            string url = $"http://localhost:5000/api/User/api/changepassworduser?username={username}&password={password}";

            HttpResponseMessage res = await HttpClientManager.Client.PostAsync(url, content);

            if (res.IsSuccessStatusCode)
            {
                string responsebody = await res.Content.ReadAsStringAsync();
                MessageBox.Show(responsebody);
                usernametxt.Text = "";
                passwordtxt.Text = "";
            }
            else
            {
                string responsebody = await res.Content.ReadAsStringAsync();
                MessageBox.Show(responsebody);
                

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            UserDashboard dd= new UserDashboard();
            this.Hide();
            dd.Show();
        }
    }
}
