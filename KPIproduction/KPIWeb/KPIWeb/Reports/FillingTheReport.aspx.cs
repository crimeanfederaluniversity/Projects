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
using System.Web.UI.HtmlControls;
using System.Web.WebPages;
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
        protected double pattern1(UsersTable user, int ReportArchiveID, int spectype_, string basicAbb)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                        (from a in kpiWebDataContext.CollectedBasicParametersTable
                         join z in kpiWebDataContext.BasicParametersTable
                         on a.FK_BasicParametersTable equals z.BasicParametersTableID
                         join b in kpiWebDataContext.FourthLevelParametrs
                         on a.FK_FourthLevelSubdivisionTable equals b.FourthLevelParametrsID
                         join c in kpiWebDataContext.ThirdLevelParametrs
                         on a.FK_ThirdLevelSubdivisionTable equals c.ThirdLevelParametrsID
                         join d in kpiWebDataContext.FourthLevelSubdivisionTable
                         on a.FK_FourthLevelSubdivisionTable equals d.FourthLevelSubdivisionTableID
                         join e in kpiWebDataContext.SpecializationTable
                         on d.FK_Specialization equals e.SpecializationTableID
                         where
                            a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                         && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                         && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                         && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                         && z.AbbreviationEN == basicAbb
                         && a.FK_ReportArchiveTable == ReportArchiveID
                         && b.SpecType == spectype_
                         && a.Active == true
                         && d.Active == true
                         && (e.FK_FieldOfExpertise == 10 || e.FK_FieldOfExpertise == 11 || e.FK_FieldOfExpertise == 12)
                         select a.CollectedValue).Sum());
        }
        protected double pattern2(UsersTable user, int ReportArchiveID, string basicAbb)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                      (from a in kpiWebDataContext.CollectedBasicParametersTable
                       join z in kpiWebDataContext.BasicParametersTable
                       on a.FK_BasicParametersTable equals z.BasicParametersTableID
                       join b in kpiWebDataContext.FourthLevelParametrs
                       on a.FK_FourthLevelSubdivisionTable equals b.FourthLevelParametrsID
                       where
                          a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                       && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                       && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                       && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                       && z.AbbreviationEN == basicAbb
                       && a.FK_ReportArchiveTable == ReportArchiveID
                       && b.IsForeignStudentsAccept == true
                       && a.Active == true
                       && z.Active == true
                       && b.Active == true
                       select a.CollectedValue).Sum());
        }
        protected double pattern3(UsersTable user, int ReportArchiveID, int SpecType)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && a.Active == true
                     // && z.Active == true
                       && b.Active == true
                 select b).ToList().Count);

        }
        protected double pattern4(UsersTable user, int ReportArchiveID, int SpecType)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && c.IsInvalidStudentsFacilities == true
                       && a.Active == true
                       && b.Active == true
                 select b).ToList().Count);
        }
        protected double pattern5(UsersTable user, int ReportArchiveID, int SpecType)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && c.IsNetworkComunication == true
                       && a.Active == true
                       && b.Active == true
                 select b).ToList().Count);
        }
        protected double pattern6(UsersTable user, int ReportArchiveID, int SpecType)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            return Convert.ToDouble(
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                 join b in kpiWebDataContext.FourthLevelSubdivisionTable
                     on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                 join c in kpiWebDataContext.FourthLevelParametrs
                     on b.FourthLevelSubdivisionTableID equals c.FourthLevelParametrsID
                 join d in kpiWebDataContext.ThirdLevelParametrs
                     on a.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                 where c.SpecType == SpecType
                       && a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                       && d.CanGraduate == true
                       && a.Active == true
                       && b.Active == true
                       && c.IsModernEducationTechnologies == true
                 select b).ToList().Count);
        }
        protected void ConfCalculate(int ReportArchiveID, UsersTable user)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            List<BasicParametersTable> calcBasicParams =
            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
             join b in kpiWebDataContext.BasicParametersTable
             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
             on b.BasicParametersTableID equals c.FK_ParametrsTable
             join d in kpiWebDataContext.BasicParametrAdditional
             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
             where
                   a.FK_ReportArchiveTable == ReportArchiveID  //из нужного отчёта
                && c.FK_UsersTable == user.UsersTableID // свяный с пользователем
                && (d.SubvisionLevel == 3 || d.SubvisionLevel == 4)//нужный уровень заполняющего
                && a.Active == true  // запись в таблице связей показателя и отчёта активна
                && c.Active == true  // запись в таблице связей показателя и пользователей активна
                && d.Calculated == true // этот показатель нужно считать
             select b).ToList();

            //узнали показатели кафедры(отчёт,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)          
            foreach (BasicParametersTable basicParam in calcBasicParams) //пройдемся по показателям
            {
                CollectedBasicParametersTable collectedBasicTmp =
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                     where a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                           && a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                           && a.FK_SecondLevelSubdivisionTable == user.FK_SecondLevelSubdivisionTable
                           && a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                           && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                           && a.FK_ReportArchiveTable == ReportArchiveID
                     select a).FirstOrDefault();
                if (collectedBasicTmp != null) // надо создать
                {
                    collectedBasicTmp.Status = 4;
                    kpiWebDataContext.SubmitChanges();
                }
            }
        }
        protected void CalcCalculate(int ReportArchiveID, UsersTable user)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<BasicParametersTable> calcBasicParams =
            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
             join b in kpiWebDataContext.BasicParametersTable
             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
             on b.BasicParametersTableID equals c.FK_ParametrsTable
             join d in kpiWebDataContext.BasicParametrAdditional
             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
             where
                   a.FK_ReportArchiveTable == ReportArchiveID  //из нужного отчёта
                && c.FK_UsersTable == user.UsersTableID // свяный с пользователем
                && (d.SubvisionLevel == 3 || d.SubvisionLevel == 4)//нужный уровень заполняющего
                && a.Active == true  // запись в таблице связей показателя и отчёта активна
                && c.Active == true  // запись в таблице связей показателя и пользователей активна
                && d.Calculated == true // этот показатель нужно считать
             select b).ToList();

            ThirdLevelParametrs Cangrad = (from a in kpiWebDataContext.ThirdLevelParametrs
                                           where a.CanGraduate == true
                                           && a.ThirdLevelParametrsID == user.FK_ThirdLevelSubdivisionTable
                                           select a).FirstOrDefault();
            if (Cangrad != null) // кафедра выпускает
            {
                //определим какого типа специальности есть на данной кафедре             
                bool AnyB = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 1
                             select a).ToList().Count() > 0 ? true : false;

                bool AnyS = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 2
                             select a).ToList().Count() > 0 ? true : false;

                bool AnyM = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 3
                             select a).ToList().Count() > 0 ? true : false;

                bool AnyA = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             join b in kpiWebDataContext.FourthLevelParametrs
                             on a.FourthLevelSubdivisionTableID equals b.FourthLevelParametrsID
                             where
                             a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
                             && b.SpecType == 4
                             select a).ToList().Count() > 0 ? true : false;
                //узнали показатели кафедры(отчёт,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)          
                foreach (BasicParametersTable basicParam in calcBasicParams) //пройдемся по показателям
                {
                    double tmp = 1000000000001;

                    if (basicParam.AbbreviationEN == "a_Och_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "a_Och_M");
                    if (basicParam.AbbreviationEN == "b_OchZ_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "b_OchZ_M");
                    if (basicParam.AbbreviationEN == "c_Z_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "c_Z_M");
                    if (basicParam.AbbreviationEN == "d_E_M_IZO") tmp = pattern1(user, ReportArchiveID, 3, "d_E_M");

                    if (basicParam.AbbreviationEN == "a_Och_M_NoIn") tmp = pattern2(user, ReportArchiveID, "a_Och_M");
                    if (basicParam.AbbreviationEN == "b_OchZ_M_NoIn") tmp = pattern2(user, ReportArchiveID, "b_OchZ_M");
                    if (basicParam.AbbreviationEN == "c_Z_M_NoIn") tmp = pattern2(user, ReportArchiveID, "c_Z_M");
                    if (basicParam.AbbreviationEN == "d_E_M_NoIn") tmp = pattern2(user, ReportArchiveID, "d_E_M");

                    if (basicParam.AbbreviationEN == "a_Och_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "a_Och_S");
                    if (basicParam.AbbreviationEN == "b_OchZ_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "b_OchZ_S");
                    if (basicParam.AbbreviationEN == "c_Z_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "c_Z_S");
                    if (basicParam.AbbreviationEN == "d_E_S_IZO") tmp = pattern1(user, ReportArchiveID, 2, "d_E_S");

                    if (basicParam.AbbreviationEN == "a_Och_S_NoIn") tmp = pattern2(user, ReportArchiveID, "a_Och_S");
                    if (basicParam.AbbreviationEN == "b_OchZ_S_NoIn") tmp = pattern2(user, ReportArchiveID, "b_OchZ_S");
                    if (basicParam.AbbreviationEN == "c_Z_S_NoIn") tmp = pattern2(user, ReportArchiveID, "c_Z_S");
                    if (basicParam.AbbreviationEN == "d_E_S_NoIn") tmp = pattern2(user, ReportArchiveID, "d_E_S");

                    if (basicParam.AbbreviationEN == "a_Och_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "a_Och_B");
                    if (basicParam.AbbreviationEN == "b_OchZ_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "b_OchZ_B");
                    if (basicParam.AbbreviationEN == "c_Z_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "c_Z_B");
                    if (basicParam.AbbreviationEN == "d_E_B_IZO") tmp = pattern1(user, ReportArchiveID, 1, "d_E_B");

                    if (basicParam.AbbreviationEN == "a_Och_B_NoIn") tmp = pattern2(user, ReportArchiveID, "a_Och_B");
                    if (basicParam.AbbreviationEN == "b_OchZ_B_NoIn") tmp = pattern2(user, ReportArchiveID, "b_OchZ_B");
                    if (basicParam.AbbreviationEN == "c_Z_B_NoIn") tmp = pattern2(user, ReportArchiveID, "c_Z_B");
                    if (basicParam.AbbreviationEN == "d_E_B_NoIn") tmp = pattern2(user, ReportArchiveID, "d_E_B");

                    if (basicParam.AbbreviationEN == "OOP_M") tmp = pattern3(user, ReportArchiveID, 3);
                    if (basicParam.AbbreviationEN == "kol_M_OP") tmp = pattern4(user, ReportArchiveID, 3);
                    if (basicParam.AbbreviationEN == "kol_M_OP_SV") tmp = pattern5(user, ReportArchiveID, 3);
                    if (basicParam.AbbreviationEN == "OOP_M_SOT") tmp = pattern6(user, ReportArchiveID, 3);

                    if (basicParam.AbbreviationEN == "OOP_S") tmp = pattern3(user, ReportArchiveID, 2);
                    if (basicParam.AbbreviationEN == "kol_S_OP") tmp = pattern4(user, ReportArchiveID, 2);
                    if (basicParam.AbbreviationEN == "kol_S_OP_SV") tmp = pattern5(user, ReportArchiveID, 2);
                    if (basicParam.AbbreviationEN == "OOP_S_SOT") tmp = pattern6(user, ReportArchiveID, 2);

                    if (basicParam.AbbreviationEN == "OOP_B") tmp = pattern3(user, ReportArchiveID, 1);
                    if (basicParam.AbbreviationEN == "kol_B_OP") tmp = pattern4(user, ReportArchiveID, 1);
                    if (basicParam.AbbreviationEN == "kol_B_OP_SV") tmp = pattern5(user, ReportArchiveID, 1);
                    if (basicParam.AbbreviationEN == "OOP_B_SOT") tmp = pattern6(user, ReportArchiveID, 1);

                    if (basicParam.AbbreviationEN == "OOP_A") tmp = pattern3(user, ReportArchiveID, 4);
                    if (basicParam.AbbreviationEN == "kol_A_OP") tmp = pattern4(user, ReportArchiveID, 4);
                    if (basicParam.AbbreviationEN == "kol_A_OP_SV") tmp = pattern5(user, ReportArchiveID, 4);
                    if (basicParam.AbbreviationEN == "OOP_A_SOT") tmp = pattern6(user, ReportArchiveID, 4);

                    if (basicParam.AbbreviationEN == "Kol_Kaf_R")
                        tmp = Convert.ToDouble((from a in kpiWebDataContext.ThirdLevelParametrs
                                                where a.ThirdLevelParametrsID == user.FK_ThirdLevelSubdivisionTable
                                                select a.IsBasic).FirstOrDefault());



                    if (tmp < 1000000000000)
                    {
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
                            collectedBasicTmp.FK_UsersTable = user.UsersTableID;
                            collectedBasicTmp.FK_BasicParametersTable = basicParam.BasicParametersTableID;
                            collectedBasicTmp.FK_ReportArchiveTable = ReportArchiveID;
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

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            string script = @"<script>
            function ConfirmSubmit() {
                var msg = 'Отправить данные на утверждение?'; 
                return confirm(msg);
            }
            </script>";

            string script2 = @"<script>
            function ConfirmSubmit() {
                var msg = 'Подтвердить достоверность данных и отправить их на обработку (Режим доступа к данным будет изменен на \'только просмотр\')?'; 
                return confirm(msg);
            }
            </script>";

            string script3 = @"<script>
            function ConfirmSubmitOn() {
                var msg = 'Вернуть отчёт на доработку?'; 
                return confirm(msg);
            }
            </script>";

            string script4 = @"<script>
            function ConfirmSubmitOnТ() {
                var msg = 'Отправить отчет на утверждение?'; 
                                document.location = '../Default.aspx';
                return alert (msg);
                
            }
            </script>";

            string script5 = @"<script>
            function ConfirmSubmitOnTT() {
                var msg = 'Отправить отчет на доработку?'; 
                return confirm(msg);
            }
            </script>";


            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                      (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ViewState["login"] = (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a.Email).FirstOrDefault();

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
            /// 
            if (!Page.IsPostBack)
            {
                Panel mypanel = (Panel)(Master.FindControl("loading"));
                mypanel.Visible = true;
                ViewState["IsPostBack"] = true;
                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                    "window.onload = function() {__doPostBack(\"\", \"\");};", true);

                //Response.Redirect("~/Reports/FillingTheReport.aspx");
            }
            else
            {
                //if (ViewState["IsPostBack"] == null)
                if ((bool)ViewState["IsPostBack"] == true)
                {
                    #region

                    Serialization modeSer = (Serialization)Session["mode"];
                    if (modeSer == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем
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
                    int l_2 = user.FK_SecondLevelSubdivisionTable == null
                        ? 0
                        : (int)user.FK_SecondLevelSubdivisionTable;
                    int l_3 = user.FK_ThirdLevelSubdivisionTable == null ? 0 : (int)user.FK_ThirdLevelSubdivisionTable;
                    int l_4 = user.FK_FourthLevelSubdivisionTable == null
                        ? 0
                        : (int)user.FK_FourthLevelSubdivisionTable;
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

                    #endregion

                    ///узнали все о пользователе

                    #region

                    List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
                    List<string> basicNames = new List<string>(); // сюда названия параметров для excel
                    /////создаем дататейбл
                    DataTable dataTable = new DataTable();
                    dataTable.Columns.Add(new DataColumn("CurrentReportArchiveID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("CollectedBasicParametersTableID", typeof(string)));
                    dataTable.Columns.Add(new DataColumn("Name", typeof(string)));


                    for (int k = 0; k <= 25; k++) //создаем кучу полей
                    {
                        dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                        dataTable.Columns.Add(new DataColumn("CollectId" + k.ToString(), typeof(string)));
                        dataTable.Columns.Add(new DataColumn("NotNull" + k.ToString(), typeof(string)));
                    }

                    #endregion

                    //создали макет дататейбла
                    int additionalColumnCount = 0;
                    List<int> StatusList = new List<int>();

                    #region

                    if (userLevel != 3)
                    {
                        List<BasicParametersTable> BasicParams =
                            (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                             join b in kpiWebDataContext.BasicParametersTable
                                 on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                             join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on b.BasicParametersTableID equals c.FK_ParametrsTable
                             join d in kpiWebDataContext.BasicParametrAdditional
                                 on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                             where a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                   && c.FK_UsersTable == UserID // свяный с пользователем
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
                            basicNames.Add(basicParam.Name);
                            CollectedBasicParametersTable collectedBasicTmp =
                                (from a in kpiWebDataContext.CollectedBasicParametersTable
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
                                collectedBasicTmp.FK_UsersTable = UserID;
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
                                collectedBasicTmp.FK_ZeroLevelSubdivisionTable = user.FK_ZeroLevelSubdivisionTable;
                                collectedBasicTmp.FK_FirstLevelSubdivisionTable = user.FK_FirstLevelSubdivisionTable;
                                collectedBasicTmp.FK_SecondLevelSubdivisionTable = user.FK_SecondLevelSubdivisionTable;
                                collectedBasicTmp.FK_ThirdLevelSubdivisionTable = user.FK_ThirdLevelSubdivisionTable;

                                kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                                kpiWebDataContext.SubmitChanges();
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
                    }
                    /* columnNames.Add("Кафедра:\r\n" + (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                   where a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                                   select a.Name).FirstOrDefault());
                 * */

                    #endregion

                    switch (userLevel) // это штука пока будет работать только для пользователя кафедры
                    {
                        case 0: //я КФУ
                            {
                                columnNames.Add((from a in kpiWebDataContext.ZeroLevelSubdivisionTable
                                                 where a.ZeroLevelSubdivisionTableID == user.FK_ZeroLevelSubdivisionTable
                                                 select a.Name).FirstOrDefault());
                                break;
                            }
                        case 1: //Я Акакдемия
                            {
                                //"Академия:\r\n" + 
                                columnNames.Add((from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                 where a.FirstLevelSubdivisionTableID == user.FK_FirstLevelSubdivisionTable
                                                 select a.Name).FirstOrDefault());
                                break;
                            }
                        case 2: //я Факультет
                            {
                                //"Факультет:\r\n" + 
                                columnNames.Add((from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                 where a.SecondLevelSubdivisionTableID == user.FK_SecondLevelSubdivisionTable
                                                 select a.Name).FirstOrDefault());
                                break;
                            }
                        case 3: //я кафедра
                            {
                                #region

                                List<BasicParametersTable> KafBasicParams =
                                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                     join b in kpiWebDataContext.BasicParametersTable
                                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                                     join d in kpiWebDataContext.BasicParametrAdditional
                                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                                     where
                                         a.FK_ReportArchiveTable == ReportArchiveID //из нужного отчёта
                                         && c.FK_UsersTable == UserID // свяный с пользователем
                                         && d.SubvisionLevel == 3 //нужный уровень заполняющего
                                         && a.Active == true // запись в таблице связей показателя и отчёта активна

                                         && (((c.CanEdit == true) && mode == 0)
                                             || ((c.CanView == true) && mode == 1)
                                             || ((c.CanConfirm == true) && mode == 2))
                                         // фильтруем по правам пользователя

                                         && c.Active == true
                                         // запись в таблице связей показателя и пользователей активна
                                         && d.Calculated == false
                                     // этот показатель нужно вводить а не считать
                                     select b).ToList();

                                //узнали показатели кафедры(отчёт,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)          
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
                                    if ((thirdParametrs.CanGraduate == true) || (basicParametrs.IsGraduating == false))
                                    //фильтруем базовые показатели для невыпускающих кафедр
                                    {
                                        DataRow dataRow = dataTable.NewRow();
                                        dataRow["CurrentReportArchiveID"] = ReportArchiveID;
                                        dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;
                                        dataRow["Name"] = basicParam.Name;
                                        basicNames.Add(basicParam.Name);
                                        CollectedBasicParametersTable collectedBasicTmp =
                                            (from a in kpiWebDataContext.CollectedBasicParametersTable
                                             where a.FK_ZeroLevelSubdivisionTable == user.FK_ZeroLevelSubdivisionTable
                                                   &&
                                                   a.FK_FirstLevelSubdivisionTable == user.FK_FirstLevelSubdivisionTable
                                                   &&
                                                   a.FK_SecondLevelSubdivisionTable ==
                                                   user.FK_SecondLevelSubdivisionTable
                                                   &&
                                                   a.FK_ThirdLevelSubdivisionTable == user.FK_ThirdLevelSubdivisionTable
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
                                                user.FK_ZeroLevelSubdivisionTable;
                                            collectedBasicTmp.FK_FirstLevelSubdivisionTable =
                                                user.FK_FirstLevelSubdivisionTable;
                                            collectedBasicTmp.FK_SecondLevelSubdivisionTable =
                                                user.FK_SecondLevelSubdivisionTable;
                                            collectedBasicTmp.FK_ThirdLevelSubdivisionTable =
                                                user.FK_ThirdLevelSubdivisionTable;

                                            kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(collectedBasicTmp);
                                            kpiWebDataContext.SubmitChanges();
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
                                }
                                columnNames.Add((from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                  where a.ThirdLevelSubdivisionTableID == user.FK_ThirdLevelSubdivisionTable
                                                                  select a.Name).FirstOrDefault());

                                #endregion

                                //Кафедра готова
                                additionalColumnCount += 1;

                                #region

                                if ((from zz in kpiWebDataContext.ThirdLevelParametrs
                                     where zz.ThirdLevelParametrsID == l_3
                                     select zz.CanGraduate).FirstOrDefault() == true)
                                // кафедра выпускающая значит специальности есть
                                {
                                    List<BasicParametersTable> SpecBasicParams =
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
                                               && c.FK_UsersTable == UserID // связаннаые с пользователем
                                               && a.Active == true

                                               && (((c.CanEdit == true) && mode == 0)
                                                   || ((c.CanView == true) && mode == 1)
                                                   || ((c.CanConfirm == true) && mode == 2))
                                             // фильтруем по правам пользователя

                                               && c.Active == true
                                         select b).ToList();
                                    //Получили показатели разрешенные пользователю в данном отчёте
                                    List<FourthLevelSubdivisionTable> Specialzations =
                                        (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                         where a.FK_ThirdLevelSubdivisionTable == l_3
                                               && a.Active == true
                                         select a).ToList();
                                    //Получили список специальностей для кафедры под пользователем 

                                    foreach (FourthLevelSubdivisionTable spec in Specialzations)
                                    {
                                        columnNames.Add("Направление подготовки\r" +
                                                        (from a in kpiWebDataContext.SpecializationTable
                                                         where a.SpecializationTableID == spec.FK_Specialization
                                                         select a.Name).FirstOrDefault().ToString() +" : "+ 
                                                         (from a in kpiWebDataContext.SpecializationTable
                                                         where a.SpecializationTableID == spec.FK_Specialization
                                                         select a.SpecializationNumber).FirstOrDefault().ToString());
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
                                        //если хоть одной специальности базовый показатель нужен то мы его выведем
                                        foreach (FourthLevelSubdivisionTable spec in Specialzations)
                                        {

                                            FourthLevelParametrs fourthParametrs =
                                                (from a in kpiWebDataContext.FourthLevelParametrs
                                                 where a.FourthLevelParametrsID == spec.FourthLevelSubdivisionTableID
                                                 select a).FirstOrDefault();
                                            // узнали параметры специальности
                                            //если этото параметр и эта специальность дружат  
                                            if (((fourthParametrs.IsForeignStudentsAccept == true) ||
                                                 (basicParametrs.ForForeignStudents == false)) //это для иностранцев
                                                &&
                                                ((fourthParametrs.SpecType == basicParametrs.SpecType) ||
                                                 (basicParametrs.SpecType == 0)))
                                            // это для деления на магистров аспирантов итд
                                            {
                                                j++; //потом проверка и следовательно БП нуно выводить
                                                CollectedBasicParametersTable collectedBasicTmp =
                                                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                     where
                                                         a.FK_BasicParametersTable ==
                                                         specBasicParam.BasicParametersTableID
                                                         && a.FK_ReportArchiveTable == ReportArchiveID
                                                         &&
                                                         (a.FK_ZeroLevelSubdivisionTable ==
                                                          user.FK_ZeroLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_FirstLevelSubdivisionTable ==
                                                          user.FK_FirstLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_SecondLevelSubdivisionTable ==
                                                          user.FK_SecondLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_ThirdLevelSubdivisionTable ==
                                                          user.FK_ThirdLevelSubdivisionTable)
                                                         &&
                                                         (a.FK_FourthLevelSubdivisionTable ==
                                                          spec.FourthLevelSubdivisionTableID)
                                                     select a).FirstOrDefault();
                                                if (collectedBasicTmp == null)
                                                {
                                                    collectedBasicTmp = new CollectedBasicParametersTable();
                                                    collectedBasicTmp.Active = true;
                                                    collectedBasicTmp.FK_UsersTable = UserID;
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
                                                        user.FK_ZeroLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_FirstLevelSubdivisionTable =
                                                        user.FK_FirstLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_SecondLevelSubdivisionTable =
                                                        user.FK_SecondLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_ThirdLevelSubdivisionTable =
                                                        spec.FK_ThirdLevelSubdivisionTable;
                                                    collectedBasicTmp.FK_FourthLevelSubdivisionTable =
                                                        spec.FourthLevelSubdivisionTableID;
                                                    kpiWebDataContext.CollectedBasicParametersTable.InsertOnSubmit(
                                                        collectedBasicTmp);
                                                    kpiWebDataContext.SubmitChanges();
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
                                            dataTable.Rows.Add(dataRow);
                                        }
                                        ///////////////////////закинули все в дататейбл
                                    }
                                    additionalColumnCount += Specialzations.Count;
                                }

                                #endregion

                                // специальности готовы
                                break;
                            }

                        #region

                        case 4: //пока рано//у нас нет ничего глубже специальности
                            {
                                break;
                            }
                        case 5: //выводить нечего
                            {
                                break;
                            }
                        default: // если уровня вообще нет или он неправильно задан//хорошо бы ошибку вывести
                            {
                                break;
                            }

                        #endregion

                        //неиспользуемая часть свича
                    }

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
                    DateTime endDate = (DateTime)(from a in kpiWebDataContext.ReportArchiveTable
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
                        GoBackButton.Visible = true;
                        GoBackButton.Text = "Вернуться без сохранения.";

                        ButtonSave.Visible = true;
                        ButtonSave.Text = "Сохранить внесенные данные.";

                        UpnDownButton.Visible = true;
                        UpnDownButton.Text = "Отправить отчёт на утверждение.";

                        TextBox1.Visible = false;

                        if (StatusList[0] == 1)
                        {
                            Label1.Text = "Данные возвращены на доработку. Проверьте корректность введенных данных.";
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

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                         "Confirm", script);
                        UpnDownButton.Attributes.Add("OnClick", "return ConfirmSubmit();");




                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                            "window.onbeforeunload = function() { return 'Date will be lost: are you sure?'; };", true);
                        ButtonSave.OnClientClick = "window.onbeforeunload = null;";
                        Button1.OnClientClick = "window.onbeforeunload = null;";
                        UpnDownButton.OnClientClick = "window.onbeforeunload = null;";
                    }
                    else if
                        (mode == 1)
                    {
                        GoBackButton.Visible = true;
                        GoBackButton.Text = "Вернуться в меню.";

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
                        Label2.Visible = false;
                        // OnClientClick="javascript:return confirm('Do you really want to \ndelete the item?');"

                    }
                    else if (mode == 2)
                    {
                        GoBackButton.Visible = true;
                        GoBackButton.Text = "Вернуться в меню без утверждения.";

                        ButtonSave.Visible = true;
                        ButtonSave.Text = "Утвердить данные.";

                        UpnDownButton.Visible = true;
                        UpnDownButton.Text = "Вернуть отчёт на доработку";

                        TextBox1.Visible = true;

                        Label1.Text = "Утверждение данных";
                        Label2.Text = "Осталось " + dateCount + " дней до закрытия отчёта";



                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                        "Confirm", script2);

                        ButtonSave.Attributes.Add("OnClick", "return ConfirmSubmit();");

                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(),
                        "ConfirmOn", script3);

                        UpnDownButton.Attributes.Add("OnClick", "return ConfirmSubmitOn();");
                    }
                    GridviewCollectedBasicParameters.DataSource = dataTable;
                    for (int j = 0; j < additionalColumnCount; j++)
                    {
                        GridviewCollectedBasicParameters.Columns[j + 4].Visible = true;
                        GridviewCollectedBasicParameters.Columns[j + 4].HeaderText = columnNames[j];
                    }
                    GridviewCollectedBasicParameters.DataBind();
                }


                Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script",
                    "window.onload = null", true);
                Panel mypanel = (Panel)(Master.FindControl("loading"));

                ViewState["IsPostBack"] = false;
                mypanel.Visible = false;




            }
        }
        protected void ButtonSave_Click(object sender, EventArgs e) //сохранение данных и пожтверждение данных
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (ViewState["CollectedBasicParametersTable"] != null && ViewState["CurrentReportArchiveID"] != null)
            {
                KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
                Dictionary<int, double> tempDictionary = new Dictionary<int, double>();

                int UserID = UserSer.Id;
                int ReportArchiveID;
                ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
                UsersTable user = (from a in KPIWebDataContext.UsersTable
                                   where a.UsersTableID == UserSer.Id
                                   select a).FirstOrDefault();

                DataTable collectedBasicParametersTable = (DataTable)ViewState["CollectedBasicParametersTable"];
                int columnCnt = (int)ViewState["ValueColumnCnt"];

                Serialization modeSer = (Serialization)Session["mode"];
                if (modeSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int mode = modeSer.mode;

                if (mode == 0) //сохранение данных
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
                                        collectedValue = Convert.ToInt32(textBox.Text);
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
                        List<CollectedBasicParametersTable> сollectedBasicParametersTable =
                            (from collectedBasicParameters in KPIWebDataContext.CollectedBasicParametersTable
                             where (from item in tempDictionary select item.Key).ToList()
                             .Contains((int)collectedBasicParameters.CollectedBasicParametersTableID)
                             select collectedBasicParameters).ToList();

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
                            сollectedBasicParameter.Status = сollectedBasicParameter.CollectedValue == null ? 0 : 2;
                        }
                        KPIWebDataContext.SubmitChanges();
                    }
                    //надо рассчитать рассчетные
                    CalcCalculate(ReportArchiveID, user);
                    int AllCnt = (int)ViewState["AllCnt"];
                    if (AllCnt == notNullCnt)
                    {
                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Пользователь " + (string)ViewState["login"] + " сохранил данные в отчете с ID = " + paramSerialization.ReportStr + "Все показатели заполнены");
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Все показатели заполнены. Необходимо отправить отчёт на утверждение');" +
                            "document.location = '../Reports/FillingTheReport.aspx';", true);


                    }
                    else
                    {
                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Пользователь " + (string)ViewState["login"] + " сохранил данные в отчете с ID = " + paramSerialization.ReportStr + "Заполнено " + notNullCnt + " показателей из " + AllCnt);
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Данные сохранены на сервере. Заполнено " + notNullCnt + " показателей из " + AllCnt + " для отправки отчёта необходимо заполнитеь еще " + (AllCnt - notNullCnt) + " показателей.');" +
                            "document.location = '../Reports/FillingTheReport.aspx';", true);

                    }

                    #endregion
                }
                else if (mode == 1) // просмотр
                {
                    Response.Redirect("~/Reports/ChooseReport.aspx");
                }
                else if (mode == 2) // подтверждение 
                {
                    #region confirm all
                    if (GridviewCollectedBasicParameters.Rows.Count > 0)
                    {
                        for (int k = 0; k < columnCnt; k++) // пройдемся по каждой колонке
                        {
                            for (int i = 0; i < GridviewCollectedBasicParameters.Rows.Count; i++)
                            {
                                Label label =
                                    (Label)
                                        GridviewCollectedBasicParameters.Rows[i].FindControl("CollectId" + k.ToString());
                                if (label != null)
                                {
                                    if (label.Text == "")
                                    {
                                        //error
                                    }
                                    else
                                    {
                                        CollectedBasicParametersTable tmpColTable =
                                            (from a in KPIWebDataContext.CollectedBasicParametersTable
                                             where a.CollectedBasicParametersTableID == Convert.ToInt32(label.Text)
                                             select a).FirstOrDefault();
                                        if (tmpColTable != null)
                                        {
                                            if (tmpColTable.Status == 3)
                                            {
                                                tmpColTable.Status = 4;
                                                KPIWebDataContext.SubmitChanges();
                                            }
                                            else
                                            {
                                                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 9 в отчете с ID = ");
                                            }
                                        }
                                        else
                                        {
                                            LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 10 в отчете с ID = ");   
                                        }
                                    }
                                }
                            }
                        }
                        LogHandler.LogWriter.WriteLog(LogCategory.INFO, "Пользователь " + (string)ViewState["login"] + " утвердил данные в отчете с ID = " + paramSerialization.ReportStr);
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Вы утвердили данные всех базовых показателей. Отчёт отправлен и доступен только в режиме \"Просмотр\".');" +
                            "document.location = '../Default.aspx';", true);


                    }
                    else
                    {
                        //error
                        LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 1 в отчете с ID = " + paramSerialization.ReportStr);
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Ошибка'); document.location = '../Default.aspx'; ", true);

                    }
                    #endregion
                }
                else
                {
                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 2 в отчете с ID = " + paramSerialization.ReportStr);
                    //error
                }
            }
        }
        protected void GridviewCollectedBasicParameters_RowDataBound(object sender, GridViewRowEventArgs e)
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
            e.Row.BackColor = color;
            int rowIndex = 0;

            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
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
                                d.BackColor = disableColor;
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
                        }
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
        protected void Button1_Click(object sender, EventArgs e) // экспорт в excel
        {
            /*int rowIndex = 0;
            DataTable collectedBasicParametersTable = (DataTable)ViewState["CollectedBasicParametersTable"];
            DataTable dt = collectedBasicParametersTable;
            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook excelBook = excelApp.Workbooks.Add(1);
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelBook.Worksheets.get_Item(1);
            Microsoft.Office.Interop.Excel.Range rng = null;
            DataRow dRow;
            DataColumn dCol;
            int colcnt = (int)ViewState["ValueColumnCnt"];
            List<string> colNames = (List<string>)ViewState["ColumnName"];
            for (int i = 0; i < colcnt; i++)
            {
                rng = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[1, i + 2]; //костыль
                rng.Value2 = colNames[i];
            }
            List<string> basicNames = (List<string>)ViewState["basicNames"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                rng = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[i + 2, 1]; //костыль
                rng.Value2 = basicNames[i];
            }
            for (int _row = 0; _row < dt.Rows.Count; _row++)
            {
                for (int _col = 3; _col < dt.Columns.Count; _col++)//костыль
                {
                    dRow = dt.Rows[_row];
                    dCol = dt.Columns[_col];
                    rng = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[_row + 2, _col - 1]; //костыль
                    TextBox tb = (TextBox)GridviewCollectedBasicParameters.Rows[_row].FindControl("Value" + (_col - 3)); // костылище
                    if (tb != null)
                    {
                        rng.Value2 = tb.Text;
                    }
                }
            }
            excelApp.Visible = true;*/
        }
        protected void Button2_Click(object sender, EventArgs e) // вернуться в меню 
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void Button3_Click(object sender, EventArgs e) // отправка на доработку и возвращение с доработки
        {

            KPIWebDataContext KPIWebDataContext = new KPIWebDataContext();
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
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
                                                (from a in KPIWebDataContext.CollectedBasicParametersTable
                                                 where
                                                     a.CollectedBasicParametersTableID == Convert.ToInt32(label.Text)
                                                 select a).FirstOrDefault();
                                            if (tmpColTable != null)
                                            {
                                                if ((tmpColTable.Status == 2) || (tmpColTable.Status == 1))
                                                {
                                                    tmpColTable.Status = 3;
                                                    KPIWebDataContext.SubmitChanges();
                                                }
                                                else
                                                {
                                                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 3 в отчете с ID = " );
                                                }
                                            }
                                            else
                                            {
                                                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 4 в отчете с ID = ");   
                                            }
                                        }
                                    }
                                }
                            }
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Отчёт отправлен на утверждение');" +
                                "document.location = '../Default.aspx';", true);

                            //SENDMAIL ()

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
                                                (from a in KPIWebDataContext.CollectedBasicParametersTable
                                                 where
                                                     a.CollectedBasicParametersTableID == Convert.ToInt32(label.Text)
                                                 select a).FirstOrDefault();
                                            if (tmpColTable != null)
                                            {
                                                if (tmpColTable.Status == 3)
                                                {
                                                    tmpColTable.Status = 1;
                                                    KPIWebDataContext.SubmitChanges();
                                                }
                                                else
                                                {
                                                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 5 в отчете с ID = ");
                                                }
                                            }
                                            else
                                            {
                                                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 6 в отчете с ID = ");   
                                            }
                                        }
                                    }
                                }
                            }
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Отчёт отправлен на доработку');" +
                                "document.location = '../Default.aspx';", true);
                            //SENDMAIL ()
                            #endregion
                        }
                    }
                }
                else
                {
                    LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 7 в отчете с ID = ");
                }
            }
            else
            {
                LogHandler.LogWriter.WriteLog(LogCategory.ERROR, "Пользователь " + (string)ViewState["login"] + " сгенерировал ошибку 8 в отчете с ID = ");
            }
        }
    }
}