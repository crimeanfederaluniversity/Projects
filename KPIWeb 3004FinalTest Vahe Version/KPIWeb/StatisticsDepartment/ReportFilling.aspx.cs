using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KPIWeb.StatisticsDepartment
{
    public partial class ReportFilling : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            KPIWebDataContext kPiDataContext = new KPIWebDataContext();
            Serialization UserSer = (Serialization)Session["UserID"];
            if (UserSer == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int userID = UserSer.Id;
            UsersTable userTable =
                (from a in kPiDataContext.UsersTable where a.UsersTableID == userID select a).FirstOrDefault();

            if ((userTable.AccessLevel != 10) && (userTable.AccessLevel != 9))
            {
                Response.Redirect("~/Default.aspx");
            }

            Serialization paramSerialization = (Serialization)Session["ReportArchiveID"];
            if (paramSerialization == null)
            {
                Response.Redirect("~/Default.aspx");
            }
            int ReportArchiveID;
            ReportArchiveID = Convert.ToInt32(paramSerialization.ReportStr);

            DataTable DataTableStatus = new DataTable();

            DataTableStatus.Columns.Add(new DataColumn("LV_1", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("LV_2", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("LV_3", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("Status", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("EmailEdit", typeof(string)));
            DataTableStatus.Columns.Add(new DataColumn("EmailConfirm", typeof(string)));


            List<UsersTable> Users = (from a in kPiDataContext.UsersTable
                                      join b in kPiDataContext.ReportArchiveAndLevelMappingTable
                                          on a.FK_FirstLevelSubdivisionTable equals b.FK_FirstLevelSubmisionTableId
                                      where b.FK_ReportArchiveTableId == ReportArchiveID
                                      && b.Active == true
                                      && a.AccessLevel == 0
                                      && a.Active == true
                                      select a).ToList();
            int ii = 0;
            foreach (UsersTable currentUser in Users)
            {
                BasicParametrsAndUsersMapping UserBasicRight = (from a in kPiDataContext.BasicParametrsAndUsersMapping
                                                                where a.FK_UsersTable == currentUser.UsersTableID
                                                                && a.Active == true
                                                                select a).FirstOrDefault();
                if (UserBasicRight != null) // к пользователю прикреплены базовые показатели
                {
                    CollectedBasicParametersTable CurrentUserFirstCollected = (from a in kPiDataContext.CollectedBasicParametersTable
                                                                               where
                                                                                   //  a.FK_UsersTable == currentUser.UsersTableID
                                                                                   //      && 
                                                                                   a.Active == true
                                                                                   && a.FK_ReportArchiveTable == ReportArchiveID
                                                                                   && ((a.FK_FirstLevelSubdivisionTable == currentUser.FK_FirstLevelSubdivisionTable) || currentUser.FK_FirstLevelSubdivisionTable == null)
                                                                                   && ((a.FK_SecondLevelSubdivisionTable == currentUser.FK_SecondLevelSubdivisionTable) || currentUser.FK_SecondLevelSubdivisionTable == null)
                                                                                   && ((a.FK_ThirdLevelSubdivisionTable == currentUser.FK_ThirdLevelSubdivisionTable) || currentUser.FK_ThirdLevelSubdivisionTable == null)
                                                                                   && ((a.FK_FourthLevelSubdivisionTable == currentUser.FK_FourthLevelSubdivisionTable) || currentUser.FK_FourthLevelSubdivisionTable == null)
                                                                                   && ((a.FK_FifthLevelSubdivisionTable == currentUser.FK_FifthLevelSubdivisionTable) || currentUser.FK_FifthLevelSubdivisionTable == null)
                                                                               select a).FirstOrDefault();
                    string status = "Нет данных";
                    int Statusn = 0;

                    if (CurrentUserFirstCollected == null)
                    {
                        status = "Заполнение не начато!";
                    }
                    else if (CurrentUserFirstCollected.Status == null)
                    {
                        Statusn = 0;
                    }
                    else
                    {
                        Statusn = (int)CurrentUserFirstCollected.Status;
                    }

                    if (Statusn == 4)
                    {                     
                        continue;
                        status = "Данные утверждены";
                    }
                    else if (Statusn == 3)
                    {
                        status = "Данные ожидают утверждения";
                    }
                    else if (Statusn == 2)
                    {
                        status = "Данные в процессе заполнения";
                    }
                    else if (Statusn == 1)
                    {
                        status = "Данные возвращены на доработку";
                    }
                    else if (Statusn == 0)
                    {
                        status = "Данные в процессе заполнения";
                    }
                    else
                    {
                        //error
                    }

                    if (UserBasicRight.CanEdit == true)
                    {
                        DataRow dataRow = DataTableStatus.NewRow();
                        dataRow["LV_1"] = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                           where a.FirstLevelSubdivisionTableID == currentUser.FK_FirstLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();
                        dataRow["LV_2"] = (from a in kPiDataContext.SecondLevelSubdivisionTable
                                           where a.SecondLevelSubdivisionTableID == currentUser.FK_SecondLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();
                        dataRow["LV_3"] = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                           where a.ThirdLevelSubdivisionTableID == currentUser.FK_ThirdLevelSubdivisionTable
                                           select a.Name).FirstOrDefault();
                        dataRow["Status"] = status;
                        dataRow["EmailEdit"] = currentUser.Email;

                        BasicParametersTable BasicConnectedToUser = (from a in kPiDataContext.BasicParametersTable
                                                                     join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                                         on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                     where b.FK_UsersTable == currentUser.UsersTableID
                                                                     && b.CanEdit == true
                                                                     && b.Active == true
                                                                     && a.Active == true
                                                                     select a).FirstOrDefault();

                        UsersTable ConfirmUserEmail = (from a in kPiDataContext.UsersTable
                                                       join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                           on a.UsersTableID equals b.FK_UsersTable
                                                       where b.FK_ParametrsTable == BasicConnectedToUser.BasicParametersTableID
                                                        && b.CanConfirm == true
                                                        && a.Active == true
                                                        && b.Active == true
                                                        && ((a.FK_FirstLevelSubdivisionTable == currentUser.FK_FirstLevelSubdivisionTable) || currentUser.FK_FirstLevelSubdivisionTable == null)
                                                        && ((a.FK_SecondLevelSubdivisionTable == currentUser.FK_SecondLevelSubdivisionTable) || currentUser.FK_SecondLevelSubdivisionTable == null)
                                                        && ((a.FK_ThirdLevelSubdivisionTable == currentUser.FK_ThirdLevelSubdivisionTable) || currentUser.FK_ThirdLevelSubdivisionTable == null)
                                                        && ((a.FK_FourthLevelSubdivisionTable == currentUser.FK_FourthLevelSubdivisionTable) || currentUser.FK_FourthLevelSubdivisionTable == null)
                                                        && ((a.FK_FifthLevelSubdivisionTable == currentUser.FK_FifthLevelSubdivisionTable) || currentUser.FK_FifthLevelSubdivisionTable == null)
                                                       select a).FirstOrDefault();
                        if (ConfirmUserEmail != null)
                        {
                            dataRow["EmailConfirm"] = ConfirmUserEmail.Email;
                        }
                        else
                        {
                            dataRow["EmailConfirm"] = "Ошибка: Отсутствует утверждающий пользователь!";
                        }
                        ii++;
                        DataTableStatus.Rows.Add(dataRow);
                    }
                    else if (UserBasicRight.CanConfirm == true)
                    {
                        BasicParametersTable BasicConnectedToUser = (from a in kPiDataContext.BasicParametersTable
                                                                     join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                                         on a.BasicParametersTableID equals b.FK_ParametrsTable
                                                                     where b.FK_UsersTable == currentUser.UsersTableID
                                                                     && b.CanConfirm == true
                                                                     && b.Active == true
                                                                     && a.Active == true
                                                                     select a).FirstOrDefault();

                        UsersTable ConfirmUserEmail = (from a in kPiDataContext.UsersTable
                                                       join b in kPiDataContext.BasicParametrsAndUsersMapping
                                                           on a.UsersTableID equals b.FK_UsersTable
                                                       where b.FK_ParametrsTable == BasicConnectedToUser.BasicParametersTableID
                                                        && b.CanEdit == true
                                                        && a.Active == true
                                                        && b.Active == true
                                                        && ((a.FK_FirstLevelSubdivisionTable == currentUser.FK_FirstLevelSubdivisionTable) || currentUser.FK_FirstLevelSubdivisionTable == null)
                                                        && ((a.FK_SecondLevelSubdivisionTable == currentUser.FK_SecondLevelSubdivisionTable) || currentUser.FK_SecondLevelSubdivisionTable == null)
                                                        && ((a.FK_ThirdLevelSubdivisionTable == currentUser.FK_ThirdLevelSubdivisionTable) || currentUser.FK_ThirdLevelSubdivisionTable == null)
                                                        && ((a.FK_FourthLevelSubdivisionTable == currentUser.FK_FourthLevelSubdivisionTable) || currentUser.FK_FourthLevelSubdivisionTable == null)
                                                        && ((a.FK_FifthLevelSubdivisionTable == currentUser.FK_FifthLevelSubdivisionTable) || currentUser.FK_FifthLevelSubdivisionTable == null)
                                                       select a).FirstOrDefault();
                        ii++;
                        if (ConfirmUserEmail == null)
                        {
                            DataRow dataRow = DataTableStatus.NewRow();
                            dataRow["LV_1"] = (from a in kPiDataContext.FirstLevelSubdivisionTable
                                               where a.FirstLevelSubdivisionTableID == currentUser.FK_FirstLevelSubdivisionTable
                                               select a.Name).FirstOrDefault();
                            dataRow["LV_2"] = (from a in kPiDataContext.SecondLevelSubdivisionTable
                                               where a.SecondLevelSubdivisionTableID == currentUser.FK_SecondLevelSubdivisionTable
                                               select a.Name).FirstOrDefault();
                            dataRow["LV_3"] = (from a in kPiDataContext.ThirdLevelSubdivisionTable
                                               where a.ThirdLevelSubdivisionTableID == currentUser.FK_ThirdLevelSubdivisionTable
                                               select a.Name).FirstOrDefault();
                            dataRow["Status"] = status;
                            dataRow["EmailConfirm"] = currentUser.Email;
                            dataRow["EmailEdit"] = "Ошибка: Отсутствует вносяший данные пользователь!";
                            DataTableStatus.Rows.Add(dataRow);
                            ii++;
                        }
                    }
                }
            }   
            GridWhoOws.DataSource = DataTableStatus;
            GridWhoOws.DataBind();
        }
    }
}