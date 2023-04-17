using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Assign.BizProcess
{
    /// <summary>
    /// [功能描述: SOC患者相关综合类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    public class OutPatientInfo : FS.SOC.HISFC.BizProcess.CommonInterface.Common.AbstractBizProcess
    {
        /// <summary>
        /// 更新挂号信息
        /// </summary>
        /// <param name="register"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.Registration.Register register,ref string error)
        {
            error = "";
            if (register == null || register.ID == "")
            {
                error = "患者信息为空";
                return -1;
            }
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.SOC.HISFC.RADT.BizLogic.ComPatient comPatientMgr = new FS.SOC.HISFC.RADT.BizLogic.ComPatient();
            this.BeginTransaction();
            regMgr.SetTrans(this.Trans);
            comPatientMgr.SetTrans(this.Trans);

            //更新患者基本信息
            if (comPatientMgr.UpdatePatient(register) < 1)
            {
                this.RollBack();
                error = "更新患者基本信息失败，原因：" + comPatientMgr.Err;
                return -1;
            }

            //更新挂号主表中的患者基本信息
            if (regMgr.UpdateRegInfo(register) < 1)
            {
                this.RollBack();
                error = "更新患者挂号信息失败，原因：" + regMgr.Err;
                return -1;
            }

            //修改信息更新挂号表急诊标志与温度
            if (regMgr.UpdateRegInfoAdd(register) < 1)
            {
                this.RollBack();
                error = "更新患者挂号信息失败，原因：" + regMgr.Err;
                return -1;
            }

            //更新挂号科室
            if (regMgr.UpdateDeptAndDoct(register.ID, register.DoctorInfo.Templet.Dept.ID, register.DoctorInfo.Templet.Dept.Name, register.DoctorInfo.Templet.Doct.ID, register.DoctorInfo.Templet.Doct.Name, register.DoctorInfo.SeeDate.ToString("yyyy-MM-dd HH:mm:ss")) < 1)
            {
                this.RollBack();
                error = "更新患者挂号科室失败，原因：" + regMgr.Err;
                return -1;
            }

            if (InterfaceManager.GetISaveRegister() != null)
            {
                if (InterfaceManager.GetISaveRegister().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, register) <= 0)
                {
                    this.RollBack();
                    error = InterfaceManager.GetISaveRegister().Err;
                    return -1;
                }
            }
            this.Commit();
            return 1;
        }

        /// <summary>
        /// 获取挂号信息（根据ClinicCode）
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register Get(string clinicCode, ref string error)
        {
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
            FS.HISFC.Models.Registration.Register register = registerMgr.GetByClinic(clinicCode);
            error = registerMgr.Err;
            return register;
        }

        /// <summary>
        /// 判断是否已经分诊
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public  bool JudgeTrige(string ClinicCode)
        {
            return new FS.HISFC.BizLogic.Registration.Register().QueryIsTriage(ClinicCode);
        }

        /// <summary>
        /// 判断是否退号
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public  bool Judgeback(string ClinicCode)
        {
            return new FS.HISFC.BizLogic.Registration.Register().QueryIsCancel(ClinicCode);
        }

        ///// <summary>
        ///// 获取挂号信息
        ///// </summary>
        ///// <param name="empiId"></param>
        ///// <param name="dt"></param>
        ///// <param name="error"></param>
        ///// <returns></returns>
        //public ArrayList GetByEmpiID(ref string empiId, DateTime dt, ref string error)
        //{
        //    FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
        //    //检索患者有效号
        //    ArrayList arlRegInfo = regMgr.QueryByEmpiId(empiId, dt);

        //    if (arlRegInfo == null)
        //    {
        //        error = regMgr.Err;
        //    }

        //    return arlRegInfo;
        //}

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <param name="cardNO">健康卡号或者CardNO</param>
        /// <param name="error">错误信息</param>
        /// <returns></returns>
        public  ArrayList GetCardNO(ref string cardNO, DateTime dt, ref string error)
        {
            FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.Models.Account.AccountCard accountCard = new FS.HISFC.Models.Account.AccountCard();
            if (accountManager.GetCardByRule(cardNO, ref accountCard) > 0)
            {
                cardNO = accountCard.Patient.PID.CardNO;
            }
            else
            {
                error = accountManager.Err;
                return null;
            }
            cardNO = accountCard.Patient.PID.CardNO;

            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();
            //检索患者有效号
            ArrayList arlRegInfo = regMgr.Query(cardNO, dt);
            if (arlRegInfo == null)
            {
                error = regMgr.Err;
            }

            return arlRegInfo;
        }

        /// <summary>
        /// 获取挂号信息
        /// </summary>
        /// <param name="cardNO">姓名</param>
        /// <returns></returns>
        public ArrayList GetName(ref string name, DateTime dt, ref string error)
        {
            FS.HISFC.BizLogic.Registration.Register regMgr = new FS.HISFC.BizLogic.Registration.Register();

            ArrayList arlRegInfo = regMgr.QueryName(name, dt);
            if (arlRegInfo == null)
            {
                error = regMgr.Err;
            }

            return arlRegInfo;
        }

        /// <summary>
        /// 查询未分诊的患者（根据护士站）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="nurseID"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyNurse(DateTime dt, string nurseID, ref string error)
        {
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
            ArrayList al = registerMgr.QueryNoTriagebyNurse(dt, nurseID);
            error = registerMgr.Err;
            return al;
        }

        /// <summary>
        /// 查询未分诊的患者（根据科室）
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="deptID"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyDept(DateTime dt, string deptID, ref string error)
        {
            FS.HISFC.BizLogic.Registration.Register registerMgr = new FS.HISFC.BizLogic.Registration.Register();
            ArrayList al = registerMgr.QueryNoTriagebyDept(dt, deptID);
            error = registerMgr.Err;
            return al;
        }
    }
}
