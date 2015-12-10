using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            UserRights userRights = new UserRights();
            if (!userRights.CanUserSeeThisPage(userID, 1, 0, 0))
            {
                Response.Redirect(ConfigurationManager.AppSettings.Get("MainSiteName"));
            } 
            ///////////////////////////////////////////////////////////////////////////////////////////////////
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();
            var vrCountry = (from a in kpiWebDataContext.BasicParametersTable
                             select a).Except(from b in kpiWebDataContext.BasicParametersTable
                                              join c in kpiWebDataContext.BasicParametersAndRolesMappingTable on b.BasicParametersTableID 
                                              equals c.FK_BasicParametersTable select b).ToList();

            Dictionary<string, int> tempDictionary = new Dictionary<string, int>();

            foreach (var obj in vrCountry)
            {
                tempDictionary.Add(obj.Name,obj.BasicParametersTableID);
            }
            if (tempDictionary.Count == 0)
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script","alert('Все базовые параметры распределены');", true);
            }
            else
            {
                DataTable dt = LINQToDataTable(tempDictionary);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
        }
               
        public DataTable LINQToDataTable<T>(IEnumerable<T> varlist)
        {
             DataTable dtReturn = new DataTable();
             PropertyInfo[] oProps = null;
             if (varlist == null) return dtReturn;
             foreach (T rec in varlist)
             {       
                  if (oProps == null)
                  {
                       oProps = ((Type)rec.GetType()).GetProperties();
                       foreach (PropertyInfo pi in oProps)
                       {
                            Type colType = pi.PropertyType;

                            if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()      
                            ==typeof(Nullable<>)))
                             {
                                 colType = colType.GetGenericArguments()[0];
                             }
                            dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                       }
                  }

                  DataRow dr = dtReturn.NewRow();

                  foreach (PropertyInfo pi in oProps)
                  {
                       dr[pi.Name] = pi.GetValue(rec, null) == null ?DBNull.Value :pi.GetValue
                       (rec,null);
                  }

                  dtReturn.Rows.Add(dr);
             }
             return dtReturn;
        }
    }
}