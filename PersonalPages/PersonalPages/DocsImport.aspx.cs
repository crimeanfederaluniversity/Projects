using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

namespace PersonalPages
{
    public partial class DocsImport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Serialization UserSer = (Serialization) Session["UserID"];
                if (UserSer == null)
                {
                    Response.Redirect("~/Default.aspx");
                }

                int userID = UserSer.Id;

                PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                UsersTable userlevel = (from a in PersonalPagesDB.UsersTable
                    where a.UsersTableID == userID
                    select a).FirstOrDefault();
                List<GroupsTable> group = (from a in PersonalPagesDB.GroupsTable
                    where a.Active == true && a.FK_SecondLevel == userlevel.FK_SecondLevelSubdivisionTable
                    select a).ToList();

                foreach (GroupsTable n in group)
                {
                    ListItem TmpItem = new ListItem();
                    TmpItem.Text = n.Name;
                    TmpItem.Value = n.ID.ToString();
                    CheckBoxList1.Items.Add(TmpItem);
                }
            }
        }
 
        protected void SaveFile()
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            PersonalPagesDataContext usersDB = new PersonalPagesDataContext();
            Personal_Documents articleDoc = (from a in usersDB.Personal_Documents where a.Active == true select a).FirstOrDefault();
            String path = Server.MapPath(@"~/PersonalPages/Documents/");

            if (FileUpload1.PostedFiles.Count > 1)
            {
                int AttachOK = 0;
                int AttachError = 0;
                string DBLinkCombine = "";
                foreach (var file in FileUpload1.PostedFiles)
                {
                    try
                    {
                        file.SaveAs(path + articleDoc.ID.ToString() + "_" + file.FileName);
                        if ((AttachOK + 1) != FileUpload1.PostedFiles.Count)
                        {
                            DBLinkCombine += articleDoc.ID.ToString() + "_" + file.FileName + "@";
                        }
                        else
                        {
                            DBLinkCombine += articleDoc.ID.ToString() + "_" + file.FileName;
                        }
                        AttachOK++;
                    }
                    catch (Exception ex)
                    {
                        AttachError++;
                    }
                    articleDoc.Doc_URL = DBLinkCombine;
                    usersDB.SubmitChanges();
                }
            }
            else
            {
                try
                {
                    FileUpload1.PostedFile.SaveAs(path + articleDoc.ID.ToString() + "_" + FileUpload1.FileName);
                    articleDoc.Doc_URL = articleDoc.ID.ToString() + "_" + FileUpload1.FileName;
                    articleDoc.FK_UsersTable = userID;
                    usersDB.SubmitChanges();
                }
                catch (Exception ex)
                {
                   
                }
            }
        }      
    
        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Metodist.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           
            foreach (ListItem n in CheckBoxList1.Items)
            {
                if (n.Selected == true)
                {
                    PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                    Personal_Documents newdoc = new Personal_Documents();
                    newdoc.Active = true;
                    newdoc.FK_Group = Convert.ToInt32(n.Value);
                    PersonalPagesDB.Personal_Documents.InsertOnSubmit(newdoc);
                    PersonalPagesDB.SubmitChanges();
                }
            }
            SaveFile();
            Response.Redirect("DocsImport.aspx");
        }

        protected void DocDeleteButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button != null)
            {
                PersonalPagesDataContext PersonalPagesDB = new PersonalPagesDataContext();
                Personal_Documents docdelete = (from a in PersonalPagesDB.Personal_Documents
                                                where a.Active == true && a.ID == Convert.ToInt32(button.CommandArgument)
                                                select a).FirstOrDefault();
                if (docdelete != null)
                {
                    docdelete.Active = false;
                    PersonalPagesDB.SubmitChanges();
                }
            }
        }
    }
}