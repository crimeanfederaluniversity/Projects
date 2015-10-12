using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Net;

namespace Competitions.Expert
{
    public partial class Applications : System.Web.UI.Page
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
             CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
             List<zApplicationTable> applicationList = (from a in CompetitionsDataBase.zApplicationTable
                                                        where a.Active == true && a.Sended == true
                                                        join b in CompetitionsDataBase.zCompetitionsTable
                                                        on a.FK_CompetitionTable equals b.ID
                                                        where b.Active == true
                                                        join c in CompetitionsDataBase.zExpertsAndApplicationMappingTable
                                                        on a.ID equals c.FK_ApplicationsTable
                                                        where c.Active == true && c.FK_UsersTable == userId
                                                        select a).Distinct().ToList();

             List<zApplicationTable> notreadyapp = new List<zApplicationTable>();
             foreach (zApplicationTable current in applicationList)
             {
                 List<zExpertPointsValue> notsended = (from a in CompetitionsDataBase.zExpertPointsValue
                                                       where a.Active == true && a.Sended == false
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
             
                DataTable dataTable = new  DataTable();

                dataTable.Columns.Add("ID", typeof (string));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Description", typeof(string));
                dataTable.Columns.Add("Autor", typeof(string));
                dataTable.Columns.Add("Competition", typeof(string));

                foreach (zApplicationTable currentApplication in notreadyapp)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentApplication.ID;
                    dataRow["Name"] = currentApplication.Name;
                    dataRow["Description"] = "";
                    dataRow["Competition"] = (from a in CompetitionsDataBase.zCompetitionsTable
                                                  where a.ID == currentApplication.FK_CompetitionTable
                                                  select  a.Name).FirstOrDefault();
                    dataRow["Autor"] = (from a in CompetitionsDataBase.UsersTable 
                                            where a.ID == currentApplication.FK_UsersTable
                                            select a.Email).FirstOrDefault();
                    dataTable.Rows.Add(dataRow);
                }
                ApplicationGV.DataSource = dataTable;
                ApplicationGV.DataBind();
            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Main.aspx");
        }
        protected void GetApplicationButtonClick(object sender, EventArgs e)
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
        protected void GetExpertPointButtonClick(object sender, EventArgs e)
        {
        }
    }
}