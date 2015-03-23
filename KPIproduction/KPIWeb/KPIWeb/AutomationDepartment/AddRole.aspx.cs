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
                Response.Redirect("~/Default.aspx");
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
            var vrCountry = ( from b in kpiWebDataContext.BasicParametersTable select b);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("VerifyChecked", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("EditChecked", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("ViewChecked", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
            dataTable.Columns.Add(new DataColumn("BasicId", typeof(string)));
            int i = 1;

            foreach (var obj in vrCountry)
            {
                DataRow dataRow = dataTable.NewRow();
                BasicParametersAndRolesMappingTable roleAndBasicMapping =
                    (from a in kpiWebDataContext.BasicParametersAndRolesMappingTable 
                     where a.FK_BasicParametersTable==obj.BasicParametersTableID
                     && a.FK_RolesTable== Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
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
                i++;
            }
            ViewState["GridviewRoleMapping"] = dataTable;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();
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
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            StringCollection sc = new StringCollection();

            if (ViewState["GridviewRoleMapping"] != null)
            {
                int currentRoleId = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                Dictionary<int, double> tempDictionary = new Dictionary<int, double>();
                DataTable roleBasicParametrs = (DataTable)ViewState["GridviewRoleMapping"];

                if (roleBasicParametrs.Rows.Count > 0)
                {
                    for (int i = 1; i <= roleBasicParametrs.Rows.Count; i++)
                    {
                        CheckBox canEdit = (CheckBox)GridView1.Rows[rowIndex].FindControl("CheckBoxCanEdit");
                        CheckBox canView = (CheckBox)GridView1.Rows[rowIndex].FindControl("CheckBoxCanView");
                        CheckBox canConfirm = (CheckBox)GridView1.Rows[rowIndex].FindControl("CheckBoxVerify");
                        Label label = (Label)GridView1.Rows[rowIndex].FindControl("Label2");

                        BasicParametersAndRolesMappingTable BasicAndRole = 
                            (from a in KPIWebDataContext.BasicParametersAndRolesMappingTable
                                                  where a.Active == true
                                                  && a.FK_RolesTable==currentRoleId
                                                  && a.FK_BasicParametersTable == Convert.ToInt32(label.Text)
                                                  select a).FirstOrDefault();
                        if (BasicAndRole != null)
                        {
                            BasicAndRole.CanConfirm = canConfirm.Checked;
                            BasicAndRole.CanEdit = canEdit.Checked;
                            BasicAndRole.CanView = canView.Checked;
                            KPIWebDataContext.SubmitChanges();
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
                            KPIWebDataContext.BasicParametersAndRolesMappingTable.InsertOnSubmit(BasicAndRole);
                            KPIWebDataContext.SubmitChanges();
                        }
                        
                        
                        rowIndex++;
                    }
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данные успешно сохранены');", true);
                }
            }
            Response.Redirect("~/AutomationDepartment/AddRole.aspx");
        }
    
    
    }
}