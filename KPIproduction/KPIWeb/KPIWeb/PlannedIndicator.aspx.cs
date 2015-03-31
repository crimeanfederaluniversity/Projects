﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
//using Microsoft.Office.Interop.Excel;
using Page = System.Web.UI.Page;
namespace KPIWeb
{
    public partial class PlannedIndicator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            if (!(Page.IsPostBack))
            {
                List<IndicatorsTable> indicatorList = (from item in kPiDataContext.IndicatorsTable
                                                       where item.Active == true
                                                       select item).ToList();
                var dictionary = new Dictionary<int, string>();
                dictionary.Add(0, "Выберите индикатор");
                foreach (IndicatorsTable item in indicatorList)
                    dictionary.Add(item.IndicatorsTableID, item.Name);
                DropDownList1.DataTextField = "Value";
                DropDownList1.DataValueField = "Key";
                DropDownList1.DataSource = dictionary;
                DropDownList1.DataBind();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<PlannedIndicator> plannedList = (from item in kPiDataContext.PlannedIndicator select item).ToList();
            GridView1.DataSource = plannedList;
            GridView1.DataBind();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            PlannedIndicator indicators = new PlannedIndicator();
            if (CheckBox1.Checked) indicators.Active = true;
            else indicators.Active = false;
            indicators.Date = DateTime.Now;
            indicators.FK_IndicatorsTable = Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value);
            indicators.Value = Convert.ToInt32(IndicatorMeasure.Text);
            kPiDataContext.PlannedIndicator.InsertOnSubmit(indicators);
            kPiDataContext.SubmitChanges();
            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Индикатор создан');", true);
        }



        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }



    }
}