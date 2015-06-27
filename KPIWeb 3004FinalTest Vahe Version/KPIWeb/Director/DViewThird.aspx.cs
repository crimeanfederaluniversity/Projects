using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Director
{
    public partial class DViewThird : System.Web.UI.Page
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
            int SecondLevel = paramSerialization.l2;
            if (!Page.IsPostBack)
            {
                //мы берем кафедры

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("StructName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StructID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

                List<ThirdLevelSubdivisionTable> Kafedras = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                               where a.FK_SecondLevelSubdivisionTable == SecondLevel
                                                               && a.Active == true
                                                               join b in kpiWebDataContext.UsersTable
                                                               on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                               where b.Active == true
                                                               join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                               on b.UsersTableID equals c.FK_UsersTable
                                                               where c.Active == true
                                                               && c.CanView == true
                                                               select a).Distinct().ToList();



                foreach (ThirdLevelSubdivisionTable CurrentThird in Kafedras)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["StructName"] = CurrentThird.Name;
                    dataRow["StructID"] = CurrentThird.ThirdLevelSubdivisionTableID;

                    int Statusn = (int) (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                                    where a.FK_ReportArchiveTable == ReportID
                                                                    && a.Active == true
                                                                    && a.FK_ThirdLevelSubdivisionTable == CurrentThird.ThirdLevelSubdivisionTableID
                                                                    select a.Status).FirstOrDefault();
                    string status = "Нет данных";
                    if (Statusn == 4)
                    {
                        status = "Данные утверждены";
                    }
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
                    }
                    else
                    {
                        //error
                    }


                    dataRow["Status"] = status;
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
                Response.Redirect("~/Director/ViewBasicParams.aspx");
            }
        }
        
    }
}