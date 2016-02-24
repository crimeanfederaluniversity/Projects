using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Chancelerry
{
    public class TableActions
    {
        private List<string> strList;
        private List<int> intList;
        private List<DateTime> dateList;
        private List<float> floatList;


        public TableRow AddRowFromList(List<string> list)
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