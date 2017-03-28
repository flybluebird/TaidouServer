using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaidouCommon.Model;

namespace TaidouServer.DB.Manager
{
    public class TaskDBManager
    {
        public List<TaskDB> GetTaskDbList(Role role)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transcation=session.BeginTransaction())
                {
                    var res=session.QueryOver<TaskDB>().Where(x=>x.Role==role);
                    TaidouServer.log.Debug(role.Name);
                   
                    transcation.Commit();
                    //return (List<TaskDB>)res.List();
                    return  (List<TaskDB>)res.List<TaskDB>();
                }   
            }
        }

        public void AddTaskDB(TaskDB taskdb)
        {
            using (var session = NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    taskdb.LastUpdateDateTime=new DateTime();
                    session.Save(taskdb);
                    transaction.Commit();
                    
                }
            }
        }

        public void UpdateTaskDB(TaskDB taskdb)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    taskdb.LastUpdateDateTime=new DateTime();
                    session.Update(taskdb);
                    transaction.Commit();
                }
            }
        }
    }
}
