using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;


namespace Competitions.User
{
    public partial class ChooseApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var userIdtmp = Session["UserID"];
            if (userIdtmp == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = (int) userIdtmp;
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
             
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("FillNSendEnabled", typeof(bool)));

               List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTables
                    where a.FK_UsersTable == userId
                          && a.Active == true
                          join b in competitionDataBase.zCompetitionsTable
                          on a.FK_CompetitionTable equals b.ID
                          where b.Active == true
                          && b.OpenForApplications == true
                    select a).Distinct().ToList();

               foreach (zApplicationTable currentApplication in applicationList)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                        where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                        select a.Name).FirstOrDefault();
                    dataTable.Rows.Add(dataRow);

                    dataRow["FillNSendEnabled"] = !currentApplication.Sended;

                }

                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
        }
        protected void NewApplication_Click(object sender, EventArgs e)
        {
            Session["ApplicationID"] = 0;
            Response.Redirect("ApplicationCreateEdit.aspx");
        }
        protected void FillButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                Session["ApplicationID"] = iD;
                Response.Redirect("ChooseApplicationAction.aspx");
            }
        }
        protected void SendButtonClick(object sender, EventArgs e)
        {
             Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTables
                    where a.Active == true
                          && a.ID == iD
                    select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    currentApplication.Sended = true;
                    competitionDataBase.SubmitChanges();
                }
            }
            Response.Redirect("ChooseApplication.aspx");
        }
        protected void GetDocButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                int iD = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                string xmlFile = File.ReadAllText("1.xml");

              //  Object T = ObjectFromXML<object>(xmlFile);
             xmlFile =    xmlFile.Replace("#111#", "Моя строка");

                File.WriteAllText("c:\\1\\2.xml", xmlFile);

                XmlDocument document = new XmlDocument();
                document.Load("1.xml");

                //XmlNameTable 

                document.Save(@"c:\\1\\3.xml");
            }
        }
    }
}