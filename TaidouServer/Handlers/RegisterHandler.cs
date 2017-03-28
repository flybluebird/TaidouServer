using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using TaidouServer.DB.Manager;
using TaidouServer.Tools;

namespace TaidouServer.Handlers
{
    class RegisterHandler:HandlerBase
    {
        private UserManager manager;

        public RegisterHandler()
        {
            manager = new UserManager();
        }

        public override void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer, SendParameters sendParameters)
        {
            User user=ParameterTool.GetParameter<User>(request.Parameters, ParameterCode.User);
            User userDB=manager.GetUserByUsername(user.Username);

           

            if (userDB != null)
            {
                response.ReturnCode = (short) ReturnCode.Fall;
                response.DebugMessage = "用户名重复";
            }
            else
            {
                user.Password=MD5Tools.GetMD5(user.Password);
                response.ReturnCode = (short) ReturnCode.Success;
                manager.AddUser(user);
            }
        
        }

        public override OperationCode OpCode {
            get { return OperationCode.Register;} 
        }
    }
}
