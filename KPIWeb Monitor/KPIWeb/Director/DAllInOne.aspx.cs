using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace KPIWeb.Director
{
    public partial class DAllInOne : System.Web.UI.Page
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
            if (!Page.IsPostBack)
            {
                Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
                if (paramSerialization == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
                int ReportID = Convert.ToInt32(paramSerialization.ReportStr);

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("BasicParametersTableID", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTable.Columns.Add(new DataColumn("Comment", typeof(string)));
                for (int k = 0; k <= 81; k++)
                {
                    dataTable.Columns.Add(new DataColumn("Value" + k.ToString(), typeof(string)));
                }
                List<string> columnNames = new List<string>();
                List<bool> ToShow = new List<bool>();



               // int additionalColumnCount = 0;
                {
                    List<SecondLevelSubdivisionTable> SecondLevelList = /*(from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                         where a.Active == true
                                                                         && a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                                         join b in kpiWebDataContext.UsersTable
                                                                         on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                                                         join c in kpiWebDataContext.BasicParametrsAndUsersMapping
                                                                         on b.UsersTableID equals c.FK_UsersTable
                                                                         where
                                                                         b.Active == true
                                                                         && c.Active == true
                                                                         && c.CanView == true
                                                                         select a).Distinct().ToList();*/
                                                                         (from a in kpiWebDataContext.SecondLevelSubdivisionTable
                                                                           where a.FK_FirstLevelSubdivisionTable == userTable.FK_FirstLevelSubdivisionTable
                                                                           join b in kpiWebDataContext.ReportArchiveAndLevelMappingTable
                                                                           on a.SecondLevelSubdivisionTableID equals b.FK_SecondLevelSubdivisionTable
                                                                           where a.Active == true
                                                                           && b.Active == true
                                                                           && b.FK_ReportArchiveTableId == ReportID
                                                                           select a).Distinct().ToList();


                    List<BasicParametersTable> BasicParametrsList = (from a in kpiWebDataContext.BasicParametersTable
                                                                 join b in kpiWebDataContext.BasicParametrAdditional
                                                                     on a.BasicParametersTableID equals b.BasicParametrAdditionalID
                                                                     where b.Calculated == false
                                                                     join c in kpiWebDataContext.ReportArchiveAndBasicParametrsMappingTable 
                                                                     on a.BasicParametersTableID equals c.FK_BasicParametrsTable
                                                                     where c.FK_ReportArchiveTable == ReportID
                                                                     && c.Active == true
                                                                 select a).ToList();
                    int additionalColumnCount = SecondLevelList.Count+1;


                    columnNames.Add("Cуммарное значение");
                    ToShow.Add(false);


                    foreach (SecondLevelSubdivisionTable Second in SecondLevelList)
                    {
                        columnNames.Add(Second.Name);
                        ToShow.Add(false);
                    }

                    foreach (BasicParametersTable CurrentBasic in BasicParametrsList)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["BasicParametersTableID"] = CurrentBasic.BasicParametersTableID;
                        dataRow["Name"] = CurrentBasic.Name;
                        string comment_ = (from a in kpiWebDataContext.BasicParametrAdditional
                                           where a.BasicParametrAdditionalID == CurrentBasic.BasicParametersTableID
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



                        int i = 0;

                        float tmp = (float)CalculateAbb.SumForLevel(CurrentBasic.BasicParametersTableID, ReportID, 0,
                        1, (int)userTable.FK_FirstLevelSubdivisionTable, 0, 0, 0, 0);

                      /*  if ((tmp == 0)&&(CheckBox1.Checked==true))
                            continue;
*/
                        dataRow["Value" + i.ToString()] = tmp;
                        ToShow[i] = true;

                        i++;
                        foreach (SecondLevelSubdivisionTable CurrentSecond in SecondLevelList)
                        {
                            dataRow["Value" + i.ToString()] = CalculateAbb.SumForLevel(CurrentBasic.BasicParametersTableID, ReportID, 0, 
                                1, CurrentSecond.FK_FirstLevelSubdivisionTable, CurrentSecond.SecondLevelSubdivisionTableID, 0, 0, 0);
                            ToShow[i] = true;
                            i++;
                        }
                        dataTable.Rows.Add(dataRow);
                    }

                    GridviewCollectedBasicParameters.DataSource = dataTable;
                    for (int j = 0; j < additionalColumnCount; j++)
                    {
                        if (ToShow[j])
                        {
                            GridviewCollectedBasicParameters.Columns[j + 3].Visible = true;
                            GridviewCollectedBasicParameters.Columns[j + 3].HeaderText = columnNames[j];
                        }
                    }
                    GridviewCollectedBasicParameters.DataBind();
                }
            }

        }
        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
    }
}