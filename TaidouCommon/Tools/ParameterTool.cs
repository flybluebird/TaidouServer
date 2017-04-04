using System;
using System.Collections.Generic;
using System.Text;
using LitJson;

namespace TaidouCommon.Tools
{
    public class ParameterTool
    {
        //将value从字典中取出
        public static T GetParameter<T>(Dictionary<byte,object> parameters, ParameterCode parameterCode,bool isObject=true )
        {
            object o = null;
            parameters.TryGetValue((byte)parameterCode,out o);
            if (isObject == false)
            {
                return (T) o;
            }
            return JsonMapper.ToObject<T>(o.ToString());
        }

        //将key,value存储到字典中
        public static void AddParameter<T>(Dictionary<byte, object> parameters, ParameterCode key, T value,
            bool isObject = true)
        {
            if (isObject)
            {
                string json=JsonMapper.ToJson(value);

                parameters.Add((byte)key,json);
            }
            else
            {
                parameters.Add((byte)key, value);
            }
        }

        //取出子操作
        public static SubCode GetSubCode(Dictionary<byte,object> parameters )
        {
            SubCode subcode=GetParameter<SubCode>(parameters,ParameterCode.SubCode,false);
            return subcode; 
        }

        //添加子操作
        public static void AddSubCode(Dictionary<byte,object> parameters,SubCode subcode )
        {
            AddParameter<SubCode>(parameters,ParameterCode.SubCode, subcode,false);
        }

        public static void AddOperationCodeSubCodeRoleId(Dictionary<byte,object> parameters,OperationCode operationCode,SubCode subCode,int RoleId)
        {
            if (parameters.ContainsKey((byte) ParameterCode.OperationCode))
            {
                parameters.Remove((byte) ParameterCode.OperationCode);
            }
            if (parameters.ContainsKey((byte) ParameterCode.SubCode))
            {
                parameters.Remove((byte) ParameterCode.SubCode);
            }
            if (parameters.ContainsKey((byte) ParameterCode.RoleID))
            {
                parameters.Remove((byte) ParameterCode.RoleID);
            }
            parameters.Add((byte) ParameterCode.OperationCode,operationCode);
            parameters.Add((byte) ParameterCode.SubCode,subCode);
            parameters.Add((byte) ParameterCode.RoleID,RoleId);
        }
    }
}
