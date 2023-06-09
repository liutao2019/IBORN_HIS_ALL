using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Integrate.Operation
{
    /// <summary>
    /// [功能描述: 手术安排业务层]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-31]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class OpsRecord : FS.HISFC.BizLogic.Operation.OpsRecord
    {
        #region 字段
        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new RADT();
        private Operation operation = new Operation();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        #endregion

        #region 属性
        protected override FS.HISFC.BizLogic.Operation.Operation operationManager
        {
            get
            {
                return this.operation;
            }
        }
        #endregion

        #region 方法
        protected override FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string id)
        {
            return this.radtManager.GetPatientInfomation(id);
        }
        protected override FS.HISFC.Models.Registration.Register GetRegInfo(string id)
        {
            ArrayList alreg = this.regMgr.QueryPatient(id);
            return alreg[0] as FS.HISFC.Models.Registration.Register;
        }
        protected override FS.HISFC.Models.Base.Department GetDeptmentById(string id)
        {
            return this.deptManager.GetDeptmentById(id);
        }
        #endregion
    }
}
