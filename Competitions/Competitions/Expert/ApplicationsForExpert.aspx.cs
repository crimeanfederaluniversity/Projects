using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.IO.Compression;

namespace Competitions.Expert
{
    public partial class ApplicationsForExpert : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            var userIdtmp = Session["UserID"];
            if (userIdtmp == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = (int)userIdtmp;
            if (!Page.IsPostBack)
            {
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("CompetitionName", typeof(string)));
                                 
                List<zApplicationTable> applicationList = (from a in competitionDataBase.zApplicationTable
                                                           where a.Active == true && a.Sended == true
                                                           join b in competitionDataBase.zCompetitionsTable
                                                           on a.FK_CompetitionTable equals b.ID
                                                           where b.Active == true
                                                           join c in competitionDataBase.zExpertsAndApplicationMappingTable
                                                           on a.ID equals c.FK_ApplicationsTable
                                                           where c.Active == true && c.FK_UsersTable == userId                                                                                                                                                                
                                                           select a).Distinct().ToList();
              
                List<zApplicationTable> notreadyapp = new List<zApplicationTable>();
                foreach (zApplicationTable current in applicationList)
                {
                    List<zExpertPointsValue> notsended = (from a in competitionDataBase.zExpertPointsValue
                                                          where a.Active == true && a.Sended == true
                                                          && a.FK_ApplicationTable == current.ID && a.FK_ExpertsTable == userId                                                          
                                                          select a).ToList();
                    if (notsended.Count == 0)
                    {
                        notreadyapp.Add(current);
                    }
                    else
                    {
                        continue;  
                    }

                }


                foreach (zApplicationTable currentApplication in notreadyapp)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["CompetitionName"] = (from a in competitionDataBase.zCompetitionsTable
                                                  where a.ID == (Convert.ToInt32(currentApplication.FK_CompetitionTable))
                                                  select a.Name).FirstOrDefault();
                    
                    dataTable.Rows.Add(dataRow);
                     
                }

                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
        }
        protected void GetDocButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                var appid = button.CommandArgument;
                int idapp = Convert.ToInt32(appid);
                string dirPath = Server.MapPath("~/documents/byApplication/" + idapp);
                string zipFile = Server.MapPath("~/documents/generatedZipFiles/") + idapp + ".zip";
                System.IO.Compression.ZipFile.CreateFromDirectory(dirPath, zipFile);
                HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=file.zip");
                HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(zipFile));
                HttpContext.Current.Response.End();
                Response.End();
            }
        }
        protected void EvaluateButtonClick(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            {
                Session["ApplicationID"] = button.CommandArgument;
                Response.Redirect("EvaluateApplication.aspx");
            }
        }
     
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
        
    }
}