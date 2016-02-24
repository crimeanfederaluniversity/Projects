using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Providers.Entities;
using System.Web.UI.WebControls;

namespace Chancelerry
{
    public class TableActions
    {
        private List<string> strList;
        private List<int> intList;
        private List<DateTime> dateList;
        private List<float> floatList;

        private void RedirectToEdit(object sender, EventArgs e)
        {
            
            ImageButton thisButton = (ImageButton)sender;
            int currentCardId = Convert.ToInt32(thisButton.Attributes["_cardID"]);
            HttpContext.Current.Session["cardID"] = currentCardId; 
            HttpContext.Current.Session["version"] = 100500;
            HttpContext.Current.Session["canEdit"] = true;
            HttpContext.Current.Response.Redirect("~/kanz/CardEdit.aspx", true);
        }

        private void RedirectToView(object sender, EventArgs e)
        {
            ImageButton thisButton = (ImageButton)sender;
            int currentCardId = Convert.ToInt32(thisButton.Attributes["_cardID"]);
            HttpContext.Current.Session["cardID"] = currentCardId;
            HttpContext.Current.Session["version"] = 100500;
            HttpContext.Current.Session["canEdit"] = false;
            HttpContext.Current.Response.Redirect("~/kanz/CardEdit.aspx", true);
        }

        public TableRow AddRowFromList(List<string> list, int cardID)
        {
            TableRow row = new TableRow();
            row.BorderStyle = BorderStyle.Solid;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                cel.Text = elm;
                row.Cells.Add(cel);
            }

            TableCell cell = new TableCell();

            ImageButton buttonEdit = new ImageButton();
            buttonEdit.ImageUrl= "http://img.stockfresh.com/files/d/dvarg/x/61/2294589_46721166.jpg";
            buttonEdit.Attributes.Add("_cardID", cardID.ToString());
            buttonEdit.Click += RedirectToEdit;

            ImageButton buttonView = new ImageButton();
            buttonView.ImageUrl = "http://www.iggintel.com/site/iguard_global_intelligence_inc/assets/images/icon-observer-v2.png";
            buttonView.Attributes.Add("_cardID", cardID.ToString());
            buttonView.Click += RedirectToView;


            row.Cells.Add(cell);

            cell.Controls.Add(buttonEdit);
            cell.Controls.Add(buttonView);

            row.Controls.Add(cell);

            return row;
        }
        public TableRow AddRowFromList(List<int> list)
        {
            TableRow row = new TableRow();
            row.BorderStyle = BorderStyle.Solid;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                var asd = elm;
                cel.Text = elm.ToString();
                row.Cells.Add(cel);
            }

            return row;
        }
        public TableRow AddRowFromList(List<DateTime> list)
        {
            TableRow row = new TableRow();
            row.BorderStyle = BorderStyle.Solid;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                var asd = elm;
                cel.Text = elm.ToShortDateString();
                row.Cells.Add(cel);
            }

            return row;
        }
        public TableRow AddRowFromList(List<float> list)
        {
            TableRow row = new TableRow();
            row.BorderStyle = BorderStyle.Solid;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                var asd = elm;
                cel.Text = elm.ToString();
                row.Cells.Add(cel);
            }

            return row;
        }

        public TableHeaderRow AddHeaderRoFromList(List<string> list)
        {
            TableHeaderRow row = new TableHeaderRow();
            row.BackColor = Color.Aqua;
            row.BorderStyle = BorderStyle.Inset;

            foreach (var elm in list)
            {
                TableCell cel = new TableCell();
                cel.BorderStyle = BorderStyle.Solid;
                var asd = elm;
                cel.Text = elm;
                row.Cells.Add(cel);
            }

            return row;
        }


    }
}