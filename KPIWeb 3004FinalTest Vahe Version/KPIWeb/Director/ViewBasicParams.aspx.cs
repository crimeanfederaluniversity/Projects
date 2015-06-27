using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Director
{
    public partial class ViewBasicParams : System.Web.UI.Page
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

            if (userTable.AccessLevel != 4)
            {
                Response.Redirect("~/Default.aspx");
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int ReportID = Convert.ToInt32(paramSerialization.ReportStr);
            int SecondLevel = paramSerialization.l2;
            int ThirdLevel = paramSerialization.l3;
            if (!Page.IsPostBack)
            {
                #region
                List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
                List<string> basicNames = new List<string>(); // сюда названия параметров для excel
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

                #region
               /* List<BasicParametersTable> KafBasicParams =
                    (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                     join b in kpiWebDataContext.BasicParametersTable
                         on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                     join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                         on b.BasicParametersTableID equals c.FK_ParametrsTable
                     join d in kpiWebDataContext.BasicParametrAdditional
                         on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                     join f in kpiWebDataContext.CollectedBasicParametersTable
                     on a.FK_BasicParametrsTable equals f.FK_BasicParametersTable
                     where
                         a.FK_ReportArchiveTable == ReportID //из нужного отчёта
                         && f.FK_ReportArchiveTable == ReportID 
                         && c.FK_UsersTable == userID // свяный с пользователем
                         && d.SubvisionLevel == 3 //нужный уровень заполняющего
                         && a.Active == true // запись в таблице связей показателя и отчёта активна
                         && c.CanView == true
                         && c.Active == true
                         && d.Calculated == false
                     select b).Distinct().ToList();*/
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

                        basicNames.Add(basicParam.Name);
                        CollectedBasicParametersTable collectedBasicTmp =
                            (from a in kpiWebDataContext.CollectedBasicParametersTable
                             where  a.FK_ThirdLevelSubdivisionTable == ThirdLevel
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
                columnNames.Add((from a in kpiWebDataContext.ThirdLevelSubdivisionTable
                                 where a.ThirdLevelSubdivisionTableID == ThirdLevel
                                 select a.Name).FirstOrDefault());

                #endregion
                int additionalColumnCount = 1;
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
                        string CurrentColumnName = "<div style=\"transform:rotate(90deg);\">" + (from a in kpiWebDataContext.SpecializationTable
                                                                                                 where a.SpecializationTableID == spec.FK_Specialization
                                                                                                 select a.SpecializationNumber).FirstOrDefault().ToString() + "</div>";

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
                            basicNames.Add(specBasicParam.Name);
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

                GridviewCollectedBasicParameters.DataSource = dataTable;
                for (int j = 0; j < additionalColumnCount; j++)
                {
                    GridviewCollectedBasicParameters.Columns[j + 3].Visible = true;
                    GridviewCollectedBasicParameters.Columns[j + 3].HeaderText = columnNames[j];
                }
                GridviewCollectedBasicParameters.DataBind();
            }
        }
    }
}