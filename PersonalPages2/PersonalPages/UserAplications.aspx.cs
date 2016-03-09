using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace PersonalPages
{
    public partial class NEw : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          /*  Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            */
            RefreshGrid();
        }
        private void RefreshGrid()
        {
          //  Serialization ser = (Serialization)Session["UserID"];
            int userToChangeId = 15043;
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("ID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Date", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Type", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Text", typeof(string)));
            dataTable.Columns.Add(new DataColumn("Status", typeof(string)));


            using (PersonalPagesDataContext kpiWebDataContext = new PersonalPagesDataContext())
            {
                List<Aplication> userApp;
                {
                    userApp = (from a in kpiWebDataContext.Aplications where a.Active == true && a.FK_UserAdd== userToChangeId select a).ToList();
                }
                foreach (var app in userApp)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = app.ID;
                    dataRow["Date"] = app.Date;
                    dataRow["Type"] = (from a in kpiWebDataContext.Aplications
                                       join b in kpiWebDataContext.ApplictionTypes on a.FK_ApplicationType equals b.ID
                                       where a.Active == true && b.Active == true && a.ID == app.ID
                                       select b.ApplicationType).FirstOrDefault();
                    switch (app.Confirmed)
                    {
                        case 0:
                            dataRow["Status"] = "На рассмотрении";
                            break;
                        case 1:
                            dataRow["Status"] = "Отклонена";
                            break;
                        case 2:
                            dataRow["Status"] = "Принята";
                            break;
                    }
                    
                    dataRow["Text"] = app.Text;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }
        protected void DeleteButtonClick(object sender, EventArgs e)
        {

            Button button = (Button)sender;
            {
                using (PersonalPagesDataContext kPiDataContext = new PersonalPagesDataContext())
                {
                    Aplication app = (from a in kPiDataContext.Aplications
                                       where a.ID == Convert.ToInt32(button.CommandArgument)
                                       select a).FirstOrDefault();
                    app.Active = false;
                    kPiDataContext.SubmitChanges();
                }
                RefreshGrid();

            }

        }
    }
}