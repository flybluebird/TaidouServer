using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Tools;

namespace TaidouServer.Tools
{
    public class RequestTool
    {
        public static void TransmitRequest(ClientPeer peer, OperationRequest request,OperationCode opcode)
        {
            foreach (var temp in peer.Team.ClientPeers)
            {
                if (temp != peer)
                {
                    EventData eventData = new EventData();
                    eventData.Parameters = request.Parameters;
                    ParameterTool.AddOperationCodeSubCodeRoleId(eventData.Parameters, opcode, peer.LoginRole.ID); 
                    temp.SendEvent(eventData, new SendParameters());
                }
            }
        }
    }
}
