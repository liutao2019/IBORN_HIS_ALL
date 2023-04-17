using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.MessageReceiver
{
    public class MFN_M01_Receiver : AbstractReceiver<NHapi.Model.V24.Message.MFN_M01, NHapi.Model.V24.Message.MFK_M01>
    {
        #region AbstractAcceptMessage 成员

        public override int ProcessMessage(NHapi.Model.V24.Message.MFN_M01 o, ref NHapi.Model.V24.Message.MFK_M01 ackMessage)
        {
            if (o is NHapi.Model.V24.Message.MFN_M01)
            {
                NHapi.Model.V24.Message.MFN_M01 MFN = o as NHapi.Model.V24.Message.MFN_M01;
                if (MFN.MFI.MasterFileIdentifier.Identifier.Value == "yz_supply")
                {
                    HL7.MasterFiles.MFN_M01.Constant constantMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Constant();
                    return constantMgr.Receive(MFN, ref this.errInfo);
                }
                else if (MFN.MFI.MasterFileIdentifier.Identifier.Value == "yz_frequency")
                {
                    HL7.MasterFiles.MFN_M01.Frequency frequencyMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Frequency();
                    return frequencyMgr.Receive(MFN, ref this.errInfo);
                }
                else if (MFN.MFI.MasterFileIdentifier.Identifier.Value == "mate_inv_dict")
                {
                    HL7.MasterFiles.MFN_M01.Matrial matrialMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Matrial();
                    return matrialMgr.Receive(MFN, ref this.errInfo);
                }
                else if (MFN.MFI.MasterFileIdentifier.Identifier.Value == "OPRSCHEMA")
                {
                    HL7.MasterFiles.MFN_M01.Schema schemaMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Schema();
                    return schemaMgr.Receive(MFN, ref this.errInfo);
                
                }
                else if (MFN.MFI.MasterFileIdentifier.Identifier.Value == "Department")
                { 
                    HL7.MasterFiles.MFN_M01.Department departmentMgr = new FS.SOC.HISFC.BizProcess.MessagePattern.HL7.MasterFiles.MFN_M01.Department();
                    return departmentMgr.Receive(MFN, ref this.errInfo);
               
                }
            }
            else
            {
                this.errInfo = "目前没有实现：" + o.GetType() + "类型的HL7消息处理";
                return -1;
            }

            return 1;
        }

        #endregion
    }
}
