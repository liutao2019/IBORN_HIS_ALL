using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Operation
{
    /// <summary>
    /// [��������: ����ҵ��]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2006-12-31]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Operation : FS.HISFC.BizLogic.Operation.Operation
    {

#region �ֶ�
        private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        private FS.HISFC.BizProcess.Integrate.Manager manager = new Manager();
        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new RADT();

        private FS.HISFC.BizLogic.Operation.OpsDiagnose diagMgr = new FS.HISFC.BizLogic.Operation.OpsDiagnose();
        //��Ͽ�����ʵ��
        // TODO: ����ҵ��㣬��Ҫ�ؼ�
        //public FS.HISFC.BizLogic.HealthRecord.Diagnose DiagnoseManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        private FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();


#endregion
        /// <summary>
        /// ���ݻ���סԺ�Ż�õ�ǰ��Ժ��סԺ��ˮ��
        /// </summary>
        /// <param name="PatientNo"></param>
        /// <returns></returns>
        private string GetInPatientNo(string PatientNo)
        {
            string strInpatientNo = string.Empty;
            ArrayList al = new ArrayList();
            try
            {
                al = this.inPatientManager.QueryInpatientNOByPatientNO(PatientNo);
                if (al == null)
                {
                    return strInpatientNo;
                }

                foreach (FS.FrameWork.Models.NeuObject obj in al)
                {
                    //Ѱ�һ�����Ժ״̬������סԺ�����¼
                    if (obj.Memo == "I")
                    {
                        strInpatientNo = obj.ID.ToString();
                        break;
                    }
                }
            }
            catch
            {
                return strInpatientNo;
            }
            return strInpatientNo;
        }

     
        public new void SetTrans(System.Data.IDbTransaction trans)
        {
            base.SetTrans(trans);
            // TODO: ����������
            //this.DiagnoseManager.SetTrans(trans);
            this.inPatientManager.SetTrans(trans);
            this.manager.SetTrans(trans);
            this.radtManager.SetTrans(trans);
            diagMgr.SetTrans(trans);
        }

        protected override string GetEmployeeName(string id)
        {
            return this.manager.GetEmployeeInfo(id).Name;
        }

        protected override FS.HISFC.Models.RADT.PatientInfo GetPatientInfo(string id)
        {
            return this.radtManager.GetPatientInfomation(id);
        }
        protected override FS.HISFC.Models.Registration.Register GetRegInfo(string id)
        {
            ArrayList alreg= this.regMgr.QueryPatient(id);
            return alreg[0] as FS.HISFC.Models.Registration.Register;
        }
        /// <summary>
        /// ����������Ż�����������Ϣ�б�
        /// </summary>
        /// <param name="OperatorNo">�������뵥����</param>
        /// <returns>���ߵ�������϶�������</returns>
        public override ArrayList GetIcdFromApp(FS.HISFC.Models.Operation.OperationAppllication opsApp)
        {
            ArrayList IcdAl = new ArrayList();
            ArrayList rtnAl = new ArrayList();

            //����סԺ��ˮ��strInPatientNo			
            switch (opsApp.PatientSouce)
            {
                case "1"://��������
                    string strInPatientNo1 = string.Empty;//����סԺ��ˮ�� 
                    strInPatientNo1 = opsApp.PatientInfo.ID.ToString();
                    try
                    {
                        //TODO:����ҵ���
                        IcdAl = diagMgr.QueryOpsDiagnose(strInPatientNo1, "7");//"7"Ϊ��ǰ�������
                        foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase diag in IcdAl)
                        {
                            if (diag.OperationNo == opsApp.ID)
                                rtnAl.Add(diag);
                        }
                    }
                    catch
                    {
                        return rtnAl;
                    }
                    break;
                    break;
                case "2"://סԺ����
                    string strInPatientNo = string.Empty;//����סԺ��ˮ�� 
                    strInPatientNo = opsApp.PatientInfo.ID.ToString();
                    try
                    {
                        //TODO:����ҵ���
                        IcdAl = diagMgr.QueryOpsDiagnose(strInPatientNo, "7");//"7"Ϊ��ǰ�������
                        foreach (FS.HISFC.Models.HealthRecord.DiagnoseBase diag in IcdAl)
                        {
                            if (diag.OperationNo == opsApp.ID)
                                rtnAl.Add(diag);
                        }
                    }
                    catch
                    {
                        return rtnAl;
                    }
                    break;
            }
            return rtnAl;
        }
    }
}
