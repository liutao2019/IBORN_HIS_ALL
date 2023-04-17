using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
    /// <summary>
    /// 患者信息查询
    /// </summary>
    public class Patient1101 : AbstractService<Models.Request.RequestGzsiModel1101, Models.Response.ResponseGzsiModel1101>
    {
        public override string InterfaceID
        {
            get
            {
                return "1101";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1101 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1101 t, ref Models.Response.ResponseGzsiModel1101 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    
    /// <summary>
    /// 患者详细信息查询[未用]
    /// </summary>
    public class Patient1160 : AbstractService<Models.Request.RequestGzsiModel1160, Models.Response.ResponseGzsiModel1160>
    {
        public override string InterfaceID
        {
            get
            {
                return "1160";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1160 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1160 t, ref Models.Response.ResponseGzsiModel1160 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    
    /// <summary>
    /// 人员定点备案
    /// </summary>
    public class Patient2505 : AbstractService<Models.Request.RequestGzsiModel2505, Models.Response.ResponseGzsiModel2505>
    {
        public override string InterfaceID
        {
            get
            {
                return "2505";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2505 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2505 t, ref Models.Response.ResponseGzsiModel2505 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    
    /// <summary>
    /// 人员定点备案撤销 
    /// </summary>
    public class Patient2506 : AbstractService<Models.Request.RequestGzsiModel2506, Models.Response.ResponseGzsiModel2506>
    {
        public override string InterfaceID
        {
            get
            {
                return "2506";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2506 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2506 t, ref Models.Response.ResponseGzsiModel2506 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医药机构费用结算对总账
    /// </summary>
    public class Patient3201 : AbstractService<Models.Request.RequestGzsiModel3201, Models.Response.ResponseGzsiModel3201>
    {
        public override string InterfaceID
        {
            get
            {
                return "3201";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3201 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3201 t, ref Models.Response.ResponseGzsiModel3201 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 医药机构费用结算对明细账
    /// </summary>
    public class Patient3202 : AbstractService<Models.Request.RequestGzsiModel3202, Models.Response.ResponseGzsiModel3202>
    {
        public override string InterfaceID
        {
            get
            {
                return "3202";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3202 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3202 t, ref Models.Response.ResponseGzsiModel3202 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 提取异地清分明细
    /// </summary>
    public class Patient3260 : AbstractService<Models.Request.RequestGzsiModel3260, Models.Response.ResponseGzsiModel3260>
    {
        public override string InterfaceID
        {
            get
            {
                return "3260";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3260 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3260 t, ref Models.Response.ResponseGzsiModel3260 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 异地清分结果确认
    /// </summary>
    public class Patient3261 : AbstractService<Models.Request.RequestGzsiModel3261, Models.Response.ResponseGzsiModel3261>
    {
        public override string InterfaceID
        {
            get
            {
                return "3261";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3261 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3261 t, ref Models.Response.ResponseGzsiModel3261 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 异地清分结果确认回退
    /// </summary>
    public class Patient3262 : AbstractService<Models.Request.RequestGzsiModel3262, Models.Response.ResponseGzsiModel3262>
    {
        public override string InterfaceID
        {
            get
            {
                return "3262";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel3262 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel3262 t, ref Models.Response.ResponseGzsiModel3262 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 就诊信息查询 
    /// </summary>
    public class Patient5201 : AbstractService<Models.Request.RequestGzsiModel5201, Models.Response.ResponseGzsiModel5201>
    {

        public override string InterfaceID
        {
            get
            {
                return "5201";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5201 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5201 t, ref Models.Response.ResponseGzsiModel5201 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 诊断信息查询
    /// </summary>
    public class Patient5202 : AbstractService<Models.Request.RequestGzsiModel5202, Models.Response.ResponseGzsiModel5202>
    {

        public override string InterfaceID
        {
            get
            {
                return "5202";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5202 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5202 t, ref Models.Response.ResponseGzsiModel5202 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 结算信息查询 
    /// </summary>
    public class Patient5203 : AbstractService<Models.Request.RequestGzsiModel5203, Models.Response.ResponseGzsiModel5203>
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

    /// <summary>
    /// 费用明细查询 
    /// </summary>
    public class Patient5204 : AbstractService<Models.Request.RequestGzsiModel5204, Models.Response.ResponseGzsiModel5204>
    {

        public override string InterfaceID
        {
            get
            {
                return "5204";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5204 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5204 t, ref Models.Response.ResponseGzsiModel5204 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 人员累计信息查询 
    /// </summary>
    public class Patient5206 : AbstractService<Models.Request.RequestGzsiModel5206, Models.Response.ResponseGzsiModel5206>
    {

        public override string InterfaceID
        {
            get
            {
                return "5206";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5206 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5206 t, ref Models.Response.ResponseGzsiModel5206 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 人员定点信息查询
    /// </summary>
    public class Patient5302 : AbstractService<Models.Request.RequestGzsiModel5302, Models.Response.ResponseGzsiModel5302>
    {

        public override string InterfaceID
        {
            get
            {
                return "5302";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5302 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5302 t, ref Models.Response.ResponseGzsiModel5302 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 在院信息查询
    /// </summary>
    public class Patient5303 : AbstractService<Models.Request.RequestGzsiModel5303, Models.Response.ResponseGzsiModel5303>
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

    /// <summary>
    /// 转院信息查询 
    /// </summary>
    public class Patient5304 : AbstractService<Models.Request.RequestGzsiModel5304, Models.Response.ResponseGzsiModel5304>
    {

        public override string InterfaceID
        {
            get
            {
                return "5304";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel5304 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel5304 t, ref Models.Response.ResponseGzsiModel5304 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 缴费查询
    /// </summary>
    public class Patient90100 : AbstractService<Models.Request.RequestGzsiModel90100, Models.Response.ResponseGzsiModel90100>
    {

        public override string InterfaceID
        {
            get
            {
                return "90100";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90100 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90100 t, ref Models.Response.ResponseGzsiModel90100 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 生育登记查询
    /// </summary>
    public class Patient90101 : AbstractService<Models.Request.RequestGzsiModel90101, Models.Response.ResponseGzsiModel90101>
    {

        public override string InterfaceID
        {
            get
            {
                return "90101";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90101 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90101 t, ref Models.Response.ResponseGzsiModel90101 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    
    /// <summary>
    /// 家庭医生签约登记查询
    /// </summary>
    public class Patient90102 : AbstractService<Models.Request.RequestGzsiModel90102, Models.Response.ResponseGzsiModel90102>
    {

        public override string InterfaceID
        {
            get
            {
                return "90102";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90102 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90102 t, ref Models.Response.ResponseGzsiModel90102 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 家庭病床登记查询
    /// </summary>
    public class Patient90103 : AbstractService<Models.Request.RequestGzsiModel90103, Models.Response.ResponseGzsiModel90103>
    {

        public override string InterfaceID
        {
            get
            {
                return "90103";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90103 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90103 t, ref Models.Response.ResponseGzsiModel90103 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
    
    /// <summary>
    /// 生育登记
    /// </summary>
    public class Patient90201 : AbstractService<Models.Request.RequestGzsiModel90201, Models.Response.ResponseGzsiModel90201>
    {
        public override string InterfaceID
        {
            get
            {
                return "90201";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90201 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90201 t, ref Models.Response.ResponseGzsiModel90201 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 家庭医生签约登记
    /// </summary>
    public class Patient90202 : AbstractService<Models.Request.RequestGzsiModel90202, Models.Response.ResponseGzsiModel90202>
    {
        public override string InterfaceID
        {
            get
            {
                return "90202";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90202 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90202 t, ref Models.Response.ResponseGzsiModel90202 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 家庭病床登记
    /// </summary>
    public class Patient90203 : AbstractService<Models.Request.RequestGzsiModel90203, Models.Response.ResponseGzsiModel90203>
    {
        public override string InterfaceID
        {
            get
            {
                return "90203";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90203 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref  Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90203 t, ref Models.Response.ResponseGzsiModel90203 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 对数明细信息查询接口 // {ACB49EE2-E85B-4AA0-96CB-2EE7912AA5E8}
    /// </summary>
    public class Patient90502 : AbstractService<Models.Request.RequestGzsiModel90502, Models.Response.ResponseGzsiModel90502>
    {
        public override string InterfaceID
        {
            get
            {
                return "90502";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel90502 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel90502 t, ref Models.Response.ResponseGzsiModel90502 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 对数明细信息查询接口 // {ACB49EE2-E85B-4AA0-96CB-2EE7912AA5E8}
    /// </summary>
    public class Patient2601 : AbstractService<Models.Request.RequestGzsiModel2601, Models.Response.ResponseGzsiModel2601>
    {
        public override string InterfaceID
        {
            get
            {
                return "2601";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel2601 t, ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t, ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel2601 t, ref Models.Response.ResponseGzsiModel2601 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }
}
