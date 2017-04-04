using System;
using System.Collections.Generic;

using System.Text;


namespace TaidouCommon
{
    //操作代码
    public enum OperationCode:byte
    {
        Login,
        GetServer,
        Register,
        Role,
        SubCode,
        TaskDB,
        InventoryDB,
        SkillDB,
        Battle,
        Enemy
    }
}
