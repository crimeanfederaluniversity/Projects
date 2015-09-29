using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class SpecParamCheckBoxes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            RangeValidatorFunctions rangeValidatorFunctions = new RangeValidatorFunctions();
            ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
            CheckBoxesToShow checkBoxesToShow = new CheckBoxesToShow();
            Serialization userSer = (Serialization) Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = userSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userId);
            if (userTable.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            ViewState["login"] = userTable.Email;
            Serialization mySession = (Serialization) Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int reportId = Convert.ToInt32(mySession.ReportArchiveID);
            int firstLevelId = Convert.ToInt32((mySession.l1));
            int secondLevelId = Convert.ToInt32(mySession.l2);
            int thirdLevelId = Convert.ToInt32(mySession.l3);

            SecondLevelSubdivisionTable secondLevel = mainFunctions.GetSecondLevelById(secondLevelId);
            FirstLevelSubdivisionTable firstLevel = mainFunctions.GetFirstLevelById(firstLevelId);
            ThirdLevelSubdivisionTable thirdLevel = new ThirdLevelSubdivisionTable();
            ReportArchiveTable report = mainFunctions.GetReportById(reportId);

            if (!Page.IsPostBack)
            {
                List<SpecializationTable> specializationTableData = (from a in kpiWebDataContext.SpecializationTable
                    join b in kpiWebDataContext.FourthLevelSubdivisionTable
                        on a.SpecializationTableID equals b.FK_Specialization
                    where b.FK_ThirdLevelSubdivisionTable == thirdLevelId
                          && b.Active == true
                    select a).ToList();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("SpecializationName", typeof (string)));

                dataTable.Columns.Add(new DataColumn("FourthlvlId", typeof (int)));
                dataTable.Columns.Add(new DataColumn("SpecNumber", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Param1Checked", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("Param1Visible", typeof (bool)));

                dataTable.Columns.Add(new DataColumn("Param2Checked", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("Param2Visible", typeof (bool)));

                dataTable.Columns.Add(new DataColumn("Param3Checked", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("Param3Visible", typeof (bool)));

                dataTable.Columns.Add(new DataColumn("Param4Checked", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("Param4Visible", typeof (bool)));

                dataTable.Columns.Add(new DataColumn("Param5Checked", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("Param5Visible", typeof (bool)));

                foreach (SpecializationTable spec in specializationTableData)
                {
                    FourthLevelSubdivisionTable currentFourth = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                        where
                            a.FK_ThirdLevelSubdivisionTable == thirdLevelId &&
                            a.FK_Specialization == spec.SpecializationTableID
                        select a).FirstOrDefault();

                    FourthLevelParametrs fourthLevelParametrs = fourthLevelParametrs =
                        (from a in kpiWebDataContext.FourthLevelParametrs
                            where a.FourthLevelParametrsID == currentFourth.FourthLevelSubdivisionTableID
                            select a).FirstOrDefault();

                    DataRow dataRow = dataTable.NewRow();
                    dataRow["SpecializationName"] = spec.Name;
                    dataRow["FourthlvlId"] = currentFourth.FourthLevelSubdivisionTableID;
                    dataRow["SpecNumber"] = spec.SpecializationNumber;
                    if (fourthLevelParametrs == null)
                    {
                        Response.Redirect("ChooseStruct.aspx");
                        dataRow["Param1Checked"] = false;
                        dataRow["Param2Checked"] = false;
                        dataRow["Param3Checked"] = false;
                        dataRow["Param4Checked"] = false;
                        dataRow["Param5Checked"] = false;
                    }
                    else
                    {
                        dataRow["Param1Checked"] = fourthLevelParametrs.IsModernEducationTechnologies;
                        dataRow["Param2Checked"] = fourthLevelParametrs.IsNetworkComunication;
                        dataRow["Param3Checked"] = fourthLevelParametrs.IsInvalidStudentsFacilities;
                        dataRow["Param4Checked"] = fourthLevelParametrs.IsForeignStudentsAccept;
                        dataRow["Param5Checked"] = false;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                //ViewState["GridviewSpec"] = dataTable;
                GridView1.DataSource = dataTable;
                GridView1.Columns[3].Visible = checkBoxesToShow.CanUserEditCheckBoxModern(userId);
                GridView1.Columns[4].Visible = checkBoxesToShow.CanUserEditCheckBoxNetwork(userId);
                GridView1.Columns[5].Visible = checkBoxesToShow.CanUserEditCheckBoxInvalid(userId);
                GridView1.Columns[6].Visible = checkBoxesToShow.CanUserEditCheckBoxForeignStudents(userId);
                GridView1.Columns[7].Visible = false;
                GridView1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            CheckBoxesToShow checkBoxesToShow = new CheckBoxesToShow();
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = userSer.Id;
            int rowIndex = 0;
            if (GridView1.Rows.Count > 0)
            {
                for (int i = 1; i <= GridView1.Rows.Count; i++)
                {
                    CheckBox checkBoxparamIsModern = (CheckBox) GridView1.Rows[rowIndex].FindControl("IsModern");
                    CheckBox checkBoxparamIsNetwork = (CheckBox) GridView1.Rows[rowIndex].FindControl("IsNetwork");
                    CheckBox checkBoxparamIsInvalid = (CheckBox) GridView1.Rows[rowIndex].FindControl("IsInvalid");
                    CheckBox checkBoxparamIsForeign = (CheckBox) GridView1.Rows[rowIndex].FindControl("IsForeign");
                    Label labelId = (Label) GridView1.Rows[rowIndex].FindControl("FourthlvlId");
                    rowIndex++;
                    var ss = labelId.Text;
                    FourthLevelParametrs fourthLevelParametrsTables = (from a in kpiWebDataContext.FourthLevelParametrs
                        where a.FourthLevelParametrsID == Convert.ToInt32(labelId.Text)
                        select a).FirstOrDefault();
                    if (fourthLevelParametrsTables != null)
                    {
                        if (checkBoxesToShow.CanUserEditCheckBoxModern(userId))
                            fourthLevelParametrsTables.IsModernEducationTechnologies = checkBoxparamIsModern.Checked;
                        if (checkBoxesToShow.CanUserEditCheckBoxNetwork(userId))
                            fourthLevelParametrsTables.IsNetworkComunication = checkBoxparamIsNetwork.Checked;
                        if (checkBoxesToShow.CanUserEditCheckBoxInvalid(userId))
                            fourthLevelParametrsTables.IsInvalidStudentsFacilities = checkBoxparamIsInvalid.Checked;
                        if (checkBoxesToShow.CanUserEditCheckBoxForeignStudents(userId))
                            fourthLevelParametrsTables.IsForeignStudentsAccept = checkBoxparamIsForeign.Checked;
                    }
                    kpiWebDataContext.SubmitChanges();
                }
            }
            Response.Redirect("~/ProrectorReportFilling/FillingPage.aspx");
        }
    }
}