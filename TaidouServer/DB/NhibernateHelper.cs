using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace TaidouServer.DB
{
    public class NhibernateHelper
    {
        private static ISessionFactory _isessionFactory = null;

        private static void InitIsessionFactory()
        {
            _isessionFactory =
                Fluently.Configure()
                    .Database(
                        MySQLConfiguration.Standard.ConnectionString(
                            db => db.Server("localhost").Database("taidouserver").Username("root").Password("1234")))
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<NhibernateHelper>())
                    .BuildSessionFactory();
        }

        private static ISessionFactory IsessionFactory
        {
            get
            {
                if (_isessionFactory == null)
                    InitIsessionFactory();
                return _isessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return IsessionFactory.OpenSession();
        }
    }
}
