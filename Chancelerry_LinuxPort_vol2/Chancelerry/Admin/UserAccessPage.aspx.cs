using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.Admin
{
    public partial class UserAccessPage : System.Web.UI.Page
    {
        ChancelerryDb chancDb =
                new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                int userId = (int)userID;
                if (userId != 1)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }

            if (!Page.IsPostBack)
            {
                GridApdate();
            }
        }
        protected void GridApdate()
        {
            int userToChangeId = Convert.ToInt32(Session["userLink"]);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
            dataTable.Columns.Add(new DataColumn("Access", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Edit", typeof(bool)));
            List<Registers> registers = (from a in chancDb.Registers where a.Active == true select a).ToList();
                     
                foreach (var link in registers)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = link.RegisterID;
                    dataRow["Name"] = link.Name;

                RegistersUsersMap check = (from b in chancDb.RegistersUsersMap
                                           where  b.Active == true &&  b.FkUser == userToChangeId &&
                                           b.FkRegister == link.RegisterID
                                           select b).FirstOrDefault();
                                           
                    if (check != null)
                    {
                        dataRow["Access"] = true;
                        if (check.CanEdit == true)
                        {
                            dataRow["Edit"] = true;
                        }
                        else
                        {
                            dataRow["Edit"] = false ;
                        }
                    }
                    else
                    {
                            dataRow["Access"] = false;
                            dataRow["Edit"] = false;
                    }
                    dataTable.Rows.Add(dataRow);
                }            
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            int userToChangeId = Convert.ToInt32(Session["userLink"]);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox confirmed = (CheckBox)GridView1.Rows[i].FindControl("Access");
                CheckBox edit = (CheckBox)GridView1.Rows[i].FindControl("Edit");
                Label label = (Label)GridView1.Rows[i].FindControl("ID");
                int regId = Convert.ToInt32(label.Text);
                if (confirmed.Checked == true)
                {                    
                    RegistersUsersMap checkedgroup = (from a in chancDb.RegistersUsersMap
                                                      where a.FkUser == userToChangeId && a.FkRegister == regId
                                                      select a).FirstOrDefault();
                    if (checkedgroup != null)
                    {
                        checkedgroup.Active = true;
                        chancDb.SubmitChanges();
                        if (edit.Checked == true)
                        {
                            checkedgroup.CanEdit = true;
                            chancDb.SubmitChanges();
                        }
                        else
                        {
                            checkedgroup.CanEdit = false;
                            chancDb.SubmitChanges();
                        }
                    }
                    else
                    {
                        RegistersUsersMap userlink = new RegistersUsersMap();
                        userlink.FkUser = userToChangeId;
                        userlink.FkRegister = Convert.ToInt32(label.Text);
                        userlink.Active = true;
                        if (edit.Checked == true)
                        {  userlink.CanEdit = true;  } 
                        else { userlink.CanEdit = false; }
                        chancDb.RegistersUsersMap.InsertOnSubmit(userlink);
                        chancDb.SubmitChanges();
                    }
                }
                else
                {
                    RegistersUsersMap checkedgroup = (from a in chancDb.RegistersUsersMap
                                                      where a.Active == true && a.FkUser == userToChangeId && a.FkRegister == regId
                                                      select a).FirstOrDefault();
                    if (checkedgroup != null)
                    {
                        checkedgroup.Active = false;
                        chancDb.SubmitChanges();
                    }
                }         
            }
            GridApdate();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/AdminPage.aspx");
        }
    }
}