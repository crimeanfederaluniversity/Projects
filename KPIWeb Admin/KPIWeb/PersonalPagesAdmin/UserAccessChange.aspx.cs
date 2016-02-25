using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class UserAccessChange : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            RefreshGrid();
        }
        public void RefreshGrid()
        {            
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("UserAndGroupID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Email", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Date", typeof(string)));              
                dataTable.Columns.Add(new DataColumn("ConfirmButton", typeof(string)));
               

                using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                {
                    List <UsersAndUserGroupMappingTable> accessapp = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                                                                      where a.Confirmed == false select a).ToList();
  
                    foreach (var access in accessapp)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["UserAndGroupID"] = access.UserAndGroupID;
                        dataRow["Email"] = (from a in kpiWebDataContext.UsersTable where a.UsersTableID == access.FK_UserTable  select a.Email).FirstOrDefault().ToString();
           
                       // dataRow["Date"] = access.Date_Change;
                        dataTable.Rows.Add(dataRow);
                    }
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }                                 
        }
        protected void ConfirmButtonClick(object sender, EventArgs e)
        {
            
                Button button = (Button)sender;
                {
                    KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                    UsersAndUserGroupMappingTable access = (from a in kpiWebDataContext.UsersAndUserGroupMappingTable
                                                                   where a.UserAndGroupID == Convert.ToInt32(button.CommandArgument) && a.Confirmed == false
                                                                   select a).FirstOrDefault();
                Session["userIdforChange"] = access.UserAndGroupID;                 
                Response.Redirect("~/PersonalPagesAdmin/ChangeUserAccessLevel.aspx");

            }
                RefreshGrid();
           
        }

   

        
    }
}