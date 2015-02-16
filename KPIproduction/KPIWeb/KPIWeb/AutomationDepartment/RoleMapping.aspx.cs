using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;

namespace KPIWeb
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        int indexkorel = 1;
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
            //////////////////////////////////////////////////////////////////////////////////////////////////
            if (!IsPostBack)
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                List<string> role = new List<string>();

                role = (from roles in kpiWebDataContext.RolesTable
                    select roles.RoleName).ToList();

                foreach (string name in role)
                {
                    DropDownList1.Items.Add(name);
                }

            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
            Button1.Visible = true;
        }

        private void RefreshGridView()
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            var vrCountry = (from a in kpiWebDataContext.BasicParametersTable select a).Except(
                from b in kpiWebDataContext.BasicParametersTable
                join c in kpiWebDataContext.BasicParametersAndRolesMappingTable on b.BasicParametersTableID equals
                    c.FK_BasicParametersTable
                select b);

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("EditChecked", typeof (bool)));
            dataTable.Columns.Add(new DataColumn("ViewChecked", typeof (bool)));
            dataTable.Columns.Add(new DataColumn("Name", typeof (string)));

            int i = 1;

            foreach (var obj in vrCountry)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["EditChecked"] = false;
                dataRow["ViewChecked"] = false;
                dataRow["Name"] = " " + obj.BasicParametersTableID + ". " + obj.Name;
                dataTable.Rows.Add(dataRow);
                i++;
            }

            ViewState["GridviewRoleMapping"] = dataTable;
            GridviewRoles.DataSource = dataTable;
            GridviewRoles.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {           
            int rowIndex = 0;
            KPIWebDataContext kpiWebDataContext2 = new KPIWebDataContext();
            var vrCountry = (from a in kpiWebDataContext2.BasicParametersTable select a).Except(
                                   from b in kpiWebDataContext2.BasicParametersTable
                                   join c in kpiWebDataContext2.BasicParametersAndRolesMappingTable on
                                   b.BasicParametersTableID equals c.FK_BasicParametersTable
                                   select b).ToList();

            var vrRoleName = (from a in kpiWebDataContext2.RolesTable
                              where a.RoleName == DropDownList1.SelectedValue.ToString()
                              select new { a.RoleName, a.RolesTableID }).ToList();

            if (ViewState["GridviewRoleMapping"] != null)
            {
                DataTable dataTable = (DataTable) ViewState["GridviewRoleMapping"];

                if (dataTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dataTable.Rows.Count; i++)
                    {
                        CheckBox checkBoxCanEdit = (CheckBox)GridviewRoles.Rows[rowIndex].FindControl("CheckBoxCanEdit");
                        CheckBox checkBoxCanView = (CheckBox)GridviewRoles.Rows[rowIndex].FindControl("CheckBoxCanView");

                        if (checkBoxCanView != null && checkBoxCanEdit != null)
                        {
                            if (checkBoxCanEdit.Checked == true || checkBoxCanView.Checked == true)
                            {

                                using (KPIWebDataContext kpiWebDataContext = new KPIWebDataContext())
                                {
                                    BasicParametersAndRolesMappingTable bparmTables = new BasicParametersAndRolesMappingTable();
                                    bparmTables.Active = true;
                                    bparmTables.FK_RolesTable = vrRoleName[0].RolesTableID;
                                    bparmTables.FK_BasicParametersTable = vrCountry[rowIndex].BasicParametersTableID;
                                    indexkorel++;
                                    if (checkBoxCanEdit.Checked == true)
                                        bparmTables.CanEdit = true;
                                    else
                                    {
                                        bparmTables.CanEdit = false;
                                    }
                                    if (checkBoxCanView.Checked == true)
                                        bparmTables.CanView = true;
                                    else
                                    {
                                        bparmTables.CanView = false;
                                    }
                                    kpiWebDataContext.BasicParametersAndRolesMappingTable.InsertOnSubmit(bparmTables);
                                    kpiWebDataContext.SubmitChanges();
                                }
                            }
                            rowIndex++;
                        }
                    } 
               }
           }   
           RefreshGridView();
        }
    }
}