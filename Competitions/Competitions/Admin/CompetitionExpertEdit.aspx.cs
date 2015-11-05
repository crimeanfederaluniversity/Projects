using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competitions.Admin
{
    public partial class CompetitionExpertEdit : System.Web.UI.Page
    {
        private List<UsersTable> GetExpertsInGroup(int groupid)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
              
            List<UsersTable> experts = (from a in CompetitionsDataBase.UsersTable
                                        where a.Active == true  && a.AccessLevel == 5
                                        join b in CompetitionsDataBase.zExpertAndExpertGroupMappingTable
                                            on a.ID equals b.FK_UsersTable
                                        where b.Active == true && b.FK_ExpertGroupTable == groupid
                                        select a).ToList();
            return experts;
        }
        private List<UsersTable> GetExpertsOutGroup(int groupid)
        {
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            List<UsersTable> allExperts = (from a in CompetitionsDataBase.UsersTable
                                           where a.Active == true && a.AccessLevel == 5
                                           select a).ToList();
            List<UsersTable> expertsInCompetition = GetExpertsInGroup(groupid);
            foreach (UsersTable currentUser in expertsInCompetition)
            {
                allExperts.Remove(currentUser);
            }
            return allExperts;
        }
        private DataTable GetFilledDataTable(List<UsersTable> expertsList)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID", typeof(string));
            dataTable.Columns.Add("Name", typeof(string));
            foreach (UsersTable currentExpert in expertsList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID"] = currentExpert.ID;
                dataRow["Name"] = currentExpert.Email;
                dataTable.Rows.Add(dataRow);
            }
            return dataTable;

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var IdTmp = Session["GroupID"];
                if (IdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int groupid = Convert.ToInt32(IdTmp);

                connectedExpertsGV.DataSource = GetFilledDataTable(GetExpertsInGroup(groupid));
                unconnectedExpertsGV.DataSource = GetFilledDataTable(GetExpertsOutGroup(groupid));

                connectedExpertsGV.DataBind();
                unconnectedExpertsGV.DataBind();

            }
        }

        protected void ExpertDeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                var IdTmp = Session["GroupID"];
                if (IdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int groupid = Convert.ToInt32(IdTmp);

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zExpertAndExpertGroupMappingTable expertdelete =
                    (from a in CompetitionsDataBase.zExpertAndExpertGroupMappingTable
                     where a.FK_UsersTable == Convert.ToInt32(button.CommandArgument)
                           && a.Active == true
                           && a.FK_ExpertGroupTable == groupid
                     select a).FirstOrDefault();
                if (expertdelete != null)
                {
                    expertdelete.Active = false;
                    CompetitionsDataBase.SubmitChanges();
                }
            }
            Response.Redirect("CompetitionExpertEdit.aspx");

        }

        protected void ExpertAddButtonClick(object sender, EventArgs e)
        {
            
            Button button = (Button)sender;
            if (button != null)
            {
                var IdTmp = Session["GroupID"];
                if (IdTmp == null)
                {
                    Response.Redirect("Main.aspx");
                }
                int groupid = Convert.ToInt32(IdTmp);

                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                zExpertAndExpertGroupMappingTable expertadd =
                     (from a in CompetitionsDataBase.zExpertAndExpertGroupMappingTable
                      where a.FK_UsersTable == Convert.ToInt32(button.CommandArgument)
                            && a.Active == false
                            && a.FK_ExpertGroupTable == groupid
                      select a).FirstOrDefault();

                if (expertadd != null)
                {
                    expertadd.Active = true;
                    CompetitionsDataBase.SubmitChanges();
                }
                else
                {
                    zExpertAndExpertGroupMappingTable newexpertingroup = new zExpertAndExpertGroupMappingTable();
                    newexpertingroup.Active = true;
                    newexpertingroup.FK_ExpertGroupTable = groupid;
                    newexpertingroup.FK_UsersTable = Convert.ToInt32(button.CommandArgument);
                    CompetitionsDataBase.zExpertAndExpertGroupMappingTable.InsertOnSubmit(newexpertingroup);
                    CompetitionsDataBase.SubmitChanges();
                }
            }
            Response.Redirect("CompetitionExpertEdit.aspx");
        
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationSovetexpertEdit.aspx");
        }
    }
}