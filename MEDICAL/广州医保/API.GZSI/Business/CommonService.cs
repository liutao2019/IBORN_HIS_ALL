using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
    /// <summary>
    /// 医疗机构信息查询
    /// </summary>
    public class CommonService1201 : AbstractService<Models.Request.RequestGzsiModel1201, Models.Response.ResponseGzsiModel1201>
    {
        public override string InterfaceID
        {
            get
            {
                return "1201";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1201 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1201 t, ref Models.Response.ResponseGzsiModel1201 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 西药中成药目录下载
    /// </summary>
    public class CommonService1301 : AbstractService<Models.Request.RequestGzsiModel1301, Models.Response.ResponseGzsiModel1301>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1301 t, ref Models.Response.ResponseGzsiModel1301 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 中药饮片目录下载
    /// </summary>
    public class CommonService1302 : AbstractService<Models.Request.RequestGzsiModel1302, Models.Response.ResponseGzsiModel1302>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1302 t, ref Models.Response.ResponseGzsiModel1302 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医疗机构制剂目录下载
    /// </summary>
    public class CommonService1303 : AbstractService<Models.Request.RequestGzsiModel1303, Models.Response.ResponseGzsiModel1303>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1303 t, ref Models.Response.ResponseGzsiModel1303 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 民族药品目录查询
    /// </summary>
    public class CommonService1304 : AbstractService<Models.Request.RequestGzsiModel1304, Models.Response.ResponseGzsiModel1304>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1304 t, ref Models.Response.ResponseGzsiModel1304 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医疗服务项目目录下载
    /// </summary>
    public class CommonService1305 : AbstractService<Models.Request.RequestGzsiModel1305, Models.Response.ResponseGzsiModel1305>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1305 t, ref Models.Response.ResponseGzsiModel1305 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医用耗材目录下载
    /// </summary>
    public class CommonService1306 : AbstractService<Models.Request.RequestGzsiModel1306, Models.Response.ResponseGzsiModel1306>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1306 t, ref Models.Response.ResponseGzsiModel1306 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 疾病与诊断目录下载
    /// </summary>
    public class CommonService1307 : AbstractService<Models.Request.RequestGzsiModel1307, Models.Response.ResponseGzsiModel1307>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1307 t, ref Models.Response.ResponseGzsiModel1307 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 手术操作目录下载
    /// </summary>
    public class CommonService1308 : AbstractService<Models.Request.RequestGzsiModel1308, Models.Response.ResponseGzsiModel1308>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1308 t, ref Models.Response.ResponseGzsiModel1308 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 门诊慢特病种目录下载
    /// </summary>
    public class CommonService1309 : AbstractService<Models.Request.RequestGzsiModel1309, Models.Response.ResponseGzsiModel1309>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1309 t, ref Models.Response.ResponseGzsiModel1309 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 按病种付费病种目录下载
    /// </summary>
    public class CommonService1310 : AbstractService<Models.Request.RequestGzsiModel1310, Models.Response.ResponseGzsiModel1310>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1310 t, ref Models.Response.ResponseGzsiModel1310 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 日间手术治疗病种目录下载
    /// </summary>
    public class CommonService1311 : AbstractService<Models.Request.RequestGzsiModel1311, Models.Response.ResponseGzsiModel1311>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1311 t, ref Models.Response.ResponseGzsiModel1311 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医保目录信息查询
    /// </summary>
    public class CommonService1312 : AbstractService<Models.Request.RequestGzsiModel1312, Models.Response.ResponseGzsiModel1312>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1312 t, ref Models.Response.ResponseGzsiModel1312 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 肿瘤形态学目录下载
    /// </summary>
    public class CommonService1313 : AbstractService<Models.Request.RequestGzsiModel1313, Models.Response.ResponseGzsiModel1313>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1313 t, ref Models.Response.ResponseGzsiModel1313 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 中医疾病目录下载
    /// </summary>
    public class CommonService1314 : AbstractService<Models.Request.RequestGzsiModel1314, Models.Response.ResponseGzsiModel1314>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1314 t, ref Models.Response.ResponseGzsiModel1314 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 中医证候目录下载
    /// </summary>
    public class CommonService1315 : AbstractService<Models.Request.RequestGzsiModel1315, Models.Response.ResponseGzsiModel1315>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1315 t, ref Models.Response.ResponseGzsiModel1315 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医疗目录与医保目录匹配信息查询
    /// </summary>
    public class CommonService1316 : AbstractService<Models.Request.RequestGzsiModel1316, Models.Response.ResponseGzsiModel1316>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1316 t, ref Models.Response.ResponseGzsiModel1316 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医药机构目录匹配信息查询
    /// </summary>
    public class CommonService1317 : AbstractService<Models.Request.RequestGzsiModel1317, Models.Response.ResponseGzsiModel1317>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1317 t, ref Models.Response.ResponseGzsiModel1317 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医保目录限价信息查询
    /// </summary>
    public class CommonService1318 : AbstractService<Models.Request.RequestGzsiModel1318, Models.Response.ResponseGzsiModel1318>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1318 t, ref Models.Response.ResponseGzsiModel1318 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医保目录先自付比例信息查询
    /// </summary>
    public class CommonService1319 : AbstractService<Models.Request.RequestGzsiModel1319, Models.Response.ResponseGzsiModel1319>
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
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1319 t, ref Models.Response.ResponseGzsiModel1319 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 字典表查询
    /// </summary>
    public class CommonService1901 : AbstractService<Models.Request.RequestGzsiModel1901, Models.Response.ResponseGzsiModel1901>
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
    /// 目录对照上传
    /// </summary>
    public class CommonService3301 : AbstractService<Models.Request.RequestGzsiModel3301, Models.Response.ResponseGzsiModel3301>
    {
        public override string InterfaceID
        {
            get
            {
                return "3301";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3301 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3301 t, ref Models.Response.ResponseGzsiModel3301 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 目录对照撤销
    /// </summary>
    public class CommonService3302 : AbstractService<Models.Request.RequestGzsiModel3302, Models.Response.ResponseGzsiModel3302>
    {
        public override string InterfaceID
        {
            get
            {
                return "3302";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3302 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3302 t, ref Models.Response.ResponseGzsiModel3302 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 目录对照作废
    /// </summary>
    public class CommonService3360 : AbstractService<Models.Request.RequestGzsiModel3360, Models.Response.ResponseGzsiModel3360>
    {
        public override string InterfaceID
        {
            get
            {
                return "3360";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3360 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3360 t, ref Models.Response.ResponseGzsiModel3360 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 科室上传
    /// </summary>
    public class CommonService3401 : AbstractService<Models.Request.RequestGzsiModel3401, Models.Response.ResponseGzsiModel3401>
    {
        public override string InterfaceID
        {
            get
            {
                return "3401";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3401 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3401 t, ref Models.Response.ResponseGzsiModel3401 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 科室信息变更
    /// </summary>
    public class CommonService3402 : AbstractService<Models.Request.RequestGzsiModel3402, Models.Response.ResponseGzsiModel3402>
    {
        public override string InterfaceID
        {
            get
            {
                return "3402";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3402 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3402 t, ref Models.Response.ResponseGzsiModel3402 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 科室信息撤销
    /// </summary>
    public class CommonService3403 : AbstractService<Models.Request.RequestGzsiModel3403, Models.Response.ResponseGzsiModel3403>
    {
        public override string InterfaceID
        {
            get
            {
                return "3403";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3403 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3403 t, ref Models.Response.ResponseGzsiModel3403 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 病床上传
    /// </summary>
    public class CommonService3461 : AbstractService<Models.Request.RequestGzsiModel3461, Models.Response.ResponseGzsiModel3461>
    {
        public override string InterfaceID
        {
            get
            {
                return "3461";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3461 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3461 t, ref Models.Response.ResponseGzsiModel3461 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 病床变更
    /// </summary>
    public class CommonService3462 : AbstractService<Models.Request.RequestGzsiModel3462, Models.Response.ResponseGzsiModel3462>
    {
        public override string InterfaceID
        {
            get
            {
                return "3462";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3462 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3462 t, ref Models.Response.ResponseGzsiModel3462 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 病床撤销
    /// </summary>
    public class CommonService3463 : AbstractService<Models.Request.RequestGzsiModel3463, Models.Response.ResponseGzsiModel3463>
    {
        public override string InterfaceID
        {
            get
            {
                return "3463";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3463 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3463 t, ref Models.Response.ResponseGzsiModel3463 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医师上传
    /// </summary>
    public class CommonService3467 : AbstractService<Models.Request.RequestGzsiModel3467, Models.Response.ResponseGzsiModel3467>
    {
        public override string InterfaceID
        {
            get
            {
                return "3467";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3467 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3467 t, ref Models.Response.ResponseGzsiModel3467 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医师变更
    /// </summary>
    public class CommonService3468 : AbstractService<Models.Request.RequestGzsiModel3468, Models.Response.ResponseGzsiModel3468>
    {
        public override string InterfaceID
        {
            get
            {
                return "3468";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3468 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3468 t, ref Models.Response.ResponseGzsiModel3468 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医师撤销
    /// </summary>
    public class CommonService3469 : AbstractService<Models.Request.RequestGzsiModel3469, Models.Response.ResponseGzsiModel3469>
    {
        public override string InterfaceID
        {
            get
            {
                return "3469";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3469 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3469 t, ref Models.Response.ResponseGzsiModel3469 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    public class CommonService3501 : AbstractService<Models.Request.RequestGzsiModel3501, Models.Response.ResponseGzsiModel3501>
    {
        public override string InterfaceID
        {
            get
            {
                return "3501";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3501 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3501 t, ref Models.Response.ResponseGzsiModel3501 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    public class CommonService3502 : AbstractService<Models.Request.RequestGzsiModel3502, Models.Response.ResponseGzsiModel3502>
    {
        public override string InterfaceID
        {
            get
            {
                return "3502";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3502 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3502 t, ref Models.Response.ResponseGzsiModel3502 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    public class CommonService3503 : AbstractService<Models.Request.RequestGzsiModel3503, Models.Response.ResponseGzsiModel3503>
    {
        public override string InterfaceID
        {
            get
            {
                return "3503";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3503 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3503 t, ref Models.Response.ResponseGzsiModel3503 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    public class CommonService3504 : AbstractService<Models.Request.RequestGzsiModel3504, Models.Response.ResponseGzsiModel3504>
    {
        public override string InterfaceID
        {
            get
            {
                return "3504";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3504 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3504 t, ref Models.Response.ResponseGzsiModel3504 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    public class CommonService3505 : AbstractService<Models.Request.RequestGzsiModel3505, Models.Response.ResponseGzsiModel3505>
    {
        public override string InterfaceID
        {
            get
            {
                return "3505";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3505 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3505 t, ref Models.Response.ResponseGzsiModel3505 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    public class CommonService3506: AbstractService<Models.Request.RequestGzsiModel3506, Models.Response.ResponseGzsiModel3506>
    {
        public override string InterfaceID
        {
            get
            {
                return "3506";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3506 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3506 t, ref Models.Response.ResponseGzsiModel3506 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    public class CommonService3507 : AbstractService<Models.Request.RequestGzsiModel3507, Models.Response.ResponseGzsiModel3507>
    {
        public override string InterfaceID
        {
            get
            {
                return "3507";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3507 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3507 t, ref Models.Response.ResponseGzsiModel3507 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 目录对照查询
    /// </summary>
    public class CommonService5163 : AbstractService<Models.Request.RequestGzsiModel5163, Models.Response.ResponseGzsiModel5163>
    {
        public override string InterfaceID
        {
            get
            {
                return "5163";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5163 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5163 t, ref Models.Response.ResponseGzsiModel5163 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医保签到
    /// </summary>
    public class CommonService9001 : AbstractService<Models.Request.RequestGzsiModel9001, Models.Response.ResponseGzsiModel9001>
    {
        public override string InterfaceID
        {
            get
            {
                return "9001";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel9001 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel9001 t, ref Models.Response.ResponseGzsiModel9001 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 文件下载
    /// </summary>
    public class CommonService9102 : AbstractService<Models.Request.RequestGzsiModel9102, Models.Response.ResponseGzsiModel9102>
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
    /// 医执人员信息查询
    /// </summary>
    public class CommonService5102 : AbstractService<Models.Request.RequestGzsiModel5102, Models.Response.ResponseGzsiModel5102>
    {
        public override string InterfaceID
        {
            get
            {
                return "5102";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5102 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5102 t, ref Models.Response.ResponseGzsiModel5102 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医执人员信息查询
    /// </summary>
    public class CommonService5401 : AbstractService<Models.Request.RequestGzsiModel5401, Models.Response.ResponseGzsiModel5401>
    {
        public override string InterfaceID
        {
            get
            {
                return "5401";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5401 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5401 t, ref Models.Response.ResponseGzsiModel5401 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医执人员信息查询
    /// </summary>
    public class CommonService5402 : AbstractService<Models.Request.RequestGzsiModel5402, Models.Response.ResponseGzsiModel5402>
    {
        public override string InterfaceID
        {
            get
            {
                return "5402";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5402 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5402 t, ref Models.Response.ResponseGzsiModel5402 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }


    public class CommonService5203 : AbstractService<Models.Request.RequestGzsiModel5203, Models.Response.ResponseGzsiModel5203>
    {
        public override string InterfaceID
        {
            get
            {
                return "5203";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5203 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5203 t, ref Models.Response.ResponseGzsiModel5203 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }


    public class CommonService4101 : AbstractService<Models.Request.RequestGzsiModel4101, Models.Response.ResponseGzsiModel4101>
    {
        public override string InterfaceID
        {
            get
            {
                return "4101";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4101 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4101 t, ref Models.Response.ResponseGzsiModel4101 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    public class CommonService4101A : AbstractService<Models.Request.RequestGzsiModel4101A, Models.Response.ResponseGzsiModel4101A>
    {
        public override string InterfaceID
        {
            get
            {
                return "4101A";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4101A t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4101A t, ref Models.Response.ResponseGzsiModel4101A e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    public class CommonService5260 : AbstractService<Models.Request.RequestGzsiModel5260, Models.Response.ResponseGzsiModel5260>
    {
        public override string InterfaceID
        {
            get
            {
                return "5260";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5260 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5260 t, ref Models.Response.ResponseGzsiModel5260 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    public class CommonService5261 : AbstractService<Models.Request.RequestGzsiModel5261, Models.Response.ResponseGzsiModel5261>
    {
        public override string InterfaceID
        {
            get
            {
                return "5261";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5261 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5261 t, ref Models.Response.ResponseGzsiModel5261 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
}
