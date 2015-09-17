using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Curator
{
    public partial class CuratorTZ : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
             if (!Page.IsPostBack)

            {
                CompetitionDataContext curator = new CompetitionDataContext();

                  List<zActionPRManualTable> comp = (from a in curator.zActionPRManualTables where a.Active == true select a).ToList();              
                foreach (zActionPRManualTable n in comp)
                {
                    ListItem TmpItem = new ListItem();
                    TmpItem.Text = n.ActionPR;
                    TmpItem.Value = n.ID.ToString();
                    CheckBoxList1.Items.Add(TmpItem);
                }

                var sessionParam = Session["CompetitionID"];
                if (sessionParam == null)
                {
                    //error
                    Response.Redirect("ChooseCompetition.aspx");
                }
                else
                {
                    int iD = (int)sessionParam;
                    if (iD > 0)
                    {
                        CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                        zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTables
                            where a.Active == true
                                  && a.ID == iD
                            select a).FirstOrDefault();
                        if (currentCompetition == null)
                        {
                            //error
                            Response.Redirect("CuratorCompetition.aspx");
                        }
                        else
                        {
                            NameTextBox.Text = currentCompetition.Name;
                            DescriptionTextBox.Text = currentCompetition.Number;
                            BudjetTextBox.Text = currentCompetition.Budjet.ToString();
               
                            Calendar1.SelectedDate = Convert.ToDateTime(currentCompetition.StartDate);
                            Calendar2.SelectedDate = Convert.ToDateTime(currentCompetition.EndDate);
                             
                    }
                }
            }
        }
        }
        protected void CreateSaveButtonClick(object sender, EventArgs e)
        {
         /*   CompetitionDataContext competitionDataBase = new CompetitionDataContext();
             var sessionParam = Session["CompetitionID"];
            if (sessionParam == null)
            {
                //error
                Response.Redirect("ChooseCompetition.aspx");
            }
            else
            {
                int iD = (int) sessionParam;
                if (iD > 0)
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTable
                            where a.Active == true
                                  && a.ID == iD
                            select a).FirstOrDefault();
                        if (currentCompetition == null)
                        {
                            //error
                            Response.Redirect("ChooseCompetition.aspx");
                        }
                        else
                        {
                            currentCompetition.Name = NameTextBox.Text;
                            currentCompetition.Number = DescriptionTextBox.Text;
                            currentCompetition.Budjet = Convert.ToDouble(BudjetTextBox.Text);
                            currentCompetition.FK_Curator = Convert.ToInt32(DropDownList1.SelectedIndex);
                            currentCompetition.StartDate = Calendar1.SelectedDate;
                            currentCompetition.EndDate = Calendar2.SelectedDate;
                            if (FileUpload1.HasFile)
                            {
                                try
                                {
                                    String path = Server.MapPath("~/documents/templates");
                                    Directory.CreateDirectory(path + "\\\\" + currentCompetition.ID.ToString());
                                    FileUpload1.PostedFile.SaveAs(path + "\\\\" + currentCompetition.ID.ToString() + "\\\\" + FileUpload1.FileName);
                                    currentCompetition.TemplateDocName = FileUpload1.FileName;
                                    // FileStatusLabel.Text = "Файл загружен!";
                                }
                                catch (Exception ex)
                                {
                                    //  FileStatusLabel.Text = "Не удалось загрузить файл.";
                                }
                            }
                            competitionDataBase.SubmitChanges();
                        }
                    }
                }
                else
                {
                    if ((NameTextBox.Text.Length > 0) && (DescriptionTextBox.Text.Length > 0))
                    {
                        zCompetitionsTable newCompetition = new zCompetitionsTable();
                        newCompetition.Name = NameTextBox.Text;
                        newCompetition.Number = DescriptionTextBox.Text;
                        newCompetition.Budjet = Convert.ToDouble(BudjetTextBox.Text);
                        newCompetition.FK_Curator = Convert.ToInt32(DropDownList1.SelectedIndex);
                        newCompetition.StartDate = Calendar1.SelectedDate;
                        newCompetition.EndDate = Calendar2.SelectedDate;
                        newCompetition.Active = true;
                        newCompetition.OpenForApplications = false;
                        competitionDataBase.zCompetitionsTable.InsertOnSubmit(newCompetition);
                        competitionDataBase.SubmitChanges();
                        if (FileUpload1.HasFile)
                        {
                            try
                            {
                                String path = Server.MapPath("~/documents/templates");
                                Directory.CreateDirectory(path + "\\\\" + newCompetition.ID.ToString());
                                FileUpload1.PostedFile.SaveAs(path + "\\\\" + newCompetition.ID.ToString() + "\\\\" + FileUpload1.FileName);
                                newCompetition.TemplateDocName = FileUpload1.FileName;
                                // FileStatusLabel.Text = "Файл загружен!";
                            }
                            catch (Exception ex)
                            {
                                //  FileStatusLabel.Text = "Не удалось загрузить файл.";
                            }
                        }
                        competitionDataBase.SubmitChanges();
                    }
                }
            }
            Response.Redirect("ChooseCompetition.aspx");*/
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuratorCompetition.aspx");
        }
         
    }
}