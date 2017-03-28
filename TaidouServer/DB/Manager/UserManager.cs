﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Threading;
using NHibernate;
using TaidouCommon.Model;

namespace TaidouServer.DB.Manager
{
    public class UserManager
    {
        public User GetUserByUsername(string username)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    var res = session.QueryOver<User>().Where(x => x.Username == username);
                    transaction.Commit();
                    if (res.List() != null && res.List().Count > 0)
                    {
                        return res.List()[0];
                    }
                    return null;
                }
            }
        }

        public void AddUser(User user)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transcation=session.BeginTransaction())
                {
                    session.Save(user);
                    transcation.Commit();
                }
            }
        }
    }
}
