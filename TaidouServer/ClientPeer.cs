using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExitGames.Logging;
using log4net;
using Photon.SocketServer;
using Photon.SocketServer.ServerToServer;
using PhotonHostRuntimeInterfaces;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouServer.Handlers;

namespace TaidouServer
{
    public class ClientPeer:PeerBase
    {
        public User LoginUser { get; set; }//存储当前登录的user的账号 
        public Role LoginRole { get; set; }//存储当前登陆的角色
        public Team team { get; set; }

        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public ClientPeer(IRpcProtocol protocol, IPhotonPeer unmanagedPeer) : base(protocol, unmanagedPeer)
        {
        }

        //取得client端传过来的要求，并且加以处理
        protected override void OnOperationRequest(OperationRequest operationRequest, SendParameters sendParameters)
        {
            HandlerBase handler;
            TaidouServer.Instance.handlers.TryGetValue(operationRequest.OperationCode,out handler);//由operationcode,得到要进行操作的具体handler
            OperationResponse response=new OperationResponse(); 
            response.OperationCode = operationRequest.OperationCode;
            response.Parameters=new Dictionary<byte, object>();
            if (handler != null)
            {
                handler.OnHandlerMessage(operationRequest,response,this,sendParameters);
                SendOperationResponse(response,sendParameters);//向client发送响应
            }
            else
            {
                log.Debug("Can't find operation code:"+operationRequest.OperationCode);
            }
        }
        //失去连接时，处理的事项
        protected override void OnDisconnect(DisconnectReason reasonCode, string reasonDetail)
        {
            if (TaidouServer.Instance.clientPeersForTeam.Contains(this))
            {
                TaidouServer.Instance.clientPeersForTeam.Remove(this);
            }
            log.Debug("a client is disconnect!"+reasonCode);
        } 
    }
}
