using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rank.Forms
{ 
    public class Calculate
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        public void CalculateUserPoint(int paramId, int articleid, int userId)
        {
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();
            if (rights.AccessLevel == 0 )
            {
                Rank_Parametrs weight = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
            Rank_Mark mark = (from a in ratingDB.Rank_Mark
                              join b in ratingDB.Rank_Articles
                              on a.ID equals b.FK_mark
                              where b.Active == true && b.ID == articleid
                              select a).FirstOrDefault();
            Rank_DifficaltPoint point = (from a in ratingDB.Rank_DifficaltPoint
                                         join b in ratingDB.Rank_UserArticleMappingTable
                                         on a.ID equals b.FK_point
                                         where b.Active == true && b.FK_Article == articleid && b.FK_User == userId
                                         select a).FirstOrDefault();
            Rank_UserParametrValue calculate = (from a in ratingDB.Rank_UserParametrValue
                                                where a.Active == true && a.FK_parametr == paramId && a.FK_user == userId
                                                select a).FirstOrDefault();

            if (calculate == null)
            {
                calculate = new Rank_UserParametrValue();
                calculate.Active = true;
                calculate.FK_parametr = paramId;
                calculate.FK_user = userId;
                calculate.Accept = false;
                ratingDB.Rank_UserParametrValue.InsertOnSubmit(calculate);
                ratingDB.SubmitChanges();

            }
                if (weight!=null && weight.Weight != null && mark!=null && mark.Points != null && point!=null && point.Value != null)
                {
                    double sum;
                    sum = weight.Weight.Value * mark.Points.Value * point.Value.Value;
                    calculate.Value = sum;
                    ratingDB.SubmitChanges();
                }
            }
   
            if (rights.AccessLevel == 1) // рейтинг кафедры
            {               
                Rank_StructPoints point = (from a in ratingDB.Rank_StructPoints
                         where a.Active == true && a.FK_parametr == paramId &&
                         (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                          && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                          && a.FK_thirdlvl == rights.FK_ThirdLevelSubdivisionTable)
                         select a).FirstOrDefault();
                if(point == null)
                {
                    point = new Rank_StructPoints();
                    point.Active = true;
                    point.FK_firstlvl = rights.FK_FirstLevelSubdivisionTable;
                    point.FK_secondlvl = rights.FK_SecondLevelSubdivisionTable;
                    point.FK_thirdlvl = rights.FK_ThirdLevelSubdivisionTable;
                    point.Accept = false;
                    ratingDB.Rank_StructPoints.InsertOnSubmit(point);
                    ratingDB.SubmitChanges();

                }
                List<UsersTable> kafedra = (from a in ratingDB.UsersTable
                                            where a.Active == true &&   (a.FK_FirstLevelSubdivisionTable == rights.FK_FirstLevelSubdivisionTable
                                             && a.FK_SecondLevelSubdivisionTable == rights.FK_SecondLevelSubdivisionTable
                                             && a.FK_ThirdLevelSubdivisionTable == rights.FK_ThirdLevelSubdivisionTable)
                                            select a).ToList();
                if(kafedra!= null && kafedra.Count > 0)
                {
                    double structvalue =0;
                    foreach (var tmp in kafedra)
                    {
                        Rank_UserParametrValue calculate = (from a in ratingDB.Rank_UserParametrValue
                                                            where a.Active == true && a.FK_parametr == paramId && a.FK_user == tmp.UsersTableID
                                                            select a).FirstOrDefault();
                        if (calculate.Value != null)
                        {
                            structvalue = structvalue + calculate.Value.Value;
                        }                     
                    }
                    double reitingkaf = structvalue / kafedra.Count;
                    point.Value = reitingkaf;
                    ratingDB.SubmitChanges();
                }
                
            }
            if (rights.AccessLevel == 1)// рейтинг завкафедры
            {
                Rank_Parametrs weight = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
                Rank_Mark mark = (from a in ratingDB.Rank_Mark
                                  join b in ratingDB.Rank_Articles
                                  on a.ID equals b.FK_mark
                                  where b.Active == true && b.ID == articleid
                                  select a).FirstOrDefault();
                Rank_DifficaltPoint point = (from a in ratingDB.Rank_DifficaltPoint
                                             join b in ratingDB.Rank_UserArticleMappingTable
                                             on a.ID equals b.FK_point
                                             where b.Active == true && b.FK_Article == articleid && b.FK_User == userId
                                             select a).FirstOrDefault();
                Rank_UserParametrValue calculate = (from a in ratingDB.Rank_UserParametrValue
                                                    where a.Active == true && a.FK_parametr == paramId && a.FK_user == userId
                                                    select a).FirstOrDefault();
                Rank_StructPoints kafpoint = (from a in ratingDB.Rank_StructPoints
                                           where a.Active == true && a.FK_parametr == paramId &&
                                           (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                            && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                            && a.FK_thirdlvl == rights.FK_ThirdLevelSubdivisionTable)
                                           select a).FirstOrDefault();
                if (calculate == null)
                {
                    calculate = new Rank_UserParametrValue();
                    calculate.Active = true;
                    calculate.FK_parametr = paramId;
                    calculate.FK_user = userId;
                    calculate.Accept = false;
                    ratingDB.Rank_UserParametrValue.InsertOnSubmit(calculate);
                    ratingDB.SubmitChanges();

                }

                if (weight != null && weight.Weight != null && mark != null && mark.Points != null && point != null && point.Value != null)
                {
                    if (calculate.Value != null && point.Value != null)
                    {
                        double sum;
                        sum = weight.Weight.Value * mark.Points.Value * point.Value.Value;
                        calculate.Value = (sum + kafpoint.Value) / 2;
                        ratingDB.SubmitChanges();
                    }
                }
               
            }
            if (rights.AccessLevel == 2) // рейтинг факультета
            {
                Rank_StructPoints point = (from a in ratingDB.Rank_StructPoints
                                           where a.Active == true && a.FK_parametr == paramId &&
                                           (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                            && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                            && a.FK_thirdlvl == null)
                                           select a).FirstOrDefault();
                if (point == null)
                {
                    point = new Rank_StructPoints();
                    point.Active = true;
                    point.FK_firstlvl = rights.FK_FirstLevelSubdivisionTable;
                    point.FK_secondlvl = rights.FK_SecondLevelSubdivisionTable;
                    point.FK_thirdlvl = rights.FK_ThirdLevelSubdivisionTable;
                    point.Accept = false;
                    ratingDB.Rank_StructPoints.InsertOnSubmit(point);
                    ratingDB.SubmitChanges();

                }
                List<Rank_StructPoints> faculty = (from a in ratingDB.Rank_StructPoints  where a.Active == true   
                                                   && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable select a).ToList();
                if (faculty != null && faculty.Count > 0)
                {
                    double structvalue = 0;
                    foreach (var tmp in faculty)
                    {
                        if (tmp.Value != null)
                        {
                            structvalue = structvalue + tmp.Value.Value;
                        }
                    }
                    double reitingfac = structvalue / faculty.Count;
                    point.Value = reitingfac;
                    ratingDB.SubmitChanges();
                }

            }
            if (rights.AccessLevel == 2)// рейтинг декана
            {
                Rank_Parametrs weight = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
                Rank_Mark mark = (from a in ratingDB.Rank_Mark
                                  join b in ratingDB.Rank_Articles
                                  on a.ID equals b.FK_mark
                                  where b.Active == true && b.ID == articleid
                                  select a).FirstOrDefault();
                Rank_DifficaltPoint point = (from a in ratingDB.Rank_DifficaltPoint
                                             join b in ratingDB.Rank_UserArticleMappingTable
                                             on a.ID equals b.FK_point
                                             where b.Active == true && b.FK_Article == articleid && b.FK_User == userId
                                             select a).FirstOrDefault();
                Rank_UserParametrValue calculate = (from a in ratingDB.Rank_UserParametrValue
                                                    where a.Active == true && a.FK_parametr == paramId && a.FK_user == userId
                                                    select a).FirstOrDefault();
                Rank_StructPoints facpoint = (from a in ratingDB.Rank_StructPoints
                                              where a.Active == true && a.FK_parametr == paramId &&
                                              (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                               && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                               && a.FK_thirdlvl == null)
                                              select a).FirstOrDefault();
                if (calculate == null)
                {
                    calculate = new Rank_UserParametrValue();
                    calculate.Active = true;
                    calculate.FK_parametr = paramId;
                    calculate.FK_user = userId;
                    calculate.Accept = false;
                    ratingDB.Rank_UserParametrValue.InsertOnSubmit(calculate);
                    ratingDB.SubmitChanges();

                }

                if (weight != null && weight.Weight != null && mark != null && mark.Points != null && point != null && point.Value != null)
                {
                    if (calculate.Value != null && point.Value != null)
                    {
                        double sum;
                        sum = weight.Weight.Value * mark.Points.Value * point.Value.Value;
                        calculate.Value = (sum + facpoint.Value) / 2;
                        ratingDB.SubmitChanges();
                    }
                }

            }
            if (rights.AccessLevel == 5) // рейтинг академии
            {
                Rank_StructPoints point = (from a in ratingDB.Rank_StructPoints
                                           where a.Active == true && a.FK_parametr == paramId &&
                                           (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                            && a.FK_secondlvl == null
                                            && a.FK_thirdlvl == null)
                                           select a).FirstOrDefault();
                if (point == null)
                {
                    point = new Rank_StructPoints();
                    point.Active = true;
                    point.FK_firstlvl = rights.FK_FirstLevelSubdivisionTable;
                    point.FK_secondlvl = rights.FK_SecondLevelSubdivisionTable;
                    point.FK_thirdlvl = rights.FK_ThirdLevelSubdivisionTable;
                    point.Accept = false;
                    ratingDB.Rank_StructPoints.InsertOnSubmit(point);
                    ratingDB.SubmitChanges();
                }
                List<Rank_StructPoints> academy = (from a in ratingDB.Rank_StructPoints
                                                   where a.Active == true
                                                   && a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                                   select a).ToList();
                if (academy != null && academy.Count > 0)
                {
                    double structvalue = 0;
                    foreach (var tmp in academy)
                    {
                        if (tmp.Value != null)
                        {
                            structvalue = structvalue + tmp.Value.Value;
                        }
                    }
                    double reitingacad = structvalue / academy.Count;
                    point.Value = reitingacad;
                    ratingDB.SubmitChanges();
                }

            }
            if (rights.AccessLevel == 5)// рейтинг директор
            {
                Rank_Parametrs weight = (from item in ratingDB.Rank_Parametrs where item.ID == paramId select item).FirstOrDefault();
                Rank_Mark mark = (from a in ratingDB.Rank_Mark
                                  join b in ratingDB.Rank_Articles
                                  on a.ID equals b.FK_mark
                                  where b.Active == true && b.ID == articleid
                                  select a).FirstOrDefault();
                Rank_DifficaltPoint point = (from a in ratingDB.Rank_DifficaltPoint
                                             join b in ratingDB.Rank_UserArticleMappingTable
                                             on a.ID equals b.FK_point
                                             where b.Active == true && b.FK_Article == articleid && b.FK_User == userId
                                             select a).FirstOrDefault();
                Rank_UserParametrValue calculate = (from a in ratingDB.Rank_UserParametrValue
                                                    where a.Active == true && a.FK_parametr == paramId && a.FK_user == userId
                                                    select a).FirstOrDefault();
                Rank_StructPoints acadpoint = (from a in ratingDB.Rank_StructPoints
                                              where a.Active == true && a.FK_parametr == paramId &&
                                              (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                               && a.FK_secondlvl == null
                                               && a.FK_thirdlvl == null)
                                              select a).FirstOrDefault();
                if (calculate == null)
                {
                    calculate = new Rank_UserParametrValue();
                    calculate.Active = true;
                    calculate.FK_parametr = paramId;
                    calculate.FK_user = userId;
                    calculate.Accept = false;
                    ratingDB.Rank_UserParametrValue.InsertOnSubmit(calculate);
                    ratingDB.SubmitChanges();

                }

                if (weight != null && weight.Weight != null && mark != null && mark.Points != null && point != null && point.Value != null)
                {
                    if (calculate.Value != null && point.Value != null)
                    {
                        double sum;
                    sum = weight.Weight.Value * mark.Points.Value * point.Value.Value;
                    calculate.Value = (sum + acadpoint.Value) / 2;
                    ratingDB.SubmitChanges();
                        }
                }

            }
        }
        
     }
}