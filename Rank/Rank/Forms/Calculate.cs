using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rank.Forms
{
    public class Calculate
    {
        RankDBDataContext ratingDB = new RankDBDataContext();
        public void CalculateUserArticlePoint(int paramId, int articleid, int userId) // для конкретного пользователя баллы за отдельную статью 
        {
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();
          
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
                Rank_UserArticleMappingTable articlevalue = (from a in ratingDB.Rank_UserArticleMappingTable
                                                             where a.Active == true && a.FK_Article == articleid && a.FK_User == userId
                                                             join b in ratingDB.Rank_Articles on a.FK_Article equals b.ID
                                                             where b.Active == true
                                                             select a).FirstOrDefault();
                if (paramId == 5 )
                {
                    Rank_ArticleValues ipp = (from a in ratingDB.Rank_ArticleValues
                                              where a.Active == true && a.FK_Field == 1183 && a.FK_Article == articleid
                                              select a).FirstOrDefault();
                    double allsum = 0;                 
                    if ( mark != null && mark.Points != null && point != null && point.Value != null && ipp.Value != null)
                    {
                      Rank_DropDownValues IPP = (from a in ratingDB.Rank_DropDownValues
                                                 where a.Active == true && a.FK_dropdown == 5 && a.Name == ipp.Value
                                                 select a).FirstOrDefault();
                            if (IPP.FloatValue.HasValue)
                            {
                                allsum = IPP.FloatValue.Value * mark.Points.Value * point.Value.Value;
                            }                           
                        }                          
                    articlevalue.ValuebyArticle = allsum;
                    ratingDB.SubmitChanges();
                }

                else
                {
                    double allsum = 0;
                    if (weight != null && weight.Weight != null && mark != null && mark.Points != null && point != null && point.Value != null)
                    {
                        allsum = weight.Weight.Value * mark.Points.Value * point.Value.Value;
                        articlevalue.ValuebyArticle = allsum;
                        ratingDB.SubmitChanges();
                    }
                }           
        }
 
        public void CalculateUserParametrPoint(int paramId,  int userId)  // посчитать баллы показателей индивидуального рейтинга
        {
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();
          
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
                List<Rank_UserArticleMappingTable> userarticlevalue = (from a in ratingDB.Rank_UserArticleMappingTable
                                                                       where a.Active == true && a.FK_User == userId && a.UserConfirm == true
                                                                       join b in ratingDB.Rank_Articles on a.FK_Article equals b.ID
                                                                       where b.Active == true && b.FK_parametr == paramId && b.Status != 0
                                                                                                                      select a).ToList();
                double sum = 0;
                foreach (var tmp in userarticlevalue)
                { if (tmp.ValuebyArticle.HasValue)
                    {
                        sum = sum + tmp.ValuebyArticle.Value;
                    }
                }
                calculate.Value = sum;
                ratingDB.SubmitChanges();

            // рейтинг индивидуальный
            
                Rank_UserRatingPoints point = (from a in ratingDB.Rank_UserRatingPoints
                                               where a.Active == true && a.FK_User == userId
                                               select a).FirstOrDefault();
                if (point == null)
                {
                    point = new Rank_UserRatingPoints();
                    point.Active = true;
                    point.FK_User = userId;
                    ratingDB.Rank_UserRatingPoints.InsertOnSubmit(point);
                    ratingDB.SubmitChanges();
                }
                Rank_UserRatingPoints calculate0 = (from a in ratingDB.Rank_UserRatingPoints
                                                    where a.Active == true && a.FK_User == userId
                                                    select a).FirstOrDefault();

                List<Rank_UserParametrValue> allparam = (from a in ratingDB.Rank_UserParametrValue
                                                         where a.Active == true && a.FK_user == userId
                                                         select a).ToList();
                double userrating = 0;
                if (allparam != null)
                {
                    foreach (var a in allparam)
                    {
                        if (a.Value.HasValue)
                        {
                            userrating = userrating + a.Value.Value;
                        }
                    }
                }
                calculate0.Value = userrating;
                ratingDB.SubmitChanges();
            
        }
        
        public void CalculateHeadParametrPoint( int userId)  // посчитать баллы показателей руководителей
        {
            Rank_UserRatingPoints point = (from a in ratingDB.Rank_UserRatingPoints
                                           where a.Active == true && a.FK_User == userId && a.Headtype == true
                                          select a).FirstOrDefault();
            if (point == null)
            {
                point = new Rank_UserRatingPoints();
                point.Active = true;
                point.FK_User = userId;
                point.Headtype = true;
                ratingDB.Rank_UserRatingPoints.InsertOnSubmit(point);
                ratingDB.SubmitChanges();
            }
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();


            
            if (rights.AccessLevel == 1)// рейтинг завкафедры
            {

                Rank_UserRatingPoints calculate1 = (from a in ratingDB.Rank_UserRatingPoints
                                                   where a.Active == true && a.FK_User == userId && a.Headtype == false
                                                   select a).FirstOrDefault();

                Rank_StructPoints kafpoint = (from a in ratingDB.Rank_StructPoints
                                                    where a.Active == true &&
                                                    (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                                     && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                                     && a.FK_thirdlvl == rights.FK_ThirdLevelSubdivisionTable)
                                                    select a).FirstOrDefault();

                if (calculate1 != null && calculate1.Value.HasValue && kafpoint != null && kafpoint.Value.HasValue)
                {
                    point.Value = (calculate1.Value + kafpoint.Value) / 2; 
                    ratingDB.SubmitChanges();
                }

            }

            if (rights.AccessLevel == 2)// рейтинг декана
            {
                Rank_UserRatingPoints calculate2 = (from a in ratingDB.Rank_UserRatingPoints
                                                    where a.Active == true && a.FK_User == userId && a.Headtype == false
                                                    select a).FirstOrDefault();

                Rank_StructPoints facpoint = (from a in ratingDB.Rank_StructPoints
                                              where a.Active == true &&
                                              (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                               && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                               && a.FK_thirdlvl == null)
                                              select a).FirstOrDefault();

                if (calculate2 != null && calculate2.Value.HasValue && facpoint != null && facpoint.Value.HasValue)
                {
                    point.Value = (calculate2.Value + facpoint.Value) / 2;
                    ratingDB.SubmitChanges();
                }

            }

            if (rights.AccessLevel == 4)// рейтинг директор
            {

                Rank_UserRatingPoints calculate3 = (from a in ratingDB.Rank_UserRatingPoints
                                                   where a.Active == true && a.FK_User == userId && a.Headtype == false
                                                    select a).FirstOrDefault();

                Rank_StructPoints acadpoint = (from a in ratingDB.Rank_StructPoints
                                               where a.Active == true &&
                                               (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                                && a.FK_secondlvl == null
                                                && a.FK_thirdlvl == null)
                                               select a).FirstOrDefault();
                if (calculate3 != null && calculate3.Value.HasValue && acadpoint != null && acadpoint.Value.HasValue)
                {
                    point.Value = (calculate3.Value + acadpoint.Value) / 2;
                    ratingDB.SubmitChanges();
                }
            }
        }

        public void CalculateStructPoint(int userId)
        {
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();
            if (rights.AccessLevel == 0) // рейтинг кафедры
            {               
                Rank_StructPoints point = (from a in ratingDB.Rank_StructPoints
                                           where a.Active == true  &&
                                           (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                            && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                            && a.FK_thirdlvl == rights.FK_ThirdLevelSubdivisionTable)
                                           select a).FirstOrDefault();
                if (point == null)
                {
                    point = new Rank_StructPoints();
                    point.Active = true;
                    point.FK_firstlvl = rights.FK_FirstLevelSubdivisionTable;
                    point.FK_secondlvl = rights.FK_SecondLevelSubdivisionTable;
                    point.FK_thirdlvl = rights.FK_ThirdLevelSubdivisionTable;
                    ratingDB.Rank_StructPoints.InsertOnSubmit(point);
                    ratingDB.SubmitChanges();

                }
                List<UsersTable> kafedra = (from a in ratingDB.UsersTable
                                            where a.Active == true && (a.FK_FirstLevelSubdivisionTable == rights.FK_FirstLevelSubdivisionTable
                                             && a.FK_SecondLevelSubdivisionTable == rights.FK_SecondLevelSubdivisionTable
                                             && a.FK_ThirdLevelSubdivisionTable == rights.FK_ThirdLevelSubdivisionTable)
                                            select a).ToList();
                if (kafedra != null && kafedra.Count > 0)
                {
                    double kafstavka = 0;                  
                    double sum = 0;                   
                    foreach (var tmp in kafedra)
                    {
                        if(tmp.Stavka.HasValue)
                        {
                            kafstavka = kafstavka + tmp.Stavka.Value;
                        }                        
                        
                        Rank_UserRatingPoints calculate = (from a in ratingDB.Rank_UserRatingPoints
                                                            where a.Active == true  && a.FK_User == tmp.UsersTableID && a.Headtype == false
                                                           select a).FirstOrDefault();
                        if (calculate != null &&  calculate.Value.HasValue)
                        {
                            sum = sum + calculate.Value.Value;
                        }
                    }
                    double reitingkaf = sum / kafstavka;
                    point.Value = reitingkaf;
                    ratingDB.SubmitChanges();
                }

            }
            // рейтинг факультета           
                Rank_StructPoints point2 = (from a in ratingDB.Rank_StructPoints
                                           where a.Active == true  &&
                                           (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                            && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                            && a.FK_thirdlvl == null)
                                           select a).FirstOrDefault();
                if (point2 == null)
                {
                    point2 = new Rank_StructPoints();
                    point2.Active = true;
                    point2.FK_firstlvl = rights.FK_FirstLevelSubdivisionTable;
                    point2.FK_secondlvl = rights.FK_SecondLevelSubdivisionTable;
                    point2.FK_thirdlvl = null;
                    ratingDB.Rank_StructPoints.InsertOnSubmit(point2);
                    ratingDB.SubmitChanges();
                }
                List<Rank_StructPoints> faculty = (from a in ratingDB.Rank_StructPoints
                                                   where a.Active == true && (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable 
                                                   && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable && a.FK_thirdlvl != null) select a).ToList();   
                if (faculty != null && faculty.Count > 0)
                {
                    double sum = 0;                   
                    foreach (var tmp in faculty)
                    {                    
                        if (tmp.Value != null)
                        {
                            sum = sum + tmp.Value.Value;
                        }
                    }
                    double reitingfac = sum / faculty.Count;
                    point2.Value = reitingfac;
                    ratingDB.SubmitChanges();
                }           
          // рейтинг академии
            
                Rank_StructPoints point3 = (from a in ratingDB.Rank_StructPoints
                                           where a.Active == true && 
                                           (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                            && a.FK_secondlvl == null
                                            && a.FK_thirdlvl == null)
                                           select a).FirstOrDefault();
                if (point3 == null)
                {
                    point3 = new Rank_StructPoints();
                    point3.Active = true;
                    point3.FK_firstlvl = rights.FK_FirstLevelSubdivisionTable;
                    point3.FK_secondlvl = null;
                    point3.FK_thirdlvl = null;
                   ratingDB.Rank_StructPoints.InsertOnSubmit(point3);
                    ratingDB.SubmitChanges();
                }
                List<Rank_StructPoints> academy = (from a in ratingDB.Rank_StructPoints
                                                   where a.Active == true && a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable && 
                                                   a.FK_secondlvl != null && a.FK_thirdlvl != null
                                                   select a).ToList();
                if (academy != null && academy.Count > 0)
                {
                    double sum = 0;                    
                    foreach (var tmp in academy)
                    {
                        if (tmp.Value != null)
                        {
                            sum = sum + tmp.Value.Value;
                        }
                    }
                    double reitingacad = sum / academy.Count;
                    point3.Value = reitingacad;
                    ratingDB.SubmitChanges();
                }

            }
        
        
     
    }
}
 