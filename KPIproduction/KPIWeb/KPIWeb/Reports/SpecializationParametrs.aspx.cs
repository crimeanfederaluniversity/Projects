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

            ViewState["UserTable"] = userTable.FK_ThirdLevelSubdivisionTable;
            if (userTable.AccessLevel != 0)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (!Page.IsPostBack)
            {
                int UserID = UserSer.Id;
                List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                                                                     join b in kPiDataContext.FourthLevelSubdivisionTable
                                                                     on a.SpecializationTableID equals b.FK_Specialization
                                                                     where b.FK_ThirdLevelSubdivisionTable == userTable.FK_ThirdLevelSubdivisionTable && b.Active == true
                                                                     select a).ToList();
                

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("SpecializationID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("SpecializationName", typeof(string)));

                dataTable.Columns.Add(new DataColumn("FourthlvlId", typeof(int)));

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
                    dataRow["FourthlvlId"] = (from a in kPiDataContext.FourthLevelSubdivisionTable
                        where
                            a.FK_ThirdLevelSubdivisionTable == userTable.FK_ThirdLevelSubdivisionTable &&
                            a.FK_Specialization == spec.SpecializationTableID
                        select a.FourthLevelSubdivisionTableID).FirstOrDefault();

                    dataTable.Rows.Add(dataRow);

                    
                }
                ViewState["GridviewSpec"] = dataTable;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void DeleteSpecializationButtonClick(object sender, EventArgs e)
        {
         Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    var check =
                    (from a in kPiDataContext.FourthLevelSubdivisionTable where a.FK_Specialization == Convert.ToInt32(button.CommandArgument) select a)
                        .FirstOrDefault();

                    check.Active = false;
                    kPiDataContext.SubmitChanges();
                }
                Response.Redirect("~/Reports/SpecializationParametrs.aspx");

            }

        }

        protected void AddSpecializationButtonClick(object sender, EventArgs e)
        {
           
            bool isHere = false;
            Button button = (Button)sender;
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                var par = button.CommandArgument.ToString();
                var check =
                    (from a in kPiDataContext.SpecializationTable where a.SpecializationNumber == par select a)
                        .FirstOrDefault();

              /*  List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                                                                     join b in kPiDataContext.FourthLevelSubdivisionTable
                                                                     on a.SpecializationTableID equals b.FK_Specialization
                                                                     where b.FK_ThirdLevelSubdivisionTable == (int)ViewState["UserTable"]
                                                                     select a).ToList();

                if (check != null)
                {

                    foreach (SpecializationTable spec in specializationTableData)
                    {
                        if (spec.SpecializationNumber.Equals(par)) isHere = true;
                    }
                }*/

                if (!isHere)
                {
                     using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                     {
                        FourthLevelSubdivisionTable forthlvlsudtab = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                            where a.FK_Specialization == check.SpecializationTableID && a.FK_ThirdLevelSubdivisionTable == (int)ViewState["UserTable"]
                            select a).FirstOrDefault();

                        if (forthlvlsudtab != null)
                        {
                             forthlvlsudtab.Active = true;
                         }
                         else
                        {
                         forthlvlsudtab = new FourthLevelSubdivisionTable();
                             forthlvlsudtab.FK_Specialization = check.SpecializationTableID;
                             forthlvlsudtab.Active = true;
                             forthlvlsudtab.Name = check.Name;
                             forthlvlsudtab.FK_ThirdLevelSubdivisionTable = (int)ViewState["UserTable"];
                             kpiWebDataContext.FourthLevelSubdivisionTable.InsertOnSubmit(forthlvlsudtab);
                             //Page.DataBind();
                         }
                         kpiWebDataContext.SubmitChanges();
                     }

                }
                Response.Redirect("~/Reports/SpecializationParametrs.aspx");
            }

                
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            if (ViewState["GridviewSpec"] != null)
            {
                DataTable dataTable = (DataTable) ViewState["GridviewSpec"];



                if (dataTable.Rows.Count > 0)
                {

                    for (int i = 1; i <= dataTable.Rows.Count; i++)
                    {

                        CheckBox checkBoxparam = (CheckBox) GridView1.Rows[rowIndex].FindControl("Param4CheckBox");
                        Label labelId = (Label) GridView1.Rows[rowIndex].FindControl("FourthlvlId");
                        rowIndex++;
                         if (checkBoxparam.Checked == true)
                        {
                            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                            {
                                FourthLevelParametrs fourthLevelParametrsTables = new FourthLevelParametrs();
                                fourthLevelParametrsTables.FourthLevelParametrsID = Convert.ToInt32(labelId.Text);
                                fourthLevelParametrsTables.IsForeignStudentsAccept = true;
                                var code = (from f4 in kpiWebDataContext.FourthLevelSubdivisionTable     // получаем код специальности
                                            join spec in kpiWebDataContext.SpecializationTable
                                            on f4.FK_Specialization equals spec.SpecializationTableID
                                            where f4.FourthLevelSubdivisionTableID == Convert.ToInt32(labelId.Text)
                                            select spec.SpecializationNumber).FirstOrDefault();

                                fourthLevelParametrsTables.SpecType = Action.Encode(code);

                                kpiWebDataContext.FourthLevelParametrs.InsertOnSubmit(fourthLevelParametrsTables);
                                kpiWebDataContext.SubmitChanges();
                            }

                        }

                    }
                }
            }




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
