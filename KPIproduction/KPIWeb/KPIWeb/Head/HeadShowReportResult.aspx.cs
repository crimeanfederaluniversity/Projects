using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Text.RegularExpressions;
using System.Dynamic;
using System.Text;
using System.Web.WebPages;
using System.Drawing;

namespace KPIWeb.Head
{
    public partial class HeadShowReportResult : System.Web.UI.Page
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
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            UsersTable userTable_ =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

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
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем

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
                int UserID = UserSer.Id;
                int ReportArchiveID;
                ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                UsersTable userTable = (from a in kpiWebDataContext.UsersTable where a.UsersTableID == UserID select a).FirstOrDefault();
                int l_0 = (userTable.FK_ZeroLevelSubdivisionTable == null)?0:(int)userTable.FK_ZeroLevelSubdivisionTable;
                int l_1 = (userTable.FK_FirstLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_FirstLevelSubdivisionTable;
                int l_2 = (userTable.FK_SecondLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_SecondLevelSubdivisionTable;
                int l_3 = (userTable.FK_ThirdLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_ThirdLevelSubdivisionTable;
                int l_4 = (userTable.FK_FourthLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_FourthLevelSubdivisionTable;
                int l_5 = (userTable.FK_FifthLevelSubdivisionTable == null) ? 0 : (int)userTable.FK_FifthLevelSubdivisionTable;

                Serialization level = (Serialization)Session["level"];
                if (level == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                //int level = level.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем
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
                    (from a in kpiWebDataContext.CalculatedParametrs
                        join b in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                        on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                        join c in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                        on a.CalculatedParametrsID equals c.FK_CalculatedParametrsTable
                        where b.FK_ReportArchiveTable == ReportArchiveID 
                        && c.FK_UsersTable == UserID
                         && (((c.CanEdit == true) && mode == 0)
                                || ((c.CanView == true) && mode == 1)
                                || ((c.CanConfirm == true) && mode == 2))
                        select  a).ToList();
                
                foreach (CalculatedParametrs calcPar in list_calcParams)
                {
                    CollectedCalculatedParametrs colCalc = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                    where a.FK_ReportArchiveTable == ReportArchiveID
                    && a.FK_CalculatedParametrs == calcPar.CalculatedParametrsID
                    select a).FirstOrDefault();
                                            
                    if(colCalc==null)
                    {
                        colCalc = new CollectedCalculatedParametrs();
                        colCalc.FK_CalculatedParametrs = calcPar.CalculatedParametrsID;
                        colCalc.FK_ReportArchiveTable = ReportArchiveID;
                        colCalc.Confirmed =false;
                        colCalc.Active = true;
                        colCalc.FK_UsersTable = userID;                                                   
                        colCalc.CollectedValue = null;
                        colCalc.SavedDateTime = DateTime.Now;
                        kpiWebDataContext.CollectedCalculatedParametrs.InsertOnSubmit(colCalc);
                        kpiWebDataContext.SubmitChanges();
                     }
                    float tmp = 0;
                        if (colCalc.Confirmed != true)
                        {
                            tmp = (float)CalculateAbb.CalculateForLevel(1, calcPar.Formula, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5, 0); ;

                            if ((tmp < -(float)1E+20) || (tmp > (float)1E+20)
                                || (tmp == null) || (float.IsNaN(tmp))
                                || (float.IsInfinity(tmp)) || (float.IsNegativeInfinity(tmp))
                                || (float.IsPositiveInfinity(tmp)) || (!tmp.ToString().IsFloat()))
                            {
                                tmp = (float)1E+20;
                            }

                            colCalc.CollectedValue = tmp;
                            kpiWebDataContext.SubmitChanges();
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
                            dataRow["CalculatedParametrsResult"] = tmp.ToString("0.000");
                        }
                            
                        dataRow["checkBoxCalcId"] = colCalc.CollectedCalculatedParametrsID;
                        int Allcnt = 0;
                        int Insertcnt = 0;
                        int Confcnt = 0;
                        List<int> BasicIdList = CalculateAbb.GetBasicIdList(calcPar.Formula);
                        foreach (int Basic in BasicIdList)
                        { 
                             int tmpInsert = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                              join b in kpiWebDataContext.BasicParametrAdditional
                                              on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                             where b.Calculated == false
                             &&  a.FK_BasicParametersTable == Basic
                             && (a.FK_ZeroLevelSubdivisionTable   == l_0 || l_0 == 0)
                             && (a.FK_FirstLevelSubdivisionTable  == l_1 || l_1 == 0)
                             && (a.FK_SecondLevelSubdivisionTable == l_2 || l_2 == 0)
                             && (a.FK_ThirdLevelSubdivisionTable  == l_3 || l_3 == 0)
                             && (a.FK_FourthLevelSubdivisionTable == l_4 || l_4 == 0)
                             && (a.FK_FifthLevelSubdivisionTable  == l_5 || l_5 == 0)   
                             && a.CollectedValue != null
                             && a.FK_ReportArchiveTable == ReportArchiveID
                             select a).ToList().Count();
                             Insertcnt+=tmpInsert;

                             int tmpconf = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                            join b in kpiWebDataContext.BasicParametrAdditional
                                            on a.FK_BasicParametersTable equals b.BasicParametrAdditionalID
                                            where b.Calculated == false                                            
                             && a.FK_BasicParametersTable == Basic
                             && (a.FK_ZeroLevelSubdivisionTable   == l_0 || l_0 == 0)
                             && (a.FK_FirstLevelSubdivisionTable  == l_1 || l_1 == 0)
                             && (a.FK_SecondLevelSubdivisionTable == l_2 || l_2 == 0)
                             && (a.FK_ThirdLevelSubdivisionTable  == l_3 || l_3 == 0)
                             && (a.FK_FourthLevelSubdivisionTable == l_4 || l_4 == 0)
                             && (a.FK_FifthLevelSubdivisionTable  == l_5 || l_5 == 0)   
                             && a.ConfirmedThirdLevel == true
                             && a.FK_ReportArchiveTable == ReportArchiveID
                             select a).ToList().Count();
                             Confcnt+=tmpconf;
                             BasicParametrAdditional bpt = (from a in kpiWebDataContext.BasicParametrAdditional
                                                         where a.BasicParametrAdditionalID == Basic
                                                         select a).FirstOrDefault();
                             int tmpAll = 0 ;
                             if (bpt.SubvisionLevel == 4)
                             {                                         
                                 tmpAll = (from a in kpiWebDataContext.UsersTable
                                           join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                           on a.UsersTableID equals b.FK_UsersTable
                                           join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                           on a.FK_ThirdLevelSubdivisionTable equals c.ThirdLevelSubdivisionTableID
                                           join d in kpiWebDataContext.ThirdLevelParametrs
                                           on c.ThirdLevelSubdivisionTableID equals d.ThirdLevelParametrsID
                                           join ee in kpiWebDataContext.FourthLevelSubdivisionTable
                                           on c.ThirdLevelSubdivisionTableID equals ee.FK_ThirdLevelSubdivisionTable
                                           join f in kpiWebDataContext.FourthLevelParametrs
                                           on ee.FourthLevelSubdivisionTableID equals f.FourthLevelParametrsID
                                           join z in kpiWebDataContext.BasicParametrAdditional
                                           on b.FK_ParametrsTable equals z.BasicParametrAdditionalID
                                           where
                                           z.Calculated == false
                                           && ((f.SpecType == bpt.SpecType) || (bpt.SpecType==0))
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
                                 tmpAll = (from a in kpiWebDataContext.BasicParametersTable
                                               join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                               on a.BasicParametersTableID equals b.FK_ParametrsTable
                                               join c in kpiWebDataContext.UsersTable
                                               on b.FK_UsersTable equals c.UsersTableID
                                               join d in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                               on a.BasicParametersTableID equals d.FK_BasicParametrsTable
                                                join z in kpiWebDataContext.BasicParametrAdditional
                                                on b.FK_ParametrsTable equals z.BasicParametrAdditionalID
                                               where
                                                 z.Calculated==false
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
                        dataRow["info0"] =Allcnt+"/"+ Insertcnt + "/" + Confcnt;
                        dt_calculate.Rows.Add(dataRow);
                }
                #endregion
                #region
                List<IndicatorsTable> list_indicators =
                    (from a in kpiWebDataContext.IndicatorsTable
                     join b in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                     on a.IndicatorsTableID equals b.FK_IndicatorsTable
                     join c in kpiWebDataContext.IndicatorsAndUsersMapping
                     on a.IndicatorsTableID equals c.FK_IndicatorsTable
                     where b.FK_ReportArchiveTable == ReportArchiveID
                     && c.FK_UsresTable == UserID
                     && (((c.CanEdit == true) && mode == 0)
                                || ((c.CanView == true) && mode == 1)
                                || ((c.CanConfirm == true) && mode == 2))
                     select a).ToList();
                ///////////////////////////теперь индикаторы
                
                foreach (IndicatorsTable indicator in list_indicators)
                {
                    CollectedIndocators colInd = (from a in kpiWebDataContext.CollectedIndocators
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
                        kpiWebDataContext.CollectedIndocators.InsertOnSubmit(colInd);
                        kpiWebDataContext.SubmitChanges();
                    }
                    float tmp;
                        tmp = (float)CalculateAbb.CalculateForLevel(2,indicator.Formula, ReportArchiveID, l_0, l_1, l_2, l_3, l_4, l_5, 0); 
                    if (colInd.Confirmed!=true)
                    {
                        if ((tmp < -(float)1E+20) || (tmp > (float)1E+20)
                            ||(tmp==null)||(float.IsNaN(tmp))
                            ||(float.IsInfinity(tmp))||(float.IsNegativeInfinity(tmp))
                            ||(float.IsPositiveInfinity(tmp))|| (!tmp.ToString().IsFloat()))
                        {
                            tmp = (float) 1E+20;
                        }                     
                        colInd.CollectedValue = tmp;
                        colInd.LastChangeDateTime = DateTime.Now;
                        colInd.UserIP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).Select(ip => ip.ToString()).FirstOrDefault() ?? "";
                        kpiWebDataContext.SubmitChanges();
                    }

                    List<int> CollectedId = CalculateAbb.GetCollectIdList(indicator.Formula);

                        int Allcnt = CollectedId.Count();
                        int Confcnt = 0;
                        foreach(int collected in CollectedId)
                        {
                            int tmpAll = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                             where a.FK_CalculatedParametrs == collected                            
                             && a.Confirmed == true
                             select a).ToList().Count();
                            Confcnt += tmpAll;
                        }

                    DataRow dataRow = dt_indicator.NewRow();
                    dataRow["IndicatorName"] = indicator.Name;
                    if (tmp == (float) 1E+20)
                    {
                        dataRow["IndicatorResult"] = "Недостаточно данных";
                    }
                    else
                    {
                        dataRow["IndicatorResult"] = tmp.ToString("0.000");
                    }
                    dataRow["info0"] = Confcnt +"  из "+ Allcnt;
                    dt_indicator.Rows.Add(dataRow);
                }
                #endregion

                CalculatedParametrsTable.DataSource = dt_calculate;
                CalculatedParametrsTable.DataBind();
                IndicatorsTable.DataSource = dt_indicator;
                IndicatorsTable.DataBind();
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
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем

            if (mode == 1)
            {
                Response.Redirect("~/Default.aspx");
            }
            else if (mode == 2)
            {
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                for (int i=0; i<IndicatorsTable.Rows.Count;i++)
                {
                    var chB = IndicatorsTable.Rows[i].FindControl("checkBoxInd") as CheckBox;
                    var lbl = IndicatorsTable.Rows[i].FindControl("checkBoxIndId") as Label;
                    if (chB != null)
                    {
                        if (chB.Checked)
                        {
                            CollectedIndocators ColIndicator = (from a in kpiWebDataContext.CollectedIndocators
                                                                where a.CollectedIndocatorsID == Convert.ToInt32(lbl.Text)
                                                                select a).FirstOrDefault();
                            ColIndicator.Confirmed = true;
                            kpiWebDataContext.SubmitChanges();
                        }
                    }
                }

                for (int i=0; i<CalculatedParametrsTable.Rows.Count;i++)
                {
                    var chB = CalculatedParametrsTable.Rows[i].FindControl("checkBoxCalc") as CheckBox;
                    var lbl = CalculatedParametrsTable.Rows[i].FindControl("checkBoxCalcId") as Label;
                    if (chB.Checked)
                    {
                        CollectedCalculatedParametrs ColCalc = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                                                where a.CollectedCalculatedParametrsID == Convert.ToInt32(lbl.Text)
                                                                    select a).FirstOrDefault();
                        ColCalc.Confirmed = true;
                        kpiWebDataContext.SubmitChanges();
                    }                   
                }             
            }
        }

        protected void IndicatorsTable_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
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

            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем

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

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            var chB = e.Row.FindControl("checkBoxInd") as CheckBox;      
            var lbl = e.Row.FindControl("checkBoxIndId") as Label;
            if(chB !=null)
            {
                if (mode != 2)
                {
                    chB.Visible = false;
                }
                else
                {
                    e.Row.Visible = true;
                    chB.Checked = (from a in kpiWebDataContext.CollectedIndocators
                                   where a.CollectedIndocatorsID == Convert.ToInt32(lbl.Text)
                                   select a.Confirmed).FirstOrDefault() == true ? true : false;
                    if (chB.Checked)
                    {
                        chB.Enabled = false;
                    }
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

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
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

            Serialization modeSer = (Serialization)Session["mode"];
            if (modeSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int mode = modeSer.mode; // 0 заполняем // 1 смотрим // 2 смотрим и подтверждаем
            ///////
            var chB = e.Row.FindControl("checkBoxCalc") as CheckBox;
            var lbl = e.Row.FindControl("checkBoxCalcId") as Label;
            if (chB != null)
            {
                if (mode != 2)
                {
                    chB.Visible = false;
                }
                else
                {
                    e.Row.Visible = true;
                    chB.Checked = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                   where a.CollectedCalculatedParametrsID == Convert.ToInt32(lbl.Text)
                                   select a.Confirmed).FirstOrDefault() == true ? true : false;
                    if (chB.Checked)
                    {
                        chB.Enabled = false;
                    }
                }
            }
        }

    }
}