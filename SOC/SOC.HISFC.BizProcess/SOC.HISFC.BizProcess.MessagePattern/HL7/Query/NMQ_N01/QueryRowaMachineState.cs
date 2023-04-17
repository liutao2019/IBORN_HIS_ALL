using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.HL7.Query.NMQ_N01
{
    class QueryRowaMachineState
    {
        //查询设备状态
        public int ProcessMessage(FS.HISFC.Models.Pharmacy.ApplyOut applyout, ref NHapi.Model.V24.Message.NMQ_N01 nmqn01, ref string errInfo)
        {

            //查询设备状态
            nmqn01 = new NHapi.Model.V24.Message.NMQ_N01();
            nmqn01.MSH.MessageType.MessageType.Value = "NMQ";
            nmqn01.MSH.MessageType.TriggerEvent.Value = "N01";
            FS.HL7Message.V24.Function.GenerateMSH(nmqn01.MSH);

            nmqn01.QRY_WITH_DETAIL.QRD.QueryID.Value = "NMQN01001";
            nmqn01.QRY_WITH_DETAIL.QRD.GetWhatSubjectFilter(0).Text.Value = "发药机设备";
            nmqn01.QRY_WITH_DETAIL.QRD.GetWhatSubjectFilter(0).Identifier.Value = "RowaMachine";
            nmqn01.QRY_WITH_DETAIL.QRD.GetWhatDepartmentDataCode(0).Identifier.Value = "OutpatientPharmacy";
            nmqn01.QRY_WITH_DETAIL.QRD.GetWhatDepartmentDataCode(0).Text.Value = "门诊西药房";

            return 1;

        }
    }
}
