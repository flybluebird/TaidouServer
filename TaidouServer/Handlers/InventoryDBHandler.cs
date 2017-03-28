using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Testing.Values;
using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using TaidouServer.DB.Manager;

namespace TaidouServer.Handlers
{
    public class InventoryDBHandler:HandlerBase
    {
        private InventoryManager inventoryManager;

        public InventoryDBHandler()
        {
            inventoryManager=new InventoryManager();
        }

        public override void OnHandlerMessage(OperationRequest request, OperationResponse response, ClientPeer peer,SendParameters sendParameters)
        {
            SubCode subcode=ParameterTool.GetParameter<SubCode>(request.Parameters,ParameterCode.SubCode,false);
            ParameterTool.AddParameter(response.Parameters,ParameterCode.SubCode, subcode,false);
            switch (subcode)
            {
                case SubCode.GetInventoryDB:
                    Role role = peer.LoginRole;
                    List<InventoryDB> list=inventoryManager.GetInventoryDB(role);
                    foreach (var inventorydb in list)
                    {
                        inventorydb.Role = null;
                    }
                    ParameterTool.AddParameter(response.Parameters,ParameterCode.InventoryDBList, list);
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
                case SubCode.AddInventoryDB:
                    TaidouServer.log.Debug("在此将物品添加到数据库中");
                    InventoryDB itemDb=ParameterTool.GetParameter<InventoryDB>(request.Parameters,ParameterCode.InventoryDB);
                    itemDb.Role = peer.LoginRole; 
                    TaidouServer.log.Debug(itemDb.Level);
                    inventoryManager.AddInventoryDB(itemDb); 
                    itemDb.Role = null;
                    ParameterTool.AddParameter(response.Parameters,ParameterCode.InventoryDB,itemDb);
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
                case SubCode.UpdateInventoryDB:
                    InventoryDB itemdb1=ParameterTool.GetParameter<InventoryDB>(request.Parameters,ParameterCode.InventoryDB);
                    itemdb1.Role = peer.LoginRole;
                    inventoryManager.UpdateInventoryDB(itemdb1);
                    break;
                case SubCode.ChangeInventoryDBList:
                    List<InventoryDB> itemList =ParameterTool.GetParameter<List<InventoryDB>>(request.Parameters,ParameterCode.InventoryDBList);
                    foreach (var temp in itemList)
                    {
                        temp.Role = peer.LoginRole;
                    }
                    inventoryManager.ChangeInventoryDBList(itemList);
                    break;
                case SubCode.UpgradeEquip:
                    InventoryDB itemdb2=ParameterTool.GetParameter<InventoryDB>(request.Parameters,ParameterCode.InventoryDB);
                    Role role2=ParameterTool.GetParameter<Role>(request.Parameters,ParameterCode.Role);
                    peer.LoginRole = role2;
                    role2.User = peer.LoginUser;
                    itemdb2.Role = role2;
                    inventoryManager.UpgradeEquip(itemdb2,role2);
                    break;
            }
        }

        public override OperationCode OpCode {
            get { return OperationCode.InventoryDB;} 
        }
    }
}
