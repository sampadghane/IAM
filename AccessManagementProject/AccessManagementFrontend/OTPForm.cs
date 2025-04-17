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
    public partial class OTPForm : Form
    {
        string username = "";
        public OTPForm(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string otp = otptxt.Text;
            var data = new { username, otp };
            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string apiUrl = $"http://localhost:5000/api/User/api/check-otp?username={username}&otp={otp}";
            HttpResponseMessage response = await HttpClientManager.Client.PostAsync(apiUrl, content);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("OTP verification successful, You can reset Your password");
                PasswordReset dd = new PasswordReset(username);
                this.Hide();
                dd.Show();
            }
            else
            {
                string responsebody = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responsebody);
            }
        }
    }
}
