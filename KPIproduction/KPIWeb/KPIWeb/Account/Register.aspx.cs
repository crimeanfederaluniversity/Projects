using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using KPIWeb.Models;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace KPIWeb.Account
{
    public partial class Register : Page
    {
        protected void FillGridVIews(int reportID_)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            ///////////////////////////////////////////////////////////////////////////////////////////////////////                
            List<IndicatorsTable> indicatorTable =
            (from item in kPiDataContext.IndicatorsTable where item.Active == true select item).ToList();
            DataTable dataTableIndicator = new DataTable();

            dataTableIndicator.Columns.Add(new DataColumn("IndicatorID", typeof(string)));
            dataTableIndicator.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
            dataTableIndicator.Columns.Add(new DataColumn("IndicatorEditCheckBox", typeof(bool)));
            dataTableIndicator.Columns.Add(new DataColumn("IndicatorViewCheckBox", typeof(bool)));
            dataTableIndicator.Columns.Add(new DataColumn("IndicatorConfirmCheckBox", typeof(bool)));
            foreach (IndicatorsTable indicator in indicatorTable)
            {
                DataRow dataRow = dataTableIndicator.NewRow();
                dataRow["IndicatorID"] = indicator.IndicatorsTableID.ToString();
                dataRow["IndicatorName"] = indicator.Name;
                dataRow["IndicatorEditCheckBox"] = false;
                dataRow["IndicatorViewCheckBox"] = false;
                dataRow["IndicatorConfirmCheckBox"] = false;
                /*dataRow["IndicatorCheckBox"] = ((from a in kPiDataContext.ReportArchiveAndIndicatorsMappingTable
                                                 where a.Active == true
                                                 && a.FK_IndicatorsTable == indicator.IndicatorsTableID
                                                 && a.FK_ReportArchiveTable == reportID
                                                 select a).Count() > 0) ? true : false;*/
                dataTableIndicator.Rows.Add(dataRow);
            }
            Gridview1.DataSource = dataTableIndicator;
            Gridview1.DataBind();

            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<CalculatedParametrs> calcParams =
            (from item in kPiDataContext.CalculatedParametrs where item.Active == true select item).ToList();
            DataTable dataTableCalc = new DataTable();

            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsID", typeof(string)));
            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsName", typeof(string)));
            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsEditCheckBox", typeof(bool)));
            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsViewCheckBox", typeof(bool)));
            dataTableCalc.Columns.Add(new DataColumn("CalculatedParametrsConfirmCheckBox", typeof(bool)));
            foreach (CalculatedParametrs calcParam in calcParams)
            {
                DataRow dataRow = dataTableCalc.NewRow();
                dataRow["CalculatedParametrsID"] = calcParam.CalculatedParametrsID.ToString();
                dataRow["CalculatedParametrsName"] = calcParam.Name;
                dataRow["CalculatedParametrsEditCheckBox"] = false;
                dataRow["CalculatedParametrsViewCheckBox"] = false;
                dataRow["CalculatedParametrsConfirmCheckBox"] = false;
                /*dataRow["CalculatedParametrsCheckBox"] = ((from a in kPiDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                                           where a.Active == true
                                                           && a.FK_CalculatedParametrsTable == calcParam.CalculatedParametrsID
                                                           && a.FK_ReportArchiveTable == reportID
                                                           select a).Count() > 0) ? true : false;*/
                dataTableCalc.Rows.Add(dataRow);
            }
            Gridview2.DataSource = dataTableCalc;
            Gridview2.DataBind();
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            List<BasicParametersTable> basicParams =
          (from item in kPiDataContext.BasicParametersTable where item.Active == true select item).ToList();
            DataTable dataTableBasic = new DataTable();

            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsID", typeof(string)));
            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsName", typeof(string)));
            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsEditCheckBox", typeof(bool)));
            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsViewCheckBox", typeof(bool)));
            dataTableBasic.Columns.Add(new DataColumn("BasicParametrsConfirmCheckBox", typeof(bool)));
            foreach (BasicParametersTable basic in basicParams)
            {
                DataRow dataRow = dataTableBasic.NewRow();
                dataRow["BasicParametrsID"] = basic.BasicParametersTableID.ToString();
                dataRow["BasicParametrsName"] = basic.Name;
                dataRow["BasicParametrsEditCheckBox"] = false;
                dataRow["BasicParametrsViewCheckBox"] = false;
                dataRow["BasicParametrsConfirmCheckBox"] = false;
                /*dataRow["BasicParametrsCheckBox"] = ((from a in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                      where a.Active == true
                                                      && a.FK_BasicParametrsTable == basic.BasicParametersTableID
                                                      && a.FK_ReportArchiveTable == reportID
                                                      select a).Count() > 0) ? true : false;*/
                dataTableBasic.Rows.Add(dataRow);
            }
            Gridview3.DataSource = dataTableBasic;
            Gridview3.DataBind();
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            ViewState["BasicDataTable"] = dataTableBasic;
            ViewState["CalculateDataTable"] = dataTableCalc;
            ViewState["IndicatorDataTable"] = dataTableIndicator;            
        }
        protected void CreateUser_Click(object sender, EventArgs e)
        {
            //try
            //{
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                if ((from a in kPiDataContext.UsersTable where a.Login == UserName.Text select a).ToList().Count>0)
                {
                     Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Пользователь с таким логином уже существует, выберите другой логин!');", true);    
                }
                else
                {
                UsersTable user = new UsersTable();
                user.Active = true;
                user.Login = UserName.Text;
                user.Password = Password.Text;
                user.Email = Email.Text;

                int selectedValue = -1;
                if (int.TryParse(DropDownList1.SelectedValue, out selectedValue) && selectedValue > 0)
                    user.FK_FirstLevelSubdivisionTable = selectedValue;

                selectedValue = -1;
                if (int.TryParse(DropDownList2.SelectedValue, out selectedValue) && selectedValue > 0)
                    user.FK_SecondLevelSubdivisionTable = selectedValue;

                selectedValue = -1;
                if (int.TryParse(DropDownList3.SelectedValue, out selectedValue) && selectedValue > 0)
                    user.FK_ThirdLevelSubdivisionTable = selectedValue;

                user.AccessLevel = 0; ///////НАДО ПРОДУМАТЬ

            if (DropDownList5.SelectedIndex == 2)
            {
                user.AccessLevel = 5;
            }

            if (DropDownList5.SelectedIndex == 3)
            {
                user.AccessLevel = 10;
            }

                user.FK_ZeroLevelSubdivisionTable = 1;

                kPiDataContext.UsersTable.InsertOnSubmit(user);
                kPiDataContext.SubmitChanges();
                //// ПОЛЬЗОВАТЕЛЬ СОЗДАН

                int userID = user.UsersTableID;

                ///////////////////////////////////////////шаблон//////////////////////////////////
                int rowIndex = 0;

                //int currentRoleId = Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value);
                //DataTable roleBasicParametrs = (DataTable)ViewState["GridviewRoleMapping"];

                if (Gridview3.Rows.Count > 0)
                {
                    for (int k = 1; k <= Gridview3.Rows.Count; k++)
                    {
                        CheckBox canEdit = (CheckBox)Gridview3.Rows[rowIndex].FindControl("BasicParametrsEditCheckBox");
                        CheckBox canView = (CheckBox)Gridview3.Rows[rowIndex].FindControl("BasicParametrsViewCheckBox");
                        CheckBox canConfirm = (CheckBox)Gridview3.Rows[rowIndex].FindControl("BasicParametrsConfirmCheckBox");
                        Label label = (Label)Gridview3.Rows[rowIndex].FindControl("BasicParametrsID");

                        BasicParametrsAndUsersMapping BasicAndUsers = new BasicParametrsAndUsersMapping();
                        BasicAndUsers.Active = true;
                        BasicAndUsers.FK_ParametrsTable = Convert.ToInt32(label.Text);
                        BasicAndUsers.CanConfirm = canConfirm.Checked;
                        BasicAndUsers.CanEdit = canEdit.Checked;
                        BasicAndUsers.CanView = canView.Checked;
                        BasicAndUsers.FK_UsersTable = userID;
                        kPiDataContext.BasicParametrsAndUsersMapping.InsertOnSubmit(BasicAndUsers);
                        kPiDataContext.SubmitChanges();
                        rowIndex++;
                    }
                    if (DropDownList5.SelectedIndex == 2)// если человек руководитель
                    {
                        rowIndex = 0;
                        if (Gridview1.Rows.Count > 0)
                        {
                            for (int k = 1; k <= Gridview1.Rows.Count; k++)
                            {
                                CheckBox canEdit = (CheckBox)Gridview1.Rows[rowIndex].FindControl("IndicatorEditCheckBox");
                                CheckBox canView = (CheckBox)Gridview1.Rows[rowIndex].FindControl("IndicatorViewCheckBox");
                                CheckBox canConfirm = (CheckBox)Gridview1.Rows[rowIndex].FindControl("IndicatorConfirmCheckBox");
                                Label label = (Label)Gridview1.Rows[rowIndex].FindControl("IndicatorID");

                                IndicatorsAndUsersMapping indAndUser = new IndicatorsAndUsersMapping();
                                indAndUser.Active = true;
                                indAndUser.FK_IndicatorsTable = Convert.ToInt32(label.Text);
                                indAndUser.CanConfirm = canConfirm.Checked;
                                indAndUser.CanEdit = canEdit.Checked;
                                indAndUser.CanView = canView.Checked;
                                indAndUser.FK_UsresTable = userID;
                                kPiDataContext.IndicatorsAndUsersMapping.InsertOnSubmit(indAndUser);
                                kPiDataContext.SubmitChanges();
                                rowIndex++;
                            }
                        }
                        rowIndex = 0;
                        if (Gridview2.Rows.Count > 0)
                        {
                            for (int k = 1; k <= Gridview2.Rows.Count; k++)
                            {
                                CheckBox canEdit = (CheckBox)Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsEditCheckBox");
                                CheckBox canView = (CheckBox)Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsViewCheckBox");
                                CheckBox canConfirm = (CheckBox)Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsConfirmCheckBox");
                                Label label = (Label)Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsID");

                                CalculatedParametrsAndUsersMapping calcAndUser = new CalculatedParametrsAndUsersMapping();
                                calcAndUser.Active = true;
                                calcAndUser.FK_CalculatedParametrsTable = Convert.ToInt32(label.Text);
                                calcAndUser.CanConfirm = canConfirm.Checked;
                                calcAndUser.CanEdit = canEdit.Checked;
                                calcAndUser.CanView = canView.Checked;
                                calcAndUser.FK_UsersTable = userID;
                                kPiDataContext.CalculatedParametrsAndUsersMapping.InsertOnSubmit(calcAndUser);
                                kPiDataContext.SubmitChanges();
                                rowIndex++;
                            }
                        }
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    LogHandler.LogWriter.WriteError(ex);
            //}
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
        protected void Page_Load(object sender, EventArgs e)
        {

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            ///////////////////////////////////////////////////проверили на админа
            if (!Page.IsPostBack)
            {
                Gridview1.Visible = false;
                Label25.Visible = false;
                Gridview2.Visible = false;
                Label26.Visible = false;
                Gridview3.Visible = false;
                Label27.Visible = false;
                DropDownList4.Visible = false;
                Label24.Visible = false;

                List<RolesTable> Roles = (from a in kPiDataContext.RolesTable
                                          where a.Active == true
                                          select a).ToList();
                int i = 1;
                DropDownList4.Items.Add("Выберите шаблон");
                foreach (RolesTable role in Roles)
                {
                    DropDownList4.Items.Add(role.RoleName);
                    DropDownList4.Items[i].Value = role.RolesTableID.ToString();
                    i++;
                }
             ////записали роли в дроп даун
                List<FirstLevelSubdivisionTable> First_stageList =
                    (from item in kPiDataContext.FirstLevelSubdivisionTable
                        select item).OrderBy(mc => mc.Name).ToList();

                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

                FillGridVIews(0);

                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();           
            /// записали академии в дроп даун
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
                    Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script", "alert('Произошла ошибка.');",true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
            }
        }
    
        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList4.SelectedIndex != 0)
            {
                Gridview3.DataSource = null;
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                var vrCountry = (from b in kpiWebDataContext.BasicParametersTable select b);

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("BasicParametrsConfirmCheckBox", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("BasicParametrsEditCheckBox", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("BasicParametrsViewCheckBox", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("BasicParametrsName", typeof (string)));
                dataTable.Columns.Add(new DataColumn("BasicParametrsID", typeof (string)));
                int i = 1;

                foreach (var obj in vrCountry)
                {
                    DataRow dataRow = dataTable.NewRow();
                    BasicParametersAndRolesMappingTable roleAndBasicMapping =
                        (from a in kpiWebDataContext.BasicParametersAndRolesMappingTable
                            where a.FK_BasicParametersTable == obj.BasicParametersTableID
                                  &&
                                  a.FK_RolesTable ==
                                  Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value)
                            select a).FirstOrDefault();
                    if (roleAndBasicMapping != null)
                    {
                        dataRow["BasicParametrsEditCheckBox"] = roleAndBasicMapping.CanEdit;
                        dataRow["BasicParametrsViewCheckBox"] = roleAndBasicMapping.CanView;
                        dataRow["BasicParametrsConfirmCheckBox"] = roleAndBasicMapping.CanConfirm;
                    }
                    else
                    {
                        dataRow["BasicParametrsEditCheckBox"] = false;
                        dataRow["BasicParametrsViewCheckBox"] = false;
                        dataRow["BasicParametrsConfirmCheckBox"] = false;
                    }
                    dataRow["BasicParametrsID"] = obj.BasicParametersTableID.ToString();
                    dataRow["BasicParametrsName"] = obj.Name;
                    dataTable.Rows.Add(dataRow);
                    i++;
                }
                // ViewState["GridviewRoleMapping"] = dataTable;
                Gridview3.DataSource = dataTable;
                Gridview3.DataBind();
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
           // if 
        }

        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList5.SelectedIndex == 0 || DropDownList5.SelectedIndex ==3)
            {
                Gridview1.Visible = false;
                Label25.Visible = false;
                Gridview2.Visible = false;
                Label26.Visible = false;
                Gridview3.Visible = false;
                Label27.Visible = false;
                DropDownList4.Visible = false;
                Label24.Visible = false;
            }
            else if (DropDownList5.SelectedIndex == 1)
            {
                Gridview1.Visible = false;
                Label25.Visible = false;
                Gridview2.Visible = false;
                Label26.Visible = false;
                Gridview3.Visible = true;
                Label27.Visible = true;
                DropDownList4.Visible = true;
                Label24.Visible = true;
            }
            else if (DropDownList5.SelectedIndex == 2)
            {
                Gridview1.Visible = true;
                Label25.Visible = true;
                Gridview2.Visible = true;
                Label26.Visible = true;
                Gridview3.Visible = true;
                Label27.Visible = true;
                DropDownList4.Visible = false;
                Label24.Visible = false;
            }
        }

        protected void Gridview3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

}