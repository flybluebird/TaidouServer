using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using TaidouCommon.Model;

namespace TaidouServer.DB.Manager
{
    class ServerpropertyManager
    {
        //得到服务器的列表
        public List<ServerProperty> GetServerList()
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var serverlist=session.QueryOver<ServerProperty>();
                    transaction.Commit();
                    return (List<ServerProperty>)serverlist.List<ServerProperty>();
                }
            }
        }
    }
}
