using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class AddRole : System.Web.UI.Page
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

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/AutomationDepartment.aspx");
            }
            ////////////////////////////////////////////////////////
            /// 
            if (!Page.IsPostBack)
            {
                List<RolesTable> Roles = (from a in kPiDataContext.RolesTable
                    where a.Active == true
                    select a).ToList();
                int i = 0;
                foreach (RolesTable role in Roles)
                {
                    DropDownList1.Items.Add(role.RoleName);
                    DropDownList1.Items[i].Value = role.RolesTableID.ToString();
                    i++;
                }
            }

        }
        private void RefreshGridView() // стягиваем с базы в грид с проставленными галочками основываясь на дроп дауне
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();


            var vrCountry = (from b in kpiWebDataContext.BasicParametersTable select b);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("VerifyChecked", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("EditChecked", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("ViewChecked", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("BasicId", typeof(string)));
            RolesTable role = (from a in kpiWebDataContext.RolesTable
                where a.RolesTableID == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                select a).FirstOrDefault();

            #region
            foreach (var obj in vrCountry)
            {
                DataRow dataRow = dataTable.NewRow();
                BasicParametersAndRolesMappingTable roleAndBasicMapping =
                    (from a in kpiWebDataContext.BasicParametersAndRolesMappingTable
                     where a.FK_BasicParametersTable == obj.BasicParametersTableID
                     && a.FK_RolesTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                     select a).FirstOrDefault();
                if (roleAndBasicMapping != null)
                {
                    dataRow["EditChecked"] = roleAndBasicMapping.CanEdit;
                    dataRow["ViewChecked"] = roleAndBasicMapping.CanView;
                    dataRow["VerifyChecked"] = roleAndBasicMapping.CanConfirm;
                }
                else
                {
                    dataRow["EditChecked"] = false;
                    dataRow["ViewChecked"] = false;
                    dataRow["VerifyChecked"] = false;
                }
                dataRow["BasicId"] = obj.BasicParametersTableID.ToString();
                dataRow["Name"] = obj.Name;
                dataTable.Rows.Add(dataRow);
            }
            ViewState["BasicRoleMapping"] = dataTable;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
            #endregion

            if ((bool)role.IsHead)
            {
                var calcParam = (from a in kpiWebDataContext.CalculatedParametrs select a);
                var Indicators = (from c in kpiWebDataContext.IndicatorsTable select c);

                DataTable calcTable = new DataTable();
                calcTable.Columns.Add(new DataColumn("VerifyChecked1", typeof(bool)));
                calcTable.Columns.Add(new DataColumn("ViewChecked1", typeof(bool)));
                calcTable.Columns.Add(new DataColumn("Name1", typeof(string)));
                calcTable.Columns.Add(new DataColumn("CalcID", typeof(string)));

                DataTable indicatorTable = new DataTable();
                indicatorTable.Columns.Add(new DataColumn("VerifyChecked2", typeof(bool)));
                indicatorTable.Columns.Add(new DataColumn("ViewChecked2", typeof(bool)));
                indicatorTable.Columns.Add(new DataColumn("Name2", typeof(string)));
                indicatorTable.Columns.Add(new DataColumn("IndID", typeof(string)));

                
                #region
                foreach (var obj in calcParam)
                {
                    DataRow dataRow = calcTable.NewRow();
                    CalculatedParametrsAndRolesMappingTable roleAndCalcMapping =
                        (from a in kpiWebDataContext.CalculatedParametrsAndRolesMappingTable
                         where a.FK_CalculatedParametrs == obj.CalculatedParametrsID
                         && a.FK_RolesTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                         select a).FirstOrDefault();
                    if (roleAndCalcMapping != null)
                    {
                        dataRow["ViewChecked1"] = roleAndCalcMapping.CanView;
                        dataRow["VerifyChecked1"] = roleAndCalcMapping.CanConfirm;
                    }
                    else
                    {
                        dataRow["ViewChecked1"] = false;
                        dataRow["VerifyChecked1"] = false;
                    }
                    dataRow["CalcID"] = obj.CalculatedParametrsID.ToString();
                    dataRow["Name1"] = obj.Name;
                    calcTable.Rows.Add(dataRow);
                }
                CalcGrid.DataSource = calcTable;
                CalcGrid.DataBind();
               // ViewState["CalcRoleMapping"] = CalcGrid;
                #endregion
                
                
                #region
                foreach (var obj in Indicators)
                {
                    DataRow dataRow = indicatorTable.NewRow();
                    IndicatorsAndRolesMappingTable roleAndIndMapping =
                        (from a in kpiWebDataContext.IndicatorsAndRolesMappingTable
                         where a.FK_Indicators == obj.IndicatorsTableID
                         && a.FK_RolesTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                         select a).FirstOrDefault();
                    if (roleAndIndMapping != null)
                    {
                        dataRow["ViewChecked2"] = roleAndIndMapping.CanView;
                        dataRow["VerifyChecked2"] = roleAndIndMapping.CanConfirm;
                    }
                    else
                    {
                        dataRow["ViewChecked2"] = false;
                        dataRow["VerifyChecked2"] = false;
                    }
                    dataRow["IndID"] = obj.IndicatorsTableID.ToString();
                    dataRow["Name2"] = obj.Name;
                    indicatorTable.Rows.Add(dataRow);
                }
                IndicatorGrid.DataSource = indicatorTable;
                IndicatorGrid.DataBind();
               // ViewState["IndRoleMapping"] = IndicatorGrid;
                #endregion                                   
            }      
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            RefreshGridView();
        }
        protected void Button1_Click(object sender, EventArgs e)//прост добавляем роль
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            RolesTable role = new RolesTable();

            if (CheckBox1.Checked) role.Active = true;
            else role.Active = false;

            if (CheckBox2.Checked) role.IsHead = true;
            else role.IsHead = false;

            role.RoleName = TextBox1.Text;
            kPiDataContext.RolesTable.InsertOnSubmit(role);
            kPiDataContext.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Роль добавлена');", true);
           // Response.Redirect();
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            CalcGrid.DataSource = null;
            CalcGrid.DataBind();

            IndicatorGrid.DataSource = null;
            IndicatorGrid.DataBind();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
             int currentRoleId = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                #region
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                        CheckBox canEdit = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxCanEdit");
                        CheckBox canView = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxCanView");
                        CheckBox canConfirm = (CheckBox)GridView1.Rows[i].FindControl("CheckBoxVerify");
                        Label label = (Label)GridView1.Rows[i].FindControl("Label2");

                        BasicParametersAndRolesMappingTable BasicAndRole = 
                            (from a in kpiWebDataContext.BasicParametersAndRolesMappingTable
                                                  where a.Active == true
                                                  && a.FK_RolesTable==currentRoleId
                                                  && a.FK_BasicParametersTable == Convert.ToInt32(label.Text)
                                                  select a).FirstOrDefault();
                        if (BasicAndRole != null)
                        {
                            BasicAndRole.CanConfirm = canConfirm.Checked;
                            BasicAndRole.CanEdit = canEdit.Checked;
                            BasicAndRole.CanView = canView.Checked;
                            kpiWebDataContext.SubmitChanges();
                        }
                        else if ((canConfirm.Checked) || (canView.Checked) || (canEdit.Checked))
                        {
                            BasicAndRole = new BasicParametersAndRolesMappingTable();
                            BasicAndRole.FK_BasicParametersTable = Convert.ToInt32(label.Text);
                            BasicAndRole.FK_RolesTable = currentRoleId;
                            BasicAndRole.Active = true;
                            BasicAndRole.CanConfirm = canConfirm.Checked;
                            BasicAndRole.CanEdit = canEdit.Checked;
                            BasicAndRole.CanView = canView.Checked;
                            kpiWebDataContext.BasicParametersAndRolesMappingTable.InsertOnSubmit(BasicAndRole);
                            kpiWebDataContext.SubmitChanges();
                        }                                              
            }
#endregion
             RolesTable role = (from a in kpiWebDataContext.RolesTable
                where a.RolesTableID == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                select a).FirstOrDefault();

            if ((bool) role.IsHead)
            {
                #region
                for (int i = 0; i < CalcGrid.Rows.Count; i++)
                {
                    //CheckBox canEdit = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxCanEdit");
                    CheckBox canView = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxCanView1");
                    CheckBox canConfirm = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxVerify1");
                    Label label = (Label)CalcGrid.Rows[i].FindControl("Label3");

                    CalculatedParametrsAndRolesMappingTable CalcAndRole =
                        (from a in kpiWebDataContext.CalculatedParametrsAndRolesMappingTable
                         where a.Active == true
                         && a.FK_RolesTable == currentRoleId
                         && a.FK_CalculatedParametrs == Convert.ToInt32(label.Text)
                         select a).FirstOrDefault();
                    if (CalcAndRole != null)
                    {
                        CalcAndRole.CanConfirm = canConfirm.Checked;
                       // BasicAndRole.CanEdit = canEdit.Checked;
                        CalcAndRole.CanView = canView.Checked;
                        kpiWebDataContext.SubmitChanges();
                    }
                    else if ((canConfirm.Checked) || (canView.Checked))
                    {
                        CalcAndRole = new CalculatedParametrsAndRolesMappingTable();
                        CalcAndRole.FK_CalculatedParametrs = Convert.ToInt32(label.Text);
                        CalcAndRole.FK_RolesTable = currentRoleId;
                        CalcAndRole.Active = true;
                        CalcAndRole.CanConfirm = canConfirm.Checked;
                        //CalcAndRole.CanEdit = canEdit.Checked;
                        CalcAndRole.CanView = canView.Checked;
                        kpiWebDataContext.CalculatedParametrsAndRolesMappingTable.InsertOnSubmit(CalcAndRole);
                        kpiWebDataContext.SubmitChanges();
                    }
                }
                #endregion
                #region
                for (int i = 0; i < IndicatorGrid.Rows.Count; i++)
                {
                    //CheckBox canEdit = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxCanEdit");
                    CheckBox canView = (CheckBox)IndicatorGrid.Rows[i].FindControl("CheckBoxCanView2");
                    CheckBox canConfirm = (CheckBox)IndicatorGrid.Rows[i].FindControl("CheckBoxVerify2");
                    Label label = (Label)IndicatorGrid.Rows[i].FindControl("Label5");

                    IndicatorsAndRolesMappingTable IndAndRole =
                        (from a in kpiWebDataContext.IndicatorsAndRolesMappingTable
                         where a.Active == true
                         && a.FK_RolesTable == currentRoleId
                         && a.FK_Indicators == Convert.ToInt32(label.Text)
                         select a).FirstOrDefault();
                    if (IndAndRole != null)
                    {
                        IndAndRole.CanConfirm = canConfirm.Checked;
                        // BasicAndRole.CanEdit = canEdit.Checked;
                        IndAndRole.CanView = canView.Checked;
                        kpiWebDataContext.SubmitChanges();
                    }
                    else if ((canConfirm.Checked) || (canView.Checked))
                    {
                        IndAndRole = new IndicatorsAndRolesMappingTable();
                        IndAndRole.FK_Indicators = Convert.ToInt32(label.Text);
                        IndAndRole.FK_RolesTable = currentRoleId;
                        IndAndRole.Active = true;
                        IndAndRole.CanConfirm = canConfirm.Checked;
                        //CalcAndRole.CanEdit = canEdit.Checked;
                        IndAndRole.CanView = canView.Checked;
                        kpiWebDataContext.IndicatorsAndRolesMappingTable.InsertOnSubmit(IndAndRole);
                        kpiWebDataContext.SubmitChanges();
                    }
                }
                #endregion
            }

            //int rowIndex = 0;
            //StringCollection sc = new StringCollection();

           // if (ViewState["GridviewRoleMapping"] != null)
           // {
            /*
                int currentRoleId = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                Dictionary<int, double> tempDictionary = new Dictionary<int, double>();
                DataTable roleBasicParametrs = (DataTable)ViewState["GridviewRoleMapping"];

                if (roleBasicParametrs.Rows.Count > 0)
                {
                    for (int i = 1; i <= roleBasicParametrs.Rows.Count; i++)
                    {
                        
                        rowIndex++;
                    }
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данные успешно сохранены');", true);
              //  }
             */
          //  }
            Response.Redirect("~/AutomationDepartment/AddRole.aspx");
        }    
    }
}