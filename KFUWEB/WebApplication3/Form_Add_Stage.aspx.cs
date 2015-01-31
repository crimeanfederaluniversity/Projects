using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication3
{
    public partial class WebForm2 : System.Web.UI.Page
    {

        private int id;
        List<First_stage> listFirstStages = DataBaseCommunicator.GetFirst_stageTable();
        List<Second_stage> listSecondStages;
        List<Third_stage> listThirdStages;
        
        
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                foreach (First_stage FS in listFirstStages)
                {
                    DropDownList1.Items.Add(FS.name);
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownList2.Items.Clear();
            DropDownList3.Items.Clear();
            DropDownList4.Items.Clear();

            listSecondStages = DataBaseCommunicator.GetSecond_stageTable(listFirstStages[DropDownList1.SelectedIndex].id_first_stage);

            foreach (Second_stage SS in listSecondStages)
            {
                DropDownList2.Items.Add(SS.name);
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
           
        }

        protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
         
            DropDownList3.Items.Clear();
            DropDownList4.Items.Clear();

            listSecondStages = DataBaseCommunicator.GetSecond_stageTable(listFirstStages[DropDownList1.SelectedIndex].id_first_stage);
            listThirdStages = DataBaseCommunicator.GetThird_stageTable(listSecondStages[DropDownList2.SelectedIndex].id_second_stage);

            foreach (Third_stage TS in listThirdStages)
            {
                DropDownList3.Items.Add(TS.name);
            }
          
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Sql_Insert sqlInsert;

        }
    }
}