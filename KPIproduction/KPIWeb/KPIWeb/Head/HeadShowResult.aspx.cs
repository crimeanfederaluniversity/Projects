using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;

namespace KPIWeb.Head
{
    public partial class HeadShowResult : System.Web.UI.Page
    {
        public int col_ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable_ =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();


            if ((userTable_.AccessLevel == 9)||(userTable_.AccessLevel == 10))
            {
                userTable_ =
                    (from a in kPiDataContext.UsersTable where a.UsersTableID == 8164 select a).FirstOrDefault(); // чтобы мониторинг мог зайти
                userID = userTable_.UsersTableID;//чтобы мониторинг мог зайти
            }
            
            if (userTable_.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            /////////////////////////////////////////////////////////////////
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем

            if (mode == 1)
            {
                Button1.Text = "Вернуться в меню выбора";
            }
            else if (mode == 2)
            {
                Button1.Text = "Подтвердить правильность рассчитанных данных";
            }
            if (!Page.IsPostBack)
            {
               // int UserID = UserSer.Id;
                int ReportArchiveID;
                ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
              ///  kPiDataContext kPiDataContext = new kPiDataContext();

              //  UsersTable userTable = (from a in kPiDataContext.UsersTable where a.UsersTableID == UserID select a).FirstOrDefault();
                int l_0 = (userTable_.FK_ZeroLevelSubdivisionTable == null) ? 0 : (int)userTable_.FK_ZeroLevelSubdivisionTable;
                int l_1 = (userTable_.FK_FirstLevelSubdivisionTable == null) ? 0 : (int)userTable_.FK_FirstLevelSubdivisionTable;
                int l_2 = (userTable_.FK_SecondLevelSubdivisionTable == null) ? 0 : (int)userTable_.FK_SecondLevelSubdivisionTable;
                int l_3 = (userTable_.FK_ThirdLevelSubdivisionTable == null) ? 0 : (int)userTable_.FK_ThirdLevelSubdivisionTable;
                int l_4 = (userTable_.FK_FourthLevelSubdivisionTable == null) ? 0 : (int)userTable_.FK_FourthLevelSubdivisionTable;
                int l_5 = (userTable_.FK_FifthLevelSubdivisionTable == null) ? 0 : (int)userTable_.FK_FifthLevelSubdivisionTable;

                Serialization level = (Serialization)Session["level"];
                if (level == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                //int level = level.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем
                l_0 = level.l0;
                l_1 = level.l1;
                l_2 = level.l2;
                l_3 = level.l3;
                l_4 = level.l4;
                l_5 = level.l5;

                DataTable dt_calculate = new DataTable();
                DataTable dt_indicator = new DataTable();

                dt_calculate.Columns.Add(new DataColumn("CalculatedParametrsName", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("CalculatedParametrsResult", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("checkBoxCalcId", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("checkBoxCalc", typeof(string)));
                dt_calculate.Columns.Add(new DataColumn("info0", typeof(string)));

                dt_indicator.Columns.Add(new DataColumn("IndicatorName", typeof(string)));
                dt_indicator.Columns.Add(new DataColumn("IndicatorResult", typeof(string)));
                dt_indicator.Columns.Add(new DataColumn("info0", typeof(string)));

                #region
                List<CalculatedParametrs> list_calcParams =
                    (from a in kPiDataContext.CalculatedParametrs
                     join b in kPiDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                     on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                     join c in kPiDataContext.CalculatedParametrsAndUsersMapping
                     on a.CalculatedParametrsID equals c.FK_CalculatedParametrsTable
                     where b.FK_ReportArchiveTable == ReportArchiveID
                     && c.FK_UsersTable == userID
                      && (((c.CanEdit == true) && mode == 0)
                             || ((c.CanView == true) && mode == 1)
                             || ((c.CanConfirm == true) && mode == 2))
                     select a).ToList();

                List<int> AllcntList = new List<int>();
                List<int> InsertcntList = new List<int>();
                List<int> ConfcntList = new List<int>();
                
                //Parallel.ForEach()
                foreach(CalculatedParametrs calcPar in list_calcParams)
               // Parallel.ForEach(list_calcParams, calcPar =>
                {
#region
                    CollectedCalculatedParametrs colCalc = (from a in kPiDataContext.CollectedCalculatedParametrs
                                                            where a.FK_ReportArchiveTable == ReportArchiveID
                                                            && a.FK_CalculatedParametrs == calcPar.CalculatedParametrsID
                                                            select a).FirstOrDefault();

                    if (colCalc == null)
                    {
                        colCalc = new CollectedCalculatedParametrs();
                        colCalc.FK_CalculatedParametrs = calcPar.CalculatedParametrsID;
                        colCalc.FK_ReportArchiveTable = ReportArchiveID;
                        colCalc.Confirmed = false;
                        colCalc.Active = true;
                        colCalc.FK_UsersTable = userID;
                        colCalc.CollectedValue = null;
                        colCalc.SavedDateTime = DateTime.Now;
                        kPiDataContext.CollectedCalculatedParametrs.InsertOnSubmit(colCalc);
                        kPiDataContext.SubmitChanges();
                    }
                    float tmp = 0;
                    if (colCalc.Confirmed != true)
                    {
                        tmp = (float)CalculateAbb.CalculateForLevel(1, calcPar.Formula, ReportArchiveID,0, l_0, l_1, l_2, l_3, l_4, l_5, 0); ;

                        if ((tmp < -(float)1E+20) || (tmp > (float)1E+20)
                            || (tmp == null) || (float.IsNaN(tmp))
                            || (float.IsInfinity(tmp)) || (float.IsNegativeInfinity(tmp))
                            || (float.IsPositiveInfinity(tmp)) || (!tmp.ToString().IsFloat()))
                        {
                            tmp = (float)1E+20;
                        }

                        colCalc.CollectedValue = tmp;
                        kPiDataContext.SubmitChanges();
                    }
                    else
                    {
                        tmp = (float)colCalc.CollectedValue;
                    }

                    DataRow dataRow = dt_calculate.NewRow();
                    dataRow["CalculatedParametrsName"] = calcPar.Name;

                    if (tmp == (float)1E+20)
                    {
                        dataRow["CalculatedParametrsResult"] = "Недостаточно данных";
                    }
                    else
                    {
                        dataRow["CalculatedParametrsResult"] = tmp.ToString("0.00");
                    }

                    dataRow["checkBoxCalcId"] = colCalc.CollectedCalculatedParametrsID;
                    int Allcnt = 0;
                    int Insertcnt = 0;
                    int Confcnt = 0;
                    List<int> BasicIdList = CalculateAbb.GetBasicIdList(calcPar.Formula);
                    foreach (int Basic in BasicIdList)
                    {
                        int tmpInsert = (from a in kPiDataContext.CollectedBasicParametersTable
                                         join b in kPiDataContext.BasicParametrAdditional
                                         on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                         where b.Calculated == false
                                         && a.FK_BasicParametersTable == Basic
                                         && (a.FK_ZeroLevelSubdivisionTable == l_0 || l_0 == 0)
                                         && (a.FK_FirstLevelSubdivisionTable == l_1 || l_1 == 0)
                                         && (a.FK_SecondLevelSubdivisionTable == l_2 || l_2 == 0)
                                         && (a.FK_ThirdLevelSubdivisionTable == l_3 || l_3 == 0)
                                         && (a.FK_FourthLevelSubdivisionTable == l_4 || l_4 == 0)
                                         && (a.FK_FifthLevelSubdivisionTable == l_5 || l_5 == 0)
                                         && a.CollectedValue != null
                                         && a.FK_ReportArchiveTable == ReportArchiveID
                                         select a).ToList().Count();
                        Insertcnt += tmpInsert;

                        int tmpconf = (from a in kPiDataContext.CollectedBasicParametersTable
                                       join b in kPiDataContext.BasicParametrAdditional
                                       on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                       where b.Calculated == false
                        && a.FK_BasicParametersTable == Basic
                        && (a.FK_ZeroLevelSubdivisionTable == l_0 || l_0 == 0)
                        && (a.FK_FirstLevelSubdivisionTable == l_1 || l_1 == 0)
                        && (a.FK_SecondLevelSubdivisionTable == l_2 || l_2 == 0)
                        && (a.FK_ThirdLevelSubdivisionTable == l_3 || l_3 == 0)
                        && (a.FK_FourthLevelSubdivisionTable == l_4 || l_4 == 0)
                        && (a.FK_FifthLevelSubdivisionTable == l_5 || l_5 == 0)
                        && a.Status == 4
                        && a.FK_ReportArchiveTable == ReportArchiveID
                                       select a).ToList().Count();
                        Confcnt += tmpconf;
                        BasicParametrAdditional bpt = (from a in kPiDataContext.BasicParametrAdditional
                                                       where a.BasicParametrAdditionalID == Basic
                                                       select a).FirstOrDefault();
                        int tmpAll = 0;
                        if (bpt.SubvisionLevel == 4)
                        {
                            tmpAll = (from a in kPiDataContext.UsersTable
                                      join b in kPiDataContext.BasicParametrsAndUsersMapping
                                      on a.UsersTableID equals b.FK_UsersTable
                                      join c in kPiDataContext.ThirdLevelSubdivisionTable
                                      on a.FK_ThirdLevelSubdivisionTable equals c.ThirdLevelSubdivisionTableID
                                      join d in kPiDataContext.ThirdLevelParametrs
                                      on c.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                                      join ee in kPiDataContext.FourthLevelSubdivisionTable
                                      on c.ThirdLevelSubdivisionTableID equals ee.FK_ThirdLevelSubdivisionTable
                                      join f in kPiDataContext.FourthLevelParametrs
                                      on ee.FourthLevelSubdivisionTableID equals f.FourthLevelParametrsID
                                      join z in kPiDataContext.BasicParametrAdditional
                                      on b.FK_ParametrsTable equals z.BasicParametrAdditionalID
                                      where
                                      z.Calculated == false
                                      && ((f.SpecType == bpt.SpecType) || (bpt.SpecType == 0))
                                      && d.CanGraduate == true
                                      && a.Active == true
                                      && b.Active == true
                                      && c.Active == true
                                      && ee.Active == true
                                      && b.FK_ParametrsTable == Basic
                                      && b.CanEdit == true
                                      && ((f.IsForeignStudentsAccept == true) || (f.IsForeignStudentsAccept == bpt.ForForeignStudents))
                                      //для того чтобы специальность с иностранными студентами считала свои базовые показатели
                                      ///если БП только для инстранцев второе условие будет true
                                      ///если оба true то будут считаться специально с иностранцами
                                      ///если БП для всех то один true один false
                                      ///будут считаться все специальности
                                      select b).ToList().Count();
                        }
                        else
                        {
                            tmpAll = (from a in kPiDataContext.BasicParametersTable
                                      join b in kPiDataContext.BasicParametrsAndUsersMapping
                                      on a.BasicParametersTableID equals b.FK_ParametrsTable
                                      join c in kPiDataContext.UsersTable
                                      on b.FK_UsersTable equals c.UsersTableID
                                      join d in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                                      on a.BasicParametersTableID equals d.FK_BasicParametrsTable
                                      join z in kPiDataContext.BasicParametrAdditional
                                      on b.FK_ParametrsTable equals z.BasicParametrAdditionalID
                                      where
                                        z.Calculated == false
                                           && b.CanEdit == true
                                        && d.FK_ReportArchiveTable == ReportArchiveID
                                        && (c.FK_ZeroLevelSubdivisionTable == l_0 || l_0 == 0)
                                        && (c.FK_FirstLevelSubdivisionTable == l_1 || l_1 == 0)
                                        && (c.FK_SecondLevelSubdivisionTable == l_2 || l_2 == 0)
                                        && (c.FK_ThirdLevelSubdivisionTable == l_3 || l_3 == 0)
                                        && (c.FK_FourthLevelSubdivisionTable == l_4 || l_4 == 0)
                                        && (c.FK_FifthLevelSubdivisionTable == l_5 || l_5 == 0)
                                        && a.BasicParametersTableID == Basic
                                      select a).ToList().Count();
                        }
                        Allcnt += tmpAll;
                    }
                    if (Allcnt == 0)
                    {
                        dataRow["info0"] = "Авто";
                    }
                    else if ((Confcnt > Insertcnt)||(Insertcnt>Allcnt))
                    {
                        //SENDMAIL (email =admin  title = "ошибка в расчётных показателях у руководства" body = "Allcnt="+Allcnt+" Confcnt"+Confcnt +Insertcnt+user + report + indicatorID )                   
                        dataRow["info0"] = "Данные недоступны";
                    }  
                    else
                    {                     
                       // dataRow["info0"] = Allcnt + "/" Insertcnt + "/" + Confcnt;
                        dataRow["info0"] = (((double)Insertcnt / (double)Allcnt) * 100).ToString("0.0") + "%/" + (((double)Confcnt / (double)Insertcnt) * 100).ToString("0.0") + "%";
                    }
                    dt_calculate.Rows.Add(dataRow);
                    AllcntList.Add(Allcnt);
                    InsertcntList.Add(Insertcnt);
                    ConfcntList.Add(Confcnt);
#endregion
                }//) ;

                ViewState["AllcntC"] = AllcntList;
                ViewState["InsertcntC"] = InsertcntList;
                ViewState["ConfcntC"] = ConfcntList;

                #endregion
                #region

                List<int> AllcntListI = new List<int>();
                List<int> ConfcntListI = new List<int>();
                List<IndicatorsTable> list_indicators =
                    (from a in kPiDataContext.IndicatorsTable
                     join b in kPiDataContext.ReportArchiveAndIndicatorsMappingTable
                     on a.IndicatorsTableID equals b.FK_IndicatorsTable
                     join c in kPiDataContext.IndicatorsAndUsersMapping
                     on a.IndicatorsTableID equals c.FK_IndicatorsTable
                     where b.FK_ReportArchiveTable == ReportArchiveID
                     && c.FK_UsresTable == userID
                     && (((c.CanEdit == true) && mode == 0)
                                || ((c.CanView == true) && mode == 1)
                                || ((c.CanConfirm == true) && mode == 2))
                     select a).ToList();
                ///////////////////////////теперь индикаторы

                foreach (IndicatorsTable indicator in list_indicators)
                {
                    CollectedIndocators colInd = (from a in kPiDataContext.CollectedIndocators
                                                  where a.FK_ReportArchiveTable == ReportArchiveID
                                                  && a.FK_Indicators == indicator.IndicatorsTableID
                                                  select a).FirstOrDefault();
                    if (colInd == null)
                    {
                        colInd = new CollectedIndocators();
                        colInd.FK_Indicators = indicator.IndicatorsTableID;
                        colInd.FK_ReportArchiveTable = ReportArchiveID;
                        colInd.Confirmed = false;
                        colInd.Active = true;
                        colInd.FK_UsersTable = userID;
                        colInd.CollectedValue = null;
                        colInd.SavedDateTime = DateTime.Now;
                        kPiDataContext.CollectedIndocators.InsertOnSubmit(colInd);
                        kPiDataContext.SubmitChanges();
                    }
                    float tmp;
                    tmp = (float)CalculateAbb.CalculateForLevel(2, indicator.Formula, ReportArchiveID, 0, l_0, l_1, l_2, l_3, l_4, l_5, 0);
                    if (colInd.Confirmed != true)
                    {
                        if ((tmp < -(float)1E+20) || (tmp > (float)1E+20)
                            || (tmp == null) || (float.IsNaN(tmp))
                            || (float.IsInfinity(tmp)) || (float.IsNegativeInfinity(tmp))
                            || (float.IsPositiveInfinity(tmp)) || (!tmp.ToString().IsFloat()))
                        {
                            tmp = (float)1E+20;
                        }
                        colInd.CollectedValue = tmp;
                        colInd.LastChangeDateTime = DateTime.Now;
                        colInd.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                        kPiDataContext.SubmitChanges();
                    }

                    List<int> CollectedId = CalculateAbb.GetCollectIdList(indicator.Formula);

                    int Allcnt = CollectedId.Count();
                    int Confcnt = 0;
                    foreach (int collected in CollectedId)
                    {
                        int tmpAll = (from a in kPiDataContext.CollectedCalculatedParametrs
                                      where a.FK_CalculatedParametrs == collected
                                      && a.Confirmed == true
                                      select a).ToList().Count();
                        Confcnt += tmpAll;
                    }

                    DataRow dataRow = dt_indicator.NewRow();
                    dataRow["IndicatorName"] = indicator.Name;
                    if (tmp == (float)1E+20)
                    {
                        dataRow["IndicatorResult"] = "Недостаточно данных";
                    }
                    else
                    {
                        dataRow["IndicatorResult"] = tmp.ToString("0.00");
                    }
                    if (Allcnt < Confcnt) //что то не так
                    {
                        dataRow["info0"] = "Данные недоступны";
                        //SENDMAIL (email =admin  title = "ошибка в индикаторах у руководства" body = "Allcnt="+Allcnt+" Confcnt"+Confcnt +user + report + indicatorID )
             
                    }
                    else
                    {
                        dataRow["info0"] = Confcnt + "  из " + Allcnt;
                    }
                    dt_indicator.Rows.Add(dataRow);
                    
                    AllcntListI.Add(Allcnt);
                    ConfcntListI.Add(Confcnt);
                }
                ViewState["AllcntI"] = AllcntListI;
                ViewState["ConfcntI"] = ConfcntListI;

                #endregion

                CalculatedParametrsTable.DataSource = dt_calculate;
                CalculatedParametrsTable.DataBind();
                IndicatorsTable.DataSource = dt_indicator;
                IndicatorsTable.DataBind();

                Button8.BackColor = System.Drawing.Color.LightSalmon;
                Button7.BackColor = System.Drawing.Color.Yellow;
                Button4.BackColor = System.Drawing.Color.LimeGreen;
                Button5.BackColor = System.Drawing.Color.Red;
                Button6.BackColor = System.Drawing.Color.LightSkyBlue;
                Button9.BackColor = System.Drawing.Color.FloralWhite;

                Label1.Text = "Данные Полностью заполнены и частично утверждены";
                Label2.Text = "Данные Заполнены и утверждены уровнем ответственных ниже";
                Label3.Text = "Данные готовы к отправке (заполнены и утверждены)";
                Label4.Text = "Ошибка";
                Label5.Text = "Данные рассчитываются автоматически";
                Label6.Text = "Данные неполностью заполнены";
                

                if (mode == 2)
                {
                    //IndicatorsTable.Columns[2].Visible = true;
                    CalculatedParametrsTable.Columns[2].Visible = true;
                }

                
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем

            if (mode == 1)
            {
                Response.Redirect("~/Default.aspx");
            }
            else if (mode == 2)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                for (int i = 0; i < IndicatorsTable.Rows.Count; i++)
                {
                    var chB = IndicatorsTable.Rows[i].FindControl("checkBoxInd") as CheckBox;
                    var lbl = IndicatorsTable.Rows[i].FindControl("checkBoxIndId") as Label;
                    if (chB != null)
                    {
                        if (chB.Checked)
                        {
                            CollectedIndocators ColIndicator = (from a in kPiDataContext.CollectedIndocators
                                                                where a.CollectedIndocatorsID == Convert.ToInt32(lbl.Text)
                                                                select a).FirstOrDefault();
                            ColIndicator.Confirmed = true;
                            kPiDataContext.SubmitChanges();
                        }
                    }
                }

                for (int i = 0; i < CalculatedParametrsTable.Rows.Count; i++)
                {
                    var chB = CalculatedParametrsTable.Rows[i].FindControl("checkBoxCalc") as CheckBox;
                    var lbl = CalculatedParametrsTable.Rows[i].FindControl("checkBoxCalcId") as Label;
                    if (chB.Checked)
                    {
                        CollectedCalculatedParametrs ColCalc = (from a in kPiDataContext.CollectedCalculatedParametrs
                                                                where a.CollectedCalculatedParametrsID == Convert.ToInt32(lbl.Text)
                                                                select a).FirstOrDefault();
                        ColCalc.Confirmed = true;
                        kPiDataContext.SubmitChanges();
                    }
                }
            }
        }

        protected void IndicatorsTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           /* Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;*/
            /*
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            int ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
            */
            /*
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем
            */
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

            List<int> AllcntList = (List<int>)ViewState["AllcntI"];
            List<int> ConfcntList = (List<int>)ViewState["ConfcntI"];

            if ((e.Row.RowIndex >= 0) && (e.Row.RowIndex < AllcntList.Count()))
            {
                var lbl = e.Row.FindControl("info0") as Label;

                int Allcnt = AllcntList[e.Row.RowIndex];
                int Confcnt = ConfcntList[e.Row.RowIndex];

                if (Allcnt == Confcnt)
                {
                    lbl.BackColor = confirmedColor;
                    DataControlFieldCell d = lbl.Parent as DataControlFieldCell;
                    d.BackColor = confirmedColor;
                }
            }
        }

        protected void CalculatedParametrsTable_RowDataBound(object sender, GridViewRowEventArgs e)
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

            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
           /* if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            int ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
            */
            
            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и утверждаем
             
            List<int> AllcntList = (List<int>)ViewState["AllcntC"];
            List<int> InsertcntList = (List<int>)ViewState["InsertcntC"];
            List<int> ConfcntList = (List<int>)ViewState["ConfcntC"];

            if ((e.Row.RowIndex >= 0) && (e.Row.RowIndex < AllcntList.Count()))
            {
                var chB = e.Row.FindControl("checkBoxCalc") as CheckBox;
                var lbl = e.Row.FindControl("checkBoxCalcId") as Label;

                int Allcnt = AllcntList[e.Row.RowIndex];
                int Insertcnt = InsertcntList[e.Row.RowIndex];
                int Confcnt = ConfcntList[e.Row.RowIndex];

                Color boxColor = color;

                if (Allcnt == Insertcnt)
                {
                    boxColor = System.Drawing.Color.LightSalmon;
                }

                if ((Allcnt == Insertcnt) && (Allcnt == Confcnt))
                {
                    boxColor = System.Drawing.Color.Yellow;
                }

                if (chB != null) //Параметр утвержден
                {
                    if (mode != 2)
                    {
                        chB.Visible = false;
                        chB.Checked = (from a in kPiDataContext.CollectedCalculatedParametrs
                                       where a.CollectedCalculatedParametrsID == Convert.ToInt32(lbl.Text)
                                       select a.Confirmed).FirstOrDefault() == true ? true : false;
                        if (chB.Checked)
                        {
                            boxColor = System.Drawing.Color.LimeGreen;
                        }
                    }
                    else
                    {
                        chB.Visible = true;
                        chB.Checked = (from a in kPiDataContext.CollectedCalculatedParametrs
                                       where a.CollectedCalculatedParametrsID == Convert.ToInt32(lbl.Text)
                                       select a.Confirmed).FirstOrDefault() == true ? true : false;

                        if (chB.Checked)
                        {
                            boxColor = System.Drawing.Color.LimeGreen;
                            chB.Enabled = false;
                        }
                        if ((Allcnt != Insertcnt) || (Insertcnt != Confcnt))
                        {
                            chB.Enabled = false;
                        }
                        if (Allcnt == 0)
                        {
                            chB.Enabled = false;
                        }
                    }
                }
                if (Allcnt == 0)
                {
                    boxColor = System.Drawing.Color.LightSkyBlue;
                }

                if ((Confcnt > Insertcnt) || (Insertcnt > Allcnt))
                {
                    boxColor = System.Drawing.Color.Red;
                }

                var lbl_ = e.Row.FindControl("info0") as Label;
                if (lbl_ != null)
                {
                    lbl_.BackColor = boxColor;
                    DataControlFieldCell d = lbl_.Parent as DataControlFieldCell;
                    d.BackColor = boxColor;
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
        }

    }
}