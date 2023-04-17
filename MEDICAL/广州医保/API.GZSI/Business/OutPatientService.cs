using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
    /// <summary>
    /// 门诊挂号
    /// </summary>
    public class OutPatient2201 : AbstractService<Models.Request.RequestGzsiModel2201, Models.Response.ResponseGzsiModel2201>
    {
        public override string InterfaceID
        {
            get
            {
                return "2201";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2201 t,ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno,appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2201 t, ref Models.Response.ResponseGzsiModel2201 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 门诊撤销挂号
    /// </summary>
    public class OutPatient2202 : AbstractService<Models.Request.RequestGzsiModel2202, Models.Response.ResponseGzsiModel2202>
    {
        public override string InterfaceID
        {
            get
            {
                return "2202";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2202 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2202 t, ref Models.Response.ResponseGzsiModel2202 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 门诊就诊信息上传
    /// </summary>
    public class OutPatient2203 : AbstractService<Models.Request.RequestGzsiModel2203, Models.Response.ResponseGzsiModel2203>
    {
        public override string InterfaceID
        {
            get
            {
                return "2203";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2203 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2203 t, ref Models.Response.ResponseGzsiModel2203 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 门诊费用明细信息上传
    /// </summary>
    public class OutPatient2204 : AbstractService<Models.Request.RequestGzsiModel2204, Models.Response.ResponseGzsiModel2204>
    {
        public override string InterfaceID
        {
            get
            {
                return "2204";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2204 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2204 t, ref Models.Response.ResponseGzsiModel2204 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 门诊费用明细信息撤销
    /// </summary>
    public class OutPatient2205 : AbstractService<Models.Request.RequestGzsiModel2205, Models.Response.ResponseGzsiModel2205>
    {
        public override string InterfaceID
        {
            get
            {
                return "2205";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2205 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2205 t, ref Models.Response.ResponseGzsiModel2205 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 门诊预结算
    /// </summary>
    public class OutPatient2206 : AbstractService<Models.Request.RequestGzsiModel2206, Models.Response.ResponseGzsiModel2206>
    {
        public override string InterfaceID
        {
            get
            {
                return "2206";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2206 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2206 t, ref Models.Response.ResponseGzsiModel2206 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 门诊结算
    /// </summary>
    public class OutPatient2207 : AbstractService<Models.Request.RequestGzsiModel2207, Models.Response.ResponseGzsiModel2207>
    {
        public override string InterfaceID
        {
            get
            {
                return "2207";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2207 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2207 t, ref Models.Response.ResponseGzsiModel2207 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 门诊结算撤销
    /// </summary>
    public class OutPatient2208 : AbstractService<Models.Request.RequestGzsiModel2208, Models.Response.ResponseGzsiModel2208>
    {
        public override string InterfaceID
        {
            get
            {
                return "2208";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2208 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2208 t, ref Models.Response.ResponseGzsiModel2208 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 查询就诊信息
    /// </summary>
    public class OutPatient5303 : AbstractService<Models.Request.RequestGzsiModel5303, Models.Response.ResponseGzsiModel5303>
    {
        public override string InterfaceID
        {
            get
            {
                return "5303";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5303 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5303 t, ref Models.Response.ResponseGzsiModel5303 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
}
