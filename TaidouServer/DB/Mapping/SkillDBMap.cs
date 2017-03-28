using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TaidouCommon.Model;

namespace TaidouServer.DB.Mapping
{
    class SkillDBMap:ClassMap<SkillDB>
    {
        public SkillDBMap()
        {
            Id(x => x.ID).Column("id");
            Map(x => x.SkillId).Column("skillid");
            Map(x => x.Level).Column("level");
            References(x => x.Role).Column("roleid");
            Table("skilldb");
        }
    }
}
