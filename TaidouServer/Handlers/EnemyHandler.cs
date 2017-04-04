using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Tools;

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
                    foreach (var temp in peer.Team.ClientPeers)
                    {
                        if (temp != peer)
                        {
                            EventData eventData=new EventData();
                            eventData.Parameters = request.Parameters;
                            ParameterTool.AddOperationCodeSubCodeRoleId(eventData.Parameters,OpCode,subcode,peer.LoginRole.ID);
                            temp.SendEvent(eventData, new SendParameters());
                        }
                    }
                    break;
            }
        }

        public override OperationCode OpCode {
            get { return OperationCode.Enemy;} 
        }
    }
}
