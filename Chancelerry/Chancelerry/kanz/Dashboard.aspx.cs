﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Chancelerry.kanz
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            /////////////////////////////////////////////////////////////////////

            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();

            // Забираем только прикрепленные регистры в  RegUsrMap для данного пользователя и проверяем на активность в Registers.
            var registers = (from rum in dataContext.RegistersUsersMap  
                join reg in dataContext.Registers on rum.fk_user equals (int)userID
                             where reg.active && rum.active
                select new {reg.registerID, reg.name}).ToList();

            GridViewRegisters.DataSource = (from r in dataContext.Registers select r).ToList();

            BoundField boundField = new BoundField();
            boundField.DataField = "registerID";
            boundField.HeaderText = "ID";
            boundField.Visible = false;
            GridViewRegisters.Columns.Add(boundField);



            ButtonField coluButtonField = new ButtonField();
            coluButtonField.DataTextField = "name";
            coluButtonField.HeaderText = "Реестры документов";
            coluButtonField.ButtonType = ButtonType.Link;
            coluButtonField.CommandName = "Link";
            GridViewRegisters.Columns.Add(coluButtonField);

            DataBind();

        }

        protected void GridViewRegisters_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();

            switch (e.CommandName)
            {
                case "Link":
                {
                        var registers = (from rum in dataContext.RegistersUsersMap
                                         join reg in dataContext.Registers on rum.fk_user equals (int)Session["userID"]
                                         where reg.active && rum.active
                                         select new { reg.registerID, reg.name }).ToList();

                    Session["registerID"] = registers[Convert.ToInt32(e.CommandArgument)].registerID;
                    Response.Redirect("RegisterView.aspx");
                }
                    break;
            }
        }
    }
}