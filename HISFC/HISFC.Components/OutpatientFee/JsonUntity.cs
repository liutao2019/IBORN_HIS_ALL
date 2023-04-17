using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FS.HISFC.Components.OutpatientFee
{
    public static class JsonUntity
    {
        //{E61C8FDD-81C2-434e-AF60-6B1BF1AF4CA2}

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
    }
}
