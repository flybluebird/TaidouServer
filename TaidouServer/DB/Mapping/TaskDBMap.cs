using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using TaidouCommon.Model;

namespace TaidouServer.DB.Mapping
{
    class TaskDBMap:ClassMap<TaskDB>
    {
        public TaskDBMap()
        {
            Id(x => x.ID).Column("id");
            Map(x => x.TaskID).Column("taskid");
            Map(x => x.TaskState).Column("state");
            Map(x => x.TaskType).Column("type");
            Map(x => x.LastUpdateDateTime).Column("lastupdatetime");
            References(x => x.Role).Column("roleid");
            Table("taskdb");
        }
    }
}
