using System;
using System.Collections.Generic;
using System.Text;

namespace TaidouCommon.Model
{
    public class SkillDB
    {
        public virtual int ID { get; set; }
        public virtual int SkillId { get; set; }
        public virtual Role Role { get; set; }
        public virtual int Level { get; set; }
    }
}
