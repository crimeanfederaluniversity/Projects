using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class Indicators : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            int UserId = UserSer.Id;
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            List<RolesTable> UserRoles = (from a in kPiDataContext.UsersAndRolesMappingTable
                                          join b in kPiDataContext.RolesTable
                                          on a.FK_RolesTable equals b.RolesTableID
                                          where a.FK_UsersTable == UserId && b.Active == true
                                          select b).ToList();
            foreach (RolesTable Role in UserRoles)
            {
                if (Role.Role != 8) //нельзя давать пользователю роли и заполняющего и админа 
                {
                    Response.Redirect("Login.aspx");
                }
            }
            /*
            Label1.Text = "0";
            IndicatorName.Text = "";
            IndicatorFormula.Text = "";
            IndicatorMeasure.Text = "";
            */
            List<IndicatorsTable> indicatorList = (from item in kPiDataContext.IndicatorsTables select item).ToList();
            var dictionary = new Dictionary<int, string>();
            dictionary.Add(0, "Добавить новый индикатор");

            foreach (IndicatorsTable item in indicatorList)
                dictionary.Add(item.IndicatorsTableID, item.Name);

            DropDownList1.DataTextField = "Value";
            DropDownList1.DataValueField = "Key";
            DropDownList1.DataSource = dictionary;
            DropDownList1.DataBind();
           
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Label1.Text = "0";
            IndicatorName.Text = "";
            IndicatorFormula.Text = "";
            IndicatorMeasure.Text = "";
           
            int SelectedValue = -1;
            if (int.TryParse(DropDownList1.SelectedValue, out SelectedValue) && SelectedValue != -1)
            {
                if (SelectedValue > 0)
                {
                    KPIWebDataContext kPiDataContext =
                        new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
                    IndicatorsTable indicator = (from item in kPiDataContext.IndicatorsTables
                        where item.IndicatorsTableID == SelectedValue
                        select item).FirstOrDefault();
                    if (indicator.Active == true) CheckBox1.Checked = true;
                    else CheckBox1.Checked = false;
                    Label5.Text = indicator.IndicatorsTableID.ToString();
                    IndicatorName.Text = indicator.Name;
                    IndicatorFormula.Text = indicator.Formula;
                    IndicatorMeasure.Text = indicator.Measure;
                }
            }
            else
            {
                //error
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));            
            int SelectedValue = -1;
            if (int.TryParse(Label5.Text, out SelectedValue) && SelectedValue != -1)
            {
                if (SelectedValue > 0)
                {
                    IndicatorsTable indicators = (from item in kPiDataContext.IndicatorsTables
                        where item.IndicatorsTableID == SelectedValue
                        select item).FirstOrDefault();
                    //indicators.IndicatorsTableID = SelectedValue;
                    if (CheckBox1.Checked) indicators.Active = true;
                    else indicators.Active = false;
                    indicators.Name = IndicatorName.Text;
                    indicators.Formula = IndicatorFormula.Text;
                    indicators.Measure = IndicatorMeasure.Text;
                    kPiDataContext.SubmitChanges();
                }
                else
                {
                    IndicatorsTable indicators = new IndicatorsTable();
                    if (CheckBox1.Checked) indicators.Active = true;
                    else indicators.Active = false;
                    indicators.Name = IndicatorName.Text;
                    indicators.Formula = IndicatorFormula.Text;
                    indicators.Measure = IndicatorMeasure.Text;
                    kPiDataContext.IndicatorsTables.InsertOnSubmit(indicators);
                    kPiDataContext.SubmitChanges();
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            string tmp_check = CalculateAbb.CheckAbbString(IndicatorFormula.Text);
            if (tmp_check != "0")
            {
                TextBox1.Text = tmp_check;
            }
            else
            {
                TextBox1.Text = CalculateAbb.Calculate(IndicatorFormula.Text, 32).ToString();
            }
            
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext(ConfigurationManager.AppSettings.Get("ConnectionString"));
            List<BasicParametersTable> basicParamsList = (from a in kPiDataContext.BasicParametersTable
                                                          where a.Name.Contains(SearchBox.Text) || a.AbbreviationEN.Contains(SearchBox.Text)
                                                          select a).ToList();
            GridView1.DataSource = basicParamsList;
            GridView1.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}