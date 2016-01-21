using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace KPIWeb.Decan
{
    public partial class DecViewBasicParams : System.Web.UI.Page
    {
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
            if (!userRights.CanUserSeeThisPage(userID, 8 ,0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }
            int ReportID = Convert.ToInt32(paramSerialization.ReportStr);
            int SecondLevel = (int) userTable.FK_SecondLevelSubdivisionTable;
            int ThirdLevel = paramSerialization.l3;
            

            SecondLevelSubdivisionTable Second = (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                  where a.SecondLevelSubdivisionTableID == SecondLevel
                                                  select a).FirstOrDefault();
             
            Label1.Text = Second.Name;

            ReportArchiveTable Report = (from a in kpiWebDataContext.ReportArchiveTable
                                         where a.ReportArchiveTableID == ReportID
                                         select a).FirstOrDefault();
            Label2.Text = Report.Name;

            //если ThirdLevel равен 0  то покажем по всем кафедрам
            if (!Page.IsPostBack)
            {
                #region

                List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
                //                List<string> basicNames = new List<string>(); // сюда названия параметров для excel
                /////создаем дататейбл
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Comment", typeof(string)));
                for (int k = 0; k <= 40; k++) //создаем кучу полей
                {
                    dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                }
                #endregion
                int additionalColumnCount = 0;
                if (ThirdLevel != 0)
                {
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
                            MainFunctions comment = new MainFunctions();
                            string comment_ = comment.GetCommentForBasicInReport(basicParam.BasicParametersTableID, ReportID);
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
                            /*
                            columnNames.Add("Направление подготовки\r" +
                                            (from a in kpiWebDataContext.SpecializationTable
                                             where a.SpecializationTableID == spec.FK_Specialization
                                             select a.Name).FirstOrDefault().ToString() +" : "+ 
                                             (from a in kpiWebDataContext.SpecializationTable
                                             where a.SpecializationTableID == spec.FK_Specialization
                                             select a.SpecializationNumber).FirstOrDefault().ToString());
                           */
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
                                    dataRow["Value" + i] = collectedBasicTmp.CollectedValue.ToString();
                                }
                                i++;
                            }
                            if (j > 0)
                            {
                                //  basicNames.Add(specBasicParam.Name);
                                dataRow["Name"] = specBasicParam.Name;
                                dataRow["BasicParametersTableID"] = specBasicParam.BasicParametersTableID;
                                MainFunctions comment = new MainFunctions();
                                string comment_ = comment.GetCommentForBasicInReport(specBasicParam.BasicParametersTableID, ReportID);
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
                }
                else
                {
                    List<ThirdLevelSubdivisionTable> OnlyKafedras = (from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                                                     join b in kpiWebDataContext.UsersTable
                                                                         on a.ThirdLevelSubdivisionTableID equals b.FK_ThirdLevelSubdivisionTable
                                                                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                     on b.UsersTableID equals c.FK_UsersTable
                                                                     join d in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                                     on a.ThirdLevelSubdivisionTableID equals d.FK_ThirdLevelSubdivisionTable
                                                                     where
                                                                     d.FK_ReportArchiveTableId == ReportID
                                                                     && d.Active == true
                                                                     && a.Active == true
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

                            MainFunctions comment = new MainFunctions();
                            string comment_ = comment.GetCommentForBasicInReport(currentBasic.BasicParametersTableID, ReportID);
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
                }

                GridviewCollectedBasicParameters.DataSource = dataTable;

                for (int j = 0; j < additionalColumnCount; j++)
                {
                    GridviewCollectedBasicParameters.Columns[j + 3].Visible = true;
                    GridviewCollectedBasicParameters.Columns[j + 3].HeaderText = columnNames[j];
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
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }

        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
        }
    }
}