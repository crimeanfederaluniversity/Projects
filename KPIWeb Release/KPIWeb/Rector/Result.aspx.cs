﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using System.Net;
using System.Text;
using Microsoft.Ajax.Utilities;
using Button = System.Web.UI.WebControls.Button;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;

namespace KPIWeb.Rector
{
    public partial class Result : System.Web.UI.Page
    {
        public int FindUserId(int deepness, int id)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            //int userID =0
            UsersTable user = null;
            if (deepness == 2)
            {
               user = (from a in kpiWebDataContext.UsersTable
                           where a.Active == true
                                 && a.FK_FirstLevelSubdivisionTable == id
                                 && a.FK_SecondLevelSubdivisionTable == null
                           select a).FirstOrDefault();
            }
            if (deepness == 3)
            {
                user = (from a in kpiWebDataContext.UsersTable
                           where a.Active == true
                                 && a.FK_SecondLevelSubdivisionTable == id
                                 && a.FK_ThirdLevelSubdivisionTable == null
                           select a).FirstOrDefault();               
            }
            if (deepness == 4)
            {
                user = (from a in kpiWebDataContext.UsersTable
                           where a.Active == true
                                 && a.FK_ThirdLevelSubdivisionTable == id
                                 && a.FK_FourthLevelSubdivisionTable == null
                                 join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                 on a.UsersTableID equals b.FK_UsersTable
                                 where b.Active == true
                                 && b.CanConfirm == true
                           select a).FirstOrDefault();
            }
            if (user == null)
            return 0;

            return user.UsersTableID;

        }
        public bool FindUser(ForRCalc.Struct curStruct, int id)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            int deepness = ForRCalc.StructDeepness(curStruct);

            if (deepness == 2)
            {
                int cnt = (from a in kpiWebDataContext.UsersTable
                    where a.Active == true
                          && a.FK_FirstLevelSubdivisionTable == id
                          && a.FK_SecondLevelSubdivisionTable == null
                    select a).Count();
                if (cnt > 0) return true;
            }
            if (deepness == 3)
            {
                int cnt = (from a in kpiWebDataContext.UsersTable
                           where a.Active == true
                                 && a.FK_SecondLevelSubdivisionTable == id
                                 && a.FK_ThirdLevelSubdivisionTable == null
                           select a).Count();
                if (cnt > 0) return true;
            }
            if (deepness == 4)
            {
                int cnt = (from a in kpiWebDataContext.UsersTable
                           where a.Active == true
                                 && a.FK_ThirdLevelSubdivisionTable == id
                                 && a.FK_FourthLevelSubdivisionTable == null
                           join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                           on a.UsersTableID equals b.FK_UsersTable
                           where b.Active == true
                           && b.CanConfirm == true
                           select a).Count();
                if (cnt > 0) return true;
            }
            return false;
        }
        public class MyObject
        {
            public int Id;
            public int ParentId;
            public string Name;
            public string UrlAddr;
            public int Active;
        }
        public class ObjectToSort
        {
            public ForRCalc.Struct ObjStruct { get; set; }
            public int ID { get; set; }
            public string Number { get; set; }
            public string Name { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string Comment { get; set; }
            public string CommentEnabled { get; set; }
            public bool CanConfirm { get; set; }
            public bool ShowLable { get; set; }
            public bool CanWatchWhoOws { get; set; }
            public string LableText { get; set; }
            public string LableColor { get; set; }
            public double Value { get; set; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            #region get user data

            Panel5.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
            Panel7.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
            Panel6.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");


            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;

            UsersTable userTable_ =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            ViewState["login"] =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a.Email).FirstOrDefault();

            if (userTable_.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }


          //  string parameter = Request["__EVENTARGUMENT"];
            string target = Request["__EVENTTARGET"];
            string parameter = Request["__EVENTARGUMENT"];
            if (parameter != null && target!=null)
            {
                if (target == "CommentSendParam")
                {
                    int ParamId = -1;
                    if (int.TryParse(parameter, out ParamId) && ParamId > 0)
                    {
                        DoConfirm(ParamId);
                    }
                }

                if (target == "EmailSendParam")
                {
                    if (parameter.Contains('_'))
                    {
                        string[] str = parameter.Split('_');
                        int deepness = 0;
                        int structID = 0;
                        int.TryParse(str[0], out deepness);
                        int.TryParse(str[1], out structID);
                        if ((deepness > 0) && (structID > 0))
                        {
                            string messageTo = "";
                            UsersTable userToSend = new UsersTable();
                            if (deepness == 2)
                            {
                                 userToSend = (from a in kpiWebDataContext.UsersTable
                                    where a.FK_FirstLevelSubdivisionTable == structID
                                          && a.AccessLevel == 4
                                    select a).FirstOrDefault();

                                FirstLevelSubdivisionTable tmpfirst = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                    where a.FirstLevelSubdivisionTableID == structID
                                        select a).FirstOrDefault();
                                    if (tmpfirst != null)
                                    {
                                        messageTo = tmpfirst.Name.Replace('\n', ' ').Replace('\r', ' ');
                                    }
                            }
                            else if (deepness == 3)
                            {
                                 userToSend = (from a in kpiWebDataContext.UsersTable
                                                join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                                on a.FK_FirstLevelSubdivisionTable equals b.FK_FirstLevelSubdivisionTable
                                                where b.SecondLevelSubdivisionTableID == structID
                                                && a.AccessLevel == 4
                                                select a).FirstOrDefault();

                                FirstLevelSubdivisionTable tmpfirst = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                                       join b in kpiWebDataContext.SecondLevelSubdivisionTable 
                                                                       on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                                                       where b.SecondLevelSubdivisionTableID == structID
                                                                       select a).FirstOrDefault();

                                
                                SecondLevelSubdivisionTable tmpSecond =(from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                       where a.SecondLevelSubdivisionTableID == structID
                                                                       select a).FirstOrDefault();
                                    if (tmpfirst != null)
                                    {
                                        messageTo = tmpfirst.Name.Replace('\n', ' ').Replace('\r', ' ')+", "+tmpSecond.Name.Replace('\n', ' ').Replace('\r', ' ');
                                    }
                            }
                            else if (deepness == 4)
                            {
                                 userToSend = (from a in kpiWebDataContext.UsersTable
                                                join b in kpiWebDataContext.SecondLevelSubdivisionTable
                                                on a.FK_FirstLevelSubdivisionTable equals b.FK_FirstLevelSubdivisionTable
                                                join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                                                where c.ThirdLevelSubdivisionTableID == structID
                                                && a.AccessLevel == 4                                              
                                                select a).FirstOrDefault();

                                FirstLevelSubdivisionTable tmpfirst = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                                       join b in kpiWebDataContext.SecondLevelSubdivisionTable 
                                                                       on a.FirstLevelSubdivisionTableID equals b.FK_FirstLevelSubdivisionTable
                                                                       join c in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                       on b.SecondLevelSubdivisionTableID equals c.FK_SecondLevelSubdivisionTable
                                                                       where c.ThirdLevelSubdivisionTableID == structID
                                                                       select a).FirstOrDefault();

                                
                                SecondLevelSubdivisionTable tmpSecond =(from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                        join b in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                        on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                                                       where b.ThirdLevelSubdivisionTableID == structID
                                                                       select a).FirstOrDefault();
                                ThirdLevelSubdivisionTable tmpThird =
                                    (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                        where a.ThirdLevelSubdivisionTableID == structID
                                        select a).FirstOrDefault();
                                    if (tmpfirst != null)
                                    {
                                        messageTo = tmpfirst.Name.Replace('\n', ' ').Replace('\r', ' ') + ", " + tmpSecond.Name.Replace('\n', ' ').Replace('\r', ' ') + ", " + tmpThird.Name.Replace('\n', ' ').Replace('\r',  ' ');
                                    }
                            }                            
                            //FindUserId(deepness, structID);
                            if (userToSend !=null)
                            {
                                EmailTemplate EmailParams = (from a in kpiWebDataContext.EmailTemplate
                                                             where a.Name == "ProrectorMessage2"
                                                             && a.Active == true
                                                             select a).FirstOrDefault();
                                string userPosition = userTable_.Position;
                                if (userTable_.Email == "muravasereda.aurika@gmail.com")
                                {
                                    userPosition = "Директор Департамента управления качеством и проектных решений";
                                }
                                        Action.MassMailing(userToSend.Email,
                                        EmailParams.EmailTitle.Replace("#TITLE#", EmailTitle.Text),
                                        
                                        EmailParams.EmailContent.Replace("#CONTENT#", TextBox2.Text)
                                            .Replace("#SENDFROM#", userPosition + " " + userTable_.Email).Replace("#SENDTO#", messageTo), null);


                                    LogHandler.LogWriter.WriteLog(LogCategory.INFO,
                                        "Prorector " + userTable_.Email + " sent a message to director of user id=" +
                                        userToSend.UsersTableID.ToString());
                            }
                        }
                    }
                }
            }

            

            #endregion

            if (!IsPostBack)
            {
                #region session

                RectorHistorySession rectorHistory = (RectorHistorySession) Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                ShowUnConfirmed unConfirmed = (ShowUnConfirmed) Session["unConfirmed"];
                bool ShowUnconfirmed = true;
                if (unConfirmed == null)
                {
                    ShowUnconfirmed = false;
                }
                else
                {
                    if (unConfirmed.DoShowUnConfirmed == false)
                    {
                        ShowUnconfirmed = false;
                    }
                    else
                    {
                        Button7.Enabled = false;
                        unConfirmed.DoShowUnConfirmed = false;
                        Session["unConfirmed"] = unConfirmed;
                    }
                }



                #region check for get

                string val = this.Request.QueryString["HLevel"]; //hisoty level сова придумал)
                if (val != null)
                {
                    rectorHistory.CurrentSession = Convert.ToInt32(val);
                    Session["rectorHistory"] = rectorHistory;
                }

                #endregion

                if ((rectorHistory.SessionCount - rectorHistory.CurrentSession) < 2)
                {
                    GoForwardButton.Enabled = false;
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                #endregion

                #region DataTable init

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ID", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Number", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Abb", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
                dataTable.Columns.Add(new DataColumn("StartDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("EndDate", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Value", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Title", typeof (string)));
                dataTable.Columns.Add(new DataColumn("PlannedValue", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Progress", typeof (string)));
                dataTable.Columns.Add(new DataColumn("Color", typeof (string)));
                dataTable.Columns.Add(new DataColumn("UserID", typeof(string))); // тут вместо ID пользователя которому нужно отправлять сообщение, хранится через подчеркивание глубина структурного подразделения и ID его струтурного подразделения
                dataTable.Columns.Add(new DataColumn("UserPosition", typeof(string)));
                
                ///color table
                /// 0 - no color  // can't confirm
                /// 1 - green (confirmed)
                /// 2 - red (unconfirmed but calculated)
                /// 3 - orange (can confirm)
                /// 
                dataTable.Columns.Add(new DataColumn("CanWatchWhoOws", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("CanConfirm", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("ShowLable", typeof (bool)));
                dataTable.Columns.Add(new DataColumn("LableText", typeof (string)));
                dataTable.Columns.Add(new DataColumn("LableColor", typeof (string)));

                dataTable.Columns.Add(new DataColumn("Comment", typeof (string)));
                dataTable.Columns.Add(new DataColumn("CommentEnabled", typeof (string)));

                #endregion

                #region global page settings

                ReportArchiveTable ReportTable = (from a in kpiWebDataContext.ReportArchiveTable
                    where a.ReportArchiveTableID == ReportID
                    select a).FirstOrDefault();

                double daysLeft = ((DateTime) ReportTable.EndDateTime - DateTime.Now).TotalDays;

                ReportTitle.Text = ReportTable.Name + " " + ReportTable.StartDateTime.ToString().Split(' ')[0] + " - " +
                                   ReportTable.EndDateTime.ToString().Split(' ')[0];

                #endregion

                #region Show Uncinfirmed Button 

                Button7.Visible = false;
                if ((daysLeft < ReportTable.DaysBeforeToCalcForRector) &&
                    (ReportTable.DaysBeforeToCalcForRector != 0))
                {
                    Button7.Enabled = true;
                }
                else
                {
                    Button7.Enabled = false;
                }

                if (ShowUnconfirmed == true)
                {
                    Button7.Enabled = false;
                }

                #endregion

                if (ViewType == 0) // просмотр для структурных подразделений
                {
                    #region преднастройка страницы                    

                    string title = "";
                    PageFullName.Text = "";
                    if (ParamType == 0)
                    {
                        PageFullName.Text += "<b>";
                        PageFullName.Text += (from a in kpiWebDataContext.IndicatorsTable
                            where a.IndicatorsTableID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</b>  </br>";
                    }
                    else if (ParamType == 1)
                    {
                        PageFullName.Text += "<b>";
                        PageFullName.Text += (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</b>  </br>";
                    }
                    else if (ParamType == 2)
                    {
                        PageFullName.Text += "<b>";
                        PageFullName.Text += (from a in kpiWebDataContext.BasicParametersTable
                            where a.BasicParametersTableID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</b>  </br>";
                    }

                    int Deep = ForRCalc.StructDeepness(mainStruct);
                    if (Deep == 1)
                    {
                    }
                    if (Deep == 2)
                    {
                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }
                    if (Deep == 3)
                    {
                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                            where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }
                    if (Deep == 4)
                    {

                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                            where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                            where a.ThirdLevelSubdivisionTableID == mainStruct.Lv_3
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }

                    if (SpecID != 0)
                    {
                        PageFullName.Text += "Направление подготовки \"" +
                                             (from a in kpiWebDataContext.SpecializationTable
                                                 where a.SpecializationTableID == SpecID
                                                 select a.Name).FirstOrDefault() + "\" </br>";
                    }
                    if (mainStruct.Lv_1 == 0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "КФУ");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    else
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, mainStruct.Name);
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    //задади имя текущей сессии


                    title = "Подразделения";
                    if (ForRCalc.StructDeepness(mainStruct) > 3)
                    {
                        title = "Направления подготовки";
                    }


                    #endregion

                    #region fill grid

                    int BasicParamLevel = 0;
                    if (ParamType == 2)
                    {
                        BasicParamLevel = (int) (from a in kpiWebDataContext.BasicParametrAdditional
                            where a.BasicParametrAdditionalID == ParamID
                            select a.SubvisionLevel).FirstOrDefault();

                    }
                    List<ForRCalc.Struct> currentStructList = new List<ForRCalc.Struct>();
                    if (SpecID != 0)
                    {
                        currentStructList = ForRCalc.GetChildStructList(mainStruct, ReportID, SpecID);
                    }
                    else
                    {
                        currentStructList = ForRCalc.GetChildStructList(mainStruct, ReportID);
                    }

                    List<ObjectToSort> sorted = new List<ObjectToSort>();

                    foreach (ForRCalc.Struct currentStruct in currentStructList)
                    {

                        ObjectToSort OtS = new ObjectToSort();
                        OtS.ObjStruct = currentStruct;
                        OtS.ID = ForRCalc.GetLastID(currentStruct);
                        OtS.Number = "num";
                        OtS.Name = currentStruct.Name;
                        OtS.StartDate = "nun";
                        OtS.EndDate = "nun";
                        OtS.Comment = "nun";
                        OtS.CommentEnabled = "hidden";

                        OtS.CanConfirm = true;
                        OtS.ShowLable = false;
                        OtS.CanWatchWhoOws = false;

                        OtS.LableText = "";
                        OtS.LableColor = "#000000";

                        OtS.Value =
                            ForRCalc.GetCalculatedWithParams(currentStruct, ParamType, ParamID, ReportID, SpecID);
                        sorted.Add(OtS);
                    }

                    sorted.Sort((value1, value2) => value1.Value.CompareTo(value2.Value));
                    sorted.Reverse();

                    foreach (var currentStruct in sorted)
                    {

                        DataRow dataRow = dataTable.NewRow();

                        dataRow["ID"] = currentStruct.ID.ToString();
                        dataRow["Number"] = currentStruct.Number;
                        dataRow["Name"] = currentStruct.Name;
                        dataRow["StartDate"] = currentStruct.StartDate;
                        dataRow["EndDate"] = currentStruct.EndDate;
                        dataRow["Comment"] = currentStruct.Comment;
                        dataRow["CommentEnabled"] = currentStruct.CommentEnabled;

                        dataRow["CanConfirm"] = currentStruct.CanConfirm;
                        dataRow["ShowLable"] = currentStruct.ShowLable;
                        dataRow["CanWatchWhoOws"] = currentStruct.CanWatchWhoOws;

                        dataRow["LableText"] = currentStruct.LableText;
                        dataRow["LableColor"] = currentStruct.LableColor;

                        dataRow["Value"] = currentStruct.Value.ToString("0.##");
                        
                        //////////////
                        /// 
                        UsersTable director = (from a in kpiWebDataContext.UsersTable
                            where a.FK_FirstLevelSubdivisionTable == currentStruct.ObjStruct.Lv_1
                                  && a.AccessLevel == 4
                                  && a.Active == true
                            select a).FirstOrDefault();

                        if (director!=null)
                        {
                            dataRow["UserID"] = ForRCalc.StructDeepness(currentStruct.ObjStruct).ToString() + "_" +
                                                currentStruct.ID.ToString();

                            if (director.Position != null)
                            {
                                dataRow["UserPosition"] = director.Position;
                            }
                            else
                            {
                                dataRow["UserPosition"] = "Директор структурного подразделения";
                            }
                            /*
                            int userToSendId = FindUserId(ForRCalc.StructDeepness(currentStruct.ObjStruct), currentStruct.ID);
                            UsersTable userToSend = (from a in kpiWebDataContext.UsersTable
                                                     where a.UsersTableID == userToSendId
                                                     select a).FirstOrDefault();
                            if (userToSend.Position != null)
                            {
                                dataRow["UserPosition"] = userToSend.Position;
                            }
                            else
                            {
                                if (ForRCalc.StructDeepness(currentStruct.ObjStruct) == 3)
                                {
                                    dataRow["UserPosition"] = "Представитель структурного подразделения";
                                }
                                else
                                {
                                    dataRow["UserPosition"] = "Ответственный за введенные данные";
                                }
                            }*/
                        }
                        else
                        {
                                                    
                            dataRow["UserPosition"] = "";
                            dataRow["UserID"] = 0;
                        }

                        
                        
                        //////////
                        dataTable.Rows.Add(dataRow);
                    }


                    #endregion

                    #region DataGridBind

                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = title;
                    Grid.DataBind();

                    #endregion

                    #region постнастройка страницы

                    Grid.Columns[12].Visible = false;
                    Grid.Columns[11].Visible = false;
                    Grid.Columns[9].Visible = false;
                    Grid.Columns[8].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[2].Visible = false;
                    Grid.Columns[1].Visible = false;

                   if  (ForRCalc.StructDeepness(mainStruct)>3)
                   {
                       Grid.Columns[14].Visible = false;
                   }
                    if ((ForRCalc.StructDeepness(mainStruct) > (BasicParamLevel - 1)) ||
                        (ForRCalc.StructDeepness(mainStruct) > 2) && (SpecID != 0)) // дальше углубляться нельзя
                    {
                        Grid.Columns[10].Visible = false;
                    }

                    #endregion
                }
                else if (ViewType == 1) // просмотр для показателей (верхние 3 шт)
                {
                    #region преднастройка страницы

                    string name_text = "";
                    string value_text = "";
                    string progress_text = "";
                    string confirm_text = "";
                    string detalize_text = "";

                    if (ParamType == 0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Значения целевых показателей для КФУ");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                        PageFullName.Text = "Значения целевых показателей (ЦП) для КФУ";
                        //PageName.Text = "Значения индикторов для КФУ";
                        name_text = "Название ЦП";
                        value_text = "Значение ЦП";
                        progress_text = "Степень готовности первичных данных";
                        confirm_text = "Утвердить ЦП";
                        detalize_text = "Просмотреть первичные данные для ЦП";
                    }
                    else if (ParamType == 1)
                    {
                        if (ParamID == 0)
                        {
                            RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                                SpecID, "Значения первичных данных (ПД) для КФУ");
                            rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                            Session["rectorHistory"] = rectorHistory;
                            PageFullName.Text = "Значения первичных данных (ПД) для КФУ";
                        }
                        else
                        {
                            string tmp = (from a in kpiWebDataContext.IndicatorsTable
                                where a.IndicatorsTableID == ParamID
                                select a.Name).FirstOrDefault();

                            RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                                SpecID, "Первичные данные для целевого показателя: " + tmp);
                            rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                            Session["rectorHistory"] = rectorHistory;

                            PageFullName.Text = "Первичные данные (ПД) целевого показателя <b> \"" + tmp +
                                                "\"</b>  для КФУ";
                            FormulaLable.Text = (from a in kpiWebDataContext.IndicatorsTable
                                where a.IndicatorsTableID == ParamID
                                select a.Formula).FirstOrDefault();
                            FormulaLable.Visible = true;
                        }
                        name_text = "Названия ПД";
                        value_text = "Значение ПД";
                        progress_text = "Степень готовности базовых показателей";
                        confirm_text = "Утвердить ПД";
                        detalize_text = "Просмотреть базовые показатели для ПД";
                    }
                    else if (ParamType == 2)
                    {
                        string tmp = (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a.Name).FirstOrDefault();
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Базовые показатели для первич: " + tmp);
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;

                        PageFullName.Text = "Базовые показатели первичного показателя <b>  \"" + tmp + "\"</b>  для КФУ";

                        name_text = "Названия БП";
                        value_text = "Значение БП";
                        //progress_text = "Степень готовности базовых показателей";
                        //confirm_text = "Утвердить ПД";
                        //Ыdetalize_text = "Просмотреть базовые показатели для ПД";

                        FormulaLable.Text = (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a.Formula).FirstOrDefault();
                        FormulaLable.Visible = true;
                    }

                    #endregion
                    ///////////
                    #region fill grid
                    if (ParamType == 0) //считаем целевой показатель
                    {
                        #region indicator

                        List<IndicatorsTable> IndicatorsNotUnique = (
                            from a in kpiWebDataContext.IndicatorsTable
                            join b in kpiWebDataContext.IndicatorsAndUsersMapping
                                on a.IndicatorsTableID equals b.FK_IndicatorsTable
                            join c in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                on a.IndicatorsTableID equals c.FK_IndicatorsTable
                            where
                                a.Active == true
                                && b.CanView == true
                                && b.FK_UsresTable == userID
                                && c.FK_ReportArchiveTable == ReportID
                            select a).OrderBy(mc => mc.SortID).ToList();
                        ////для уникальнности
                        int IDForUnique = 0;
                        List<IndicatorsTable> Indicators = new List<IndicatorsTable>();
                        foreach (IndicatorsTable CurrentIndicator in IndicatorsNotUnique)
                        {
                            if (CurrentIndicator.IndicatorsTableID != IDForUnique)
                            {
                                Indicators.Add(CurrentIndicator);
                            }
                            IDForUnique = CurrentIndicator.IndicatorsTableID;
                        }

                        // теперь повторяющихся индикаторов нет
                        // нашли все целевой показатель привязанные к пользователю
                        int calcConfCnt = 0;
                        foreach (IndicatorsTable CurrentIndicator in Indicators)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentIndicator.IndicatorsTableID; //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            if (CurrentIndicator.Measure != null)
                            {
                                if (CurrentIndicator.Measure.Length > 0)
                                {
                                    dataRow["Name"] = CurrentIndicator.Name + " (" + CurrentIndicator.Measure + ")";
                                }
                                else
                                {
                                    dataRow["Name"] = CurrentIndicator.Name;
                                }
                            }
                            else
                            {
                                dataRow["Name"] = CurrentIndicator.Name;
                            }

                            dataRow["CanWatchWhoOws"] = false;
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";

                            ConfirmationHistory CommentRow = (from a in kpiWebDataContext.ConfirmationHistory
                                where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                      && a.FK_ReportTable == ReportID
                                select a).OrderByDescending(mc => mc.Date).FirstOrDefault();



                            if (CommentRow != null)
                            {
                                if (CommentRow.Comment.Length > 0)
                                {
                                    dataRow["Comment"] = "От: " +
                                                         CommonCode.GetUserById(Convert.ToInt32(CommentRow.FK_UsersTable)) +
                                                         "</br>" + CommentRow.Comment;
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


                            PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                                where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                      && a.Date > DateTime.Now
                                //Q1
                                select a).OrderBy(x => x.Date).FirstOrDefault();

                            if (plannedValue != null)
                            {
                                dataRow["PlannedValue"] = plannedValue.Value;
                            }
                            else
                            {
                                dataRow["PlannedValue"] = "Не определено";
                            }

                            #region user can confirm

                            bool canConfirm = (bool) (from a in kpiWebDataContext.IndicatorsAndUsersMapping
                                where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                      && a.FK_UsresTable == userID
                                select a.CanConfirm).FirstOrDefault();

                            #endregion

                            # region are calculated confirmed 

                            List<CalculatedParametrs> CalculatedList =
                                Abbreviature.GetCalculatedList(CurrentIndicator.Formula);

                            bool CalcAreConfirmed = true;

                            int AllCalculated = 0;
                            int AllConfirmedCalculated = 0;
                            foreach (CalculatedParametrs CurrentCalculated in CalculatedList)
                            {
                                AllCalculated++;
                                CollectedCalculatedParametrs tmp_ =
                                    (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                        where
                                            a.FK_CalculatedParametrs == CurrentCalculated.CalculatedParametrsID
                                            && a.FK_ReportArchiveTable == ReportID
                                        select a).FirstOrDefault();
                                if (tmp_ == null)
                                {
                                    CalcAreConfirmed = false;
                                }
                                else
                                {
                                    if (tmp_.Confirmed == null)
                                    {
                                        CalcAreConfirmed = false;
                                    }
                                    else if (tmp_.Confirmed == false)
                                    {
                                        CalcAreConfirmed = false;
                                    }
                                    else
                                    {
                                        AllConfirmedCalculated++;
                                    }
                                }

                                if ((ReportID == 1) || (ReportID == 3)) // удалишь потом //CONNECTREPORTTMP
                                {
                                    int SecReport = ReportID==1?3:1;
                                    ReportArchiveAndIndicatorsMappingTable reparch = (from a in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                                                                      where a.FK_IndicatorsTable == CurrentIndicator.IndicatorsTableID
                                                                                      && a.FK_ReportArchiveTable == SecReport
                                                                                      && a.Active == true
                                                                                      select a).FirstOrDefault();
                                    if (reparch != null)
                                    {
                                        AllCalculated++;
                                        CollectedCalculatedParametrs tmp_2 =
                                        (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                         where
                                             a.FK_CalculatedParametrs == CurrentCalculated.CalculatedParametrsID
                                             && a.FK_ReportArchiveTable == SecReport
                                         select a).FirstOrDefault();
                                        if (tmp_2 == null)
                                            {
                                                CalcAreConfirmed = false;
                                            }
                                            else
                                            {
                                                if (tmp_2.Confirmed == null)
                                                {
                                                    CalcAreConfirmed = false;
                                                }
                                                else if (tmp_2.Confirmed == false)
                                                {
                                                    CalcAreConfirmed = false;
                                                }
                                                else
                                                {
                                                    AllConfirmedCalculated++;
                                                }
                                            }
                                    }

                                }
                            }
                            dataRow["Progress"] = AllConfirmedCalculated.ToString() + " из " + AllCalculated.ToString();

                            #endregion

                            #region get calculated if confirmed; calculate if not confirmed

                            string value_ = "";

                            CollectedIndocators collected = (from a in kpiWebDataContext.CollectedIndocators
                                where a.FK_ReportArchiveTable == ReportID
                                      && a.FK_Indicators == CurrentIndicator.IndicatorsTableID
                                select a).FirstOrDefault();

                            if (collected == null)
                            {
                                collected = new CollectedIndocators();
                                collected.FK_Indicators = CurrentIndicator.IndicatorsTableID;
                                collected.FK_ReportArchiveTable = ReportID;
                                collected.FK_UsersTable = userID;
                                collected.Confirmed = false;
                                collected.LastChangeDateTime = DateTime.Now;
                                collected.Active = true;
                                collected.CollectedValue = 0;
                                kpiWebDataContext.CollectedIndocators.InsertOnSubmit(collected);
                                kpiWebDataContext.SubmitChanges();
                            }

                            if (collected.Confirmed == true) // данные подтверждены
                            {
                                calcConfCnt++;
                                dataRow["CanConfirm"] = false;
                                dataRow["ShowLable"] = true;
                                dataRow["LableText"] = "Утверждено";
                                dataRow["Color"] = "1"; // confirmed
                                value_ = ((float) collected.CollectedValue).ToString("0.##");
                            }
                            else // данные уже есть но еще не подтверждены
                            {
                                if (canConfirm == false)
                                {
                                    dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = " "; // нет права утверждать
                                    dataRow["LableColor"] = "#101010";
                                    value_ = "Недостаточно данных";
                                    if (ShowUnconfirmed)
                                    {
                                        dataRow["Color"] = "2";
                                        /// 0 - no color  // can't confirm
                                        /// 1 - green (confirmed)
                                        /// 2 - red (unconfirmed but calculated)
                                        /// 3 - orange (can confirm)
                                        /// 
                                        double tmp =
                                            ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct,
                                                ParamType,
                                                CurrentIndicator.IndicatorsTableID, ReportID, SpecID));

                                        if (tmp == (float) 1E+20)
                                        {
                                            value_ = "Рассчет невозможен";
                                        }
                                        else
                                        {
                                            value_ = tmp.ToString("0.##");
                                        }
                                    }
                                }
                                else if (!CalcAreConfirmed)
                                {
                                    /*dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = "Не все расчетные утверждены";
                                    */
                                    calcConfCnt++;
                                    dataRow["CanConfirm"] = true;
                                    dataRow["ShowLable"] = false;
                                    dataRow["LableText"] = "";

                                    dataRow["LableColor"] = "#101010";
                                    value_ = "Недостаточно данных";
                                    if (ShowUnconfirmed)
                                    {
                                        dataRow["Color"] = "2";
                                        double tmp =
                                            ForRCalc.CalculatedForDB(
                                                (float) ForRCalc.GetCalculatedWithParams(mainStruct, ParamType,
                                                    CurrentIndicator.IndicatorsTableID, ReportID, SpecID));
                                        if (tmp == (float) 1E+20)
                                        {
                                            value_ = "Рассчет невозможен";
                                        }
                                        else
                                        {
                                            value_ = tmp.ToString("0.##");
                                        }
                                    }
                                }
                                else
                                {
                                    calcConfCnt++;
                                    dataRow["CanConfirm"] = true;
                                    dataRow["ShowLable"] = false;
                                    dataRow["LableText"] = "";
                                    dataRow["LableColor"] = "#FFFFFF";
                                    dataRow["Color"] = "3";
                                    collected.CollectedValue =
                                        ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, ParamType,
                                            CurrentIndicator.IndicatorsTableID, ReportID, SpecID));
                                    //12;                               
                                    kpiWebDataContext.SubmitChanges();
                                    value_ = ((float) collected.CollectedValue).ToString("0.##");
                                }
                            }
                            dataRow["UserID"] = 0;
                            dataRow["UserPosition"] = "";
                            dataRow["Value"] = value_;

                            #endregion

                            dataTable.Rows.Add(dataRow);
                        }

                        if (calcConfCnt == Indicators.Count)
                        {
                            //   Button7.Enabled = false;
                        }

                        #endregion indicator
                    }
                    if (ParamType == 1) //показываем рассчетный входящий в ID целевой показатель
                    {
                        #region calculated

                        //ID  - это айди Индиктора
                        List<CalculatedParametrs> CalculatedList;
                        if (ParamID != 0)
                        {
                            IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                where a.IndicatorsTableID == ParamID
                                select a).FirstOrDefault();
                            CalculatedList = Abbreviature.GetCalculatedList(Indicator.Formula);
                        }
                        else
                        {
                            CalculatedList = (from a in kpiWebDataContext.CalculatedParametrs
                                join b in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                                    on a.CalculatedParametrsID equals b.FK_CalculatedParametrsTable
                                join c in kpiWebDataContext.ReportArchiveAndCalculatedParametrsMappingTable
                                    on a.CalculatedParametrsID equals c.FK_CalculatedParametrsTable
                                where a.Active == true
                                      && b.CanView == true
                                      && b.FK_UsersTable == userID
                                      && c.FK_ReportArchiveTable == ReportID
                                select a).ToList();
                        }

                        ////для уникальнности
                        CalculatedList = CalculatedList.OrderBy(o => o.CalculatedParametrsID).ToList();
                        // List<CalculatedParametrs> SortedList = CalculatedList.OrderBy(o => o.CalculatedParametrsID).ToList();

                        int IDForUnique = 0;
                        List<CalculatedParametrs> CalculatedListUnique = new List<CalculatedParametrs>();
                        foreach (CalculatedParametrs CurrentCalc in CalculatedList)
                        {
                            if (CurrentCalc.CalculatedParametrsID != IDForUnique)
                            {
                                CalculatedListUnique.Add(CurrentCalc);
                            }
                            IDForUnique = CurrentCalc.CalculatedParametrsID;
                        }
                        // теперь повторяющихся  нет
                        int calcConfCnt = 0;
                        foreach (CalculatedParametrs CurrentCalculated in CalculatedListUnique)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrentCalculated.CalculatedParametrsID;
                            //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";
                            if (CurrentCalculated.Measure != null)
                            {
                                if (CurrentCalculated.Measure.Length > 0)
                                {
                                    dataRow["Name"] = CurrentCalculated.Name + " (" + CurrentCalculated.Measure + ")";
                                }
                                else
                                {
                                    dataRow["Name"] = CurrentCalculated.Name;

                                }
                            }
                            else
                            {
                                dataRow["Name"] = CurrentCalculated.Name;
                            }
                            // 
                            //dataRow["Name"] = CurrentCalculated.Name + " (" + CurrentCalculated.Measure + ")";
                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";

                            ConfirmationHistory CommentRow = (from a in kpiWebDataContext.ConfirmationHistory
                                where a.FK_CalculatedParamTable == CurrentCalculated.CalculatedParametrsID
                                      && a.FK_ReportTable == ReportID
                                select a).OrderByDescending(mc => mc.Date).FirstOrDefault();
                            if (CommentRow != null)
                            {
                                if (CommentRow.Comment.Length > 0)
                                {
                                    dataRow["Comment"] = CommentRow.Comment;
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

                            //   dataRow["CanWatchWhoOws"] = "false";
                            //   dataRow["CanConfirm"] = "true";
                            //   dataRow["ShowLable"] = "false";

                            dataRow["Abb"] = CurrentCalculated.AbbreviationEN;

                            #region get calculated if confirmed; calculate if not confirmed

                            #region user can edit

                            CalculatedParametrsAndUsersMapping Calc =
                                (from a in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                                    where a.FK_CalculatedParametrsTable == CurrentCalculated.CalculatedParametrsID
                                          && a.FK_UsersTable == userID
                                    select a).FirstOrDefault();
                            bool canConfirm;
                            if (Calc != null)
                            {
                                canConfirm = (bool) Calc.CanConfirm;
                            }
                            else
                            {
                                canConfirm = false;
                            }

                            #endregion

                            #region check if all users confirmed basics

                            List<BasicParametersTable> BasicList = Abbreviature.GetBasicList(CurrentCalculated.Formula);
                            int AllBasicsUsersCanEdit = 0;
                            int AllConfirmedBasics = 0;
                            int AllConnectedToReportAndUser = 0;
                            int AllConfirmedBasics2 = 0;

                            foreach (BasicParametersTable Basic in BasicList)
                            {

                                AllBasicsUsersCanEdit += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                    where a.FK_ReportArchiveTable == ReportID
                                          && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                    select a).Count();

                                AllConfirmedBasics += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                    where a.FK_ReportArchiveTable == ReportID
                                          && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                          && a.Status == 4
                                    select a).Count();
                                AllConfirmedBasics2 += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                    where a.FK_ReportArchiveTable == ReportID
                                          && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                          && a.Status == 5
                                    select a).Count();
                            }
                            bool BasicsAreConfirmed = true;
                            bool CanConfirmBas = false;
                            if (AllBasicsUsersCanEdit == AllConfirmedBasics2)
                            {
                                CanConfirmBas = true;
                            }
                            if (AllBasicsUsersCanEdit != AllConfirmedBasics)
                            {
                                BasicsAreConfirmed = false;
                            }

                            #endregion


                            string tmp2 =
                                ((((float) AllConfirmedBasics)*100)/((float) AllBasicsUsersCanEdit)).ToString(
                                    "####################.0") +
                                "%";
                            if (tmp2 == "0,0%")
                            {
                                if (CanConfirmBas)
                                {
                                    dataRow["Progress"] = "100,0%";
                                }
                                else
                                {
                                    dataRow["Progress"] = "0,0%";
                                }
                            }

                            if (float.IsNaN((((float) AllConfirmedBasics)*100)/((float) AllBasicsUsersCanEdit)))
                            {
                                dataRow["Progress"] = "100,0%";
                            }

                            string value_ = "";

                            CollectedCalculatedParametrs collected =
                                (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                    where a.FK_ReportArchiveTable == ReportID
                                          && a.FK_CalculatedParametrs == CurrentCalculated.CalculatedParametrsID
                                    select a).FirstOrDefault();
                            if (collected == null) // данных нет
                            {
                                collected = new CollectedCalculatedParametrs();
                                collected.FK_CalculatedParametrs = CurrentCalculated.CalculatedParametrsID;
                                collected.FK_ReportArchiveTable = ReportID;
                                collected.FK_UsersTable = userID;
                                collected.Confirmed = false;
                                collected.LastChangeDateTime = DateTime.Now;
                                collected.Active = true;
                                collected.CollectedValue = ForRCalc.GetCalculatedWithParams(mainStruct, ParamType,
                                    CurrentCalculated.CalculatedParametrsID, ReportID, SpecID); //11;
                                kpiWebDataContext.CollectedCalculatedParametrs.InsertOnSubmit(collected);
                                kpiWebDataContext.SubmitChanges();
                            }

                            UsersTable ConfirmUser = (from a in kpiWebDataContext.UsersTable
                                join b in kpiWebDataContext.CalculatedParametrsAndUsersMapping
                                    on a.UsersTableID equals b.FK_UsersTable
                                where
                                    b.CanConfirm == true
                                    && a.Active == true
                                    && b.FK_CalculatedParametrsTable == CurrentCalculated.CalculatedParametrsID
                                    && b.Active == true

                                select a).FirstOrDefault();
                            string UserName = "";
                            if (ConfirmUser != null)
                            {
                                if (ConfirmUser.Position != null)
                                {
                                    if (ConfirmUser.Position.Length > 2)
                                    {
                                        UserName = ConfirmUser.Position;
                                    }
                                    else
                                    {
                                        UserName = ConfirmUser.Email;
                                    }
                                }
                                else
                                {
                                    UserName = ConfirmUser.Email;
                                }
                            }
                            else
                            {
                                UserName = "не определено";
                            }
                            if (collected.Confirmed == true) //данные подтверждены
                            {
                                calcConfCnt++;

                                dataRow["CanWatchWhoOws"] = false;
                                dataRow["CanConfirm"] = false;
                                dataRow["ShowLable"] = true;
                                dataRow["LableText"] = "Утверждено ";
                                dataRow["LableColor"] = Color.LawnGreen;
                                dataRow["Color"] = "1";
                                value_ = ((float) collected.CollectedValue).ToString("0.##");
                            }
                            else // данные есть но не подтверждены
                            {
                                if (canConfirm == false) //
                                {
                                    dataRow["CanWatchWhoOws"] = false;
                                    dataRow["CanConfirm"] = false;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = "Ответственный: " + UserName;
                                    value_ = "Недостаточно данных";
                                    if (ShowUnconfirmed)
                                    {
                                        dataRow["Color"] = "2";
                                        double tmp =
                                            ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct,
                                                ParamType,
                                                CurrentCalculated.CalculatedParametrsID, ReportID, SpecID));
                                        if (tmp == (float) 1E+20)
                                        {
                                            value_ = "Рассчет невозможен";
                                        }
                                        else
                                        {
                                            value_ = tmp.ToString("0.##");
                                        }
                                    }
                                }
                                else if (BasicsAreConfirmed == false)
                                {
                                    if (CanConfirmBas == true)
                                    {
                                        calcConfCnt++;
                                        dataRow["CanConfirm"] = true;
                                        dataRow["CanWatchWhoOws"] = false;
                                        dataRow["ShowLable"] = false;
                                        dataRow["LableText"] = "";
                                        dataRow["Color"] = "3";
                                        dataRow["LableColor"] = "#000000";
                                        collected.CollectedValue =
                                            ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct,
                                                ParamType,
                                                CurrentCalculated.CalculatedParametrsID, ReportID, SpecID));
                                        kpiWebDataContext.SubmitChanges();
                                        value_ = ((float) collected.CollectedValue).ToString("0.##");
                                    }
                                    else
                                    {

                                        dataRow["CanWatchWhoOws"] = true;
                                        dataRow["CanConfirm"] = false;
                                        dataRow["ShowLable"] = false;
                                        dataRow["LableText"] = "";
                                        /*
                                        dataRow["CanConfirm"] = false;
                                        dataRow["ShowLable"] = true;
                                        dataRow["LableText"] = "Не все базовые показатели утверждены";*/
                                        dataRow["LableColor"] = Color.LightBlue;
                                        value_ = "Недостаточно данных";
                                        if (ShowUnconfirmed)
                                        {
                                            dataRow["Color"] = "2";
                                            double tmp =
                                                ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct,
                                                    ParamType,
                                                    CurrentCalculated.CalculatedParametrsID, ReportID, SpecID));
                                            if (tmp == (float) 1E+20)
                                            {
                                                value_ = "Рассчет невозможен";
                                            }
                                            else
                                            {
                                                value_ = tmp.ToString("0.##");
                                            }
                                        }
                                    }
                                }

                                else //тута
                                {
                                    calcConfCnt++;
                                    dataRow["CanConfirm"] = false;
                                    dataRow["CanWatchWhoOws"] = true;
                                    dataRow["ShowLable"] = true;
                                    dataRow["LableText"] = "Не утверждено директорами академий";
                                    dataRow["Color"] = "3";
                                    dataRow["LableColor"] = "#000000";
                                    collected.CollectedValue =
                                        ForRCalc.CalculatedForDB(ForRCalc.GetCalculatedWithParams(mainStruct, ParamType,
                                            CurrentCalculated.CalculatedParametrsID, ReportID, SpecID));
                                    kpiWebDataContext.SubmitChanges();
                                    value_ = ((float) collected.CollectedValue).ToString("0.##");
                                }
                            }
                            dataRow["Value"] = value_;
                            dataRow["UserPosition"] = "";
                            dataRow["UserID"] = 0;
                            #endregion

                            dataTable.Rows.Add(dataRow);
                        }
                        if (calcConfCnt == CalculatedListUnique.Count)
                        {
                            Button7.Enabled = false;
                        }

                        #endregion
                    }
                    if (ParamType == 2) //
                    {
                        #region basic

                        //ID - Рассчетного айдишник
                        CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                            where a.CalculatedParametrsID == ParamID
                            select a).FirstOrDefault();
                        List<BasicParametersTable> BasicList = Abbreviature.GetBasicList(Calculated.Formula);

                        ////для уникальнности
                        BasicList = BasicList.OrderBy(mc => mc.BasicParametersTableID).ToList();
                        int IDForUnique = 0;
                        List<BasicParametersTable> BasicListUnique = new List<BasicParametersTable>();
                        // List<CalculatedParametrs> CalculatedListUnique = new List<CalculatedParametrs>();
                        foreach (BasicParametersTable CurrebtBasic in BasicList)
                        {
                            if (CurrebtBasic.BasicParametersTableID != IDForUnique)
                            {
                                BasicListUnique.Add(CurrebtBasic);
                            }
                            IDForUnique = CurrebtBasic.BasicParametersTableID;
                        }
                        // теперь повторяющихся  нет

                        foreach (BasicParametersTable CurrebtBasic in BasicListUnique)
                        {
                            DataRow dataRow = dataTable.NewRow();
                            dataRow["ID"] = CurrebtBasic.BasicParametersTableID; //GetLastID(currentStruct).ToString();
                            dataRow["Number"] = "num";

                            if (CurrebtBasic.Measure != null)
                            {
                                if (CurrebtBasic.Measure.Length > 0)
                                {
                                    dataRow["Name"] = CurrebtBasic.Name + " (" + CurrebtBasic.Measure + ")";
                                }
                                else
                                {
                                    dataRow["Name"] = CurrebtBasic.Name;
                                }
                            }
                            else
                            {
                                dataRow["Name"] = CurrebtBasic.Name;
                            }
                            // dataRow["Name"] = CurrebtBasic.Name + " (" + CurrebtBasic.Measure + ")";

                            dataRow["StartDate"] = "nun";
                            dataRow["EndDate"] = "nun";
                            dataRow["Abb"] = CurrebtBasic.AbbreviationEN;
                            dataRow["CanWatchWhoOws"] = false;
                            dataRow["CanConfirm"] = true;
                            dataRow["ShowLable"] = false;
                            dataRow["LableText"] = "";
                            dataRow["LableColor"] = "#000000";

                            dataRow["Comment"] = "nun";
                            dataRow["CommentEnabled"] = "hidden";


                            double tmpdd = ForRCalc.GetCalculatedWithParams(mainStruct, ParamType,
                                CurrebtBasic.BasicParametersTableID,
                                ReportID, SpecID);
                            dataRow["Value"] = tmpdd.ToString("0.##");
                            dataRow["UserPosition"] = "";
                            dataRow["UserID"] = 0;
                            dataTable.Rows.Add(dataRow);
                        }

                        #endregion
                    }

                    #endregion  //////////////////////

                    #region DataGridBind

                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = name_text;
                    Grid.Columns[6].HeaderText = value_text;
                    Grid.Columns[8].HeaderText = progress_text;
                    Grid.Columns[9].HeaderText = confirm_text;
                    Grid.Columns[11].HeaderText = detalize_text;
                    Grid.DataBind();

                    #endregion

                    #region постнастройки страницы

                    Grid.Columns[14].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[1].Visible = false;

                    if (ParamType == 0)
                    {
                        Grid.Columns[2].Visible = false;
                        Grid.Columns[10].Visible = false; //
                        Grid.Columns[12].Visible = false; //
                        Label12.Text = ".....Требует Вашего утверждения";
                        Button7.Visible = true;
                    }

                    if (ParamType == 1)
                    {
                        Button7.Visible = true;
                        if (ParamID == 0)
                        {
                            Grid.Columns[2].Visible = false;
                        }
                        Grid.Columns[10].Visible = false; //
                        Grid.Columns[12].Visible = false; //
                        Grid.Columns[7].Visible = false;
                    }

                    if (ParamType == 2) // дальше углубляться нельзя
                    {
                        Grid.Columns[7].Visible = false;
                        Grid.Columns[9].Visible = false;
                        Grid.Columns[8].Visible = false;
                        Grid.Columns[11].Visible = false;
                    }

                    #endregion
                }
                else if (ViewType == 2) // просмотр по специальностям
                {
                    #region преднастройка страницы

                    if (ParamType == 0)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Целевой показатель (ЦП) для направления подготовки");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    else if (ParamType == 1)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Первичные данные (ПД) для направления подготовки");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                    }
                    else if (ParamType == 2)
                    {
                        RectorSession tmpses = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID,
                            SpecID, "Базовый показатель (БП) для направления подготовки");
                        rectorHistory.RectorSession[rectorHistory.CurrentSession] = tmpses;
                        Session["rectorHistory"] = rectorHistory;
                        string tmp = (from a in kpiWebDataContext.BasicParametersTable
                            where a.BasicParametersTableID == ParamID
                            select a.Name).FirstOrDefault();
                        PageFullName.Text = "Значения базового показателя (БП) <b>  \"" + tmp +
                                            "\" </b>  по направлениям подготовки для КФУ";
                    }

                    string title = "Направления подготовки";

                    #endregion

                    #region fill grid

                    List<SpecializationTable> SpecTable = (from a in kpiWebDataContext.SpecializationTable
                        join b in kpiWebDataContext.FourthLevelSubdivisionTable
                            on a.SpecializationTableID equals b.FK_Specialization
                        where a.Active == true
                              && b.Active == true
                        select a).OrderBy(mc => mc.SpecializationTableID).ToList();

                    //взяли все специальности которые привязаны к кафедрам
                    int old = 0;
                    List<ObjectToSort> sorted = new List<ObjectToSort>();
                    foreach (SpecializationTable currentSpec in SpecTable)
                    {
                        if (currentSpec.SpecializationTableID != old)
                        {

                            ObjectToSort OtS = new ObjectToSort();

                            OtS.ID = currentSpec.SpecializationTableID; //GetLastID(currentStruct).ToString();
                            OtS.Number = "num";
                            OtS.Name = currentSpec.Name + ": " + (from a in kpiWebDataContext.SpecializationTable
                                where a.SpecializationTableID == currentSpec.SpecializationTableID
                                select a.SpecializationNumber).FirstOrDefault().ToString() + " " +
                                       Action.EncodeToStr((from a in kpiWebDataContext.SpecializationTable
                                           where a.SpecializationTableID == currentSpec.SpecializationTableID
                                           select a.SpecializationNumber).FirstOrDefault().ToString());
                            //currentStruct.Name; // Шифр добавить!!
                            OtS.StartDate = "nun";
                            OtS.EndDate = "nun";
                            OtS.Comment = "nun";
                            OtS.CommentEnabled = "hidden";

                            OtS.CanConfirm = true;
                            OtS.ShowLable = false;
                            OtS.CanWatchWhoOws = false;

                            OtS.LableText = "";
                            OtS.LableColor = "#000000";

                            OtS.Value =
                                ForRCalc.GetCalculatedWithParams(mainStruct, ParamType, ParamID, ReportID,
                                    currentSpec.SpecializationTableID);

                            sorted.Add(OtS);

                        }
                        else
                        {

                        }
                        old = currentSpec.SpecializationTableID;
                    }


                    sorted.Sort((value1, value2) => value1.Value.CompareTo(value2.Value));
                    sorted.Reverse();

                    foreach (var currentSpec in sorted)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = currentSpec.ID.ToString();
                        dataRow["Number"] = currentSpec.Number;
                        dataRow["Name"] = currentSpec.Name;
                        dataRow["StartDate"] = currentSpec.StartDate;
                        dataRow["EndDate"] = currentSpec.EndDate;
                        dataRow["Comment"] = currentSpec.Comment;
                        dataRow["CommentEnabled"] = currentSpec.CommentEnabled;

                        dataRow["CanConfirm"] = currentSpec.CanConfirm;
                        dataRow["ShowLable"] = currentSpec.ShowLable;
                        dataRow["CanWatchWhoOws"] = currentSpec.CanWatchWhoOws;

                        dataRow["LableText"] = currentSpec.LableText;
                        dataRow["LableColor"] = currentSpec.LableColor;

                        dataRow["Value"] = currentSpec.Value.ToString("0.##");
                        dataRow["UserPosition"] = "";
                        dataRow["UserID"] = 0;
                        dataTable.Rows.Add(dataRow);
                    }




                    #endregion

                    #region DataGridBind

                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = title;
                    Grid.DataBind();

                    #endregion

                    #region постнастройка страницы

                    Grid.Columns[14].Visible = false;
                    Grid.Columns[12].Visible = false;
                    Grid.Columns[11].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[9].Visible = false;
                    Grid.Columns[8].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[2].Visible = false;
                    Grid.Columns[1].Visible = false;

                    #endregion
                }
                else if (ViewType == 3) // Должники
                {
                    #region

                    PageFullName.Text = "";
                    PageFullName.Text += "<b>";
                    PageFullName.Text += (from a in kpiWebDataContext.CalculatedParametrs
                        where a.CalculatedParametrsID == ParamID
                        select a.Name).FirstOrDefault();
                    PageFullName.Text += "</b>  </br>";

                    int Deep = ForRCalc.StructDeepness(mainStruct);
                    if (Deep == 1)
                    {
                    }
                    if (Deep == 2)
                    {
                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }
                    if (Deep == 3)
                    {
                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                            where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }
                    if (Deep == 4)
                    {

                        PageFullName.Text += (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                            where a.FirstLevelSubdivisionTableID == mainStruct.Lv_1
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                            where a.SecondLevelSubdivisionTableID == mainStruct.Lv_2
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";

                        PageFullName.Text += (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                            where a.ThirdLevelSubdivisionTableID == mainStruct.Lv_3
                            select a.Name).FirstOrDefault();
                        PageFullName.Text += "</br>";
                    }
                    Label11.Text = "..... Данные готовы к утверждению";
                    Panel5.BackColor = Color.FromArgb(100, 0, 255, 0);
                    Panel5.Visible = false;
                    Label12.Text = "..... Не утверждено директором";
                    Panel6.BackColor = Color.FromArgb(100, 255, 255, 0);
                    Label13.Text = "..... В процессе заполнения";
                    Panel7.BackColor = Color.FromArgb(100, 255, 0, 0);
                    #endregion
                  
                    #region fill grid

                    CalculatedParametrs Calculated = (from a in kpiWebDataContext.CalculatedParametrs
                        where a.CalculatedParametrsID == ParamID
                        select a).FirstOrDefault();

                    List<BasicParametersTable> BasicList = Abbreviature.GetBasicList(Calculated.Formula);
                    List<ForRCalc.Struct> currentStructList = new List<ForRCalc.Struct>();
                    currentStructList = ForRCalc.GetChildStructList(mainStruct, ReportID);

                    foreach (ForRCalc.Struct currentStruct in currentStructList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["ID"] = ForRCalc.GetLastID(currentStruct).ToString();
                        dataRow["Number"] = "num";
                        dataRow["Name"] = currentStruct.Name;
                        dataRow["StartDate"] = "nun";
                        dataRow["EndDate"] = "nun";
                        dataRow["CanConfirm"] = false;
                        dataRow["ShowLable"] = false;
                        dataRow["CanWatchWhoOws"] = false;
                        dataRow["LableText"] = "";
                        dataRow["LableColor"] = "#000000";
                        dataRow["Value"] = "nun";

                        dataRow["Comment"] = "nun";
                        dataRow["CommentEnabled"] = "hidden";




                        #region check if all users confirmed basics

                        int AllBasicsUsersCanEdit = 0;
                        int AllConfirmedBasics = 0;
                        int AllConfirmedBasics2 = 0;
                        foreach (BasicParametersTable Basic in BasicList)
                        {
                            /*
                            List<UsersTable> UserTableList = (from a in kpiWebDataContext.UsersTable
                                join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                on a.UsersTableID equals b.FK_UsersTable
                                join c in kpiWebDataContext.BasicParametrAdditional 
                                on b.FK_ParametrsTable equals c.BasicParametrAdditionalID
                                where a.Active == true
                                      && c.Calculated == false
                                      && b.Active == true
                                      && b.CanEdit == true
                                      && b.FK_ParametrsTable == Basic.BasicParametersTableID

                                      && ((a.FK_ZeroLevelSubdivisionTable == currentStruct.Lv_0) || (currentStruct.Lv_0 == 0))
                                      && ((a.FK_FirstLevelSubdivisionTable == currentStruct.Lv_1) || (currentStruct.Lv_1 == 0))
                                      && ((a.FK_SecondLevelSubdivisionTable == currentStruct.Lv_2) || (currentStruct.Lv_2 == 0))
                                      && ((a.FK_ThirdLevelSubdivisionTable == currentStruct.Lv_3) || (currentStruct.Lv_3 == 0))
                                    //  && ((a.FK_FourthLevelSubdivisionTable == currentStruct.Lv_4) || (currentStruct.Lv_4 == 0))

                                select a).ToList();

                            int SpecCnt = 0;

                            BasicParametrAdditional basicAdditional =
                                (from a in kpiWebDataContext.BasicParametrAdditional
                                    where a.BasicParametrAdditionalID == Basic.BasicParametersTableID
                                    select a).FirstOrDefault();
                            if (basicAdditional.SubvisionLevel != 4)
                            {
                                AllBasicsUsersCanEdit += UserTableList.Count();
                            }
                            else
                            {
                                foreach (UsersTable CurUSer in UserTableList)
                                {
                                    SpecCnt += (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                        where a.FK_ThirdLevelSubdivisionTable == CurUSer.FK_ThirdLevelSubdivisionTable
                                        && a.Active == true
                                        select a).Count();
                                }
                                AllBasicsUsersCanEdit += (UserTableList.Count() * SpecCnt);
                            }
                       */
                            AllBasicsUsersCanEdit += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                where a.FK_ReportArchiveTable == ReportID
                                      && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                      &&
                                      ((a.FK_ZeroLevelSubdivisionTable == currentStruct.Lv_0) ||
                                       (currentStruct.Lv_0 == 0))
                                      &&
                                      ((a.FK_FirstLevelSubdivisionTable == currentStruct.Lv_1) ||
                                       (currentStruct.Lv_1 == 0))
                                      &&
                                      ((a.FK_SecondLevelSubdivisionTable == currentStruct.Lv_2) ||
                                       (currentStruct.Lv_2 == 0))
                                      &&
                                      ((a.FK_ThirdLevelSubdivisionTable == currentStruct.Lv_3) ||
                                       (currentStruct.Lv_3 == 0))
                                      &&
                                      ((a.FK_FourthLevelSubdivisionTable == currentStruct.Lv_4) ||
                                       (currentStruct.Lv_4 == 0))
                                select a).Count();

                            AllConfirmedBasics += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                where a.FK_ReportArchiveTable == ReportID
                                      && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                      && a.Status == 4
                                      &&
                                      ((a.FK_ZeroLevelSubdivisionTable == currentStruct.Lv_0) ||
                                       (currentStruct.Lv_0 == 0))
                                      &&
                                      ((a.FK_FirstLevelSubdivisionTable == currentStruct.Lv_1) ||
                                       (currentStruct.Lv_1 == 0))
                                      &&
                                      ((a.FK_SecondLevelSubdivisionTable == currentStruct.Lv_2) ||
                                       (currentStruct.Lv_2 == 0))
                                      &&
                                      ((a.FK_ThirdLevelSubdivisionTable == currentStruct.Lv_3) ||
                                       (currentStruct.Lv_3 == 0))
                                      &&
                                      ((a.FK_FourthLevelSubdivisionTable == currentStruct.Lv_4) ||
                                       (currentStruct.Lv_4 == 0))
                                select a).Count();

                            AllConfirmedBasics2 += (from a in kpiWebDataContext.CollectedBasicParametersTable
                                where a.FK_ReportArchiveTable == ReportID
                                      && a.FK_BasicParametersTable == Basic.BasicParametersTableID
                                      && a.Status == 5
                                      &&
                                      ((a.FK_ZeroLevelSubdivisionTable == currentStruct.Lv_0) ||
                                       (currentStruct.Lv_0 == 0))
                                      &&
                                      ((a.FK_FirstLevelSubdivisionTable == currentStruct.Lv_1) ||
                                       (currentStruct.Lv_1 == 0))
                                      &&
                                      ((a.FK_SecondLevelSubdivisionTable == currentStruct.Lv_2) ||
                                       (currentStruct.Lv_2 == 0))
                                      &&
                                      ((a.FK_ThirdLevelSubdivisionTable == currentStruct.Lv_3) ||
                                       (currentStruct.Lv_3 == 0))
                                      &&
                                      ((a.FK_FourthLevelSubdivisionTable == currentStruct.Lv_4) ||
                                       (currentStruct.Lv_4 == 0))
                                select a).Count();
                        }
                        bool BasicsAreConfirmed = true;
                        bool BasicsCanConf2 = false;

                        if (AllBasicsUsersCanEdit != AllConfirmedBasics)
                        {
                            BasicsAreConfirmed = false;
                        }

                        if (AllBasicsUsersCanEdit == AllConfirmedBasics2)
                        {
                            BasicsCanConf2 = true;
                        }

                        #endregion

                        if (AllBasicsUsersCanEdit == 0)
                        {
                            dataRow["Progress"] = "";
                        }
                        else
                        {
                            string tmp2 =

                                ((((float) AllConfirmedBasics)*100)/((float) AllBasicsUsersCanEdit)).ToString("0.0") +
                                "%";


                            if (tmp2 == "100,0%")
                            {
                                dataRow["Progress"] = "100,0% (не утверждено директором академии)";
                                dataRow["Color"] = 3;
                            }
                            else if (BasicsCanConf2)
                            {
                                dataRow["Progress"] = "100,0%";
                                dataRow["Color"] = 1;
                            }
                            else

                            {
                                dataRow["Progress"] = tmp2;
                                dataRow["Color"] = 2;
                            }


                            if (float.IsNaN((((float) AllConfirmedBasics)*100)/((float) AllBasicsUsersCanEdit)))
                            {
                                if (BasicsCanConf2 == false)
                                {
                                    dataRow["Progress"] = "100,0% (не утверждено директором академии)";
                                    dataRow["Color"] = 3;
                                }
                                else
                                {
                                    dataRow["Progress"] = "100,0%";
                                    dataRow["Color"] = 1;
                                }

                            }
                            dataRow["UserPosition"] = "";
                            dataRow["UserID"] = 0;
                            dataTable.Rows.Add(dataRow);
                        }


                        // dataTable.Rows.Add(dataRow);
                    }

                    #endregion

                    #region DataGridBind

                    Grid.DataSource = dataTable;
                    Grid.Columns[3].HeaderText = "Подразделения";
                    if (ForRCalc.StructDeepness(mainStruct) > 3)
                    {
                        Grid.Columns[3].HeaderText = "Направления подготовки";
                    }
                    Grid.DataBind();

                    #endregion

                    #region постнастройка страницы
                    Grid.Columns[14].Visible = false;
                    Grid.Columns[12].Visible = false;
                    Grid.Columns[11].Visible = false;
                    Grid.Columns[9].Visible = false;
                    //  Grid.Columns[8].Visible = false;
                    Grid.Columns[7].Visible = false;
                    Grid.Columns[6].Visible = false;
                    Grid.Columns[5].Visible = false;
                    Grid.Columns[4].Visible = false;
                    Grid.Columns[2].Visible = false;
                    Grid.Columns[1].Visible = false;
                    if (ForRCalc.StructDeepness(mainStruct) > 2)
                    {
                        Grid.Columns[10].Visible = false;
                    }

                    #endregion
                }
                else
                {
                    //error // wrong ViewType
                }

                RefreshHistory();

                if (((ViewType == 1) && ((ParamType == 0) || (ParamType == 1))) || (ViewType == 3))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "ShowLegend", "ShowLegend()", true);
                }
            }
        }
        protected void Button8Click(object sender, EventArgs e)
        {
            
        }
        protected void ButtonConfirmClick(object sender, EventArgs e)
        {            
            /*
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                int ParamType = CurrentRectorSession.sesParamType;

                if (ParamType == 0) // indicator
                {
                    CollectedIndocators Indicator = (from a in kpiWebDataContext.CollectedIndocators
                        where a.FK_Indicators == Convert.ToInt32(button.CommandArgument)
                        select a).FirstOrDefault();
                    Indicator.Confirmed = true;
                    kpiWebDataContext.SubmitChanges();
                    Response.Redirect("~/Rector/Result.aspx");
                }
                else if (ParamType == 1) // calculated
                {
                    CollectedCalculatedParametrs Calculated = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                                     where a.FK_CalculatedParametrs == Convert.ToInt32(button.CommandArgument)
                                                     select a).FirstOrDefault();
                    Calculated.Confirmed = true;
                    kpiWebDataContext.SubmitChanges();
                    Response.Redirect("~/Rector/Result.aspx");
                }
            }*/
        }
        protected void Button1Click(object sender, EventArgs e) //по структуре
        {
            Button button = (Button)sender;
            {
                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID,"");

                if (currentRectorSession.sesViewType == 1)
                {//впервые перешли на разложение по структуре сразу после показателя
                    currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 0;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
                }
                else if (currentRectorSession.sesViewType == 2)
                {//впервые перешли на разложение по структуре после выбора специальности
                    currentRectorSession.sesSpecID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 0;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
                }
                else if (currentRectorSession.sesViewType == 3)
                {
                    currentRectorSession.sesStruct = ForRCalc.StructDeeper(currentRectorSession.sesStruct, Convert.ToInt32(button.CommandArgument));  
                }
                else
                {
                    currentRectorSession.sesStruct = ForRCalc.StructDeeper(currentRectorSession.sesStruct, Convert.ToInt32(button.CommandArgument));               
                }       
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button2Click(object sender, EventArgs e) // по составляющим
        {
            Button button = (Button)sender;
            {


                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID,"");
                //RectorSession currentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                currentRectorSession.sesParamType++;       
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button3Click(object sender, EventArgs e) //по специальности
        {
            Button button = (Button)sender;
            {              
                RectorHistorySession rectorHistory = (RectorHistorySession) Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;
                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID,"");

                currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                currentRectorSession.sesViewType = 2;      
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (rectorHistory.CurrentSession == 0)
            {
                Response.Redirect("~/Rector/RectorChooseReport.aspx");
            }

            rectorHistory.CurrentSession--;
            Session["rectorHistory"] = rectorHistory;
            Response.Redirect("~/Rector/Result.aspx");
        }
        protected void GoForwardButton_Click(object sender, EventArgs e)
        {
            RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            if (rectorHistory.CurrentSession < rectorHistory.SessionCount) // есть куда переходить
            {
                rectorHistory.CurrentSession++;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {           
            Response.Redirect("~/Rector/RectorMain.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        private void BindTree(IEnumerable<MyObject> list, TreeNode parentNode)
        {
            var nodes = list.Where(x => parentNode == null ? x.ParentId == 0 : x.ParentId == int.Parse(parentNode.Value));
            foreach (var node in nodes)
            {
                TreeNode newNode = new TreeNode(node.Name, node.Id.ToString());
                
                if (node.Active == 1)
                {
                    newNode.NavigateUrl = node.UrlAddr;
                }
                else
                {
                    newNode.SelectAction = TreeNodeSelectAction.None;
                }
                if (parentNode == null)
                {
                    TreeView1.Nodes.Add(newNode);
                }
                else
                {
                    parentNode.ChildNodes.Add(newNode);
                }
                BindTree(list, newNode);
            }
        }
        public void RefreshHistory()
        {
            /*
            #region history            
            RectorHistorySession rectorHistory_ = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory_ == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (rectorHistory_.Visible == true)
            {
                Button6.Text = "Скрыть историю";
                TreeView1.Visible = true;
                List<MyObject> list = new List<MyObject>();
                for (int i = 0; i < rectorHistory_.SessionCount; i++)
                {
                    RectorSession curSesion = rectorHistory_.RectorSession[i];
                    int tmp = rectorHistory_.CurrentSession == i ? 0:1;
                    list.Add(new MyObject() { Id = i+1, Name = curSesion.sesName, ParentId = i, UrlAddr = "Result?&HLevel="+i,Active=tmp });                       
                }
                BindTree(list, null);
                TreeView1.ExpandAll();
            }
            else
            {
                Button6.Text = "Показать историю";
                TreeView1.Visible = false;
                TreeView1.Nodes.Clear();
                TreeView1.DataBind();
            }
            #endregion
             */
        }
        protected void Button6_Click(object sender, EventArgs e)
        {

            RectorHistorySession rectorHistory_ = (RectorHistorySession)Session["rectorHistory"];
            if (rectorHistory_ == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (rectorHistory_.Visible == true)
            {
                rectorHistory_.Visible = false;
            }
            else
            {
                rectorHistory_.Visible = true;
            }
            Session["rectorHistory"] = rectorHistory_;

            RefreshHistory();
        }
        protected void Grid_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
        protected void Button7_Click(object sender, EventArgs e)
        {
            ShowUnConfirmed unConfirmed =new ShowUnConfirmed(true);
            Session["unConfirmed"] = unConfirmed;
            Response.Redirect("~/Rector/Result.aspx");
        }
        protected void Grid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            #region
            var ColorLable = e.Row.FindControl("Color") as Label;
            var PageConfirmButton = e.Row.FindControl("ConfirmButton") as Button;
            var PageButton2 = e.Row.FindControl("Button2") as Button;
            var ValueLable = e.Row.FindControl("Value") as Label;
            var IDLable = e.Row.FindControl("IDs") as Label;

            //// костыль 0%
            var Button1_ = e.Row.FindControl("Button1") as Button;                       
            var PLable_ = e.Row.FindControl("ProgressLable") as Label;
            if ((Button1_ != null) && (PLable_ != null))
            {
                if (PLable_.Text == "0,0%")
                {
                    Button1_.Enabled = false;
                }
                if (PLable_.Text == "100,0% (не утверждено директором академии)")
                {
                    Button1_.Enabled = false;
                }
                if (PLable_.Text == "100,0%")
                {
                    Button1_.Enabled = false;
                }
            }

            if ((Button1_ != null) &&(ValueLable != null))
            {
                if (ValueLable.Text == "0")
                {
                    Button1_.Enabled = false;
                    var Button2_ = e.Row.FindControl("Button3") as Button;    
                     if ((Button1_ != null)!=null)
                     {
                         Button2_.Enabled = false;
                     }
                }
            }
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
         /*   if (Button1_ != null )
            {
                if (Button1_.Enabled !=false)
                {
                    RectorHistorySession rectorHistory = (RectorHistorySession) Session["rectorHistory"];
                    if (rectorHistory == null)
                    {
                        Response.Redirect("~/Default.aspx");
                    }
                    RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];                        
                    int ReportID = CurrentRectorSession.sesReportID;
                    int tmpjj = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                 where a.FK_BasicParametersTable == Convert.ToInt32(Button1_.CommandArgument)
                                     && a.Active == true && a.FK_ReportArchiveTable == ReportID
                                 select a).Count();
                    if (tmpjj < 2) Button1_.Enabled = false;
                }
            }
            */
            if (IDLable != null)
            {
                if (IDLable.Text.IsInt())
                {
                    
                    BasicParametrAdditional tmp = (from a in kpiWebDataContext.BasicParametrAdditional
                                                where a.BasicParametrAdditionalID == Convert.ToInt32(IDLable.Text)
                                                select a).FirstOrDefault();

                    int BasicParamLevel = 0;
                    if (tmp != null)
                        BasicParamLevel = (int)tmp.SubvisionLevel;

                    var Button2_ = e.Row.FindControl("Button3") as Button;
                    if ((Button1_ != null) && (BasicParamLevel != 4))
                    {
                        Button2_.Enabled = false;
                    }
                }
            }
            //end костыль 0%

            if (ColorLable != null)
            {

                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                

                PageConfirmButton.Enabled = false;
                PageButton2.Enabled = false;
                int ColorNumber = -1;
                if (int.TryParse(ColorLable.Text, out ColorNumber) && ColorNumber > -1)
                {
                    switch (ColorNumber)
                    {
                        case 0:
                        {
                         
                         break;   
                        }
                        case 1: // утверждено
                        {
                            e.Row.Style.Add("background-color", "rgba(0, 255, 0, 0.3)");
                            PageButton2.Enabled = true;

                            break;
                        }
                        case 2: // можно утвердить
                        {
                            e.Row.Style.Add("background-color", "rgba(255, 0, 0, 0.3)");
                            PageButton2.Enabled = true;

                            break;
                        }
                        case 3: // рассчитано на неутвержденных данных
                        {
                            e.Row.Style.Add("background-color", "rgba(255, 255, 0, 0.3)");
                            PageConfirmButton.Enabled = true;
                            PageButton2.Enabled = true;

                            break;
                        }
                        default:
                        {                            
                            break;
                        }
                    }                    
                }

                if ((CurrentRectorSession.sesViewType == 1) || (CurrentRectorSession.sesParamType == 0))
                {
                    PageButton2.Enabled = true;
                }

            }
#endregion
            var ConfirmButton = e.Row.FindControl("ConfirmButton") as Button;
            if (ConfirmButton != null)
            {
                ConfirmButton.OnClientClick = "javascript:return showCommentSection(" + ConfirmButton.CommandArgument+ ");";
            }


            var EmailButton = e.Row.FindControl("Button8") as Button;

            if (EmailButton != null)
            {
                if (EmailButton.CommandArgument == "0")
                {
                    EmailButton.Enabled = false;
                }
                else
                {
                    var LblPosition = e.Row.FindControl("LblPosition") as Label;
                    EmailButton.OnClientClick = "javascript:return showEmailSection('" + EmailButton.CommandArgument +
                                                "','" + LblPosition.Text + "');";
                }
            }

        }
        protected void Button8_Click(object sender, EventArgs e)
        {         

        }
        public void DoConfirm(int ParamId)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

            RectorHistorySession rectorHistory = (RectorHistorySession) Session["rectorHistory"];
            if (rectorHistory == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
            int ReportID = CurrentRectorSession.sesReportID;

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;

            int ParamType = CurrentRectorSession.sesParamType;

            if (ParamType == 0) // indicator
            {
                CollectedIndocators Indicator = (from a in kpiWebDataContext.CollectedIndocators
                                                 where a.FK_Indicators == ParamId
                                                 && a.FK_ReportArchiveTable == ReportID
                                                 select a).FirstOrDefault();
                Indicator.Confirmed = true;
                kpiWebDataContext.SubmitChanges();
                #region save params in DB with comment
                ConfirmationHistory ConfirmParam = new ConfirmationHistory();
                ConfirmParam.Date = DateTime.Now;
                ConfirmParam.FK_IndicatorsTable = ParamId;
                ConfirmParam.FK_ReportTable = CurrentRectorSession.sesReportID;
                ConfirmParam.FK_UsersTable = userID;
                ConfirmParam.Name = "Подтверждение ЦП проректором";
                ConfirmParam.Comment = TextBox1.Text;
                kpiWebDataContext.ConfirmationHistory.InsertOnSubmit(ConfirmParam);
                kpiWebDataContext.SubmitChanges();
                #endregion

                if ((ReportID == 1) || (ReportID == 3)) // удалишь потом //CONNECTREPORTTMP
                {
                    int SecReport = ReportID == 1 ? 3 : 1;
                    ReportArchiveAndIndicatorsMappingTable reparch = (from a in kpiWebDataContext.ReportArchiveAndIndicatorsMappingTable
                                                                      where a.FK_IndicatorsTable == ParamId
                                                                      && a.FK_ReportArchiveTable == SecReport
                                                                      && a.Active == true
                                                                      select a).FirstOrDefault();
                    if (reparch != null)
                    {
                        CollectedIndocators Indicator2 = (from a in kpiWebDataContext.CollectedIndocators
                                                         where a.FK_Indicators == ParamId
                                                         && a.FK_ReportArchiveTable == SecReport
                                                         select a).FirstOrDefault();
                        Indicator2.Confirmed = true;
                        kpiWebDataContext.SubmitChanges();
                        #region save params in DB with comment
                        ConfirmationHistory ConfirmParam2 = new ConfirmationHistory();
                        ConfirmParam2.Date = DateTime.Now;
                        ConfirmParam2.FK_IndicatorsTable = ParamId;
                        ConfirmParam2.FK_ReportTable = SecReport;
                        ConfirmParam2.FK_UsersTable = userID;
                        ConfirmParam2.Name = "Подтверждение ЦП проректором";
                        ConfirmParam2.Comment = TextBox1.Text;
                        kpiWebDataContext.ConfirmationHistory.InsertOnSubmit(ConfirmParam2);
                        kpiWebDataContext.SubmitChanges();
                        #endregion
                    }
                }
                Response.Redirect("~/Rector/Result.aspx");
            }
            else if (ParamType == 1) // calculated
            {
                CollectedCalculatedParametrs Calculated = (from a in kpiWebDataContext.CollectedCalculatedParametrs
                                                           where a.FK_CalculatedParametrs == ParamId
                                                           && a.FK_ReportArchiveTable == ReportID
                                                           select a).FirstOrDefault();
                Calculated.Confirmed = true;
                kpiWebDataContext.SubmitChanges();
                #region save params in DB with comment
                ConfirmationHistory ConfirmParam = new ConfirmationHistory();
                ConfirmParam.Date = DateTime.Now;
                ConfirmParam.FK_CalculatedParamTable = ParamId;
                ConfirmParam.FK_ReportTable = CurrentRectorSession.sesReportID;
                ConfirmParam.FK_UsersTable = userID;
                ConfirmParam.Name = "Подтверждение ПД проректором";
                ConfirmParam.Comment = TextBox1.Text;
                kpiWebDataContext.ConfirmationHistory.InsertOnSubmit(ConfirmParam);
                kpiWebDataContext.SubmitChanges();
                #endregion
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void ButtonOweClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                RectorHistorySession rectorHistory = (RectorHistorySession)Session["rectorHistory"];
                if (rectorHistory == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                RectorSession CurrentRectorSession = rectorHistory.RectorSession[rectorHistory.CurrentSession];
                ForRCalc.Struct mainStruct = CurrentRectorSession.sesStruct;
                int ViewType = CurrentRectorSession.sesViewType;
                int ParamID = CurrentRectorSession.sesParamID;
                int ParamType = CurrentRectorSession.sesParamType;
                int ReportID = CurrentRectorSession.sesReportID;
                int SpecID = CurrentRectorSession.sesSpecID;

                RectorSession currentRectorSession = new RectorSession(mainStruct, ViewType, ParamID, ParamType, ReportID, SpecID, "");

                    currentRectorSession.sesParamID = Convert.ToInt32(button.CommandArgument);
                    currentRectorSession.sesViewType = 3;
                    currentRectorSession.sesStruct.Lv_0 = 1;
                    currentRectorSession.sesStruct.Lv_1 = 0;
                    currentRectorSession.sesStruct.Lv_2 = 0;
                    currentRectorSession.sesStruct.Lv_3 = 0;
                    currentRectorSession.sesStruct.Lv_4 = 0;
                    currentRectorSession.sesStruct.Lv_5 = 0;
           
                rectorHistory.CurrentSession++;
                rectorHistory.SessionCount = rectorHistory.CurrentSession + 1;
                rectorHistory.RectorSession[rectorHistory.CurrentSession] = currentRectorSession;
                Session["rectorHistory"] = rectorHistory;
                Response.Redirect("~/Rector/Result.aspx");
            }
        }
        protected void Button8_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/RectorChooseReport.aspx");
        }       
    }
}