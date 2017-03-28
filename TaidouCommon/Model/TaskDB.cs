using System;
using System.Collections.Generic;
using System.Text;

namespace TaidouCommon.Model
{
    public class TaskDB
    {
        public virtual int ID { get; set; }
        public virtual Role Role { get; set; }
        public virtual int TaskID { get; set; }
        public virtual int TaskType { get; set; }
        public virtual int TaskState { get; set; }
        public virtual DateTime LastUpdateDateTime { get; set; }
    }
}
