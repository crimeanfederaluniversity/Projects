using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Account
{
    public partial class UserRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string val = this.Request.QueryString["id"];
            if (val == null)
            {
                Label1.Text = "Страница недоступна обратитесь к администрации";
            }
            else
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                UsersTable user = (from usersTables in kPiDataContext.UsersTable
                                   where
                                   usersTables.PassCode == val
                                   && usersTables.Confirmed == false
                                   && usersTables.Active == true
                                   select usersTables).FirstOrDefault();
                if (user == null)
                {
                    Label1.Text = "Страница недоступна обратитесь к администрации";
                }
                else
                {
                    Label1.Text = user.UsersTableID.ToString();
                    Label1.Visible = false;
                    SaveButton.Enabled = true;
                    PassText.Enabled = true;
                    ConfText.Enabled = true;

                    Label2.Visible = true;
                    Label3.Visible = true;
                    SaveButton.Visible = true;
                    PassText.Visible = true;
                    ConfText.Visible = true;
                }                
            }            
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            string val = this.Request.QueryString["id"];
            if (val == null)
            {
                Label1.Text = "Страница недоступна обратитесь к администрации";
                Label1.Visible = true;
            }
            {
                if (PassText.Text == ConfText.Text)
                {
                    int userID = Convert.ToInt32(Label1.Text);
                    KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                    UsersTable user = (from usersTables in kPiDataContext.UsersTable
                                       where
                                       usersTables.PassCode == val
                                       && usersTables.UsersTableID == userID
                                       && usersTables.Confirmed == false
                                       && usersTables.Active == true
                                       select usersTables).FirstOrDefault();
                    if (user == null)
                    {
                        Label1.Text = "Страница недоступна обратитесь к администрации";
                        Label1.Visible = true;
                    }
                    else
                    {
                        user.Password = PassText.Text;
                        user.Confirmed = true;
                        kPiDataContext.SubmitChanges();
                        //LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Пользователь " + user.Login + " вошел в систему ");
                        Serialization UserSerId = new Serialization(user.UsersTableID);
                        Session["UserID"] = UserSerId;
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "alert('Поздравляем! Вы успешно зарегистрировались.');" +
                            "document.location = '../Default.aspx';", true);
                       // Response.Redirect("~/Default.aspx");  
                    }
                }
            }                       
        }
    }
}