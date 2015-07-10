using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace Competition
{
    public partial class Userpage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                
                CompetitionDBDataContext newCompetition = new CompetitionDBDataContext();
                DataTable dataTableBid = new DataTable();

                dataTableBid.Columns.Add(new DataColumn("ID_Konkurs", typeof(int)));
                dataTableBid.Columns.Add(new DataColumn("Number", typeof(string)));
                dataTableBid.Columns.Add(new DataColumn("Name", typeof(string)));
                dataTableBid.Columns.Add(new DataColumn("StartDate", typeof(string)));
                dataTableBid.Columns.Add(new DataColumn("EndDate", typeof(string)));
                 

                List<Konkursy> konkurs = (from a in newCompetition.Konkursy where a.Active == true select a).ToList();
                foreach (Konkursy n in konkurs)
                {
                    DataRow dataRow = dataTableBid.NewRow();

                    dataRow["ID_Konkurs"] = n.ID_Konkurs;
                    dataRow["Number"] = n.Number;
                    dataRow["Name"] = n.Name;
                    dataRow["StartDate"] = n.StartDate;
                    dataRow["EndDate"] = n.EndDate;
                   
                }
                    GridView1.DataSource = konkurs;
                    GridView1.DataBind();
                    //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "Script", "alert('Конкурсы');", true);         
        }

        protected void Bid_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            {
                using (CompetitionDBDataContext newBid = new CompetitionDBDataContext())
                {
                    Konkursy id = (from a in newBid.Konkursy
                                   where a.ID_Konkurs == Convert.ToInt32(button.CommandArgument)
                                   select a).FirstOrDefault();

                    Session["ID_Konkurs"] = id.ID_Konkurs;

                    Bids newbid = new Bids();
                    newbid.Active = true;
                    newbid.FK_Konkurs = id.ID_Konkurs;
                    newbid.Date = DateTime.Now;
                    newbid.Status = "В процессе заполнения";

                    newBid.Bids.InsertOnSubmit(newbid);
                    newBid.SubmitChanges();

                    Session["ID_Bid"] = newbid.ID_Bid;
                    int iduser = (int)Session["ID_User"];
                    
                    User_BidMapingTable newlink = new User_BidMapingTable();
                    newlink.Active = true;
                    newlink.FK_Bid = newbid.ID_Bid;
                    newlink.FK_User = iduser;

                    newBid.User_BidMapingTable.InsertOnSubmit(newlink);
                    newBid.SubmitChanges();
                    Response.Redirect("~/ZapolnenieForm.aspx");
                }
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
     
            Response.Redirect("~/UserBid.aspx");
        }
    }
}