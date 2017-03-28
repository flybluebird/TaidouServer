using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using TaidouServer.DB.Manager;

namespace TaidouServer.Handlers
{
    class SkillDBHandler:HandlerBase
    {
        private SkillDBManager skilldbManager;

        public SkillDBHandler()
        {
            skilldbManager=new SkillDBManager();
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subcode=ParameterTool.GetSubCode(request.Parameters);
            ParameterTool.AddSubCode(response.Parameters,subcode);
            switch (subcode)
            {
                case SubCode.AddSkillDB:
                    SkillDB skilldb=ParameterTool.GetParameter<SkillDB>(request.Parameters,ParameterCode.SkillDB);
                    skilldb.Role = peer.LoginRole;
                    skilldbManager.AddSkillDB(skilldb);
                    skilldb.Role = null;
                    ParameterTool.AddParameter(response.Parameters,ParameterCode.SkillDB,skilldb);
                    break;
                case SubCode.GetSkillDBList:
                    List<SkillDB> list =skilldbManager.Get(peer.LoginRole);
                    foreach (var temp in list)
                    {
                        temp.Role = null;
                    } 
                    ParameterTool.AddParameter(response.Parameters,ParameterCode.SkillDBList, list);
                    break;
                case SubCode.UpdateSkillDB:
                    SkillDB skilldb1=ParameterTool.GetParameter<SkillDB>(request.Parameters,ParameterCode.SkillDB);
                    skilldb1.Role = peer.LoginRole;
                    skilldbManager.UpdateSkillDB(skilldb1);
                    
                    break;
                case SubCode.UpgradeSkillDB:
                    SkillDB skilldb2=ParameterTool.GetParameter<SkillDB>(request.Parameters,ParameterCode.SkillDB);
                    Role role=ParameterTool.GetParameter<Role>(request.Parameters,ParameterCode.Role);
                    role.User = peer.LoginUser;
                    skilldb2.Role = role;
                    skilldbManager.UpgradeSkillDB(skilldb2,role);
                    skilldb2.Role = null;
                    ParameterTool.AddParameter(response.Parameters, ParameterCode.SkillDB, skilldb2);
                    break;
            }
        }

        public override OperationCode OpCode {
            get { return OperationCode.SkillDB;}
        }
    }
}
