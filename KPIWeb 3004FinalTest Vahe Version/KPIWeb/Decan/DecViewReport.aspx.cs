﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Decan
{
    public partial class DecViewReport : System.Web.UI.Page
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

            if (userTable.AccessLevel != 2)
            {
                Response.Redirect("~/Default.aspx");
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int ReportID = Convert.ToInt32(paramSerialization.ReportStr);
      
            int SecondLevel = (int) userTable.FK_SecondLevelSubdivisionTable;

            SecondLevelSubdivisionTable Second = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                  where a.SecondLevelSubdivisionTableID == SecondLevel
                                                  select a).FirstOrDefault();
            Label1.Text = Second.Name;

            ReportArchiveTable Report = (from a in kpiWebDataContext.ReportArchiveTable
                                         where a.ReportArchiveTableID == ReportID
                                         select a).FirstOrDefault();
            Label2.Text = Report.Name;

            if (!Page.IsPostBack)
            {
                //мы берем кафедры

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("StructName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StructID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Color", typeof(string)));

                List<ThirdLevelSubdivisionTable> OnlyKafedras = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                 join b in kpiWebDataContext.UsersTable
                                                                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                                 join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                 on b.UsersTableID equals c.FK_UsersTable
                                                                 where
                                                                 a.Active == true
                                                                 && b.Active == true
                                                                 && c.Active == true
                                                                 && c.FK_ParametrsTable == 3828
                                                                 && c.CanView == true
                                                                 && b.FK_SecondLevelSubdivisionTable == SecondLevel
                                                                 select a).Distinct().ToList();



                List<ThirdLevelSubdivisionTable> KafedrasnFaculties =
                                                               (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                join b in kpiWebDataContext.UsersTable
                                                                    on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                                join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                on b.UsersTableID equals c.FK_UsersTable
                                                                where
                                                                a.Active == true
                                                                && b.Active == true
                                                                && c.Active == true
                                                                && c.CanView == true
                                                                && b.FK_SecondLevelSubdivisionTable == SecondLevel
                                                                select a).Distinct().ToList();

                List<ThirdLevelSubdivisionTable> NoKafedra = KafedrasnFaculties;
                int confirmedCafedras = 0;
                bool soglasovano = false;
                foreach (ThirdLevelSubdivisionTable currentCafedra in OnlyKafedras)
                {
                    NoKafedra.Remove(currentCafedra);
                    int coll = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                where a.FK_ThirdLevelSubdivisionTable == currentCafedra.ThirdLevelSubdivisionTableID
                                && a.FK_ReportArchiveTable == ReportID
                                && a.Status == 4
                                select a).Count();
                    int coll2 = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                 where a.FK_ThirdLevelSubdivisionTable == currentCafedra.ThirdLevelSubdivisionTableID
                                 && a.FK_ReportArchiveTable == ReportID
                                 && a.Status == 5
                                 select a).Count();
                    if (coll2 > 0)
                    {
                        soglasovano = true;
                    }

                    if (coll > 0)
                    {
                        confirmedCafedras++;
                    }

                }


                foreach (ThirdLevelSubdivisionTable CurrentThird in NoKafedra)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = CurrentThird.Name;
                    dataRow["StructID"] = CurrentThird.ThirdLevelSubdivisionTableID;

                    int Statusn = (int)(from a in kpiWebDataContext.CollectedBasicParametersTable
                                        where a.FK_ReportArchiveTable == ReportID
                                        && a.Active == true
                                        && a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                        select a.Status).FirstOrDefault();
                    string status = "Нет данных";
                    if (Statusn == 4)
                    {
                        status = "Данные утверждены";
                        dataRow["Color"] = 2;
                    }
                    else if (Statusn == 3)
                    {
                        status = "Данные ожидают утверждения";
                        dataRow["Color"] = 1;
                    }
                    else if (Statusn == 2)
                    {
                        status = "Данные в процессе заполнения";
                        dataRow["Color"] = 1;
                    }
                    else if (Statusn == 1)
                    {
                        status = "Данные возвращены на доработку";
                        dataRow["Color"] = 1;
                    }
                    else if (Statusn == 0)
                    {
                        status = "Данные в процессе заполнения";
                        dataRow["Color"] = 1;
                    }
                    else if (Statusn == 5)
                    {
                        status = "Данные согласованы";
                        dataRow["Color"] = 3;
                    }
                    else
                    {
                        dataRow["Color"] = 1;
                        //error
                    }


                    dataRow["Status"] = status;
                    dataTable.Rows.Add(dataRow);
                }

                if (OnlyKafedras.Count() > 0)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = "Кафедры";
                    dataRow["StructID"] = 0;

                    if (confirmedCafedras == OnlyKafedras.Count)
                    {
                        dataRow["Color"] = 2;
                    }
                    else if (soglasovano)
                    {
                        dataRow["Color"] = 3;
                    }
                    else
                    {
                        dataRow["Color"] = 1;
                    }


                    dataRow["Status"] = "Утверждено " + confirmedCafedras.ToString() + " из " + OnlyKafedras.Count.ToString();
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        protected void ButtonDetailClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
                paramSerialization.l3 = Convert.ToInt32(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии второй уровень
                Response.Redirect("~/Decan/DecViewBasicParams.aspx");
            }
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
    }
}