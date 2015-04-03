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
    public partial class ChangeUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();


            ////////////////////////////////////////////////////////
            /// 
            if (!Page.IsPostBack)
            {
                List<UsersTable> users = (from a in kPiDataContext.UsersTable
                    where a.UsersTableID == 8132
                    select a).ToList();

                RefreshGridView();
            }

        }

        private void RefreshGridView() // стягиваем с базы в грид с проставленными галочками 
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                    
            Serialization ser =  (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("~/AutomationDepartment/EditUser.aspx");
            }
            int userToChangeId = ser.Id;
            var vrCountry = (from b in kpiWebDataContext.BasicParametersTable select b);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("VerifyChecked", typeof (bool)));
            dataTable.Columns.Add(new DataColumn("EditChecked", typeof (bool)));
            dataTable.Columns.Add(new DataColumn("ViewChecked", typeof (bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
            dataTable.Columns.Add(new DataColumn("BasicId", typeof (string)));
            UsersTable users = (from a in kpiWebDataContext.UsersTable
                where a.UsersTableID == userToChangeId
                select a).FirstOrDefault();

            #region

            foreach (var obj in vrCountry)
            {
                DataRow dataRow = dataTable.NewRow();
                BasicParametrsAndUsersMapping userAndBasicMapping =
                    (from a in kpiWebDataContext.BasicParametrsAndUsersMapping
                     where a.FK_UsersTable == userToChangeId
                        && a.FK_ParametrsTable == obj.BasicParametersTableID
                        select a).FirstOrDefault();
                if (userAndBasicMapping != null)
                {
                    dataRow["EditChecked"] = userAndBasicMapping.CanEdit;
                    dataRow["ViewChecked"] = userAndBasicMapping.CanView;
                    dataRow["VerifyChecked"] = userAndBasicMapping.CanConfirm;
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

            // if ((bool)role.IsHead)
            // {
            var calcParam = (from a in kpiWebDataContext.CalculatedParametrs select a);
            var Indicators = (from c in kpiWebDataContext.IndicatorsTable select c);

            DataTable calcTable = new DataTable();
            calcTable.Columns.Add(new DataColumn("VerifyChecked1", typeof (bool)));
            calcTable.Columns.Add(new DataColumn("ViewChecked1", typeof (bool)));
            calcTable.Columns.Add(new DataColumn("Name1", typeof (string)));
            calcTable.Columns.Add(new DataColumn("CalcID", typeof (string)));

            DataTable indicatorTable = new DataTable();
            indicatorTable.Columns.Add(new DataColumn("VerifyChecked2", typeof (bool)));
            indicatorTable.Columns.Add(new DataColumn("ViewChecked2", typeof (bool)));
            indicatorTable.Columns.Add(new DataColumn("Name2", typeof (string)));
            indicatorTable.Columns.Add(new DataColumn("IndID", typeof (string)));


            #region

            foreach (var obj in calcParam)
            {
                DataRow dataRow = calcTable.NewRow();
                CalculatedParametrsAndUsersMapping userAndcalculatedparametrs =
                    (from a in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                        where a.FK_CalculatedParametrsTable == obj.CalculatedParametrsID
                              && a.FK_UsersTable == userToChangeId
                        select a).FirstOrDefault();
                if (userAndcalculatedparametrs != null)
                {
                    dataRow["ViewChecked1"] = userAndcalculatedparametrs.CanView;
                    dataRow["VerifyChecked1"] = userAndcalculatedparametrs.CanConfirm;
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

            #endregion


            #region

            foreach (var obj in Indicators)
            {
                DataRow dataRow = indicatorTable.NewRow();
                IndicatorsAndUsersMapping userAndIndMapping =
                    (from a in kpiWebDataContext.IndicatorsAndUsersMapping
                        where a.FK_IndicatorsTable == obj.IndicatorsTableID
                              && a.FK_UsresTable == userToChangeId
                        select a).FirstOrDefault();
                if (userAndIndMapping != null)
                {
                    dataRow["ViewChecked2"] = userAndIndMapping.CanView;
                    dataRow["VerifyChecked2"] = userAndIndMapping.CanConfirm;
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

      /*  protected void Button1_Click(object sender, EventArgs e)
        {
            RefreshGridView();
        }*/

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization ser = (Serialization)Session["userIdforChange"];
            if (ser == null)
            {
                Response.Redirect("~/AutomationDepartment/EditUser.aspx");
            }
            int userToChangeId = ser.Id;
            int currentuserId = userToChangeId;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                CheckBox canEdit = (CheckBox) GridView1.Rows[i].FindControl("CheckBoxCanEdit");
                CheckBox canView = (CheckBox) GridView1.Rows[i].FindControl("CheckBoxCanView");
                CheckBox canConfirm = (CheckBox) GridView1.Rows[i].FindControl("CheckBoxVerify");
                Label label = (Label) GridView1.Rows[i].FindControl("Label2");

                BasicParametrsAndUsersMapping BasicAndUser =
                    (from a in kpiWebDataContext.BasicParametrsAndUsersMapping
                        where a.Active == true
                              && a.FK_UsersTable == currentuserId
                              && a.FK_ParametrsTable == Convert.ToInt32(label.Text)
                        select a).FirstOrDefault();
                if (BasicAndUser != null)
                {
                    BasicAndUser.CanConfirm = canConfirm.Checked;
                    BasicAndUser.CanEdit = canEdit.Checked;
                    BasicAndUser.CanView = canView.Checked;
                    kpiWebDataContext.SubmitChanges();
                }
                else if ((canConfirm.Checked) || (canView.Checked) || (canEdit.Checked))
                {
                    BasicAndUser = new BasicParametrsAndUsersMapping();
                    BasicAndUser.FK_ParametrsTable = Convert.ToInt32(label.Text);
                    BasicAndUser.FK_UsersTable = currentuserId;
                    BasicAndUser.Active = true;
                    BasicAndUser.CanConfirm = canConfirm.Checked;
                    BasicAndUser.CanEdit = canEdit.Checked;
                    BasicAndUser.CanView = canView.Checked;
                    kpiWebDataContext.BasicParametrsAndUsersMapping.InsertOnSubmit(BasicAndUser);
                    kpiWebDataContext.SubmitChanges();
                }
            }
            for (int i = 0; i < CalcGrid.Rows.Count; i++)
            {
                //CheckBox canEdit = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxCanEdit");
                CheckBox canView = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxCanView1");
                CheckBox canConfirm = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxVerify1");
                Label label = (Label)CalcGrid.Rows[i].FindControl("Label3");

                CalculatedParametrsAndUsersMapping CalcAndUser =
                    (from a in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                     where a.Active == true
                     && a.FK_UsersTable == userToChangeId
                     && a.FK_CalculatedParametrsTable == Convert.ToInt32(label.Text)
                     select a).FirstOrDefault();
                if (CalcAndUser != null)
                {
                    CalcAndUser.CanConfirm = canConfirm.Checked;
                    // BasicAndRole.CanEdit = canEdit.Checked;
                    CalcAndUser.CanView = canView.Checked;
                    kpiWebDataContext.SubmitChanges();
                }
                else if ((canConfirm.Checked) || (canView.Checked))
                {
                    CalcAndUser = new CalculatedParametrsAndUsersMapping();
                    CalcAndUser.FK_CalculatedParametrsTable = Convert.ToInt32(label.Text);
                    CalcAndUser.FK_UsersTable = userToChangeId;
                    CalcAndUser.Active = true;
                    CalcAndUser.CanConfirm = canConfirm.Checked;
                    //CalcAndRole.CanEdit = canEdit.Checked;
                    CalcAndUser.CanView = canView.Checked;
                    kpiWebDataContext.CalculatedParametrsAndUsersMapping.InsertOnSubmit(CalcAndUser);
                    kpiWebDataContext.SubmitChanges();
                }
            }
            for (int i = 0; i < IndicatorGrid.Rows.Count; i++)
            {
                //CheckBox canEdit = (CheckBox)CalcGrid.Rows[i].FindControl("CheckBoxCanEdit");
                CheckBox canView = (CheckBox)IndicatorGrid.Rows[i].FindControl("CheckBoxCanView2");
                CheckBox canConfirm = (CheckBox)IndicatorGrid.Rows[i].FindControl("CheckBoxVerify2");
                Label label = (Label)IndicatorGrid.Rows[i].FindControl("Label5");

                IndicatorsAndUsersMapping IndAnduser =
                    (from a in kpiWebDataContext.IndicatorsAndUsersMapping
                     where a.Active == true
                     && a.FK_UsresTable == userToChangeId
                     && a.FK_IndicatorsTable == Convert.ToInt32(label.Text)
                     select a).FirstOrDefault();
                if (IndAnduser != null)
                {
                    IndAnduser.CanConfirm = canConfirm.Checked;
                    // BasicAndRole.CanEdit = canEdit.Checked;
                    IndAnduser.CanView = canView.Checked;
                    kpiWebDataContext.SubmitChanges();
                }
                else if ((canConfirm.Checked) || (canView.Checked))
                {
                    IndAnduser = new IndicatorsAndUsersMapping();
                    IndAnduser.FK_IndicatorsTable = Convert.ToInt32(label.Text);
                    IndAnduser.FK_UsresTable = userToChangeId;
                    IndAnduser.Active = true;
                    IndAnduser.CanConfirm = canConfirm.Checked;
                    //CalcAndRole.CanEdit = canEdit.Checked;
                    IndAnduser.CanView = canView.Checked;
                    kpiWebDataContext.IndicatorsAndUsersMapping.InsertOnSubmit(IndAnduser);
                    kpiWebDataContext.SubmitChanges();
                }
            }
            Response.Redirect("~/AutomationDepartment/EditUser.aspx");
        }

        /* protected void Button2_Click(object sender, EventArgs e)
        {
            RefreshGridView();
        }
        protected void Button1_Click(object sender, EventArgs e)//прост добавляем роль
        {/*
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
           // Response.Redirect();*/
        }
       /* protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            CalcGrid.DataSource = null;
            CalcGrid.DataBind();

            IndicatorGrid.DataSource = null;
            IndicatorGrid.DataBind();
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
             int currentRoleId = 8132
                #region
            for ( int i = 0; i < GridView1.Rows.Count; i++)
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
            }*/

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
            // Response.Redirect("~/AutomationDepartment/AddRole.aspx");
        }
    //}