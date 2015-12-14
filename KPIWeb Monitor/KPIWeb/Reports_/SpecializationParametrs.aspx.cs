using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlTypes;
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
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ViewState["UserTable"] = userTable.FK_ThirdLevelSubdivisionTable;
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 9, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            if (!Page.IsPostBack)
            {
                int UserID = UserSer.Id;
                List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                                                                     join b in kPiDataContext.FourthLevelSubdivisionTable
                                                                     on a.SpecializationTableID equals b.FK_Specialization
                                                                     where b.FK_ThirdLevelSubdivisionTable == userTable.FK_ThirdLevelSubdivisionTable 
                                                                     && b.Active == true
                                                                     select a).ToList();

                CheckBox1.Checked = (from a in kPiDataContext.ThirdLevelParametrs where a.ThirdLevelParametrsID == userTable.FK_ThirdLevelSubdivisionTable select a.CanGraduate).FirstOrDefault();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("SpecializationID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("SpecializationName", typeof(string)));

                dataTable.Columns.Add(new DataColumn("FourthlvlId", typeof(int)));
                dataTable.Columns.Add(new DataColumn("SpecNumber", typeof(string)));

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

                    dataRow["SpecNumber"] = spec.SpecializationNumber;
                    dataTable.Rows.Add(dataRow);

                    
                }
                ViewState["GridviewSpec"] = dataTable;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
            if (CheckBox1.Checked)
            {
                GridView1.Visible = true;
                Label3.Visible    = true;
                Label1.Visible    = true;
                GridView2.Visible = true;
                Button2.Visible   = true;
                TextBox1.Visible  = true;
            }
            else
            {
                GridView1.Visible = false;
                Label3.Visible = false;
                Label1.Visible = false;
                GridView2.Visible = false;
                Button2.Visible = false;
                TextBox1.Visible = false;
            }
        }

        protected void DeleteSpecializationButtonClick(object sender, EventArgs e)
        {
         Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    var check =
                    (from a in kPiDataContext.FourthLevelSubdivisionTable where 
                         a.FourthLevelSubdivisionTableID == Convert.ToInt32(button.CommandArgument) select a)
                        .FirstOrDefault();

                    check.Active = false;
                    kPiDataContext.SubmitChanges();
                }
                Response.Redirect("~/Reports_/SpecializationParametrs.aspx");

            }

        }

        protected void AddSpecializationButtonClick(object sender, EventArgs e)
        {
           
           
            Button button = (Button)sender;
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                var par = button.CommandArgument.ToString();
                var check =
                    (from a in kPiDataContext.SpecializationTable where a.SpecializationNumber == par select a)
                        .FirstOrDefault(); // выбираем специальности по её коду

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

                     using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext()) // проверяем есть/нет записываем в базу
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
                         }


                        kpiWebDataContext.SubmitChanges();

                         // Добавляем запись в таблицу параметров для этой специальности

                         FourthLevelParametrs fourthLevelParametrs = (from a in kpiWebDataContext.FourthLevelParametrs
                             where a.FourthLevelParametrsID == forthlvlsudtab.FourthLevelSubdivisionTableID
                             select a).FirstOrDefault();

                         if (fourthLevelParametrs == null)
                         {
                             fourthLevelParametrs = new FourthLevelParametrs();
                             fourthLevelParametrs.Active = true;
                             fourthLevelParametrs.IsModernEducationTechnologies = false;
                             fourthLevelParametrs.IsNetworkComunication = false;
                             fourthLevelParametrs.IsInvalidStudentsFacilities = false;
                             fourthLevelParametrs.IsForeignStudentsAccept = false;
                             fourthLevelParametrs.FourthLevelParametrsID = forthlvlsudtab.FourthLevelSubdivisionTableID;

                             var code = (from f4 in kpiWebDataContext.FourthLevelSubdivisionTable     // получаем код специальности
                                            join spec in kpiWebDataContext.SpecializationTable
                                            on f4.FK_Specialization equals spec.SpecializationTableID
                                            where f4.FourthLevelSubdivisionTableID == forthlvlsudtab.FourthLevelSubdivisionTableID
                                            select spec.SpecializationNumber).FirstOrDefault();

                             fourthLevelParametrs.SpecType = Action.Encode(code);
                             kpiWebDataContext.FourthLevelParametrs.InsertOnSubmit(fourthLevelParametrs);
                         }


                         kpiWebDataContext.SubmitChanges();
                     }

                Response.Redirect("~/Reports_/SpecializationParametrs.aspx");
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

                        CheckBox checkBoxparamIsModern = (CheckBox) GridView1.Rows[rowIndex].FindControl("IsModern");
                        CheckBox checkBoxparamIsNetwork = (CheckBox)GridView1.Rows[rowIndex].FindControl("IsNetwork");
                        CheckBox checkBoxparamIsInvalid = (CheckBox)GridView1.Rows[rowIndex].FindControl("IsInvalid");
                        CheckBox checkBoxparamIsForeign = (CheckBox)GridView1.Rows[rowIndex].FindControl("IsForeign");
                        Label labelId = (Label) GridView1.Rows[rowIndex].FindControl("FourthlvlId");
                        rowIndex++;
                        var ss = labelId.Text;
                            using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                            {
                                FourthLevelParametrs fourthLevelParametrsTables = (from a in kpiWebDataContext.FourthLevelParametrs where a.FourthLevelParametrsID == Convert.ToInt32(labelId.Text) select a).FirstOrDefault();
                                ThirdLevelParametrs thirdLevelParametrs = (from a in kpiWebDataContext.ThirdLevelParametrs where a.ThirdLevelParametrsID == (int)ViewState["UserTable"] select a).FirstOrDefault();

                                if (fourthLevelParametrsTables != null)
                                {
                                    fourthLevelParametrsTables.IsModernEducationTechnologies =checkBoxparamIsModern.Checked;
                                    fourthLevelParametrsTables.IsNetworkComunication = checkBoxparamIsNetwork.Checked;
                                    fourthLevelParametrsTables.IsInvalidStudentsFacilities = checkBoxparamIsInvalid.Checked;
                                    fourthLevelParametrsTables.IsForeignStudentsAccept = checkBoxparamIsForeign.Checked;
                                }

                                if (thirdLevelParametrs != null)
                                    thirdLevelParametrs.CanGraduate = CheckBox1.Checked;

                                kpiWebDataContext.SubmitChanges();
                            }
                    }
                }
            }

            Response.Redirect("~/Reports_/ChooseReport.aspx");


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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Проставляем галочки в GridView  

            for (int i = 0; i <= GridView1.Rows.Count; i++)
            {
                var fourthlvlId = e.Row.FindControl("FourthlvlId") as Label;
                var checkBoxparamIsModern = e.Row.FindControl("IsModern") as CheckBox;
                var checkBoxparamIsNetwork = e.Row.FindControl("IsNetwork") as CheckBox;
                var checkBoxparamIsInvalid = e.Row.FindControl("IsInvalid") as CheckBox;
                var checkBoxparamIsForeign = e.Row.FindControl("IsForeign") as CheckBox;

                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                FourthLevelParametrs fourthLevelParametrs = new FourthLevelParametrs();

                if (fourthlvlId != null)
                fourthLevelParametrs = (from a in kpiWebDataContext.FourthLevelParametrs where a.FourthLevelParametrsID == Convert.ToInt32(fourthlvlId.Text) select a).FirstOrDefault(); 

                if (fourthLevelParametrs != null)
                {
                    if (checkBoxparamIsModern != null)
                        checkBoxparamIsModern.Checked = fourthLevelParametrs.IsModernEducationTechnologies.Value;
                    if (checkBoxparamIsNetwork != null)
                        checkBoxparamIsNetwork.Checked = fourthLevelParametrs.IsNetworkComunication.Value;
                    if (checkBoxparamIsInvalid != null)
                        checkBoxparamIsInvalid.Checked = fourthLevelParametrs.IsInvalidStudentsFacilities.Value;
                    if (checkBoxparamIsForeign != null)
                        checkBoxparamIsForeign.Checked = fourthLevelParametrs.IsForeignStudentsAccept.Value;
                }

            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                GridView1.Visible = true;
                Label3.Visible = true;
                Label1.Visible = true;
                GridView2.Visible = true;
                Button2.Visible = true;
                TextBox1.Visible = true;
            }
            else
            {
                GridView1.Visible = false;
                Label3.Visible = false;
                Label1.Visible = false;
                GridView2.Visible = false;
                Button2.Visible = false;
                TextBox1.Visible = false;
            }
        }
    }
}
