using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using FS.SOC.HISFC.InpatientFee.BizProcess;

namespace FS.SOC.HISFC.RADT.BizProcess
{
    /// <summary>
    /// [功能描述: 住院费用相关的逻辑业务类（外部调用的逻辑业务层不需要启事务）]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-03]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class InpatientFee:AbstractBizProcess
    {
        private FS.SOC.HISFC.InpatientFee.BizProcess.Fee feeManager = new FS.SOC.HISFC.InpatientFee.BizProcess.Fee();

        public int ProcessChangePact(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.PactInfo newPactInfo)
        {
            this.feeManager.SetTrans(this.Trans);
            if (this.feeManager.ProcessChangePact(patientInfo,patientInfo.Pact, newPactInfo) <= 0)
            {
                this.err = this.feeManager.Err;
                return -1;
            }

            return 1;
        }

        public ArrayList QueryInpatientPact()
        {
            FS.SOC.HISFC.Fee.BizLogic.PactInfo pactInfoManager = new FS.SOC.HISFC.Fee.BizLogic.PactInfo();
            pactInfoManager.SetTrans(this.Trans);

            ArrayList al = pactInfoManager.QueryPactUnitForInpatient();
            if (al == null)
            {
                this.err = pactInfoManager.Err;
                return null;
            }

            return al;
        }
    }
}
