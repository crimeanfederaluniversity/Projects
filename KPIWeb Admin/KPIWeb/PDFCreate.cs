using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

using System.Configuration;
using System.Net;
using System.Data;
using System.Collections.Specialized;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.WebPages;
using iTextSharp.text;
using iTextSharp.text.pdf;
//using Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;
using Label = System.Web.UI.WebControls.Label;
using Page = System.Web.UI.Page;
using TextBox = System.Web.UI.WebControls.TextBox;


namespace KPIWeb
{
    public class PDFCreate
    {
        public static string ExportPDF(GridView GridToExport, int[] WidthArray, string Header, int CurrentPageSize, int ColumnCount, string ReportName, string UserPositionName, string UserStructName)
        {
            GridToExport.AllowPaging = false;
            // GridToExport.DataBind();
            BaseFont bf = BaseFont.CreateFont(Environment.GetEnvironmentVariable("windir") + @"\fonts\Arial.TTF",
                BaseFont.IDENTITY_H, true);
            iTextSharp.text.pdf.PdfPTable table = new iTextSharp.text.pdf.PdfPTable(ColumnCount + 2);
            int[] widths = new int[ColumnCount + 2];

            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 7, iTextSharp.text.Font.NORMAL);
            font.Color = new BaseColor(0, 0, 0);
            ///////
            widths[0] = WidthArray[0];
            string IdText = System.Web.HttpContext.Current.Server.HtmlDecode(GridToExport.HeaderRow.Cells[0].Text);
            iTextSharp.text.pdf.PdfPCell HeaderIdcell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, IdText, font));
            HeaderIdcell.BackgroundColor = new BaseColor(200, 200, 200);
            table.AddCell(HeaderIdcell);
            ///////
            widths[1] = WidthArray[2];
            string NameHeaderText = System.Web.HttpContext.Current.Server.HtmlDecode(GridToExport.HeaderRow.Cells[2].Text);
            iTextSharp.text.pdf.PdfPCell NameCell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, NameHeaderText, font));
            NameCell.BackgroundColor = new BaseColor(200, 200, 200);
            table.AddCell(NameCell);

            for (int x = 0; x < ColumnCount; x++)
            {
                widths[x + 2] = WidthArray[x + 4];
                string cellText = System.Web.HttpContext.Current.Server.HtmlDecode(GridToExport.HeaderRow.Cells[x + 4].Text);
                // iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                font.Color = new BaseColor(0, 0, 0);
                iTextSharp.text.pdf.PdfPCell cell = new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                cell.BackgroundColor = new BaseColor(200, 200, 200);
                table.AddCell(cell);
            }
            table.SetWidths(widths);
            for (int i = 0; i < GridToExport.Rows.Count; i++)
            {
                if (GridToExport.Rows[i].RowType == DataControlRowType.DataRow)
                {
                    //iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);
                    //font.Color = new BaseColor(0, 0, 0);

                    ///////////// PARAM ID
                    iTextSharp.text.pdf.PdfPCell IDcell =
                        new iTextSharp.text.pdf.PdfPCell(new Phrase(12, System.Web.HttpContext.Current.Server.HtmlDecode(GridToExport.Rows[i].Cells[0].Text), font));
                    if (i % 2 == 0)
                    {
                        IDcell.BackgroundColor = new BaseColor(230, 230, 230);
                    }
                    table.AddCell(IDcell);
                    /////////////
                    ///////////// PARAM NAME
                    Label NameTextBox = (Label)GridToExport.Rows[i].FindControl("Name");
                    iTextSharp.text.pdf.PdfPCell Namecell =
                        new iTextSharp.text.pdf.PdfPCell(new Phrase(12, System.Web.HttpContext.Current.Server.HtmlDecode(NameTextBox.Text), font));
                    if (i % 2 == 0)
                    {
                        Namecell.BackgroundColor = new BaseColor(230, 230, 230);
                    }
                    table.AddCell(Namecell);
                    /////////////

                    for (int j = 0; j < ColumnCount; j++)
                    {
                        TextBox textBox = (TextBox)GridToExport.Rows[i].FindControl("Value" + j.ToString());
                        string cellText = System.Web.HttpContext.Current.Server.HtmlDecode(textBox.Text);
                        iTextSharp.text.pdf.PdfPCell cell =
                            new iTextSharp.text.pdf.PdfPCell(new Phrase(12, cellText, font));
                        if (i % 2 == 0)
                        {
                            cell.BackgroundColor = new BaseColor(230, 230, 230);
                        }
                        table.AddCell(cell);
                    }
                }
            }
            string DocPath = "";
            Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 20f, 20f);
            using (MemoryStream myMemoryStream = new MemoryStream())
            {
                PdfWriter.GetInstance(pdfDoc, myMemoryStream);
                Chunk c1 = new Chunk(Header);
                pdfDoc.Open();

                //////////////////////////////////////////

                iTextSharp.text.Font TitleFont = new iTextSharp.text.Font(bf, 12, iTextSharp.text.Font.NORMAL);

                Paragraph para = new Paragraph(ReportName, TitleFont);//название отчета
                para.Alignment = Element.ALIGN_CENTER;
                pdfDoc.Add(para);

                para = new Paragraph(UserPositionName, TitleFont);//должность
                para.Alignment = Element.ALIGN_LEFT;
                pdfDoc.Add(para);

                para = new Paragraph(UserStructName, TitleFont);//кафедра
                para.Alignment = Element.ALIGN_LEFT;
                pdfDoc.Add(para);

                para = new Paragraph(" ");
                pdfDoc.Add(para);

                /////////////////////////////////////////
                table.WidthPercentage = 100;
                pdfDoc.Add(table);
                pdfDoc.Close();
                byte[] content = myMemoryStream.ToArray();
                string DocName = "Document" + DateTime.Now;
                DocName = DocName.Replace(":", "");
                DocName = DocName.Replace(".", "");
                DocName = DocName.Replace(" ", "");
                DocName += ".pdf";
                DocPath = System.Web.HttpContext.Current.Server.MapPath("~/PDFArchive/" + DocName);
                using (FileStream fs = File.Create(DocPath))
                {
                    fs.Write(content, 0, (int)content.Length);
                }
            }
            return DocPath;
        }

        public static string StructLastName(int UserID)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            UsersTable userTable =
                      (from a in kPiDataContext.UsersTable where a.UsersTableID == UserID select a).FirstOrDefault();
            int deepness = 5;
            if (userTable.FK_FifthLevelSubdivisionTable == null)
            {
                deepness = 4;
            }
            if (userTable.FK_FourthLevelSubdivisionTable == null)
            {
                deepness = 3;
            }
            if (userTable.FK_ThirdLevelSubdivisionTable == null)
            {
                deepness = 2;
            }
            if (userTable.FK_SecondLevelSubdivisionTable == null)
            {
                deepness = 1;
            }
            if (userTable.FK_FirstLevelSubdivisionTable == null)
            {
                deepness = 0;
            }
            switch (deepness)
            {
                case 0:
                    {
                        return (from a in kPiDataContext.ZeroLevelSubdivisionTable
                                where
                                    a.ZeroLevelSubdivisionTableID == userTable.FK_ZeroLevelSubdivisionTable
                                select a.Name).FirstOrDefault();
                        break;
                    }
                case 1:
                    {
                        return (from a in kPiDataContext.FirstLevelSubdivisionTable
                                where
                                    a.FirstLevelSubdivisionTableID == userTable.FK_FirstLevelSubdivisionTable
                                select a.Name).FirstOrDefault();
                        break;
                    }
                case 2:
                    {
                        return (from a in kPiDataContext.SecondLevelSubdivisionTable
                                where
                                    a.SecondLevelSubdivisionTableID == userTable.FK_SecondLevelSubdivisionTable
                                select a.Name).FirstOrDefault();
                        break;
                    }
                case 3:
                    {
                        return (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                where
                                    a.ThirdLevelSubdivisionTableID == userTable.FK_ThirdLevelSubdivisionTable
                                select a.Name).FirstOrDefault();
                        break;
                    }
                case 4:
                    {
                        return (from a in kPiDataContext.FourthLevelSubdivisionTable
                                where
                                    a.FourthLevelSubdivisionTableID == userTable.FK_FourthLevelSubdivisionTable
                                select a.Name).FirstOrDefault();
                        break;
                    }
                default:
                    {
                        return "";
                        break;
                    }
            }
        }
    }
}