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
            if (rights.AccessLevel != 9 && rights.AccessLevel != 10)
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

                Rank_UserArticleMappingTable articlevalue = (from a in ratingDB.Rank_UserArticleMappingTable
                                                             where a.Active == true && a.FK_Article == articleid && a.FK_User == userId
                                                             join b in ratingDB.Rank_Articles on a.FK_Article equals b.ID
                                                             where b.Active == true
                                                          //   && b.Status == 1
                                                             select a).FirstOrDefault();
                double allsum = 0;
                if (weight != null && weight.Weight != null && mark != null && mark.Points != null && point != null && point.Value != null)
                {
                    allsum = weight.Weight.Value * mark.Points.Value * point.Value.Value;
                    articlevalue.ValuebyArticle = allsum;
                    ratingDB.SubmitChanges();
                }
            }

        }

        public void CalculateUserParametrPoint(int paramId, int articleid, int userId)  // посчитать баллы показателей индивидуального рейтинга
        {
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();
            if (rights.AccessLevel != 9 && rights.AccessLevel != 10)
            {
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
                                                                       where b.Active == true && b.FK_parametr == paramId
                                                                       && b.Status != 0
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
            }
        }
        /*
        public void CalculateHeadParametrPoint( int userId)  // посчитать баллы показателей руководителей
        {
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
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();
            if (rights.AccessLevel == 1)// рейтинг завкафедры
            {
                
                List<Rank_UserParametrValue> calculate = (from a in ratingDB.Rank_UserParametrValue
                                                    where a.Active == true && a.FK_user == userId
                                                    select a).ToList();

                List<Rank_StructPoints> kafpoint = (from a in ratingDB.Rank_StructPoints
                                                    where a.Active == true &&
                                                    (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                                     && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                                     && a.FK_thirdlvl == rights.FK_ThirdLevelSubdivisionTable)
                                                    select a).ToList();

                double lsum1 = 0;
                double ksum1 = 0;

                foreach (var tmp in calculate)
                {
                    if(tmp.Value.HasValue)
                    lsum1 = lsum1 + tmp.Value.Value;
                }
                foreach(var kaf in kafpoint)
                {
                    if (kaf.Value.HasValue)
                        ksum1 = ksum1 + kaf.Value.Value;
                }
                double allsum2 = 0;
                allsum2 = (lsum1 + ksum1) / 2;
                point.Value = allsum2;
                ratingDB.SubmitChanges();


            }

            if (rights.AccessLevel == 2)// рейтинг декана
            {
                List<Rank_UserParametrValue> calculate = (from a in ratingDB.Rank_UserParametrValue
                                                          where a.Active == true && a.FK_user == userId
                                                          select a).ToList();

                List<Rank_StructPoints> facpoint = (from a in ratingDB.Rank_StructPoints
                                                    where a.Active == true &&
                                                    (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                                     && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable
                                                     && a.FK_thirdlvl == null)
                                                    select a).ToList();

                double lsum2 = 0;
                double fsum2 = 0;

                foreach (var tmp in calculate)
                {
                    if (tmp.Value.HasValue)
                        lsum2 = lsum2 + tmp.Value.Value;
                }
                foreach (var kaf in facpoint)
                {
                    if (kaf.Value.HasValue)
                        fsum2 = fsum2 + kaf.Value.Value;
                }
                double allsum3 = 0;
                allsum3 = (lsum2 + fsum2) / 2;
                point.Value = allsum3;
                ratingDB.SubmitChanges();

            }


            if (rights.AccessLevel == 5)// рейтинг директор
            {

                List<Rank_UserParametrValue> calculate = (from a in ratingDB.Rank_UserParametrValue
                                                          where a.Active == true && a.FK_user == userId
                                                          select a).ToList();

                List<Rank_StructPoints> acadpoint = (from a in ratingDB.Rank_StructPoints
                                                     where a.Active == true &&
                                                     (a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable
                                                      && a.FK_secondlvl == null
                                                      && a.FK_thirdlvl == null)
                                                     select a).ToList();
                double lsum3 = 0;
                double asum3 = 0;

                foreach (var tmp in calculate)
                {
                    if (tmp.Value.HasValue)
                        lsum3 = lsum3 + tmp.Value.Value;
                }
                foreach (var kaf in acadpoint)
                {
                    if (kaf.Value.HasValue)
                        asum3 = asum3 + kaf.Value.Value;
                }
                double allsum4 = 0;
                allsum4 = (lsum3 + asum3) / 2;
                point.Value = allsum4;
                ratingDB.SubmitChanges();
                
            }
            
        }

        public void CalculateStructParametrPoint(int paramId,  int userId)
        {
            UsersTable rights = (from item in ratingDB.UsersTable where item.UsersTableID == userId select item).FirstOrDefault();

            if (rights.AccessLevel == 1) // рейтинг кафедры
            {
                Rank_StructPoints point = (from a in ratingDB.Rank_StructPoints
                                           where a.Active == true && a.FK_parametr == paramId &&
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
                    point.Accept = false;
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
                        
                        Rank_UserParametrValue calculate = (from a in ratingDB.Rank_UserParametrValue
                                                            where a.Active == true && a.FK_parametr == paramId && a.FK_user == tmp.UsersTableID                                                     
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
                List<Rank_StructPoints> faculty = (from a in ratingDB.Rank_StructPoints
                                                   where a.Active == true && a.FK_secondlvl == rights.FK_SecondLevelSubdivisionTable                                             
                                                   select a).ToList();   
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
                    point.Value = reitingfac;
                    ratingDB.SubmitChanges();
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
                                                   where a.Active == true && a.FK_firstlvl == rights.FK_FirstLevelSubdivisionTable 
                                                   //&& a.Accept == true
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
                    point.Value = reitingacad;
                    ratingDB.SubmitChanges();
                }

            }
        }
        */
     
    }
}
 