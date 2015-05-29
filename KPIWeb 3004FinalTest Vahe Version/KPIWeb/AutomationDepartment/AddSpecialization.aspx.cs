using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.AutomationDepartment
{
    public partial class AddSpecialization : System.Web.UI.Page
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

            if (userTable.AccessLevel != 10)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        protected void Button1_Click(object sender, EventArgs e) //пока безо всяких проверок //окай
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            string tmpStr;
            tmpStr = TextBox1.Text;
            string[] tmpStrArr = tmpStr.Split('\r');
            int i = 0;

            foreach (string tmpStrf in tmpStrArr)
            {
                string tmp = tmpStrf.Replace("\n", "");
                if (((tmp.Split('#').Length - 1) != 1) && (tmp != ""))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка в строке" + i.ToString() + "');", true);
                    i = 0;
                    break;
                }
                i++;
            }
            if (i > 0)
            {
                foreach (string tmpStrf in tmpStrArr)
                {
                    if (tmpStrf.Length > 10)
                    {
                        string tmp = tmpStrf.Replace("\n", "");
                        FieldOfExpertise field = new FieldOfExpertise();
                        string[] strArrf = tmp.Split('#');
                        strArrf[0] = strArrf[0].TrimEnd();
                        strArrf[0] = strArrf[0].TrimStart();
                        strArrf[1] = strArrf[1].TrimEnd();
                        strArrf[1] = strArrf[1].TrimStart();
                        field.Active = strArrf[0]=="0"?false:true;
                        field.Name = strArrf[1];
                        kPiDataContext.FieldOfExpertise.InsertOnSubmit(field);
                    }
                }
            }
            kPiDataContext.SubmitChanges();           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            string tmpStr;
            tmpStr = TextBox2.Text;
            string[] tmpStrArr = tmpStr.Split('\r');
            int i = 0;

            foreach (string tmpStrf in tmpStrArr)
            {
                string tmp = tmpStrf.Replace("\n", "");
                if (((tmp.Split('#').Length - 1) != 3) && (tmp != ""))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка в строке" + i.ToString() + "');", true);
                    i = 0;
                    break;
                }
                i++;
            }
            if (i > 0)
            {
                foreach (string tmpStrf in tmpStrArr)
                {
                    if (tmpStrf.Length > 10)
                    {
                        string tmp = tmpStrf.Replace("\n", "");
                        SpecializationTable spec = new SpecializationTable();
                        string[] strArrf = tmp.Split('#');
                        strArrf[0] = strArrf[0].TrimEnd();
                        strArrf[0] = strArrf[0].TrimStart();
                        strArrf[1] = strArrf[1].TrimEnd();
                        strArrf[1] = strArrf[1].TrimStart();
                        strArrf[2] = strArrf[2].TrimEnd();
                        strArrf[2] = strArrf[2].TrimStart();
                        strArrf[3] = strArrf[3].TrimEnd();
                        strArrf[3] = strArrf[3].TrimStart();
                        spec.Active = strArrf[0] == "0" ? false : true;
                        spec.Name = strArrf[1];
                        spec.SpecializationNumber = strArrf[2];
                        spec.FK_FieldOfExpertise = Convert.ToInt32(strArrf[3]);
                        kPiDataContext.SpecializationTable.InsertOnSubmit(spec);
                    }
                }
            }
            kPiDataContext.SubmitChanges();
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            string tmpStr;
            tmpStr = TextBox3.Text;
            string[] tmpStrArr = tmpStr.Split('\r');
            int i = 0;

            foreach (string tmpStrf in tmpStrArr)
            {
                string tmp = tmpStrf.Replace("\n", "");
                if (((tmp.Split('#').Length - 1) != 10) && (tmp != ""))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка в строке" + i.ToString() + "');", true);
                    i = 0;
                    break;
                }
                i++;
            }
            if (i > 0)
            {
                foreach (string tmpStrf in tmpStrArr)
                {
                    if (tmpStrf.Length > 10)
                    {
                        string tmp = tmpStrf.Replace("\n", "");
                        BasicParametersTable basicParametr = new BasicParametersTable();
                        string[] strArrf = tmp.Split('#');

                        strArrf[0] = strArrf[0].TrimEnd();
                        strArrf[0] = strArrf[0].TrimStart();
                        strArrf[1] = strArrf[1].TrimEnd();
                        strArrf[1] = strArrf[1].TrimStart();
                        strArrf[2] = strArrf[2].TrimEnd();
                        strArrf[2] = strArrf[2].TrimStart();
                        strArrf[3] = strArrf[3].TrimEnd();
                        strArrf[3] = strArrf[3].TrimStart();
                        strArrf[4] = strArrf[4].TrimEnd();
                        strArrf[4] = strArrf[4].TrimStart();
                        strArrf[5] = strArrf[5].TrimEnd();
                        strArrf[5] = strArrf[5].TrimStart();
                        strArrf[6] = strArrf[6].TrimEnd();
                        strArrf[6] = strArrf[6].TrimStart();
                        strArrf[7] = strArrf[7].TrimEnd();
                        strArrf[7] = strArrf[7].TrimStart();
                        strArrf[8] = strArrf[8].TrimEnd();
                        strArrf[8] = strArrf[8].TrimStart();
                        strArrf[9] = strArrf[9].TrimEnd();
                        strArrf[9] = strArrf[9].TrimStart();
                        strArrf[10] = strArrf[10].TrimEnd();
                        strArrf[10] = strArrf[10].TrimStart();
                        basicParametr.Active = true;
                        basicParametr.Name = strArrf[0];
                        basicParametr.AbbreviationEN = strArrf[1];
                        basicParametr.AbbreviationRU = strArrf[2];
                        basicParametr.Measure = strArrf[3];
                        kPiDataContext.BasicParametersTable.InsertOnSubmit(basicParametr);
                        kPiDataContext.SubmitChanges();

                        BasicParametrAdditional basicParamAdd = new BasicParametrAdditional();
                        basicParamAdd.BasicParametrAdditionalID = basicParametr.BasicParametersTableID;
                        basicParamAdd.Active = true;
                        basicParamAdd.ForForeignStudents = (Convert.ToInt32(strArrf[4])>0)?true:false;
                        basicParamAdd.SubvisionLevel = Convert.ToInt32(strArrf[5]);
                        basicParamAdd.IsGraduating = (Convert.ToInt32(strArrf[6]) > 0) ? true : false;
                        basicParamAdd.Calculated = (Convert.ToInt32(strArrf[7]) > 0) ? true : false;
                        basicParamAdd.SpecType = Convert.ToInt32(strArrf[8]);
                        basicParamAdd.DataType = Convert.ToInt32(strArrf[9]);
                        basicParamAdd.Comment = strArrf[10];
                        kPiDataContext.BasicParametrAdditional.InsertOnSubmit(basicParamAdd);
                    }
                }
                kPiDataContext.SubmitChanges();
            }           
        }


        protected void Button4_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            string tmpStr;
            tmpStr = TextBox4.Text;
            string[] tmpStrArr = tmpStr.Split('\r');
            int i = 0;

            foreach (string tmpStrf in tmpStrArr)
            {
                string tmp = tmpStrf.Replace("\n", "");
                if (((tmp.Split('#').Length - 1) != 4) && (tmp != ""))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка в строке" + i.ToString() + "');", true);
                    i = 0;
                    break;
                }
                i++;
            }
            if (i > 0)
            {
                foreach (string tmpStrf in tmpStrArr)
                {
                    if (tmpStrf.Length > 4)
                    {
                        string tmp = tmpStrf.Replace("\n", "");
                        CalculatedParametrs calcPar = new CalculatedParametrs();
                        string[] strArrf = tmp.Split('#');

                        strArrf[0] = strArrf[0].TrimEnd();
                        strArrf[0] = strArrf[0].TrimStart();
                        strArrf[1] = strArrf[1].TrimEnd();
                        strArrf[1] = strArrf[1].TrimStart();
                        strArrf[2] = strArrf[2].TrimEnd();
                        strArrf[2] = strArrf[2].TrimStart();
                        strArrf[3] = strArrf[3].TrimEnd();
                        strArrf[3] = strArrf[3].TrimStart();
                        strArrf[4] = strArrf[4].TrimEnd();
                        strArrf[4] = strArrf[4].TrimStart();

                        calcPar.Active = true;
                        calcPar.Name = strArrf[0];
                        calcPar.AbbreviationEN = strArrf[1];
                        calcPar.AbbreviationRU = strArrf[2];
                        calcPar.Measure = strArrf[3];
                        calcPar.Formula = strArrf[4];
                        kPiDataContext.CalculatedParametrs.InsertOnSubmit(calcPar);
                        kPiDataContext.SubmitChanges();
                    }
                }
            }
        }


        protected void Button5_Click(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();

            string tmpStr;
            tmpStr = TextBox5.Text;
            string[] tmpStrArr = tmpStr.Split('\r');
            int i = 0;

            foreach (string tmpStrf in tmpStrArr)
            {
                string tmp = tmpStrf.Replace("\n", "");
                if (((tmp.Split('#').Length - 1) != 2) && (tmp != ""))
                {
                    Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Ошибка в строке" + i.ToString() + "');", true);
                    i = 0;
                    break;
                }
                i++;
            }
            if (i > 0)
            {
                foreach (string tmpStrf in tmpStrArr)
                {
                    if (tmpStrf.Length > 2)
                    {
                        string tmp = tmpStrf.Replace("\n", "");
                        IndicatorsTable indpar = new IndicatorsTable();
                        string[] strArrf = tmp.Split('#');

                        strArrf[0] = strArrf[0].TrimEnd();
                        strArrf[0] = strArrf[0].TrimStart();
                        strArrf[1] = strArrf[1].TrimEnd();
                        strArrf[1] = strArrf[1].TrimStart();
                        strArrf[2] = strArrf[2].TrimEnd();
                        strArrf[2] = strArrf[2].TrimStart();
                   

                        indpar.Active = true;
                        indpar.Name = strArrf[0];
                       // indpar.AbbreviationEN = strArrf[1];
                       // indpar.AbbreviationRU = strArrf[2];
                        indpar.Formula = strArrf[1];
                        indpar.Measure = strArrf[2];
                        
                        kPiDataContext.IndicatorsTable.InsertOnSubmit(indpar);
                        kPiDataContext.SubmitChanges();
                    }
                }
            }
        }

    }
}