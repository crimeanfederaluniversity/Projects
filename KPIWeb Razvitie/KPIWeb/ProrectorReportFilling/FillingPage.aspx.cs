using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.WebPages;
using iTextSharp.text;

namespace KPIWeb.ProrectorReportFilling
{
    public partial class FillingPage : System.Web.UI.Page
    {
        public int col_ = 0;
        public bool reportIsConfirmed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            RangeValidatorFunctions rangeValidatorFunctions = new RangeValidatorFunctions();
            CollectedDataStatusProcess collectedDataStatusProcess = new CollectedDataStatusProcess();
            ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = userSer.Id;
            UsersTable userTable = mainFunctions.GetUserById(userId);
            if (userTable.AccessLevel != 5)
            {
                Response.Redirect("~/Default.aspx");
            }
            ViewState["login"] = userTable.Email;
            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int reportId = Convert.ToInt32(mySession.ReportArchiveID);
            int firstLevelId = Convert.ToInt32((mySession.l1));
            int secondLevelId = Convert.ToInt32(mySession.l2);
            int thirdLevelId = Convert.ToInt32(mySession.l3);
           
            SecondLevelSubdivisionTable secondLevel = mainFunctions.GetSecondLevelById(secondLevelId);
            FirstLevelSubdivisionTable firstLevel = mainFunctions.GetFirstLevelById(firstLevelId);
            ThirdLevelSubdivisionTable thirdLevel = new ThirdLevelSubdivisionTable();
            ReportArchiveTable report = mainFunctions.GetReportById(reportId);
          
            ReportNameLabel.Text = report.Name;
            FirstLevelNameLabel.Text = firstLevel.Name;
            SecondLevelNameLabel.Text = secondLevel.Name;
            bool byAllThirdLevelKafedras = false;
            if (thirdLevelId == 0)
            {
                byAllThirdLevelKafedras = true;
                ThirdLevelNameLabel.Text = "Другое";
            }
            else
            {
                thirdLevel = mainFunctions.GetThirdLevelById(thirdLevelId);
                ThirdLevelNameLabel.Text = thirdLevel.Name;
                if (thirdLevel.Name == "Деканат")
                {
                    ThirdLevelNameLabel.Text = "Данные по контингенту студентов";
                }
            }
            if (!Page.IsPostBack)
            {
                #region DataTableCreate

                List<string> columnNames = new List<string>(); // сюда сохраняем названия колонок
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
                    dataTable.Columns.Add(new DataColumn("RangeValidatorEnabled" + k.ToString(), typeof(bool)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorMinValue" + k.ToString(), typeof(double)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorMaxValue" + k.ToString(), typeof(double)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorType" + k.ToString(), typeof(ValidationDataType)));
                    dataTable.Columns.Add(new DataColumn("RangeValidatorMessage" + k.ToString(), typeof(string)));
                    dataTable.Columns.Add(new DataColumn("TextBoxReadOnly" + k.ToString(), typeof(bool)));
                    
                }
                int additionalColumnCount = 0;
                if (byAllThirdLevelKafedras)
                {
                    #region

                    List<ThirdLevelSubdivisionTable> thirdLevelToShowList =
                        toGetOnlyNeededStructAutoFilter.GetThirdLevelList(reportId, userId, secondLevelId, 1);

                       
                    string status = collectedDataStatusProcess.GetStatusNameForStructListInReportByStructIdListnLevel((from a in thirdLevelToShowList select a.ThirdLevelSubdivisionTableID).ToList(), 3,
                            reportId, userId,false);
                    DataStatusLabel.Text = "Статус данных: " + status;

                    Button6.Enabled = false;
                    if (status == "Данные внесены")
                    {
                        Button6.Enabled = true;
                    }

                    if (status == "Данные отправлены")
                    {
                        Button6.Enabled = false;
                        SaveButton.Enabled = false;
                        FillButton.Enabled = false;
                    }


                    List<BasicParametersTable> basicParametersToFillList = (from a in kpiWebDataContext.BasicParametersTable
                                                                            where a.Active == true

                                                                            join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                                on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                            where b.Active == true
                                                                                  && b.FK_UsersTable == userId
                                                                                  && b.CanEdit == true

                                                                            join c in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                                on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                            where c.Active == true
                                                                                  && c.FK_ReportArchiveTable == reportId

                                                                            join d in kpiWebDataContext.BasicParametrAdditional
                                                                                on a.BasicParametersTableID equals d.BasicParametrAdditionalID
                                                                            where d.Calculated == false
                                                                            && d.SubvisionLevel == 3
                                                                            
                                                                            select a).Distinct().ToList();

                    columnNames = (from a in thirdLevelToShowList
                        select a.Name).ToList();
                    additionalColumnCount = thirdLevelToShowList.Count();
                    foreach (BasicParametersTable currentBasic in basicParametersToFillList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["Name"] = currentBasic.Name;
                        dataRow["CurrentReportArchiveID"] = reportId;
                        dataRow["BasicParametersTableID"] = currentBasic.BasicParametersTableID;
                        dataRow["Comment"] = mainFunctions.GetCommentForBasicInReport(currentBasic.BasicParametersTableID, reportId); ;
                        BasicParametrAdditional basicParametrAdditional =
                            (from a in kpiWebDataContext.BasicParametrAdditional
                                where a.BasicParametrAdditionalID == currentBasic.BasicParametersTableID
                                select a).FirstOrDefault();
                        if (basicParametrAdditional == null)
                        {
                            Response.Redirect("~/Default.aspx");
                        }
                        int dataType = (int) basicParametrAdditional.DataType;
                        int columnId = 0;
                        CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
                        foreach (ThirdLevelSubdivisionTable currentThird in thirdLevelToShowList)
                        {
                            CollectedBasicParametersTable currentCollectedData =
                                collectedDataProcess.GetCollectedBasicParametrByReportBasicLevel(reportId,
                                    currentBasic.BasicParametersTableID, 3, currentThird.ThirdLevelSubdivisionTableID,
                                    true,null,
                                    userId);
                            if (currentCollectedData.Status > 4) reportIsConfirmed = true;
                            dataRow["Value" + columnId] = currentCollectedData.CollectedValue.ToString();
                            dataRow["CollectId" + columnId] =
                                currentCollectedData.CollectedBasicParametersTableID.ToString();
                            dataRow["TextBoxReadOnly" + columnId] = currentCollectedData.Status == 5 ? true : false;
                            dataRow["NotNull" + columnId] = 1.ToString();

                            dataRow["RangeValidatorEnabled" + columnId] =
                                rangeValidatorFunctions.GetValidateEnabledForDataType(dataType);
                            dataRow["RangeValidatorMinValue" + columnId] =
                                rangeValidatorFunctions.GetMinValueForDataType(dataType);
                            dataRow["RangeValidatorMaxValue" + columnId] =
                                rangeValidatorFunctions.GetMaxValueForDataType(dataType);
                            dataRow["RangeValidatorType" + columnId] =
                                rangeValidatorFunctions.GetValidateTypeForDataType(dataType);
                            dataRow["RangeValidatorMessage" + columnId] =
                                rangeValidatorFunctions.GetValidateErrorTextForDataType(dataType);

                            columnId++;
                        }
                        for (int i = thirdLevelToShowList.Count; i < 41; i++)
                        {
                            dataRow["RangeValidatorEnabled" + i] = false;
                            dataRow["RangeValidatorMinValue" + i] = 0;
                            dataRow["RangeValidatorMaxValue" + i] = 0;
                            dataRow["RangeValidatorType" + i] = ValidationDataType.String;
                            dataRow["RangeValidatorMessage" + i] = "Ошибка";
                            dataRow["TextBoxReadOnly" + i] = false;
                            
                        }
                        dataTable.Rows.Add(dataRow);
                    }

                    #endregion
                }
                else
                {
                    #region
                    #region надо будет оптимизировать
                    /*
                    List<BasicParametersTable> basicParametersToFillList = (from a in kpiWebDataContext.BasicParametersTable
                                                                            where a.Active == true
                                                                            join b in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                                on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                            where b.Active == true
                                                                                  && b.FK_UsersTable == userId
                                                                                  && b.CanEdit == true
                                                                            join c in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                                                                                on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                            where c.Active == true
                                                                                  && c.FK_ReportArchiveTable == reportId
                                                                            join d in kpiWebDataContext.BasicParametrAdditional
                                                                                on a.BasicParametersTableID equals d.BasicParametrAdditionalID
                                                                            where d.Calculated == false
                                                                            select a).Distinct().ToList();
                    additionalColumnCount = 1;
                    foreach (BasicParametersTable currentBasic in basicParametersToFillList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["Name"] = currentBasic.Name;
                        dataRow["CurrentReportArchiveID"] = reportId;
                        dataRow["BasicParametersTableID"] = currentBasic.BasicParametersTableID;
                        dataRow["Comment"] = mainFunctions.GetCommentForBasicInReport(currentBasic.BasicParametersTableID, reportId); ;
                        BasicParametrAdditional basicParametrAdditional =
                            (from a in kpiWebDataContext.BasicParametrAdditional
                             where a.BasicParametrAdditionalID == currentBasic.BasicParametersTableID
                             select a).FirstOrDefault();
                        if (basicParametrAdditional == null)
                        {
                            Response.Redirect("~/Default.aspx");
                        }
                        int dataType = (int)basicParametrAdditional.DataType;
                        int columnId = 0;

                            CollectedBasicParametersTable currentCollectedData =
                                mainFunctions.GetCollectedBasicParametrByReportBasicLevel(reportId,
                                    currentBasic.BasicParametersTableID, 3, thirdLevelId,
                                    true, null,
                                    userId);
                            dataRow["Value" + columnId] = currentCollectedData.CollectedValue.ToString();
                            dataRow["CollectId" + columnId] =
                                currentCollectedData.CollectedBasicParametersTableID.ToString();
                            dataRow["NotNull" + columnId] = 1.ToString();

                            dataRow["RangeValidatorEnabled" + columnId] =
                                rangeValidatorFunctions.GetValidateEnabledForDataType(dataType);
                            dataRow["RangeValidatorMinValue" + columnId] =
                                rangeValidatorFunctions.GetMinValueForDataType(dataType);
                            dataRow["RangeValidatorMaxValue" + columnId] =
                                rangeValidatorFunctions.GetMaxValueForDataType(dataType);
                            dataRow["RangeValidatorType" + columnId] =
                                rangeValidatorFunctions.GetValidateTypeForDataType(dataType);
                            dataRow["RangeValidatorMessage" + columnId] =
                                rangeValidatorFunctions.GetValidateErrorTextForDataType(dataType);

                            columnId++;
                        
                        for (int i = 1; i < 41; i++)
                        {
                            dataRow["RangeValidatorEnabled" + i] = false;
                            dataRow["RangeValidatorMinValue" + i] = 0;
                            dataRow["RangeValidatorMaxValue" + i] = 0;
                            dataRow["RangeValidatorType" + i] = ValidationDataType.String;
                            dataRow["RangeValidatorMessage" + i] = "Ошибка";
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                    */
                    #endregion

                    string status = collectedDataStatusProcess.GetStatusNameForStructInReportByStructIdNLevel(thirdLevelId, 3,
                            reportId, userId,false);
                    DataStatusLabel.Text = "Статус данных: " + status;

                    Button6.Enabled = false;
                    if (status == "Данные внесены")
                    {
                        Button6.Enabled = true;                     
                    }

                    if (status == "Данные отправлены")
                    {
                        Button6.Enabled = false;
                        SaveButton.Enabled = false;
                        FillButton.Enabled = false;
                    }

                    if ((from a in kpiWebDataContext.ThirdLevelParametrs
                     where a.ThirdLevelParametrsID == thirdLevelId
                     select a.CanGraduate).FirstOrDefault() == true)
                // кафедра выпускающая значит специальности есть
                {
                    List<BasicParametersTable> basicParamsForSpec =
                        (from a in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable
                         join b in kpiWebDataContext.BasicParametersTable
                             on a.FK_BasicParametrsTable equals b.BasicParametersTableID
                         join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                             on b.BasicParametersTableID equals c.FK_ParametrsTable
                         join d in kpiWebDataContext.BasicParametrAdditional
                             on b.BasicParametersTableID equals d.BasicParametrAdditionalID
                         where a.FK_ReportArchiveTable == reportId //для отчёта
                               && d.SubvisionLevel == 4 // для уровня заполняющего
                               && d.Calculated == false //только вводимые параметры
                               && c.FK_UsersTable == userTable.UsersTableID // связаннаые с пользователем
                               && a.Active
                               && c.CanEdit
                               && c.Active
                         select b).ToList();
                    //Получили показатели разрешенные пользователю в данном отчёте
                    List<FourthLevelSubdivisionTable> specialzations =
                        (from a in kpiWebDataContext.FourthLevelSubdivisionTable
                         where a.FK_ThirdLevelSubdivisionTable == thirdLevelId
                               && a.Active == true
                         select a).ToList();
                    //Получили список специальностей для кафедры под пользователем 

                    foreach (FourthLevelSubdivisionTable spec in specialzations)
                    {
                        string currentColumnName = (from a in kpiWebDataContext.SpecializationTable
                                                    where a.SpecializationTableID == spec.FK_Specialization
                                                    select a.SpecializationNumber).FirstOrDefault().ToString();

                        columnNames.Add(currentColumnName);
                        //запомнили название специальности // оно нам пригодится)
                    }
                    
                    foreach (BasicParametersTable currentBasicParam in basicParamsForSpec)
                    {
                        int i = additionalColumnCount;
                        DataRow dataRow = dataTable.NewRow();

                        for (int jj = 0; jj < 41; jj++)
                        {
                            dataRow["RangeValidatorEnabled" + jj] = false;
                            dataRow["RangeValidatorMinValue" + jj] = 0;
                            dataRow["RangeValidatorMaxValue" + jj] = 0;
                            dataRow["RangeValidatorType" + jj] = ValidationDataType.String;
                            dataRow["RangeValidatorMessage" + jj] = "Error";
                            dataRow["TextBoxReadOnly" + jj] = false;
                           // dataRow["NotNull" + i] =dataRow["TextBoxReadOnly" + columnId] = currentCollectedData.Status == 5 ? true : false; 0.ToString();
                        }

                        BasicParametrAdditional basicParametrs =
                            (from a in kpiWebDataContext.BasicParametrAdditional
                             where a.BasicParametrAdditionalID == currentBasicParam.BasicParametersTableID
                             select a).FirstOrDefault();
                        int dataType = (int)basicParametrs.DataType;
                        //узнали параметры базового показателя
                        int j = 0;
                        CollectedDataProcess  collectedDataProcess = new CollectedDataProcess();
                        //если хоть одной специальности базовый показатель нужен то мы его выведем
                        foreach (FourthLevelSubdivisionTable currentSpecialization in specialzations)
                        {


                            FourthLevelParametrs fourthParametrs =
                                (from a in kpiWebDataContext.FourthLevelParametrs
                                    where
                                        a.FourthLevelParametrsID == currentSpecialization.FourthLevelSubdivisionTableID
                                        && a.Active == true 
                
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
                                CollectedBasicParametersTable collectedBasicTmp =  collectedDataProcess.GetCollectedBasicParametrByReportBasicLevel(reportId,
                                    currentBasicParam.BasicParametersTableID, 4,
                                    currentSpecialization.FourthLevelSubdivisionTableID, true,null, userId);
                                if (collectedBasicTmp.Status > 4) reportIsConfirmed = true;                                  
                                dataRow["Value" + i] = collectedBasicTmp.CollectedValue.ToString();
                                dataRow["CollectId" + i] =
                                    collectedBasicTmp.CollectedBasicParametersTableID.ToString();
                                dataRow["TextBoxReadOnly" + i] = collectedBasicTmp.Status == 5 ? true : false;
                                dataRow["NotNull" + i] = 1.ToString();
                                dataRow["RangeValidatorEnabled" + i] =
                                rangeValidatorFunctions.GetValidateEnabledForDataType(dataType);
                                dataRow["RangeValidatorMinValue" + i] =
                                    rangeValidatorFunctions.GetMinValueForDataType(dataType);
                                dataRow["RangeValidatorMaxValue" + i] =
                                    rangeValidatorFunctions.GetMaxValueForDataType(dataType);
                                dataRow["RangeValidatorType" + i] =
                                    rangeValidatorFunctions.GetValidateTypeForDataType(dataType);
                                dataRow["RangeValidatorMessage" + i] =
                                    rangeValidatorFunctions.GetValidateErrorTextForDataType(dataType);
                            }
                            i++;
                        }
                        if (j > 0)
                        {
                            dataRow["Name"] = currentBasicParam.Name;
                            dataRow["CurrentReportArchiveID"] = reportId;
                            dataRow["BasicParametersTableID"] = currentBasicParam.BasicParametersTableID;
                            dataRow["Comment"] = mainFunctions.GetCommentForBasicInReport(currentBasicParam.BasicParametersTableID,reportId);
                            dataRow["CommentEnabled"] = "visible";
                            dataTable.Rows.Add(dataRow);
                        }
                        ///////////////////////закинули все в дататейбл
                    }
                    additionalColumnCount += specialzations.Count;
                }
                    
                #endregion                   
                }
                #endregion
                if (reportIsConfirmed) SaveButton.Enabled = false;
                GridviewCollectedBasicParameters.DataSource = dataTable;
                ViewState["ColumnCount"] = additionalColumnCount;
                for (int j = 0; j < additionalColumnCount; j++)
                {
                    GridviewCollectedBasicParameters.Columns[j + 4].Visible = true;
                    GridviewCollectedBasicParameters.Columns[j + 4].HeaderText = columnNames[j];
                }
                GridviewCollectedBasicParameters.DataBind();
                if (GridviewCollectedBasicParameters.Rows.Count > 0)
                {

                }
                else
                {
                    Response.Redirect("ChooseStruct.aspx");
                }

            }
        }
        public void SaveData()
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            int columnCount = (int)ViewState["ColumnCount"];
            if (GridviewCollectedBasicParameters.Rows.Count > 0)
            {
                int rowIndex = 0;
                for (int i = 1; i <= GridviewCollectedBasicParameters.Rows.Count; i++) //в каждой строчке
                {
                    for (int k = 0; k < columnCount; k++) // пройдемся по каждой колонке
                    {
                        TextBox textBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("Value" + k.ToString());
                        Label label = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("CollectId" + k.ToString());
                        if (textBox != null && label != null)
                        {
                            double collectedValue = double.NaN;
                            if (textBox.Text.IsFloat())
                            {
                                collectedValue = Convert.ToDouble(textBox.Text);
                            }
                            int collectedBasicParametersTableId = -1;
                            if (int.TryParse(label.Text, out collectedBasicParametersTableId) &&
                                collectedBasicParametersTableId > -1)
                            {
                                CollectedBasicParametersTable currentCollected =
                                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                                     where a.Active == true
                                           && a.CollectedBasicParametersTableID == collectedBasicParametersTableId
                                     select a).FirstOrDefault();
                                if (currentCollected != null)
                                {
                                    currentCollected.CollectedValue = mainFunctions.ClearDouble(collectedValue);
                                    kpiWebDataContext.SubmitChanges();
                                }
                            }
                        }
                    }
                    rowIndex++;
                }
            }
            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int reportId = Convert.ToInt32(mySession.ReportArchiveID);
            int secondLevelId = Convert.ToInt32(mySession.l2);
            int thirdLevelId = Convert.ToInt32(mySession.l3);
            Serialization userSer = (Serialization)Session["UserID"];
            if (userSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userId = userSer.Id;

            AutoCalculateAfterSave autoCalculateAfterSave = new AutoCalculateAfterSave();
            ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter =new ToGetOnlyNeededStructAutoFilter();

            if (thirdLevelId == 0)
            {
                List<ThirdLevelSubdivisionTable> thirdLevelToShowList =
                        toGetOnlyNeededStructAutoFilter.GetThirdLevelList(reportId, userId, secondLevelId, 1);
                autoCalculateAfterSave.AutoCalculate(reportId, userId,0,0,thirdLevelToShowList,0);
            }
            else
            {
                autoCalculateAfterSave.AutoCalculate(reportId, userId, thirdLevelId,3,null,0);
            }


            
        }
        public void ConfirmData()
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            int columnCount = (int)ViewState["ColumnCount"];
            if (GridviewCollectedBasicParameters.Rows.Count > 0)
            {
                int rowIndex = 0;
                for (int i = 1; i <= GridviewCollectedBasicParameters.Rows.Count; i++) //в каждой строчке
                {
                    for (int k = 0; k < columnCount; k++) // пройдемся по каждой колонке
                    {
                        TextBox textBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("Value" + k.ToString());
                        Label label = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("CollectId" + k.ToString());
                        if (textBox != null && label != null)
                        {
                            double collectedValue = double.NaN;
                            if (textBox.Text.IsFloat())
                            {
                                collectedValue = Convert.ToDouble(textBox.Text);
                            }
                            int collectedBasicParametersTableId = -1;
                            if (int.TryParse(label.Text, out collectedBasicParametersTableId) &&
                                collectedBasicParametersTableId > -1)
                            {
                                CollectedBasicParametersTable currentCollected =
                                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                                     where a.Active == true
                                           && a.CollectedBasicParametersTableID == collectedBasicParametersTableId
                                     select a).FirstOrDefault();
                                if (currentCollected != null)
                                {
                                    currentCollected.Status = 5;
                                    kpiWebDataContext.SubmitChanges();
                                }
                            }
                        }
                    }
                    rowIndex++;
                }
            }
        }
        public void FillWithZero()
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            MainFunctions mainFunctions = new MainFunctions();
            int columnCount = (int)ViewState["ColumnCount"];
            if (GridviewCollectedBasicParameters.Rows.Count > 0)
            {
                int rowIndex = 0;
                for (int i = 1; i <= GridviewCollectedBasicParameters.Rows.Count; i++) //в каждой строчке
                {
                    for (int k = 0; k < columnCount; k++) // пройдемся по каждой колонке
                    {
                        TextBox textBox = (TextBox)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("Value" + k.ToString());
                        Label label = (Label)GridviewCollectedBasicParameters.Rows[rowIndex].FindControl("CollectId" + k.ToString());
                        if (textBox != null && label != null)
                        {
                            if (!textBox.Text.Any())
                            {
                                textBox.Text = "0";
                            }
                           /* double collectedValue = double.NaN;
                            if (textBox.Text.IsFloat())
                            {
                                collectedValue = Convert.ToDouble(textBox.Text);
                            }
                            int collectedBasicParametersTableId = -1;
                            if (int.TryParse(label.Text, out collectedBasicParametersTableId) &&
                                collectedBasicParametersTableId > -1)
                            {
                                CollectedBasicParametersTable currentCollected =
                                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                                     where a.Active == true
                                           && a.CollectedBasicParametersTableID == collectedBasicParametersTableId
                                     select a).FirstOrDefault();
                                if (currentCollected != null)
                                {
                                    if (currentCollected)
                                    currentCollected.CollectedValue = mainFunctions.ClearDouble(collectedValue);
                                    kpiWebDataContext.SubmitChanges();
                                }
                            }*/
                        }
                    }
                    rowIndex++;
                }
            }
        }
        protected void SaveButton_Click(object sender, EventArgs e)
        {
            SaveData();
            Response.Redirect("FillingPage.aspx");
        }
        protected void GridviewCollectedBasicParameters_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
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
                int columnCount = (int)ViewState["ColumnCount"];
                KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
                for (int i = 1; i <= columnCount; i++)
                {
                    {
                        TextBox lblMinutes = e.Row.FindControl("Value" + rowIndex) as TextBox;
                        Label notNullLbl = e.Row.FindControl("NotNull" + rowIndex) as Label;
                        if (notNullLbl != null)
                        {
                            if (!notNullLbl.Text.Any())
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
//                                DataControlFieldCell d = lblMinutes.Parent as DataControlFieldCell;
//                                d.BackColor = color;
//                                lblMinutes.BackColor = color;                                               
                            }
                        }
                        rowIndex++;
                    }
                }
            }
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            if (!reportIsConfirmed)
                SaveData();


            ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;

            Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
            if (mySession == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int reportID = Convert.ToInt32(mySession.ReportArchiveID);
            int firstLevelId = Convert.ToInt32((mySession.l1));
            int secondLevelId = Convert.ToInt32(mySession.l2);
            List<ThirdLevelSubdivisionTable> noKafedra = toGetOnlyNeededStructAutoFilter.GetThirdLevelList(
                reportID, userID, secondLevelId, 2);

            List<ThirdLevelSubdivisionTable> OnlyKafedras = toGetOnlyNeededStructAutoFilter.GetThirdLevelList(
                reportID, userID, secondLevelId, 1);

            if ((noKafedra.Count == 1) && (OnlyKafedras.Count == 0))
            {
                Response.Redirect("ChooseFaculty.aspx");
            }

            if (noKafedra.Count == 0)
            {
                Response.Redirect("ChooseFaculty.aspx");
            }

            Response.Redirect("~/ProrectorReportFilling/ChooseStruct.aspx");
        }
        protected void FillWithZeroButtonClick(object sender, EventArgs e)
        {
            FillWithZero();
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
             Button button = (Button)sender;
             {
                 KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                 MainFunctions mainFunctions = new MainFunctions();
                 ToGetOnlyNeededStructAutoFilter toGetOnlyNeededStructAutoFilter = new ToGetOnlyNeededStructAutoFilter();
                 Serialization userSer = (Serialization)Session["UserID"];
                 if (userSer == null)
                 {
                     Response.Redirect("~/Default.aspx");
                 }
                 int userId = userSer.Id;
                 UsersTable userTable = mainFunctions.GetUserById(userId);
                 if (userTable.AccessLevel != 5)
                 {
                     Response.Redirect("~/Default.aspx");
                 }
                 ViewState["login"] = userTable.Email;
                 Serialization mySession = (Serialization)Session["ProrectorFillingSession"];
                 if (mySession == null)
                 {
                     Response.Redirect("~/Default.aspx");
                 }
                 int reportId = Convert.ToInt32(mySession.ReportArchiveID);
                 int firstLevelId = Convert.ToInt32((mySession.l1));
                 int secondLevelId = Convert.ToInt32(mySession.l2);
                 int thirdLevelId = Convert.ToInt32(mySession.l3);

                 List<ThirdLevelSubdivisionTable> thirdLevelListToFillWithZero = new List<ThirdLevelSubdivisionTable>();

                 if (thirdLevelId != 0)
                 {
                     thirdLevelListToFillWithZero.Add(mainFunctions.GetThirdLevelById(thirdLevelId));
                 }
                 else
                 {
                     thirdLevelListToFillWithZero =
                        toGetOnlyNeededStructAutoFilter.GetThirdLevelList(reportId, userId, secondLevelId, 1);
                 }

                 CollectedDataProcess collectedDataProcess = new CollectedDataProcess();
               
                 foreach (ThirdLevelSubdivisionTable currentThirdLevel in thirdLevelListToFillWithZero)
                 {
                     ThirdLevelParametrs currentThirdParam = (from a in kPiDataContext.ThirdLevelParametrs
                                                              where a.ThirdLevelParametrsID == currentThirdLevel.ThirdLevelSubdivisionTableID
                                                              select a).FirstOrDefault();
                     List<BasicParametersTable> basicsForThirdInReportForUser =
                         (from a in kPiDataContext.BasicParametersTable
                          where a.Active == true

                          join b in kPiDataContext.BasicParametrsAndSubdivisionClassMappingTable
                              on a.BasicParametersTableID equals b.FK_BasicParametrsTable
                          where b.Active == true
                          && b.FK_SubdivisionClassTable == currentThirdParam.FK_SubdivisionClassTable

                          join c in kPiDataContext.ReportArchiveAndBasicParametrsMappingTable
                              on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                          where c.Active == true
                                && c.FK_ReportArchiveTable == reportId

                          join d in kPiDataContext.BasicParametrsAndUsersMapping
                              on a.BasicParametersTableID equals d.FK_ParametrsTable
                          where d.Active == true
                                && d.FK_UsersTable == userId
                                && d.CanEdit == true

                          select a).Distinct().ToList();
                     foreach (BasicParametersTable currentBasic in basicsForThirdInReportForUser)
                     {
                         //CONFIRMATION
                         collectedDataProcess.ConfirmCollectedBasic(reportId,
                             currentBasic.BasicParametersTableID, 3, currentThirdLevel.ThirdLevelSubdivisionTableID);
                     }
                 }
                 ConfirmData();
                 Response.Redirect("FillingPage.aspx");
             }
            Response.Redirect("FillingPage.aspx");
        }
    }
}