using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class ApplicationExpertsAndExpertsGroupEdit : System.Web.UI.Page
    {

        protected void AddExpertButtonClick(object sender, EventArgs e)
        {       
            var sessionParam1 = Session["ApplicationID"];
                   Button button = (Button) sender;
         
                if (sessionParam1 == null && button != null)
                {
                    //error
                    Response.Redirect("~/Default.aspx");
                }
                int applicationId = Convert.ToInt32(sessionParam1);
                int userId = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                                                        where a.Active == true && a.Accept == false
                                                              && a.ID == applicationId && a.Sended == true
                                                        select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    zExpertsAndApplicationMappingTable expertlink =
                        (from a in competitionDataBase.zExpertsAndApplicationMappingTable
                            where a.Active == true && a.FK_ApplicationsTable == applicationId
                                  && a.FK_UsersTable == userId
                            select a).FirstOrDefault();
                    if (expertlink == null)
                    {
                            zExpertsAndApplicationMappingTable newexpertlink = new zExpertsAndApplicationMappingTable();
                            newexpertlink.Active = true;
                            newexpertlink.FK_ApplicationsTable = currentApplication.ID;
                            newexpertlink.FK_UsersTable = userId;
                            competitionDataBase.zExpertsAndApplicationMappingTable.InsertOnSubmit(newexpertlink);
                            competitionDataBase.SubmitChanges();
                        }
                    }
                Response.Redirect("~/ApplicationExpertsAndExpertsGroupEdit.aspx");
                }

            
        protected void DeleteExpertButtonClick(object sender, EventArgs e)
        {
            var sessionParam1 = Session["ApplicationID"];
                   Button button = (Button) sender;
         
                if (sessionParam1 == null && button != null)
                {
                    //error
                    Response.Redirect("~/Default.aspx");
                }
                int applicationId = Convert.ToInt32(sessionParam1);
                int userId = Convert.ToInt32(button.CommandArgument);
                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                                                        where a.Active == true && a.Accept == false
                                                              && a.ID == applicationId && a.Sended == true
                                                        select a).FirstOrDefault();
                if (currentApplication != null)
                {
                    zExpertsAndApplicationMappingTable expertlink =
                        (from a in competitionDataBase.zExpertsAndApplicationMappingTable
                            where a.Active == true && a.FK_ApplicationsTable == applicationId
                                  && a.FK_UsersTable == userId
                            select a).FirstOrDefault();
                    if (expertlink != null)
                    {
                        expertlink.Active = false;
                        competitionDataBase.SubmitChanges();
                    }
                }
                Response.Redirect("~/ApplicationExpertsAndExpertsGroupEdit.aspx");
            }
        
        protected void AddExpertGroupClick(object sender, EventArgs e)
        {
            var sessionParam1 = Session["ApplicationID"];
            Button button = (Button)sender;

            if (sessionParam1 == null && button != null)
            {
                //error
                Response.Redirect("~/Default.aspx");
            }
            int applicationId = Convert.ToInt32(sessionParam1);
            int groupId = Convert.ToInt32(button.CommandArgument);
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            zApplicationTable currentApplication = (from a in competitionDataBase.zApplicationTable
                                                    where a.Active == true && a.Accept == false
                                                          && a.ID == applicationId && a.Sended == true
                                                    select a).FirstOrDefault();
            if (currentApplication != null)
            {
                List<zExpertAndExpertGroupMappingTable> currentgroup =
                    (from a in competitionDataBase.zExpertAndExpertGroupMappingTable
                        where a.Active == true && a.FK_ExpertGroupTable == groupId
                        select a).ToList();
                if (currentgroup != null)
                {
                    foreach (var n in currentgroup)
                    {
                     zExpertsAndApplicationMappingTable expertlink =
                     (from a in competitionDataBase.zExpertsAndApplicationMappingTable
                     where a.Active == true && a.FK_ApplicationsTable == applicationId
                           && a.FK_UsersTable == n.FK_UsersTable
                     select a).FirstOrDefault();
                        if (expertlink == null)
                        {
                            zExpertsAndApplicationMappingTable newexpertlink = new zExpertsAndApplicationMappingTable();
                            newexpertlink.Active = true;
                            newexpertlink.FK_ApplicationsTable = currentApplication.ID;
                            newexpertlink.FK_UsersTable = n.FK_UsersTable;
                            competitionDataBase.zExpertsAndApplicationMappingTable.InsertOnSubmit(newexpertlink);
                            competitionDataBase.SubmitChanges();
                        }
                    }
                }
                
            }
            Response.Redirect("~/ApplicationExpertsAndExpertsGroupEdit.aspx");
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            CompetitionDataContext competitionsDataBase = new CompetitionDataContext();
            List<zExpertGroup> expertGroupsList = (from a in competitionsDataBase.zExpertGroup
                where a.Active == true
                select a).ToList();

            DataTable dataTable1 = new DataTable();
            dataTable1.Columns.Add("ExpertGroupId", typeof(int));
            dataTable1.Columns.Add("ExpertGroupName", typeof(string));
            dataTable1.Columns.Add("UserId", typeof(int));
            dataTable1.Columns.Add("UserName", typeof(string));


            foreach (zExpertGroup currentExpertGroup in expertGroupsList)
            {
                List<UsersTable> expertsInGroup = (from a in competitionsDataBase.UsersTable
                    where a.Active == true
                    join b in competitionsDataBase.zExpertAndExpertGroupMappingTable
                        on a.ID equals b.FK_UsersTable
                    where b.Active == true
                          && b.FK_ExpertGroupTable == currentExpertGroup.ID
                    select a).Distinct().ToList();
                foreach (UsersTable currentUser in expertsInGroup)
                {
                    DataRow dataRow = dataTable1.NewRow();
                    dataRow["ExpertGroupId"] = currentExpertGroup.ID;
                    dataRow["ExpertGroupName"] = currentExpertGroup.Name;
                    dataRow["UserId"] = currentUser.ID;
                    dataRow["UserName"] = currentUser.Email;
                    dataTable1.Rows.Add(dataRow);   
                }
            }
            ExpertsGV.DataSource = dataTable1;
            ExpertsGV.DataBind();
        }
        public static void MergeRows(GridView gridView)
        {
            for (int rowIndex = gridView.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow row = gridView.Rows[rowIndex];
                GridViewRow previousRow = gridView.Rows[rowIndex + 1];

                for (int i = 0; i < row.Cells.Count; i++)
                {
                    if (i != 1) 
                        continue;

                    if ((row.Cells[i].Text == previousRow.Cells[i].Text))
                    {
                        int j = i + 5;
                        row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                               previousRow.Cells[i].RowSpan + 1;
                        previousRow.Cells[i].Visible = false;

                        row.Cells[j].RowSpan = previousRow.Cells[j].RowSpan < 2 ? 2 :
                                               previousRow.Cells[j].RowSpan + 1;
                        previousRow.Cells[j].Visible = false;
                    }
                }
            }
        }

        protected void ExpertsGV_PreRender(object sender, EventArgs e)
        {
            MergeRows(ExpertsGV);
        }
    }
}