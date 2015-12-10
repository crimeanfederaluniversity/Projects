﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net.Util.TypeConverters;

namespace KPIWeb.AutomationDepartment
{
    public partial class WatchingParamtrsState : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization) Session["UserID"];
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
            //////////////////////////////////////////////////////////////////

            if (!Page.IsPostBack)
            {
                List<FirstLevelSubdivisionTable> First_stageList =
                    (from item in kPiDataContext.FirstLevelSubdivisionTable
                     select item).OrderBy(mc => mc.Name).ToList();

                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите значение");

                foreach (var item in First_stageList)
                    dictionary.Add(item.FirstLevelSubdivisionTableID, item.Name);

               // FillGridVIews(0);

                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();


                List<ReportArchiveTable> reportList =
                 (from item in kPiDataContext.ReportArchiveTable
                  select item).OrderBy(mc => mc.Name).ToList();

                var diction = new Dictionary<int, string>();
                diction.Add(0, "Выберите отчет");

                foreach (var item in reportList)
                    diction.Add(item.ReportArchiveTableID, item.Name);

                

                DropDownList4.DataTextField = "Value";
                DropDownList4.DataValueField = "Key";
                DropDownList4.DataSource = diction;
                DropDownList4.DataBind();
            }



        }

        private void RefreshGrid()


        {
            KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();


            var vrCountry = (from b in kpiWebDataContext.CollectedBasicParametersTable select b);

            DataTable dataTable = new DataTable();

            dataTable.Columns.Add(new DataColumn("Name", typeof (string)));
            dataTable.Columns.Add(new DataColumn("Value", typeof (int)));
            dataTable.Columns.Add(new DataColumn("State", typeof (string)));
            dataTable.Columns.Add(new DataColumn("BasicId", typeof (int)));
            CollectedBasicParametersTable parametersTable = (from a in kpiWebDataContext.CollectedBasicParametersTable
                //where a.RolesTableID == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                select a).FirstOrDefault();

            List<ZeroLevelSubdivisionTable> zeroLevelSubdivisionTable =
                (from a in kpiWebDataContext.ZeroLevelSubdivisionTable select a).ToList();
            List<FirstLevelSubdivisionTable> firstLevelSubdivisionTable =
                (from a in kpiWebDataContext.FirstLevelSubdivisionTable select a).ToList();
            List<SecondLevelSubdivisionTable> secondLevelSubdivisionTable =
                (from a in kpiWebDataContext.SecondLevelSubdivisionTable select a).ToList();
            List<ThirdLevelSubdivisionTable> thirdLevelSubdivisionTable =
                (from a in kpiWebDataContext.ThirdLevelSubdivisionTable select a).ToList();
            List<FourthLevelSubdivisionTable> fourthLevelSubdivisionTable =
                (from a in kpiWebDataContext.FourthLevelSubdivisionTable select a).ToList();
            List<FifthLevelSubdivisionTable> fifthLevelSubdivisionTable =
                (from a in kpiWebDataContext.FifthLevelSubdivisionTable select a).ToList();

            foreach (var obj in vrCountry)
            {
                DataRow dataRow = dataTable.NewRow();
                List<CollectedBasicParametersTable> parameters =
                    (from a in kpiWebDataContext.CollectedBasicParametersTable
                        //where a.FK_BasicParametersTable == obj.BasicParametersTableID
                        // && a.FK_RolesTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                        select a).ToList();
            /*  if (obj.CollectedValue!=null)
              { 
                    dataRow["Value"] = (from a in kpiWebDataContext.CollectedBasicParametersTable
                        //where a.FK_BasicParametersTable == obj.
                                        select obj.CollectedValue).FirstOrDefault();
              }
              else
              {
                  dataRow["Value"] = 0;
              }*/
                // ViewState["BasicRoleMapping"] = dataTable;
                  dataRow["BasicId"] = (from a in kpiWebDataContext.BasicParametersTable select a.BasicParametersTableID).FirstOrDefault();
                    dataRow["Name"] = (from a in kpiWebDataContext.BasicParametersTable select a.Name).FirstOrDefault();
                    dataTable.Rows.Add(dataRow);
                GridView1.DataSource = dataTable;
                GridView1.DataBind();
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<SecondLevelSubdivisionTable> second_stageList =
                    (from item in kPiDataContext.SecondLevelSubdivisionTable
                        where item.FK_FirstLevelSubdivisionTable == SelectedValue
                        select item).OrderBy(mc => mc.SecondLevelSubdivisionTableID).ToList();

                if (second_stageList != null && second_stageList.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in second_stageList)
                        dictionary.Add(item.SecondLevelSubdivisionTableID, item.Name);
                    DropDownList2.DataTextField = "Value";
                    DropDownList2.DataValueField = "Key";
                    DropDownList2.DataSource = dictionary;
                    DropDownList2.DataBind();
                }
                else
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);                   
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script", "alert('Произошла ошибка.');", true);
            }
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList3.Items.Clear();
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            int SelectedValue = -1;
            if (int.TryParse(DropDownList2.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                List<ThirdLevelSubdivisionTable> third_stage = (from item in kPiDataContext.ThirdLevelSubdivisionTable
                    where item.FK_SecondLevelSubdivisionTable == SelectedValue
                    select item).OrderBy(mc => mc.ThirdLevelSubdivisionTableID).ToList();

                if (third_stage != null && third_stage.Count() > 0)
                {
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(-1, "Выберите значение");
                    foreach (var item in third_stage)
                        dictionary.Add(item.ThirdLevelSubdivisionTableID, item.Name);
                    DropDownList3.DataTextField = "Value";
                    DropDownList3.DataValueField = "Key";
                    DropDownList3.DataSource = dictionary;
                    DropDownList3.DataBind();
                }
                else
                {
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Произошла ошибка.');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script", "alert('Произошла ошибка.');", true);
            }
        }

        protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {


            RefreshGrid();


            /* KPIWebDataContext kpiWebDataContext = new KPIWebDataContext();


             var vrCountry = (from b in kpiWebDataContext.BasicParametersTable select b);

             DataTable dataTable = new DataTable();
             //dataTable.Columns.Add(new DataColumn("VerifyChecked", typeof(bool)));
             //dataTable.Columns.Add(new DataColumn("EditChecked", typeof(bool)));
            
             dataTable.Columns.Add(new DataColumn("Name", typeof(string)));
             dataTable.Columns.Add(new DataColumn("Value", typeof(int)));
             dataTable.Columns.Add(new DataColumn("State", typeof(string)));
             dataTable.Columns.Add(new DataColumn("BasicId", typeof(string)));
             CollectedBasicParametersTable parametersTable = (from a in kpiWebDataContext.CollectedBasicParametersTable
                                //where a.RolesTableID == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                                select a).FirstOrDefault();

            
             foreach (var obj in vrCountry)
             {
                 DataRow dataRow = dataTable.NewRow();
                 CollectedBasicParametersTable parameters =
                     (from a in kpiWebDataContext.CollectedBasicParametersTable
                      //where a.FK_BasicParametersTable == obj.BasicParametersTableID
                     // && a.FK_RolesTable == Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)
                      select a).FirstOrDefault();
                 if (parameters != null)
                     /*  {
                     dataRow["EditChecked"] = parameters.CanEdit;
                     dataRow["ViewChecked"] = parameters.CanView;
                     dataRow["VerifyChecked"] = roleAndBasicMapping.CanConfirm;
                 }
                 else
                 {
                     dataRow["EditChecked"] = false;
                     dataRow["ViewChecked"] = false;
                     dataRow["VerifyChecked"] = false;
                 }

                     dataRow["Value"] = (from a in kpiWebDataContext.CollectedBasicParametersTable
                         where a.FK_BasicParametersTable == obj.BasicParametersTableID
                         select a).FirstOrDefault();
                 dataRow["BasicId"] = obj.BasicParametersTableID.ToString();
                 dataRow["Name"] = obj.Name;
                 dataTable.Rows.Add(dataRow);
             }
            // ViewState["BasicRoleMapping"] = dataTable;
            GridView1.DataSource = dataTable;
            GridView1.DataBind();*/
        }
    }
}