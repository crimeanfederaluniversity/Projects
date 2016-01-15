using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.Rector
{
    public class TreeViewTable
    {
        private int columncount = 5;
        public string CreateTreeViewTable( )
        {
            List<string> tmpList = new List<string>();
            tmpList.Add("title1");
            tmpList.Add("title2");
            tmpList.Add("title3");
            tmpList.Add("title4");
            tmpList.Add("title5");

            string tmp="";
            tmp += GetTreeViewStart();
            tmp += GetTreeViewTitle(tmpList);

            tmp += GetTreeViewEnd();
            return tmp;
        }

        public string GetTreeViewTitle(List<string> titleNames)
        {
            string tmpstr = "<tr> <th>";

            for (int i = 0; i < columncount; i++)
            {
                tmpstr += titleNames[i];
                if (i <( columncount-1))
                tmpstr += "</th> <th>";
            }

            tmpstr += "</th></tr>";

            return tmpstr;
        }

        public string GetTreeViewStart()
        {
            return "<table>";
        }

        public string GetTreeViewEnd()
        {
            return "</table>";
        }
    }

    public class TreeViewData
    {
        private string treeViewIndex;
        private string treeViewName;
        private string treeViewDataValue1;
        private string treeViewDataValue2;
        private string treeViewDataValue3;
        private string treeViewDataValue4;
        private string treeViewDataValue5;
        private List<TreeViewData> treeViewDataChildren;
    }
    
    public class ArrayRawDataParser
    {
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


      
        public string GetData(string fileName)
        {
            string fileContent = getFileContent (fileName);
            List<string> lines = fileContent.Split('@').ToList();
            List<List<string>> cells = new List<List<string>>();
            foreach (string line in lines)
            {
                List<string> tmplList = line.Split('#').ToList();
                cells.Add(clearList(tmplList));
            }







            string tmpstr = "";
            foreach (List<string> line in cells)
            {
                foreach (string cell in line)
                {
                    tmpstr += cell + "|||";
                }
                tmpstr += "<br />";
            }
            return tmpstr;
        }


    }
    public partial class ViewStruct : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ArrayRawDataParser arayParser = new ArrayRawDataParser();
            Label1.Text = arayParser.GetData("dataarray.txt");
        }


       
    }
}