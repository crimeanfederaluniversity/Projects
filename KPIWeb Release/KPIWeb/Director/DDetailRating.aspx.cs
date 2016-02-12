using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using KPIWeb.Rector;
namespace KPIWeb.Director
{
    public partial class DDetailRating : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
              Ramsi newSession = (Ramsi)Session["DirectorSession"];

              int indicatorid = newSession.IndicatorId;
              int idreport = newSession.ReportId;
              KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
              ReportArchiveTable ReportTable = (from a in kpiWebDataContext.ReportArchiveTable
                                                where a.ReportArchiveTableID == idreport
                                                select a).FirstOrDefault();
              ReportTitle.Text = ReportTable.Name + " " + ReportTable.StartDateTime.ToString().Split(' ')[0] + " - " +ReportTable.EndDateTime.ToString().Split(' ')[0];
              PageFullName.Text += "<b>";
              PageFullName.Text += (from a in kpiWebDataContext.IndicatorsTable
                                    where a.IndicatorsTableID == indicatorid
                                    select a.Name).FirstOrDefault().ToString();
              PageFullName.Text += "</b>  </br>";

              DataTable dataTable = new DataTable();
              dataTable.Columns.Add(new DataColumn("ID", typeof(string)));            
              dataTable.Columns.Add(new DataColumn("Abb", typeof(string)));
              dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
              dataTable.Columns.Add(new DataColumn("Value", typeof(string)));

              string tmp = (from a in kpiWebDataContext.IndicatorsTable
                            where a.IndicatorsTableID == indicatorid
                            select a.Name).FirstOrDefault();
             
            List<CalculatedParametrs> CalculatedList;
            //if (indicatorid != 0)
            //{
                IndicatorsTable Indicator = (from a in kpiWebDataContext.IndicatorsTable
                                             where a.IndicatorsTableID == indicatorid
                                             select a).FirstOrDefault();
                CalculatedList = Abbreviature.GetCalculatedList(Indicator.Formula);
                CalculatedList = CalculatedList.OrderBy(o => o.CalculatedParametrsID).ToList();

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

                UsersTable idacad = (from a in kpiWebDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();
                int academy = Convert.ToInt32(idacad.FK_FirstLevelSubdivisionTable);
                ForRCalc collected = new ForRCalc();
                ForRCalc.Struct mainStruct = new ForRCalc.Struct(1, academy, "");
                foreach (CalculatedParametrs CurrentCalculated in CalculatedListUnique)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["Abb"] = CurrentCalculated.AbbreviationEN;
                    dataRow["ID"] = CurrentCalculated.CalculatedParametrsID;
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
                    dataRow["Value"] = ForRCalc.GetCalculatedWithParams(mainStruct, 1, CurrentCalculated.CalculatedParametrsID, idreport, 0, userID);
                    dataTable.Rows.Add(dataRow);
                }

                Grid.DataSource = dataTable;
                Grid.DataBind();

                FormulaLable.Text = (from a in kpiWebDataContext.IndicatorsTable
                                     where a.IndicatorsTableID == indicatorid
                                     select a.Formula).FirstOrDefault();
                FormulaLable.Visible = true;

            
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Director/DRating.aspx");
        }
       
        
        protected void Button22_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Director/DMain.aspx");
        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Rector/ViewDocument.aspx");
        }
    }
}