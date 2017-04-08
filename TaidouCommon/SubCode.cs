using System;
using System.Collections.Generic;
using System.Text;

namespace TaidouCommon
{
    public enum SubCode
    {
        GetRole,
        AddRole,
        UpdateRole,
        SelectRole,
        GetTaskDB,
        AddTaskDB,
        UpdateTaskDB,
        GetInventoryDB,
        AddInventoryDB,
        UpdateInventoryDB,
        ChangeInventoryDBList,
        UpgradeEquip,
        AddSkillDB,
        UpdateSkillDB,
        UpgradeSkillDB,
        GetSkillDBList,
        SendTeam,//组队的请求
        CancelTeam,//取消组队请求
        GetTeam,//组队成功
        SyncPositionAndRotation,//同步位置和旋转
        SyncPlayerMoveAnimation,//同步角色动画
        CreateEnemy,//创建敌人
        SyncEnemyPosition,//同步敌人位置
        SyncEnemyAnimation,//同步敌人动画
        SyncPlayerAnimation//同步主角的动画
    }
}
