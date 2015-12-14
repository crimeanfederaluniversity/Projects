using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace KPIWeb.Director
{
    public partial class ViewBasicParams : System.Web.UI.Page
    {
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

        public int col_ = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            ViewState["LocalUserID"] = userID;

            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 7, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int ReportID = Convert.ToInt32(paramSerialization.ReportStr);
            int SecondLevel = paramSerialization.l2;
            int ThirdLevel = paramSerialization.l3;

            SecondLevelSubdivisionTable Second = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                  where a.SecondLevelSubdivisionTableID == SecondLevel
                                                  select a).FirstOrDefault();
            Label1.Text = Second.Name;

            ReportArchiveTable Report = (from a in kpiWebDataContext.ReportArchiveTable
                                         where a.ReportArchiveTableID == ReportID
                                         select a).FirstOrDefault();
            Label2.Text = Report.Name;

            if (!Page.IsPostBack)
            {
                #region

                List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Comment", typeof(string)));
                dataTable.Columns.Add(new DataColumn("text", typeof(string)));
                for (int k = 0; k <= 40; k++) //создаем кучу полей
                {
                    dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                }
                #endregion


                DataTable dataTableToEdit = new DataTable();
                dataTableToEdit.Columns.Add(new DataColumn("ID", typeof(string)));
                dataTableToEdit.Columns.Add(new DataColumn("LevelName", typeof(string)));



                int additionalColumnCount = 0;
                if (ThirdLevel != 0)
                {
                    //тут только деканат



                    #region
                    #region
                    List<BasicParametersTable> KafBasicParams = (from a in kpiWebDataContext.BasicParametersTable
                                                                 where a.Active == true
                                                                 join b in kpiWebDataContext.BasicParametrAdditional
                                                                     on a.BasicParametersTableID equals b.BasicParametrAdditionalID
                                                                 where b.Calculated == false
                                                                 join c in kpiWebDataContext.CollectedBasicParametersTable
                                                                     on a.BasicParametersTableID equals c.FK_BasicParametersTable
                                                                 where c.Active == true
                                                                 && c.CollectedValue != null
                                                                 && c.FK_ThirdLevelSubdivisionTable == ThirdLevel
                                                                 && c.FK_FourthLevelSubdivisionTable == null
                                                                 select a).Distinct().ToList();
                    //узнали показатели кафедры(отчёт,разрешенияПользователя,Уровеньвводяшего,вводящийся показатель)          
                    foreach (BasicParametersTable basicParam in KafBasicParams) //пройдемся по показателям
                    {
                        //если этото параметр и эта кафедра дружат
                        ThirdLevelParametrs thirdParametrs =
                            (from a in kpiWebDataContext.ThirdLevelParametrs
                             where a.ThirdLevelParametrsID == ThirdLevel
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
                            dataRow["BasicParametersTableID"] = basicParam.BasicParametersTableID;
                            dataRow["Name"] = basicParam.Name;
                            string comment_ = (from a in kpiWebDataContext.BasicParametrAdditional
                                               where a.BasicParametrAdditionalID == basicParam.BasicParametersTableID
                                               && a.Active == true
                                               select a.Comment).FirstOrDefault();
                            if (comment_ != null)
                            {
                                if (comment_.Length > 3)
                                {
                                    dataRow["Comment"] = comment_;

                                }
                                else
                                {
                                    dataRow["Comment"] = " ";
                                }
                            }
                            else
                            {
                                dataRow["Comment"] = " ";
                            }

                            //   basicNames.Add(basicParam.Name);
                            CollectedBasicParametersTable collectedBasicTmp =
                                (from a in kpiWebDataContext.CollectedBasicParametersTable
                                 where a.FK_ThirdLevelSubdivisionTable == ThirdLevel
                                       && a.FK_BasicParametersTable == basicParam.BasicParametersTableID
                                       && a.FK_ReportArchiveTable == ReportID
                                 select a).FirstOrDefault();
                            if (collectedBasicTmp != null)
                            {
                                dataRow["Value0"] = collectedBasicTmp.CollectedValue.ToString();
                            }
                            else
                            {
                                dataRow["Value0"] = " ";
                            }
                            dataTable.Rows.Add(dataRow);
                        }
                    }
                    string str0 = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                   where a.ThirdLevelSubdivisionTableID == ThirdLevel
                                   select a.Name).FirstOrDefault();
                    if (str0 == "Деканат")
                    {
                        columnNames.Add("Факультет");
                    }
                    else
                    {
                        columnNames.Add(str0);
                    }

                    #endregion
                    additionalColumnCount = 1;
                    #region

                    if ((from zz in kpiWebDataContext.ThirdLevelParametrs
                         where zz.ThirdLevelParametrsID == ThirdLevel
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
                             where a.FK_ReportArchiveTable == ReportID //для отчёта
                                   && d.SubvisionLevel == 4 // для уровня заполняющего
                                   && d.Calculated == false //только вводимые параметры
                                   && c.FK_UsersTable == userID // связаннаые с пользователем
                                   && a.Active == true
                                   && c.CanView == true
                                   && c.Active == true
                             select b).ToList();
                        //Получили показатели разрешенные пользователю в данном отчёте
                        List<FourthLevelSubdivisionTable> Specialzations =
                            (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                             where a.FK_ThirdLevelSubdivisionTable == ThirdLevel
                                   && a.Active == true
                             select a).ToList();
                        //Получили список специальностей для кафедры под пользователем 
                        foreach (FourthLevelSubdivisionTable spec in Specialzations)
                        {
                            string CurrentColumnName = (from a in kpiWebDataContext.SpecializationTable
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
                                             && a.FK_ReportArchiveTable == ReportID
                                            &&
                                             (a.FK_ThirdLevelSubdivisionTable ==
                                              ThirdLevel)
                                             &&
                                             (a.FK_FourthLevelSubdivisionTable ==
                                              spec.FourthLevelSubdivisionTableID)
                                         select a).FirstOrDefault();
                                    if (collectedBasicTmp == null)
                                    {
                                        continue;
                                    }
                                    dataRow["Value" + i] = collectedBasicTmp.CollectedValue.ToString();
                                }
                                i++;
                            }
                            if (j > 0)
                            {
                                dataRow["Name"] = specBasicParam.Name;
                                dataRow["BasicParametersTableID"] = specBasicParam.BasicParametersTableID;
                                string comment_ = (from a in kpiWebDataContext.BasicParametrAdditional
                                                   where a.BasicParametrAdditionalID == specBasicParam.BasicParametersTableID
                                                   && a.Active == true
                                                   select a.Comment).FirstOrDefault();
                                if (comment_ != null)
                                {
                                    if (comment_.Length > 3)
                                    {
                                        dataRow["Comment"] = comment_;
                                    }
                                    else
                                    {
                                        dataRow["Comment"] = " ";
                                    }
                                }
                                else
                                {
                                    dataRow["Comment"] = " ";
                                }

                                dataTable.Rows.Add(dataRow);
                            }
                            ///////////////////////закинули все в дататейбл
                        }
                        additionalColumnCount += Specialzations.Count;
                    }
                    #endregion
                    #endregion
                    if ((DateTime)Report.EndDateTime < DateTime.Now)
                    {
                        GridView1.Visible = false;
                    }
                    else
                    {
                        ThirdLevelSubdivisionTable ThirdLevelTable = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                      where
                                                                          a.ThirdLevelSubdivisionTableID == ThirdLevel
                                                                      select a).FirstOrDefault();

                        DataRow dataRowEdit = dataTableToEdit.NewRow();
                        dataRowEdit["ID"] = ThirdLevel;
                        dataRowEdit["LevelName"] = ThirdLevelTable.Name;
                        dataTableToEdit.Rows.Add(dataRowEdit);
                        GridView1.DataSource = dataTableToEdit;
                        GridView1.DataBind();
                    }

                }
                else
                {
                    // тут несколько кафедр
                    #region
                    List<ThirdLevelSubdivisionTable> OnlyKafedras = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                     join b in kpiWebDataContext.UsersTable
                                                                         on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                     on b.UsersTableID equals c.FK_UsersTable
                                                                     where
                                                                     a.Active == true
                                                                     && b.Active == true
                                                                     && c.Active == true
                                                                     && c.FK_ParametrsTable == 3828
                                                                     && c.CanView == true
                                                                     && b.FK_SecondLevelSubdivisionTable == SecondLevel
                                                                     select a).Distinct().ToList();
                    if (OnlyKafedras.Count() > 0)
                    {
                        foreach (ThirdLevelSubdivisionTable curThird in OnlyKafedras)
                        {
                            columnNames.Add(curThird.Name);
                            additionalColumnCount++;
                        }

                        List<BasicParametersTable> KafBasicParams = (from a in kpiWebDataContext.BasicParametersTable
                                                                     where a.Active == true
                                                                     join b in kpiWebDataContext.BasicParametrAdditional
                                                                         on a.BasicParametersTableID equals b.BasicParametrAdditionalID
                                                                     where b.Calculated == false
                                                                     join c in kpiWebDataContext.CollectedBasicParametersTable
                                                                         on a.BasicParametersTableID equals c.FK_BasicParametersTable
                                                                     where c.Active == true
                                                                     && c.CollectedValue != null
                                                                     && c.FK_ThirdLevelSubdivisionTable == OnlyKafedras[0].ThirdLevelSubdivisionTableID
                                                                     && c.FK_FourthLevelSubdivisionTable == null
                                                                     select a).Distinct().ToList();
                        foreach (BasicParametersTable currentBasic in KafBasicParams)
                        {



                            DataRow dataRow = dataTable.NewRow();
                            dataRow["Name"] = currentBasic.Name;
                            dataRow["BasicParametersTableID"] = currentBasic.BasicParametersTableID;
                            int i = 0;

                            string comment_ = (from a in kpiWebDataContext.BasicParametrAdditional
                                               where a.BasicParametrAdditionalID == currentBasic.BasicParametersTableID
                                               && a.Active == true
                                               select a.Comment).FirstOrDefault();
                            if (comment_ != null)
                            {
                                if (comment_.Length > 3)
                                {
                                    dataRow["Comment"] = comment_;
                                }
                                else
                                {
                                    dataRow["Comment"] = " ";
                                }
                            }
                            else
                            {
                                dataRow["Comment"] = " ";
                            }

                            foreach (ThirdLevelSubdivisionTable currentThird in OnlyKafedras)
                            {

                                CollectedBasicParametersTable curcollect = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                                                            where a.FK_BasicParametersTable == currentBasic.BasicParametersTableID
                                                                            && a.FK_ThirdLevelSubdivisionTable == currentThird.ThirdLevelSubdivisionTableID
                                                                            && a.FK_ReportArchiveTable == ReportID
                                                                            select a).FirstOrDefault();
                                if (curcollect == null)
                                {
                                    dataRow["Value" + i] = "";
                                }
                                else
                                {
                                    if (curcollect.CollectedValue == null)
                                    {
                                        dataRow["Value" + i] = "";
                                    }
                                    else
                                    {
                                        dataRow["Value" + i] = ((float)curcollect.CollectedValue).ToString("0");
                                    }
                                }



                                i++;

                            }
                            dataTable.Rows.Add(dataRow);
                        }

                    }


                    if ((DateTime)Report.EndDateTime < DateTime.Now)
                    {
                        GridView1.Visible = false;
                    }
                    else
                    {
                        foreach (ThirdLevelSubdivisionTable currentThird in OnlyKafedras)
                        {
                            DataRow dataRowEdit = dataTableToEdit.NewRow();
                            dataRowEdit["ID"] = currentThird.ThirdLevelSubdivisionTableID;
                            dataRowEdit["LevelName"] = currentThird.Name;
                            dataTableToEdit.Rows.Add(dataRowEdit);
                        }
                        GridView1.DataSource = dataTableToEdit;
                        GridView1.DataBind();
                    }

                    #endregion
                }

                GridviewCollectedBasicParameters.DataSource = dataTable;
                ViewState["ValueColumnCnt"] = additionalColumnCount;
                for (int j = 0; j < additionalColumnCount; j++)
                {
                    GridviewCollectedBasicParameters.Columns[j + 4].Visible = true;
                    GridviewCollectedBasicParameters.Columns[j + 4].HeaderText = columnNames[j];
                }
                GridviewCollectedBasicParameters.DataBind();
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
            /*
            for (int i = 0; i <= GridviewCollectedBasicParameters.Rows.Count; i++)
            {
                {
                    var lblMinutes = e.Row.FindControl("Value" + i) as TextBox;
                    if (lblMinutes != null)
                    {
                        if (lblMinutes.Text.Count() > 0)
                        {
                        }
                        else
                        {
                            lblMinutes.Visible = false;
                            if (e.Row.RowType == DataControlRowType.DataRow)
                            {
                                DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
                                d.BackColor = disableColor;
                            }
                        }
                    }
                }
            
            }*/

        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }

        protected void Button23_Click(object sender, EventArgs e)
        {
            Response.ContentType = "Application/pdf";
            Response.TransmitFile(CreatePdf());
            Response.End();
        }

        protected void Button1Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                //го создавать сессии
                Serialization UserSer = (Serialization)Session["UserID"];
                if (UserSer == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
                }
                int userID = UserSer.Id;
                //сессия пользователя жива здорова
                Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
                if (paramSerialization == null)
                {
                    Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
                }
                //Номер отчета уже в сессии            
                Serialization modeSer = new Serialization(4, null, null);
                Session["mode"] = modeSer;
                // Mode для FillingReport  = 4 значит входит директор

                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();

                UsersTable userTable =
                    (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

                ThirdLevelSubdivisionTable thirdLevel = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                         where a.ThirdLevelSubdivisionTableID == Convert.ToInt32(button.CommandArgument)
                                                         select a).FirstOrDefault();
                var login =
                    (from a in kpiWebDataContext.UsersTable
                     where a.UsersTableID == (int)ViewState["LocalUserID"]
                     select a.Email).FirstOrDefault();
                LogHandler.LogWriter.WriteLog(LogCategory.INFO, "0CR0: Пользователь " + login + " зашел на страницу редактирования отчета для подразделения " + thirdLevel.Name +
                    " ID 3rd level =" + thirdLevel.ThirdLevelSubdivisionTableID.ToString() + " с ID отчета = " + paramSerialization.ReportStr);

                bool IsDecanat = (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                                  where a.FK_ThirdLevelSubdivisionTable == thirdLevel.ThirdLevelSubdivisionTableID
                                      && a.Active == true
                                  select a).Distinct().Count() > 0 ? true : false;

                if (IsDecanat)
                {
                    Response.Redirect("~/Reports_/Parametrs.aspx");
                }
                else
                {
                    paramSerialization.l3 = Convert.ToInt32(button.CommandArgument.ToString());
                    Session["ReportArchiveID"] = paramSerialization;
                    Response.Redirect("~/Reports_/FillingTheReport.aspx");
                }
            }
        }
    }
}