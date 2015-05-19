using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class Regisration : System.Web.UI.Page   
    {
        private string RandomString(int size)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
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
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
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
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);                   
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
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
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
            /// 
            /// в зависимости от того кто вошел изменяем интерфейс
                if (userTable.AccessLevel == 9)
                {
                    UserNameText.Enabled = false;
                    UserNameLabel.Enabled = false;
                    errorNoName.Enabled = false;
                    UserNameText.Visible = false;
                    UserNameLabel.Visible = false;
                    errorNoName.Visible = false;

                    PasswordText.Enabled = false;
                    PassLabel.Enabled = false;
                    errorNoPass.Enabled = false;
                    PasswordText.Visible = false;
                    PassLabel.Visible = false;
                    errorNoPass.Visible = false;

                    ConfirmPasswordText.Enabled = false;
                    ConfPassLabel.Enabled = false;
                    errorNoConfirm.Enabled = false;
                    ErrorWrongConfirm.Enabled = false;
                    ConfirmPasswordText.Visible = false;
                    ConfPassLabel.Visible = false;
                    errorNoConfirm.Visible = false;
                    ErrorWrongConfirm.Visible = false;                                                      
                }
            }
        }
        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {
          
        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
           // if 
        }
        protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

                Gridview1.Visible = false;
                Label25.Visible = false;
                Gridview2.Visible = false;
                Label26.Visible = false;
                Gridview3.Visible = false;
                Label27.Visible = false;
                DropDownList4.Visible = false;
                Label24.Visible = false;

                if (DropDownList5.SelectedIndex == 0 || DropDownList5.SelectedIndex == 3)
                {                  
                }
                else if (DropDownList5.SelectedIndex == 5)
                {
                  /*  Gridview1.Visible = true;
                    Label25.Visible = true;
                    Gridview2.Visible = true;
                    Label26.Visible = true;
                    Gridview3.Visible = true;
                    Label27.Visible = true;
                    DropDownList4.Visible = true;
                    Label24.Visible = true;*/
                }
                else if (DropDownList5.SelectedIndex == 1)
                {
                    List<RolesTable> Roles = (from a in kPiDataContext.RolesTable
                                              where a.Active == true
                                              && a.IsHead == false
                                              select a).ToList();
                    int i = 1;
                    DropDownList4.Items.Clear();
                    DropDownList4.Items.Add("Выберите шаблон");
                    foreach (RolesTable role in Roles)
                    {
                        DropDownList4.Items.Add(role.RoleName);
                        DropDownList4.Items[i].Value = role.RolesTableID.ToString();
                        i++;
                    }
                    DropDownList4.Visible = true;
                    Label24.Visible = true;
                    //////
                    if (userTable.AccessLevel == 10)
                     {
                        Gridview3.Visible = true;
                        Label27.Visible = true;
                        
                     }
                     else if (userTable.AccessLevel == 9)
                     {            
                     }             
                }
                else if (DropDownList5.SelectedIndex == 2)
                {
                    Gridview1.Visible = true;
                    Label25.Visible = true;
                    Gridview2.Visible = true;
                    Label26.Visible = true;
                    Gridview3.Visible = true;
                    Label27.Visible = true;

                    Gridview3.Columns[1].Visible = false;
                    Gridview3.Columns[3].Visible = false;


                    List<RolesTable> Roles = (from a in kPiDataContext.RolesTable
                                              where a.Active == true
                                              && a.IsHead == true
                                              select a).ToList();
                    int i = 1;
                    DropDownList4.Items.Clear();
                    DropDownList4.Items.Add("Выберите шаблон");
                    foreach (RolesTable role in Roles)
                    {
                        DropDownList4.Items.Add(role.RoleName);
                        DropDownList4.Items[i].Value = role.RolesTableID.ToString();
                        i++;
                    }
                    DropDownList4.Visible = true;
                    Label24.Visible = true;
                    ////записали роли в дроп даун
                }
        
            }    
        protected void Gridview3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void DropDownList2_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
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
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
            }
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            
            if (((from a in kPiDataContext.UsersTable where a.Login == UserNameText.Text select a).ToList().Count > 0)&&(UserNameText.Text.Length>2))
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Пользователь с таким логином уже существует, выберите другой логин!');", true);
            }

            else if ((from a in kPiDataContext.UsersTable where a.Email == EmailText.Text select a).ToList().Count > 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                    "alert('Введенный адрес электронной почты уже зарегестрирован, введите другой');", true);
            }
            else
            {
                #region check for user with the same basic parametrs allouence

                int match_cnt_sum = 0;
                int rowIndex = 0;
                if (Gridview3.Rows.Count > 0)
                {

                    for (int k = 1; k <= Gridview3.Rows.Count; k++)
                    {
                        CheckBox canEdit = (CheckBox) Gridview3.Rows[rowIndex].FindControl("BasicParametrsEditCheckBox");
                        CheckBox canConfirm = (CheckBox) Gridview3.Rows[rowIndex].FindControl("BasicParametrsConfirmCheckBox");
                        Label label = (Label) Gridview3.Rows[rowIndex].FindControl("BasicParametrsID");

                        //BasicParametrsAndUsersMapping BasicAndUsers = new BasicParametrsAndUsersMapping();                    
                        int first = 0;
                        int second = 0;
                        int third = 0;

                        if ((DropDownList1.SelectedIndex!=null)&&(DropDownList1.SelectedIndex > 0))
                        {
                            first = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
                        }

                        if ((DropDownList2.SelectedIndex!=null)&&(DropDownList2.SelectedIndex > 0))
                        {
                            second = Convert.ToInt32(DropDownList2.Items[DropDownList2.SelectedIndex].Value);
                        }

                        if ((DropDownList3.SelectedIndex!=null)&&(DropDownList3.SelectedIndex > 0))
                        {
                            third = Convert.ToInt32(DropDownList3.Items[DropDownList3.SelectedIndex].Value);
                        }

                        int match_cnt = (from a in kPiDataContext.BasicParametrsAndUsersMapping
                            join b in kPiDataContext.UsersTable
                                on a.FK_UsersTable equals b.UsersTableID
                            where
                                (((a.CanConfirm ==true)&&(canConfirm.Checked==true))||((a.CanEdit == true)&&(canEdit.Checked==true)))
                                && a.Active == true
                                && b.Active == true                               
                                && b.FK_ZeroLevelSubdivisionTable == 1
                                && a.FK_ParametrsTable == Convert.ToInt32(label.Text)
                                && ((b.FK_FirstLevelSubdivisionTable == first))// || (b.FK_FirstLevelSubdivisionTable == null))
                                && ((b.FK_SecondLevelSubdivisionTable == second))// || (b.FK_SecondLevelSubdivisionTable == null))
                                && ((b.FK_ThirdLevelSubdivisionTable == third))// || (b.FK_ThirdLevelSubdivisionTable == null))                               
                            select a).ToList().Count();
                        match_cnt_sum += match_cnt;
                        rowIndex++;
                    }
                }
                #endregion
                if (match_cnt_sum > 0)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                        "alert('Пользователь для заданного подразделения с " + match_cnt_sum +
                        " совпадениями возможностей редактирования поджтвержения существует');", true);
                }
                else
                {
                    UsersTable user = new UsersTable();
                    user.Active = true;
                    user.Login = UserNameText.Text;
                    user.Password = PasswordText.Text;
                    user.Email = EmailText.Text;

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
                        user.AccessLevel = 9;
                    }

                    user.FK_ZeroLevelSubdivisionTable = 1;
                    string passCode = RandomString(25);
                    user.PassCode = passCode;
                    user.Confirmed = false;                
                    kPiDataContext.UsersTable.InsertOnSubmit(user);
                    kPiDataContext.SubmitChanges();
                    //// ПОЛЬЗОВАТЕЛЬ СОЗДАН
                    ///                    

                    int userID = user.UsersTableID;
                    
                    ///////////////////////////////////////////шаблон//////////////////////////////////
                    rowIndex = 0;

                    if (Gridview3.Rows.Count > 0)
                    {
                        for (int k = 1; k <= Gridview3.Rows.Count; k++)
                        {
                            CheckBox canEdit =
                                (CheckBox) Gridview3.Rows[rowIndex].FindControl("BasicParametrsEditCheckBox");
                            CheckBox canView =
                                (CheckBox) Gridview3.Rows[rowIndex].FindControl("BasicParametrsViewCheckBox");
                            CheckBox canConfirm =
                                (CheckBox) Gridview3.Rows[rowIndex].FindControl("BasicParametrsConfirmCheckBox");
                            Label label = (Label) Gridview3.Rows[rowIndex].FindControl("BasicParametrsID");

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
                        if (DropDownList5.SelectedIndex == 2) // если человек руководитель
                        {
                            rowIndex = 0;
                            if (Gridview1.Rows.Count > 0)
                            {
                                for (int k = 1; k <= Gridview1.Rows.Count; k++)
                                {
                                    /*CheckBox canEdit =
                                        (CheckBox) Gridview1.Rows[rowIndex].FindControl("IndicatorEditCheckBox");*/
                                    CheckBox canView =
                                        (CheckBox) Gridview1.Rows[rowIndex].FindControl("IndicatorViewCheckBox");
                                    CheckBox canConfirm =
                                        (CheckBox) Gridview1.Rows[rowIndex].FindControl("IndicatorConfirmCheckBox");
                                    Label label = (Label) Gridview1.Rows[rowIndex].FindControl("IndicatorID");

                                    IndicatorsAndUsersMapping indAndUser = new IndicatorsAndUsersMapping();
                                    indAndUser.Active = true;
                                    indAndUser.FK_IndicatorsTable = Convert.ToInt32(label.Text);
                                    indAndUser.CanConfirm = canConfirm.Checked;
                                    //indAndUser.CanEdit = canEdit.Checked;
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
                                    /*CheckBox canEdit =
                                        (CheckBox)
                                            Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsEditCheckBox");*/
                                    CheckBox canView =
                                        (CheckBox)
                                            Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsViewCheckBox");
                                    CheckBox canConfirm =
                                        (CheckBox)
                                            Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsConfirmCheckBox");
                                    Label label = (Label) Gridview2.Rows[rowIndex].FindControl("CalculatedParametrsID");

                                    CalculatedParametrsAndUsersMapping calcAndUser =
                                        new CalculatedParametrsAndUsersMapping();
                                    calcAndUser.Active = true;
                                    calcAndUser.FK_CalculatedParametrsTable = Convert.ToInt32(label.Text);
                                    calcAndUser.CanConfirm = canConfirm.Checked;
                                 //   calcAndUser.CanEdit = canEdit.Checked;
                                    calcAndUser.CanView = canView.Checked;
                                    calcAndUser.FK_UsersTable = userID;
                                    kPiDataContext.CalculatedParametrsAndUsersMapping.InsertOnSubmit(calcAndUser);
                                    kPiDataContext.SubmitChanges();
                                    rowIndex++;
                                }
                            }
                        }
                    }
                    Action.MassMailing(user.Email,"Ваш почтовый адресс был зарегистрирован в системе ИАС 'КФУ-Программа развития'",
                        "Здравствуйте!"+Environment.NewLine+ 
                        "Ваш почтовый адрес был указан при регистрации в системе ИАС 'КФУ-Программа развития!'"+Environment.NewLine+
                        "Пожалуйста, проигнорируйте это письмо, если оно попало к вам по ошибке." + Environment.NewLine +
                        "Для продолжения регистрации перейдите по ссылке ниже:" + Environment.NewLine +
                        "http:"+"//razvitie.cfu-portal.ru/Account/UserRegister?&id=" + passCode + Environment.NewLine +
                        "Спасибо!"
                        , null);
                    
                }
            }
        }
        protected void DropDownList4_SelectedIndexChanged1(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            var vrCountry = (from b in kpiWebDataContext.BasicParametersTable select b);
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add(new DataColumn("BasicParametrsConfirmCheckBox", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("BasicParametrsEditCheckBox", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("BasicParametrsViewCheckBox", typeof(bool)));
            dataTable.Columns.Add(new DataColumn("BasicParametrsName", typeof(string)));
            dataTable.Columns.Add(new DataColumn("BasicParametrsID", typeof(string)));

            #region
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
            }
            //ViewState["BasicRoleMapping"] = dataTable;
            Gridview3.DataSource = dataTable;
            Gridview3.DataBind();
            #endregion
            RolesTable role = (from a in kpiWebDataContext.RolesTable
                               where a.RolesTableID == Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value)
                               select a).FirstOrDefault();
            ViewState["IsHead"] = role.IsHead;
            if ((bool)role.IsHead)
            {

                //////////////////
                var calcParam = (from a in kpiWebDataContext.CalculatedParametrs select a);
                var Indicators = (from c in kpiWebDataContext.IndicatorsTable select c);

                DataTable calcTable = new DataTable();
                calcTable.Columns.Add(new DataColumn("CalculatedParametrsConfirmCheckBox", typeof(bool)));
                calcTable.Columns.Add(new DataColumn("CalculatedParametrsViewCheckBox", typeof(bool)));
                calcTable.Columns.Add(new DataColumn("CalculatedParametrsName", typeof(string)));
                calcTable.Columns.Add(new DataColumn("CalculatedParametrsID", typeof(string)));

                DataTable indicatorTable = new DataTable();
                indicatorTable.Columns.Add(new DataColumn("IndicatorConfirmCheckBox", typeof(bool)));
                indicatorTable.Columns.Add(new DataColumn("IndicatorViewCheckBox", typeof(bool)));
                indicatorTable.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
                indicatorTable.Columns.Add(new DataColumn("IndicatorID", typeof(string)));
                #region
                foreach (var obj in calcParam)
                {
                    DataRow dataRow = calcTable.NewRow();
                    CalculatedParametrsAndRolesMappingTable roleAndCalcMapping =
                        (from a in kpiWebDataContext.CalculatedParametrsAndRolesMappingTable
                         where a.FK_CalculatedParametrs == obj.CalculatedParametrsID
                         && a.FK_RolesTable == Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value)
                         select a).FirstOrDefault();
                    if (roleAndCalcMapping != null)
                    {
                        dataRow["CalculatedParametrsViewCheckBox"] = roleAndCalcMapping.CanView;
                        dataRow["CalculatedParametrsConfirmCheckBox"] = roleAndCalcMapping.CanConfirm;
                    }
                    else
                    {
                        dataRow["CalculatedParametrsViewCheckBox"] = false;
                        dataRow["CalculatedParametrsConfirmCheckBox"] = false;
                    }
                    dataRow["CalculatedParametrsID"] = obj.CalculatedParametrsID.ToString();
                    dataRow["CalculatedParametrsName"] = obj.Name;
                    calcTable.Rows.Add(dataRow);
                }
                Gridview2.DataSource = calcTable;
                Gridview2.DataBind();
                // ViewState["CalcRoleMapping"] = CalcGrid;
                #endregion
                #region
                foreach (var obj in Indicators)
                {
                    DataRow dataRow = indicatorTable.NewRow();
                    IndicatorsAndRolesMappingTable roleAndIndMapping =
                        (from a in kpiWebDataContext.IndicatorsAndRolesMappingTable
                         where a.FK_Indicators == obj.IndicatorsTableID
                         && a.FK_RolesTable == Convert.ToInt32(DropDownList4.Items[DropDownList4.SelectedIndex].Value)
                         select a).FirstOrDefault();
                    if (roleAndIndMapping != null)
                    {
                        dataRow["IndicatorViewCheckBox"] = roleAndIndMapping.CanView;
                        dataRow["IndicatorConfirmCheckBox"] = roleAndIndMapping.CanConfirm;
                    }
                    else
                    {
                        dataRow["IndicatorViewCheckBox"] = false;
                        dataRow["IndicatorConfirmCheckBox"] = false;
                    }
                    dataRow["IndicatorID"] = obj.IndicatorsTableID.ToString();
                    dataRow["IndicatorName"] = obj.Name;
                    indicatorTable.Rows.Add(dataRow);
                }
                Gridview1.DataSource = indicatorTable;
                Gridview1.DataBind();
                // ViewState["IndRoleMapping"] = IndicatorGrid;
                #endregion
            }      
        }

        protected void Gridview3_RowDataBound(object sender, GridViewRowEventArgs e)
        {/*
            bool IsHead = (bool) ViewState["IsHead"];
            if (IsHead)
            {
                gri
            }
            */
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}