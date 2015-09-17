using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace Competitions
{
    public class XmlTableCreate
    {
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        private XmlNode CreateTableNodeWithHeader(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tbl", "w");
            newNode.AppendChild(CreateTableParamNode(document));
            newNode.AppendChild(CreateTableGrid(document));
            return newNode;
        }
        private XmlNode CreateTableParamNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tblPr", "w");
            newNode.AppendChild(CreateTableParamWidthNode(document));
            newNode.AppendChild(CreateTableParamBorderNode(document));
            newNode.AppendChild(CreateTableParamLook(document));
            return newNode;
        }
        private XmlNode CreateTableParamWidthNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tblW", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("w:w","w");
            newAttribute0.Value = "0";
            XmlAttribute newAttribute1 = document.CreateAttribute("w:type", "w");
            newAttribute1.Value = "auto";
            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            return newNode;
        }
        private XmlNode CreateTableParamBorderNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tblBorders", "w");

            newNode.AppendChild(CreateTableParamBorderTopNode(document));
            newNode.AppendChild(CreateTableParamBorderLeftNode(document));
            newNode.AppendChild(CreateTableParamBorderBottomNode(document));
            newNode.AppendChild(CreateTableParamBorderRightNode(document));
            newNode.AppendChild(CreateTableParamBorderInsideHNode(document));
            newNode.AppendChild(CreateTableParamBorderInsideVNode(document));

            return newNode;
        }
        private XmlNode CreateTableParamBorderTopNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:top", "w");

            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "single";
            XmlAttribute newAttribute1 = document.CreateAttribute("w:sz", "w");
            newAttribute1.Value = "4";
            XmlAttribute newAttribute2 = document.CreateAttribute("wx:bdrwidth", "wx");
            newAttribute2.Value = "10";
            XmlAttribute newAttribute3 = document.CreateAttribute("w:space", "w");
            newAttribute3.Value = "0";
            XmlAttribute newAttribute4 = document.CreateAttribute("w:color", "w");
            newAttribute4.Value = "auto";

            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            newNode.Attributes.Append(newAttribute2);
            newNode.Attributes.Append(newAttribute3);
            newNode.Attributes.Append(newAttribute4);

            return newNode;
        }
        private XmlNode CreateTableParamBorderLeftNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:left", "w");

            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "single";
            XmlAttribute newAttribute1 = document.CreateAttribute("w:sz", "w");
            newAttribute1.Value = "4";
            XmlAttribute newAttribute2 = document.CreateAttribute("wx:bdrwidth", "wx");
            newAttribute2.Value = "10";
            XmlAttribute newAttribute3 = document.CreateAttribute("w:space", "w");
            newAttribute3.Value = "0";
            XmlAttribute newAttribute4 = document.CreateAttribute("w:color", "w");
            newAttribute4.Value = "auto";

            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            newNode.Attributes.Append(newAttribute2);
            newNode.Attributes.Append(newAttribute3);
            newNode.Attributes.Append(newAttribute4);

            return newNode;
        }
        private XmlNode CreateTableParamBorderBottomNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:bottom", "w");

            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "single";
            XmlAttribute newAttribute1 = document.CreateAttribute("w:sz", "w");
            newAttribute1.Value = "4";
            XmlAttribute newAttribute2 = document.CreateAttribute("wx:bdrwidth", "wx");
            newAttribute2.Value = "10";
            XmlAttribute newAttribute3 = document.CreateAttribute("w:space", "w");
            newAttribute3.Value = "0";
            XmlAttribute newAttribute4 = document.CreateAttribute("w:color", "w");
            newAttribute4.Value = "auto";

            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            newNode.Attributes.Append(newAttribute2);
            newNode.Attributes.Append(newAttribute3);
            newNode.Attributes.Append(newAttribute4);

            return newNode;
        }
        private XmlNode CreateTableParamBorderRightNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:right", "w");

            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "single";
            XmlAttribute newAttribute1 = document.CreateAttribute("w:sz", "w");
            newAttribute1.Value = "4";
            XmlAttribute newAttribute2 = document.CreateAttribute("wx:bdrwidth", "wx");
            newAttribute2.Value = "10";
            XmlAttribute newAttribute3 = document.CreateAttribute("w:space", "w");
            newAttribute3.Value = "0";
            XmlAttribute newAttribute4 = document.CreateAttribute("w:color", "w");
            newAttribute4.Value = "auto";

            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            newNode.Attributes.Append(newAttribute2);
            newNode.Attributes.Append(newAttribute3);
            newNode.Attributes.Append(newAttribute4);

            return newNode;
        }
        private XmlNode CreateTableParamBorderInsideHNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:insideH", "w");

            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "single";
            XmlAttribute newAttribute1 = document.CreateAttribute("w:sz", "w");
            newAttribute1.Value = "4";
            XmlAttribute newAttribute2 = document.CreateAttribute("wx:bdrwidth", "wx");
            newAttribute2.Value = "10";
            XmlAttribute newAttribute3 = document.CreateAttribute("w:space", "w");
            newAttribute3.Value = "0";
            XmlAttribute newAttribute4 = document.CreateAttribute("w:color", "w");
            newAttribute4.Value = "auto";

            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            newNode.Attributes.Append(newAttribute2);
            newNode.Attributes.Append(newAttribute3);
            newNode.Attributes.Append(newAttribute4);

            return newNode;
        }
        private XmlNode CreateTableParamBorderInsideVNode(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:insideV", "w");

            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "single";
            XmlAttribute newAttribute1 = document.CreateAttribute("w:sz", "w");
            newAttribute1.Value = "4";
            XmlAttribute newAttribute2 = document.CreateAttribute("wx:bdrwidth", "xw");
            newAttribute2.Value = "10";
            XmlAttribute newAttribute3 = document.CreateAttribute("w:space","w");
            newAttribute3.Value = "0";
            XmlAttribute newAttribute4 = document.CreateAttribute("w:color","w");
            newAttribute4.Value = "auto";

            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            newNode.Attributes.Append(newAttribute2);
            newNode.Attributes.Append(newAttribute3);
            newNode.Attributes.Append(newAttribute4);

            return newNode;
        }
        private XmlNode CreateTableParamLook(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tblLook", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "04A0";           
            newNode.Attributes.Append(newAttribute0);

            return newNode;
        }
        private XmlNode CreateTableGrid(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tblGrid", "w");
            newNode.AppendChild(CreateTableGridCol(document));
            return newNode;
        }
        private XmlNode CreateTableGridCol(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:gridCol", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("w:w", "w");
            newAttribute0.Value = "9345";
            newNode.Attributes.Append(newAttribute0);

            return newNode;
        }
        private XmlNode CreateTableRow(XmlDocument document, List<string> valueList)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tr", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("wsp:rsidR", "wsp");
            XmlAttribute newAttribute1 = document.CreateAttribute("wsp:rsidRPr", "wsp");
            XmlAttribute newAttribute2 = document.CreateAttribute("wsp:rsidTr", "wsp");
            newAttribute0.Value = "00BE031B";
            newAttribute1.Value = "00BE031B";
            newAttribute2.Value = "00BE031B";
            newNode.Attributes.Append(newAttribute0);                     
            newNode.Attributes.Append(newAttribute1);                       
            newNode.Attributes.Append(newAttribute2);

            foreach (string currentValue in valueList)
            {
                newNode.AppendChild(CreateTableRowColumn(document, currentValue));
            }

            return newNode;
        }
        private XmlNode CreateTableRowColumn(XmlDocument document, string value)//cell а не column
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tc", "w");
            newNode.AppendChild(CreateTableRowColumnProp(document));
            newNode.AppendChild(CreateTableRowColumnP(document, value));
            return newNode;
        }
        private XmlNode CreateTableRowColumnProp(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tcPr", "w");
            newNode.AppendChild(CreateTableRowColumnPropWidth(document));
            newNode.AppendChild(CreateTableRowColumnPropShd(document));
            return newNode;
        }
        private XmlNode CreateTableRowColumnPropWidth(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:tcW", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("w:w", "w");
            newAttribute0.Value = "9345";
            newNode.Attributes.Append(newAttribute0);
            XmlAttribute newAttribute1 = document.CreateAttribute("w:type", "w");
            newAttribute1.Value = "dxa";
            newNode.Attributes.Append(newAttribute1);
            return newNode;
        }
        private XmlNode CreateTableRowColumnPropShd(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:shd", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("w:val", "w");
            newAttribute0.Value = "clear";
            newNode.Attributes.Append(newAttribute0);
            XmlAttribute newAttribute1 = document.CreateAttribute("w:color", "w");
            newAttribute1.Value = "auto";
            newNode.Attributes.Append(newAttribute1);
            XmlAttribute newAttribute2 = document.CreateAttribute("w:fill", "w");
            newAttribute2.Value = "auto";
            newNode.Attributes.Append(newAttribute2);
            return newNode;
        }
        private XmlNode CreateTableRowColumnP(XmlDocument document, string value)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:p", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("wsp:rsidR", "wsp");
            XmlAttribute newAttribute1 = document.CreateAttribute("wsp:rsidRPr", "wsp");
            XmlAttribute newAttribute2 = document.CreateAttribute("wsp:rsidRDefault", "wsp");
            XmlAttribute newAttribute3 = document.CreateAttribute("wsp:rsidP", "wsp");
            newAttribute0.Value = "00BE031B";
            newAttribute1.Value = "00BE031B";
            newAttribute2.Value = "00BE031B";
            newAttribute3.Value = "00BE031B";
            newNode.Attributes.Append(newAttribute0);
            newNode.Attributes.Append(newAttribute1);
            newNode.Attributes.Append(newAttribute2);
            newNode.Attributes.Append(newAttribute3);

            newNode.AppendChild(CreateTableRowColumnPProp(document));
            newNode.AppendChild(CreateTableRowColumnPRow(document,value));
            return newNode;
        }
        private XmlNode CreateTableRowColumnPProp(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:pPr", "w");
            newNode.AppendChild(CreateTableRowColumnPPropSpacing(document));
            return newNode;
        }
        private XmlNode CreateTableRowColumnPPropSpacing(XmlDocument document)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:spacing", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("w:after", "w");
            newAttribute0.Value = "0";
            newNode.Attributes.Append(newAttribute0);
            XmlAttribute newAttribute1 = document.CreateAttribute("w:line", "w");
            newAttribute1.Value = "240";
            newNode.Attributes.Append(newAttribute1);
            XmlAttribute newAttribute2 = document.CreateAttribute("w:line-rule", "w");
            newAttribute2.Value = "auto";
            newNode.Attributes.Append(newAttribute2);
            return newNode;
        }
        private XmlNode CreateTableRowColumnPRow(XmlDocument document, string value)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:r", "w");
            XmlAttribute newAttribute0 = document.CreateAttribute("wsp:rsidRPr", "wsp");
            newAttribute0.Value = "00BE031B";
            newNode.Attributes.Append(newAttribute0);

            newNode.AppendChild(CreateTableRowColumnPRowText(document, value));
            return newNode;
        }
        private XmlNode CreateTableRowColumnPRowText(XmlDocument document, string value)
        {
            XmlNode newNode = document.CreateNode(XmlNodeType.Element, "w:t", "w");
            newNode.InnerText = value;
            return newNode;
        }
        public XmlNode GetXmlTable(XmlDocument document, List<List<string>> nestedDataList) // nested  Вложенный лист// внутренний лист по колонкам//основной по строкам
        {
            XmlNode newNode = CreateTableNodeWithHeader(document);
            foreach (List<string> currentRowList in nestedDataList)
            {
                newNode.AppendChild(CreateTableRow(document, currentRowList));
            }
            return newNode;
        }

    }
}