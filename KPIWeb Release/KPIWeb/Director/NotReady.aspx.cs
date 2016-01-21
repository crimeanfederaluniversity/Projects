using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Director
{
    public partial class NotReady : System.Web.UI.Page
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
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("Faculty", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Kafedra", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

                List<ThirdLevelSubdivisionTable> Third = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                          where a.Active == true
                                                          join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                              on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                          where b.Active == true
                                                          && b.FK_FirstLevelSubmisionTableId == userTable.FK_FirstLevelSubdivisionTable
                                                          && b.FK_ReportArchiveTableId == Report.ReportArchiveTableID
                                                          select a).Distinct().ToList();

                foreach (ThirdLevelSubdivisionTable CurrentThird in Third)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Faculty"] = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                          where a.SecondLevelSubdivisionTableID == CurrentThird.FK_SecondLevelSubdivisionTable
                                          select a.Name).FirstOrDefault();
                    dataRow["Kafedra"] = CurrentThird.Name;

                    CollectedBasicParametersTable collected = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                               where a.Active == true
                                                               && a.FK_ReportArchiveTable == ReportID
                                                               && a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                               join b in kpiWebDataContext.BasicParametrAdditional
                                                               on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                               where b.Calculated == false
                                                               select a).FirstOrDefault();
                    string status = "Нет данных";

                    if (collected == null)
                    {
                        dataRow["Status"] = "Заполнение на начато";
                        dataTable.Rows.Add(dataRow);
                    }
                    else
                    {
                        int Statusn = (int)collected.Status;
                        if (Statusn == 4)
                        {
                            status = "Данные утверждены";
                        }
                        else
                        {
                            dataRow["Status"] = "Данные в процессе заполнения";
                            dataTable.Rows.Add(dataRow);
                        }
                        /*
                        else if (Statusn == 3)
                        {
                            status = "Данные ожидают утверждения";
                        }
                        else if (Statusn == 2)
                        {
                            status = "Данные в процессе заполнения";
                        }
                        else if (Statusn == 1)
                        {
                            status = "Данные возвращены на доработку";
                        }
                        else if (Statusn == 0)
                        {
                            status = "Данные в процессе заполнения";
                        }*/
                    }

                    
                    


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
    }
}