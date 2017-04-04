using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaidouCommon.Model;

namespace TaidouServer.DB.Manager
{
    class InventoryManager
    {
        public List<InventoryDB> GetInventoryDB(Role role)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    var res=session.QueryOver<InventoryDB>().Where(x=>x.Role==role);
                    transaction.Commit();
                    return (List<InventoryDB>) res.List<InventoryDB>();
                }
            }
        }

        public void AddInventoryDB(InventoryDB itemDb)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
     
                    session.Save(itemDb);
                    transaction.Commit();              
                }   
            }
        }

        public void ChangeInventoryDBList(List<InventoryDB> itemlist)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    foreach (var item in itemlist)
                    {
                        session.Update(itemlist);
                    }                 
                    transaction.Commit();
                    
                }
            }
        }

        public void UpdateInventoryDB(InventoryDB itemDb)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                    session.Update(itemDb);
                    transaction.Commit();
                }
            }
        }

        public void UpgradeEquip(InventoryDB itemdb2, Role role2)
        {
            using (var session=NhibernateHelper.OpenSession())
            {
                using (var transaction=session.BeginTransaction())
                {
                   
                    session.Update(itemdb2);
                    session.Update(role2);
                    transaction.Commit();
                }
            }
        }
    }
}
