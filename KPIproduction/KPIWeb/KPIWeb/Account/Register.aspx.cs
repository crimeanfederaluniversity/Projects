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
        protected void CreateUser_Click(object sender, EventArgs e)
        {   
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
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

            user.AccessLevel = 10;

            kPiDataContext.UsersTable.InsertOnSubmit(user);
            kPiDataContext.SubmitChanges();   //// ПОЛЬЗОВАТЕЛЬ СОЗДАН

            int userID = user.UsersTableID;

            KPIWebDataContext kPiDataContext1 = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            List<UsersTable> UsersTable_ = (from item in kPiDataContext1.UsersTable
                                            where item.Login == UserName.Text select item).ToList();
            int ID_=-1;
            foreach (var item in UsersTable_)
                ID_=item.UsersTableID;
            ////////УЗНАЛИ ИД ПОЛЬЗОВАТЕЛЯ
            int i = 0;
            foreach (ListItem  Li in CheckBoxList1.Items)
            { 
                if (Li.Selected)
                {
                    UsersAndRolesMappingTable UserAndRoleMap = new UsersAndRolesMappingTable();
                    UserAndRoleMap.Active = true;
                    selectedValue = -1;
                   // if (int.TryParse(CheckBoxList1.SelectedValue, out selectedValue) && selectedValue > 0)

                   // UserAndRoleMap.FK_RolesTable = connect_[i];

                    UserAndRoleMap.FK_RolesTable = int.Parse(CheckBoxList1.Items[i].Value);

                    UserAndRoleMap.FK_UsersTable = ID_;
                    kPiDataContext1.UsersAndRolesMappingTable.InsertOnSubmit(UserAndRoleMap);
                    
                }
                i++;
            }
            kPiDataContext1.SubmitChanges();

            ///////////////////////////////////////////шаблон//////////////////////////////////
            int rowIndex = 0;

            if (ViewState["GridviewRoleMapping"] != null)
            {
                int currentRoleId = Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value);
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                DataTable roleBasicParametrs = (DataTable)ViewState["GridviewRoleMapping"];

                if (roleBasicParametrs.Rows.Count > 0)
                {
                    for (int k = 1; k <= roleBasicParametrs.Rows.Count; k++)
                    {
                        CheckBox canEdit = (CheckBox)GridviewRoles.Rows[rowIndex].FindControl("CheckBoxCanEdit");
                        CheckBox canView = (CheckBox)GridviewRoles.Rows[rowIndex].FindControl("CheckBoxCanView");
                        CheckBox canConfirm = (CheckBox)GridviewRoles.Rows[rowIndex].FindControl("CheckBoxVerify");
                        Label label = (Label)GridviewRoles.Rows[rowIndex].FindControl("Label2");

                        BasicParametrsAndUsersMapping BasicAndUsers = new BasicParametrsAndUsersMapping();

                       
                            BasicAndUsers.Active = true;
                            BasicAndUsers.FK_ParametrsTable = Convert.ToInt32(label.Text);
                            BasicAndUsers.CanConfirm = canConfirm.Checked;
                            BasicAndUsers.CanEdit = canEdit.Checked;
                            BasicAndUsers.CanView = canView.Checked;
                            BasicAndUsers.FK_UsersTable = userID;

                            KPIWebDataContext.BasicParametrsAndUsersMapping.InsertOnSubmit(BasicAndUsers);
                            KPIWebDataContext.SubmitChanges();

                       /* if (BasicAndRole != null)
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
                        }*/


                        rowIndex++;
                    }
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Пользователь зарегестрирован');", true);
                }
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
                List<SecondLevelSubdivisionTable> second_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable
                                                       where item.FK_FirstLevelSubdivisionTable== SelectedValue
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
            }
        }    
        protected void Page_Load(object sender, EventArgs e)
        {
           
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            int UserId = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));

            List<RolesTable> UserRoles = (from a in kPiDataContext.UsersAndRolesMappingTable
                                          join b in kPiDataContext.RolesTable
                                          on a.FK_RolesTable equals b.RolesTableID
                                          where a.FK_UsersTable == UserId && b.Active == true
                                          select b).ToList();

            if (!Page.IsPostBack)
            {
                List<RolesTable> Roles = (from a in kPiDataContext.RolesTable
                                          where a.Active == true
                                          select a).ToList();
                int i = 0;
                foreach (RolesTable role in Roles)
                {
                    DropDownList4.Items.Add(role.RoleName);
                    DropDownList4.Items[i].Value = role.RolesTableID.ToString();
                    i++;
                }
            }

          

            UsersTable userTable = (from a in kPiDataContext.UsersTable where a.UsersTableID == UserId select a).FirstOrDefault();
  

            foreach (RolesTable Role in UserRoles)
            {
                if (userTable.AccessLevel != 10) //нельзя давать пользователю роли и заполняющего и админа 
                {
                    Response.Redirect("~/Account/Login.aspx");
                }              
            }
          
            if (!Page.IsPostBack)
            {
              
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


                List<RolesTable> RolesTableList =
                    (from item in kPiDataContext.RolesTable select item).OrderBy(mc => mc.RoleName).ToList();

                var dictionary_roles = new Dictionary<int, string>();
                dictionary_roles.Add(0, "Выберите значение");

                int i = 0;
                foreach (var item in RolesTableList)
                {
                    CheckBoxList1.Items.Add(item.RoleName);
                    CheckBoxList1.Items[i].Value = item.RolesTableID.ToString();
                    i++;                                  
                }
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
            }
        }
        protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RefreshGridView() // стягиваем с базы в грид с проставленными галочками основываясь на дроп дауне
        {
            GridviewRoles.DataSource = null;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            var vrCountry = (from b in kpiWebDataContext.BasicParametersTable select b);

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
                     where a.FK_BasicParametersTable == obj.BasicParametersTableID
                     && a.FK_RolesTable == Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value)
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
            GridviewRoles.DataSource = dataTable;
            GridviewRoles.DataBind();
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshGridView();
        }
    }

}