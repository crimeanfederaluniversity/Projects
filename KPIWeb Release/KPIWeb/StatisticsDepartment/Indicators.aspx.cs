﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
//using Microsoft.Office.Interop.Excel;
using Page = System.Web.UI.Page;

namespace KPIWeb.StatisticsDepartment
{
    public partial class Indicators : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            int userID = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10)&&(userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }
            ////////////////////////////////////////////////////////////////

            if (!Page.IsPostBack)
            {
                
            }
        }

        protected bool abbExists(string abb,int id)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
         //   int indabbcnt = from a in kPiDataContext.IndicatorsTable
           //                 where a.
            int calcabbcnt = (from a in kPiDataContext.CalculatedParametrs
                where a.AbbreviationEN == abb
                && a.CalculatedParametrsID!=id
                select a).ToList().Count();
            int basicabbcnt = (from a in kPiDataContext.BasicParametersTable
                               where a.AbbreviationEN == abb
                && a.BasicParametersTableID!=id
                select a).ToList().Count();
            if ((calcabbcnt + basicabbcnt) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label5.Text = "0";
            IndicatorName.Text = "";
            IndicatorFormula.Text = "";
            IndicatorMeasure.Text = "";
            TextBoxAbb.Text = "";
            LabelAbbError.Visible = false;
           
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                if (SelectedValue > 0)
                {
                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext();
                    if ((int)ViewState["state"] == 0) ///// целевой показатель
                    {
                        IndicatorsTable indicator = (from item in kPiDataContext.IndicatorsTable
                                                     where item.IndicatorsTableID == SelectedValue
                                                     select item).FirstOrDefault();
                        if (indicator.Active == true) CheckBox1.Checked = true;
                        else CheckBox1.Checked = false;
                        Label5.Text = indicator.IndicatorsTableID.ToString();
                        IndicatorName.Text = indicator.Name;
                        IndicatorFormula.Text = indicator.Formula;
                        IndicatorMeasure.Text = indicator.Measure;
                    }
                    else if ((int)ViewState["state"] == 1)///рассчетные показатели
                    {
                        CalculatedParametrs calcParams = (from item in kPiDataContext.CalculatedParametrs
                                                     where item.CalculatedParametrsID == SelectedValue
                                                     select item).FirstOrDefault();
                        if (calcParams.Active == true) CheckBox1.Checked = true;
                        else CheckBox1.Checked = false;
                        Label5.Text = calcParams.CalculatedParametrsID.ToString();
                        IndicatorName.Text = calcParams.Name;
                        TextBoxAbb.Text = calcParams.AbbreviationEN;
                        IndicatorFormula.Text = calcParams.Formula;
                        IndicatorMeasure.Text = calcParams.Measure;
                    }                   
                }
            }
            else
            {
                //error
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();            
            int SelectedValue = -1;
            if (int.TryParse(Label5.Text, out SelectedValue) && SelectedValue != -1)
            {
                if (SelectedValue > 0)
                {
                    if ((int)ViewState["state"] == 0) ///// целевой показатель
                    {
                        IndicatorsTable indicators = (from item in kPiDataContext.IndicatorsTable
                            where item.IndicatorsTableID == SelectedValue
                            select item).FirstOrDefault();
                        if (CheckBox1.Checked) indicators.Active = true;
                        else indicators.Active = false;
                        indicators.Name = IndicatorName.Text;
                        indicators.Formula = IndicatorFormula.Text;
                        indicators.Measure = IndicatorMeasure.Text;
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Целевой показатель изменен');", true);    
                    }
                    else if ((int)ViewState["state"] == 1)///рассчетные показатели
                    {
                        if (abbExists(TextBoxAbb.Text,
                            Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                                "alert('Аббревиатура не является уникальной, измените её');", true);
                        }
                        else
                        {
                            CalculatedParametrs calcParams = (from item in kPiDataContext.CalculatedParametrs
                                where item.CalculatedParametrsID == SelectedValue
                                select item).FirstOrDefault();
                            if (CheckBox1.Checked) calcParams.Active = true;
                            else calcParams.Active = false;
                            calcParams.Name = IndicatorName.Text;
                            calcParams.Formula = IndicatorFormula.Text;
                            calcParams.AbbreviationEN = TextBoxAbb.Text;
                            calcParams.Measure = IndicatorMeasure.Text;
                            Page.ClientScript.RegisterClientScriptBlock(typeof (Page), "Script",
                                "alert('Расчётный показатель изменен');", true);
                        }
                    }
                }
                else
                {
                    if ((int)ViewState["state"] == 0) ///// целевой показатель
                    {
                        IndicatorsTable indicators = new IndicatorsTable();
                        if (CheckBox1.Checked) indicators.Active = true;
                        else indicators.Active = false;
                        indicators.Name = IndicatorName.Text;
                        indicators.Formula = IndicatorFormula.Text;
                        indicators.Measure = IndicatorMeasure.Text;
                        kPiDataContext.IndicatorsTable.InsertOnSubmit(indicators);
                        Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Целевой показатель создан');", true);
                    }
                    else if ((int)ViewState["state"] == 1)///рассчетные показатели
                    {
                        if (abbExists(TextBoxAbb.Text,Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)))
                        {
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Аббревиатура не является уникальной, измените её');", true);
                        }
                        else
                        {
                            CalculatedParametrs calcParams = new CalculatedParametrs();
                            if (CheckBox1.Checked) calcParams.Active = true;
                            else calcParams.Active = false;
                            calcParams.Name = IndicatorName.Text;
                            calcParams.Formula = IndicatorFormula.Text;
                            calcParams.Measure = IndicatorMeasure.Text;
                            calcParams.AbbreviationEN = TextBoxAbb.Text;
                            kPiDataContext.CalculatedParametrs.InsertOnSubmit(calcParams);
                            Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Расчётный параметр создан');", true);                         
                        }                
                    }          
                }
                kPiDataContext.SubmitChanges();
            }
        } //Добавление в БД // внутри разделение на целевой показатель и расчётные

        protected void Button2_Click(object sender, EventArgs e)
        {
                    
        }///Рассчет

        protected void Button4_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            List<BasicParametersTable> basicParamsList = (from a in kPiDataContext.BasicParametersTable
                                                          where a.Name.Contains(SearchBox.Text) || a.AbbreviationRU.Contains(SearchBox.Text) || a.AbbreviationEN.Contains(SearchBox.Text)
                                                          select a).ToList();
            GridView1.DataSource = basicParamsList;
            GridView1.DataBind();
        }////ПОИСК

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (TextBox2.Text == "") TextBox2.Text = 0.ToString();
            if (TextBox3.Text == "") TextBox3.Text = 0.ToString();
            if (TextBox4.Text == "") TextBox4.Text = 0.ToString();
            if (TextBox5.Text == "") TextBox5.Text = 0.ToString();
            if (TextBox6.Text == "") TextBox6.Text = 0.ToString();
            if (TextBox7.Text == "") TextBox7.Text = 0.ToString();

            TextBox1.Text = CalculateAbb.CalculateForLevel(1, IndicatorFormula0.Text, 2007, 0,
            Convert.ToInt32(TextBox2.Text), Convert.ToInt32(TextBox3.Text), Convert.ToInt32(TextBox4.Text),
            Convert.ToInt32(TextBox5.Text), Convert.ToInt32(TextBox6.Text), Convert.ToInt32(TextBox7.Text),0,-1).ToString();
        }//Рассчет с подразделениями

        protected void Menu1_MenuItemClick(object sender, MenuEventArgs e)
        {
            int viewidx = Int32.Parse(e.Item.Value);
            
            switch (viewidx)
            {
                case 0://работа с целевой показатель
                {
                    if (ViewState["state"]!=null)
                    if ((int)ViewState["state"] != 0)
                    {
                        CheckBox1.Checked = false;
                        Label5.Text = "0";
                        IndicatorName.Text = "";
                        IndicatorFormula.Text = "";
                        //TextBox8.Text = "";
                        IndicatorMeasure.Text = "";
                    }
                    ViewState["state"] = 0;

                    addtitle.Text = "Форма редактирования целевого показателя";
                    Label1.Text = "Название целевого показателя";
                    Button3.Text = "Сохранить изменения целевого показателя";
                    TextBoxAbb.Visible = false;
                    LabelAbb.Visible = false;
                    LabelAbbError.Visible = false;
                   // DropDownList1.DataSource=""
                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext();
                    List<IndicatorsTable> indicatorList = (from item in kPiDataContext.IndicatorsTable select item).ToList();
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(0, "Добавить новый целевой показатель");
                    foreach (IndicatorsTable item in indicatorList)
                        dictionary.Add(item.IndicatorsTableID, item.Name);
                    DropDownList1.DataTextField = "Value";
                    DropDownList1.DataValueField = "Key";
                    DropDownList1.DataSource = dictionary;
                    DropDownList1.DataBind();

                    MultiView1.ActiveViewIndex = 0;                   
                    break;
                }
                case 1://работа с расчётными показателями
                {
                    if (ViewState["state"] != null)
                    if ((int)ViewState["state"] != 1)
                    {
                        CheckBox1.Checked = false;
                        Label5.Text = "0";
                        IndicatorName.Text = "";
                        IndicatorFormula.Text = "";
                        TextBoxAbb.Text = "";
                        IndicatorMeasure.Text = "";
                    }
                    ViewState["state"] = 1;
                    addtitle.Text = "Форма редактирования расчётного показателя";
                    Label1.Text = "Название расчётного показателя";
                    Button3.Text = "Сохранить изменения расчётного показателя";
                    TextBoxAbb.Visible = true;
                    LabelAbb.Visible = true;
                    LabelAbbError.Visible = false;

                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext();

                    List<CalculatedParametrs> calcParamsTable = (from item in kPiDataContext.CalculatedParametrs select item).ToList();
                    var dictionary = new Dictionary<int, string>();
                    dictionary.Add(0, "Добавить новый расчётный показатель");
                    foreach (CalculatedParametrs item in calcParamsTable)
                        dictionary.Add(item.CalculatedParametrsID, item.Name);
                    DropDownList1.DataTextField = "Value";
                    DropDownList1.DataValueField = "Key";
                    DropDownList1.DataSource = dictionary;
                    DropDownList1.DataBind();

                    MultiView1.ActiveViewIndex = 0;
                    break;
                }
                case 2://проверка формул
                {
                    MultiView1.ActiveViewIndex = 1;
                    break;
                }
                case 3://поисх аббревиатур
                {
                    MultiView1.ActiveViewIndex = 2;
                    break;
                }
                default://невозможно)
                {
                    break;
                }
            }
        }

        protected void TextBoxAbb_TextChanged(object sender, EventArgs e)
        {
            LabelAbbError.Visible = true;
            if (!abbExists(TextBoxAbb.Text,Convert.ToInt32(DropDownList1.Items[DropDownList1.SelectedIndex].Value)))
            {
                LabelAbbError.ForeColor = Color.Green;
                LabelAbbError.Text = "Аббревиатура уникальна.";
                LabelAbbError.Visible = true;
            }
            else
            {
                LabelAbbError.ForeColor = Color.Red;
                LabelAbbError.Text = "Аббревиатура не является уникальной.";
                LabelAbbError.Visible = true;
            }
        }
    }
}