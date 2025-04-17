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
using static System.Net.WebRequestMethods;

namespace AccessManagementFrontend
{
    public partial class UserDashboard : Form
    {
        //private static readonly HttpClient client = new HttpClient();
        public UserDashboard()
        {
            InitializeComponent();

        }



        private void UserDashboard_Load(object sender, EventArgs e)
        {
            if (UserInfo.role == "user")
            {
                CreateUserbtn.Visible = false;
            }

            if(UserInfo.role == "user")
            {
                ViewLogsbtn.Visible = false;
            }

            if (UserInfo.role == "user")
            {
              ChangeUserPasswordBtn.Visible = false;
            }

            if (UserInfo.role == "user")
            {
                button2.Visible = false;
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            ChangePassword cn=new ChangePassword();
            cn.Show();

        }

        private void CreateUserbtn_Click(object sender, EventArgs e)
        {
            CreateUser cc=new CreateUser();
            this.Hide();
            cc.Show();

        }

        


        private async void ViewLogsbtn_Click(object sender, EventArgs e)
        {
            string apiurl = "http://localhost:5000/api/Audit/api/getAllLogs";

            HttpResponseMessage response = await HttpClientManager.Client.GetAsync(apiurl);

            if (response.IsSuccessStatusCode)
            {

                string jsonResponse = await response.Content.ReadAsStringAsync();

                LogResponse logResponse = JsonConvert.DeserializeObject<LogResponse>(jsonResponse);

                List<Log> logsList = logResponse.Logs.ToList();

                LogsForm logsForm = new LogsForm(logsList);
                this.Hide();
                logsForm.Show();


            }
        }

        private void profilebtn_Click(object sender, EventArgs e)
        {
            ProfilePage pp=new ProfilePage();
            this.Hide();
            pp.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ChnageUserPassword dd=new ChnageUserPassword();
            
            this.Hide();
            dd.Show();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            DeleteUserForm dd=new DeleteUserForm();
            this.Hide();
            dd.Show();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            string url = "http://localhost:5000/api/User/api/logout";
           HttpResponseMessage res =await HttpClientManager.Client.GetAsync(url);

            if (res.IsSuccessStatusCode)
            {
                UserInfo.name = "";
                UserInfo.username = "";
                UserInfo.role = "";
               
                LoginPage dd = new LoginPage();
                dd.Show();
                this.Hide();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            security_que sq = new security_que();
            this.Hide();
            sq.ShowDialog();
            this.Show();
        }
    }
}
