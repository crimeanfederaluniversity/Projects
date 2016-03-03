using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.PersonalPagesAdmin
{
    public partial class CreatePersonalPage : System.Web.UI.Page
    {

        public void ChangeView()
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            if (DropDownList4.SelectedIndex == 0)
            {
                Label5.Text = "Должность";
                Label6.Text = "Ученая степень";
                List<ThirdLevelSubdivisionTable> Third_stageList = (from item in kPiDataContext.ThirdLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                var dictionary3 = new Dictionary<int, string>();
                dictionary3.Add(-1, "Выберите кафедру");
                foreach (var item in Third_stageList)
                {
                    dictionary3.Add(item.ThirdLevelSubdivisionTableID, item.Name);

                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary3;
                    DropDownList3.DataBind();
                }
            }
            else
            {
                Label5.Text = "Курс";
                Label6.Text = "Год поступления";
                List<StudentGroupsTable> Third_stageList = (from item in kPiDataContext.StudentGroupsTable select item).OrderBy(mc => mc.Name).ToList();
                var dictionary3 = new Dictionary<int, string>();
                dictionary3.Add(-1, "Выберите группу");
                foreach (var item in Third_stageList)
                {
                    dictionary3.Add(item.ID, item.Name);

                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary3;
                    DropDownList3.DataBind();
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 19, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            if (!(Page.IsPostBack))
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof(int)));
                dataTable.Columns.Add(new DataColumn("Access", typeof(bool)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                List<UserGroupTable> groups = (from a in kPiDataContext.UserGroupTable where a.Active == true select a).Distinct().ToList();
                foreach (var grouped in groups)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = grouped.UserGroupID;
                    dataRow["Name"] = grouped.UserGroupName;
                    dataTable.Rows.Add(dataRow);
                }

                GridView1.DataSource = dataTable;
                GridView1.DataBind();

                ChangeView();



                List<FirstLevelSubdivisionTable> First_stageList = (from item in kPiDataContext.FirstLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();
                List<SecondLevelSubdivisionTable> Second_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable select item).OrderBy(mc => mc.Name).ToList();


                var dictionary = new Dictionary<int, string>();
                dictionary.Add(-1, "Выберите академию");
                var dictionary2 = new Dictionary<int, string>();
                dictionary2.Add(-1, "Выберите факультет");


                foreach (var item in First_stageList)
                {
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

                    DropDownList1.DataTextField = "Value";
                    DropDownList1.DataValueField = "Key";
                    DropDownList1.DataSource = dictionary;
                    DropDownList1.DataBind();
                }
                foreach (var item in Second_stageList)
                {
                    dictionary2.Add(item.SecondLevelSubdivisionTableID, item.Name);

                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary2;
                    DropDownList2.DataBind();
                }


            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            if ((from a in kPiDataContext.UsersTable where a.Email == EmailText.Text && a.Active == true select a).ToList().Count > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Введенный адрес электронной почты уже зарегестрирован, введите другой');", true);
            }
            else
            {
                UsersTable user = new UsersTable();
                user.Active = true;
                user.Surname = Surname.Text;
                user.Name = Name.Text;
                user.Patronimyc = Patronimyc.Text;
                if (Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value) != -1)
                {
                    user.FK_FirstLevelSubdivisionTable = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                }
                if (Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value) != -1)
                {
                    user.FK_SecondLevelSubdivisionTable = Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value);
                }
                user.Data_Status = true;
                user.Email = EmailText.Text;
                user.Login = "";
                user.Password = "";
                if (DropDownList4.SelectedIndex == 0)
                {
                    user.AccessLevel = 0;
                    user.Position = Textbox1.Text;
                    user.AcademicDegree = Textbox2.Text;
                    if (Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value) != -1)
                    {
                        user.FK_ThirdLevelSubdivisionTable = Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value);
                    }
                }
                else
                {
                    user.AccessLevel = Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value);
                    user.Kurs = Convert.ToInt32(Textbox1.Text);
                    user.YearEnter = Convert.ToInt32(Textbox2.Text);
                    if (Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value) != -1)
                    {
                        user.FK_StudentGroup = Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value);
                    }
                }
                kPiDataContext.UsersTable.InsertOnSubmit(user);
                kPiDataContext.SubmitChanges();
                for (int i = 1; i < GridView1.Rows.Count; i++)
                {
                    CheckBox confirmed = (CheckBox)GridView1.Rows[i].FindControl("Access");
                    Label label = (Label)GridView1.Rows[i].FindControl("ID");
                    if (confirmed.Checked == true)
                    {
                        UsersAndUserGroupMappingTable newuseraccess = new UsersAndUserGroupMappingTable();
                        newuseraccess.Active = true;
                        newuseraccess.Confirmed = true;
                        newuseraccess.FK_GroupTable = Convert.ToInt32(label.Text);
                        newuseraccess.FK_UserTable = user.UsersTableID;
                        kPiDataContext.UsersAndUserGroupMappingTable.InsertOnSubmit(newuseraccess);
                        kPiDataContext.SubmitChanges();
                    }
                }
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Пользователь добавлен!');", true);
            }
        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeView();
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int SelectedValue = -1;

            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList = (from item in kPiDataContext.SecondLevelSubdivisionTable
                                                                      where item.FK_FirstLevelSubdivisionTable == SelectedValue
                                                                      select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();
                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();

                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList2.Enabled = true;
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
                }
            }

        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            int SelectedValue = -1;

            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                Serialization UserSer = (Serialization)Session["UserID"];
                if (UserSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                int userID = UserSer.Id;
                ViewState["ID"] = userID;

                UsersTable userTable =
                    (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
                if (DropDownList4.SelectedIndex == 0)
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

                        DropDownList3.Enabled = true;
                        DropDownList3.DataTextField = "Value";
                        DropDownList3.DataValueField = "Key";
                        DropDownList3.DataSource = dictionary;
                        DropDownList3.DataBind();
                    }
                }
                else
                {
                    List<StudentGroupsTable> group_stage = (from item in kPiDataContext.StudentGroupsTable
                                                            where item.FK_SecondLevel == SelectedValue
                                                            select item).OrderBy(mc => mc.ID).ToList();
                    if (group_stage != null && group_stage.Count() > 0)
                    {
                        var dictionary = new Dictionary<int, string>();

                        dictionary.Add(-1, "Выберите значение");

                        foreach (var item in group_stage)
                            dictionary.Add(item.ID, item.Name);

                        DropDownList3.Enabled = true;
                        DropDownList3.DataTextField = "Value";
                        DropDownList3.DataValueField = "Key";
                        DropDownList3.DataSource = dictionary;
                        DropDownList3.DataBind();
                    }
                }
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PersonalPagesAdmin/PersonalMainPage.aspx");
        }
    }

}
