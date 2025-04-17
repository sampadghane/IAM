using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace AccessManagementFrontend
{
    public partial class LogsForm : Form
    {
        public LogsForm(List<Log> logs)
        {
            InitializeComponent();
            AddScrollablePanel();
            displayLogs(logs);
        }

        private void AddScrollablePanel()
        {
            
            Panel scrollablePanel = new Panel();
            scrollablePanel.Name = "scrollablePanel";
            scrollablePanel.Location = new System.Drawing.Point(10, 10);
            scrollablePanel.Size = new System.Drawing.Size(1000, 500);  // Adjust the panel size
            scrollablePanel.AutoScroll = true;  
            // Add the panel to the form
            this.Controls.Add(scrollablePanel);
        }

        private void displayLogs(List<Log> logs)
        {
            logs.Reverse();
          
            Panel scrollablePanel = (Panel)this.Controls["scrollablePanel"];

            
            int yPosition = 90;  

            foreach (var log in logs)
            {
               
                TextBox logTextBox = new TextBox();
                logTextBox.Multiline = true;  
                logTextBox.ReadOnly = true;   
                logTextBox.ScrollBars = ScrollBars.None;  
                logTextBox.Text = $"User: {log.User} \nAction: {log.Action} \nTime: {log.TimeStamp}";  

               
                logTextBox.Location = new System.Drawing.Point(10, yPosition);
                logTextBox.Size = new System.Drawing.Size(480, 80); 

               
                scrollablePanel.Controls.Add(logTextBox);

               
                yPosition += 90;  
            }
        }

        private void Logs_Load(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string username=username_txt.Text;

            string apiurl = $"http://localhost:5000/api/Audit/api/getlogsofuser?username={username}";

            var data = new { username };
            var jsonContent = JsonConvert.SerializeObject(data);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            HttpResponseMessage res = await HttpClientManager.Client.GetAsync(apiurl);

            if (res.IsSuccessStatusCode)
            {
                string jsonResponse = await res.Content.ReadAsStringAsync();

                LogResponse logResponse = JsonConvert.DeserializeObject<LogResponse>(jsonResponse);

                List<Log> logsList = logResponse.Logs.ToList();

                LogsForm logsForm = new LogsForm(logsList);
                this.Hide();
                logsForm.Show();
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
