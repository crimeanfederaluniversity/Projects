using System;
using System.Collections.Generic;
using System.Data;
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

                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("fieldId", typeof (int)));
                dataTable.Columns.Add(new DataColumn("fieldName", typeof (string)));
                dataTable.Columns.Add(new DataColumn("fieldWeight", typeof (double)));
                dataTable.Columns.Add(new DataColumn("fieldIsAdd", typeof (bool)));

                using (ChancelerryDBDataContext dataContext = new ChancelerryDBDataContext())
                {
                    // Все возможные поля для таблицы в данном реестре и для данного пользователя
                    var all = (from a in dataContext.Fields
                               join b in dataContext.RegistersView on a.fieldID equals b.fk_field
                               join c in dataContext.RegistersUsersMap on b.fk_registersUsersMap equals c.registersUsersMapID
                               where c.fk_user == (int)Session["userID"] && c.fk_register == (int)Session["registerID"]
                               select new {a.fieldID, a.name, b.weight});

                    // Уже прикрученные поля к этому пользователю
                    var userView = (from a in dataContext.Fields
                        join b in dataContext.RegistersView on a.fieldID equals b.fk_field
                        join c in dataContext.RegistersUsersMap on b.fk_registersUsersMap equals c.registersUsersMapID
                        where c.fk_register == 1 && c.fk_user == 1 && b.active
                        select a.fieldID).ToList();

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