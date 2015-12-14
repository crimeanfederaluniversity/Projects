using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Reports
{
    public partial class Parametrs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int ReportID = Convert.ToInt32(paramSerialization.ReportStr);
            int SecondLevel = paramSerialization.l2;
            int ThirdLevel = paramSerialization.l3;

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 7, 9, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            
            if (userRights.CanUserSeeThisPage(userID, 7, 0, 0))
            {
                userTable = (from a in kPiDataContext.UsersTable
                             where a.Active == true
                             && a.FK_ThirdLevelSubdivisionTable == ThirdLevel
                             join b in kPiDataContext.BasicParametrsAndUsersMapping
                                 on a.UsersTableID equals b.FK_UsersTable
                                 where b.Active == true
                                 && b.CanEdit == true
                             select a).FirstOrDefault();
            }
            ViewState["UserTable"] = userTable.FK_ThirdLevelSubdivisionTable;
            ////////////////////////////////////////////////////

            

            if (!Page.IsPostBack)
            {
                int UserID = UserSer.Id;
                List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                    join b in kPiDataContext.FourthLevelSubdivisionTable
                        on a.SpecializationTableID equals b.FK_Specialization
                    where b.FK_ThirdLevelSubdivisionTable == userTable.FK_ThirdLevelSubdivisionTable
                          && b.Active == true
                    select a).ToList();

           

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("SpecializationID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("SpecializationName", typeof (string)));

                dataTable.Columns.Add(new DataColumn("FourthlvlId", typeof (int)));
                dataTable.Columns.Add(new DataColumn("SpecNumber", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Param1Label", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Param1CheckBox", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Param2Label", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Param2CheckBox", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Param3Label", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Param3CheckBox", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Param4Label", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Param4CheckBox", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Param5Label", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Param5CheckBox", typeof (string)));

                foreach (SpecializationTable spec in specializationTableData)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["SpecializationID"] = spec.SpecializationTableID;
                    dataRow["SpecializationName"] = spec.Name;
                    dataRow["FourthlvlId"] = (from a in kPiDataContext.FourthLevelSubdivisionTable
                                              where
                                                  a.FK_ThirdLevelSubdivisionTable == userTable.FK_ThirdLevelSubdivisionTable &&
                                                  a.FK_Specialization == spec.SpecializationTableID
                                              select a.FourthLevelSubdivisionTableID).FirstOrDefault();

                    dataRow["SpecNumber"] = spec.SpecializationNumber;
                    dataTable.Rows.Add(dataRow);


                }
                ViewState["GridviewSpec"] = dataTable;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Проставляем галочки в GridView  

            for (int i = 0; i <= GridView1.Rows.Count; i++)
            {
                var fourthlvlId = e.Row.FindControl("FourthlvlId") as Label;
                var checkBoxparamIsModern = e.Row.FindControl("IsModern") as CheckBox;
                var checkBoxparamIsNetwork = e.Row.FindControl("IsNetwork") as CheckBox;
                var checkBoxparamIsInvalid = e.Row.FindControl("IsInvalid") as CheckBox;
                var checkBoxparamIsForeign = e.Row.FindControl("IsForeign") as CheckBox;

                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                FourthLevelParametrs fourthLevelParametrs = new FourthLevelParametrs();

                if (fourthlvlId != null)
                    fourthLevelParametrs = (from a in kpiWebDataContext.FourthLevelParametrs where a.FourthLevelParametrsID == Convert.ToInt32(fourthlvlId.Text) select a).FirstOrDefault();

                if (fourthLevelParametrs != null)
                {
                    if (checkBoxparamIsModern != null)
                        checkBoxparamIsModern.Checked = fourthLevelParametrs.IsModernEducationTechnologies.Value;
                    if (checkBoxparamIsNetwork != null)
                        checkBoxparamIsNetwork.Checked = fourthLevelParametrs.IsNetworkComunication.Value;
                    if (checkBoxparamIsInvalid != null)
                        checkBoxparamIsInvalid.Checked = fourthLevelParametrs.IsInvalidStudentsFacilities.Value;
                    if (checkBoxparamIsForeign != null)
                        checkBoxparamIsForeign.Checked = fourthLevelParametrs.IsForeignStudentsAccept.Value;
                }

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            if (ViewState["GridviewSpec"] != null)
            {
                DataTable dataTable = (DataTable)ViewState["GridviewSpec"];



                if (dataTable.Rows.Count > 0)
                {

                    for (int i = 1; i <= dataTable.Rows.Count; i++)
                    {

                        CheckBox checkBoxparamIsModern = (CheckBox)GridView1.Rows[rowIndex].FindControl("IsModern");
                        CheckBox checkBoxparamIsNetwork = (CheckBox)GridView1.Rows[rowIndex].FindControl("IsNetwork");
                        CheckBox checkBoxparamIsInvalid = (CheckBox)GridView1.Rows[rowIndex].FindControl("IsInvalid");
                        CheckBox checkBoxparamIsForeign = (CheckBox)GridView1.Rows[rowIndex].FindControl("IsForeign");
                        Label labelId = (Label)GridView1.Rows[rowIndex].FindControl("FourthlvlId");
                        rowIndex++;
                        var ss = labelId.Text;
                        using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                        {
                            FourthLevelParametrs fourthLevelParametrsTables = (from a in kpiWebDataContext.FourthLevelParametrs where a.FourthLevelParametrsID == Convert.ToInt32(labelId.Text) select a).FirstOrDefault();
                            ThirdLevelParametrs thirdLevelParametrs = (from a in kpiWebDataContext.ThirdLevelParametrs where a.ThirdLevelParametrsID == (int)ViewState["UserTable"] select a).FirstOrDefault();

                            if (fourthLevelParametrsTables != null)
                            {
                                fourthLevelParametrsTables.IsModernEducationTechnologies = checkBoxparamIsModern.Checked;
                                fourthLevelParametrsTables.IsNetworkComunication = checkBoxparamIsNetwork.Checked;
                                fourthLevelParametrsTables.IsInvalidStudentsFacilities = checkBoxparamIsInvalid.Checked;
                                fourthLevelParametrsTables.IsForeignStudentsAccept = checkBoxparamIsForeign.Checked;
                            }

                            kpiWebDataContext.SubmitChanges();
                        }
                    }
                }
            }

            Response.Redirect("~/Reports_/FillingTheReport.aspx");


        }
    }
}