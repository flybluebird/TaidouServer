using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Threading;
using TaidouCommon.Model;

namespace TaidouServer.DB.Manager
{
    public class SkillDBManager
    {
        public void AddSkillDB(SkillDB skilldb)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    session.Save(skilldb);
                    transaction.Commit();
                }
            }
        }

        public void UpdateSkillDB(SkillDB skilldb)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    session.Update(skilldb);
                    transaction.Commit();
                }
            }
        }

        public void UpgradeSkillDB(SkillDB skilldb,Role role)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    TaidouServer.log.Debug(skilldb.Level);
                    session.SaveOrUpdate(skilldb);
                    session.Update(role);
                    transaction.Commit();
                }
            }
        }

        public List<SkillDB> Get(Role role)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    var res=session.QueryOver<SkillDB>().Where(x=>x.Role==role);
                    transaction.Commit();
                    foreach (var temp in res.List<SkillDB>())
                    {
                        TaidouServer.log.Debug(temp.ID);
                    }
                    return (List<SkillDB>) res.List<SkillDB>(); 
                }
            }
        }
    }
}
