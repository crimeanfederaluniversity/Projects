using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Director
{
    public partial class DCheckedInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 4)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Faculty", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Kafedra", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Checked3", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("Checked2", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("Checked0", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("Checked1", typeof(bool)));

                List<FourthLevelSubdivisionTable> fourth = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                                      join b in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                          on a.FK_ThirdLevelSubdivisionTable equals b.ThirdLevelSubdivisionTableID
                                                      join c in kpiWebDataContext.SecondLevelSubdivisionTable
                                                          on b.FK_SecondLevelSubdivisionTable equals c.SecondLevelSubdivisionTableID
                                                      where c.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                      && a.Active == true
                                                      && b.Active == true
                                                      && c.Active == true
                                                      select a).ToList();

                foreach (FourthLevelSubdivisionTable CurrentFourth in  fourth)
                {
                    SpecializationTable spec = (from a in kpiWebDataContext.SpecializationTable where a.SpecializationTableID == CurrentFourth.FK_Specialization select a).FirstOrDefault();
                    FourthLevelParametrs fourthParam = (from a in kpiWebDataContext.FourthLevelParametrs where a.FourthLevelParametrsID == CurrentFourth.FourthLevelSubdivisionTableID select a).FirstOrDefault();
                    ThirdLevelSubdivisionTable third = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable where a.ThirdLevelSubdivisionTableID == CurrentFourth.FK_ThirdLevelSubdivisionTable select a).FirstOrDefault();
                    SecondLevelSubdivisionTable second = (from a in kpiWebDataContext.SecondLevelSubdivisionTable where a.SecondLevelSubdivisionTableID == third.FK_SecondLevelSubdivisionTable select a).FirstOrDefault();
                    
                     DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = CurrentFourth.FourthLevelSubdivisionTableID;
                    dataRow["Number"] = spec.SpecializationNumber;
                    dataRow["Name"] = spec.Name;
                    dataRow["Faculty"] = second.Name;
                    dataRow["Kafedra"] = third.Name;
                    dataRow["Checked3"] = (bool) fourthParam.IsForeignStudentsAccept;
                    dataRow["Checked2"] = (bool) fourthParam.IsInvalidStudentsFacilities;
                    dataRow["Checked0"] = (bool) fourthParam.IsModernEducationTechnologies;
                    dataRow["Checked1"] = (bool) fourthParam.IsNetworkComunication;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
               
            }
        }

        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}