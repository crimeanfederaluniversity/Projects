using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.IO.Compression;


namespace Competitions.Admin
{


    public partial class ExpertPointPage : System.Web.UI.Page
    {
        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }

        private List<UsersTable> GetExpertsInApplicationList(int applicationId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                where a.Active == true
                      && a.AccessLevel == 5 
                join b in CompetitionsDataBase.zExpertsAndApplicationMappingTables
                    on a.ID equals b.FK_UsersTable
                where b.Active == true
                      && b.FK_ApplicationsTable == applicationId
                select a).ToList();
            return experts;

        }
   
        private DataTable GetFilledDataTable(List<UsersTable> expertsList)
        {
            
            var appid = Session["ApplicationID"];
            int idapp = Convert.ToInt32(appid);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add("ID", typeof(string));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("AccessLevel", typeof(string));
                dataTable.Columns.Add("SendedDataTime", typeof(string));
                dataTable.Columns.Add(new DataColumn("Color", typeof(string)));
                foreach (UsersTable currentExpert in expertsList)
                {
                    CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentExpert.ID;
                    dataRow["Name"] = currentExpert.Email;

     zExpertsAndCompetitionMappngTamplateTable accesstext = (from f in CompetitionsDataBase.zExpertsAndCompetitionMappngTamplateTable                                               
                                               where f.Active == true && f.FK_UsersTable == currentExpert.ID
                                               select f).FirstOrDefault();
                     if (accesstext == null)
                     { 
                         dataRow["AccessLevel"] =  "Привлеченный эксперт"; 
                     }
                     else 
                     {
                         dataRow["AccessLevel"] = "Член экспертного совета";
                     }
                    
                    

                    dataRow["SendedDataTime"] = (from a in CompetitionsDataBase.zExpertPointsValue
                                              where a.Active == true
                                              && a.FK_ExpertsTable == currentExpert.ID
                                              && a.FK_ApplicationTable == idapp
                                                 select a.SendedDataTime).Distinct().FirstOrDefault().ToString().Split(' ')[0];
                   
                    var appID = Session["ApplicationID"];
                    int IDapp = Convert.ToInt32(appID);
                    
                    List<zExpertPointsValue> nwList = (from a in CompetitionsDataBase.zExpertPointsValue
                                                       where a.Active == true
                                                       && a.FK_ExpertsTable == Convert.ToInt32(currentExpert.ID)
                                                       && a.FK_ApplicationTable == IDapp
                                                       select a).ToList();
                   // download.Enabled = true;
                    dataRow["Color"] = 3; // зеленый
                    foreach (zExpertPointsValue currentValue in nwList)
                    {
                        if (currentValue.Sended != true)
                        { dataRow["Color"] = 1; } // красный

                        if (currentValue.Sended != true && currentValue.LastChangeDataTime != null)                      
                        { dataRow["Color"] = 2; }// желтый
    
                        
                    }
                    if (nwList.Count < 1)
                    {
                        dataRow["Color"] = 1; // красный
                       
                    }

                    dataTable.Rows.Add(dataRow);
                }
            return dataTable;

        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
         
            var lblColor = e.Row.FindControl("Color") as Label;
            if (lblColor != null)
            {
                if (lblColor.Text == "1") // красный 
                {
                    e.Row.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
                }
                if (lblColor.Text == "2") // желтый
                {
                    e.Row.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");
                }
                if (lblColor.Text == "3") // зеленый
                {
                    e.Row.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
                }
            }
        
            var appid = Session["ApplicationID"];
            int idapp = Convert.ToInt32(appid);
            Button download = (Button)e.Row.FindControl("ExpertDownloadButton");
            if (download != null)
            {
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<zExpertPointsValue> nwList = (from a in CompetitionsDataBase.zExpertPointsValue
                                                   where a.Active == true
                                                   && a.FK_ExpertsTable == Convert.ToInt32(download.CommandArgument)
                                                   && a.FK_ApplicationTable == idapp
                                                   select a).ToList();
                download.Enabled = true;
               // dataRow["Color"] = 3; // зеленый
                foreach (zExpertPointsValue currentValue in nwList)
                {
                    if (currentValue.Sended != true)
                       // dataRow["Color"] = 1; // красный
                        download.Enabled = false;
                }
                if (nwList.Count < 1)
                {
                   // dataRow["Color"] = 1; // красный
                    download.Enabled = false;
                }

            }
        }    
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {          
                var appIdTmp = Session["ApplicationID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int applicationId = Convert.ToInt32(appIdTmp);

                ExpertsPointGV.DataSource = GetFilledDataTable(GetExpertsInApplicationList(applicationId));
                ExpertsPointGV.DataBind();
        
            }
        }
        
        protected void ExpertDownloadButtonClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Функционал в разработке!');", true);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var appid = Session["ApplicationID"];
            int idapp = Convert.ToInt32(appid);
            string dirPath = Server.MapPath("~/documents/byApplication/" + idapp);
            string zipFile = Server.MapPath("~/documents/generatedZipFiles/") + idapp+".zip";
            System.IO.Compression.ZipFile.CreateFromDirectory(dirPath, zipFile);
            HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
            HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=file.zip");
            HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(zipFile));
            HttpContext.Current.Response.End();
            Response.End();
            
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReadyApplications.aspx");
        }
         
    }
}