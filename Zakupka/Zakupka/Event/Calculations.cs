using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zakupka.Event
{
    public class Calculations
    {
        public DataValidator validator = new DataValidator();
        ZakupkaDBDataContext zakupkaDB = new ZakupkaDBDataContext();
        public void CalculateMIyProject(int projectId ,int eventId)
        {
            #region считается и записывается СМЕТА ПРОЕКТА
            ProjectsValues smeta = (from a in zakupkaDB.ProjectsValues
                                    where a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                    join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                    where b.engname == "smeta"
                                    select a).FirstOrDefault();
            if (smeta == null)
            {
                int id = (from b in zakupkaDB.Fields where b.engname == "smeta" select b.filedID).FirstOrDefault();
                smeta = new ProjectsValues();
                smeta.active = true;
                smeta.fk_field = id;
                smeta.fk_event = eventId;
                smeta.fk_project = projectId;
                zakupkaDB.ProjectsValues.InsertOnSubmit(smeta);
                zakupkaDB.SubmitChanges();             
            }
            List<ProjectsValues> valueList = (from a in zakupkaDB.ProjectsValues
                                              where a.active == true && a.fk_field == null
                                               && a.fk_kosgu != null && a.fk_project == projectId && a.fk_event == eventId
                                              select a).ToList();
            Decimal smet = 0;
            if (valueList.Count != 0)
            {
               
                foreach (var n in valueList)
                {
                    if (n.value != "")
                        smet = smet + Convert.ToDecimal(n.value);
                }
                smeta.value = smet.ToString();
                zakupkaDB.SubmitChanges();
            }
            else
            {
                smeta.value = "";
                zakupkaDB.SubmitChanges();
            }
            #endregion
            #region ЗАКОНТРАКТОВАНО

            ProjectsValues zakontract = (from a in zakupkaDB.ProjectsValues
                                         where a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                         join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                         where b.engname == "zakontract"
                                         select a).FirstOrDefault();
            if (zakontract == null)
            {
                int id = (from b in zakupkaDB.Fields where b.engname == "zakontract" select b.filedID).FirstOrDefault();
                zakontract = new ProjectsValues();
                zakontract.active = true;
                zakontract.fk_field = id;
                zakontract.fk_event = eventId;
                zakontract.fk_project = projectId;
                zakupkaDB.ProjectsValues.InsertOnSubmit(zakontract);
                zakupkaDB.SubmitChanges();           
            }
                List<CollectedValues> allcontractkosgu1 = (from a in zakupkaDB.CollectedValues
                                                           where a.active == true && a.fk_project == projectId && a.fk_event == eventId && a.fk_kosgu != null
                                                           join c in zakupkaDB.ContractProjectMappingTable on a.fk_contract equals c.fk_contract
                                                           where c.Active == true && c.fk_project == projectId
                                                           select a).ToList();
                Decimal sum1 = 0;
                foreach (var n in allcontractkosgu1)
                {
                    if (n.value != "")
                    sum1 = sum1 + Convert.ToDecimal(n.value);
                }
                Decimal sumkosgu1 = smet - sum1;
                zakontract.value = sum1.ToString();
                zakupkaDB.SubmitChanges();
            #endregion
            #region СУММА ПРЕВЫШЕНИЯ
            ProjectsValues summa = (from a in zakupkaDB.ProjectsValues
                                    where a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                    join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                    where b.engname == "summa"
                                    select a).FirstOrDefault();
            if (summa == null)
            {
                int id = (from b in zakupkaDB.Fields where b.engname == "summa" select b.filedID).FirstOrDefault();
                summa = new ProjectsValues();
                summa.active = true;
                summa.fk_field = id;
                summa.fk_event = eventId;
                summa.fk_project = projectId;
                zakupkaDB.ProjectsValues.InsertOnSubmit(summa);
                zakupkaDB.SubmitChanges();             
            }

                Decimal sumkosgu = sum1 - smet;
                if (sumkosgu < 0)
                 sumkosgu = 0;

                summa.value = sumkosgu.ToString();
                zakupkaDB.SubmitChanges();

            #endregion
            #region ОПЛАЧЕНО ПО ДОГОВОРАМ
            ProjectsValues bycontract = (from a in zakupkaDB.ProjectsValues
                                         where a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                         join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                         where b.engname == "paybycontract" && b.staticvalue == true
                                         select a).FirstOrDefault();
            if (bycontract == null)
            {
                int id = (from b in zakupkaDB.Fields where b.engname == "paybycontract" select b.filedID).FirstOrDefault();
                bycontract = new ProjectsValues();
                bycontract.active = true;
                bycontract.fk_field = id;
                bycontract.fk_event = eventId;
                bycontract.fk_project = projectId;
                zakupkaDB.ProjectsValues.InsertOnSubmit(bycontract);
                zakupkaDB.SubmitChanges();
            }
                List<CollectedValues> factpay = (from a in zakupkaDB.CollectedValues
                                                 join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                                 where b.engname == "factpay"  
                                                 join c in zakupkaDB.ContractProjectMappingTable on a.fk_contract equals c.fk_contract
                                                 where c.Active == true && c.fk_project == projectId
                                                 && a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                                 select a).ToList();
                Decimal oplacheno = 0;
                foreach (var n in factpay)
                {
                    if(n.value != "")                      
                    oplacheno = oplacheno + Convert.ToDecimal(n.value);
                }
                bycontract.value = oplacheno.ToString();
                zakupkaDB.SubmitChanges();
            #endregion
            #region ОСТАТОК ПО ДОГОВОРАМ
            ProjectsValues ostatok = (from a in zakupkaDB.ProjectsValues
                                      where a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                      join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                      where b.engname == "leftbycontract" && b.staticvalue == true
                                      select a).FirstOrDefault();
            if (ostatok == null)
            {
                int id = (from b in zakupkaDB.Fields where b.engname == "leftbycontract" select b.filedID).FirstOrDefault();
                ostatok = new ProjectsValues();
                ostatok.active = true;
                ostatok.fk_field = id;
                ostatok.fk_event = eventId;
                ostatok.fk_project = projectId;
                zakupkaDB.ProjectsValues.InsertOnSubmit(ostatok);
                zakupkaDB.SubmitChanges();
            }
                List<CollectedValues> contractostatok = (from a in zakupkaDB.CollectedValues
                                                         join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                                         where b.engname == "contractleft" 
                                                         join c in zakupkaDB.ContractProjectMappingTable on a.fk_contract equals c.fk_contract
                                                         where c.Active == true && c.fk_project == projectId
                                                         && a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                                         select a).ToList();
            
                Decimal ost = 0;
                foreach (var n in contractostatok)
                {
                    if (n.value != "")
                    ost = ost + Convert.ToDecimal(n.value);
                }
                ostatok.value = ost.ToString();
                zakupkaDB.SubmitChanges();

            #endregion
            #region ОСТАТОК ПО ПРОЕКТУ
            ProjectsValues ostbypr = (from a in zakupkaDB.ProjectsValues
                                      where a.active == true && a.fk_project == projectId && a.fk_event == eventId
                                      join b in zakupkaDB.Fields on a.fk_field equals b.filedID
                                      where b.engname == "leftbyproject" && b.staticvalue == true
                                      select a).FirstOrDefault();
            if (ostbypr == null)
            {
                int id = (from b in zakupkaDB.Fields where b.engname == "leftbyproject" select b.filedID).FirstOrDefault();
                ostbypr = new ProjectsValues();
                ostbypr.active = true;
                ostbypr.fk_field = id;
                ostbypr.fk_event = eventId;
                ostbypr.fk_project = projectId;
                zakupkaDB.ProjectsValues.InsertOnSubmit(ostbypr);
                zakupkaDB.SubmitChanges();
            }
            
            Decimal ostatokProject = smet - oplacheno;
            ostbypr.value = ostatokProject.ToString();
            zakupkaDB.SubmitChanges();

            #endregion
        }
    }
}