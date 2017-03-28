using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TaidouCommon.Model;

namespace TaidouServer.DB.Mapping
{
    class RoleMap:ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.ID).Column("id");
            Map(x => x.Name).Column("name");
            Map(x => x.Level).Column("level");
            Map(x => x.Isman).Column("isman");
            Map(x => x.Exp).Column("exp");
            Map(x => x.Diamond).Column("diamond");
            Map(x => x.Coin).Column("coin");
            Map(x => x.Energy).Column("energy");
            Map(x => x.Thoughen).Column("thoughen");
            References(x => x.User).Column("userid");
            Table("role");
        }
    }
}
