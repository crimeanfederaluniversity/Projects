using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Expert
{
    public partial class EvaluateApplication : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                var sessionParam1 = Session["ApplicationID"];
                 var sessionParam2 = Session["UserID"];
                if ((sessionParam1 == null)||(sessionParam2==null))
                {
                    //error
                    Response.Redirect("~/Default.aspx");
                }
                int applicationId = Convert.ToInt32(sessionParam1);
                int userId = Convert.ToInt32(sessionParam2);
                CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
                List<zExpertPoints> expertPointsList = (from a in CompetitionsDataBase.zExpertPoints
                                                            where a.Active == true          
                                                            && a.ID !=6
                                                            select a).ToList();
                DataTable dataTable = new DataTable();

                dataTable.Columns.Add("ID", typeof(string));
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Text", typeof(string));
                dataTable.Columns.Add("ErrorText", typeof(string));
                dataTable.Columns.Add("MaxValue", typeof(int));
                dataTable.Columns.Add("MinValue", typeof(int));

                foreach (zExpertPoints currentExpertPoint in expertPointsList)
                {
                    zExpertPointsValue currentExpertPointValue = (from a in CompetitionsDataBase.zExpertPointsValue
                        where a.FK_ApplicationTable == applicationId
                              && a.FK_ExpertsTable == userId
                              && a.FK_ExpertPoints == currentExpertPoint.ID
                              && a.Sended == false
                        select a).FirstOrDefault();
                    if (currentExpertPointValue == null)
                    {
                        zExpertPointsValue currentExpertPointValue2 = new zExpertPointsValue();
                        currentExpertPointValue2.FK_ApplicationTable = applicationId;
                        currentExpertPointValue2.FK_ExpertsTable = userId;
                        currentExpertPointValue2.LastChangeDataTime = DateTime.Now;
                        currentExpertPointValue2.FK_ExpertPoints = currentExpertPoint.ID;
                        currentExpertPointValue2.Sended = false;
                        currentExpertPointValue2.Active = true;
                        CompetitionsDataBase.zExpertPointsValue.InsertOnSubmit(currentExpertPointValue2);
                        CompetitionsDataBase.SubmitChanges();               
                    }
                    else
                    {
                                           
                    }
                    DataRow dataRow = dataTable.NewRow();
                    dataRow["ID"] = currentExpertPointValue.ID;
                    dataRow["Name"] = currentExpertPoint.Name;
                    dataRow["ErrorText"] = "Значение должно быть в диапазоне от 0 до "+currentExpertPoint.MaxValue;
                    dataRow["MaxValue"] = currentExpertPoint.MaxValue;
                    dataRow["MinValue"] = currentExpertPoint.MinValue;
                    if (currentExpertPointValue.Value != null)
                    {
                        dataRow["Text"] = currentExpertPointValue.Value;
                    }
                    else
                    {
                        dataRow["Text"] = "";
                    }
                    dataTable.Rows.Add(dataRow);
               
                }
                EvaluateGV.DataSource = dataTable;
                EvaluateGV.DataBind();

                #region doComment
                zExpertPointsValue commentExpertPointValue = (from a in CompetitionsDataBase.zExpertPointsValue
                                                              where a.FK_ApplicationTable == applicationId
                                                                    && a.FK_ExpertsTable == userId
                                                                    && a.FK_ExpertPoints == 6
                                                              select a).FirstOrDefault();
                if (commentExpertPointValue == null)
                {
                    commentExpertPointValue = new zExpertPointsValue();
                    commentExpertPointValue.FK_ApplicationTable = applicationId;
                    commentExpertPointValue.FK_ExpertsTable = userId;
                    commentExpertPointValue.LastChangeDataTime = DateTime.Now;
                    commentExpertPointValue.FK_ExpertPoints = 6;
                    commentExpertPointValue.Sended = false;

                    CompetitionsDataBase.zExpertPointsValue.InsertOnSubmit(commentExpertPointValue);
                    CompetitionsDataBase.SubmitChanges();

                }

                if (commentExpertPointValue.Value != null)
                {
                    CommentTextBox.Text = commentExpertPointValue.Value;
                }
                else
                {
                    CommentTextBox.Text = "";
                }
                ViewState["CommentID"] = commentExpertPointValue.ID;
                #endregion
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var sessionParam1 = Session["ApplicationID"];
            var sessionParam2 = Session["UserID"];
            if ((sessionParam1 == null) || (sessionParam2 == null))
            {
                //error
                Response.Redirect("~/Default.aspx");
            }
            int applicationId = Convert.ToInt32(sessionParam1);
            int userId = Convert.ToInt32(sessionParam2);
            CompetitionDataContext CompetitionsDataBase = new CompetitionDataContext();
            foreach (GridViewRow dataRow in EvaluateGV.Rows)
            {
                Label labelID = (Label) dataRow.FindControl("ID");
                TextBox valueTextBox = (TextBox)dataRow.FindControl("ValueTextBox");
                if (labelID != null)
                {
                    zExpertPointsValue currentExpertPointValue = (from a in CompetitionsDataBase.zExpertPointsValue
                                                                  where a.ID == Convert.ToInt32(labelID.Text)
                                                                  select a).FirstOrDefault();
                    if (currentExpertPointValue != null)
                    {
                        if (valueTextBox.Text.Any())
                        currentExpertPointValue.Value = valueTextBox.Text;
                        currentExpertPointValue.LastChangeDataTime = DateTime.Now;
                        CompetitionsDataBase.SubmitChanges();
                    }
                }
            }
            #region doComment

            var commentIdTemp = ViewState["CommentID"];
            if (commentIdTemp != null)
            {
                int commentId = (int) commentIdTemp;
                zExpertPointsValue commentExpertPointValue = (from a in CompetitionsDataBase.zExpertPointsValue
                                                              where a.ID == commentId
                    select a).FirstOrDefault();
                if (commentExpertPointValue != null)
                {
                    if (CommentTextBox.Text.Any())
                        commentExpertPointValue.Value = CommentTextBox.Text;
                    commentExpertPointValue.LastChangeDataTime = DateTime.Now;
                    CompetitionsDataBase.SubmitChanges();
                }
            }

            #endregion
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApplicationsForExpert.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
                 var sessionParam1 = Session["ApplicationID"];
                 var sessionParam2 = Session["UserID"];
                 if ((sessionParam1 == null)||(sessionParam2==null))
                {
                    //error
                    Response.Redirect("~/Default.aspx");
                }
                int applicationId = Convert.ToInt32(sessionParam1);
                int userId = Convert.ToInt32(sessionParam2);

                CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                List<zExpertPointsValue> currentApplication = (from a in competitionDataBase.zExpertPointsValue where a.Active == true
                                                               && a.FK_ApplicationTable == applicationId && a.FK_ExpertsTable == userId
                                                               select a).ToList();
                 
                    foreach (zExpertPointsValue current in currentApplication)
                    {
                        current.Sended = true;
                        current.SendedDataTime = DateTime.Now;
                        competitionDataBase.SubmitChanges();
                    }
                    Response.Redirect("~/Default.aspx");
            }
        
        
    }
}