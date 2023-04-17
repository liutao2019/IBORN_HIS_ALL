using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
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

    /// <summary>
    /// 文件下载
    /// </summary>
    public class CommonService9101 : AbstractService<Models.Request.RequestGzsiModel9101, Models.Response.ResponseGzsiModel9101>
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
}
