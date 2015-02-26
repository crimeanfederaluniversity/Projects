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

        }

        protected void Button1_Click(object sender, EventArgs e) //пока безо всяких проверок
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
                if (((tmp.Split('#').Length - 1) != 5) && (tmp != ""))
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

                        basicParametr.Active = true;
                        basicParametr.Name = strArrf[0];
                        basicParametr.AbbreviationEN = strArrf[1];
                        basicParametr.AbbreviationRU = strArrf[2];
                        basicParametr.Measure = strArrf[3];
                        basicParametr.SubvisionLevel = Convert.ToInt32(strArrf[4]);
                        basicParametr.FK_FieldOfExpertise = Convert.ToInt32(strArrf[5]);
                        basicParametr.ForeignStudents = Convert.ToInt32(strArrf[6]);
                        kPiDataContext.BasicParametersTable.InsertOnSubmit(basicParametr);

                        /*BasicParametrAdditional basicParamAdd = new BasicParametrAdditional();
                        basicParamAdd.BasicParametrAdditionalID = basicParametr.BasicParametersTableID;
                        basicParamAdd.Active = true;
                        basicParamAdd.ForForeignStudents = (Convert.ToInt32(strArrf[5])>0)?true:false;
                        */                     
                    }
                }
                kPiDataContext.SubmitChanges();
            }
            
        }
    }
}