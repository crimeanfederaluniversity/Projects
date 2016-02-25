using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb
{
    public partial class CreateNewModule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 19, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            } 

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("Id", typeof (string)));
            dataTable.Columns.Add(new DataColumn("ProjectName", typeof (string)));

            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
            {
                List<Projects> projects;
                {
                    projects = (from a in kpiWebDataContext.Projects where a.Active == true select a).ToList();
                }

                foreach (var user in projects)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Id"] = user.Id;
                    dataRow["ProjectName"] = user.ProjectName;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Projects newlink = new Projects();
            newlink.Active = true;
            newlink.ProjectName = ModuleName.Text;
            newlink.CreateButton = true;
            kPiDataContext.Projects.InsertOnSubmit(newlink);
            kPiDataContext.SubmitChanges();

            UserGroupTable newmodule = new UserGroupTable();
            newmodule.Active = true;
            newmodule.Fk_ProjectsTable = newlink.Id;
            newmodule.URLtoGroupMain = ModuleLink.Text;
            newmodule.UserGroupName = ModuleName.Text;
            kPiDataContext.UserGroupTable.InsertOnSubmit(newmodule);
            kPiDataContext.SubmitChanges();

            
            
            Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                "alert('Модуль добавлен!');", true);
        }

        protected void DeleteButtonClick(object sender, EventArgs e)
        {
                Button button = (Button) sender;
                {
                    using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                    {
                        Projects delete =
                            (from a in kPiDataContext.Projects
                                where a.Id == Convert.ToInt32(button.CommandArgument)
                                select a).FirstOrDefault();

                        delete.Active = false;
                        kPiDataContext.SubmitChanges();
                    }
                }
            }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/PersonalMainPage.aspx");
        }
        }
    }
