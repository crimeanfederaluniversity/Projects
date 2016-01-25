using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Director
{
    public partial class ConfirmCheckBoxes : System.Web.UI.Page
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

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int reportId = Convert.ToInt32(paramSerialization.ReportStr);

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
                dataTable.Columns.Add(new DataColumn("color", typeof(int)));
                List<FourthLevelSubdivisionTable> fourth = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                                            join b in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                on a.FK_ThirdLevelSubdivisionTable equals b.ThirdLevelSubdivisionTableID
                                                            join c in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                on b.FK_SecondLevelSubdivisionTable equals c.SecondLevelSubdivisionTableID
                                                                join d in kpiWebDataContext.ReportArchiveAndLevelMappingTable 
                                                                on b.ThirdLevelSubdivisionTableID equals d.FK_ThirdLevelSubdivisionTable
                                                                where d.Active == true
                                                                && d.FK_ReportArchiveTableId == reportId
                                                            where c.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                            && a.Active == true
                                                            && b.Active == true
                                                            && c.Active == true
                                                            select a).Distinct().ToList();
                bool canconfirm = true;

                CollectedBasicParametersTable collectedForCheckbox =
                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                            where 
                                    a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                  && a.FK_ReportArchiveTable == reportId
                                  && a.FK_BasicParametersTable == 3946
                                  && a.Active == true
                                  && a.CollectedValue == 1
                            select a).FirstOrDefault();
                bool isCheckboxesConfirmed = false;
                    if (collectedForCheckbox == null)
                    {
                        isCheckboxesConfirmed = false;
                    }
                    else
                    {
                        isCheckboxesConfirmed = true;
                    }
#region
                foreach (FourthLevelSubdivisionTable CurrentFourth in fourth)
                {
                    SpecializationTable spec = (from a in kpiWebDataContext.SpecializationTable where a.SpecializationTableID == CurrentFourth.FK_Specialization select a).FirstOrDefault();
                    FourthLevelParametrs fourthParam = (from a in kpiWebDataContext.FourthLevelParametrs where a.FourthLevelParametrsID == CurrentFourth.FourthLevelSubdivisionTableID select a).FirstOrDefault();
                    ThirdLevelSubdivisionTable third = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable where a.ThirdLevelSubdivisionTableID == CurrentFourth.FK_ThirdLevelSubdivisionTable select a).FirstOrDefault();
                    SecondLevelSubdivisionTable second = (from a in kpiWebDataContext.SecondLevelSubdivisionTable where a.SecondLevelSubdivisionTableID == third.FK_SecondLevelSubdivisionTable select a).FirstOrDefault();


                    
                    CollectedBasicParametersTable collected = (from a in kpiWebDataContext.CollectedBasicParametersTable
                        where a.Active == true
                              && a.FK_ThirdLevelSubdivisionTable == CurrentFourth.FK_ThirdLevelSubdivisionTable
                        select a).FirstOrDefault();



                    
                   // int rowStatus
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = CurrentFourth.FourthLevelSubdivisionTableID;
                    dataRow["Number"] = spec.SpecializationNumber;
                    dataRow["Name"] = spec.Name;
                    dataRow["Faculty"] = second.Name;
                    dataRow["Kafedra"] = third.Name;
                    dataRow["Checked3"] = (bool)fourthParam.IsForeignStudentsAccept;
                    dataRow["Checked2"] = (bool)fourthParam.IsInvalidStudentsFacilities;
                    dataRow["Checked0"] = (bool)fourthParam.IsModernEducationTechnologies;
                    dataRow["Checked1"] = (bool)fourthParam.IsNetworkComunication;
                    if (collected == null)
                    {
                        dataRow["color"] = 0;
                    }
                    else
                    {
                        if (collected.Status == 0 || collected.Status == 1 || collected.Status == 2 ||
                            collected.Status == 3)
                        {
                            dataRow["color"] = 0;
                            canconfirm = false;
                        }
                        if (collected.Status == 4)
                        dataRow["color"] = 1;
                    }
                    if (isCheckboxesConfirmed)
                    {
                        canconfirm = false;
                        dataRow["color"] = 2;
                    }
                    dataTable.Rows.Add(dataRow);
                }
#endregion

                statusLabel.Text = "Данные в процессе заполнения";
                if (canconfirm)
                    statusLabel.Text = "Данные ожидают утверждения";
                if (isCheckboxesConfirmed)
                    statusLabel.Text = "Данные утверждены";
                    
                    Button23.Enabled = canconfirm;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();

            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Director/DReportView.aspx");
        }

        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var lblColor = e.Row.FindControl("color") as Label;
            if (lblColor != null)
            {
                if (lblColor.Text == "0" ) // красный 
                {
                    e.Row.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
                }
                if (lblColor.Text == "1") // желтый
                {
                    e.Row.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");
                }
                if (lblColor.Text == "2") // зеленый
                {
                    e.Row.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
                }
            }
        }

        protected void Button23_Click(object sender, EventArgs e)
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

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int reportId = Convert.ToInt32(paramSerialization.ReportStr);


            CollectedBasicParametersTable newConfirmCollected = new CollectedBasicParametersTable();
            newConfirmCollected.Active = true;
            newConfirmCollected.FK_BasicParametersTable = 3946;
            newConfirmCollected.CollectedValue = 1;
            newConfirmCollected.FK_ReportArchiveTable = reportId;
            newConfirmCollected.FK_FirstLevelSubdivisionTable = userTable.FK_FirstLevelSubdivisionTable;
            newConfirmCollected.SavedDateTime = DateTime.Now;
            newConfirmCollected.LastChangeDateTime = DateTime.Now;
            newConfirmCollected.FK_UsersTable = userTable.UsersTableID;
            kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(newConfirmCollected);
            kpiWebDataContext.SubmitChanges();
            Response.Redirect("~/Director/ConfirmCheckBoxes.aspx");

        }
    }
}