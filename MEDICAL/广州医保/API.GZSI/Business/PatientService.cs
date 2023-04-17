using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace API.GZSI.Business
{
    /// <summary>
    /// 患者信息查询
    /// </summary>
    public class PatientService1101 : AbstractService<Models.Request.RequestGzsiModel1101, Models.Response.ResponseGzsiModel1101>
    {
        public override string InterfaceID
        {
            get
            {
                return "1101";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1101 t,ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t,ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1101 t, ref Models.Response.ResponseGzsiModel1101 e, params object[] appendParams)
        {
            return base.ConvertReceiverMessageToModel(m, t, ref e, appendParams);
        }
    }

    /// <summary>
    /// 患者详细信息查询
    /// </summary>
    public class PatientService1160 : AbstractService<Models.Request.RequestGzsiModel1160, Models.Response.ResponseGzsiModel1160>
    {
        public override string InterfaceID
        {
            get
            {
                return "1160";
            }
        }

        protected override string ConvertModelToSendMessage(Models.Request.RequestGzsiModel1160 t,ref string Infno, params object[] appendParams)
        {
            return base.ConvertModelToSendMessage(t,ref Infno, appendParams);
        }

        protected override int ConvertReceiverMessageToModel(string m, Models.Request.RequestGzsiModel1160 t, ref Models.Response.ResponseGzsiModel1160 e, params object[] appendParams)
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

}
