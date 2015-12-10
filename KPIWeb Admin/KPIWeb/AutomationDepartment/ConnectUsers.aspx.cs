using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class ConnectUsers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 1, 2, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }  
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWeb = new KPIWebDataContext();
            List<UsersTable> UsersFind = (from a in kpiWeb.UsersTable
                                          where a.Email == TextBox1.Text
                                          select a).ToList();
            
            if (UsersFind.Count==0)
            {
                Label1.Text = "Пользователь не найден";
            }
            else if ( UsersFind.Count>1 )
            {
                Label1.Text = "Пользователей несколько";         
            }
            else
            {
                LBLID0.Text = UsersFind[0].UsersTableID.ToString();
                Label1.Text = UsersFind[0].Email + "  " + UsersFind[0].Position;
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWeb = new KPIWebDataContext();
            List<UsersTable> UsersFind = (from a in kpiWeb.UsersTable
                                          where a.Email == TextBox2.Text
                                          select a).ToList();

            if (UsersFind.Count == 0)
            {
                Label2.Text = "Пользователь не найден";
            }
            else if (UsersFind.Count > 1)
            {
                Label2.Text = "Пользователей несколько";
            }
            else
            {
                LBLID1.Text = UsersFind[0].UsersTableID.ToString();
                Label2.Text = UsersFind[0].Email + "  " + UsersFind[0].Position;
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWeb = new KPIWebDataContext();
            UsersTable User0 = (from a in kpiWeb.UsersTable
                                where a.UsersTableID == Convert.ToInt32(LBLID0.Text)
                                select a).FirstOrDefault();
            UsersTable User1 = (from a in kpiWeb.UsersTable
                                where a.UsersTableID == Convert.ToInt32(LBLID1.Text)
                                select a).FirstOrDefault();

            User1.Email = User0.Email;
            User1.PassCode = User0.PassCode;
            User1.Login = User0.Login;
            User1.Password = User0.Password;
            User1.Confirmed = User0.Confirmed;

            MultiUser Multiuser1 = new MultiUser();
            MultiUser Multiuser0 = new MultiUser();

            Multiuser1.FK_UserCanAccess = User0.UsersTableID;
            Multiuser1.FK_UserToAccess = User1.UsersTableID;
            Multiuser1.Active = true;

            Multiuser0.FK_UserCanAccess = User1.UsersTableID;
            Multiuser0.FK_UserToAccess = User0.UsersTableID;
            Multiuser0.Active = true;

            kpiWeb.MultiUser.InsertOnSubmit(Multiuser1);
            kpiWeb.MultiUser.InsertOnSubmit(Multiuser0);

            kpiWeb.SubmitChanges();
        }
    }
}