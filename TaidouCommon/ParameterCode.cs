using System;
using System.Collections.Generic;
using System.Text;

namespace TaidouCommon
{
    //参数代码
    public enum ParameterCode:byte
    {
        ServerList,
        User,
        RoleList,
        Role,
        SubCode,
        OperationCode,
        TaskDB,
        TaskDBList,
        InventoryDBList,
        InventoryDB,
        SkillDB,
        SkillDBList,
        MasterRoleID,
        Position,//位置
        EulerAngles,//旋转
        RoleID,
        PlayerMoveAnimationModel,
        PlayerAnimationModel,
        CreateEnemyModel,
        SyncEnemyPositionModel,
        SyncEnemyAnimationModel
    }
}
