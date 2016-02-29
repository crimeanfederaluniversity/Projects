using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class RectorApplications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                    Response.Redirect("~/PersonalPagesAdmin/PersonalMainPage.aspx");
            }
            int userID = UserSer.Id;

            RefreshGrid();
        }
        private void RefreshGrid()
        { 
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
                dataTable.Columns.Add(new DataColumn("FIO", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Text", typeof(string)));
              
                dataTable.Columns.Add(new DataColumn("TelephonNumber", typeof(string)));

                using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                {
                    List<Aplication> rectorapp;
                    {
                        rectorapp = (from a in kpiWebDataContext.Aplications where a.Confirmed == 0 && a.Active == true && a.FK_ApplicationType == 2 select a).ToList();
                    }
                    foreach (var app in rectorapp)
                    {
                        UsersTable fio = (from a in kpiWebDataContext.UsersTable where a.UsersTableID == app.FK_UserAdd && a.Active==true select a).FirstOrDefault();                    
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = app.ID;
                        dataRow["Date"] = app.Date;
                        dataRow["FIO"] = fio.Email;
                        dataRow["Text"] = app.Text; 
         
                        dataRow["TelephonNumber"] = app.TelephoneNumber;        
                        dataTable.Rows.Add(dataRow);
                    }
                    GridView1.DataSource = dataTable;
                    GridView1.DataBind();
                }                                 
        }

        protected void YesButtonClick(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    Aplication app = (from a in kPiDataContext.Aplications
                                           where a.ID == Convert.ToInt32(button.CommandArgument)
                                           select a).FirstOrDefault();

                    app.Confirmed = 1;
                    kPiDataContext.SubmitChanges();
                }
                RefreshGrid();

            }

        }
        protected void NoButtonClick(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    Aplication app = (from a in kPiDataContext.Aplications
                                      where a.ID == Convert.ToInt32(button.CommandArgument)
                                      select a).FirstOrDefault();

                    app.Confirmed = 2;
                    kPiDataContext.SubmitChanges();
                }
                RefreshGrid();

            }

        }

    }
}