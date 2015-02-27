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

namespace KPIWeb.Reports
{
    public partial class FillingTheReport : System.Web.UI.Page
    {
        public int col_ = 0;

        protected void CalcCalculate(int ReportArchiveID, int UserID, UsersTable user, int l_0, int l_1, int l_2, int l_3, int l_4, int l_5)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            List<BasicParametersTable> KafBasicParams =
            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
             join b in kpiWebDataContext.BasicParametersTable
             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
             on b.BasicParametersTableID equals c.FK_ParametrsTable
             join d in kpiWebDataContext.BasicParametrAdditional
             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
             where
                   a.FK_ReportArchiveTable == ReportArchiveID  //из нужного отчета
                && c.FK_UsersTable == UserID // свяный с пользователем
                && d.SubvisionLevel == 3 //нужный уровень заполняющего
                && a.Active == true  // запись в таблице связей показателя и отчета активна

                && c.Active == true  // запись в таблице связей показателя и пользователей активна
                && d.Calculated == true // этот показатель нужно вводить а не считать
             select b).ToList();

            //узнали показатели кафедры(отчет,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)          
            foreach (BasicParametersTable basicParam in KafBasicParams) //пройдемся по показателям
            {
                //если этото параметр и эта кафедра дружат
                ThirdLevelParametrs thirdParametrs =
                    (from a in kpiWebDataContext.ThirdLevelParametrs
                     where a.ThirdLevelParametrsID == l_3
                     select a).FirstOrDefault();
                // узнали параметры специальности
                BasicParametrAdditional basicParametrs =
                    (from a in kpiWebDataContext.BasicParametrAdditional
                     where
                         a.BasicParametrAdditionalID == basicParam.BasicParametersTableID
                     select a).FirstOrDefault();
                //узнали параметры базового показателя
                if ((thirdParametrs.CanGraduate == true) || (basicParametrs.IsGraduating == false)) //фильтруем базовые показатели для невыпускающих кафедр
                {                    
                    CollectedBasicParametersTable collectedBasicTmp =
                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                         where a.FK_ZeroLevelSubdivisionTable ==    user.FK_ZeroLevelSubdivisionTable
                             && a.FK_FirstLevelSubdivisionTable ==  user.FK_FirstLevelSubdivisionTable
                             && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                             && a.FK_ThirdLevelSubdivisionTable ==  user.FK_ThirdLevelSubdivisionTable
                             && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                             && a.FK_ReportArchiveTable == ReportArchiveID
                         select a).FirstOrDefault();
                    if (collectedBasicTmp == null) // надо создать
                    {
                        collectedBasicTmp = new CollectedBasicParametersTable();
                        collectedBasicTmp.Active = true;
                        collectedBasicTmp.FK_UsersTable = UserID;
                        collectedBasicTmp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                        collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
                        ///поехали страдать
                        double tmp = 0;
                        if (basicParam.AbbreviationEN == "a_Och_M") tmp = Convert.ToDouble(
                            (from a in kpiWebDataContext.CollectedBasicParametersTable 
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FK_FourthLevelSubdivisionTable equals b.FourthLevelParametrsID
                             join c in kpiWebDataContext.ThirdLevelParametrs
                             on a.FK_ThirdLevelSubdivisionTable equals c.ThirdLevelParametrsID
                             join d in kpiWebDataContext.FourthLevelSubdivisionTable
                             on a.FK_FourthLevelSubdivisionTable equals d.FourthLevelSubdivisionTableID
                             join e in kpiWebDataContext.SpecializationTable
                             on d.FK_Specialization equals  e.SpecializationTableID
                             where 
                                a.FK_ZeroLevelSubdivisionTable ==    user.FK_ZeroLevelSubdivisionTable
                             && a.FK_FirstLevelSubdivisionTable ==  user.FK_FirstLevelSubdivisionTable
                             && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                             && a.FK_ThirdLevelSubdivisionTable ==  user.FK_ThirdLevelSubdivisionTable
                             && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                             && a.FK_ReportArchiveTable == ReportArchiveID
                             && b.SpecType == 3
                             && (e.FK_FieldOfExpertise == 10 || e.FK_FieldOfExpertise == 11 || e.FK_FieldOfExpertise == 12)
                                 select a.CollectedValue).Sum());

                        //прекратили
                        collectedBasicTmp.CollectedValue = tmp;
                        collectedBasicTmp.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                        collectedBasicTmp.LastChangeDateTime = DateTime.Now;
                        collectedBasicTmp.SavedDateTime = DateTime.Now;
                        collectedBasicTmp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                        collectedBasicTmp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                        collectedBasicTmp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                        collectedBasicTmp.FK_ThirdLevelSubdivisionTable = user.FK_ThirdLevelSubdivisionTable;

                        kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                        kpiWebDataContext.SubmitChanges();
                    }
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable =
                      (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if (userTable.AccessLevel != 0)
            {
                Response.Redirect("~/Default.aspx");
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            /////////////////////////////////////////////////////////////////////////
            if (!Page.IsPostBack)
            {
                Serialization modeSer = (Serialization)Session["mode"];
                if (modeSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем
                ////////////////
                int UserID = UserSer.Id;
                int ReportArchiveID;
                ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                UsersTable user = (from a in kpiWebDataContext.UsersTable
                                   where a.UsersTableID == UserID
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
                ////ранги пользователя
                /// -1 никто ниоткуда/// 0 с Кфу /// 1 с Академии/// 2 с Факультета/// 3 с кафедры/// 4 с специализация/// 5 с под специализацией,пока нет
                ///узнали все о пользователе
                List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
                /////создаем дататейбл
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));

                for (int k = 0; k <= 10; k++)  //создаем кучу полей
                {
                    dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CollectId" + k.ToString(), typeof(string)));
                }
                int additionalColumnCount = 0;
                switch (userLevel) // это штука пока будет работать только для пользователя кафедры
                {
                    case 0: //вытаскиваем все универы
                        {
                            break;
                        }
                    case 1: //вытаскиваем все факультеты
                        {

                            break;
                        }
                    case 2://я Факультет
                        {
                            break;
                        }
                    case 3: //я кафедра
                        {
                            List<BasicParametersTable> KafBasicParams =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                             on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where
                                   a.FK_ReportArchiveTable == ReportArchiveID  //из нужного отчета
                                && c.FK_UsersTable == UserID // свяный с пользователем
                                && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                && a.Active == true  // запись в таблице связей показателя и отчета активна

                                && (((c.CanEdit == true) && mode == 0)
                                || ((c.CanView == true) && mode == 1)
                                || ((c.CanConfirm == true) && mode == 2)) // фильтруем по правам пользователя

                                && c.Active == true  // запись в таблице связей показателя и пользователей активна
                                && d.Calculated == false // этот показатель нужно вводить а не считать
                             select b).ToList();

                            //узнали показатели кафедры(отчет,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)          
                            foreach (BasicParametersTable basicParam in KafBasicParams) //пройдемся по показателям
                            {
                                //если этото параметр и эта кафедра дружат
                                ThirdLevelParametrs thirdParametrs =
                                    (from a in kpiWebDataContext.ThirdLevelParametrs
                                     where a.ThirdLevelParametrsID == l_3
                                     select a).FirstOrDefault();
                                // узнали параметры специальности
                                BasicParametrAdditional basicParametrs =
                                    (from a in kpiWebDataContext.BasicParametrAdditional
                                     where
                                         a.BasicParametrAdditionalID == basicParam.BasicParametersTableID
                                     select a).FirstOrDefault();
                                //узнали параметры базового показателя
                                if ((thirdParametrs.CanGraduate == true) || (basicParametrs.IsGraduating == false)) //фильтруем базовые показатели для невыпускающих кафедр
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                                    dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;
                                    dataRow["Name"] = basicParam.Name;
                                    CollectedBasicParametersTable collectedBasicTmp =
                                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                                         where a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                                             && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                                             && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                                             && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                                             && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                             && a.FK_ReportArchiveTable == ReportArchiveID
                                         select a).FirstOrDefault();
                                    if (collectedBasicTmp == null) // надо создать
                                    {
                                        collectedBasicTmp = new CollectedBasicParametersTable();
                                        collectedBasicTmp.Active = true;
                                        collectedBasicTmp.FK_UsersTable = UserID;
                                        collectedBasicTmp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                                        collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
                                        collectedBasicTmp.CollectedValue = 0;
                                        collectedBasicTmp.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                                        collectedBasicTmp.LastChangeDateTime = DateTime.Now;
                                        collectedBasicTmp.SavedDateTime = DateTime.Now;
                                        collectedBasicTmp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                                        collectedBasicTmp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                                        collectedBasicTmp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                                        collectedBasicTmp.FK_ThirdLevelSubdivisionTable = user.FK_ThirdLevelSubdivisionTable;

                                        kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                                        kpiWebDataContext.SubmitChanges();
                                    }
                                    dataRow["Value0"] = collectedBasicTmp.CollectedValue.ToString();
                                    dataRow["CollectId0"] = collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                            columnNames.Add("Кафедра:\r\n" + (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                              where a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                                              select a.Name).FirstOrDefault());
                            //Кафедра готова
                            additionalColumnCount += 1;

                            if ((from zz in kpiWebDataContext.ThirdLevelParametrs
                                 where zz.ThirdLevelParametrsID == l_3
                                 select zz.CanGraduate).FirstOrDefault() == true) // кафедра выпускающая значит специальности есть
                            {
                                List<BasicParametersTable> SpecBasicParams =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where a.FK_ReportArchiveTable == ReportArchiveID //для отчета
                                           && d.SubvisionLevel == 4 // для уровня заполняющего
                                           && d.Calculated == false //только вводимые параметры
                                           && c.FK_UsersTable == UserID // связаннаые с пользователем
                                           && a.Active == true

                                           && (((c.CanEdit == true) && mode == 0)
                                            || ((c.CanView == true) && mode == 1)
                                            || ((c.CanConfirm == true) && mode == 2)) // фильтруем по правам пользователя

                                           && c.Active == true
                                     select b).ToList();
                                //Получили показатели разрешенные пользователю в данном отчете
                                List<FourthLevelSubdivisionTable> Specialzations =
                                    (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                     where a.FK_ThirdLevelSubdivisionTable == l_3
                                           && a.Active == true
                                     select a).ToList();
                                //Получили список специальностей для кафедры под пользователем 

                                foreach (FourthLevelSubdivisionTable spec in Specialzations)
                                {
                                    columnNames.Add("Специальность:\n\r" +
                                                    (from a in kpiWebDataContext.SpecializationTable
                                                     where a.SpecializationTableID == spec.FK_Specialization
                                                     select a.Name).FirstOrDefault().ToString());
                                    //запомнили название специальности // оно нам пригодится)
                                }

                                foreach (BasicParametersTable specBasicParam in SpecBasicParams)
                                {
                                    int i = additionalColumnCount;
                                    DataRow dataRow = dataTable.NewRow();
                                    BasicParametrAdditional basicParametrs =
                                           (from a in kpiWebDataContext.BasicParametrAdditional
                                            where
                                                a.BasicParametrAdditionalID == specBasicParam.BasicParametersTableID
                                            select a).FirstOrDefault();
                                    //узнали параметры базового показателя
                                    int j = 0;
                                    foreach (FourthLevelSubdivisionTable spec in Specialzations)
                                    {
                                        j = 0;//если хоть одной специальности базовый показатель нужен то мы его выведем
                                        FourthLevelParametrs fourthParametrs =
                                            (from a in kpiWebDataContext.FourthLevelParametrs
                                             where a.FourthLevelParametrsID == spec.FourthLevelSubdivisionTableID
                                             select a).FirstOrDefault();
                                        // узнали параметры специальности
                                        //если этото параметр и эта специальность дружат                                
                                        if (((fourthParametrs.IsForeignStudentsAccept == true) || (basicParametrs.ForForeignStudents == false)) //это для иностранцев
                                            && (fourthParametrs.SpecType == basicParametrs.SpecType) || (basicParametrs.SpecType == 0)) // это для деления на магистров аспирантов итд
                                        {
                                            j++; //потом проверка и следовательно БП нуно выводить
                                            CollectedBasicParametersTable collectedBasicTmp =
                                                (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                 where a.FK_BasicParametersTable == specBasicParam.BasicParametersTableID
                                                       && a.FK_ReportArchiveTable == ReportArchiveID
                                                       && (a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable)
                                                       && (a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable)
                                                       && (a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable)
                                                       && (a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable)
                                                       && (a.FK_FourthLevelSubdivisionTable == spec.FourthLevelSubdivisionTableID)
                                                 select a).FirstOrDefault();
                                            if (collectedBasicTmp == null)
                                            {
                                                collectedBasicTmp = new CollectedBasicParametersTable();
                                                collectedBasicTmp.Active = true;
                                                collectedBasicTmp.FK_UsersTable = UserID;
                                                collectedBasicTmp.FK_BasicParametersTable = specBasicParam.BasicParametersTableID;
                                                collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
                                                collectedBasicTmp.CollectedValue = 0;
                                                collectedBasicTmp.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                                                collectedBasicTmp.LastChangeDateTime = DateTime.Now;
                                                collectedBasicTmp.SavedDateTime = DateTime.Now;
                                                collectedBasicTmp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                                                collectedBasicTmp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                                                collectedBasicTmp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                                                collectedBasicTmp.FK_ThirdLevelSubdivisionTable = spec.FK_ThirdLevelSubdivisionTable;
                                                collectedBasicTmp.FK_FourthLevelSubdivisionTable = spec.FourthLevelSubdivisionTableID;
                                                kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                                                kpiWebDataContext.SubmitChanges();
                                            }
                                            dataRow["Name"] = specBasicParam.Name;
                                            dataRow["Value" + i] = collectedBasicTmp.CollectedValue.ToString();
                                            dataRow["CollectId" + i] = collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                                        }
                                        i++;
                                    }
                                    if (j > 0)
                                    {
                                        dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                                        dataRow["BasicParametersTableID"] = specBasicParam.BasicParametersTableID;
                                        dataTable.Rows.Add(dataRow);
                                    }
                                    ///////////////////////закинули все в дататейбл
                                }
                                additionalColumnCount += Specialzations.Count;
                            }
                            break;
                        }
                    case 4://пока рано//у нас нет ничего глубже специальности
                        {
                            break;
                        }
                    case 5://выводить нечего
                        {
                            break;
                        }
                    default: // если уровня вообще нет или он неправильно задан//хорошо бы ошибку вывести
                        {
                            break;
                        }
                }

                ViewState["CollectedBasicParametersTable"] = dataTable;
                ViewState["CurrentReportArchiveID"] = ReportArchiveID;
                ViewState["ValueColumnCnt"] = additionalColumnCount;

                if (mode == 0)
                {
                    ButtonSave.Text = "Сохранить внесенные данные";
                    Label1.Text = "Ввведите значения в таблицу показателей и нажмите кнопку внизу формы для сохранения данных";
                }
                else if
                    (mode == 1)
                {
                    Label1.Text = "Просмотр введенных данных";
                    ButtonSave.Text = "Вернуться в меню выбора отчета";
                }
                else
                    if (mode == 2)
                    {
                        Label1.Text = "Проверьте корректность введенных данных. Если данные верны, нажмите кнопку внизу формы";
                        ButtonSave.Text = "Подтвердить правильность введенных данных";
                    }

                GridviewCollectedBasicParameters.DataSource = dataTable;
                for (int j = 0; j < additionalColumnCount; j++)
                {
                    GridviewCollectedBasicParameters.Columns[j + 4].Visible = true;
                    GridviewCollectedBasicParameters.Columns[j + 4].HeaderText = columnNames[j];
                }
                GridviewCollectedBasicParameters.DataBind();
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (ViewState["CollectedBasicParametersTable"] != null && ViewState["CurrentReportArchiveID"] != null)
            {
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                Dictionary<int, double> tempDictionary = new Dictionary<int, double>();
                DataTable collectedBasicParametersTable = (DataTable)ViewState["CollectedBasicParametersTable"];
                int columnCnt = (int)ViewState["ValueColumnCnt"];

                Serialization modeSer = (Serialization)Session["mode"];
                if (modeSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int mode = modeSer.mode;

                if (collectedBasicParametersTable.Rows.Count > 0)
                {
                    int rowIndex = 0;
                    for (int i = 1; i <= collectedBasicParametersTable.Rows.Count; i++) //в каждой строчке
                    {
                        /// //сохраним вложенные данные
                        for (int k = 0; k < columnCnt; k++) // пройдемся по каждой колонке
                        {
                            TextBox textBox =
                                (TextBox)
                                    GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("Value" +
                                                                                                k.ToString());
                            Label label =
                                (Label)
                                    GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("CollectId" +
                                                                                                k.ToString());

                            if (textBox != null && label != null)
                            {
                                double collectedValue = -1;
                                if (double.TryParse(textBox.Text, out collectedValue) && collectedValue > -1)
                                {
                                    int collectedBasicParametersTableID = -1;
                                    if (int.TryParse(label.Text, out collectedBasicParametersTableID) &&
                                        collectedBasicParametersTableID > -1)
                                        tempDictionary.Add(collectedBasicParametersTableID, collectedValue);
                                }
                            }
                        }
                        rowIndex++;
                    }
                }

                if (mode == 0)
                {
                    if (tempDictionary.Count > 0)
                    {
                        //Список ранее введенных пользователем данных для данной кампании (отчета)
                        List<CollectedBasicParametersTable> сollectedBasicParametersTable =
                            (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTable
                             where (from item in tempDictionary select item.Key).ToList()
                             .Contains((int)collectedBasicParameters.CollectedBasicParametersTableID)
                             select collectedBasicParameters).ToList();

                        string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                        foreach (var сollectedBasicParameter in сollectedBasicParametersTable)
                        {
                            сollectedBasicParameter.CollectedValue =
                                (from item in tempDictionary
                                 where item.Key == сollectedBasicParameter.CollectedBasicParametersTableID
                                 select item.Value).FirstOrDefault();
                            сollectedBasicParameter.LastChangeDateTime = DateTime.Now;
                            сollectedBasicParameter.UserIP = localIP;
                        }
                        KPIWebDataContext.SubmitChanges();
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "alert('Данные успешно подтверждены');", true);
                    }
                   // CalcCalculate();
                    //надо рассчитать рассчетные
                }
                else if (mode == 1)
                {
                    Response.Redirect("~/Reports/ChooseReport.aspx");
                }
                else if (mode == 2)
                {
                    if (tempDictionary.Count > 0)
                    {
                        //Список ранее введенных пользователем данных для данной кампании (отчета)
                        List<CollectedBasicParametersTable> сollectedBasicParametersTable =
                            (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTable
                             where (from item in tempDictionary select item.Key).ToList()
                             .Contains((int)collectedBasicParameters.CollectedBasicParametersTableID)
                             select collectedBasicParameters).ToList();

                        foreach (var сollectedBasicParameter in сollectedBasicParametersTable)
                        {
                            сollectedBasicParameter.ConfirmedThirdLevel = true;
                        }
                        KPIWebDataContext.SubmitChanges();
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "alert('Данные успешно подтверждены');", true);
                    }
                    //надо подтвердить рассчетные
                }
                else
                {
                    //ERROR
                }
            }
        }

        protected void GridviewCollectedBasicParameters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Color color;
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
            e.Row.BackColor = color;
            int rowIndex = 0;

            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode;

            for (int i = 1; i <= GridviewCollectedBasicParameters.Columns.Count; i++)
            {

                {
                    var lblMinutes = e.Row.FindControl("Value" + rowIndex) as TextBox;
                    if (lblMinutes != null)
                    {
                        lblMinutes.ReadOnly = (mode == 0) ? false : true;
                    }
                    if (lblMinutes != null && lblMinutes.Text.Count() == 0)
                    {
                        lblMinutes.Visible = false;
                        if (e.Row.RowType == DataControlRowType.DataRow)
                        {
                            DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                            d.BackColor = disableColor;
                        }
                    }
                    else if (lblMinutes != null)
                    {
                        lblMinutes.BackColor = color;
                    }
                    rowIndex++;
                }
            }
        }

        protected void GridviewCollectedBasicParameters_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridviewCollectedBasicParameters_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {

        }

        protected void GridviewCollectedBasicParameters_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
        }
    }
}