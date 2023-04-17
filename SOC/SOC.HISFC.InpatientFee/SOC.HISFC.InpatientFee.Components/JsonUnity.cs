using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bussiness.Util
{
    //{962111D3-7FAB-470b-8941-1A6D16FEEFD2}
    public static class JsonUntity
    {
        /// <summary>
        /// 将字典类型序列化为json字符串
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="dict">要序列化的字典数据</param>
        /// <returns>json字符串</returns>
        public static string SerializeDictionaryToJsonString<TKey, TValue>(Dictionary<TKey, TValue> dict)
        {
            if (dict.Count == 0)
                return "";

            string jsonStr = JsonConvert.SerializeObject(dict);
            return jsonStr;
        }


        //{5188534A-2ABB-4BD1-8960-83C83C812793}
        /// <summary>
        /// 将类序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static string SerializeEntity<T>(T dict)
        {
            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            string jsonStr = JsonConvert.SerializeObject(dict, Formatting.Indented, jsonSetting);
            return jsonStr;
        }

        public static T DeSerializeEntity<T>(string dict)
        {
            var jsonSetting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };

            T jsonStr = JsonConvert.DeserializeObject<T>(dict);//DeSerializeObject(dict, Formatting.Indented, jsonSetting);
            return jsonStr;
        }

        /// <summary>
        /// 将json字符串反序列化为字典类型
        /// </summary>
        /// <typeparam name="TKey">字典key</typeparam>
        /// <typeparam name="TValue">字典value</typeparam>
        /// <param name="jsonStr">json字符串</param>
        /// <returns>字典数据</returns>
        public static Dictionary<TKey, TValue> DeserializeStringToDictionary<TKey, TValue>(string jsonStr)
        {
            if (string.IsNullOrEmpty(jsonStr))
                return new Dictionary<TKey, TValue>();

            Dictionary<TKey, TValue> jsonDict = JsonConvert.DeserializeObject<Dictionary<TKey, TValue>>(jsonStr);

            return jsonDict;

        }
    }
}
