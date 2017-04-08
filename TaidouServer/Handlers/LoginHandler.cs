using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LitJson;
using NHibernate.Impl;
using Photon.SocketServer;
using Photon.SocketServer.Rpc;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouServer.DB.Manager;
using TaidouServer.Tools;

namespace TaidouServer.Handlers
{
    

    class LoginHandler:HandlerBase
    {
        private UserManager manager;

        public LoginHandler()
        {
            manager=new UserManager();
        }

        public override void OnHandlerMessage(OperationRequest request,OperationResponse response,ClientPeer peer,SendParameters sendParameters)
        {
            
            Dictionary<byte, object> parameters = request.Parameters;
            object userObj=null;
            parameters.TryGetValue((byte)ParameterCode.User,out userObj);
            User user=JsonMapper.ToObject<User>(userObj.ToString());
            //由名字得到数据库中的user对象
            User userDB=manager.GetUserByUsername(user.Username);
            //数据库中存在对象，并且密码输入正确
            if (userDB != null && userDB.Password == MD5Tools.GetMD5(user.Password))
            {
                //登录取得成功
                response.ReturnCode = (short) ReturnCode.Success;
                peer.LoginUser = userDB;
            } 
            else
            {
                response.ReturnCode = (short) ReturnCode.Fall;
                response.DebugMessage = "用户名或密码错误";
            }
          
        }

        public override OperationCode OpCode {
            get { return OperationCode.Login;} 
        }
    }
}
