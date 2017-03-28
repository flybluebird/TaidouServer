using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;

namespace TaidouServer.Handlers
{
    class BattleHandler:HandlerBase
    {
        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer,SendParameters sendParameters)
        {
            SubCode subcode=ParameterTool.GetSubCode(request.Parameters);
            ParameterTool.AddSubCode(response.Parameters,subcode);

            switch (subcode)
            {
                case SubCode.SendTeam:
                    if (TaidouServer.Instance.clientPeersForTeam.Count >= 2)
                    {
                        //取得集合中的前两个peer，和当前的peer进行组队
                        ClientPeer peer1 = TaidouServer.Instance.clientPeersForTeam[0];
                        ClientPeer peer2 = TaidouServer.Instance.clientPeersForTeam[1];
                        Team t=new Team(peer1,peer2,peer);
                        TaidouServer.Instance.clientPeersForTeam.RemoveRange(0,2);
                        List<Role> rolelist=new List<Role>();
                        foreach (var clientPeer in t.ClientPeers)
                        {
                            rolelist.Add(clientPeer.LoginRole);
                        }
                        ParameterTool.AddParameter(response.Parameters,ParameterCode.RoleList, rolelist);
                        response.ReturnCode = (short) ReturnCode.GetTeam;
                        SendRequestByPeer(peer1, subcode, rolelist, sendParameters);
                        SendRequestByPeer(peer2,subcode,rolelist,sendParameters);
                    }
                    else
                    {
                        //当前客户端人数不足的时候，把自身加载进来等待组队
                        TaidouServer.Instance.clientPeersForTeam.Add(peer);
                        response.ReturnCode = (short) ReturnCode.WaitTeam;
                    }
                    break;
                case SubCode.CancelTeam:
                    TaidouServer.Instance.clientPeersForTeam.Remove(peer);
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
            }
        }

        void SendRequestByPeer(ClientPeer peer,SubCode subcode,List<Role> rolelist,SendParameters sendParameters )
        {
            OperationResponse response=new OperationResponse();
            response.Parameters=new Dictionary<byte, object>();
            ParameterTool.AddSubCode(response.Parameters,subcode);
            ParameterTool.AddParameter(response.Parameters,ParameterCode.RoleList,rolelist);
            response.ReturnCode = (short) ReturnCode.GetTeam;
            peer.SendOperationResponse(response,sendParameters);
        }

        public override OperationCode OpCode {
            get { return OperationCode.Battle;}
        }
    }
}
