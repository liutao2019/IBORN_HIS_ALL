using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Integrate.Operation
{
    /// <summary>
    /// [��������: ��������ҵ���]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-31]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class OpsRecord : FS.HISFC.BizLogic.Operation.OpsRecord
    {
        #region �ֶ�
        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new RADT();
        private Operation operation = new Operation();
        FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        #endregion

        #region ����
        protected override FS.HISFC.BizLogic.Operation.Operation operationManager
        {
            get
            {
                return this.operation;
            }
        }
        #endregion

        #region ����
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
