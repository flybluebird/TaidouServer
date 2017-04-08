using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Tools;
using TaidouServer.Tools;

namespace TaidouServer.Handlers
{
    class EnemyHandler:HandlerBase
    {
        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer,
            SendParameters sendParameters)
        {
            SubCode subcode=ParameterTool.GetSubCode(request.Parameters);

            switch (subcode)
            {
                case SubCode.CreateEnemy: 
                    RequestTool.TransmitRequest(peer,request,OpCode);
                    break;
                case SubCode.SyncEnemyPosition:
                    RequestTool.TransmitRequest(peer, request, OpCode);
                    break;
                case SubCode.SyncEnemyAnimation:
                    RequestTool.TransmitRequest(peer, request, OpCode);
                    break;
            }
        }

        

        public override OperationCode OpCode {
            get { return OperationCode.Enemy;} 
        }
    }
}
