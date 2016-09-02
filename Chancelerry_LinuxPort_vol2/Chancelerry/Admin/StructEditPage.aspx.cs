using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.Admin
{
   
    public class TreeViewTable
    {
        private int columncount = 7;
        private string tableNameTemplate = "tableTemplate";
        private int tableNameId = 0;
  
        TextBox nameTextBox = new TextBox() { ID = "nameTextBox0"};
        

        ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
        public void addNewChildButton_Clicked(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;         
            Struct newstructchild = new Struct();
            newstructchild.Active = true;
            newstructchild.FkParent = Convert.ToInt32(button.CommandArgument);
            newstructchild.Name = nameTextBox.Text;
            chancDb.Struct.InsertOnSubmit(newstructchild);
            chancDb.SubmitChanges();
            HttpContext.Current.Response.Redirect("~/Admin/StructEditPage.aspx");       
        }
        public void RecursiveDeleteChild (int  current)
        {
            Struct deletestruct = (from a in chancDb.Struct where a.Active == true && a.STRuCtID == current select a).FirstOrDefault();
            List<Struct> children = (from b in chancDb.Struct where b.Active == true && b.FkParent ==  current  select b).ToList();
            deletestruct.Active = false;
            chancDb.SubmitChanges();
            foreach (Struct child in children)
                RecursiveDeleteChild(child.STRuCtID);          
        }
        public void delChildButton_Clicked(object sender, EventArgs e)
        {
            LinkButton button = (LinkButton)sender;
            int id = Convert.ToInt32(button.CommandArgument);
            RecursiveDeleteChild(id);       
            HttpContext.Current.Response.Redirect("~/Admin/StructEditPage.aspx");
        }
        public Table GetTable(TreeViewData current , int deepness)
        {
            Table tableToReturn = new Table();
            TableRow newRow = new TableRow();
            TableCell nameCell = new TableCell();
            /*CheckBox checkBox = new CheckBox();
            checkBox.Text = current.treeViewName;
            checkBox.ID = "checkBox" + current.treeViewIndex;
            nameCell.Controls.Add(checkBox);*/
            nameCell.Text = current.treeViewName;
            TableCell controlCell = new TableCell();
            LinkButton addNewChildButton = new LinkButton();
            addNewChildButton.Text = "Добавить";   
            addNewChildButton.CommandArgument = current.treeViewIndex.ToString();                        
            addNewChildButton.Click += addNewChildButton_Clicked;            
            controlCell.Controls.Add(addNewChildButton);

            if (deepness != 0)
            {
                LinkButton delChildButton = new LinkButton();
                delChildButton.Text = "Удалить";
                delChildButton.CommandArgument = current.treeViewIndex.ToString();
                delChildButton.OnClientClick = "return confirm('Вы уверены что хотите удалить структурное подразделение? Внимание! Все структурные подразделения входящие в состав удаляемого также будут удалены!');";

                delChildButton.Click += delChildButton_Clicked;
                controlCell.Controls.Add(delChildButton);
            }
            newRow.Cells.Add(nameCell);
            newRow.Cells.Add(controlCell);

            tableToReturn.Rows.Add(newRow);

            foreach (TreeViewData child in current.treeViewDataChildren)
            {
                TableRow childRow = new TableRow();
                TableCell childCell = new TableCell();
                childCell.Controls.Add(GetTable(child, deepness + 1));
                childRow.Cells.Add(childCell);
                tableToReturn.Rows.Add(childRow);
            }

            return tableToReturn;
        }

        public Panel GetPanelWithTable(TreeViewData treeViewData)
        {
            Panel panelToReturn = new Panel();
            nameTextBox.Attributes.Add("placeholder","Введите название нового структурного подразделения");
            panelToReturn.Controls.Add(nameTextBox);
            panelToReturn.Controls.Add(GetTable(treeViewData, 0));
            return panelToReturn;
        }

        private string CreateLine(int nameColumnCount, int deepness, TreeViewData treeViewData, string childTableId)
        {
            string tmpstr = "";
            bool collspanstarted = false;
            for (int i = 0; i < nameColumnCount; i++)
            {

                if (i == deepness - 1)
                {
                    tmpstr += "<td colspan=" + (nameColumnCount - deepness + 1).ToString() + ">";
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
            return tmpstr;
        }

        private string RecursiveStringCreate(TreeViewData treeViewData, int deepness, int columnCount)
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
        public string CreateTreeViewTable(TreeViewData treeViewData)
        {
            string tmp = "<table > <tbody>";
            tmp += RecursiveStringCreate(treeViewData, 0, 6);
            tmp += "</tbody></table>";
            return tmp;
        }
    }
    
    public class TreeViewData
    {
        public int treeViewIndex;
        public string treeViewName;
        public List<TreeViewData> treeViewDataChildren;
    }
    public partial class StructEditPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }


            CreateTree();
        }

        public TreeViewData RecursiveFillTreeView (List<Struct> allStruct, Struct current)
        {
            TreeViewData currentData = new TreeViewData();
            currentData.treeViewIndex = current.STRuCtID;
            currentData.treeViewName = current.Name;

            List<Struct> children = (from a in allStruct
                                     where a.FkParent == current.STRuCtID
                                     select a).ToList();
            currentData.treeViewDataChildren = new List<TreeViewData>();
            if (children.Count>0)
            {
                
                foreach(Struct child in children)
                {
                    currentData.treeViewDataChildren.Add(RecursiveFillTreeView(allStruct, child));
                }
            }
            return currentData;
        }

        public void CreateTree()
        {                 
            ChancelerryDb chancDb = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"]));
            TreeViewData treeViewData = new TreeViewData();
            Struct first = (from a in chancDb.Struct where a.Active == true && a.FkParent == null select a).FirstOrDefault();
            List<Struct> allStruct = (from a in chancDb.Struct where a.Active == true && a.FkParent != null select a).ToList();
            treeViewData = RecursiveFillTreeView(allStruct, first);
            TreeViewTable treeViewTable = new TreeViewTable();
            //mainDiv.InnerHtml=treeViewTable.CreateTreeViewTable(treeViewData);
            mainDiv.Controls.Add(treeViewTable.GetPanelWithTable(treeViewData));
        }
    }
}