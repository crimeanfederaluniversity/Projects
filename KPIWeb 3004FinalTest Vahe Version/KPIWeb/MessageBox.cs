using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KPIWeb
{
    public class MessageBox
    {
        public static void Show(System.Web.UI.UserControl parent, string message)
        {
            Show(parent, "MessageBoxShow", message);
        }

        public static void Show(System.Web.UI.UserControl parent, string functionName, string message)
        {
            if (parent != null && parent.Page != null)
            {
                Show(parent.Page, functionName, message);
            }
        }

        public static void Show(System.Web.UI.Page parent, string message)
        {
            Show(parent, "MessageBoxShow", message);
        }
        public static void Show(System.Web.UI.Page parent, string functionName, string message)
        {
            if (parent != null)
            {
                parent.RegisterStartupScript(functionName, "<script language=\"JavaScript\"> function " + functionName + " (){ alert('" + message.Replace("\\", "\\\\").Replace("\r\n", "\\n").Replace("'", "\\'") + "');} </script>");
            }
        }


    }
}