using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class SendInvite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            List<FirstLevelSubdivisionTable> FirstList = (from a in kpiWebDataContext.FirstLevelSubdivisionTable
                                                          where a.Active == true
                                                          select a).ToList();
            int i = 0;

                    foreach (FirstLevelSubdivisionTable current in FirstList)
                    {
                        CheckBoxList1.Items.Add(current.Name);
                        CheckBoxList1.Items[i].Value = current.FirstLevelSubdivisionTableID.ToString();
                        i++;
                    }    
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            
            var users = (from a in kPiDataContext.UsersTable where a.Active && a.Confirmed == false select a).ToList();

            EmailTemplate EmailParams = (from a in kPiDataContext.EmailTemplate
                                         where a.Name == "InviteToRegister"
                                         && a.Active == true
                                         select a).FirstOrDefault();
            foreach (var user in users)
            {
                Action.MassMailing(user.Email, EmailParams.EmailTitle,
                EmailParams.EmailContent.Replace("#LINK#", ConfigurationManager.AppSettings.Get("SiteName") + "/Account/UserRegister?&id=" + user.PassCode).
                Replace("#SiteName#", ConfigurationManager.AppSettings.Get("SiteName")), null);
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            foreach (ListItem currentAcademy in CheckBoxList1.Items)
            {
                if (currentAcademy.Selected == true)
                {
                    var users = (from a in kPiDataContext.UsersTable where a.Active 
                                     && a.Confirmed == false
                                 && a.FK_FirstLevelSubdivisionTable == Convert.ToInt32(currentAcademy.Value)
                                 select a).ToList();

                    EmailTemplate EmailParams = (from a in kPiDataContext.EmailTemplate
                                                 where a.Name == "InviteToRegister"
                                                 && a.Active == true
                                                 select a).FirstOrDefault();
                    foreach (var user in users)
                    {
                        Action.MassMailing(user.Email, EmailParams.EmailTitle,
                        EmailParams.EmailContent.Replace("#LINK#", ConfigurationManager.AppSettings.Get("SiteName") + "/Account/UserRegister?&id=" + user.PassCode).
                        Replace("#SiteName#", ConfigurationManager.AppSettings.Get("SiteName")), null);
                    }
                }
            }          
        }
    }
}