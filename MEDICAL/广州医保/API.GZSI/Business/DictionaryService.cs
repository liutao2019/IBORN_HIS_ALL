using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
    /// <summary>
    /// 码表下载
    /// </summary>
    public class Dictionary1901 : AbstractService<Models.Request.RequestGzsiModel1901, Models.Response.ResponseGzsiModel1901>
    {
        public override string InterfaceID
        {
            get
            {
                return "1901";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1901 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1901 t, ref Models.Response.ResponseGzsiModel1901 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 文件上传
    /// </summary>
    public class Dictionary91010 : AbstractService<string, Models.Response.ResponseGzsiModel9101>
    {
        public override string InterfaceID
        {
            get
            {
                return "9101";
            }
        }

        protected override string ConvertModelToSendMessage(string t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, string t, ref Models.Response.ResponseGzsiModel9101 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 文件上传
    /// </summary>
    public class Dictionary9101 : AbstractService<Models.Request.RequestGzsiModel9101, Models.Response.ResponseGzsiModel9101>
    {
        public override string InterfaceID
        {
            get
            {
                return "9101";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel9101 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel9101 t, ref Models.Response.ResponseGzsiModel9101 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    #region 目录下载
    /// <summary>
    /// 文件下载
    /// </summary>
    public class Dictionary9102 : AbstractService<Models.Request.RequestGzsiModel9102, Models.Response.ResponseGzsiModel9102>
    {
        public override string InterfaceID
        {
            get
            {
                return "9102";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel9102 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel9102 t, ref Models.Response.ResponseGzsiModel9102 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }


    /// <summary>
    /// 【1301】西药中成药目录下载
    /// </summary>
    public class Download1301 : AbstractService<Models.Request.RequestGzsiModel1301, Models.Response.ResponseGzsiModel1301>
    {
        public override string InterfaceID
        {
            get
            {
                return "1301";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1301 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1301 t, ref Models.Response.ResponseGzsiModel1301 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 【1302】中药饮片目录下载
    /// </summary>
    public class Download1302 : AbstractService<Models.Request.RequestGzsiModel1302, Models.Response.ResponseGzsiModel1302>
    {
        public override string InterfaceID
        {
            get
            {
                return "1302";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1302 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1302 t, ref Models.Response.ResponseGzsiModel1302 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1303】西药中成药目录下载
    /// </summary>
    public class Download1303 : AbstractService<Models.Request.RequestGzsiModel1303, Models.Response.ResponseGzsiModel1303>
    {
        public override string InterfaceID
        {
            get
            {
                return "1303";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1303 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1303 t, ref Models.Response.ResponseGzsiModel1303 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1304】民族药品目录查询
    /// </summary>
    public class Download1304 : AbstractService<Models.Request.RequestGzsiModel1304, Models.Response.ResponseGzsiModel1304>
    {
        public override string InterfaceID
        {
            get
            {
                return "1304";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1304 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1304 t, ref Models.Response.ResponseGzsiModel1304 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1305】医疗服务项目目录下载
    /// </summary>
    public class Download1305 : AbstractService<Models.Request.RequestGzsiModel1305, Models.Response.ResponseGzsiModel1305>
    {
        public override string InterfaceID
        {
            get
            {
                return "1305";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1305 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1305 t, ref Models.Response.ResponseGzsiModel1305 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }



    /// <summary>
    /// 【1306】医用耗材目录下载
    /// </summary>
    public class Download1306 : AbstractService<Models.Request.RequestGzsiModel1306, Models.Response.ResponseGzsiModel1306>
    {
        public override string InterfaceID
        {
            get
            {
                return "1306";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1306 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1306 t, ref Models.Response.ResponseGzsiModel1306 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1307】疾病与诊断目录下载
    /// </summary>
    public class Download1307 : AbstractService<Models.Request.RequestGzsiModel1307, Models.Response.ResponseGzsiModel1307>
    {
        public override string InterfaceID
        {
            get
            {
                return "1307";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1307 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1307 t, ref Models.Response.ResponseGzsiModel1307 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1308】手术操作目录下载
    /// </summary>
    public class Download1308 : AbstractService<Models.Request.RequestGzsiModel1308, Models.Response.ResponseGzsiModel1308>
    {
        public override string InterfaceID
        {
            get
            {
                return "1308";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1308 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1308 t, ref Models.Response.ResponseGzsiModel1308 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 【1309】门诊慢特病种目录下载
    /// </summary>
    public class Download1309 : AbstractService<Models.Request.RequestGzsiModel1309, Models.Response.ResponseGzsiModel1309>
    {
        public override string InterfaceID
        {
            get
            {
                return "1309";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1309 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1309 t, ref Models.Response.ResponseGzsiModel1309 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1310】按病种付费病种目录下载
    /// </summary>
    public class Download1310 : AbstractService<Models.Request.RequestGzsiModel1310, Models.Response.ResponseGzsiModel1310>
    {
        public override string InterfaceID
        {
            get
            {
                return "1310";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1310 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1310 t, ref Models.Response.ResponseGzsiModel1310 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }


    /// <summary>
    /// 【1311】日间手术治疗病种目录下载
    /// </summary>
    public class Download1311 : AbstractService<Models.Request.RequestGzsiModel1311, Models.Response.ResponseGzsiModel1311>
    {
        public override string InterfaceID
        {
            get
            {
                return "1311";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1311 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1311 t, ref Models.Response.ResponseGzsiModel1311 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1312】医保目录信息查询
    /// </summary>
    public class Download1312 : AbstractService<Models.Request.RequestGzsiModel1312, Models.Response.ResponseGzsiModel1312>
    {
        public override string InterfaceID
        {
            get
            {
                return "1312";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1312 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1312 t, ref Models.Response.ResponseGzsiModel1312 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1313】肿瘤形态学目录下载
    /// </summary>
    public class Download1313 : AbstractService<Models.Request.RequestGzsiModel1313, Models.Response.ResponseGzsiModel1313>
    {
        public override string InterfaceID
        {
            get
            {
                return "1313";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1313 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1313 t, ref Models.Response.ResponseGzsiModel1313 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 【1314】中医疾病目录下载
    /// </summary>
    public class Download1314 : AbstractService<Models.Request.RequestGzsiModel1314, Models.Response.ResponseGzsiModel1314>
    {
        public override string InterfaceID
        {
            get
            {
                return "1314";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1314 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1314 t, ref Models.Response.ResponseGzsiModel1314 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }


    /// <summary>
    /// 【1315】中医证候目录下载
    /// </summary>
    public class Download1315 : AbstractService<Models.Request.RequestGzsiModel1315, Models.Response.ResponseGzsiModel1315>
    {
        public override string InterfaceID
        {
            get
            {
                return "1315";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1315 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1315 t, ref Models.Response.ResponseGzsiModel1315 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1316】医疗目录与医保目录匹配信息查询
    /// </summary>
    public class Download1316 : AbstractService<Models.Request.RequestGzsiModel1316, Models.Response.ResponseGzsiModel1316>
    {
        public override string InterfaceID
        {
            get
            {
                return "1316";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1316 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1316 t, ref Models.Response.ResponseGzsiModel1316 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1317】医药机构目录匹配信息查询
    /// </summary>
    public class Download1317 : AbstractService<Models.Request.RequestGzsiModel1317, Models.Response.ResponseGzsiModel1317>
    {
        public override string InterfaceID
        {
            get
            {
                return "1317";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1317 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1317 t, ref Models.Response.ResponseGzsiModel1317 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 【1318】医保目录限价信息查询
    /// </summary>
    public class Download1318 : AbstractService<Models.Request.RequestGzsiModel1318, Models.Response.ResponseGzsiModel1318>
    {
        public override string InterfaceID
        {
            get
            {
                return "1318";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1318 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1318 t, ref Models.Response.ResponseGzsiModel1318 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【1319】医保目录先自付比例信息查询
    /// </summary>
    public class Download1319 : AbstractService<Models.Request.RequestGzsiModel1319, Models.Response.ResponseGzsiModel1319>
    {
        public override string InterfaceID
        {
            get
            {
                return "1319";
            }
        }
        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1319 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1319 t, ref Models.Response.ResponseGzsiModel1319 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }

    #endregion

    }
}
