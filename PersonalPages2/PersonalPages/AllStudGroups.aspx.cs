using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PersonalPages
{
    public partial class AllStudGroups : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
               
                PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                List<GroupsTable> groupList = (from a in PersonalPagesDB.GroupsTable
                                                where a.Active == true
                                                select a).ToList();
                if (groupList != null)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add("ID", typeof(string));
                    dataTable.Columns.Add("Name", typeof(string));

                    foreach (GroupsTable current in groupList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = current.ID;
                        dataRow["Name"] = current.Name;
                        dataTable.Rows.Add(dataRow);

                    }
                    StudGroupGV.DataSource = dataTable;
                    StudGroupGV.DataBind();
                }

            }
        }
        protected void GroupButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                Session["GroupID"] = button.CommandArgument;
                Response.Redirect("~/CreateStudGroup.aspx");
            }
        }

        protected void GroupDeleteButtonClick(object sender, EventArgs e)
        {
             Button button = (Button)sender;
            if (button != null)
            {
                PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                GroupsTable groupdelete = (from a in PersonalPagesDB.GroupsTable
                                               where a.Active == true && a.ID == Convert.ToInt32(button.CommandArgument)
                                               select a).FirstOrDefault();
                if (groupdelete != null)
                {
                    groupdelete.Active = false;
                    PersonalPagesDB.SubmitChanges();
                }             
            }
            Response.Redirect("~/AllStudGroups.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
             Serialization UserSer = (Serialization)Session["UserID"];
                if (UserSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                int userID = UserSer.Id;  
            PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
            GroupsTable newgrouplist = new GroupsTable();
            newgrouplist.Active = true;
            newgrouplist.Name = TextBox1.Text;
            newgrouplist.FK_SecondLevel =
                (from a in PersonalPagesDB.UsersTable
                    where a.UsersTableID == userID
                    select a.FK_SecondLevelSubdivisionTable).FirstOrDefault();
            PersonalPagesDB.GroupsTable.InsertOnSubmit(newgrouplist);
            PersonalPagesDB.SubmitChanges();
            Response.Redirect("~/AllStudGroups.aspx");

        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserMainPage.aspx");
        }
    }
}