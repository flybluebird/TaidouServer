using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Photon.SocketServer;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net;
using log4net.Config;
using TaidouCommon;
using TaidouServer.Handlers;

namespace TaidouServer
{
    class TaidouServer:ApplicationBase
    {
        private static TaidouServer _instance;

        public static readonly ILogger log = ExitGames.Logging.LogManager.GetCurrentClassLogger();

        public Dictionary<byte, HandlerBase> handlers = new Dictionary<byte, HandlerBase>();
        public List<ClientPeer> clientPeersForTeam=new List<ClientPeer>();

        public TaidouServer()
        {
            _instance = this;
            RegisterHandlers();
        }

        public static TaidouServer Instance
        {
            get { return _instance; }
        }

        //建立连线，并且回传给PhotonServer
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            return new ClientPeer(initRequest.Protocol,initRequest.PhotonPeer);
        }

        //初始化gameserver
        protected override void Setup()
        {
            //日志的配置文件
            ExitGames.Logging.LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance);
            GlobalContext.Properties["Photon:ApplicationLogPath"] = Path.Combine(this.ApplicationRootPath, "log");
            GlobalContext.Properties["LogFileName"] = "MS" + this.ApplicationName;
            XmlConfigurator.ConfigureAndWatch(new FileInfo(Path.Combine(this.BinaryPath, "log4net.config")));

            log.Debug("Application is connectted!");
        }

        void RegisterHandlers()
        {
            //handlers.Add((byte)OperationCode.Login,new LoginHandler());//把loginhandler交给taidouServer进行管理
            //handlers.Add((byte)OperationCode.GetServer,new ServerHandler());
            //handlers.Add((byte)OperationCode.Register,new RegisterHandler());

            Type[] types=Assembly.GetAssembly(typeof(HandlerBase)).GetTypes();
            foreach (var type in types)
            {
                if (type.FullName.EndsWith("Handler"))
                {
                    Activator.CreateInstance(type); 
                }
            }
        }
        //关闭gamesrever,并且释放资源
        protected override void TearDown()
        {
            log.Debug("Application tear down.");
        }
    }
}
