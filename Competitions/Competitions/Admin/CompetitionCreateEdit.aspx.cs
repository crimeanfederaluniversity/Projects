using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Admin
{
    public partial class NewCompetition : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)

            {
                CompetitionDataContext curator = new CompetitionDataContext();

                List<UsersTable> curators = (from a in curator.UsersTable
                                       where a.AccessLevel == 15 && a.Active == true
                                       select a).ToList();

                ListItem tmpItem2 = new ListItem();
                tmpItem2.Text = "Выберите куратора";
                tmpItem2.Value = "0";
                DropDownList1.Items.Add(tmpItem2);

                foreach (UsersTable current in curators)
                {
                    ListItem tmpItem = new ListItem();
                    tmpItem.Text = current.Email;
                    tmpItem.Value = current.ID.ToString();
                    DropDownList1.Items.Add(tmpItem);
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
                            Response.Redirect("ChooseCompetition.aspx");
                        }
                        else
                        {
                            NameTextBox.Text = currentCompetition.Name;
                            DescriptionTextBox.Text = currentCompetition.Number;
                            BudjetTextBox.Text = currentCompetition.Budjet.ToString();
                            DropDownList1.SelectedIndex = Convert.ToInt32(currentCompetition.FK_Curator);
                            Calendar1.SelectedDate = Convert.ToDateTime(currentCompetition.StartDate);
                            Calendar2.SelectedDate = Convert.ToDateTime(currentCompetition.EndDate);
                            if (currentCompetition.TemplateDocName!=null) 
                            {
                                if (currentCompetition.TemplateDocName.Any())
                                {
                                    LinkButton1.Text = currentCompetition.TemplateDocName;
                                }
                                else
                                {
                                    LinkButton1.Visible = false;
                                    LinkButton1.Enabled = false;
                                }
                            }
                            else
                            {
                                LinkButton1.Visible = false;
                                LinkButton1.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
        protected void CreateSaveButtonClick(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
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
                        zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTables
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
                        competitionDataBase.zCompetitionsTables.InsertOnSubmit(newCompetition);
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
            Response.Redirect("ChooseCompetition.aspx");
        }
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChooseCompetition.aspx");
        }
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            CompetitionDataContext competitionDataBase = new CompetitionDataContext();
            var sessionParam = Session["CompetitionID"];
            if (sessionParam == null)
            {
                //error
                Response.Redirect("ChooseCompetition.aspx");
            }
            int iD = (int) sessionParam;
            if (iD > 0)
            {
                zCompetitionsTable currentCompetition = (from a in competitionDataBase.zCompetitionsTables
                    where a.Active == true
                          && a.ID == iD
                    select a).FirstOrDefault();
                if (currentCompetition == null)
                {
                    //error
                    Response.Redirect("ChooseCompetition.aspx");
                }
                        String path = Server.MapPath("~/documents/templates") + "\\\\" + currentCompetition.ID.ToString() + "\\\\" + currentCompetition.TemplateDocName;
                        HttpContext.Current.Response.ContentType = "application/x-zip-compressed";
                        HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment; filename=" + currentCompetition.TemplateDocName);
                        HttpContext.Current.Response.BinaryWrite(ReadByteArryFromFile(path));
                        HttpContext.Current.Response.End();
                        Response.End();
                    
                }

            }     
        private byte[] ReadByteArryFromFile(string destPath)
        {
            byte[] buff = null;
            FileStream fs = new FileStream(destPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            long numBytes = new FileInfo(destPath).Length;
            buff = br.ReadBytes((int)numBytes);
            return buff;
        }
       
    }
}