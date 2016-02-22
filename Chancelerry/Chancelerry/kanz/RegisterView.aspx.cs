using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class RegisterView : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];

            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }

            /////////////////////////////////////////////////////////////////////

            var regId = Session["registerID"];
            ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext();

            var register = 
                (from r in dataContext.Registers
                 where r.registerID == Convert.ToInt32(regId)
                 select r).FirstOrDefault();

            
            if (register != null)
            {
                var regName = register.name;
                var regModel = register.fk_registersModel;

                RegisterNameLabel.Text = regName;

                // Достаем поля для данного реестра и пользователя на основе RegisterView и прав пользователя RegistersUsersMap c сортировкой по весу
                var fields = (from regUsrMap in dataContext.RegistersUsersMap
                              join regView in dataContext.RegistersView on regUsrMap.registersUsersMapID equals regView.fk_registersUsersMap
                              join _fields in dataContext.Fields on regView.fk_field equals _fields.fieldID
                              where regUsrMap.fk_user == Convert.ToInt32(userID) && regUsrMap.fk_register == register.registerID
                              select new {_fields.fieldID, _fields.name, regView.weight}).OrderBy(w=>w.weight) .ToList();
 

                foreach (var field4 in fields)
                {
                    // Смотрим все карточки для поля в этом реестре, выбираем значения, инстанс и версию.
                    var data4 = (from CollCards in dataContext.CollectedCards
                                 join colFieldsVal in dataContext.CollectedFieldsValues on CollCards.collectedCardID equals
                                 colFieldsVal.fk_collectedCard
                                 join field in dataContext.Fields on colFieldsVal.fk_field equals field.fieldID
                                 where CollCards.fk_register == 1 && colFieldsVal.fk_field == field4.fieldID
                                 select new {colFieldsVal.valueInt, colFieldsVal.valueText, colFieldsVal.valueFloat, colFieldsVal.valueData, colFieldsVal.instance, colFieldsVal.version} ).ToList();

                    GridViewData.DataSource = data4;

                    BoundField boundField = new BoundField();
                    boundField.DataField = "valueText";
                    boundField.HeaderText = field4.name;
                    GridViewData.Columns.Add(boundField);
                }
 
                DataBind();

            }


            if (!Page.IsPostBack)
            {
                
            }
        }
    }
}