using System;
using System.Collections.Generic;

using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using TaidouServer.DB.Manager;

namespace TaidouServer.Handlers
{
    class RoleHandler:HandlerBase
    {
        private RoleManager manager=null;

        public RoleHandler()
        {
            manager=new RoleManager();
            
        }

        public override void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer, SendParameters sendParameters)
        {
            //得到当前操作的子代码，根据不同的操作，进行不同的查询
            SubCode subCode=ParameterTool.GetParameter<SubCode>(request.Parameters, ParameterCode.SubCode,false);
            
            Dictionary<byte, object> parameters = response.Parameters;
            parameters.Add((byte)ParameterCode.SubCode,subCode);

            response.OperationCode = request.OperationCode;
            response.Parameters = parameters;

            switch (subCode)
            {
                case SubCode.AddRole:
                    Role role = ParameterTool.GetParameter<Role>( request.Parameters, ParameterCode.Role);
                    role.User = peer.LoginUser;
                    manager.AddRole(role);
                    role.User = null;
                    ParameterTool.AddParameter(parameters, ParameterCode.Role, role);
                    break;
                case SubCode.GetRole:
                    List<Role> rolelist=manager.GetRoleListByUser(peer.LoginUser);
                    foreach (var role1 in rolelist)
                    {
                        role1.User = null;
                    }
                    ParameterTool.AddParameter(parameters, ParameterCode.RoleList, rolelist); 
                    
                    break;
                case SubCode.SelectRole:
                    peer.LoginRole=ParameterTool.GetParameter<Role>(request.Parameters,ParameterCode.Role);
                    break;
                    case SubCode.UpdateRole:
                    Role role2=ParameterTool.GetParameter<Role>(request.Parameters,ParameterCode.Role);
                    role2.User = peer.LoginUser;
                    manager.UpdateRole(role2);
                    role2.User = null;

                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
            }
           
        }

        public override OperationCode OpCode {
            get { return OperationCode.Role;}
        }
    } 
}
