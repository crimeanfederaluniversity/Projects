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
                Response.Redirect("~/Account/Login.aspx");
            }
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            List<RolesTable> UserRoles = (from a in kPiDataContext.UsersAndRolesMappingTable
                                          join b in kPiDataContext.RolesTable
                                          on a.FK_RolesTable equals b.RolesTableID
                                          where a.FK_UsersTable == UserSer.Id && b.Active == true
                                          select b).ToList();
            foreach (RolesTable Role in UserRoles)
            {
                if (Role.Role != 10) //нельзя давать пользователю роли и заполняющего и админа 
                {
                    Response.Redirect("~/Account/Login.aspx");
                }
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