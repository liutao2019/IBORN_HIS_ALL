using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate.Operation
{
    /// <summary>
    /// [功能描述: 手术业务]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-31]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class Operation : FS.HISFC.BizLogic.Operation.Operation
    {

#region 字段
        private FS.HISFC.BizLogic.RADT.InPatient inPatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        private FS.HISFC.BizProcess.Integrate.Manager manager = new Manager();
        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new RADT();

        private FS.HISFC.BizLogic.Operation.OpsDiagnose diagMgr = new FS.HISFC.BizLogic.Operation.OpsDiagnose();
        //诊断控制类实例
        // TODO: 病案业务层，需要重加
        //public FS.HISFC.BizLogic.HealthRecord.Diagnose DiagnoseManager = new FS.HISFC.BizLogic.HealthRecord.Diagnose();

        private FS.HISFC.BizLogic.Registration.Register regManager = new FS.HISFC.BizLogic.Registration.Register();
        private FS.HISFC.BizProcess.Integrate.Registration.Registration regMgr = new FS.HISFC.BizProcess.Integrate.Registration.Registration();


#endregion
        /// <summary>
        /// 根据患者住院号获得当前在院的住院流水号
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
                    //寻找患者在院状态的那条住院主表记录
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
            // TODO: 添加诊断事务
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
        /// 根据手术序号获得手术诊断信息列表
        /// </summary>
        /// <param name="OperatorNo">手术申请单对象</param>
        /// <returns>患者的手术诊断对象数组</returns>
        public override ArrayList GetIcdFromApp(FS.HISFC.Models.Operation.OperationAppllication opsApp)
        {
            ArrayList IcdAl = new ArrayList();
            ArrayList rtnAl = new ArrayList();

            //患者住院流水号strInPatientNo			
            switch (opsApp.PatientSouce)
            {
                case "1"://门诊手术
                    string strInPatientNo1 = string.Empty;//患者住院流水号 
                    strInPatientNo1 = opsApp.PatientInfo.ID.ToString();
                    try
                    {
                        //TODO:病案业务层
                        IcdAl = diagMgr.QueryOpsDiagnose(strInPatientNo1, "7");//"7"为术前诊断类型
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
                case "2"://住院手术
                    string strInPatientNo = string.Empty;//患者住院流水号 
                    strInPatientNo = opsApp.PatientInfo.ID.ToString();
                    try
                    {
                        //TODO:病案业务层
                        IcdAl = diagMgr.QueryOpsDiagnose(strInPatientNo, "7");//"7"为术前诊断类型
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
