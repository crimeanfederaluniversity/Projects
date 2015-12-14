using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.WebPages;
using iTextSharp.text;
using iTextSharp.text.pdf;
using KPIWeb.Reports_;
//using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;
using Page = System.Web.UI.Page;
using TextBox = System.Web.UI.WebControls.TextBox;

namespace KPIWeb.Reports
{
    public partial class FillingTheReport : System.Web.UI.Page
    {
        public int col_ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            #region JavaScriptFunctions
            string script = @"<script>
            function ConfirmSubmit() {
                var msg = confirm('Режим доступа к данным будет изменен на \'только просмотр\'.Отправить данные на утверждение?'); 
                    if (msg == true)
                    {
                        document.getElementById('LoadPanel_').style.visibility = 'visible'
                            return true;
                    }
                    else
                    {
                        document.getElementById('LoadPanel_').style.visibility = 'hidden'
                            return false;
                    }
            }
            </script>";

            string script2 = @"<script>
            function ConfirmSubmitA() { 
                var msg = confirm('Режим доступа к данным будет изменен на \'только просмотр\'.Подтвердить достоверность данных и отправить их на обработку?'); 
                    if (msg == true)
                    {
                        document.getElementById('LoadPanel_').style.visibility = 'visible'
                            return true;
                    }
                    else
                    {
                        document.getElementById('LoadPanel_').style.visibility = 'hidden'
                            return false;
                    }

            }
            </script>";

            string script3 = @"<script>
            function ConfirmSubmitOn() {
                var msg = confirm('Режим доступа к данным будет изменен на \'только просмотр\'.Вернуть отчёт на доработку?'); 
                    if (msg == true)
                    {
                        document.getElementById('LoadPanel_').style.visibility = 'visible'
                            return true;
                    }
                    else
                    {
                        document.getElementById('LoadPanel_').style.visibility = 'hidden'
                            return false;
                    }
            }
            </script>";

            string script4 = @"<script>
            function ConfirmSubmitOnТ() {
                var msg = 'Режим доступа к данным будет изменен на \'только просмотр\'. Отправить отчет на утверждение?'; 
                                document.location = '../Default.aspx';
                return alert (msg);
                
            }
            </script>";

            string script5 = @"<script>
            function ConfirmSubmitOnTT() {
                var msg = 'Режим доступа к данным будет изменен на \'только просмотр\'.Отправить отчет на доработку?'; 
                return confirm(msg);
            }
            </script>";

            #endregion
            #region Initialization n Sessions
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
            int SecondLevel = paramSerialization.l2;
            int ThirdLevel = paramSerialization.l3;


            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                      (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            
            ViewState["login"] = (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a.Email).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 7, 9, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            if (userRights.CanUserSeeThisPage(userID, 7, 0, 0))
            {
                userTable = (from a in kPiDataContext.UsersTable
                             where a.Active == true
                             && a.FK_ThirdLevelSubdivisionTable == ThirdLevel
                             join b in kPiDataContext.BasicParametrsAndUsersMapping
                                 on a.UsersTableID equals b.FK_UsersTable
                             where b.Active == true
                             && b.CanEdit == true
                             select a).FirstOrDefault();
            }
            ViewState["userTableID"] = (int)userTable.UsersTableID;
            #endregion
            /////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {             
                    #region GetUserInfo

                    Serialization modeSer = (Serialization)Session["mode"];
                    if (modeSer == null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
                    }
                    int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем //4 зашел директор
                    ////////////////
                    int l_0 = userTable.FK_ZeroLevelSubdivisionTable == null ? 0 : (int)userTable.FK_ZeroLevelSubdivisionTable;
                    int l_1 = userTable.FK_FirstLevelSubdivisionTable == null ? 0 : (int)userTable.FK_FirstLevelSubdivisionTable;
                    int l_2 = userTable.FK_SecondLevelSubdivisionTable == null? 0: (int)userTable.FK_SecondLevelSubdivisionTable;
                    int l_3 = userTable.FK_ThirdLevelSubdivisionTable == null ? 0 : (int)userTable.FK_ThirdLevelSubdivisionTable;
                    int l_4 = userTable.FK_FourthLevelSubdivisionTable == null? 0: (int)userTable.FK_FourthLevelSubdivisionTable;
                    int l_5 = userTable.FK_FifthLevelSubdivisionTable == null ? 0 : (int)userTable.FK_FifthLevelSubdivisionTable;
                    int userLevel = 5;
                    userLevel = l_5 == 0 ? 4 : userLevel;
                    userLevel = l_4 == 0 ? 3 : userLevel;
                    userLevel = l_3 == 0 ? 2 : userLevel;
                    userLevel = l_2 == 0 ? 1 : userLevel;
                    userLevel = l_1 == 0 ? 0 : userLevel;
                    userLevel = l_0 == 0 ? -1 : userLevel;

                    ////ранги пользователя
                    /// -1 никто ниоткуда/// 0 с Кфу /// 1 с Академии/// 2 с Факультета/// 3 с кафедры/// 4 с специализация/// 5 с под специализацией,пока нет

                    #endregion
                    #region DataTableCreate

                    List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
                    List<string> basicNames = new List<string>(); // сюда названия параметров для excel
                    /////создаем дататейбл
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));

                    dataTable.Columns.Add(new DataColumn("Comment", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CommentEnabled", typeof(string)));

                    for (int k = 0; k <= 40; k++) //создаем кучу полей
                    {
                        dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                        dataTable.Columns.Add(new DataColumn("CollectId" + k.ToString(), typeof(string)));
                        dataTable.Columns.Add(new DataColumn("NotNull" + k.ToString(), typeof(string)));
                    }

                    #endregion
                    int additionalColumnCount = 0;
                    List<int> StatusList = new List<int>();
                    #region

                    if (userLevel == 3)
                    {
                        List<BasicParametersTable> BasicParams =
                            (from a in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kPiDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kPiDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kPiDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                   && c.FK_UsersTable == userTable.UsersTableID // свяный с пользователем
                                   && d.SubvisionLevel == userLevel //нужный уровень заполняющего
                                   && a.Active == true // запись в таблице связей показателя и отчёта активна
                                   && (((c.CanEdit == true) && mode == 0)
                                       || ((c.CanView == true) && mode == 1)
                                       || ((c.CanConfirm == true) && mode == 2)) // фильтруем по правам пользователя
                                   && c.Active == true // запись в таблице связей показателя и пользователей активна
                                   && d.Calculated == false
                             // этот показатель нужно вводить а не считать
                             select b).ToList();
                        //узнали показатели
                        foreach (BasicParametersTable basicParam in BasicParams) //пройдемся по показателям
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                            dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;
                            dataRow["Name"] = basicParam.Name;

                            string comment_ = (from a in kPiDataContext.BasicParametrAdditional
                                               where a.BasicParametrAdditionalID == basicParam.BasicParametersTableID
                                               && a.Active == true
                                               select a.Comment).FirstOrDefault();
                            if (comment_ != null)
                            {
                                if (comment_.Length > 3)
                                {
                                    dataRow["Comment"] = comment_;
                                    dataRow["CommentEnabled"] = "visible";
                                }
                                else
                                {
                                    dataRow["Comment"] = "nun";
                                    dataRow["CommentEnabled"] = "hidden";
                                }
                            }
                            else
                            {
                                dataRow["Comment"] = "nun";
                                dataRow["CommentEnabled"] = "hidden";
                            }




                            basicNames.Add(basicParam.Name);
                            CollectedBasicParametersTable collectedBasicTmp =
                                (from a in kPiDataContext.CollectedBasicParametersTable
                                 where ((a.FK_ZeroLevelSubdivisionTable == l_0) || l_0 == 0)
                                       && ((a.FK_FirstLevelSubdivisionTable == l_1) || l_1 == 0)
                                       && ((a.FK_SecondLevelSubdivisionTable == l_2) || l_2 == 0)
                                       && ((a.FK_ThirdLevelSubdivisionTable == l_3) || l_3 == 0)
                                       && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                       && a.FK_ReportArchiveTable == ReportArchiveID
                                 select a).FirstOrDefault();
                            if (collectedBasicTmp == null) // надо создать
                            {
                                collectedBasicTmp = new CollectedBasicParametersTable();
                                collectedBasicTmp.Active = true;
                                collectedBasicTmp.FK_UsersTable = userTable.UsersTableID;
                                collectedBasicTmp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                                collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
                                collectedBasicTmp.CollectedValue = null;
                                collectedBasicTmp.UserIP =
                                    Dns.GetHostEntry(Dns.GetHostName())
                                        .AddressList.Where(
                                            ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                                        .Select(ip => ip.ToString())
                                        .FirstOrDefault() ?? "";
                                collectedBasicTmp.LastChangeDateTime = DateTime.Now;
                                collectedBasicTmp.SavedDateTime = DateTime.Now;
                                collectedBasicTmp.FK_ZeroLevelSubdivisionTable = userTable.FK_ZeroLevelSubdivisionTable;
                                collectedBasicTmp.FK_FirstLevelSubdivisionTable = userTable.FK_FirstLevelSubdivisionTable;
                                collectedBasicTmp.FK_SecondLevelSubdivisionTable = userTable.FK_SecondLevelSubdivisionTable;
                                collectedBasicTmp.FK_ThirdLevelSubdivisionTable = userTable.FK_ThirdLevelSubdivisionTable;
                                collectedBasicTmp.Status = 0;
                                kPiDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                                kPiDataContext.SubmitChanges();
                            }
                            dataRow["Value0"] = collectedBasicTmp.CollectedValue.ToString();
                            dataRow["CollectId0"] = collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                            dataRow["NotNull0"] = 1.ToString();
                            dataTable.Rows.Add(dataRow);
                            if (collectedBasicTmp.Status != null)
                            {
                                StatusList.Add((int)collectedBasicTmp.Status);
                            }
                            else
                            {
                                StatusList.Add(0);
                            }
                        }
                        additionalColumnCount += 1;
                        columnNames.Add("  ");
                    }
                    /* columnNames.Add("Кафедра:\r\n" + (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                                   where a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                                   select a.Name).FirstOrDefault());
                 * */

                    #endregion
                    #region

                                if ((from zz in kPiDataContext.ThirdLevelParametrs
                                     where zz.ThirdLevelParametrsID == l_3
                                     select zz.CanGraduate).FirstOrDefault() == true)
                                // кафедра выпускающая значит специальности есть
                                {
                                    List<BasicParametersTable> SpecBasicParams =
                                        (from a in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                         join b in kPiDataContext.BasicParametersTable
                                             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                         join c in kPiDataContext.BasicParametrsAndUsersMapping
                                             on b.BasicParametersTableID equals c.FK_ParametrsTable
                                         join d in kPiDataContext.BasicParametrAdditional
                                             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                         where a.FK_ReportArchiveTable == ReportArchiveID //для отчёта
                                               && d.SubvisionLevel == 4 // для уровня заполняющего
                                               && d.Calculated == false //только вводимые параметры
                                               && c.FK_UsersTable == userTable.UsersTableID // связаннаые с пользователем
                                               && a.Active == true

                                               && (((c.CanEdit == true) && mode == 0)
                                                   || ((c.CanView == true) && mode == 1)
                                                   || ((c.CanConfirm == true) && mode == 2)
                                                   || ((c.CanEdit == true) && mode == 4))
                                             // фильтруем по правам пользователя

                                               && c.Active == true
                                         select b).ToList();
                                    //Получили показатели разрешенные пользователю в данном отчёте
                                    List<FourthLevelSubdivisionTable> Specialzations =
                                        (from a in kPiDataContext.FourthLevelSubdivisionTable
                                         where a.FK_ThirdLevelSubdivisionTable == l_3
                                               && a.Active == true
                                         select a).ToList();
                                    //Получили список специальностей для кафедры под пользователем 

                                    foreach (FourthLevelSubdivisionTable spec in Specialzations)
                                    {
                                        /*
                                        columnNames.Add("Направление подготовки\r" +
                                                        (from a in kPiDataContext.SpecializationTable
                                                         where a.SpecializationTableID == spec.FK_Specialization
                                                         select a.Name).FirstOrDefault().ToString() +" : "+ 
                                                         (from a in kPiDataContext.SpecializationTable
                                                         where a.SpecializationTableID == spec.FK_Specialization
                                                         select a.SpecializationNumber).FirstOrDefault().ToString());
                                         *  string CurrentColumnName = "<div style=\"transform:rotate(90deg);\">" + (from a in kPiDataContext.SpecializationTable
                                                                                                                  where a.SpecializationTableID == spec.FK_Specialization
                                                                                                                  select a.SpecializationNumber).FirstOrDefault().ToString() + "</div>";

                                       */

                                        string CurrentColumnName = (from a in kPiDataContext.SpecializationTable
                                                                    where a.SpecializationTableID == spec.FK_Specialization
                                                                    select a.SpecializationNumber).FirstOrDefault().ToString();


                                        columnNames.Add(CurrentColumnName);

                                        //запомнили название специальности // оно нам пригодится)
                                    }

                                    foreach (BasicParametersTable specBasicParam in SpecBasicParams)
                                    {
                                        int i = additionalColumnCount;
                                        DataRow dataRow = dataTable.NewRow();
                                        BasicParametrAdditional basicParametrs =
                                            (from a in kPiDataContext.BasicParametrAdditional
                                             where
                                                 a.BasicParametrAdditionalID == specBasicParam.BasicParametersTableID
                                             select a).FirstOrDefault();
                                        //узнали параметры базового показателя
                                        int j = 0;
                                        //если хоть одной специальности базовый показатель нужен то мы его выведем
                                        foreach (FourthLevelSubdivisionTable spec in Specialzations)
                                        {

                                            FourthLevelParametrs fourthParametrs =
                                                (from a in kPiDataContext.FourthLevelParametrs
                                                 where a.FourthLevelParametrsID == spec.FourthLevelSubdivisionTableID
                                                 select a).FirstOrDefault();
                                            // узнали параметры специальности
                                            // если этото параметр и эта специальность дружат  
                                            if (((fourthParametrs.IsForeignStudentsAccept == true) ||
                                                 (basicParametrs.ForForeignStudents == false)) //это для иностранцев
                                                &&
                                                ((fourthParametrs.SpecType == basicParametrs.SpecType) ||
                                                 (basicParametrs.SpecType == 0)))
                                            // это для деления на магистров аспирантов итд
                                            {
                                                j++; //потом проверка и следовательно БП нуно выводить
                                                CollectedBasicParametersTable collectedBasicTmp =
                                                    (from a in kPiDataContext.CollectedBasicParametersTable
                                                     where
                                                         a.FK_BasicParametersTable ==
                                                         specBasicParam.BasicParametersTableID
                                                         && a.FK_ReportArchiveTable == ReportArchiveID
                                                         &&
                                                         (a.FK_ZeroLevelSubdivisionTable ==
                                                          userTable.FK_ZeroLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_FirstLevelSubdivisionTable ==
                                                          userTable.FK_FirstLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_SecondLevelSubdivisionTable ==
                                                          userTable.FK_SecondLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_ThirdLevelSubdivisionTable ==
                                                          userTable.FK_ThirdLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_FourthLevelSubdivisionTable ==
                                                          spec.FourthLevelSubdivisionTableID)
                                                     select a).FirstOrDefault();
                                                if (collectedBasicTmp == null)
                                                {
                                                    collectedBasicTmp = new CollectedBasicParametersTable();
                                                    collectedBasicTmp.Active = true;
                                                    collectedBasicTmp.Status = 0;
                                                    collectedBasicTmp.FK_UsersTable = userTable.UsersTableID;
                                                    collectedBasicTmp.FK_BasicParametersTable =
                                                        specBasicParam.BasicParametersTableID;
                                                    collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
                                                    collectedBasicTmp.CollectedValue = null;
                                                    collectedBasicTmp.UserIP =
                                                        Dns.GetHostEntry(Dns.GetHostName())
                                                            .AddressList.Where(
                                                                ip =>
                                                                    ip.AddressFamily ==
                                                                    System.Net.Sockets.AddressFamily.InterNetwork)
                                                            .Select(ip => ip.ToString())
                                                            .FirstOrDefault() ?? "";
                                                    collectedBasicTmp.LastChangeDateTime = DateTime.Now;
                                                    collectedBasicTmp.SavedDateTime = DateTime.Now;
                                                    collectedBasicTmp.FK_ZeroLevelSubdivisionTable =
                                                        userTable.FK_ZeroLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_FirstLevelSubdivisionTable =
                                                        userTable.FK_FirstLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_SecondLevelSubdivisionTable =
                                                        userTable.FK_SecondLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_ThirdLevelSubdivisionTable =
                                                        spec.FK_ThirdLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_FourthLevelSubdivisionTable =
                                                        spec.FourthLevelSubdivisionTableID;
                                                    kPiDataContext.CollectedBasicParametersTable.InsertOnSubmit(
                                                        collectedBasicTmp);
                                                    kPiDataContext.SubmitChanges();
                                                }
                                                dataRow["Value" + i] = collectedBasicTmp.CollectedValue.ToString();
                                                dataRow["CollectId" + i] =
                                                    collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                                                dataRow["NotNull" + i] = 1.ToString();

                                                if (collectedBasicTmp.Status != null)
                                                {
                                                    StatusList.Add((int)collectedBasicTmp.Status);
                                                }
                                                else
                                                {
                                                    StatusList.Add(0);
                                                }
                                            }
                                            i++;
                                        }
                                        if (j > 0)
                                        {
                                            basicNames.Add(specBasicParam.Name);
                                            dataRow["Name"] = specBasicParam.Name;
                                            dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                                            dataRow["BasicParametersTableID"] = specBasicParam.BasicParametersTableID;

                                            string comment_ = (from a in kPiDataContext.BasicParametrAdditional
                                                               where a.BasicParametrAdditionalID == specBasicParam.BasicParametersTableID
                                                               && a.Active == true
                                                               select a.Comment).FirstOrDefault();
                                            if (comment_ != null)
                                            {
                                                if (comment_.Length > 3)
                                                {
                                                    dataRow["Comment"] = comment_;
                                                    dataRow["CommentEnabled"] = "visible";
                                                }
                                                else
                                                {
                                                    dataRow["Comment"] = "nun";
                                                    dataRow["CommentEnabled"] = "hidden";
                                                }
                                            }
                                            else
                                            {
                                                dataRow["Comment"] = "nun";
                                                dataRow["CommentEnabled"] = "hidden";
                                            }

                                            dataTable.Rows.Add(dataRow);
                                        }
                                        ///////////////////////закинули все в дататейбл
                                    }
                                    additionalColumnCount += Specialzations.Count;
                                }

                                #endregion
                    ViewState["CollectedBasicParametersTable"] = dataTable;
                    ViewState["CurrentReportArchiveID"] = ReportArchiveID;
                    ViewState["ValueColumnCnt"] = additionalColumnCount;
                    ViewState["ColumnName"] = columnNames;
                    ViewState["basicNames"] = basicNames;

                    int tmpStatCount = 0;
                    foreach (int tmpStat in StatusList)
                    {
                        if (tmpStat == 2)
                        {
                            tmpStatCount++;
                        }
                    }
                    //определение дней
                    DateTime endDate = (DateTime)(from a in kPiDataContext.ReportArchiveTable
                                                  where a.ReportArchiveTableID == ReportArchiveID
                                                  select a.EndDateTime).FirstOrDefault();
                    if (endDate == null)
                    {
                        endDate = DateTime.Now.AddDays(2);
                    }
                    DateTime startDate = DateTime.Now;
                    int dateCount = 0;
                    while (startDate < endDate)
                    {
                        startDate = startDate.AddDays(1);
                        dateCount++;
                    }

                    // определение дней
                    if (mode == 0)
                    {
                        #region
                        GoBackButton.Visible = true;
                        GoBackButton.Text = "Вернуться без сохранения";

                        ButtonSave.Visible = true;
                        ButtonSave.Text = "Сохранить внесенные данные";

                        UpnDownButton.Visible = true;
                        UpnDownButton.Text = "Отправить отчёт на утверждение";

                        TextBox1.Visible = false;

                        if (StatusList[0] == 1)
                        {
                            Label1.Text = "Данные возвращены на доработку. Проверьте корректность введенных данных";
                        }
                        else
                        {
                            if (tmpStatCount == StatusList.Count())
                            {
                                Label1.Text = "Все показатели заполнены. Необходимо отправить отчёт на утверждение";
                                Label3.Text = "Все показатели заполнены. Необходимо отправить отчёт на утверждение";
                                UpnDownButton.Enabled = true;
                            }
                            else
                            {
                                Label1.Text = "Заполнено " + tmpStatCount + " показателей из " + StatusList.Count();
                                Label3.Text = "Заполнено " + tmpStatCount + " показателей из " + StatusList.Count();
                                UpnDownButton.Enabled = false;
                            }
                        }
                        ViewState["AllCnt"] = StatusList.Count();

                        Label2.Text = "Осталось " + dateCount + " дней до закрытия отчёта";
                        if ((DateTime.Now > endDate))
                        {

                            Label2.Text = "";
                        }
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                         "Confirm", script);
                        UpnDownButton.Attributes.Add("OnClick", "return ConfirmSubmit();");




                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "window.onbeforeunload = function() { return 'Date will be lost: are you sure?'; };", true);
                        ButtonSave.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        Button1.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        UpnDownButton.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        GoBackButton.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        #endregion
                    }
                    else if (mode == 1)
                    {
                        #region
                        GoBackButton.Visible = true;
                        GoBackButton.Text = "Вернуться в меню";

                        ButtonSave.Visible = false;

                        UpnDownButton.Visible = false;

                        TextBox1.Visible = false;

                        if ((StatusList[0] == 0) || (StatusList[0] == 2))
                        {
                            Label1.Text = "Заполнено " + tmpStatCount + " показателей из " + StatusList.Count();
                            Label3.Text = "Заполнено " + tmpStatCount + " показателей из " + StatusList.Count();
                        }
                        else if (StatusList[0] == 1)
                        {
                            Label1.Text = "Данные возвращены на доработку";
                            Label2.Text = "Данные возвращены на доработку";
                        }
                        else if (StatusList[0] == 3)
                        {
                            Label1.Text = "Данные отправлены на утверждение";
                            Label2.Text = "Данные отправлены на утверждение";
                        }
                        else if (StatusList[0] == 4)
                        {
                            Label1.Text = "Данные утверждены";
                            Label2.Text = "Данные утверждены";
                        }
                        Label2.Text = "Осталось " + dateCount + " дней до закрытия отчёта";
                        if ((DateTime.Now > endDate))
                        {
                            Label2.Text = "";
                        }
                        #endregion
                    }
                    else if (mode == 2)
                    {
                        #region
                        GoBackButton.Visible = true;
                        GoBackButton.Text = "Вернуться в меню без утверждения";

                        ButtonSave.Visible = true;
                        ButtonSave.Text = "Утвердить данные";

                        UpnDownButton.Visible = true;
                        UpnDownButton.Text = "Вернуть отчёт на доработку";

                        TextBox1.Visible = true;

                        Label1.Text = "Утверждение данных";
                        Label2.Text = "Осталось " + dateCount + " дней до закрытия отчёта";
                        if ((DateTime.Now > endDate))
                        {
                            Label2.Text = "";
                        }


                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                        "Confirm", script2);

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                        "ConfirmOn", script3);

                        ButtonSave.Attributes.Add("OnClick", "return ConfirmSubmitA();");
                        UpnDownButton.Attributes.Add("OnClick", "return ConfirmSubmitOn();");
                        #endregion
                    }
                    else if (mode == 4)
                    {
                        #region
                        GoBackButton.Visible = true;
                        GoBackButton.Text = "Назад";

                        ButtonSave.Visible = true;
                        ButtonSave.Text = "Сохранить внесенные данные";

                        UpnDownButton.Visible = false;
                        UpnDownButton.Text = "Отправить отчёт на утверждение";

                        TextBox1.Visible = false;


                                Label1.Text = "Заполнено " + tmpStatCount + " показателей из " + StatusList.Count();
                                Label3.Text = "Заполнено " + tmpStatCount + " показателей из " + StatusList.Count();
                                UpnDownButton.Enabled = false;

                        ViewState["AllCnt"] = StatusList.Count();

                        Label2.Text = "Осталось " + dateCount + " дней до закрытия отчёта";
                        if ((DateTime.Now > endDate))
                        {

                            Label2.Text = "";
                        }
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                         "Confirm", script);
                        UpnDownButton.Attributes.Add("OnClick", "return ConfirmSubmit();");




                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "window.onbeforeunload = function() { return 'Date will be lost: are you sure?'; };", true);
                        ButtonSave.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        Button1.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        UpnDownButton.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        GoBackButton.OnClientClick = "window.onbeforeunload = null; document.getElementById('LoadPanel_').style.visibility = 'visible'; ";
                        #endregion
                    }
                    GridviewCollectedBasicParameters.DataSource = dataTable;
                    for (int j = 0; j < additionalColumnCount; j++)
                    {
                        GridviewCollectedBasicParameters.Columns[j + 4].Visible = true;
                        GridviewCollectedBasicParameters.Columns[j + 4].HeaderText = columnNames[j];
                    }
                    GridviewCollectedBasicParameters.DataBind();
              //  }
/*

                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                    "window.onload = null", true);
                Panel mypanel = (Panel)(Master.FindControl("loading"));

                ViewState["IsPostBack"] = false;
                mypanel.Visible = false;
*/
            }
        }
        protected void ButtonSave_Click(object sender, EventArgs e) //сохранение данных и пожтверждение данных
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            if (ViewState["CollectedBasicParametersTable"] != null && ViewState["CurrentReportArchiveID"] != null)
            {
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                Dictionary<int, double> tempDictionary = new Dictionary<int, double>();

                int UserID = UserSer.Id;
                int ReportArchiveID;
                ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
               /* UsersTable user = (from a in KPIWebDataContext.UsersTable
                                   where a.UsersTableID == UserSer.Id
                                   select a).FirstOrDefault();
                */
                int userTableID  = (int)ViewState["userTableID"];
           //     UsersTable userTable = (UsersTable)ViewState["userTable"];
                UsersTable userTable = (from a in KPIWebDataContext.UsersTable
                                        where a.UsersTableID == userTableID
                                   select a).FirstOrDefault();

                DataTable collectedBasicParametersTable = (DataTable)ViewState["CollectedBasicParametersTable"];
                int columnCnt = (int)ViewState["ValueColumnCnt"];

                Serialization modeSer = (Serialization)Session["mode"];
                if (modeSer == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
                }
                int mode = modeSer.mode;

                if ((mode == 0)||(mode == 4)) //сохранение данных
                {
                    #region save data
                    
                    //int allCnt=0; 
                    int notNullCnt = 0;
                    if (collectedBasicParametersTable.Rows.Count > 0)
                    {
                        int rowIndex = 0;
                        for (int i = 1; i <= collectedBasicParametersTable.Rows.Count; i++) //в каждой строчке
                        {
                            /// //сохраним вложенные данные
                            for (int k = 0; k < columnCnt; k++) // пройдемся по каждой колонке
                            {
                                TextBox textBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("Value" + k.ToString());
                                Label label = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("CollectId" + k.ToString());

                                if (textBox != null && label != null)
                                {
                                    //allCnt++;
                                    double collectedValue = double.NaN;
                                    if (textBox.Text.IsFloat())
                                    {
                                        notNullCnt++;
                                        collectedValue = Convert.ToDouble(textBox.Text);
                                    }
                                    int collectedBasicParametersTableID = -1;
                                    if (int.TryParse(label.Text, out collectedBasicParametersTableID) &&
                                        collectedBasicParametersTableID > -1)
                                        tempDictionary.Add(collectedBasicParametersTableID, collectedValue);
                                }
                            }
                            rowIndex++;
                        }
                    }
                    if (tempDictionary.Count > 0)
                    {
                        //Список ранее введенных пользователем данных для данной кампании (отчёта)

                        List<CollectedBasicParametersTable> сollectedBasicParametersTable = new List<CollectedBasicParametersTable>();

                        foreach (var currentLine in tempDictionary)
                        {
                            сollectedBasicParametersTable.Add((from a in KPIWebDataContext.CollectedBasicParametersTable
                                                                  where a.CollectedBasicParametersTableID == currentLine.Key
                                                                   select a).FirstOrDefault());
                        }
                        

                        string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                        foreach (var сollectedBasicParameter in сollectedBasicParametersTable)
                        {
                            double tmpD = (from item in tempDictionary
                                           where item.Key == сollectedBasicParameter.CollectedBasicParametersTableID
                                           select item.Value).FirstOrDefault();
                            if (double.IsNaN(tmpD))
                            {
                                сollectedBasicParameter.CollectedValue = null;
                            }
                            else
                            {
                                сollectedBasicParameter.CollectedValue = tmpD;
                            }

                            сollectedBasicParameter.LastChangeDateTime = DateTime.Now;
                            сollectedBasicParameter.UserIP = localIP;
                            if (mode !=4)
                            сollectedBasicParameter.Status = сollectedBasicParameter.CollectedValue == null ? 0 : 2;
                        }
                        KPIWebDataContext.SubmitChanges();
                    }
                    //надо рассчитать рассчетные
                    AutoCalculateAfterSave calculate = new AutoCalculateAfterSave();
                    calculate.AutoCalculate(ReportArchiveID,UserID,(int)userTable.FK_ThirdLevelSubdivisionTable,3,null,0);
                    //Calculate.CalcCalculate(ReportArchiveID, userTable);
                    int AllCnt = (int)ViewState["AllCnt"];
                    if (AllCnt == notNullCnt)
                    {
                        string tmpStr = "";
                        IsMaster newMaster = (IsMaster)Session["IsMaster"];
                        if (newMaster != null)
                        {
                            tmpStr = " as director with MasterKey=" + newMaster.MPassword + " ";
                        }

                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RT0: User " + (string)ViewState["login"] + tmpStr + " save data in report ID = " + paramSerialization.ReportStr + "All indicators are filled" + " from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Все показатели заполнены. Необходимо отправить отчёт на утверждение');" +
                            "document.location = '../Reports_/FillingTheReport.aspx';", true);


                    }
                    else
                    {
                        string tmpStr = "";
                        IsMaster newMaster = (IsMaster)Session["IsMaster"];
                        if (newMaster != null)
                        {
                            tmpStr = " as director with MasterKey=" + newMaster.MPassword + " ";
                        }

                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RT1: User " + (string)ViewState["login"] + tmpStr + " save data in report ID = " + paramSerialization.ReportStr + "Filled " + notNullCnt + " indicators from " + AllCnt + " Ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Данные сохранены на сервере. Заполнено " + notNullCnt + " показателей из " + AllCnt + ", для отправки отчёта необходимо заполнить еще " + (AllCnt - notNullCnt) + " показателя.');" +
                            "document.location = '../Reports_/FillingTheReport.aspx';", true);

                    }

                    #endregion
                }
                else if (mode == 1) // просмотр
                {
                    Response.Redirect("~/Reports_/ChooseReport.aspx");
                }
                else if (mode == 2) // подтверждение 
                {
                    #region confirm all
                    if (GridviewCollectedBasicParameters.Rows.Count > 0)
                    {
                        List<CollectedBasicParametersTable> CollectedToChange = (from a in KPIWebDataContext.CollectedBasicParametersTable
                                                                                 where a.FK_ThirdLevelSubdivisionTable == userTable.FK_ThirdLevelSubdivisionTable
                                                                                 && a.FK_ReportArchiveTable == ReportArchiveID
                                                                                 && a.Active == true
                                                                                 select a).ToList();
                        foreach (CollectedBasicParametersTable CollectedBasic in CollectedToChange)
                        {
                            CollectedBasic.Status = 4;
                        }
                        KPIWebDataContext.SubmitChanges();

                        string tmpStr = "";
                        IsMaster newMaster = (IsMaster)Session["IsMaster"];
                        if (newMaster != null)
                        {
                            tmpStr = " as director with MasterKey=" + newMaster.MPassword + " ";
                        }

                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0RT3: User " + (string)ViewState["login"] + tmpStr + " confirm data in report ID = " + paramSerialization.ReportStr + " from ip: " + Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault());
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Вы утвердили данные всех базовых показателей. Отчёт отправлен и доступен только в режиме \"Просмотр\".');" +
                            "document.location = '../Default.aspx';", true);

                        #region
                        UsersTable UserToSend = (from a in KPIWebDataContext.UsersTable
                                                 where a.UsersTableID == UserID
                                                 select a).FirstOrDefault();
                        ReportArchiveTable CurrentReport = (from a in KPIWebDataContext.ReportArchiveTable
                                                            where a.ReportArchiveTableID == Convert.ToInt32(paramSerialization.ReportStr)
                                                            select a).FirstOrDefault();


                        if (UserToSend == null)
                        {

                        }
                        else
                        {
                            string reportName = "-";
                            if (CurrentReport != null)
                            {
                                reportName = CurrentReport.Name;
                            }

                            string pdfPath = CreatePdf();
                            EmailTemplate EmailParams = (from a in KPIWebDataContext.EmailTemplate
                                                         where a.Name == "DataConfirmed"
                                                         && a.Active == true
                                                         select a).FirstOrDefault();
                            if (EmailParams != null)
                            {
                                Action.MassMailing(UserToSend.Email, EmailParams.EmailTitle,
                                    EmailParams.EmailContent.Replace("#SiteName#", ConfigurationManager.AppSettings.Get("SiteName")).Replace("#ReportName#", reportName), pdfPath);

                                BasicParametersTable BasicConnectedToUser = (from a in KPIWebDataContext.BasicParametersTable
                                                                             join b in KPIWebDataContext.BasicParametrsAndUsersMapping
                                                                                 on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                             where b.FK_UsersTable == UserToSend.UsersTableID
                                                                             && b.CanEdit == true
                                                                             && b.Active == true
                                                                             && a.Active == true
                                                                             select a).FirstOrDefault();

                                UsersTable UserToSend2 = (from a in KPIWebDataContext.UsersTable
                                                          join b in KPIWebDataContext.BasicParametrsAndUsersMapping
                                                              on a.UsersTableID equals b.FK_UsersTable
                                                          where b.FK_ParametrsTable == BasicConnectedToUser.BasicParametersTableID
                                                           && b.CanConfirm == true
                                                           && a.Active == true
                                                           && b.Active == true
                                                           && ((a.FK_FirstLevelSubdivisionTable == UserToSend.FK_FirstLevelSubdivisionTable) || UserToSend.FK_FirstLevelSubdivisionTable == null)
                                                           && ((a.FK_SecondLevelSubdivisionTable == UserToSend.FK_SecondLevelSubdivisionTable) || UserToSend.FK_SecondLevelSubdivisionTable == null)
                                                           && ((a.FK_ThirdLevelSubdivisionTable == UserToSend.FK_ThirdLevelSubdivisionTable) || UserToSend.FK_ThirdLevelSubdivisionTable == null)
                                                           && ((a.FK_FourthLevelSubdivisionTable == UserToSend.FK_FourthLevelSubdivisionTable) || UserToSend.FK_FourthLevelSubdivisionTable == null)
                                                           && ((a.FK_FifthLevelSubdivisionTable == UserToSend.FK_FifthLevelSubdivisionTable) || UserToSend.FK_FifthLevelSubdivisionTable == null)
                                                          select a).FirstOrDefault();


                                if (UserToSend2.Email != UserToSend.Email) // если одно мыло на двоих то не отправляем 2 письмо
                                    Action.MassMailing(UserToSend2.Email, EmailParams.EmailTitle,
                                        EmailParams.EmailContent.Replace("#SiteName#", ConfigurationManager.AppSettings.Get("SiteName")).Replace("#ReportName#", reportName), pdfPath);
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        string tmpStr = "";
                        IsMaster newMaster = (IsMaster)Session["IsMaster"];
                        if (newMaster != null)
                        {
                            tmpStr = " as director as director with MasterKey=" + newMaster.MPassword + " ";
                        }

                        //error
                        LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0RTE1: Пользователь " + (string)ViewState["login"] + tmpStr + " сгенерировал ошибку 1 в отчете с ID = " + paramSerialization.ReportStr);
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ошибка'); document.location = '../Default.aspx'; ", true);

                    }
                    #endregion
                }
                else
                {
                    string tmpStr = "";
                    IsMaster newMaster = (IsMaster)Session["IsMaster"];
                    if (newMaster != null)
                    {
                        tmpStr = " as director with MasterKey=" + newMaster.MPassword + " ";
                    }

                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0RTE2 User " + (string)ViewState["login"] + tmpStr + " generate an error 2 in report c ID = " + paramSerialization.ReportStr);
                    //error
                }
            }
        }
        protected void GridviewCollectedBasicParameters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >=0)
            {
                Color color;
                Color confirmedColor = System.Drawing.Color.LimeGreen;
                Color disableColor = System.Drawing.Color.LightGray;
                if (col_ == 0)
                {
                    col_ = 1;
                    color = System.Drawing.Color.FloralWhite;
                }
                else
                {
                    col_ = 0;
                    color = System.Drawing.Color.GhostWhite;
                }
                int rowIndex = 0;
                e.Row.BackColor = color;
                Serialization modeSer = (Serialization)Session["mode"];
                if (modeSer == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
                }
                int mode = modeSer.mode;

                int columnCnt = (int)ViewState["ValueColumnCnt"];

                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                for (int i = 1; i <= columnCnt; i++)
                {
                    {
                        var lblMinutes = e.Row.FindControl("Value" + rowIndex) as TextBox;
                        var NotNullLbl = e.Row.FindControl("NotNull" + rowIndex) as Label;
                        if (NotNullLbl != null)
                        {
                            if (NotNullLbl.Text.Count() == 0)
                            {
                                lblMinutes.Visible = false;
                                if (e.Row.RowType == DataControlRowType.DataRow)
                                {

                                    DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                    //  lblMinutes.Attributes.Add("OnChange", "textChanged()");
                                    d.BackColor = disableColor;
                                    //d.CssClass = "DisableClass";
                                }
                            }
                            else
                            {
                                Label lbl = e.Row.FindControl("CollectId" + rowIndex) as Label;
                                RangeValidator Validator = e.Row.FindControl("Validate" + rowIndex) as RangeValidator;

                                int Status = Convert.ToInt32((from a in kpiWebDataContext.CollectedBasicParametersTable
                                                              where a.CollectedBasicParametersTableID == Convert.ToInt32(lbl.Text)
                                                              select a.Status).FirstOrDefault());

                                if (mode == 0) // редактировать
                                {
                                    #region edit
                                    int type = Convert.ToInt32((from a in kpiWebDataContext.CollectedBasicParametersTable
                                                                join b in kpiWebDataContext.BasicParametrAdditional
                                                                    on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                                where a.CollectedBasicParametersTableID == Convert.ToInt32(lbl.Text)
                                                                select b.DataType).FirstOrDefault());

                                    if (Status == 4) // данные подтверждены первым уровнем
                                    {
                                        lblMinutes.ReadOnly = true;
                                        DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                        d.BackColor = confirmedColor;
                                        lblMinutes.BackColor = confirmedColor;
                                        if (Validator != null)
                                        {
                                            Validator.Enabled = false;
                                        }
                                    }
                                    else
                                    {
                                        DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                        d.BackColor = color;
                                        lblMinutes.BackColor = color;
                                        #region validator choose
                                        if (Validator != null)
                                        {
                                            if (type == 0)
                                            {
                                                Validator.MinimumValue = "0";
                                                Validator.MaximumValue = "1";
                                                Validator.Type = ValidationDataType.Integer;
                                                Validator.Text = "Только 0 или 1";
                                            }
                                            if (type == 1)
                                            {
                                                Validator.MinimumValue = "0";
                                                Validator.MaximumValue = "1000000";
                                                Validator.Type = ValidationDataType.Integer;
                                                Validator.Text = "Только целочисленное значение";
                                            }
                                            if (type == 2)
                                            {
                                                Validator.MinimumValue = "0";
                                                Validator.MaximumValue = "1000000000000";
                                                Validator.Type = ValidationDataType.Double;
                                                Validator.Text = "Только цифры и запятая";
                                            }
                                        }
                                        #endregion
                                    }
                                    #endregion
                                }
                                else if (mode == 1) //смотреть
                                {
                                    #region view
                                    lblMinutes.ReadOnly = true;
                                    lblMinutes.BackColor = color;
                                    /*
                                    Color tmpColor = Color.Red;

                                    if (Status == 0) // не должны сюда попадать
                                    {
                                        tmpColor = Color.Blue;
                                    }
                                    else if (Status == 1) // вернули на доработку
                                    {
                                        tmpColor = Color.Orange;
                                    }
                                    else if (Status == 2) // данные внесены
                                    {
                                        tmpColor = Color.Yellow;
                                    }
                                    else if (Status == 3)// данные отпралены на верификацию
                                    {
                                        tmpColor = Color.GreenYellow;
                                    }
                                    else if (Status == 4) // данные верифицированы
                                    {
                                        tmpColor = Color.Green;
                                    }

                                    DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                    d.BackColor = tmpColor;
                                    lblMinutes.BackColor = tmpColor;
                                    */

                                    if (Validator != null)
                                    {
                                        Validator.Enabled = false;
                                    }
                                    #endregion
                                }
                                else if (mode == 2) // утверждать
                                {
                                    #region confirm
                                    lblMinutes.ReadOnly = true;
                                    Validator.Enabled = false;
                                    lblMinutes.BackColor = color;
                                    /* // страница подтверждения с чексбоксами
                                    var chBox = e.Row.FindControl("Checked" + rowIndex) as CheckBox;
                                    chBox.Visible = true;
                                                               
                                    lblMinutes.ReadOnly = true;
                                    Validator.MinimumValue = "0";
                                    Validator.MaximumValue = "10000000";
                                    Validator.Type = ValidationDataType.Double;
                                    Validator.Text = "Невозможно утвердить";
                                                                
                                    if (Status == 4) // данные подтверждены
                                    {                                    
                                        DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                        d.BackColor = confirmedColor;
                                        lblMinutes.BackColor = confirmedColor;
                                        chBox.Checked = true;
                                        chBox.Enabled = false;
                                        chBox.BackColor = confirmedColor;                                   
                                    }
                                    else
                                    {
                                        //lblMinutes.ReadOnly = false;
                                        DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                        d.BackColor = color;
                                        lblMinutes.BackColor = color;
                                   
                                        chBox.Checked = false;
                                        chBox.Enabled = true;
                                        chBox.BackColor = color;
                                     
                                    }*/
                                    #endregion
                                }
                                else if (mode == 4)
                                {
                                    #region Masteredit
                                    int type = Convert.ToInt32((from a in kpiWebDataContext.CollectedBasicParametersTable
                                                                join b in kpiWebDataContext.BasicParametrAdditional
                                                                    on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                                                where a.CollectedBasicParametersTableID == Convert.ToInt32(lbl.Text)
                                                                select b.DataType).FirstOrDefault());


                                    DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                    d.BackColor = color;
                                    lblMinutes.BackColor = color;

                                    if (Validator != null)
                                    {
                                        if (type == 0)
                                        {
                                            Validator.MinimumValue = "0";
                                            Validator.MaximumValue = "1";
                                            Validator.Type = ValidationDataType.Integer;
                                            Validator.Text = "Только 0 или 1";
                                        }
                                        if (type == 1)
                                        {
                                            Validator.MinimumValue = "0";
                                            Validator.MaximumValue = "1000000";
                                            Validator.Type = ValidationDataType.Integer;
                                            Validator.Text = "Только целочисленное значение";
                                        }
                                        if (type == 2)
                                        {
                                            Validator.MinimumValue = "0";
                                            Validator.MaximumValue = "1000000000000";
                                            Validator.Type = ValidationDataType.Double;
                                            Validator.Text = "Только цифры и запятая";
                                        }
                                    }

                                    #endregion
                                }
                            }
                        }
                        rowIndex++;
                    }
                }
            }
        }
        public string CreatePdf()
        {
            int[] Widhts = new int[40];
            for (int i = 0; i < 40; i++)
                Widhts[i] = 0;
            Widhts[0] = 2;
            Widhts[2] = 10;
            int colcnt = (int)ViewState["ValueColumnCnt"];
            for (int i = 0; i < colcnt; i++)
            {
                Widhts[i + 4] = 2;
            }

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                      (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            ReportArchiveTable CurrentReport = (from a in kPiDataContext.ReportArchiveTable
                                                where a.ReportArchiveTableID == Convert.ToInt32(paramSerialization.ReportStr)
                                                select a).FirstOrDefault();
            string filePath = PDFCreate.ExportPDF(GridviewCollectedBasicParameters, Widhts, " ", 3, colcnt, "Название отчета: " + CurrentReport.Name, "Ваш email адрес: " + userTable.Email, PDFCreate.StructLastName(userTable.UsersTableID));
            return filePath;
        }
        protected void Button1_Click(object sender, EventArgs e) // экспорт в excel
        {
            Response.ContentType = "Application/pdf";
            Response.TransmitFile(CreatePdf());
            Response.End();

        }
        protected void Button2_Click(object sender, EventArgs e) // вернуться в меню 
        {
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int mode = modeSer.mode;
            if (mode == 4)
            {
                Response.Redirect("~/Director/DViewThird.aspx");
            }
            Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }
        protected void Button3_Click(object sender, EventArgs e) // отправка на доработку и возвращение с доработки
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int userID = UserSer.Id;

            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int mode = modeSer.mode;

            int columnCnt = (int)ViewState["ValueColumnCnt"];
            if (columnCnt != null)
            {
                if (GridviewCollectedBasicParameters.Rows.Count > 0)
                {
                    if ((mode == 0) || (mode == 2))
                    {
                        if (mode == 0) //отправляем данные на подтверждение
                        {
                            bool wasReturned = false; // данные сейчас на доработке?
                            #region send to confirm

                            for (int k = 0; k < columnCnt; k++) // пройдемся по каждой колонке
                            {
                                for (int i = 0; i < GridviewCollectedBasicParameters.Rows.Count; i++)
                                {
                                    Label label =
                                        (Label)
                                            GridviewCollectedBasicParameters.Rows[i].FindControl("CollectId" +
                                                                                                 k.ToString());
                                    if (label != null)
                                    {
                                        if (label.Text == "")
                                        {

                                        }
                                        else
                                        {
                                            CollectedBasicParametersTable tmpColTable =
                                                (from a in kPiDataContext.CollectedBasicParametersTable
                                                 where
                                                     a.CollectedBasicParametersTableID == Convert.ToInt32(label.Text)
                                                 select a).FirstOrDefault();
                                            if (tmpColTable != null)
                                            {
                                                if ((tmpColTable.Status == 2) || (tmpColTable.Status == 1))
                                                {
                                                    if ((tmpColTable.Status == 1))
                                                    {
                                                        wasReturned = true;
                                                    }
                                                    tmpColTable.Status = 3;
                                                    kPiDataContext.SubmitChanges();
                                                }
                                                else
                                                {
                                                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0FRE3: Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 3 в отчете с ID = ");
                                                }
                                            }
                                            else
                                            {
                                                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0FRE4: Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 4 в отчете с ID = ");
                                            }
                                        }
                                    }
                                }
                            }
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Отчёт отправлен на утверждение');" +
                                "document.location = '../Default.aspx';", true);

                            #region
                            BasicParametersTable BasicConnectedToUser = (from a in kPiDataContext.BasicParametersTable
                                                                         join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                                             on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                         where b.FK_UsersTable == userID
                                                                         && b.CanEdit == true
                                                                         && b.Active == true
                                                                         && a.Active == true
                                                                         select a).FirstOrDefault();

                            UsersTable CurrentUser = (from a in kPiDataContext.UsersTable
                                                      where a.UsersTableID == userID
                                                      select a).FirstOrDefault();

                            UsersTable UserToSend = (from a in kPiDataContext.UsersTable
                                                     join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                         on a.UsersTableID equals b.FK_UsersTable
                                                     where b.FK_ParametrsTable == BasicConnectedToUser.BasicParametersTableID
                                                      && b.CanConfirm == true
                                                      && a.Active == true
                                                      && b.Active == true
                                                      && ((a.FK_FirstLevelSubdivisionTable == CurrentUser.FK_FirstLevelSubdivisionTable) || CurrentUser.FK_FirstLevelSubdivisionTable == null)
                                                      && ((a.FK_SecondLevelSubdivisionTable == CurrentUser.FK_SecondLevelSubdivisionTable) || CurrentUser.FK_SecondLevelSubdivisionTable == null)
                                                      && ((a.FK_ThirdLevelSubdivisionTable == CurrentUser.FK_ThirdLevelSubdivisionTable) || CurrentUser.FK_ThirdLevelSubdivisionTable == null)
                                                      && ((a.FK_FourthLevelSubdivisionTable == CurrentUser.FK_FourthLevelSubdivisionTable) || CurrentUser.FK_FourthLevelSubdivisionTable == null)
                                                      && ((a.FK_FifthLevelSubdivisionTable == CurrentUser.FK_FifthLevelSubdivisionTable) || CurrentUser.FK_FifthLevelSubdivisionTable == null)
                                                     select a).FirstOrDefault();

                            if (UserToSend == null)
                            {

                            }
                            else
                            {
                                EmailTemplate EmailParams;
                                if (wasReturned)
                                {
                                    EmailParams = (from a in kPiDataContext.EmailTemplate
                                                   where a.Name == "DataSendToConfirmAfterRemake"
                                                                && a.Active == true
                                                   select a).FirstOrDefault();
                                }
                                else
                                {
                                    EmailParams = (from a in kPiDataContext.EmailTemplate
                                                   where a.Name == "DataSendToConfirm"
                                                   && a.Active == true
                                                   select a).FirstOrDefault();
                                }

                                if (EmailParams != null)
                                    Action.MassMailing(UserToSend.Email, EmailParams.EmailTitle,
                                        EmailParams.EmailContent.Replace("#SiteName#", ConfigurationManager.AppSettings.Get("SiteName")), null);
                            }
                            #endregion
                            #endregion
                        }
                        else // (mode == 2) // данные обратно на доработку
                        {
                            #region send back to correct

                            for (int k = 0; k < columnCnt; k++) // 
                            {
                                for (int i = 0; i < GridviewCollectedBasicParameters.Rows.Count; i++)
                                {
                                    Label label =
                                        (Label)
                                            GridviewCollectedBasicParameters.Rows[i].FindControl("CollectId" +
                                                                                                 k.ToString());

                                    if (label != null)
                                    {
                                        if (label.Text == "")
                                        {

                                        }
                                        else
                                        {
                                            CollectedBasicParametersTable tmpColTable =
                                                (from a in kPiDataContext.CollectedBasicParametersTable
                                                 where
                                                     a.CollectedBasicParametersTableID == Convert.ToInt32(label.Text)
                                                 select a).FirstOrDefault();
                                            if (tmpColTable != null)
                                            {
                                                if (tmpColTable.Status == 3)
                                                {
                                                    tmpColTable.Status = 1;
                                                    kPiDataContext.SubmitChanges();
                                                }
                                                else
                                                {
                                                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0FRE5: Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 5 в отчете с ID = ");
                                                }
                                            }
                                            else
                                            {
                                                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0FRE6: Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 6 в отчете с ID = ");
                                            }
                                        }
                                    }
                                }
                            }
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Отчёт отправлен на доработку');" +
                                "document.location = '../Default.aspx';", true);

                            #region
                            BasicParametersTable BasicConnectedToUser = (from a in kPiDataContext.BasicParametersTable
                                                                         join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                                             on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                         where b.FK_UsersTable == userID
                                                                         && b.CanConfirm == true
                                                                         && b.Active == true
                                                                         && a.Active == true
                                                                         select a).FirstOrDefault();

                            UsersTable CurrentUser = (from a in kPiDataContext.UsersTable
                                                      where a.UsersTableID == userID
                                                      select a).FirstOrDefault();

                            UsersTable UserToSend = (from a in kPiDataContext.UsersTable
                                                     join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                         on a.UsersTableID equals b.FK_UsersTable
                                                     where b.FK_ParametrsTable == BasicConnectedToUser.BasicParametersTableID
                                                      && b.CanEdit == true
                                                      && a.Active == true
                                                      && b.Active == true
                                                        && ((a.FK_FirstLevelSubdivisionTable == CurrentUser.FK_FirstLevelSubdivisionTable) || CurrentUser.FK_FirstLevelSubdivisionTable == null)
                                                         && ((a.FK_SecondLevelSubdivisionTable == CurrentUser.FK_SecondLevelSubdivisionTable) || CurrentUser.FK_SecondLevelSubdivisionTable == null)
                                                         && ((a.FK_ThirdLevelSubdivisionTable == CurrentUser.FK_ThirdLevelSubdivisionTable) || CurrentUser.FK_ThirdLevelSubdivisionTable == null)
                                                         && ((a.FK_FourthLevelSubdivisionTable == CurrentUser.FK_FourthLevelSubdivisionTable) || CurrentUser.FK_FourthLevelSubdivisionTable == null)
                                                         && ((a.FK_FifthLevelSubdivisionTable == CurrentUser.FK_FifthLevelSubdivisionTable) || CurrentUser.FK_FifthLevelSubdivisionTable == null)
                                                     select a).FirstOrDefault();

                            if (UserToSend == null)
                            {

                            }
                            else
                            {

                                EmailTemplate EmailParams = (from a in kPiDataContext.EmailTemplate
                                                             where a.Name == "DataSendToRemake"
                                                             && a.Active == true
                                                             select a).FirstOrDefault();
                                if (EmailParams != null)
                                    Action.MassMailing(UserToSend.Email, EmailParams.EmailTitle,
                                        EmailParams.EmailContent.Replace("#SiteName#", ConfigurationManager.AppSettings.Get("SiteName")).Replace("#Comment#", TextBox1.Text), null);
                            }

                            #endregion
                            #endregion
                        }
                    }
                }
                else
                {
                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "0FRE7: Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 7 в отчете с ID = ");
                }
            }
            else
            {
                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 8 в отчете с ID = ");
            }
        }
    }
}