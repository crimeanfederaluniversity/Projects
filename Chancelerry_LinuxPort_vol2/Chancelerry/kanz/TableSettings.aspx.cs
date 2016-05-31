using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Configuration;
using System.Web.UI.WebControls;
using Npgsql;

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

                using (ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"])))
                {
                    int regId;
                    int userId;
                    int.TryParse(Session["registerID"].ToString(), out regId);
                    int.TryParse(Session["userID"].ToString(), out userId);

                    //кусок Ваге // создаем для пользователя все связи к выбранному регистру если их нет
                    List<Fields> allFieldsInTable = (from a in dataContext.Fields
                        join b in dataContext.FieldsGroups
                            on a.FkFieldsGroup equals b.FieldsGroupID
                        join c in dataContext.RegistersModels
                            on b.FkRegisterModel equals c.RegisterModelID
                        join d in dataContext.Registers
                            on c.RegisterModelID equals d.FkRegistersModel
                        where a.Active == true
                              && b.Active == true
                              && c.Active == true
                              && d.Active == true
                              //&& d.RegisterID == (int) Session["registerID"] PORT
                              && d.RegisterID == regId
                        select a).Distinct().ToList();
                    List<Fields> allFieldsWithUser = (from a in dataContext.Fields
                                                      join b in dataContext.RegistersView on a.FieldID equals b.FkField
                                                      join c in dataContext.RegistersUsersMap on b.FkRegistersUsersMap equals c.RegistersUsersMapID
                                                      where
                                                      a.Active == true                         
                                                      && c.Active == true
                                                     // && b.active == tr
                                                     // && c.FkRegister == (int)Session["registerID"] PORT
                                                     // && c.FkUser == (int)Session["userID"] PORT
                                                     && c.FkRegister == regId
                                                     && c.FkUser == userId
                                                      select a).ToList();
                    RegistersUsersMap registerUserMap = (from a in dataContext.RegistersUsersMap
                                                         where a.Active
                                                              // && a.FkUser == (int)Session["userID"] PORT
                                                              // && a.FkRegister == (int)Session["registerID"] PORT
                                                              && a.FkUser == userId
                                                              && a.FkRegister == regId
                                                         select a).FirstOrDefault();

                    
                    foreach (Fields currentField in allFieldsInTable)
                    {
                        if (allFieldsWithUser.Contains(currentField))
                        {
                            continue;
                        }
                        else
                        {
                            RegistersView registersView = new RegistersView();
                            registersView.Active = true;
                            registersView.FkRegistersUsersMap = registerUserMap.RegistersUsersMapID ;
                            registersView.FkField = currentField.FieldID;
                            registersView.Weight = 0;
                            dataContext.RegistersView.InsertOnSubmit(registersView);
                            dataContext.SubmitChanges();
                        }
                    }
                    /*
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
                        dataRow["fieldId"] = item.FieldID;
                        dataRow["fieldName"] = item.Name;
                        dataRow["fieldWeight"] = (from a in dataContext.RegistersView
                            where
                                a.FkRegistersUsersMap == registerUserMap.RegistersUsersMapID &&
                                a.FkField == item.FieldID
                            //&& a.active == true
                            select a.Weight).FirstOrDefault();
                        dataRow["fieldIsAdd"] = (from a in dataContext.RegistersView
                                                 where
                                                     a.FkRegistersUsersMap == registerUserMap.RegistersUsersMapID &&
                                                     a.FkField == item.FieldID
                                                 //&& a.active == true
                                                 select a.Active).FirstOrDefault();
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
            int regId;
            int userId;
            int.TryParse(Session["registerID"].ToString(), out regId);
            int.TryParse(Session["userID"].ToString(), out userId);

            int rowIndex = 0;
            //Button button = (Button) sender;
            using (ChancelerryDb dataContext = new ChancelerryDb(new NpgsqlConnection(WebConfigurationManager.AppSettings["ConnectionStringToPostgre"])))
            {
                // Запись в RegisterUserMap для данного пользователя и реестра
                var regUsrMap = (from a in dataContext.RegistersUsersMap
                                     // where a.FkRegister == (int)Session["registerID"] && a.FkUser == (int) Session["userID"] PORT
                                 where a.FkRegister == regId && a.FkUser == userId
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
                         //   TextBox fieldName = (TextBox) GridView1.Rows[rowIndex].FindControl("fieldName");
                            TextBox fieldWeight = (TextBox) GridView1.Rows[rowIndex].FindControl("fieldWeight");
                            CheckBox fieldIsAdd = (CheckBox) GridView1.Rows[rowIndex].FindControl("fieldIsAdd");

                            int fieldid;
                            double fieldweight = Convert.ToDouble(fieldWeight.Text);

                            int.TryParse(fieldId.Text, out fieldid);
                            // Запрос на существование записи в ТАБЛИЦЕ(БД) registersView с параметрами из таблицы
                            RegistersView registersView = (from a in dataContext.RegistersView
                                                           where
                                                                a.FkRegistersUsersMap == regUsrMap.RegistersUsersMapID &&
                                                                //a.FkField == Convert.ToInt32(fieldId.Text)
                                                                a.FkField == fieldid
                                                           select a).FirstOrDefault();

                            // Если такая запись в ТАБЛИЦЕ существует, то присваиваем значения active и weight из таблицы(на странице)
                            if (registersView != null)
                            {
                                registersView.Active = fieldIsAdd.Checked;
                                registersView.Weight = Convert.ToDouble(fieldWeight.Text);
                            }
                            else
                            {
                                // Если нет, то создаем новую запись в таблице RegistersView  с параметрами из таблицы
                                registersView = new RegistersView();
                                registersView.Active = true;
                                registersView.FkRegistersUsersMap = regUsrMap.RegistersUsersMapID;
                                //registersView.FkField = Convert.ToInt32(fieldId.Text); PORT
                                //registersView.Weight = Convert.ToDouble(fieldWeight.Text); PORT
                                registersView.FkField = fieldid;
                                registersView.Weight = fieldweight;
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