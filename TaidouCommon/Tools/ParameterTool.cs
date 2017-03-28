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
        public static void AddParameter<T>( Dictionary<byte, object> parameters, ParameterCode key, T value,
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

        public static SubCode GetSubCode(Dictionary<byte,object> parameters )
        {
            SubCode subcode=GetParameter<SubCode>(parameters,ParameterCode.SubCode,false);
            return subcode; 
        }

        public static void AddSubCode(Dictionary<byte,object> parameters,SubCode subcode )
        {
            AddParameter<SubCode>(parameters,ParameterCode.SubCode, subcode,false);
        }
    }
}
