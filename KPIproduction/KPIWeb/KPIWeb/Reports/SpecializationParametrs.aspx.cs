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
    public partial class SpecializationParametrs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                int UserID = UserSer.Id;
                List<SpecializationTable> specializationTableData= (from a in kPiDataContext.SpecializationTable
                    join b in kPiDataContext.FourthLevelSubdivisionTable
                    on a.SpecializationTableID equals b.FK_Specialization
                    where b.FK_ThirdLevelSubdivisionTable == userTable.FK_ThirdLevelSubdivisionTable
                    select a).ToList();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("SpecializationID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("SpecializationName", typeof(string)));

                dataTable.Columns.Add(new DataColumn("Param1Label", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Param1CheckBox", typeof(string)));

                dataTable.Columns.Add(new DataColumn("Param2Label", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Param2CheckBox", typeof(string)));

                dataTable.Columns.Add(new DataColumn("Param3Label", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Param3CheckBox", typeof(string)));

                dataTable.Columns.Add(new DataColumn("Param4Label", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Param4CheckBox", typeof(string)));

                dataTable.Columns.Add(new DataColumn("Param5Label", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Param5CheckBox", typeof(string)));

                dataTable.Columns.Add(new DataColumn("DeleteSpecializationLabel", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DeleteSpecializationButton", typeof(string)));
               
                foreach (SpecializationTable spec in specializationTableData)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["SpecializationID"] = spec.SpecializationTableID ;
                    dataRow["SpecializationName"] = spec.Name;
                    dataTable.Rows.Add(dataRow);
                }
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void DeleteSpecializationButtonClick(object sender, EventArgs e)
        {
        
        }

        protected void AddSpecializationButtonClick(object sender, EventArgs e)
        {
        
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
        
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                                                                 
                                                                 where a.Name.Contains(TextBox1.Text)
                                                                 || a.SpecializationNumber.Contains(TextBox1.Text)
                                                                 select a).ToList();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("SpecializationID", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SpecializationName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("SpecializationNumber", typeof(string)));
            dataTable.Columns.Add(new DataColumn("AddSpecializationLabel", typeof(string)));
            dataTable.Columns.Add(new DataColumn("AddSpecializationButton", typeof(string)));

            foreach (SpecializationTable spec in specializationTableData)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["SpecializationID"] = spec.SpecializationTableID;
                dataRow["SpecializationName"] = spec.Name;
                dataRow["SpecializationNumber"] = spec.SpecializationNumber;
                dataTable.Rows.Add(dataRow);
            }
            GridView2.DataSource = dataTable;
            GridView2.DataBind();
        }
    }
}
