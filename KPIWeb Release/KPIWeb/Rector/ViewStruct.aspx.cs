using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Owin.Security.Provider;

namespace KPIWeb.Rector
{
    public class TreeViewTable
    {
        private int columncount = 11;
        private string tableNameTemplate = "tableTemplate";
        private int tableNameId= 0;
        private string CreateLine(int nameColumnCount, int deepness, TreeViewData treeViewData,string childTableId)
        { 
            string tmpstr = "";
            bool collspanstarted = false;
            for (int i = 0; i < nameColumnCount; i++)
                {
                   
                    if (i == deepness-1)
                    {
                        tmpstr += "<td colspan=" + (nameColumnCount-deepness+1).ToString() + ">";
                        collspanstarted = true;
                        if (treeViewData.treeViewDataChildren.Any())
                        {
                            tmpstr +=
                                "<label><input type=\"checkbox\" name=\"a1\"><a onclick=\"showChildren('" + childTableId + "')\"><input type=\"checkbox\"></a>" + treeViewData.treeViewName + "</label> ";
                        }
                        else
                        {
                            tmpstr += treeViewData.treeViewName;
                        }
                        tmpstr += "</td>";
                    }
                    else if (!collspanstarted)
                    {
                        tmpstr += "<td> </td>";
                    }
                    
                }
            tmpstr += "<td>" + treeViewData.treeViewDataValue1.Replace(" ","") + "</td>";
            tmpstr += "<td>" + treeViewData.treeViewDataValue2.Replace(" ", "") + "</td>";
            tmpstr += "<td>" + treeViewData.treeViewDataValue3.Replace(" ", "") + "</td>";
            tmpstr += "<td>" + treeViewData.treeViewDataValue4.Replace(" ", "") + "</td>";
            tmpstr += "<td>" + treeViewData.treeViewDataValue5.Replace(" ", "") + "</td>";
            return tmpstr;
        }

        private string RecursiveStringCreate(TreeViewData treeViewData,int deepness, int columnCount)
        {
            string thisTableName = tableNameTemplate + tableNameId.ToString();
            tableNameId++;
            string tmpstr = "";
            if (deepness != 0)
            {
                tmpstr += "<tr>";
                tmpstr += CreateLine(columnCount, deepness, treeViewData, thisTableName);
                tmpstr += "</tr>";
            }
            if (treeViewData.treeViewDataChildren.Any())
            {
                if (deepness != 0)
                    tmpstr += "	<td colspan=" + columncount + " class=\"node\"><input type=\"checkbox\"> <table id = '" + thisTableName + "'>";

                tmpstr += "<tr style=\"height:0;\">";
                for (int i = 0; i < columncount; i++)
                {
                    tmpstr += "<td></td>";
                }
                tmpstr += "</tr>";
                foreach (TreeViewData child in treeViewData.treeViewDataChildren)
                {
                    tmpstr += RecursiveStringCreate(child, deepness + 1, columnCount);
                }
                if (deepness != 0)
                tmpstr += "</table> </td>";
            }
            
            return tmpstr;
        }
        public string CreateTreeViewTable(TreeViewData treeViewData )
        {
            List<string> tmpList = new List<string>();
            tmpList.Add(" ");
            tmpList.Add(" ");
            tmpList.Add(" ");
            tmpList.Add(" ");
            tmpList.Add(" ");
            tmpList.Add(" ");

            tmpList.Add("Кол-во штатных единиц");
            tmpList.Add("Из них занято");
            tmpList.Add("Должностной оклад");
            tmpList.Add("Месячный фонд");
            tmpList.Add("Годовой фонд");
            string tmp="";
            tmp += GetTreeViewStart();
            tmp += GetTreeViewTitle(tmpList);
            tmp += "<tbody>";

           
            tmp += RecursiveStringCreate(treeViewData, 0, 6);


            tmp += "</tbody>";
            tmp += GetTreeViewEnd();
            return tmp;
        }
        private string GetTreeViewTitle(List<string> titleNames)
        {

            string tmpstr = " <thead> <tr> <th>";

            for (int i = 0; i < columncount; i++)
            {
                tmpstr += titleNames[i];
                if (i <( columncount-1))
                tmpstr += "</th> <th>";
            }

            tmpstr += "</th></tr> </thead>";

            return tmpstr;
        }
        private string GetTreeViewStart()
        {
            return "<table>";
        }
        private string GetTreeViewEnd()
        {
            return "</table>";
        }
    }
    public class TreeViewData
    {
        public string treeViewIndex;
        public string treeViewName;
        public string treeViewDataValue1;
        public string treeViewDataValue2;
        public string treeViewDataValue3;
        public string treeViewDataValue4;
        public string treeViewDataValue5;
        public string treeViewDataValue6;
        public List<TreeViewData> treeViewDataChildren;
    }   
    public class ArrayRawDataParser
    {
        private List<List<string>> cellsList = new List<List<string>>();
        private int currentCellId = 0;
        private List<string> clearList(List<string> incomingList)
        {
            List<string> tmpList = new List<string>();
            foreach (string cuurentCellValue in incomingList)
            {
                string tmpValue = cuurentCellValue;
                tmpValue.Trim();
                tmpList.Add(tmpValue);
            }
            return tmpList;
        }
        private string getFileContent(string fileName)
        {
            string tmpaddr = System.Web.HttpContext.Current.Server.MapPath(fileName);
            return File.ReadAllText(tmpaddr);
        }
        private int DotCount(string str)
        {
            return str.Split(new string[] { "." }, StringSplitOptions.None).Length - 1;
        }
        private TreeViewData GetCreatedTreeViewData()
        {
            TreeViewData tmpTreeViewDate = new TreeViewData();
            List<string> currentList = cellsList[currentCellId];
            tmpTreeViewDate.treeViewDataChildren = new List<TreeViewData>();
            tmpTreeViewDate.treeViewName = currentList[1];
            tmpTreeViewDate.treeViewDataValue1 = currentList[2];
            tmpTreeViewDate.treeViewDataValue2 = currentList[3];
            tmpTreeViewDate.treeViewDataValue3 = currentList[4];
            tmpTreeViewDate.treeViewDataValue4 = currentList[5];
            tmpTreeViewDate.treeViewDataValue5 = currentList[6];

            int currentCellDotCnt = DotCount(currentList[0]);
            currentCellId++;

            if (currentCellId < cellsList.Count - 1)
            {
                int nextCellDotCnt = DotCount(cellsList[currentCellId][0]);
                while (nextCellDotCnt > currentCellDotCnt)
                {
                    tmpTreeViewDate.treeViewDataChildren.Add(GetCreatedTreeViewData());
                    nextCellDotCnt = DotCount(cellsList[currentCellId][0]);
                }
            }
            return tmpTreeViewDate;
        }
        public TreeViewData GetTreeViewData(string fileName)
        {
            string fileContent = getFileContent (fileName);
            List<string> lines = fileContent.Split('@').ToList();
            List<List<string>> cells = new List<List<string>>();
            foreach (string line in lines)
            {
                List<string> tmplList = line.Split('#').ToList();
                cells.Add(clearList(tmplList));
            }
            cellsList = cells;
            currentCellId = 0;
            TreeViewData newTreeViewData = GetCreatedTreeViewData();
            return newTreeViewData;
        }
    }
    public partial class ViewStruct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayRawDataParser arayParser = new ArrayRawDataParser();
            TreeViewData treeViewData = arayParser.GetTreeViewData("dataarray.txt");
            TreeViewTable treeTableCreator  = new TreeViewTable();
            Label1.Text = treeTableCreator.CreateTreeViewTable(treeViewData);
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }      
    }
}