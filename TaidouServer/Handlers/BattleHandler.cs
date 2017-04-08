using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using TaidouServer.Tools;

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
                    if (TaidouServer.Instance.clientPeersForTeam.Count >= 1)
                    {
                        //取得集合中的前两个peer，和当前的peer进行组队
                        ClientPeer peer1 = TaidouServer.Instance.clientPeersForTeam[0];
                        Team t = new Team(peer1, peer);
                        TaidouServer.Instance.clientPeersForTeam.RemoveRange(0, 1);
                        List<Role> rolelist=new List<Role>();
                        foreach (var clientPeer in t.ClientPeers)
                        {
                            rolelist.Add(clientPeer.LoginRole);
                        }   
                        ParameterTool.AddParameter(response.Parameters,ParameterCode.RoleList, rolelist);
                        ParameterTool.AddParameter(response.Parameters,ParameterCode.MasterRoleID, t.masterRoleID,false);
                        response.ReturnCode = (short) ReturnCode.GetTeam;
             
                        SendEventByPeer(peer1, (OperationCode) response.OperationCode,SubCode.GetTeam, rolelist,t.masterRoleID);
                       
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
                case SubCode.SyncPositionAndRotation:
                    object objPos = null;
                    request.Parameters.TryGetValue((byte) ParameterCode.Position, out objPos);
                    object objEulerAngle = null;
                    request.Parameters.TryGetValue((byte)ParameterCode.EulerAngles,out objEulerAngle);

                    foreach (ClientPeer temp in peer.Team.ClientPeers)
                    {
                        //向其余的客户端发送请求
                        if (temp != peer)
                        {
                            SendPositionAndRotation(temp, OpCode, SubCode.SyncPositionAndRotation, peer.LoginRole.ID, objPos, objEulerAngle);
                        }
                    }
                
                    break;
                case SubCode.SyncPlayerMoveAnimation:
                    
                    RequestTool.TransmitRequest(peer,request,OpCode);
                    break;
                case SubCode.SyncPlayerAnimation:
                    RequestTool.TransmitRequest(peer,request,OpCode);
                    break;
            }
        }

        //向客户端发送同步的位置和旋转进行同步
        void SendPositionAndRotation(ClientPeer peer,OperationCode opcode,SubCode subcode,int roleid,object objPos,object objEulerAngles)
        {
            EventData eventData=new EventData();
            eventData.Parameters=new Dictionary<byte, object>();
            ParameterTool.AddParameter(eventData.Parameters,ParameterCode.OperationCode,opcode,false);
            ParameterTool.AddParameter(eventData.Parameters,ParameterCode.SubCode,subcode,false);
            eventData.Parameters.Add((byte) ParameterCode.RoleID,roleid);
            eventData.Parameters.Add((byte) ParameterCode.Position, objPos.ToString());
            eventData.Parameters.Add((byte) ParameterCode.EulerAngles, objEulerAngles.ToString()); 
            peer.SendEvent(eventData, new SendParameters()); 
        }

        //发起组队
        void SendEventByPeer(ClientPeer peer,OperationCode opCode,SubCode subcode,List<Role> rolelist,int masterRoleID)
        {
            EventData eventData=new EventData();
            eventData.Parameters = new Dictionary<byte, object>();
            ParameterTool.AddParameter(eventData.Parameters, ParameterCode.OperationCode, opCode, false);
            ParameterTool.AddParameter(eventData.Parameters,ParameterCode.MasterRoleID, masterRoleID,false);
            ParameterTool.AddParameter(eventData.Parameters,ParameterCode.RoleList,rolelist);
            ParameterTool.AddSubCode(eventData.Parameters,subcode);

            peer.SendEvent(eventData, new SendParameters());
        }

        public override OperationCode OpCode {
            get { return OperationCode.Battle;}
        }
    }
}
