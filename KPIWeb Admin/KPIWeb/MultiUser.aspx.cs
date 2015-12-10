using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;

namespace KPIWeb
{
    public partial class MultiUser1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/UserLogin.aspx");
            }
            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                               where usersTables.UsersTableID == UserSer.Id
                               select usersTables).FirstOrDefault();
            if (user != null)
            {

                List<MultiUser> MultiuserList = (from a in KPIWebDataContext.MultiUser
                                                 where a.Active == true
                                                 && a.FK_UserCanAccess == user.UsersTableID
                                                 select a).ToList();
                if (MultiuserList.Count>0)
                {
                    List<UsersTable> UsersList =new List<UsersTable>();
                    UsersList.Add(user);
                    foreach (MultiUser MultUs in MultiuserList)
                    {
                        UsersList.Add ((from a in KPIWebDataContext.UsersTable where a.UsersTableID == MultUs.FK_UserToAccess && a.Active == true select a).FirstOrDefault());
                    }

                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("UserID", typeof(string)));

                    foreach (UsersTable User in UsersList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        if (User.Position!=null)
                        {
                            dataRow["Name"] = User.Position;
                        }
                        else
                        {
                            dataRow["Name"] = User.Email;
                        }
                        dataRow["UserID"] = User.UsersTableID;
                        dataTable.Rows.Add(dataRow);
                    }
                    DataView dv = dataTable.DefaultView;
                    dv.Sort = "UserID desc";
                    DataTable sortedDT = dv.ToTable();

                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        public void Directions(UsersTable user)
        {
            if (user.Position == null)
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
            }
            else if (user.Position.Length > 2)
            {
                FormsAuthentication.SetAuthCookie(user.Position, true);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
            }
            UserRights userRights = new UserRights();
            if (userRights.CanUserSeeThisPage(user.UsersTableID, 1, 0, 0))
            {
                Response.Redirect("~/AutomationDepartment/Main.aspx");
            }
            if (userRights.CanUserSeeThisPage(user.UsersTableID, 2, 0, 0))
            {
                Response.Redirect("~/StatisticsDepartment/MonitoringMain.aspx");
            }        
                FormsAuthentication.SignOut();
                Session.Abandon();
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }

        protected void GoToMainClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                 KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                UsersTable user = (from usersTables in KPIWebDataContext.UsersTable
                               where usersTables.UsersTableID == Convert.ToUInt32(button.CommandArgument)
                               select usersTables).FirstOrDefault();
                Serialization UserSerId = new Serialization(user.UsersTableID);
                Session["UserID"] = UserSerId;
                Directions(user);
            }

        }
        
    }
}