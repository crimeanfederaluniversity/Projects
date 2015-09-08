using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Head
{
    public partial class HeadWatchResult : System.Web.UI.Page
    {

        public string FloatToStrFormat(float value, float plannedValue, int DataType)
        {
            if (DataType == 1)
            {
                string tmpValue = Math.Ceiling(value).ToString();// value.ToString("0");
                return tmpValue;
            }
            else if (DataType == 2)
            {
                string tmpValue = value.ToString();
                string tmpPlanned = plannedValue.ToString();
                int PlannedNumbersAftepPoint = 2;
                if (tmpPlanned.IndexOf(',') != -1)
                {
                    PlannedNumbersAftepPoint = (tmpPlanned.Length - tmpPlanned.IndexOf(',') + 1);
                }
                int ValuePointIndex = tmpValue.IndexOf(',');
                if (ValuePointIndex != -1)
                {
                    if ((tmpValue.Length - ValuePointIndex - PlannedNumbersAftepPoint) > 0)
                    {
                        tmpValue = tmpValue.Remove(ValuePointIndex + PlannedNumbersAftepPoint, tmpValue.Length - ValuePointIndex - PlannedNumbersAftepPoint);
                    }
                }
                return tmpValue;
            }

            return "0";
        } 

        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
            if (userTable.AccessLevel != 8)
            {
                Response.Redirect("~/Default.aspx");
            }

            if (!Page.IsPostBack)
            {
                
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("ParamID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Response", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Measure", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Planned", typeof(string)));
                for (int k = 0; k <= 40; k++) //создаем кучу полей
                {
                    dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                }

                List<string> columnNames = new List<string>();
                List<bool> ToShow = new List<bool>();


                List<FirstLevelSubdivisionTable> FirstLevelList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                               where a.Active == true
                                                               select a).ToList();
                List<IndicatorsTable> IndicatorsList = (from a in kpiWebDataContext.IndicatorsTable
                                                            where a.Active == true
                                                            select a).OrderBy(mc=>mc.SortID).ToList();
                columnNames.Add("КФУ");
                ToShow.Add(false);
                foreach (FirstLevelSubdivisionTable First in FirstLevelList)
                {
                    columnNames.Add(First.AbbRu);
                    ToShow.Add(false);
                }

                int additionalColumnCount = FirstLevelList.Count;

                foreach (IndicatorsTable currentIndicator in IndicatorsList)
                {
                     DataRow dataRow = dataTable.NewRow();
                     dataRow["ParamID"] = currentIndicator.IndicatorsTableID.ToString();
                     dataRow["Name"] = currentIndicator.Name;
                     dataRow["Response"] = (from a in kpiWebDataContext.UsersTable
                                                where a.Active == true
                                                join   b in kpiWebDataContext.IndicatorsAndUsersMapping
                                                on a.UsersTableID equals b.FK_UsresTable      
                                                where b.Active == true
                                                && b.CanConfirm == true
                                                && b.FK_IndicatorsTable == currentIndicator.IndicatorsTableID
                                                select a.Position).FirstOrDefault();
                     dataRow["Measure"] = currentIndicator.Measure;
                     

                      PlannedIndicator plannedValue = (from a in kpiWebDataContext.PlannedIndicator
                      where a.FK_IndicatorsTable == currentIndicator.IndicatorsTableID
                      && a.Date > DateTime.Now
                      select a).OrderBy(x => x.Date).FirstOrDefault();

                      if (plannedValue != null)
                      {
                        dataRow["Planned"] = plannedValue.Value;
                      }
                      else
                      {
                        dataRow["Planned"] = "Не определено";
                      }

                      Rector.ChartOneValue Value0 = Rector.ForRCalc.GetCalculatedIndicator(1, currentIndicator, null, null);
                      if (Value0 != null)
                      {
                              if (Value0.value != 0)
                              {
                                  dataRow["Value0"] = FloatToStrFormat((float)Value0.value,(float) plannedValue.Value,(int)currentIndicator.DataType);
                                  ToShow[0] = true;
                              }
                              else
                              {
                                  dataRow["Value0"] = 0;
                              }
                      }         

                      int i = 1;
                    foreach (FirstLevelSubdivisionTable FirstLevel in FirstLevelList)
                    {
                        Rector.ChartOneValue Value = Rector.ForRCalc.GetCalculatedIndicator(1, currentIndicator, FirstLevel, null);
                        if (Value!=null)
                        {                          
                                if (Value.value!=0)
                                {
                                    dataRow["Value" + i.ToString()] = FloatToStrFormat((float)Value.value, (float)plannedValue.Value, (int)currentIndicator.DataType);
                                    ToShow[i] = true;
                                }
                                else
                                {
                                    dataRow["Value" + i.ToString()] = 0;
                                }                           
                        }                       
                        i++;
                    }
                    dataTable.Rows.Add(dataRow);
                }
                GridviewCollected.DataSource = dataTable;


                for (int j = 0; j < additionalColumnCount; j++)
                {
                    if (ToShow[j])
                    {
                        GridviewCollected.Columns[j + 5].Visible = true;
                        GridviewCollected.Columns[j + 5].HeaderText = columnNames[j];
                    }
                }


                GridviewCollected.DataBind();
            }

        }
    }
}