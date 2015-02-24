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
                /// -1 никто ниоткуда
                /// 0 с Кфу
                /// 1 с Академии
                /// 2 с Факультета
                /// 3 с кафедры
                /// 4 с специализация
                /// 5 с под специализацией,пока нет

                ///узнали все о пользователе
                List<BasicParametersTable> MybasicParams =
                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                        join b in kpiWebDataContext.BasicParametersTable
                            on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                        join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                            on b.BasicParametersTableID equals c.FK_ParametrsTable
                        where a.FK_ReportArchiveTable == ReportArchiveID
                        && c.FK_UsersTable==UserID
                        && b.SubvisionLevel == userLevel
                        && a.Active == true
                        && c.CanEdit == true
                        && c.Active == true
                     select b).ToList();
                //узнали личные базовые параметры 
                /////////добавим пустые поля своим показателям еслинадо
                foreach (BasicParametersTable basicParam in MybasicParams) // создадим строки для ввода данных которых нет
                {
                    CollectedBasicParametersTable collectedTemp =
                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                         where 
                               a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                               && a.FK_ReportArchiveTable == ReportArchiveID
                                && (a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable || a.FK_ZeroLevelSubdivisionTable == null)
                                && (a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable || a.FK_FirstLevelSubdivisionTable == null)
                                && (a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable || a.FK_SecondLevelSubdivisionTable == null)
                                && (a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable || a.FK_ThirdLevelSubdivisionTable == null)
                                && (a.FK_FourthLevelSubdivisionTable == user.FK_FourthLevelSubdivisionTable || a.FK_FourthLevelSubdivisionTable == null)
                                && (a.FK_FifthLevelSubdivisionTable == user.FK_FifthLevelSubdivisionTable || a.FK_FifthLevelSubdivisionTable == null)
                         select a).FirstOrDefault();
                    if (collectedTemp == null) // надо создать
                    {
                        collectedTemp = new CollectedBasicParametersTable();
                        collectedTemp.Active = true;
                        collectedTemp.FK_UsersTable = UserID;
                        collectedTemp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                        collectedTemp.FK_ReportArchiveTable = ReportArchiveID;
                        collectedTemp.CollectedValue = 0;
                        collectedTemp.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                        collectedTemp.LastChangeDateTime = DateTime.Now;
                        collectedTemp.SavedDateTime = DateTime.Now;
                        collectedTemp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                        collectedTemp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                        collectedTemp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                        collectedTemp.FK_ThirdLevelSubdivisionTable = user.FK_ThirdLevelSubdivisionTable;
                        collectedTemp.FK_FourthLevelSubdivisionTable = user.FK_FourthLevelSubdivisionTable;
                        collectedTemp.FK_FifthLevelSubdivisionTable = user.FK_FifthLevelSubdivisionTable;

                        kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedTemp);
                        kpiWebDataContext.SubmitChanges();
                    }
                }
                ////// добавили новые поля текущему пользователю
                /////создаем дататейбл
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                
                dataTable.Columns.Add(new DataColumn("MyValue", typeof(string)));
                dataTable.Columns.Add(new DataColumn("MyCollectId", typeof (string)));

                for (int k = 0; k <= 100; k++)  //создаем кучу полей
                {
                    dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CollectId" + k.ToString(), typeof(string)));
                }

                //заполним дататейбл для местных значений
                
                foreach (BasicParametersTable basicParam in MybasicParams)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                    dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;
                    dataRow["Name"] = basicParam.Name;
                    CollectedBasicParametersTable collectedBasicTmp =
                            (from a in kpiWebDataContext.CollectedBasicParametersTable
                             where
                                   (a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable || a.FK_ZeroLevelSubdivisionTable== null)
                                && (a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable || a.FK_FirstLevelSubdivisionTable == null)
                                && (a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable || a.FK_SecondLevelSubdivisionTable == null)
                                && (a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable || a.FK_ThirdLevelSubdivisionTable == null)
                                && (a.FK_FourthLevelSubdivisionTable == user.FK_FourthLevelSubdivisionTable || a.FK_FourthLevelSubdivisionTable == null)
                                && (a.FK_FifthLevelSubdivisionTable == user.FK_FifthLevelSubdivisionTable || a.FK_FifthLevelSubdivisionTable == null)
                                && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                && a.FK_ReportArchiveTable == ReportArchiveID
                                select a).FirstOrDefault();
                    dataRow["MyValue"] = collectedBasicTmp.CollectedValue.ToString();
                    dataRow["MyCollectId"] = collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                    dataTable.Rows.Add(dataRow);
                }
                

                int additionalColumnCount = 0;
                List<string> columnNames=new List<string>();

                /// 
                switch (userLevel) // это штука пока будет работать только для пользователя деканата 
                {
                    case  0: //вытаскиваем все универы
                    {
                        break;
                    }
                    case 1: //вытаскиваем все факультеты
                    {
                        break;
                    }
                    case 2://я Факультет
                    {
                        List<BasicParametersTable> LevelUpBasicParams0 =
                        (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                         join b in kpiWebDataContext.BasicParametersTable
                             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                         join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                             on b.BasicParametersTableID equals c.FK_ParametrsTable
                         where a.FK_ReportArchiveTable == ReportArchiveID
                         && b.SubvisionLevel == userLevel + 1////ВНИМАНИЕ
                         && c.FK_UsersTable == UserID
                         && a.Active == true
                         && c.CanEdit == true
                         && c.Active == true
                         select b).ToList();
                        List<ThirdLevelSubdivisionTable> fakulties =
                            (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                             where a.FK_SecondLevelSubdivisionTable == l_2
                             && a.Active == true
                             select a).ToList();
                        /// пройдемся и создадим пустые показатели)))
                        foreach (ThirdLevelSubdivisionTable fak in fakulties)
                        {
                            columnNames.Add(fak.Name);
                            foreach (BasicParametersTable basicParam in LevelUpBasicParams0) // создадим строки для ввода данных которых нет
                            {
                                CollectedBasicParametersTable collectedTemp =
                                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                                     where
                                           a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                           && a.FK_ReportArchiveTable == ReportArchiveID
                                            && (a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable)
                                            && (a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable)
                                            && (a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable)
                                            && (a.FK_ThirdLevelSubdivisionTable == fak.ThirdLevelSubdivisionTableID)
                                     select a).FirstOrDefault();
                                if (collectedTemp == null) // надо создать
                                {
                                    collectedTemp = new CollectedBasicParametersTable();
                                    collectedTemp.Active = true;
                                    collectedTemp.FK_UsersTable = UserID;
                                    collectedTemp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                                    collectedTemp.FK_ReportArchiveTable = ReportArchiveID;
                                    collectedTemp.CollectedValue = 0;
                                    collectedTemp.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                                    collectedTemp.LastChangeDateTime = DateTime.Now;
                                    collectedTemp.SavedDateTime = DateTime.Now;
                                    collectedTemp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                                    collectedTemp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                                    collectedTemp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                                    collectedTemp.FK_ThirdLevelSubdivisionTable = fak.ThirdLevelSubdivisionTableID;
                                    collectedTemp.FK_FifthLevelSubdivisionTable = user.FK_FifthLevelSubdivisionTable;
                                    kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedTemp);
                                    kpiWebDataContext.SubmitChanges();
                                }

                            }
                        }
                        /// ////////////////////////////////////////////////////////////////////сделали кафедры
                        ////////////////////////////////////////////////////////////////////////начинаем делать специальности

                        List<FourthLevelSubdivisionTable> Specialzations =
                            (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                join b in kpiWebDataContext.ThirdLevelSubdivisionTable
                                    on a.FK_ThirdLevelSubdivisionTable equals b.ThirdLevelSubdivisionTableID
                                where b.FK_SecondLevelSubdivisionTable == l_2
                                      && a.Active == true 
                                      && b.Active == true
                         select a).ToList();                       
                        /// пройдемся и создадим пустые показатели)))
                        List<BasicParametersTable> LevelUpBasicParams =
                               (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                join b in kpiWebDataContext.BasicParametersTable
                                    on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                    on b.BasicParametersTableID equals c.FK_ParametrsTable
                                where a.FK_ReportArchiveTable == ReportArchiveID
                                && b.SubvisionLevel == userLevel + 2////ВНИМАНИЕ
                                && c.FK_UsersTable == UserID
                                && a.Active == true
                                && c.CanEdit == true
                                && c.Active == true
                            /*    && b.ForeignStudents <= Convert.ToInt32((from z in kpiWebDataContext.SpecializationTable
                                                                         where z.SpecializationTableID == spec.FK_Specialization
                                                                         select z.ForeignStudents).FirstOrDefault().ToString())*/
                                select b).ToList();

                        foreach (FourthLevelSubdivisionTable spec in Specialzations)
                        {
                            columnNames.Add((from a in kpiWebDataContext.SpecializationTable
                                                 where a.SpecializationTableID==spec.FK_Specialization
                                                 select a.Name).FirstOrDefault().ToString());
                           
                            ///узнай все специальности
                            foreach (BasicParametersTable basicParam in LevelUpBasicParams) // создадим строки для ввода данных которых нет
                            {
                                //создавая пустую строку нужно проеврить убедиться что даннай БП дружит с этой специальностью
                                if ((basicParam.ForeignStudents == 1) &&
                                    (Convert.ToInt32((from z in kpiWebDataContext.SpecializationTable
                                        where z.SpecializationTableID == spec.FK_Specialization
                                        select z.ForeignStudents).FirstOrDefault()) == 0))
                                {
                                    
                                }
                                else
                                {
                                    CollectedBasicParametersTable collectedTemp =
                                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                                            where
                                                a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                                && a.FK_ReportArchiveTable == ReportArchiveID
                                                && (a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable)
                                                &&
                                                (a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable)
                                                &&
                                                (a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable)
                                                &&
                                                (a.FK_ThirdLevelSubdivisionTable == spec.FK_ThirdLevelSubdivisionTable)
                                                &&
                                                (a.FK_FourthLevelSubdivisionTable == spec.FourthLevelSubdivisionTableID)
                                            select a).FirstOrDefault();
                                    if (collectedTemp == null) // надо создать
                                    {
                                        collectedTemp = new CollectedBasicParametersTable();
                                        collectedTemp.Active = true;
                                        collectedTemp.FK_UsersTable = UserID;
                                        collectedTemp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                                        collectedTemp.FK_ReportArchiveTable = ReportArchiveID;
                                        collectedTemp.CollectedValue = 0;
                                        collectedTemp.UserIP =
                                            Dns.GetHostEntry(Dns.GetHostName())
                                                .AddressList.Where(
                                                    ip =>
                                                        ip.AddressFamily ==
                                                        System.Net.Sockets.AddressFamily.InterNetwork)
                                                .Select(ip => ip.ToString())
                                                .FirstOrDefault() ?? "";
                                        collectedTemp.LastChangeDateTime = DateTime.Now;
                                        collectedTemp.SavedDateTime = DateTime.Now;
                                        collectedTemp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                                        collectedTemp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                                        collectedTemp.FK_SecondLevelSubdivisionTable =
                                            user.FK_SecondLevelSubdivisionTable;
                                        collectedTemp.FK_ThirdLevelSubdivisionTable = spec.FK_ThirdLevelSubdivisionTable;
                                        collectedTemp.FK_FourthLevelSubdivisionTable =
                                            spec.FourthLevelSubdivisionTableID;
                                        collectedTemp.FK_FifthLevelSubdivisionTable = user.FK_FifthLevelSubdivisionTable;

                                        kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedTemp);
                                        kpiWebDataContext.SubmitChanges();
                                    }
                                }
                            }                                                                                              
                        }
                        additionalColumnCount = Specialzations.Count+fakulties.Count;

                        //заполним все что можем для кафедры
                        int firstAddCnt = 0; // считаем количество кафедр для сдвига
                        foreach (BasicParametersTable basicParam in LevelUpBasicParams0)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                            dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;
                            dataRow["Name"] = basicParam.Name;
                            int i = 0;
                            foreach (ThirdLevelSubdivisionTable fak in fakulties)
                            {
                                CollectedBasicParametersTable collectedBasicTmp =
                                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                                     where a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                        && a.FK_ReportArchiveTable == ReportArchiveID
                                         && (a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable)
                                         && (a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable)
                                         && (a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable)
                                         && (a.FK_ThirdLevelSubdivisionTable == fak.ThirdLevelSubdivisionTableID)
                                     select a).FirstOrDefault();
                                dataRow["Value" + i] = collectedBasicTmp.CollectedValue.ToString();
                                dataRow["CollectId" + i] = collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                                i++;
                                
                            }
                            firstAddCnt=fakulties.Count;
                            dataTable.Rows.Add(dataRow);
                        } 
                        //////////////////////////////////////////////// а теперь для специальности
                        /// 
                        
                        foreach (BasicParametersTable basicParam in LevelUpBasicParams)
                        {

                            DataRow dataRow = dataTable.NewRow();
                            dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                            dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;
                            dataRow["Name"] = basicParam.Name;
                            int i=0;
                            foreach (FourthLevelSubdivisionTable spec in Specialzations)
                            {
                                if ((basicParam.ForeignStudents == 1) &&
                                    (Convert.ToInt32((from z in kpiWebDataContext.SpecializationTable
                                                      where z.SpecializationTableID == spec.FK_Specialization
                                                      select z.ForeignStudents).FirstOrDefault()) == 0))
                                {

                                }
                                else
                                {
                                    CollectedBasicParametersTable collectedBasicTmp =
                                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                                            where a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                                  && a.FK_ReportArchiveTable == ReportArchiveID
                                                  &&
                                                  (a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable)
                                                  &&
                                                  (a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable)
                                                  &&
                                                  (a.FK_SecondLevelSubdivisionTable ==
                                                   user.FK_SecondLevelSubdivisionTable)
                                                  &&
                                                  (a.FK_ThirdLevelSubdivisionTable == spec.FK_ThirdLevelSubdivisionTable)
                                                  &&
                                                  (a.FK_FourthLevelSubdivisionTable ==
                                                   spec.FourthLevelSubdivisionTableID)
                                            select a).FirstOrDefault();
                                    dataRow["Value" + (i + firstAddCnt)] = collectedBasicTmp.CollectedValue.ToString();
                                    dataRow["CollectId" + (i + firstAddCnt)] =
                                        collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                                    
                                }
                                i++;
                            }
                            dataTable.Rows.Add(dataRow);
                        } 
                        break;
                    }
                    case 3://вытаскиваем все специальности
                    {
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
                 GridviewCollectedBasicParameters.DataSource = dataTable;
                 for (int j = 0; j < additionalColumnCount; j++)
                 {
                     GridviewCollectedBasicParameters.Columns[j + 5].Visible = true;
                     GridviewCollectedBasicParameters.Columns[j + 5].HeaderText = columnNames[j];
                 }                 
                 GridviewCollectedBasicParameters.DataBind();
            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
           
            StringCollection sc = new StringCollection();
            if (ViewState["CollectedBasicParametersTable"] != null && ViewState["CurrentReportArchiveID"] != null)
            {
                int currentReportArchiveID = (int)ViewState["CurrentReportArchiveID"];
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                Dictionary<int, double> tempDictionary = new Dictionary<int, double>();
                DataTable collectedBasicParametersTable = (DataTable)ViewState["CollectedBasicParametersTable"];
                int columnCnt = (int) ViewState["ValueColumnCnt"];
                if (collectedBasicParametersTable.Rows.Count > 0)
                {
                    int rowIndex = 0;
                    for (int i = 1; i <= collectedBasicParametersTable.Rows.Count; i++)//в каждой строчке
                    {
                        ////сохраняем свои
                        TextBox mytextBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("MyValue");
                        Label mylabel = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("MyCollectId");

                        if (mytextBox != null && mylabel != null)
                        {
                            double collectedValue = -1;
                            if (double.TryParse(mytextBox.Text, out collectedValue) && collectedValue > -1)
                            {
                                int collectedBasicParametersTableID = -1;
                                if (int.TryParse(mylabel.Text, out collectedBasicParametersTableID) && collectedBasicParametersTableID > -1)
                                    tempDictionary.Add(collectedBasicParametersTableID, collectedValue);
                            }
                        } 
                        ///сохранили свои данные
                        /// //сохраним вложенные данные
                        for (int k = 0; k < columnCnt; k++) // пройдемся по каждой колонке
                        {
                            TextBox textBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("Value"+k.ToString());
                            Label label = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("CollectId" + k.ToString());

                            if (textBox != null && label != null)
                            {
                                double collectedValue = -1;
                                if (double.TryParse(textBox.Text, out collectedValue) && collectedValue > -1)
                                {
                                    int collectedBasicParametersTableID = -1;
                                    if (int.TryParse(label.Text, out collectedBasicParametersTableID) && collectedBasicParametersTableID > -1)
                                        tempDictionary.Add(collectedBasicParametersTableID, collectedValue);
                                }
                            }                        
                        }
                        rowIndex++;
                    }                    
                }
                if (tempDictionary.Count > 0)
                {
                    //Список ранее введенных пользователем данных для данной кампании (отчета)
                    List<CollectedBasicParametersTable> сollectedBasicParametersTable = (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTable
                                                                                         where (from item in tempDictionary select item.Key).ToList().Contains((int)collectedBasicParameters.CollectedBasicParametersTableID)
                                                                                         select collectedBasicParameters).ToList();

                    string localIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                    foreach (var сollectedBasicParameter in сollectedBasicParametersTable)
                    {
                        сollectedBasicParameter.CollectedValue = (from item in tempDictionary where item.Key == сollectedBasicParameter.CollectedBasicParametersTableID select item.Value).FirstOrDefault();
                        сollectedBasicParameter.LastChangeDateTime = DateTime.Now;
                        сollectedBasicParameter.UserIP = localIP;
                    }
                    KPIWebDataContext.SubmitChanges();
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Данные успешно сохранены');", true);
                }
            }
        }

        protected void GridviewCollectedBasicParameters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            // Скрываем неактивные TextBox's
            
            int rowIndex = 0;
            var lblMinutes2 = e.Row.FindControl("MyValue") as TextBox;
            if (lblMinutes2 != null && lblMinutes2.Text.Count() == 0)
                lblMinutes2.Visible = false;

            for (int i = 1; i <= GridviewCollectedBasicParameters.Columns.Count; i++)
            {
                {


                    var lblMinutes = e.Row.FindControl("Value" + rowIndex) as TextBox;
                    if (lblMinutes != null && lblMinutes.Text.Count() == 0)
                        lblMinutes.Visible = false;
                    rowIndex++;


                }
            }
            

        }
    }
}