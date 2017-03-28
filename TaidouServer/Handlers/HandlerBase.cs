using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TaidouCommon;
using ILogger = ExitGames.Logging.ILogger;

namespace TaidouServer.Handlers
{
    public abstract class HandlerBase
    {
        private static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        protected HandlerBase()
        {
            TaidouServer.Instance.handlers.Add((byte)OpCode, this);
            log.Debug("Handler"+this.GetType().Name+"is register.");
        }

        public abstract void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer,SendParameters sendParameters);

        public abstract OperationCode OpCode { get; }
    }
}
