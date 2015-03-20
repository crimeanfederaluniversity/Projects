using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class AddSpecialization : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;

            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!IsPostBack)
            {
                ////записали роли в дроп даун
                List<FirstLevelSubdivisionTable> First_stageList =
                    (from item in kPiDataContext.FirstLevelSubdivisionTable
                     select item).OrderBy(mc => mc.Name).ToList();

                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

                

                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
                /// записали академии в дроп даун

            }

            if (ViewState["Selected"] != null)
            if (!IsPostBack && (bool)ViewState["Selected"] == true)
            {
                List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                                                                     join b in kPiDataContext.FourthLevelSubdivisionTable
                                                                     on a.SpecializationTableID equals b.FK_Specialization
                                                                     where b.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList3.SelectedItem.Value) 
                                                                     && b.Active == true
                                                                     select a).ToList();

                CheckBox1.Checked = (from a in kPiDataContext.ThirdLevelParametrs where a.ThirdLevelParametrsID == Convert.ToInt32(DropDownList3.SelectedItem.Value) select a.CanGraduate).FirstOrDefault();

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("SpecializationID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("SpecializationName", typeof(string)));

                dataTable.Columns.Add(new DataColumn("FourthlvlId", typeof(int)));
                dataTable.Columns.Add(new DataColumn("SpecNumber", typeof(string)));

                dataTable.Columns.Add(new DataColumn("DeleteSpecializationLabel", typeof(string)));
                dataTable.Columns.Add(new DataColumn("DeleteSpecializationButton", typeof(string)));
               
                foreach (SpecializationTable spec in specializationTableData)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["SpecializationID"] = spec.SpecializationTableID ;
                    dataRow["SpecializationName"] = spec.Name;
                    dataRow["FourthlvlId"] = (from a in kPiDataContext.FourthLevelSubdivisionTable
                        where
                            a.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList3.SelectedItem.Value) &&
                            a.FK_Specialization == spec.SpecializationTableID
                        select a.FourthLevelSubdivisionTableID).FirstOrDefault();

                    dataRow["SpecNumber"] = spec.SpecializationNumber;
                    dataTable.Rows.Add(dataRow);

                    
                }
                ViewState["GridviewSpec"] = dataTable;
                GridView1.DataSource = dataTable;
                GridView1.DataBind();


                kPiDataContext.SubmitChanges();
            }

            
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList =
                    (from item in kPiDataContext.SecondLevelSubdivisionTable
                     where item.FK_FirstLevelSubdivisionTable == SelectedValue
                     select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();

                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            int SelectedValue = -1;
            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<ThirdLevelSubdivisionTable> third_stage = (from item in kPiDataContext.ThirdLevelSubdivisionTable
                                                                where item.FK_SecondLevelSubdivisionTable == SelectedValue
                                                                select item).OrderBy(mc => mc.ThirdLevelSubdivisionTableID).ToList();

                if (third_stage != null && third_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данные успешно сохранены!');", true);
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

        protected void AddSpecializationButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                var par = button.CommandArgument.ToString();
                var check =
                    (from a in kPiDataContext.SpecializationTable where a.SpecializationNumber == par select a)
                        .FirstOrDefault(); // выбираем специальности по её коду



                using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext()) // проверяем есть/нет записываем в базу
                {
                    FourthLevelSubdivisionTable forthlvlsudtab = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                                                  where a.FK_Specialization == check.SpecializationTableID && a.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList3.SelectedItem.Value)
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
                        forthlvlsudtab.FK_ThirdLevelSubdivisionTable = Convert.ToInt32(DropDownList3.SelectedItem.Value);
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

                    FillGridView(kPiDataContext);
                }

                //Response.Redirect("~/StatisticsDepartment/AddSpecialization.aspx");
            }

                
        }

        private void FillGridView(KPIWebDataContext kPiDataContext)
        {
            List<SpecializationTable> specializationTableData = (from a in kPiDataContext.SpecializationTable
                join b in kPiDataContext.FourthLevelSubdivisionTable
                    on a.SpecializationTableID equals b.FK_Specialization
                where b.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList3.SelectedItem.Value)
                      && b.Active == true
                select a).ToList();

            CheckBox1.Checked = (from a in kPiDataContext.ThirdLevelParametrs
                where a.ThirdLevelParametrsID == Convert.ToInt32(DropDownList3.SelectedItem.Value)
                select a.CanGraduate).FirstOrDefault();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("SpecializationID", typeof (string)));
            dataTable.Columns.Add(new DataColumn("SpecializationName", typeof (string)));

            dataTable.Columns.Add(new DataColumn("FourthlvlId", typeof (int)));
            dataTable.Columns.Add(new DataColumn("SpecNumber", typeof (string)));

            dataTable.Columns.Add(new DataColumn("DeleteSpecializationLabel", typeof (string)));
            dataTable.Columns.Add(new DataColumn("DeleteSpecializationButton", typeof (string)));

            foreach (SpecializationTable spec in specializationTableData)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["SpecializationID"] = spec.SpecializationTableID;
                dataRow["SpecializationName"] = spec.Name;
                dataRow["FourthlvlId"] = (from a in kPiDataContext.FourthLevelSubdivisionTable
                    where
                        a.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList3.SelectedItem.Value) &&
                        a.FK_Specialization == spec.SpecializationTableID
                    select a.FourthLevelSubdivisionTableID).FirstOrDefault();

                dataRow["SpecNumber"] = spec.SpecializationNumber;
                dataTable.Rows.Add(dataRow);
            }
            ViewState["GridviewSpec"] = dataTable;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();

            KPIWebDataContext kpiWeb = new KPIWebDataContext();

            var Isgraduate = (from a in kpiWeb.ThirdLevelParametrs
                where a.ThirdLevelParametrsID == Convert.ToInt32(DropDownList3.SelectedItem.Value) && a.Active == true
                select a).FirstOrDefault();

        
            if ((from a in kpiWeb.FourthLevelSubdivisionTable
                where
                    a.FK_ThirdLevelSubdivisionTable == Convert.ToInt32(DropDownList3.SelectedItem.Value) &&
                    a.Active == true
                select a).Count() > 0 && Isgraduate != null ) // в общем если на кафедре специальностей больше чем 0, то она является выпускающей.


                Isgraduate.CanGraduate = true;
            else if (Isgraduate != null)
                Isgraduate.CanGraduate = false;
           

            kpiWeb.SubmitChanges();

            CheckBox1.Checked = (from a in kPiDataContext.ThirdLevelParametrs
                                 where a.ThirdLevelParametrsID == Convert.ToInt32(DropDownList3.SelectedItem.Value)
                                 select a.CanGraduate).FirstOrDefault();
         

        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList3.SelectedItem != null) ViewState["Selected"] = true;
            else ViewState["Selected"] = false;

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            FillGridView(kPiDataContext);

        }

        protected void DeleteSpecializationButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (KPIWebDataContext kPiDataContext = new KPIWebDataContext())
                {
                    var check =
                    (from a in kPiDataContext.FourthLevelSubdivisionTable
                     where
                         a.FourthLevelSubdivisionTableID == Convert.ToInt32(button.CommandArgument)
                     select a)
                        .FirstOrDefault();

                    check.Active = false;

                    
                    kPiDataContext.SubmitChanges();

                    FillGridView(kPiDataContext);
                }

                //Response.Redirect("~/StatisticsDepartment/AddSpecialization.aspx");

            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
           
        }
    }
}