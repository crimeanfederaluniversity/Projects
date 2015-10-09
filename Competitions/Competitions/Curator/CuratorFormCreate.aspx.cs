using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Competitions.Curator
{
    public partial class CuratorFormCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                CompetitionDataContext curator = new CompetitionDataContext();
                List<zTemplateFormTable> comp = (from a in curator.zTemplateFormTable where a.Active == true select a).ToList();
                foreach (zTemplateFormTable n in comp)
                {
                    ListItem TmpItem = new ListItem();
                    TmpItem.Text = n.FormName;
                    TmpItem.Value = n.ID.ToString();
                    CheckBoxList1.Items.Add(TmpItem);
                }
                var sessionParam = Session["CompetitionID"];
                if (sessionParam == null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка!');", true); 
                }
                else
                {
                    int iD = (int)sessionParam;
                    if (iD > 0)
                    {
                          foreach(ListItem current in CheckBoxList1.Items)
                        {
                        CompetitionDataContext competitionDataBase = new CompetitionDataContext();
                        zFormCompetitionMappingTable link = (from a in competitionDataBase.zFormCompetitionMappingTable
                                                                          where a.FK_Competition == iD && a.Active == true
                                                                                && a.FK_Form == Convert.ToInt32(current.Value)
                                                                          select a).Distinct().FirstOrDefault();
                        if (link != null)
                        {
                            current.Selected = true;
                        }
                       

                            }

                        }
                    }
                }
            }        

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("CuratorTZ.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
              var sessionParam = Session["CompetitionID"];
                if (sessionParam == null)
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка!');", true); 
                }
                else
                {
                    int iD = (int)sessionParam;
                    if (iD > 0)
                    {
                       CompetitionDataContext competitionDataBase = new CompetitionDataContext();                    
                          foreach(ListItem current in CheckBoxList1.Items)
                        {
                             zFormCompetitionMappingTable currentlink = (from a in competitionDataBase.zFormCompetitionMappingTable
                                                                         where  a.FK_Competition == iD
                                                                         && a.FK_Form == Convert.ToInt32(current.Value)
                                                                 select a).Distinct().FirstOrDefault();
                              if (currentlink != null)
                        {
                            currentlink.Active = current.Selected;
                            competitionDataBase.SubmitChanges();                     
                        }
                        else 
                        {
                            zFormCompetitionMappingTable newlink = new zFormCompetitionMappingTable();
                            newlink.FK_Competition = iD;
                            newlink.FK_Form = Convert.ToInt32(current.Value);
                            newlink.Active = current.Selected;
                            competitionDataBase.zFormCompetitionMappingTable.InsertOnSubmit(newlink);
                            competitionDataBase.SubmitChanges();
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Готово!');", true);
                            Response.Redirect("CuratorCompetition.aspx");

                        }
                     }
                  }
               }
             }
        
    }
}