using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;

namespace Registration.Account
{
    public partial class FormUserPublication : System.Web.UI.Page
    {
        UsersDBDataContext rating = new UsersDBDataContext();
        protected void Page_Load(object sender, EventArgs e)
        {       
            if (!IsPostBack)
            {
                Session["edituser"] = null;
                RefreshGrid();
            }
        }
        private void RefreshGrid()
        {
            DataTable dataTable = new DataTable();             
            dataTable.Columns.Add(new DataColumn("userid", typeof(string)));
            dataTable.Columns.Add(new DataColumn("firstlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("secondlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("thirdlvl", typeof(string)));
            dataTable.Columns.Add(new DataColumn("email", typeof(string)));
            dataTable.Columns.Add(new DataColumn("position", typeof(string)));
            dataTable.Columns.Add(new DataColumn("stavka", typeof(string)));
            dataTable.Columns.Add(new DataColumn("degree", typeof(string)));
            dataTable.Columns.Add(new DataColumn("fio", typeof(string)));
            List<UsersTable> authorList = (from a in rating.UsersTable where a.Active == false select a).ToList();
                  
            foreach (UsersTable value in authorList)
            {
                DataRow dataRow = dataTable.NewRow();
           
                FirstLevelSubdivisionTable first = (from a in rating.FirstLevelSubdivisionTable where a.Active == true && a.FirstLevelSubdivisionTableID == value.FK_FirstLevelSubdivisionTable select a).FirstOrDefault();
                SecondLevelSubdivisionTable second = (from a in rating.SecondLevelSubdivisionTable where a.Active == true && a.SecondLevelSubdivisionTableID == value.FK_SecondLevelSubdivisionTable select a).FirstOrDefault();
                ThirdLevelSubdivisionTable third = (from a in rating.ThirdLevelSubdivisionTable where a.Active == true && a.ThirdLevelSubdivisionTableID == value.FK_ThirdLevelSubdivisionTable select a).FirstOrDefault();
                dataRow["email"] = value.Email;
                dataRow["position"] = value.Position;
                dataRow["stavka"] = value.Stavka;
                dataRow["degree"] = value.Degree;
                dataRow["userid"] = value.UsersTableID;
                if (first != null)
                {
                    dataRow["firstlvl"] = first.Name;
                }
                else
                {
                    dataRow["firstlvl"] = "Нет привязки";
                }
                if (second != null)
                {
                    dataRow["secondlvl"] = second.Name;
                }
                else
                {
                    dataRow["secondlvl"] = "Нет привязки";
                }
                if (third != null)
                {
                    dataRow["thirdlvl"] = third.Name;
                }
                else
                {
                    dataRow["thirdlvl"] = "Нет привязки";
                }
                dataRow["fio"] = value.Surname + " " + value.Name + " " + value.Patronimyc;
             
                dataTable.Rows.Add(dataRow);
            }
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            RefreshGrid();
        }
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
        protected void GoButtonClik(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                string passCode = RandomString(25);
                UsersTable author = (from a in rating.UsersTable where a.Active == false && a.UsersTableID == Convert.ToInt32(button.CommandArgument) select a).FirstOrDefault();
                author.Active = true;
                author.PassCode = passCode;
                rating.SubmitChanges();
            
                EmailTemplate EmailParams = (from a in rating.EmailTemplate
                                             where a.Name == "InviteToRegister"
                                             && a.Active == true
                                             select a).FirstOrDefault();
                Action.MassMailing(author.Email, EmailParams.EmailTitle,
                    EmailParams.EmailContent.Replace("#LINK#", ConfigurationManager.AppSettings.Get("SiteName") + "/Account/UserRegister?&id=" + passCode), null); 
                //       LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RN0: Admin(mon) " + (string)ViewState["Login"] + " has registered a new user (With emailSend): " + EmailText.Text + "from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());

                
            }
        }
        protected void EditButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Session["edituser"] = button.CommandArgument;
                Response.Redirect("~/Account/PersonalInfo.aspx");
            }

        }
        protected void FailButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {               
                UsersTable author = (from a in rating.UsersTable where a.Active == false && a.UsersTableID == Convert.ToInt32(button.CommandArgument) select a).FirstOrDefault();           
                Action.MassMailing(author.Email, "Регистрация в системе КФУ-Рейтинги", "", null);            
            }
        }
    }
}
