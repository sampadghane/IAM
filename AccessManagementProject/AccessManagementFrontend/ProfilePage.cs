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
    public partial class ProfilePage : Form
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void ProfilePage_Load(object sender, EventArgs e)
        {
            string url = "http://localhost:5000/api/User/api/getcurrentuser";

            HttpResponseMessage res = await HttpClientManager.Client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                string content = await res.Content.ReadAsStringAsync();

                dynamic response = JsonConvert.DeserializeObject(content);
                dynamic user = response.user;

                nametxt.Text = user.name;
                roletxt.Text = user.role;
                usernametxt.Text = user.username;

            }
            else
            {
                MessageBox.Show("Error while fetching user information");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDashboard dd=new UserDashboard();
            dd.Show();
        }
    }
}
