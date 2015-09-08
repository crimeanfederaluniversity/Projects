using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Reports
{
    public partial class ChooseReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 0) && (userTable.AccessLevel!= 1))
            {
                Response.Redirect("~/Default.aspx");
            }
            //////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                #region get user reports
                List<ReportArchiveTable> reportsArchiveTablesTable = new List<ReportArchiveTable>();
                if (userTable.AccessLevel != 1)
                {
                    reportsArchiveTablesTable = (
                        from a in kpiWebDataContext.UsersTable
                        join b in kpiWebDataContext.FirstLevelSubdivisionTable
                            on a.FK_FirstLevelSubdivisionTable equals b.FirstLevelSubdivisionTableID
                        join c in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                            on b.FirstLevelSubdivisionTableID equals c.FK_FirstLevelSubmisionTableId
                        join d in kpiWebDataContext.ReportArchiveTable
                            on c.FK_ReportArchiveTableId equals d.ReportArchiveTableID
                        where a.UsersTableID == UserSer.Id
                              && a.FK_SecondLevelSubdivisionTable == c.FK_SecondLevelSubdivisionTable
                              && a.FK_ThirdLevelSubdivisionTable == c.FK_ThirdLevelSubdivisionTable
                              && a.Active == true
                              && b.Active == true
                              && c.Active == true
                              && d.Active == true
                              && d.StartDateTime < DateTime.Now
                        select d).Distinct().ToList();
                }
                else 
                {
                    reportsArchiveTablesTable = (
                        from a in kpiWebDataContext.ReportArchiveTable
                            where a.Active == true
                            select a).Distinct().ToList();
                }
                #endregion

                UsersTable user = (from a in kpiWebDataContext.UsersTable
                                       where a.UsersTableID == userID
                                       select a).FirstOrDefault();

                    int l_0 = user.FK_ZeroLevelSubdivisionTable == null ? 0 : (int)user.FK_ZeroLevelSubdivisionTable;
                    int l_1 = user.FK_FirstLevelSubdivisionTable == null ? 0 : (int)user.FK_FirstLevelSubdivisionTable;
                    int l_2 = user.FK_SecondLevelSubdivisionTable == null ? 0 : (int)user.FK_SecondLevelSubdivisionTable;
                    int l_3 = user.FK_ThirdLevelSubdivisionTable == null ? 0 : (int)user.FK_ThirdLevelSubdivisionTable;
                    int l_4 = user.FK_FourthLevelSubdivisionTable == null ? 0 : (int)user.FK_FourthLevelSubdivisionTable;
                    int l_5 = user.FK_FifthLevelSubdivisionTable == null ? 0 : (int)user.FK_FifthLevelSubdivisionTable;
                    int userLevel = 5;

                    userLevel = l_5 == 0 ? 4 : userLevel;
                    userLevel = l_4 == 0 ? 3 : userLevel;
                    userLevel = l_3 == 0 ? 2 : userLevel;
                    userLevel = l_2 == 0 ? 1 : userLevel;
                    userLevel = l_1 == 0 ? 0 : userLevel;
                    userLevel = l_0 == 0 ? -1 : userLevel;

                DataTable dataTable = new DataTable();
                
                dataTable.Columns.Add(new DataColumn("ReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("ReportName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Status", typeof(string)));

                List<int> StatenList = new List<int>();
                foreach (ReportArchiveTable ReportRow in reportsArchiveTablesTable)
                {
                    
                    #region
                  

                    int can_view =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                   join d in kpiWebDataContext.BasicParametrAdditional
                                       on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where
                                         a.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID //из нужного отчёта
                                         && c.FK_UsersTable == userID // свяный с пользователем
                                         //&& d.SubvisionLevel == 3 //нужный уровень заполняющего
                                         && a.Active == true // запись в таблице связей показателя и отчёта активна
                                         && c.CanView == true
                                         && c.Active == true // запись в таблице связей показателя и пользователей активна
                                         && d.Calculated == false
                                     // этот показатель нужно вводить а не считать
                                     select b).ToList().Count;
                    if (can_view>0)
                    {
                        
                    }
                    else
                    {
                        continue;
                    }


                    DataRow dataRow = dataTable.NewRow();
                                dataRow["ReportArchiveID"] = ReportRow.ReportArchiveTableID.ToString();
                                dataRow["ReportName"] = ReportRow.Name;
                                dataRow["StartDate"] = ReportRow.StartDateTime.ToString().Split(' ')[0];//только дата// время обрезается сплитом
                                dataRow["EndDate"] = ReportRow.EndDateTime.ToString().Split(' ')[0]; 
                                
                                //нужно определить статус данных
                                //status=0 данных нет 
                                //status=1 данные вернули на доработку
                                //status=2 данные есть
                                //status=3 данные отправлены на верификацию
                                //status=4 данные верифицированы первым первым уровнем(кафедрой)

                    // возьмем первый попавшийся заполненный показатель
                                CollectedBasicParametersTable ColTemp =
                                                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                         join b in kpiWebDataContext.UsersTable
                                                             on a.FK_UsersTable equals b.UsersTableID
                                                         join c in kpiWebDataContext.BasicParametrAdditional
                                                             on a.FK_BasicParametersTable equals c.BasicParametrAdditionalID
                                                         join d in kpiWebDataContext.BasicParametrsAndUsersMapping //
                                                         on a.FK_BasicParametersTable equals d.FK_ParametrsTable //
                                                         where
                                                             a.FK_ReportArchiveTable == ReportRow.ReportArchiveTableID
                                                             && ((b.FK_ZeroLevelSubdivisionTable == l_0) || (l_0 == 0))
                                                             && ((b.FK_FirstLevelSubdivisionTable == l_1) || (l_1 == 0))
                                                             && ((b.FK_SecondLevelSubdivisionTable == l_2) || (l_2 == 0))
                                                             && ((b.FK_ThirdLevelSubdivisionTable == l_3) || (l_3 == 0))
                                                             && ((b.FK_FourthLevelSubdivisionTable == l_4) || (l_4 == 0))
                                                             && ((b.FK_FifthLevelSubdivisionTable == l_5) || (l_5 == 0))
                                                             && ((d.CanConfirm == true) || (d.CanEdit == true)) //
                                                             && (d.FK_UsersTable == userID) //
                                                             && a.Active == true
                                                             && b.Active == true
                                                             && c.Calculated == false
                                                         select a).FirstOrDefault();

                   

                    string status = "Нет данных";
                    int Statusn = 0;

                    
                    if (ReportRow.EndDateTime > DateTime.Now)
                        Statusn = 100;


                    if (ColTemp == null)
                    {
                        Statusn = 0;
                    }
                    else if (ColTemp.Status == null)
                    {
                        Statusn = 0;
                    }
                    else
                    {
                        Statusn = (int) ColTemp.Status;
                    }

                    if (Statusn == 4)
                    {
                        status = "Данные утверждены";
                    }
                    else if (Statusn == 3)
                    {
                        status = "Данные ожидают утверждения";
                    }
                    else if (Statusn == 2)
                    {
                        status = "Данные в процессе заполнения";
                    }
                    else if (Statusn == 1)
                    {
                        status = "Данные возвращены на доработку";                       
                    }
                    else if (Statusn == 0)
                    {
                        status = "Данные в процессе заполнения";
                    }
                    else if (Statusn == 100)
                    {
                        status = "Отчет завершен";
                    }
                    else
                    {
                        //error
                    }
                    
                                StatenList.Add(Statusn);                    
                                dataRow["Status"] = status;               
                                dataTable.Rows.Add(dataRow);
                    #endregion
                }   
        
                GridView1.DataSource = dataTable;
                GridView1.DataBind();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    
                    Button btnEdit = GridView1.Rows[i].FindControl("ButtonEditReport") as Button;
                    Button btnView = GridView1.Rows[i].FindControl("ButtonViewReport") as Button;
                    Button btnConfirm = GridView1.Rows[i].FindControl("ButtonConfirmReport") as Button;

                    //status=0 данных нет 
                    //status=1 данные вернули на доработку
                    //status=2 данные есть
                    //status=3 данные отправлены на верификацию
                    //status=4 данные верифицированы первым первым уровнем(кафедрой)

                    int ConfButton = 0;
                    int ViewButton = 0;
                    int EditButton = 0;

                    if ((btnEdit != null) && (btnView != null) && (btnConfirm != null))
                    {
                        if ((StatenList[i] == 0)||(StatenList[i] == 1)||(StatenList[i] == 2))
                        {
                            ConfButton++;
                        }
                        else if (StatenList[i] == 3)
                        {
                            EditButton++;
                        }
                        else
                        {
                            ConfButton++;
                            EditButton++;
                        }
                        #region
                        int ReportArchiveID = Convert.ToInt32(btnEdit.CommandArgument);

                            if (userLevel == 3)
                            {                                
                                #region

                                int kaf_edit =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where
                                         a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                         && c.FK_UsersTable == userID // свяный с пользователем
                                         && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                         && a.Active == true // запись в таблице связей показателя и отчёта активна
                                         && c.CanEdit == true
                                         && c.Active == true // запись в таблице связей показателя и пользователей активна
                                         && d.Calculated == false
                                     // этот показатель нужно вводить а не считать
                                     select b).ToList().Count;
                                int kaf_view =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where
                                         a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                         && c.FK_UsersTable == userID // свяный с пользователем
                                         && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                         && a.Active == true // запись в таблице связей показателя и отчёта активна
                                         && c.CanView == true
                                         && c.Active == true // запись в таблице связей показателя и пользователей активна
                                         && d.Calculated == false
                                     // этот показатель нужно вводить а не считать
                                     select b).ToList().Count;
                                int kaf_conf =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where
                                         a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                         && c.FK_UsersTable == userID // свяный с пользователем
                                         && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                         && a.Active == true // запись в таблице связей показателя и отчёта активна
                                         && c.CanConfirm == true
                                         && c.Active == true // запись в таблице связей показателя и пользователей активна
                                         && d.Calculated == false
                                     // этот показатель нужно вводить а не считать
                                     select b).ToList().Count;

                                #endregion
                                #region

                                int specEdit =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where a.FK_ReportArchiveTable == ReportArchiveID //для отчёта
                                           && d.SubvisionLevel == 4 // для уровня заполняющего
                                           && d.Calculated == false //только вводимые параметры
                                           && c.FK_UsersTable == userID // связаннаые с пользователем
                                           && a.Active == true
                                           && c.CanEdit == true
                                           && c.Active == true
                                     select b).ToList().Count;
                                int specView =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where a.FK_ReportArchiveTable == ReportArchiveID //для отчёта
                                           && d.SubvisionLevel == 4 // для уровня заполняющего
                                           && d.Calculated == false //только вводимые параметры
                                           && c.FK_UsersTable == userID // связаннаые с пользователем
                                           && a.Active == true
                                           && c.CanView == true
                                           && c.Active == true
                                     select b).ToList().Count;
                                int specConf =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where a.FK_ReportArchiveTable == ReportArchiveID //для отчёта
                                           && d.SubvisionLevel == 4 // для уровня заполняющего
                                           && d.Calculated == false //только вводимые параметры
                                           && c.FK_UsersTable == userID // связаннаые с пользователем
                                           && a.Active == true
                                           && c.CanConfirm == true
                                           && c.Active == true
                                     select b).ToList().Count;

                                #endregion

                                if (!((kaf_edit + specEdit)>0))
                                {
                                    EditButton++;
                                }
                                if (!((kaf_conf + specConf)>0))
                                {
                                    ConfButton++;
                                }
                                if (!((kaf_view + specView) > 0))
                                {
                                    ViewButton++;
                                }
                            }
                            else if (userLevel == 0)
                            {
                                if (userTable.AccessLevel == 1)
                                {
                                    EditButton=0;                     
                                    ConfButton=0;
                                    ViewButton=0;
                                }
                            }    
                            else
                            {
                                #region
                                int edit = 0;
                                int view = 0;
                                int conf = 0;

                                edit = (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                        join b in kpiWebDataContext.BasicParametersTable
                                            on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                        join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                            on b.BasicParametersTableID equals c.FK_ParametrsTable
                                        join d in kpiWebDataContext.BasicParametrAdditional
                                            on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                        where
                                            a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                            && c.FK_UsersTable == userID // свяный с пользователем
                                            && d.SubvisionLevel == userLevel //нужный уровень заполняющего
                                            && a.Active == true // запись в таблице связей показателя и отчёта активна
                                            && c.CanEdit == true
                                            && c.Active == true // запись в таблице связей показателя и пользователей активна
                                            && d.Calculated == false
                                        select b).ToList().Count;

                                view = (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                        join b in kpiWebDataContext.BasicParametersTable
                                            on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                        join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                            on b.BasicParametersTableID equals c.FK_ParametrsTable
                                        join d in kpiWebDataContext.BasicParametrAdditional
                                            on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                        where
                                            a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                            && c.FK_UsersTable == userID // свяный с пользователем
                                            && d.SubvisionLevel == userLevel //нужный уровень заполняющего
                                            && a.Active == true // запись в таблице связей показателя и отчёта активна
                                            && c.CanView == true
                                            && c.Active == true // запись в таблице связей показателя и пользователей активна
                                            && d.Calculated == false
                                        select b).ToList().Count;

                                conf = (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                        join b in kpiWebDataContext.BasicParametersTable
                                            on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                        join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                            on b.BasicParametersTableID equals c.FK_ParametrsTable
                                        join d in kpiWebDataContext.BasicParametrAdditional
                                            on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                        where
                                            a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                            && c.FK_UsersTable == userID // свяный с пользователем
                                            && d.SubvisionLevel == userLevel //нужный уровень заполняющего
                                            && a.Active == true // запись в таблице связей показателя и отчёта активна
                                            && c.CanConfirm == true
                                            && c.Active == true // запись в таблице связей показателя и пользователей активна
                                            && d.Calculated == false
                                        select b).ToList().Count;
                                #endregion

                                if (!(edit > 0))
                                {
                                    EditButton++;
                                }
                                if (!(conf > 0))
                                {
                                    ConfButton++;
                                }
                                if (!(view > 0))
                                {
                                    ViewButton++;
                                }               
                        }                    
                    #endregion

                            if (StatenList[i]==100)
                            {
                                ConfButton = 0;
                                EditButton = 0;
                            }
                    /////////////////////////////////////////////////////////
                        btnConfirm.Enabled = ConfButton>0?false:true;
                        btnEdit.Enabled = EditButton>0?false:true;
                        btnView.Enabled = ViewButton > 0 ? false : true;
                    }
                }
                ///вывели все отчёты с параметрами в гридвью  
                if (GridView1.Rows.Count == 0)
                {
                    var name =
                        (from tr in kpiWebDataContext.UsersTable
                         where tr.UsersTableID == UserSer.Id
                         select tr.FirstLevelSubdivisionTable.Name).FirstOrDefault();
                    Label1.Visible = true;
                    Label2.Visible = true;
                    if (name != null) Label1.Text = "В данный момент для вашего подразделения (" + name + ") отсутствуют активные отчеты. Мы обязательно уведомим Вас о начале новой отчетной кампании.";
                    else Label1.Text = "В данный момент для вашего подразделения отсутствуют активные отчеты. Мы обязательно уведомим Вас о начале новой отчетной кампании.";

                    Label2.Text = "С уважением администрация ИАС \"КФУ-Программа развития\"";
                } 
       
            }
        }
        protected void ButtonEditClick(object sender, EventArgs e)
        {            
            Button button = (Button)sender;
            {
                Serialization UserSer = (Serialization)Session["UserID"];
                if (UserSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                int userID = UserSer.Id;
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                UsersTable userTable =
                    (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
                ThirdLevelParametrs thirdParam = (from a in kpiWebDataContext.ThirdLevelParametrs
                    where
                        a.ThirdLevelParametrsID == userTable.FK_ThirdLevelSubdivisionTable
                        && a.Active == true 
                
                    select a).FirstOrDefault();

                

                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчёта               
                Serialization modeSer = new Serialization(0,null,null);
                Session["mode"] = modeSer;

                UsersTable currentUser =
                        (from a in kpiWebDataContext.UsersTable
                         where a.UsersTableID == (int)ViewState["LocalUserID"]
                         select a).FirstOrDefault();

                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0CR0: Пользователь " + currentUser.Email + " зашел на страницу редактирования отчета с ID = " + paramSerialization.ReportStr);

                if (currentUser.AccessLevel == 1)
                {
                    Response.Redirect("~/Reports_/FillingTheReportByDepartment.aspx"); 
                }

                if (thirdParam != null)
                {
                    
                    if (thirdParam.CanGraduate)
                    {
                        FourthLevelSubdivisionTable fourth =
                        (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                         where a.Active == true
                         && a.FK_ThirdLevelSubdivisionTable == thirdParam.ThirdLevelParametrsID
                         select a).FirstOrDefault();
                        if (fourth != null)
                        {
                            Response.Redirect("~/Reports_/Parametrs.aspx");
                        }
                        else
                        {
                            Response.Redirect("~/Reports_/FillingTheReport.aspx");
                        }

                    }
                    else
                    {
                       Response.Redirect("~/Reports_/FillingTheReport.aspx");
                    }
                }
                else
                {
                    Response.Redirect("~/Reports_/FillingTheReport.aspx");
                }
            }
        }
        protected void ButtonViewClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчёта        
                Serialization modeSer = modeSer = new Serialization(1, null, null);
                /////костыль для демонстрации
                 Serialization UserSer = (Serialization)Session["UserID"];
                 if (UserSer == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }

            int userID = UserSer.Id;    
            modeSer = new Serialization(1, null, null);

                Session["mode"] = modeSer;
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                UsersTable currentUser =
                        (from a in kpiWebDataContext.UsersTable
                         where a.UsersTableID == (int)ViewState["LocalUserID"]
                         select a).FirstOrDefault();
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0CR1: Пользователь " + currentUser.Email + " зашел на страницу просмотра отчета с ID = " + paramSerialization.ReportStr);

                if (currentUser.AccessLevel == 1)
                {
                    Response.Redirect("~/Reports_/FillingTheReportByDepartment.aspx");                    
                }
                else if (currentUser.AccessLevel == 0)
                {
                    Response.Redirect("~/Reports_/FillingTheReport.aspx");
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }
        protected void ButtonConfirmClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                Serialization paramSerialization = new Serialization(button.CommandArgument.ToString());
                Session["ReportArchiveID"] = paramSerialization; // запомнили в сессии номер отчёта               
                Serialization modeSer = new Serialization(2, null, null);
                Session["mode"] = modeSer;
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                UsersTable currentUser =
                        (from a in kpiWebDataContext.UsersTable
                         where a.UsersTableID == (int)ViewState["LocalUserID"]
                         select a).FirstOrDefault();

                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0CR2: Пользователь " + currentUser.Email + " зашел на страницу просмотра и подтверждения отчета с ID = " + paramSerialization.ReportStr);

                if (currentUser.AccessLevel == 1)
                {
                    Response.Redirect("~/Reports_/FillingTheReportByDepartment.aspx");
                }
                else if (currentUser.AccessLevel == 0)
                {
                    Response.Redirect("~/Reports_/FillingTheReport.aspx");
                }
                else
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }      
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
         
        }
    }
}