using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Competitions
{
    public class MainCompetitionFunctions
    {

    }
    public class DataBaseSimilarRequests
    {
        private readonly CompetitionDataContext _competitionDataBase = new CompetitionDataContext();
        public zBlockTable GetBlockTableById(int blockId)
        {
            return (from a in _competitionDataBase.zBlockTable
                where a.ID == blockId
                select a).FirstOrDefault();
        }
        public List<zSectionTable> GetSectionsListInBlock(int blockId, int applicationId)
        {
            return (from a in _competitionDataBase.zSectionTable
                where a.Active == true
                      && a.FK_BlockID == blockId
                      join b in _competitionDataBase.zCompetitionsTable
                      on a.FK_CompetitionsTable equals b.ID
                      where b.Active == true
                      join c in _competitionDataBase.zApplicationTable
                      on b.ID equals c.FK_CompetitionTable
                      where c.Active == true
                      && c.ID == applicationId
                select a).Distinct().ToList();
        }
        public List<zColumnTable> GetColumnsListInSection(int sectionId)
        {
            return (from a in _competitionDataBase.zColumnTable
                where a.Active == true
                      && a.FK_SectionTable == sectionId
                select a).Distinct().ToList();
        }
    }
}