using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.RADT;
using FS.HISFC.Models.Base;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface.Common;
using FS.SOC.HISFC.InpatientFee.BizProcess;

namespace FS.SOC.HISFC.RADT.BizProcess
{
    /// <summary>
    /// [功能描述: 住院RADT相关的逻辑业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-03]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class Inpatient:AbstractBizProcess
    {
        #region 修改患者信息

        public int ModifyPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            return this.ModifyPatientInfo(patientInfo, false, null, false, patientInfo.InTimes);
        }

        public int ModifyPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo,bool isModifyPatientNO,string patientNO,bool isModifyInTimes,int inTimes)
        {
            if (patientInfo == null)
            {
                this.err = "患者信息为空！";
                return -1;
            }

            if (patientInfo.PID.PatientNO.Equals(patientNO))
            {
                isModifyPatientNO = false;
            }

            if (inTimes == patientInfo.InTimes)
            {
                isModifyInTimes = false;
            }
            else
            {
                patientInfo.InTimes = inTimes;
            }

            FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.BeginTransaction();
            this.SetDB(radtManager);


            //判断患者病床信息，是否已接诊，防止并发
            FS.HISFC.Models.RADT.PatientInfo patientInfoReGet = radtManager.QueryPatientInfoByInpatientNO(patientInfo.ID);

            //如当前患者床位信息为空 且 重新获取患者床位信息不为空 则提示
            if (string.IsNullOrEmpty(patientInfo.PVisit.PatientLocation.Bed.ID) && !string.IsNullOrEmpty(patientInfoReGet.PVisit.PatientLocation.Bed.ID))
            {
                this.RollBack();
                this.err = "患者床位信息已发生变动，请刷新后再试一次！";
                return -1;
            }

            //{1C12B07D-82EE-4e27-A0CD-794F8AA58F9E}
            if (patientInfoReGet.PVisit.PatientLocation.Bed.ID != patientInfo.PVisit.PatientLocation.Bed.ID)
            {
                this.RollBack();
                this.err = "患者床位信息已发生变动，请刷新后再试一次！";
                return -1;
            }

            //更新患者住院信息
            if (radtManager.UpdatePatient(patientInfo) != 1)
            {
                this.RollBack();
                this.err = radtManager.Err;
                return -1;
            }

            //更新患者基本信息
            if (radtManager.UpdatePatientInfoForInpatient(patientInfo) != 1)
            {
                this.RollBack();
                this.err = radtManager.Err;
                return -1;
            }

            bool isUpdateInTimes = isModifyInTimes;
            bool isPatientOut = false;
            FS.HISFC.Models.Base.EnumInState inState = FS.FrameWork.Public.EnumHelper.Current.GetEnum<FS.HISFC.Models.Base.EnumInState>(patientInfo.PVisit.InState.ID.ToString());
            if (inState == EnumInState.B || inState == EnumInState.N)
            {
                isPatientOut = true;
            }

            if (!isPatientOut && isModifyPatientNO && patientNO != null && patientNO.Trim().Length > 0)
            {
                patientNO = patientNO.Trim().PadLeft(10, '0');
                PatientInfo patient = new PatientInfo();
                //如果住院号有历史住院信息，获取病人信息和住院次数
                if (this.getInputPatientNO(patientNO, ref patient) < 1)
                {
                    this.RollBack();
                    return -1;
                }

                if (patient.PatientNOType == EnumPatientNOType.Second)
                {
                    if (!patient.Name.Equals(patientInfo.Name))
                    {
                        this.RollBack();
                        this.err = "输入的住院号对应的姓名与当前姓名不相同！";
                        return -1;
                    }

                    //去最大的住院次数加+1
                    int times = this.getMaxInTimes(patient.PID.PatientNO);
                    if (times <= 0)
                    {
                        patient.InTimes += 1;
                    }
                    else
                    {
                        patient.InTimes = times + 1;
                    }

                    patientInfo.InTimes = patient.InTimes;
                }
                else
                {
                    patientInfo.InTimes = 1;//等于一次
                }

                string newPatientNO = patient.PID.PatientNO;
                string oldPatientNO = patientInfo.PID.PatientNO;
                string oldCardNO = patientInfo.PID.CardNO;

                //修改住院号
                if (radtManager.UpdatePatientNO(patientInfo.ID, oldCardNO, newPatientNO) == -1)
                {
                    this.RollBack();
                    this.err = "修改住院号错误，" + radtManager.Err;
                    return -1;
                }

                //原来的住院次数大于1
                if (patientInfo.InTimes <= 1)
                {
                    //插入患者住院号回收表
                    if (radtManager.SetPatientNOShift(newPatientNO, oldPatientNO) == -1)
                    {
                        this.RollBack();
                        this.err = "回收住院号错误，" + radtManager.Err;
                        return -1;
                    }

                    if (radtManager.UpdatePatientNoState(oldPatientNO) == -1)
                    {
                        this.RollBack();
                        this.err = "回收住院号错误，" + radtManager.Err;
                        return -1;
                    }
                }

                isUpdateInTimes = true;
            }

            //未出院，并且须修改次数
            if (!isPatientOut && isUpdateInTimes)
            {
                if (radtManager.UpdatePatientInTimes(patientInfo.ID, patientInfo.InTimes) == -1)
                {
                    this.RollBack();
                    this.err = "修改住院次数错误，" + radtManager.Err;
                    return -1;
                }
            }

            FS.FrameWork.Models.NeuObject oldPatientObject = new FS.FrameWork.Models.NeuObject(patientInfoReGet.PID.PatientNO, patientInfoReGet.Name, patientInfoReGet.Memo);
            oldPatientObject.User01 = patientInfoReGet.User01;
            oldPatientObject.User02 = patientInfoReGet.User02;
            oldPatientObject.User03 = patientInfoReGet.User03;
            FS.FrameWork.Models.NeuObject newPatientObject = new FS.FrameWork.Models.NeuObject(patientNO, patientInfo.Name, patientInfo.Memo);
            //插入变更信息
            //if (radtManager.SetShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.F, "患者基本信息修改", patientInfoReGet, patientInfo, patientInfo.IsBaby) == -1)
            if (radtManager.SetShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.F, "患者基本信息修改", oldPatientObject, newPatientObject, patientInfo.IsBaby) == -1)
            {
                this.RollBack();
                this.err = "保存变更信息错误，" + radtManager.Err;
                return -1;
            }

            //提交事物
            this.Commit();

            return 1;
        }

        private int getInputPatientNO(string patientNO, ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
            this.SetDB(radtManager);

            string inpatientNO = radtManager.GetMaxinPatientNOByPatientNO(patientNO);
            if (inpatientNO == string.Empty)
            {
                //没有住院记录，说明患者第一次入院
                patient.PatientNOType = FS.HISFC.Models.RADT.EnumPatientNOType.First;
                patient.PID.PatientNO = patientNO;
                patient.PID.CardNO = "T" + patientNO.Substring(1, 9);
                patient.ID = "T001";
                patient.InTimes = 1;
            }
            else
            {
                //有入院记录，查询患者基本信息，
                patient = radtManager.QueryPatientInfoByInpatientNO(inpatientNO);
                patient.PatientNOType = FS.HISFC.Models.RADT.EnumPatientNOType.Second;
                //判断在院状态
                FS.HISFC.Models.Base.EnumInState instate = FS.FrameWork.Public.EnumHelper.Current.GetEnum<FS.HISFC.Models.Base.EnumInState>(patient.PVisit.InState.ID.ToString());

                if (instate == FS.HISFC.Models.Base.EnumInState.R || instate == FS.HISFC.Models.Base.EnumInState.I || instate == FS.HISFC.Models.Base.EnumInState.P /*|| instate == FS.HISFC.Models.Base.EnumInState.B*/)
                {
                    this.err = "输入的住院号目前处于在院状态(" + patient.PVisit.InState.Name + ")！";
                    return -1;
                }

                patient.ID = "T001";
                patient.InTimes = patient.InTimes + 1;
                patient.PVisit.PatientLocation.Bed.ID = "";
            }
            return 1;

        }

        private int getMaxInTimes(string patientNO)
        {
            FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
            //查找最大住院号
            this.SetDB(radtManager);

            ArrayList al = radtManager.QueryInpatientNOByPatientNO(patientNO, false);
            if (al == null)
            {
                return -1;
            }
            else if (al.Count <= 0)
            {
                return 0;
            }
            string inpatientNO = "";
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                //排除无非退院的
                if ((FS.HISFC.Models.Base.EnumInState)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumInState), obj.Memo) == FS.HISFC.Models.Base.EnumInState.N)
                {
                    continue;
                }
                if (string.Compare(inpatientNO, obj.ID) < 0)
                {
                    inpatientNO = obj.ID;
                }
            }
            FS.HISFC.Models.RADT.PatientInfo patient = radtManager.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patient == null)
            {
                return -1;
            }
            else if (string.IsNullOrEmpty(patient.ID))
            {
                return 0;
            }

            return patient.InTimes;
        }

        #endregion

        #region 身份变更

        public int ModifyPatientPactInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.PactInfo newPactInfo)
        {
            if (patientInfo == null)
            {
                this.err = "住院信息为空！";
                return -1;
            }

            if (newPactInfo == null)
            {
                this.err = "新身份信息为空！";
                return -1;
            }

            if (patientInfo.Pact.ID.Equals(newPactInfo.ID))
            {
                this.err = "原身份与新身份一致，不需要变更！";
                return 0;
            }

            #region 新合同单位
            FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            FS.HISFC.BizLogic.RADT.InPatient radtManager = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.SOC.HISFC.RADT.BizLogic.Inpatient socInpatientManager = new FS.SOC.HISFC.RADT.BizLogic.Inpatient();
            medcareInterfaceProxy.SetPactCode(newPactInfo.ID);
            this.BeginTransaction();
            this.SetDB(radtManager);
            this.SetDB(socInpatientManager);

            long returnValue = 0;
            FS.HISFC.Models.RADT.PatientInfo siPatientInfo = patientInfo.Clone();
            medcareInterfaceProxy.SetTrans(this.Trans);
            returnValue = medcareInterfaceProxy.Connect();
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "请确保待遇接口存在或正常初始化初始化失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(siPatientInfo);
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "待遇接口获得患者信息失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            returnValue = medcareInterfaceProxy.LogoutInpatient(siPatientInfo);
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "待遇接口无费退院失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            //变更住院主表信息
            if (socInpatientManager.UpdatePactInfo(patientInfo, newPactInfo) <= 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "修改住院主表信息错误，" + socInpatientManager.Err;
                return -1;
            }

            //插入变更记录
            if (radtManager.SetShiftData(patientInfo.ID, EnumShiftType.CP, "身份变更", patientInfo.Pact, newPactInfo, patientInfo.IsBaby) < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "更新变更表失败，" + radtManager.Err;
                return -1;
            }

            //获得更改后得患者信息 
            //chenxin 2012-11-02 旧函数
            // FS.HISFC.Models.RADT.PatientInfo oENewPatientInfo = radtManager.GetPatientInfoByPatientNO(patientInfo.ID);
            FS.HISFC.Models.RADT.PatientInfo oENewPatientInfo = radtManager.QueryPatientInfoByInpatientNO(patientInfo.ID);  
            //重新上传新
            returnValue = 0;
            medcareInterfaceProxy.SetTrans(this.Trans);
            medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);//设置旧合同单位
            returnValue = medcareInterfaceProxy.Connect();
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "请确保待遇接口存在或正常初始化初始化失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(oENewPatientInfo);
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "待遇接口获得患者信息失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            returnValue = medcareInterfaceProxy.UploadRegInfoInpatient(oENewPatientInfo);
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "待遇接口上传住院登记信息失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            #endregion

            //变更费用信息
            InpatientFee inpatientFeeManager = new InpatientFee();
            if (inpatientFeeManager.ProcessChangePact(patientInfo, newPactInfo) <= 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = inpatientFeeManager.Err;
                return -1;
            }

            #region 旧合同单位处理 // {ED2DA738-2376-4912-8E52-CE9462139845}
            returnValue = 0;
            medcareInterfaceProxy.SetPactCode(patientInfo.Pact.ID);
            medcareInterfaceProxy.SetTrans(this.Trans);
            returnValue = medcareInterfaceProxy.Connect();
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "请确保待遇接口存在或正常初始化初始化失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }

            returnValue = medcareInterfaceProxy.GetRegInfoInpatient(oENewPatientInfo);
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "旧待遇接口获得患者信息失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            returnValue = medcareInterfaceProxy.CancelRegInfoInpatient(oENewPatientInfo);
            if (returnValue < 0)
            {
                this.RollBack();
                medcareInterfaceProxy.Rollback();
                this.err = "旧待遇接口上传取消住院登记失败" + medcareInterfaceProxy.ErrMsg;
                return -1;
            }
            #endregion

            medcareInterfaceProxy.Commit();
            this.Commit();

            return 1;
        }

        #endregion
    }
}
