using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb
{
    public partial class FillingProcessInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                List<ReportArchiveTable> RepList = (from a in kPiDataContext.ReportArchiveTable
                                                    where a.Active == true
                                                    select a).ToList();
                foreach (ReportArchiveTable rep in RepList)
                {
                    ListItem Item = new ListItem();
                    Item.Text = (string)rep.Name;
                    Item.Value = (rep.ReportArchiveTableID).ToString();
                    DropDownList1.Items.Add(Item);
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (TextBox1.Text == "123_")
            {
                KPIWebDataContext kPiDataContext = new KPIWebDataContext();
                TextBox2.Text = "";
                TextBox2.Text += "Всего структурных подразделений вносящих данные: " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                        where a.Active == true
                                                                                        select a).Count().ToString();
                TextBox2.Text += Environment.NewLine;

                TextBox2.Text += "Структурных подразделений вносящих данные, где есть хоть один пользователь: " +
                                                                                    (from a in kPiDataContext.ThirdLevelParametrs
                                                                                     join b in kPiDataContext.UsersTable
                                                                                     on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                     where a.Active == true
                                                                                     && b.Active == true
                                                                                     && b.AccessLevel == 0
                                                                                     select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;

                TextBox2.Text += "Всего пользователей: " + (from b in kPiDataContext.UsersTable
                                                            where
                                                            b.AccessLevel == 0
                                                            && b.Active == true
                                                            select b).Count().ToString();
                TextBox2.Text += Environment.NewLine;

                TextBox2.Text += "Всего пользователей активировавших аккаунты: " + (from b in kPiDataContext.UsersTable
                                                                                    where
                                                                                    b.AccessLevel == 0
                                                                                    && b.Active == true
                                                                                    && b.Confirmed == true
                                                                                    select b).Count().ToString();

                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений утвердивших отчет " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                   join b in kPiDataContext.CollectedBasicParametersTable
                                                                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                   where b.Status == 4
                                                                                   select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений c отправленным на утверждение отчетом " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                                       join b in kPiDataContext.CollectedBasicParametersTable
                                                                                                       on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                                       where b.Status == 3
                                                                                                       select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений отчетом возвращенным на доработку " + (from a in kPiDataContext.ThirdLevelParametrs
                                                                                                   join b in kPiDataContext.CollectedBasicParametersTable
                                                                                                   on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                                                                                   where b.Status == 1
                                                                                                   select a).Distinct().Count().ToString();


                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Структурных подразделений внесших 1 и более показателей (учитываются утвержденные и отправленные на утверждение) " +
                    (from a in kPiDataContext.ThirdLevelParametrs
                     join b in kPiDataContext.CollectedBasicParametersTable
                     on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                     where
                     b.CollectedValue != null
                     select a).Distinct().Count().ToString();

                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Кол-во верифицирующих которые должны быть в системе для данной компании: ";
                TextBox2.Text += (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                  join b in kPiDataContext.SecondLevelSubdivisionTable
                                  on a.FK_SecondLevelSubdivisionTable equals b.SecondLevelSubdivisionTableID
                                  join c in kPiDataContext.ReportArchiveAndLevelMappingTable
                                  on b.FK_FirstLevelSubdivisionTable equals c.FK_FirstLevelSubmisionTableId
                                  where
                                  a.ThirdLevelSubdivisionTableID == c.FK_ThirdLevelSubdivisionTable
                                  && b.SecondLevelSubdivisionTableID == c.FK_SecondLevelSubdivisionTable
                                  && a.Active == true
                                  && b.Active == true
                                  && c.Active == true
                                  && c.FK_ReportArchiveTableId == 1
                                  select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;


                TextBox2.Text += "Кол-во пользователей с правами утверждающего ";
                TextBox2.Text += (from a in kPiDataContext.UsersTable
                                  where
                                  a.Active == true
                                  join b in kPiDataContext.BasicParametrsAndUsersMapping
                                  on a.UsersTableID equals b.FK_UsersTable
                                  where b.CanConfirm == true
                                  && b.CanEdit == false
                                  && b.Active == true
                                  select a).Distinct().Count().ToString();

                TextBox2.Text += Environment.NewLine;


                TextBox2.Text += "Кол-во пользователей с правами заполняющего ";
                TextBox2.Text += (from a in kPiDataContext.UsersTable
                                  where
                                  a.Active == true
                                  join b in kPiDataContext.BasicParametrsAndUsersMapping
                                  on a.UsersTableID equals b.FK_UsersTable
                                  where b.CanConfirm == false
                                  && b.CanEdit == true
                                  && b.Active == true
                                  select a).Distinct().Count().ToString();

                TextBox2.Text += Environment.NewLine;

                TextBox2.Text += "Кол-во пользователей с правами заполняющего и утверждающего ";
                TextBox2.Text += (from a in kPiDataContext.UsersTable
                                  where
                                  a.Active == true
                                  join b in kPiDataContext.BasicParametrsAndUsersMapping
                                  on a.UsersTableID equals b.FK_UsersTable
                                  where b.CanConfirm == true
                                  && b.CanEdit == true
                                  && b.Active == true
                                  select a).Distinct().Count().ToString();

                TextBox2.Text += Environment.NewLine;


                TextBox2.Text += "Кол-во верифицирующих с email адресами внесенными в систему: ";
                TextBox2.Text += (from a in kPiDataContext.UsersTable
                                  where
                                  a.Active == true
                                  join b in kPiDataContext.BasicParametrsAndUsersMapping
                                  on a.UsersTableID equals b.FK_UsersTable
                                  where b.CanConfirm == true
                                  && b.Active == true
                                  select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Кол-во верифицирующих не завершивших регистрацию: ";
                TextBox2.Text += (from a in kPiDataContext.UsersTable
                                  where
                                  a.Active == true
                                  && a.Confirmed == false
                                  join b in kPiDataContext.BasicParametrsAndUsersMapping
                                  on a.UsersTableID equals b.FK_UsersTable
                                  where b.CanConfirm == true
                                  && b.Active == true
                                  select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Кол-во верифицирующих завершивших регистрацию: ";
                TextBox2.Text += (from a in kPiDataContext.UsersTable
                                  where
                                  a.Active == true
                                  && a.Confirmed == true
                                  join b in kPiDataContext.BasicParametrsAndUsersMapping
                                  on a.UsersTableID equals b.FK_UsersTable
                                  where b.CanConfirm == true
                                  && b.Active == true
                                  select a).Distinct().Count().ToString();
                TextBox2.Text += Environment.NewLine;
                TextBox2.Text += "Кол-во верифицирующих, которые утвердили данные: ";
                TextBox2.Text += (from a in kPiDataContext.ThirdLevelParametrs
                                  join b in kPiDataContext.CollectedBasicParametersTable
                                  on a.ThirdLevelParametrsID equals b.FK_ThirdLevelSubdivisionTable
                                  where b.Status == 4
                                  select a).Distinct().Count().ToString();

                TextBox2.Text += Environment.NewLine;
            }
        }

        protected void GoBackButton_Click(object sender, EventArgs e)
        {

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}