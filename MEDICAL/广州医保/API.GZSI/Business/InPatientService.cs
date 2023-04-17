using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
    /// <summary>
    /// 住院费用明细上传
    /// </summary>
    public class InPatient2301 : AbstractService<Models.Request.RequestGzsiModel2301, Models.Response.ResponseGzsiModel2301>
    {
        public override string InterfaceID
        {
            get
            {
                return "2301";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2301 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2301 t, ref Models.Response.ResponseGzsiModel2301 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 住院费用明细撤销
    /// </summary>
    public class InPatient2302 : AbstractService<Models.Request.RequestGzsiModel2302, Models.Response.ResponseGzsiModel2302>
    {
        public override string InterfaceID
        {
            get
            {
                return "2302";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2302 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2302 t, ref Models.Response.ResponseGzsiModel2302 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 住院预结算
    /// </summary>
    public class InPatient2303 : AbstractService<Models.Request.RequestGzsiModel2303, Models.Response.ResponseGzsiModel2303>
    {
        public override string InterfaceID
        {
            get
            {
                return "2303";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2303 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2303 t, ref Models.Response.ResponseGzsiModel2303 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 住院结算
    /// </summary>
    public class InPatient2304 : AbstractService<Models.Request.RequestGzsiModel2304, Models.Response.ResponseGzsiModel2304>
    {
        public override string InterfaceID
        {
            get
            {
                return "2304";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2304 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2304 t, ref Models.Response.ResponseGzsiModel2304 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 住院结算撤销
    /// </summary>
    public class InPatient2305 : AbstractService<Models.Request.RequestGzsiModel2305, Models.Response.ResponseGzsiModel2305>
    {
        public override string InterfaceID
        {
            get
            {
                return "2305";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2305 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2305 t, ref Models.Response.ResponseGzsiModel2305 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 入院办理
    /// </summary>
    public class InPatient2401 : AbstractService<Models.Request.RequestGzsiModel2401, Models.Response.ResponseGzsiModel2401>
    {
        public override string InterfaceID
        {
            get
            {
                return "2401";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2401 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2401 t, ref Models.Response.ResponseGzsiModel2401 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 出院办理
    /// </summary>
    public class InPatient2402 : AbstractService<Models.Request.RequestGzsiModel2402, Models.Response.ResponseGzsiModel2402>
    {
        public override string InterfaceID
        {
            get
            {
                return "2402";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2402 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2402 t, ref Models.Response.ResponseGzsiModel2402 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 入院信息变更
    /// </summary>
    public class InPatient2403 : AbstractService<Models.Request.RequestGzsiModel2403, Models.Response.ResponseGzsiModel2403>
    {
        public override string InterfaceID
        {
            get
            {
                return "2403";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2403 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2403 t, ref Models.Response.ResponseGzsiModel2403 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 入院撤销
    /// </summary>
    public class InPatient2404 : AbstractService<Models.Request.RequestGzsiModel2404, Models.Response.ResponseGzsiModel2404>
    {
        public override string InterfaceID
        {
            get
            {
                return "2404";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2404 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2404 t, ref Models.Response.ResponseGzsiModel2404 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 出院撤销
    /// </summary>
    public class InPatient2405 : AbstractService<Models.Request.RequestGzsiModel2405, Models.Response.ResponseGzsiModel2405>
    {
        public override string InterfaceID
        {
            get
            {
                return "2405";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2405 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2405 t, ref Models.Response.ResponseGzsiModel2405 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 结算清单上传
    /// </summary>
    public class InPatient4101 : AbstractService<Models.Request.RequestGzsiModel4101, Models.Response.ResponseGzsiModel4101>
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
            string inputData = base.ConvertModelToSendMessage(t, ref Infno, appendParams);
            return inputData;
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4101 t, ref Models.Response.ResponseGzsiModel4101 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 结算清单上传
    /// </summary>
    public class InPatient4101A : AbstractService<Models.Request.RequestGzsiModel4101A, Models.Response.ResponseGzsiModel4101A>
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
            string inputData = base.ConvertModelToSendMessage(t, ref Infno, appendParams);
            return inputData;
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4101A t, ref Models.Response.ResponseGzsiModel4101A e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    ///自费病人费用明细信息上传
    /// </summary>
    public class InPatient4201 : AbstractService<Models.Request.RequestGzsiModel4201, Models.Response.ResponseGzsiModel4201>
    {
        public override string InterfaceID
        {
            get
            {
                return "4201";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4201 t, ref string Infno, params object[] appendParams)
        {
            string inputData = base.ConvertModelToSendMessage(t, ref Infno, appendParams);
            return inputData;
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4201 t, ref Models.Response.ResponseGzsiModel4201 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 病案首页上传
    /// </summary>
    public class InPatient4401 : AbstractService<Models.Request.RequestGzsiModel4401, Models.Response.ResponseGzsiModel4401>
    {
        public override string InterfaceID
        {
            get
            {
                return "4401";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4401 t, ref string Infno, params object[] appendParams)
        {
            string inputData = base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
            //报文特殊字段处理
            inputData = inputData.Replace("hcv_ab", "hcv-ab");
            inputData = inputData.Replace("hiv_ab", "hiv-ab");
            return inputData;
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4401 t, ref Models.Response.ResponseGzsiModel4401 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 住院医嘱上传
    /// </summary>
    public class InPatient4402 : AbstractService<Models.Request.RequestGzsiModel4402, Models.Response.ResponseGzsiModel4402>
    {
        public override string InterfaceID
        {
            get
            {
                return "4402";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4402 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4402 t, ref Models.Response.ResponseGzsiModel4402 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    /// <summary>
    /// 住院病历上传
    /// </summary>
    public class InPatient4701 : AbstractService<Models.Request.RequestGzsiModel4701, Models.Response.ResponseGzsiModel4701>
    {
        public override string InterfaceID
        {
            get
            {
                return "4701";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4701 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4701 t, ref Models.Response.ResponseGzsiModel4701 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    #region 自费患者结算清单上传
    /// <summary>
    /// 【4201A】自费病人住院费用明细信息上传
    /// </summary>
    public class Upload4201A : AbstractService<Models.Request.RequestGzsiModel4201A, Models.Response.ResponseGzsiModel4201A>
    {
        public override string InterfaceID
        {
            get
            {
                return "4201A";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4201A t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4201A t, ref Models.Response.ResponseGzsiModel4201A e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4202】自费病人住院就诊和诊断信息上传
    /// </summary>
    public class Upload4202 : AbstractService<Models.Request.RequestGzsiModel4202, Models.Response.ResponseGzsiModel4202>
    {
        public override string InterfaceID
        {
            get
            {
                return "4202";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4202 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4202 t, ref Models.Response.ResponseGzsiModel4202 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4203】自费病人就诊以及费用明细上传完成
    /// </summary>
    public class Upload4203 : AbstractService<Models.Request.RequestGzsiModel4203, Models.Response.ResponseGzsiModel4203>
    {
        public override string InterfaceID
        {
            get
            {
                return "4203";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4203 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4203 t, ref Models.Response.ResponseGzsiModel4203 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4204】自费病人住院费用明细删除
    /// </summary>
    public class Upload4204 : AbstractService<Models.Request.RequestGzsiModel4204, Models.Response.ResponseGzsiModel4204>
    {
        public override string InterfaceID
        {
            get
            {
                return "4204";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4204 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4204 t, ref Models.Response.ResponseGzsiModel4204 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4205】自费病人门诊就医信息上传
    /// </summary>
    public class Upload4205 : AbstractService<Models.Request.RequestGzsiModel4205, Models.Response.ResponseGzsiModel4205>
    {
        public override string InterfaceID
        {
            get
            {
                return "4205";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4205 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4205 t, ref Models.Response.ResponseGzsiModel4205 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4206】自费病人门诊就医信息删除
    /// </summary>
    public class Upload4206 : AbstractService<Models.Request.RequestGzsiModel4206, Models.Response.ResponseGzsiModel4206>
    {
        public override string InterfaceID
        {
            get
            {
                return "4206";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4206 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4206 t, ref Models.Response.ResponseGzsiModel4206 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4207】自费病人就医费用明细查询
    /// </summary>
    public class Upload4207 : AbstractService<Models.Request.RequestGzsiModel4207, Models.Response.ResponseGzsiModel4207>
    {
        public override string InterfaceID
        {
            get
            {
                return "4207";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4207 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4207 t, ref Models.Response.ResponseGzsiModel4207 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4208】自费病人就医就诊信息查询
    /// </summary>
    public class Upload4208 : AbstractService<Models.Request.RequestGzsiModel4208, Models.Response.ResponseGzsiModel4208>
    {
        public override string InterfaceID
        {
            get
            {
                return "4208";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4208 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4208 t, ref Models.Response.ResponseGzsiModel4208 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 【4209】自费病人就医诊断信息查询
    /// </summary>
    public class Upload4209 : AbstractService<Models.Request.RequestGzsiModel4209, Models.Response.ResponseGzsiModel4209>
    {
        public override string InterfaceID
        {
            get
            {
                return "4209";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel4209 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel4209 t, ref Models.Response.ResponseGzsiModel4209 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    #endregion
}
