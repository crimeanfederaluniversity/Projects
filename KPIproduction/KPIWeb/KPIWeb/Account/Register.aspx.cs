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
using System.Web.UI.WebControls;

namespace KPIWeb.Account
{
    public partial class Register : Page
    {

        protected void Button_click_add()
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

            selectedValue = -1;
            if (int.TryParse(DropDownList4.SelectedValue, out selectedValue) && selectedValue > 0)
                user.FK_RolesTable = selectedValue;   

            kPiDataContext.UsersTables.InsertOnSubmit(user);

            kPiDataContext.SubmitChanges();
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
           // var manager = new UserManager();
          //  var user = new ApplicationUser() { UserName = UserName.Text };
          //  IdentityResult result = manager.Create(user, Password.Text);
         //   if (result.Succeeded)
         //   {
                Button_click_add();    
               // IdentityHelper.SignIn(manager, user, isPersistent: false);
               // IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
          //  }
          ///  else 
          //  {
          //      ErrorMessage.Text = result.Errors.FirstOrDefault();
          //  }
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

                  //  DropDownListRoleStage2.Enabled = true;

                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
                }
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));

                List<FirstLevelSubdivisionTable> First_stageList = (from item in kPiDataContext.FirstLevelSubdivisionTable
                                                     select item).OrderBy(mc => mc.Name).ToList();

                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();

                List<RolesTable> RolesTableList = (from item in kPiDataContext.RolesTables select item).OrderBy(mc => mc.Name).ToList();

                var dictionary_roles = new Dictionary<int, string>();
                dictionary_roles.Add(0, "Выберите значение");

                foreach (var item in RolesTableList)
                    dictionary_roles.Add(item.RolesTableID, item.Name);

                DropDownList4.DataTextField = "Value";
                DropDownList4.DataValueField = "Key";
                DropDownList4.DataSource = dictionary_roles;
                DropDownList4.DataBind();
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

                  //  DropDownListRoleStage3.Enabled = true;

                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                }
            }
        }
    }
}