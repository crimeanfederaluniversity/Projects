using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;



namespace Competition
{
    public partial class SmetaForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                     
            if (!IsPostBack)
            {
                GridviewApdate();
            }
        }

        private void GridviewApdate() //Обновление гридвью
        {
            CompetitionDBDataContext newSmeta = new CompetitionDBDataContext();
            int idkon = (int)Session["ID_Konkurs"];
            int idbid = (int)Session["ID_Bid"];

            DataTable SmetaData = new DataTable();
            SmetaData.Columns.Add(new DataColumn("ID_Value", typeof(string)));
            SmetaData.Columns.Add(new DataColumn("Value", typeof(double)));
            SmetaData.Columns.Add(new DataColumn("Name_state", typeof(string)));

            List<Smeta> Smetas = (from a in newSmeta.Smeta
                                  where
                                      a.FK_Konkurs == idkon &&
                                      a.Active == true
                                  select a).ToList();

            foreach (Smeta n in Smetas)
            {
                SmetaValue ConstValue = (from a in newSmeta.SmetaValue
                                         where a.FK_Bid == idbid
                                         && a.Active == true
                                         && a.FK_State == n.ID_State
                                         select a).FirstOrDefault();
                if (ConstValue == null)
                {
                    ConstValue = new SmetaValue();
                    ConstValue.Active = true;
                    ConstValue.FK_Bid = idbid;
                    ConstValue.FK_State = n.ID_State;
                    newSmeta.SmetaValue.InsertOnSubmit(ConstValue);
                    newSmeta.SubmitChanges();
                }

                DataRow dataRow = SmetaData.NewRow();
                dataRow["ID_Value"] = n.ID_State;
                SmetaValue Val = (from a in newSmeta.SmetaValue
                                  join b in newSmeta.Smeta on a.FK_State equals n.ID_State
                                  where a.FK_State == n.ID_State
                                  select a).FirstOrDefault();
                if (Val.Value != null)
                {
                    dataRow["Value"] = (from a in newSmeta.SmetaValue
                                        join b in newSmeta.Smeta
                                        on a.FK_State equals n.ID_State
                                        where
                                            a.FK_State == n.ID_State
                                        select a.Value).FirstOrDefault();
                }
                dataRow["Name_state"] = n.Name_state;
                SmetaData.Rows.Add(dataRow);
            }
            GridView1.DataSource = SmetaData;
            GridView1.DataBind();
        }
       
        protected void Button1_Click(object sender, EventArgs e) // сохранение данных из гридвью в базу
        {
            CompetitionDBDataContext newSmeta = new CompetitionDBDataContext();
            SmetaValue SmetaValues = new SmetaValue();
            DataTable SmetaNames = (DataTable)ViewState["Name_state"];
            DataTable ValueOfSmeta = (DataTable)ViewState["Value"];
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                TextBox Name_Stater = (TextBox)GridView1.Rows[i].FindControl("Name_state");
                TextBox Value_grid = (TextBox)GridView1.Rows[i].FindControl("Value");
                Label Id_label = (Label)GridView1.Rows[i].FindControl("ID_Value");
                Smeta NewSmetValue = (from a in  newSmeta.Smeta
                                      join b in newSmeta.SmetaValue on a.ID_State equals b.FK_State
                                      where b.FK_State == Convert.ToInt32(Id_label.Text)
                                            && a.Active == true
                                      select a).FirstOrDefault();
                SmetaValue Smetaval =
                                (from a in newSmeta.SmetaValue
                                 join b in newSmeta.Smeta on a.FK_State equals b.ID_State
                                 where a.FK_State == Convert.ToInt32(Id_label.Text)
                                       && a.Active==true
                                 select a).FirstOrDefault();
                NewSmetValue.Name_state = Name_Stater.Text;
                Smetaval.FK_State = Convert.ToInt32(Id_label.Text);
                Smetaval.Value = Convert.ToDouble(Value_grid.Text);
                newSmeta.SubmitChanges();               
            }
        }

        protected void Button2_Click(object sender, EventArgs e) //добавление новой строки в гридвью
        {
            int idkon = (int)Session["ID_Konkurs"];
            int idbid = (int)Session["ID_Bid"];
            CompetitionDBDataContext newSmeta = new CompetitionDBDataContext();
            SmetaValue NewIndex=new SmetaValue();
            Smeta NewSmetaName = new Smeta();
            NewSmetaName.Active = true;
            NewSmetaName.FK_Konkurs = idkon;
            NewSmetaName.Type_state = true;
            NewSmetaName.Uniqvalue = "#5";
            NewSmetaName.Name_state = null;
            newSmeta.Smeta.InsertOnSubmit(NewSmetaName);
            newSmeta.SubmitChanges();

            NewIndex.Active = true;
            NewIndex.FK_Bid = idbid;
            NewIndex.FK_State = NewSmetaName.ID_State;        
           newSmeta.SmetaValue.InsertOnSubmit(NewIndex);
           newSmeta.SubmitChanges();

            GridviewApdate();
        }

        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/TeamUsers.aspx");
        }

    
    }
}