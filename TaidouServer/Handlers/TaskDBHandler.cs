
using System.Collections.Generic;

using Photon.SocketServer;
using TaidouCommon;
using TaidouCommon.Model;
using TaidouCommon.Tools;
using TaidouServer.DB.Manager;

namespace TaidouServer.Handlers
{
    public class TaskDBHandler:HandlerBase
    {
        TaskDBManager taskdbManager;

        public TaskDBHandler()
        {
            taskdbManager=new TaskDBManager();
        }

        public override void OnHandlerMessage(OperationRequest request,OperationResponse response, ClientPeer peer, SendParameters sendParameters)
        {
            SubCode subCode=ParameterTool.GetParameter<SubCode>(request.Parameters,ParameterCode.SubCode,false);
            
            response.Parameters.Add((byte) ParameterCode.SubCode,subCode);


            switch (subCode)
            {
                
                case SubCode.AddTaskDB:
                    TaskDB taskdb = ParameterTool.GetParameter<TaskDB>(request.Parameters, ParameterCode.TaskDB);
                    taskdb.Role = peer.LoginRole;
                    taskdbManager.AddTaskDB(taskdb);
                    taskdb.Role = null;
                    ParameterTool.AddParameter(response.Parameters,ParameterCode.TaskDB, taskdb);
                    response.ReturnCode = (short)ReturnCode.Success;
                    break;
                case SubCode.GetTaskDB:
                    List<TaskDB> list = taskdbManager.GetTaskDbList(peer.LoginRole);
                    foreach (var taskdbtemp in list)
                    {
                        TaidouServer.log.Debug(taskdbtemp.TaskID);
                        taskdbtemp.Role = null;
                    }
                    ParameterTool.AddParameter(response.Parameters, ParameterCode.TaskDBList, list);
                    response.ReturnCode = (short)ReturnCode.Success;
                    break;
                case SubCode.UpdateTaskDB:
                    TaskDB taskdb1=ParameterTool.GetParameter<TaskDB>(request.Parameters,ParameterCode.TaskDB);
                    taskdb1.Role = peer.LoginRole;
                    taskdbManager.UpdateTaskDB(taskdb1);
                    response.ReturnCode = (short) ReturnCode.Success;
                    break;
            }
        }

        public override OperationCode OpCode
        {
            get { return OperationCode.TaskDB; }
        }
    }
}
