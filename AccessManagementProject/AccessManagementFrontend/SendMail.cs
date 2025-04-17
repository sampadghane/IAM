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
    public partial class SendMail : Form
    {
        public SendMail()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username = usernametxt.Text;
            string question = comboBox1.Text;
            string answer = txtAnswer.Text;
            var data = new { username, question, answer };
            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            string apiurl = $"http://localhost:5000/api/User/send-email?username={username}&question={question}&answer={answer}";
            HttpResponseMessage response = await HttpClientManager.Client.PostAsync(apiurl, content);
            if (response.IsSuccessStatusCode)
            {
                OTPForm dd = new OTPForm(username);
                MessageBox.Show("Email has sent to your email address");
                this.Hide();
                dd.Show();
            }
            else
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                MessageBox.Show(responseBody);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
