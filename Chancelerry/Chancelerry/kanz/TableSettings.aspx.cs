using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Chancelerry.kanz
{
    public partial class TableSettings : System.Web.UI.Page
    {
        public class ViewFieldData
        {
            public int id { get; set; }
            public string name { get; set; }
            public double weight { get; set; }
            public bool isAdd { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userID = Session["userID"];
            if (userID == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            ///////////////////////////////////////////         
            if (!Page.IsPostBack)
            {
                Session["searchList"] = new List<TableActions.SearchValues>();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("fieldId", typeof (int)));
                dataTable.Columns.Add(new DataColumn("fieldName", typeof (string)));
                dataTable.Columns.Add(new DataColumn("fieldWeight", typeof (double)));
                dataTable.Columns.Add(new DataColumn("fieldIsAdd", typeof (bool)));

                using (ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext())
                {
                    //кусок Ваге // создаем для пользователя все связи к выбранному регистру если их нет
                    List<Fields> allFieldsInTable = (from a in dataContext.Fields
                        join b in dataContext.FieldsGroups
                            on a.fk_fieldsGroup equals b.fieldsGroupID
                        join c in dataContext.RegistersModels
                            on b.fk_registerModel equals c.registerModelID
                        join d in dataContext.Registers
                            on c.registerModelID equals d.fk_registersModel
                        where a.active == true
                              && b.active == true
                              && c.active == true
                              && d.active == true
                              && d.registerID == (int) Session["registerID"]
                        select a).Distinct().ToList();
                    List<Fields> allFieldsWithUser = (from a in dataContext.Fields
                                                      join b in dataContext.RegistersView on a.fieldID equals b.fk_field
                                                      join c in dataContext.RegistersUsersMap on b.fk_registersUsersMap equals c.registersUsersMapID
                                                      where
                                                      a.active == true                         
                                                      && c.active == true
                                                     // && b.active == tr
                                                      && c.fk_register == (int)Session["registerID"]
                                                      && c.fk_user == (int)Session["userID"]
                                                      select a).ToList();
                    RegistersUsersMap registerUserMap = (from a in dataContext.RegistersUsersMap
                                                         where a.active
                                                               && a.fk_user == (int)Session["userID"]
                                                               && a.fk_register == (int)Session["registerID"]
                                                         select a).FirstOrDefault();

                    /*
                    foreach (Fields currentField in allFieldsInTable)
                    {
                        if (allFieldsWithUser.Contains(currentField))
                        {
                            continue;
                        }
                        else
                        {
                            RegistersView registersView = new RegistersView();
                            registersView.active = true;
                            registersView.fk_registersUsersMap = registerUserMap.registersUsersMapID;
                            registersView.fk_field = currentField.fieldID;
                            registersView.weight = 0;
                            dataContext.RegistersView.InsertOnSubmit(registersView);
                            dataContext.SubmitChanges();
                        }
                    }

                    // Кусок Ваге окончен :)
                    // Все возможные поля для таблицы в данном реестре
                      var all = (from a in dataContext.Fields
                                 join b in dataContext.RegistersView on a.fieldID equals b.fk_field
                                 join c in dataContext.RegistersUsersMap on b.fk_registersUsersMap equals c.registersUsersMapID
                                 where c.fk_register == (int)Session["registerID"]
                                 select new {a.fieldID, a.name, b.weight}).Distinct();

                      // Уже прикрученные поля к этому пользователю
                      var userView = (from a in dataContext.Fields
                          join b in dataContext.RegistersView on a.fieldID equals b.fk_field
                          join c in dataContext.RegistersUsersMap on b.fk_registersUsersMap equals c.registersUsersMapID
                          where c.fk_register == (int)Session["registerID"] && c.fk_user == (int)Session["userID"] && b.active
                          select a.fieldID).Distinct().ToList();

                      var allCross = new List<ViewFieldData>();

                      // Проходим по всем полям
                      foreach (var qwe in all)
                      {
                          // Если это поле прикручено к таблице этого реестра и этому пользователю в ТАБЛИЦЕ RegisterView, то отмечаем его с галочкой Checked true
                          allCross.Add(userView.Contains(qwe.fieldID)
                              ? new ViewFieldData()
                              {
                                  id = qwe.fieldID,
                                  name = qwe.name,
                                  weight = qwe.weight,
                                  isAdd = true
                              }
                              // если нет то с галочкой UnChecked false
                              : new ViewFieldData()
                              {
                                  id = qwe.fieldID,
                                  name = qwe.name,
                                  weight = qwe.weight,
                                  isAdd = false
                              });
                      }
                      
                      // Проходим по allCross, добавляем дааные из него в таблицу

                      foreach (var item in allCross)
                      {
                          DataRow dataRow = dataTable.NewRow();
                          dataRow["fieldId"] = item.id;
                          dataRow["fieldName"] = item.name;
                          dataRow["fieldWeight"] = item.weight;
                          dataRow["fieldIsAdd"] = item.isAdd;
                          dataTable.Rows.Add(dataRow);
                      }
                      */

                    foreach (var item in allFieldsWithUser)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        dataRow["fieldId"] = item.fieldID;
                        dataRow["fieldName"] = item.name;
                        dataRow["fieldWeight"] = (from a in dataContext.RegistersView
                            where
                                a.fk_registersUsersMap == registerUserMap.registersUsersMapID &&
                                a.fk_field == item.fieldID
                            //&& a.active == true
                            select a.weight).FirstOrDefault();
                        dataRow["fieldIsAdd"] = (from a in dataContext.RegistersView
                                                 where
                                                     a.fk_registersUsersMap == registerUserMap.registersUsersMapID &&
                                                     a.fk_field == item.fieldID
                                                 //&& a.active == true
                                                 select a.active).FirstOrDefault();
                        dataTable.Rows.Add(dataRow);
                    }

                    GridView1.DataSource = dataTable;
                    ViewState["Gridview1"] = dataTable;
                    GridView1.DataBind();
                }
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int rowIndex = 0;
            Button button = (Button) sender;
            using (ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext())
            {
                // Запись в RegisterUserMap для данного пользователя и реестра
                var regUsrMap = (from a in dataContext.RegistersUsersMap
                                 where a.fk_register == (int)Session["registerID"] && a.fk_user == (int) Session["userID"]
                                 select a).FirstOrDefault();

                if (ViewState["Gridview1"] != null)
                {
                    DataTable dataTable = (DataTable) ViewState["Gridview1"];
                    // Проходим по всей таблице
                    if (dataTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dataTable.Rows.Count; i++)
                        {
                            // Находим компоненты 
                            Label fieldId = (Label) GridView1.Rows[rowIndex].FindControl("fieldId");
                            TextBox fieldName = (TextBox) GridView1.Rows[rowIndex].FindControl("fieldName");
                            TextBox fieldWeight = (TextBox) GridView1.Rows[rowIndex].FindControl("fieldWeight");
                            CheckBox fieldIsAdd = (CheckBox) GridView1.Rows[rowIndex].FindControl("fieldIsAdd");

                            // Запрос на существование записи в ТАБЛИЦЕ(БД) registersView с параметрами из таблицы
                            RegistersView registersView = (from a in dataContext.RegistersView
                                                           where
                                                                a.fk_registersUsersMap == regUsrMap.registersUsersMapID &&
                                                                a.fk_field == Convert.ToInt32(fieldId.Text)
                                                          select a).FirstOrDefault();

                            // Если такая запись в ТАБЛИЦЕ существует, то присваиваем значения active и weight из таблицы(на странице)
                            if (registersView != null)
                            {
                                registersView.active = fieldIsAdd.Checked;
                                registersView.weight = Convert.ToDouble(fieldWeight.Text);
                            }
                            else
                            {
                                // Если нет, то создаем новую запись в таблице RegistersView  с параметрами из таблицы
                                registersView = new RegistersView();
                                registersView.active = true;
                                registersView.fk_registersUsersMap = regUsrMap.registersUsersMapID;
                                registersView.fk_field = Convert.ToInt32(fieldId.Text);
                                registersView.weight = Convert.ToDouble(fieldWeight.Text);
                                dataContext.RegistersView.InsertOnSubmit(registersView);

                            }
                            dataContext.SubmitChanges();


                            rowIndex++;
                        }
                        
                    }
                }
            }
            Response.Redirect("RegisterView.aspx");
        }
    }
}