using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace KPIWeb.Director
{
    public partial class DReportView : System.Web.UI.Page
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
            int ReportID = Convert.ToInt32(paramSerialization.ReportStr);
            ReportArchiveTable Report = (from a in kpiWebDataContext.ReportArchiveTable
                                         where a.ReportArchiveTableID == ReportID
                                         select a).FirstOrDefault();
            Label1.Text = Report.Name;
            if (!Page.IsPostBack)
            {
                //мы берем факультеты

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("StructName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StructID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Color", typeof(string)));

                List<SecondLevelSubdivisionTable> Faculties = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                         where a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                         && a.Active == true
                                                         join b in kpiWebDataContext.UsersTable 
                                                         on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                                         where b.Active == true
                                                         join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                         on b.UsersTableID equals c.FK_UsersTable
                                                         where c.Active == true
                                                         && c.CanView == true
                                                         select a).Distinct().ToList();
                bool canconfirm = true;

                foreach (SecondLevelSubdivisionTable CurrentSecond in Faculties)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = CurrentSecond.Name;
                    dataRow["StructID"] = CurrentSecond.SecondLevelSubdivisionTableID;
                    int All = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                               where a.Active == true
                               && a.FK_SecondLevelSubdivisionTable == CurrentSecond.SecondLevelSubdivisionTableID
                               select a).Count();
                    int AllConfirmed = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                        where a.Active == true
                                        && a.FK_SecondLevelSubdivisionTable == CurrentSecond.SecondLevelSubdivisionTableID
                                        join b in kpiWebDataContext.CollectedBasicParametersTable
                                            on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                            where b.Status == 4
                                            && b.Active == true
                                            && b.FK_ReportArchiveTable == ReportID
                                        select a).Distinct().Count();
                    int AllConfirmed2 = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                        where a.Active == true
                                        && a.FK_SecondLevelSubdivisionTable == CurrentSecond.SecondLevelSubdivisionTableID
                                        join b in kpiWebDataContext.CollectedBasicParametersTable
                                            on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                            where b.Status == 5
                                            && b.Active == true
                                            && b.FK_ReportArchiveTable == ReportID
                                        select a).Distinct().Count();


                    if (All != AllConfirmed)
                        canconfirm = false;

                    if (AllConfirmed2 > 0)
                    {
                        dataRow["Status"] = "Утверждено";
                        dataRow["Color"] = 3; // зеленый
                    }
                    else
                    {
                        if (AllConfirmed == All)
                        {
                            dataRow["Color"] = 2; // желтый
                        }
                        else
                        {
                            dataRow["Color"] = 1; // красный
                        }
                        dataRow["Status"] = "Утвердили " + AllConfirmed.ToString() + " из " + All.ToString();
                    }
                    dataTable.Rows.Add(dataRow);
                }
                Button1.Enabled = canconfirm;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }


        protected void ButtonDetailClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
                paramSerialization.l2 = Convert.ToInt32(button.CommandArgument.ToString());  
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии второй уровень
                Response.Redirect("~/Director/DViewThird.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
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
            int ReportID = Convert.ToInt32(paramSerialization.ReportStr);
            List<CollectedBasicParametersTable> ColToConf = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                       where a.FK_ReportArchiveTable == ReportID
                                                       && a.Active == true
                                                       && a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                       select a).Distinct().ToList();
            foreach (CollectedBasicParametersTable curcol in ColToConf)
                curcol.Status = 5;
            kpiWebDataContext.SubmitChanges();

            Response.Redirect("~/Director/DMain.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
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
        }

        protected void Button23_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Director/DAllInOne.aspx");
        }
    }
}