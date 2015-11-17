using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Xml;


namespace Competitions.Admin
{


    public partial class ExpertPointPage : System.Web.UI.Page
    {
         
        private List<UsersTable> GetExpertsInApplicationList(int applicationId)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                                        where a.Active == true
                                         && a.AccessLevel == 5 
                                        join b in CompetitionsDataBase.zExpertsAndApplicationMappingTable
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

                    zExpertAndExpertGroupMappingTable accesstext = (from f in CompetitionsDataBase.zExpertAndExpertGroupMappingTable                                               
                                               where f.Active == true && f.FK_UsersTable == currentExpert.ID
                                               join a in CompetitionsDataBase.zExpertGroup
                                               on f.FK_ExpertGroupTable equals  a.ID
                                               where a.Active == true
                                               select f).FirstOrDefault();
                     if (accesstext == null)
                     { 
                         dataRow["AccessLevel"] =  "Привлеченный эксперт"; 
                     }
                     else
                     {
                         dataRow["AccessLevel"] = (from a in CompetitionsDataBase.zExpertGroup
                             where a.Active == true
                             join b in CompetitionsDataBase.zExpertAndExpertGroupMappingTable
                                 on a.ID equals b.FK_ExpertGroupTable
                             where b.FK_UsersTable == Convert.ToInt32(currentExpert.ID)
                             select a.Name).FirstOrDefault();
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
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zApplicationTable name = (from a in CompetitionsDataBase.zApplicationTable
                    where a.ID == applicationId
                    select a).FirstOrDefault();
                Label1.Text = "Заключения экспертов на заявку: " + name.Name;
                ExpertsPointGV.DataSource = GetFilledDataTable(GetExpertsInApplicationList(applicationId));
                ExpertsPointGV.DataBind();
        
            }
        }


        
        public byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }
        
        protected void ExpertDownloadButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                var appIdTmp = Session["ApplicationID"];
                if (appIdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int applicationId = Convert.ToInt32(appIdTmp);
                int userid = Convert.ToInt32(button.CommandArgument);
   
 
                                string templateFilePath = Server.MapPath("~/documents/expertdoc/expertpoint.xml");
                                string newFileName = DateTime.Now.ToString();
                                newFileName = newFileName.Replace(":", "_");
                                string newFileDirectory = Server.MapPath("~/documents/generated") + "\\" + userid.ToString();
                                string newFilePath = newFileDirectory + "\\" + newFileName;

                                Directory.CreateDirectory(newFileDirectory);

                                CreateXmlFile createXmlFile = new CreateXmlFile();
                                createXmlFile.CreateExpertDocument(templateFilePath, newFilePath, applicationId, userid);
                               

                                HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=expertpoint.xml" );
                                HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(newFilePath));
                                HttpContext.Current.Response.End();
                                Response.End();
                            }
                        }
                    
          
     ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    
     

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReadyApplications.aspx");
        }
         
    }
}