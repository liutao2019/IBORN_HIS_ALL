using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using FS.HISFC.BizLogic.RADT;
using System.Windows.Forms;
namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [功能描述: 整合的入出转管理类]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public class RADT : IntegrateBase
    {
        #region 变量

        /// <summary>
        /// 急诊留观业务层
        /// </summary>
        protected OutPatient radtEmrManager = new OutPatient();       

        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        /// <summary>
        /// 床位管理
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Bed managerBed = new FS.HISFC.BizLogic.Manager.Bed();

        /// <summary>
        /// 医嘱管理
        /// </summary>
        protected FS.HISFC.BizLogic.Order.Order managerOrder = new FS.HISFC.BizLogic.Order.Order();

        /// <summary>
        /// 科室管理
        /// </summary>
        protected FS.HISFC.BizLogic.Manager.Department managerDepartment = new FS.HISFC.BizLogic.Manager.Department();

        /// <summary>
        /// 入出转业务层
        /// </summary>
        protected InPatient inPatienMgr = new InPatient();

        /// <summary>
        /// 生命体征管理
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.LifeCharacter lfchManagement = new FS.HISFC.BizLogic.RADT.LifeCharacter();

        /// <summary>
        /// 床位日志管理
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InpatientDayReport dayReportMgr = new InpatientDayReport();

        #endregion


        /// <summary>
        /// 设置数据库事务
        /// </summary>
        /// <param name="trans">数据库事务</param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            inPatienMgr.SetTrans(trans);
            radtEmrManager.SetTrans(trans);
            inpatientManager.SetTrans(trans);
            managerBed.SetTrans(trans);
            managerDepartment.SetTrans(trans);
            managerOrder.SetTrans(trans);
            lfchManagement.SetTrans(trans);
            inPatienMgr.SetTrans(trans);
            this.trans = trans;
        }

        #region 方法

        public int GetAutoPatientNO(ref string patientNO, ref bool isRecycle)
        {
            return this.inPatienMgr.GetAutoPatientNO(ref patientNO, ref isRecycle);
        }

        public int GetInputPatientNO(string patientNO, ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            string inpatientNO = this.inPatienMgr.GetMaxinPatientNOByPatientNO(patientNO);
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
                patient = this.QueryPatientInfoByInpatientNO(inpatientNO);
                patient.PatientNOType = FS.HISFC.Models.RADT.EnumPatientNOType.Second;
                //判断在院状态
                if (patient.PVisit.InState.ID.ToString() == "R" || patient.PVisit.InState.ID.ToString() == "I" 
                    || patient.PVisit.InState.ID.ToString() == "P" 
                    //|| patient.PVisit.InState.ID.ToString() == "B"
                    )
                {
                    //if (patient.PatientType.ID == "Y")// {5B3B503C-8CF5-415b-89EB-C11A4FEE8A19}
                    //{
                    //    this.Err = "此患者已在院治疗!";
                    //    return -1;
                    //}
                    //else if (patient.PatientType.ID == "L" || patient.PatientType.ID == "P")
                    //{
                    //    this.Err = "此患者已在院治疗!";

                    //    return -1;
                    //}
                }
                patient.ID = "T001";
                patient.InTimes = patient.InTimes + 1;
                patient.PVisit.PatientLocation.Bed.ID = "";
            }
            return 1;

        }

        public string GetNewInpatientNO()
        {
            return this.inPatienMgr.GetNewInpatientNO();
        }

        /// <summary>
        /// 自动生成住院号
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 失败: -1</returns>
        public int CreateAutoInpatientNO(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.AutoCreatePatientNO(patient) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 自动生成住院号
        /// </summary>
        /// <param name="patientNO">当前住院号</param>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 失败: -1</returns>
        public int CreateAutoInpatientNO(string patientNO, ref FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.AutoCreatePatientNO(patientNO, ref patient) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 通过门诊卡号信息获得最大住院流水号
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <returns>成功: 获得最大住院流水号 失败 :null </returns>
        public string GetMaxPatientNOByCardNO(string cardNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.GetMaxPatientNOByCardNO(cardNO);
        }

        /// <summary>
        /// 根据住院号获取最大次数
        /// </summary>
        /// <param name="patientNO">住院号</param>
        /// <returns></returns>
        public int GetMaxInTimes(string patientNO)
        {
            //查找最大住院号
            this.SetDB(inPatienMgr);

            ArrayList al = inPatienMgr.QueryInpatientNOByPatientNO(patientNO, false);
            if (al == null)
            {
                return -1;
            }
            else if (al.Count <= 0)
            {
                return 0;
            }
            string  inpatientNO="";
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                ////排除无非退院的
                //if ((FS.HISFC.Models.Base.EnumInState)Enum.Parse(typeof(FS.HISFC.Models.Base.EnumInState), obj.Name) == FS.HISFC.Models.Base.EnumInState.N)
                //{
                //    continue;
                //}
                if (string.Compare(inpatientNO,obj.ID)<0)
                {
                    inpatientNO = obj.ID;
                }
            }
            FS.HISFC.Models.RADT.PatientInfo patient = inPatienMgr.QueryPatientInfoByInpatientNO(inpatientNO);
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

        /// <summary>
        /// 获取住院天数
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <returns></returns>
        public int GetInDays(string inpatientNO)
        {
            //先查找接诊时间
            this.SetDB(inPatienMgr);
            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //0:登记时间算 当天出院不加1 
            //1:登记时间算 当天出院加1
            //2:接诊时间算 当天出院不加1 
            //3:接诊时间算 当天出院加1
            int inHosDayType = controlParam.GetControlParam<int>("ZY0004", false, 3);

            FS.HISFC.Models.RADT.PatientInfo patient = inPatienMgr.QueryPatientInfoByInpatientNO(inpatientNO);
            if (patient == null)
            {
                return -1;
            }
            else if (string.IsNullOrEmpty(patient.ID))
            {
                return 0;
            }

            DateTime dtInTime = DateTime.MinValue;
            if (inHosDayType == 2 || inHosDayType == 3)
            {
                ArrayList al = inPatienMgr.QueryPatientShiftInfoNew(inpatientNO);
                if (al == null)
                {
                    return -1;
                }
                else if (al.Count <= 0)
                {
                    return 0;
                }

                foreach (FS.HISFC.Models.Invalid.CShiftData shiftData in al)
                {
                    //找到等于接诊的K
                    if (shiftData.ShitType == "K")
                    {
                        dtInTime = FS.FrameWork.Function.NConvert.ToDateTime(shiftData.Memo);
                        break;
                    }
                }
            }
            else if (inHosDayType == 0 || inHosDayType == 1)
            {
                dtInTime = patient.PVisit.InTime;
            }

            //如果小于最小时间
            //if (dtInTime <= DateTime.MinValue)
            //{
            //    dtInTime = patient.PVisit.InTime;
            //}
            if (dtInTime <= DateTime.MinValue)
            {
                return 0;
            }

            DateTime dtOutTime = patient.PVisit.PreOutTime;
            if (dtOutTime <= DateTime.MinValue)
            {
                dtOutTime = patient.PVisit.OutTime;
            }

            if (dtOutTime <= DateTime.MinValue)
            {
                dtOutTime = inPatienMgr.GetDateTimeFromSysDateTime();
            }

            if (dtOutTime.Date == dtInTime.Date)//当天入院当天出院 算1天
            {
                return 1;
            }

            if (inHosDayType == 1 || inHosDayType == 3)
            {
                return (dtOutTime.Date - dtInTime.Date).Days + 1;
            }
            else
            {
                return (dtOutTime.Date - dtInTime.Date).Days;
            }
        }

        /// <summary>
        /// 获取接诊时间
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public DateTime GetArriveDate(string inpatientNO)
        {
            //先查找接诊时间
            this.SetDB(inPatienMgr);

            ArrayList al = inPatienMgr.QueryPatientShiftInfoNew(inpatientNO);
            if (al == null)
            {
                return DateTime.MinValue;
            }
            else if (al.Count <= 0)
            {
                return DateTime.MinValue;
            }
            DateTime dtInTime = DateTime.MinValue;

            foreach (FS.HISFC.Models.Invalid.CShiftData shiftData in al)
            {
                //找到等于接诊的K
                if (shiftData.ShitType == "K")
                {
                    return dtInTime = FS.FrameWork.Function.NConvert.ToDateTime(shiftData.Memo);
                }
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// 通过门诊卡号在com_patientInfo中获得患者基本信息
        /// </summary>
        /// <param name="cardNO">门诊卡号</param>
        /// <returns>成功:患者基本信息 失败 null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfo(string cardNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfo(cardNO);
        }

        //{971E891B-4E05-42c9-8C7A-98E13996AA17}
        /// <summary>
        /// 通过身份证号在com_patientInfo中获得患者基本信息
        /// </summary>
        /// <param name="IDNO">身份证号卡号</param>
        /// <returns>成功:患者基本信息 失败 null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByIDNO(string IDNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfoByIDNO(IDNO);
        }

        /// <summary>
        /// 通过身份证号在com_patientInfo中获得患者基本信息
        /// </summary>
        /// <param name="IDNO">身份证号卡号</param>
        /// <returns>成功:患者基本信息 失败 null</returns>
        public ArrayList QueryComPatientInfoListByIDNO(string IDNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfoListByIDNO(IDNO);
        }

        /// <summary>
        /// 通过住院号在com_patientInfo中获得患者基本信息
        /// </summary>
        /// <param name="IDNO">住院号</param>
        /// <returns>成功:患者基本信息 失败 null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByPatientNo(string patientNo)
        {
            this.SetDB(inPatienMgr);

            string sqlWhere = @" where patient_no='{0}' and rownum=1 order by in_date desc";
            sqlWhere = string.Format(sqlWhere, patientNo);
            ArrayList arr = inPatienMgr.PatientInfoGet(sqlWhere);

            FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

            if (arr != null && arr.Count > 0)
            {
                patientInfo = inPatienMgr.QueryComPatientInfo((arr[0] as FS.HISFC.Models.RADT.PatientInfo).PID.CardNO);
            }

            return patientInfo;
        }

        /// <summary>
        /// 获得患者婴儿
        /// </summary>
        /// <param name="inpatientNO">妈妈住院流水号</param>
        /// <returns></returns>
        public ArrayList QueryBabiesByMother(string inpatientNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryBabiesByMother(inpatientNO);
        }

        /// <summary>
        /// 通过医保卡号在com_patientInfo中获得患者基本信息
        /// </summary>
        /// <param name="cardNO">医保卡号</param>
        /// <returns>成功:患者基本信息 失败 null</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryComPatientInfoByMcardNO(string mcardNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryComPatientInfoByMcardNO(mcardNO);
        }

        /// <summary>
        /// 自动生成住院号
        /// </summary>
        /// <param name="patientNO">当前住院号</param>
        /// <param name="usedPatientNO">使用了的住院号</param>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 失败: -1</returns>
        public int CreateAutoInpatientNO(string patientNO, string usedPatientNO, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);
           
            if (inPatienMgr.AutoCreatePatientNO(patientNO, usedPatientNO, ref patient) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 检测住院号、住院次数重复情况，保证插入住院号的唯一性
        ///  查询患者住院号，住院次数的重复情况{4949C040-E8C9-49d9-9BC2-548F7892206B}
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>成功 1 失败: -1</returns>
        public int VerifyInpatientInTimes(string inpatientno, string inTimes)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.VerifyInpatientInTimes(inpatientno, inTimes);
        }

        /// <summary>
        /// 住院患者登记
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败: -1</returns>
        public int RegisterPatient(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.InsertPatient(patient) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 更新登记时候的血滞纳金和公费日限额和日限额累计and生育保险电脑号and日限额超标金额--By kuangyh
        /// </summary>
        /// <param name="patient">住院患者基本信息实体</param>
        /// <returns>成功 1 失败: -1</returns>
        public int UpdateFeePatientInfoForRegister(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);
            if (inPatienMgr.UpdateOtherPatientInfoForRegister(patient) == -1)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新患者信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UpdatePatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.UpdatePatient(patient) <=0)
            {
                return -1;
            }

            return 1;
        }
        /// <summary>
        /// 更新未使用的住院号为使用状态
        /// </summary>
        /// <param name="oldPatientNO">旧的住院号，未使用的</param>
        /// <returns>成功 1 并发 0 应该重新获取住院号 失败: -1</returns>
        public int UpdatePatientNOState(string oldPatientNO) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.UpdatePatientNoState(oldPatientNO) == -1) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 插入患者基本登记信息com_patientinfo
        /// </summary>
        /// <param name="patient">当前患者基本信息实体</param>
        /// <returns>成功 1 并发 0 应该重新获取住院号 失败: -1</returns>
        public int RegisterComPatient(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.InsertPatientInfo(patient) == -1) 
            {
                if (this.DBErrCode == 1)
                {
                    if (inPatienMgr.UpdatePatientInfo(patient) <= 0)
                    {
                        return -1;
                    }
                }
                else 
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 插入患者基本登记信息com_patientinfo--给办卡处用，如果插入不成功则报错，不更新
        /// </summary>
        /// <param name="patient">当前患者基本信息实体</param>
        /// <returns>成功 1 并发 0 应该重新获取住院号 失败: -1</returns>
        public int RegisterComPatientbyCreateCard(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.InsertPatientInfo(patient) == -1)
            {
                MessageBox.Show("插入患者信息出错！");
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 插入患者变更信息
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 并发 0  失败: -1</returns>
        public int InsertShiftData(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            if (inPatienMgr.SetShiftData(patient.ID, FS.HISFC.Models.Base.EnumShiftType.B, "住院登记", patient.PVisit.PatientLocation.Dept,
                patient.PVisit.PatientLocation.Dept, patient.IsBaby) <= 0) 
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 根据患者查询变更记录{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatientShiftInfoNew(string clinicNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientShiftInfoNew(clinicNO);
        }
        //{FA3B8CE6-0414-423a-A92D-33678E5FF193}
        /// <summary>
        /// 插入登记即接诊患者变更信息
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 并发 0  失败: -1</returns>
        public int InsertRecievePatientShiftData(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            //变更信息
            if (inPatienMgr.SetShiftData(patient.ID, FS.HISFC.Models.Base.EnumShiftType.K, "接诊", patient.PVisit.PatientLocation.NurseCell, patient.PVisit.PatientLocation.Bed, patient.IsBaby) < 0)
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 插入变更记录
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="shiftType">变更类型</param>
        /// <param name="shiftText">变更说明</param>
        /// <param name="oldShift">以前状态</param>
        /// <param name="newShift">当前状态</param>
        /// <returns>成功 1 并发 0  失败: -1</returns>
        public int InsertShiftData(string inpatientNO, FS.HISFC.Models.Base.EnumShiftType shiftType, string shiftText, FS.FrameWork.Models.NeuObject oldShift,
            FS.FrameWork.Models.NeuObject newShift)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.SetShiftData(inpatientNO, shiftType, shiftText, oldShift, newShift, false);
        }

        /// <summary>
        /// 插入担保信息,插入的担保条件已经判断
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 并发 0 应该重新获取住院号 失败: -1</returns>
        public int InsertSurty(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            if (patient.Surety.SuretyType.ID != null && patient.Surety.SuretyCost > 0 && patient.Surety.SuretyType.ID != string.Empty)
            {
                this.SetDB(inpatientManager);

                if (inpatientManager.InsertSurty(patient) <= 0)
                {
                    return -1;
                }

            }

            return 1;
        }

        /// <summary>
        /// 更新患者科室
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 失败 -1 没有更新到数据 0</returns>
        public int UpdatePatientDept(FS.HISFC.Models.RADT.PatientInfo patient) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientDeptByInpatientNo(patient);
        }
        //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
        /// <summary>
        /// 更新患者科室
        /// </summary>
        /// <param name="patient">患者基本信息实体</param>
        /// <returns>成功 1 失败 -1 没有更新到数据 0</returns>
        public int UpdatePatientNurse(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientNursCellByInpatientNo(patient);
        }

        /// <summary>
        /// 根据住院号查询患者基本信息
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <returns>成功: 患者基本信息实体 失败 null</returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomation(string inpatientNO) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryPatientInfoByInpatientNO(inpatientNO);
        }


        /// <summary>
        /// 根据卡号查询患者住院信息  //{839B57B8-B74C-4818-9647-9881A0CE9013}
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <returns>成功:号查询患者住院信息实体 失败 null</returns>
        public ArrayList GetPatientInfomationByCardNo(string cardno)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.GetPatientInfoByCardNOAndInState(cardno);
        }

        /// <summary>
        /// 根据住院号查询患者基本信息，增加查询饮食
        /// </summary>
        /// <param name="inpatientNO">住院流水号</param>
        /// <returns>成功: 患者基本信息实体 失败 null</returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfomationNew(string inpatientNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryPatientInfoByInpatientNONew(inpatientNO);
        }

        /// <summary>
        /// 查询一段时间内的登记患者
        /// </summary>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>成功: 患者集合 失败 Null</returns>
        public ArrayList QueryPatientsByDateTime(DateTime beginTime, DateTime endTime) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryPatient(beginTime, endTime);
        }
        /// <summary>
		/// 更新基本信息表－不是患者主表  表名：com_patientinfo
		/// </summary>
		/// <param name="PatientInfo"></param>
		/// <returns></returns>
        public int UpdatePatientInfo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientInfo(PatientInfo);
        }
        /// <summary>
		/// 插入病人基本信息表-不是患者主表 表名：com_patientinfo 
		/// </summary>
		/// <param name="PatientInfo">患者基本信息</param>
		/// <returns>成功标志 0 失败，1 成功</returns>
		/// <returns></returns>
        public int InsertPatientInfo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.InsertPatientInfo(PatientInfo);
        }
        /// <summary>
        /// 获取卡号
        /// </summary>
        /// <returns></returns>
        public string GetCardNOSequece()
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.GetCardNOSequece();
        }

        /// <summary>
        /// 更新患者病案标记
        /// </summary>
        /// <param name="InpatientNO">住院流水号</param>
        /// <param name="CaseFlag">病案标记</param>
        /// <returns>1成功else失败</returns>
        public int UpdatePatientCaseFlag(string InpatientNO, string CaseFlag)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdateCase(InpatientNO, CaseFlag);
        }
        #endregion

        #region 患者查询
        #region  查询病人基本信息 com_patientinfo表
        [Obsolete("废弃,用QueryComPatientInfo代替")]
        public FS.HISFC.Models.RADT.PatientInfo GetPatient(string CardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.GetPatient(CardNO);
        }
        #endregion 
        /// <summary>
        /// 查询科室患者
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatient( string deptCode, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.PatientQuery(deptCode, istate);

        }

        /// <summary>
        /// 根据病区编码和患者状态查询病区患者
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatientBasicByNurseCell(string nurseCode, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.QueryPatientBasicByNurseCell(nurseCode, istate);
        }

         /// <summary>
        /// 患者查询-查询医疗组不同状态的患者//{AC6A5576-BA29-4dba-8C39-E7C5EBC7671E} 增加医疗组处理
        /// </summary>
        /// <param name="medicalTeamCode">科室编码</param>
        /// <param name="State">住院状态</param>
        /// <returns></returns>
        public ArrayList PatientQueryByMedicalTeam(string medicalTeamCode, FS.HISFC.Models.Base.EnumInState inState, string deptCode)
        {
            this.SetDB(radtEmrManager);
            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.PatientQueryByMedicalTeam(medicalTeamCode, istate,deptCode);
        }

        /// <summary>
        /// 根据状态查询患者
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatient( FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.QueryPatientBasicByInState(istate);

        }
        /// <summary>
        /// 根据病区状态查询患者
        /// </summary>
        /// <param name="nurseCellID">病区编码</param>
        /// <param name="inState">住院状态</param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndState(string nurseCellID,FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCell(nurseCellID,inState);

        }
        /// <summary>
        /// 根据病区科室状态查询患者
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndDept(string nurseCellID, string deptCode,FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCellAndDept(nurseCellID,deptCode,inState);

        }
                 
        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(DateTime beginTime,DateTime endTime, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);

            
            return inPatienMgr.QueryPatientInfoByTimeInState(beginTime, endTime, inState.ToString());

        }
        /// <summary>
        /// 获得医生的患者
        /// </summary>
        /// <param name="objDoc"></param>
        /// <param name="inState"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByHouseDoc(FS.FrameWork.Models.NeuObject objDoc,FS.HISFC.Models.Base.EnumInState inState,string deptCode)
        {
             this.SetDB(inPatienMgr);

            
            return inPatienMgr.QueryHouseDocPatient(objDoc, inState, deptCode);
            
        }
        /// <summary>
        /// 根据病区状态查询患者(欠费)  {62EAD92D-49F6-45d5-B378-1E573EC27728}
        /// </summary>
        /// <param name="nurseCellID">病区编码</param>
        /// <param name="inState">住院状态</param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndStateForAlert(string nurseCellID, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCellForAlert(nurseCellID, inState);

        }
        /// <summary>
        /// 获得指定科室及医生的会诊患者
        /// </summary>
        /// <param name="objDoc"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByConsultation(FS.FrameWork.Models.NeuObject objDoc, DateTime dtBegin,DateTime dtEnd, string deptCode)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryConsultation(objDoc, "0", dtBegin, dtEnd, deptCode);
            
        }

        /// <summary>
        /// 获得分给医生权限的患者
        /// </summary>
        /// <param name="objDoc"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByPermission(FS.FrameWork.Models.NeuObject objDoc)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByPermission(objDoc.ID, ((FS.HISFC.Models.Base.Employee)objDoc).Dept.ID);
            
        }

        /// <summary>
        /// 查询住院流水号根据住院号
        /// 查找患者多次入院的医嘱
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <returns></returns>
        public ArrayList QueryInpatientNoByPatientNo(string patientNo)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.QueryInpatientNOByPatientNO(patientNo);
        }

        /// <summary>
        /// 根据病区科室状态查询患者（欠费患者）{62EAD92D-49F6-45d5-B378-1E573EC27728}
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByNurseCellAndDeptForAlert(string nurseCellID, string deptCode, FS.HISFC.Models.Base.EnumInState inState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByNurseCellAndDeptForAlert(nurseCellID, deptCode, inState);

        }
        //患者查询
		/// <summary>
		/// 患者查询-按住院号查
		/// </summary>
		/// <param name="inPatientNO">住院流水号</param>
		/// <returns>患者信息 PatientInfo</returns>
        public FS.HISFC.Models.RADT.PatientInfo QueryPatientInfoByInpatientNO(string inPatientNO)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientInfoByInpatientNO(inPatientNO);
        }

        //患者查询 add by zhy 用于处理患者的终端费用
        /// <summary>
        /// 患者查询-按住院号查 
        /// </summary>
        /// <param name="inPatientNO">住院流水号</param>
        /// <returns>患者信息 PatientInfo</returns>
        /// <summary>
        /// 按姓名查询患者基本信息 FS.HISFC.Models.RADT.PatientInfo
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public decimal QueryPatientTerminalFeeByInpatientNO(string inPatientNO)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientTerminalFeeByInpatientNO(inPatientNO);
        }

        public ArrayList QueryPatientByName(string name)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByName(name);
        }
        /// <summary>
        /// {F5F57671-B453-45ff-A663-A682A000F567}
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByNameAndCardNo(string name)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByNameAndCardno(name);
        }

        //{8659FDB2-4200-475c-83B6-37092AD86D7D}
        public ArrayList QueryPatientByPhone(string phone)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientByPhone(phone);
        }

        /// <summary>
		/// 患者查询-按住院号查
		/// </summary>
		/// <param name="inPatientNO">患者住院流水号</param>
		/// <returns>返回患者信息</returns>
        public FS.HISFC.Models.RADT.PatientInfo GetPatientInfoByPatientNO(string inPatientNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.GetPatientInfoByPatientNO(inPatientNO);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNO"></param>
        /// <returns></returns>
        public ArrayList QureyPatientInfobyCardno(string cardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QureyPatientInfo(cardNO);
        }

        /// <summary>
        /// 查询患者基本信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QueryComPatientInfoByName(string name)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryComPatientInfoByName(name);
        }
        #endregion



        /// <summary>
        /// 根据有效出院召回的有效天数查询科室出院登记患者信息
        /// ----Create By By ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="inState"></param>
        /// <param name="vaildDays"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByVaildDate(string deptCode, FS.HISFC.Models.Base.EnumInState inState, int vaildDays)
        {
            this.SetDB(inPatienMgr);
            FS.HISFC.Models.RADT.InStateEnumService istate = new FS.HISFC.Models.RADT.InStateEnumService();
            istate.ID = inState;
            return inPatienMgr.PatientQueryByVaildDate(deptCode, istate, vaildDays);
        }

        /// <summary>
        /// 根据有效召回期查询一段时间内某个科室的出院登记患者(起止时间  科室代码 有效天数)
        /// ----Create By By ZhangQi
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="vaildDays"></param>
        /// <returns></returns>
        public ArrayList QueryOutHosPatient(string deptCode, string beginTime, string endTime, int vaildDays, int myPaientState)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.OutHosPatientByState(deptCode, beginTime, endTime, vaildDays, myPaientState);
        }

        /// <summary>
        /// 按就诊卡号查询住院期间有病案的患者
        /// by niuxinyuan
        /// </summary>
        /// <param name="cardNO">就诊卡号</param>
        /// <returns></returns>
        public ArrayList GetPatientInfoHaveCaseByCardNO(string cardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.GetPatientInfoHaveCaseByCardNO(cardNO);
        }

        //{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} 婴儿的费用是否可以收取到妈妈身上

        /// <summary>
        /// 通过婴儿的住院流水号,查询母亲的住院流水号
        /// </summary>
        /// <param name="babyInpatientNO">婴儿住院流水号</param>
        /// <returns>母亲的住院流水号 错误返回 null 或者 string.Empty</returns>
        public string QueryBabyMotherInpatientNO(string babyInpatientNO)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.QueryBabyMotherInpatientNO(babyInpatientNO);
        }
        //{02B13899-6FE7-4266-AC64-D3C0CDBBBC3F} 婴儿的费用是否可以收取到妈妈身上 结束
        #region 根据医保卡号查询住院患者信息
        /// <summary>
        /// 根据医保卡号查询住院患者信息
        /// </summary>
        /// <param name="markNO"></param>
        /// <returns></returns>
        public ArrayList PatientQueryByMcardNO(string mcardNO)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.PatientQueryByMcardNO(mcardNO);
        }
        #endregion
        #region 入出转大函数

        /// <summary>
        /// 更新是否启用警戒线
        /// </summary>
        /// <param name="payKindCode">合同单位类别 ALL表示全部</param>
        /// <param name="pactCode">合同单位 ALL表示全部</param>
        /// <param name="nureseCode">病区编码 ALL表示全部</param>
        /// <param name="inPaitentNo">住院流水号 ALL表示全部</param>
        /// <param name="alterFlag">是否启用警戒线</param>
        /// <returns></returns>
        public int UpdatePatientAlertFlag(string payKindCode, string pactCode, string nureseCode, string inPaitentNo, bool alterFlag)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertFlag(payKindCode, pactCode, nureseCode, inPaitentNo, alterFlag);
        }

        /// <summary>
        /// 更新是否启用警戒线
        /// </summary>
        /// <param name="payKindCode">合同单位类别 ALL表示全部</param>
        /// <param name="pactCode">合同单位 ALL表示全部</param>
        /// <param name="nureseCode">病区编码 ALL表示全部</param>
        /// <param name="inPaitentNo">住院流水号 ALL表示全部</param>
        /// <param name="alterFlag">是否启用警戒线</param>
        /// <param name="operCode">操作员</param>
        /// <returns></returns>
        public int UpdatePatientAlertFlag(string payKindCode, string pactCode, string nureseCode, string inPaitentNo, bool alterFlag, string operCode)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertFlag(payKindCode, pactCode, nureseCode, inPaitentNo, alterFlag, operCode);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// 更新患者警戒线根据住院号
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlert(string inpatientNO, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate) 
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlert(inpatientNO, moneyAlert,alertType,beginDate,endDate);
        }
        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// 更新患者警戒线根据合同单位
        /// </summary>
        /// <param name="pactID"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByPactID(string pactID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByPactID(pactID, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// 更新患者警戒线根据住院科室
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByDeptID(string deptID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByDeptID(deptID, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// 更新患者警戒线根据护士站
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByNurseCellID(string nurseCellID, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByNurseCellID(nurseCellID, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// 更新患者警戒线根据护士站和科室
        /// </summary>
        /// <param name="nurseCellID"></param>
        /// <param name="deptCode"></param>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertByNurseCellIDAndDept(string nurseCellID, string deptCode, decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientAlertByNurseCellIDAndDept(nurseCellID, deptCode, moneyAlert,alertType,beginDate,endDate);
        }

        //{A45EE85D-B1E3-4af0-ACAD-9DAF65610611}
        /// <summary>
        /// 更新患者警戒线所有患者
        /// </summary>
        /// <param name="moneyAlert"></param>
        /// <returns></returns>
        public int UpdatePatientAlertAll(decimal moneyAlert, string alertType, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.UpdatePatientAlert(moneyAlert,alertType,beginDate,endDate);           
        }


        /// <summary>
        /// 召回患者
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int CallBack(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed Bed)
        {
            //控制当患者正在出院结算的时候不能召回.
            string stopAccountFlag = inpatientManager.GetStopAccount(patientInfo.ID);

            if (stopAccountFlag == "1")
            {
                //关帐,患者正在结算
                //this.Err = "患者正在结算...请稍后再试!";{92467DA0-BE20-4a4b-8596-62598E3728A3}
                this.Err = "患者正在结算或者已经封帐...，请与住院处联系";
                return -1;
            }

            int parm = 0;
            FS.HISFC.Models.RADT.PatientInfo pMother = new FS.HISFC.Models.RADT.PatientInfo();

            //判断婴儿召回
            if (patientInfo.IsBaby)
            {
                string motherPatientNo = inPatienMgr.QueryBabyMotherInpatientNO(patientInfo.ID);

                if (string.IsNullOrEmpty(motherPatientNo))
                {
                    this.Err = "获取母亲住院号失败,婴儿住院号：" + patientInfo.PID.PatientNO + inPatienMgr.Err;
                    return -1;
                }

                //取婴儿妈妈的住院信息
                pMother = inPatienMgr.QueryPatientInfoByInpatientNO(motherPatientNo);
                if (pMother == null || pMother.ID == "")
                {
                    this.Err = "查找:" + patientInfo.Name + "母亲信息出错!" + inPatienMgr.Err;
                    return -1;
                }

                //如果妈妈不是在院状态,不能单独召回婴儿
                if (pMother.PVisit.InState.ID.ToString() != "I")
                {
                    this.Err = patientInfo.Name + "的母亲" + pMother.Name + "是出院登记状态,请先召回母亲!";
                    return -1;
                }

                //婴儿召回的床应该跟妈妈相同.不处理床位信息
                Bed = pMother.PVisit.PatientLocation.Bed.Clone();
            }

            Bed = managerBed.GetBedInfo(Bed.ID);

            //如果患者不是婴儿,不允许召回到占用的床位
            if (patientInfo.IsBaby == false &&
                Bed.Status.ID.ToString() != "U" && Bed.Status.ID.ToString() != "H")
            {
                this.Err = "您所选择的床位为" + Bed.Status.Name + ", 无法召回!";
                return -1;
            }

            //变更类型:召回
            parm = inPatienMgr.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.C, "召回");

            if (parm == -1)
            {

                this.Err = "召回失败！" + inPatienMgr.Err;
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "召回失败! 患者信息有变动,请刷新当前窗口";
                return -1;
            }

            patientInfo.PVisit.PatientLocation.Bed = Bed;
            if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.C) == -1)
            {
                Err = "处理床位日志错误！\r\n" + dayReportMgr.Err;
                return -1;
            }

            //放在前面防止出院后查询不到婴儿
            ArrayList al = inPatienMgr.QueryBabiesByMother(patientInfo.ID);
            if (al == null)
            {
                Err = inPatienMgr.Err;
                return -1;
            }
            for (int i = 0; i < al.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo babyInfo = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                babyInfo = inPatienMgr.QueryPatientInfoByInpatientNO(babyInfo.ID);
                if (babyInfo == null)
                {
                    Err = "处理床位日志错误！\r\n" + inPatienMgr.Err;
                    return -1;
                }
                if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.C) == -1)
                {
                    Err = "处理床位日志错误！\r\n" + dayReportMgr.Err;
                    return -1;
                }
            }

            this.Err = "召回成功！";
            return 1;
        }

        /// <summary>
        /// 入院登记接诊
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bed"></param>
        /// <returns></returns>
        public int ArrivePatientForReg(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bed)
        {
            if (managerBed.SetBedInfo(bed) == -1)
            {
                Err = managerBed.Err;
                return -1;
            }

            patientInfo.PVisit.PatientLocation.Bed = bed;
            if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.B) == -1)
            {
                Err = dayReportMgr.Err;
                return -1;
            }
            return 1;
        }


        #region 急诊留观出院召回
        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// 急诊留观出院召回
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Bed"></param>
        /// <param name="isOut">是否出关召回</param>
        /// <returns></returns>
        public int CallBack(FS.HISFC.Models.Registration.Register patientInfo, FS.HISFC.Models.Base.Bed Bed,bool isOut)
        {
            int parm = 0;

            Bed = managerBed.GetBedInfo(Bed.ID);

            //如果患者不是婴儿,不允许召回到占用的床位
            if (Bed.Status.ID.ToString() != "U" && Bed.Status.ID.ToString() != "H")
            {
                this.Err = "您所选择的床位为" + Bed.Status.Name + ", 无法召回!";
                return -1;
            }
            if (isOut)
            {
                //变更类型:出院召回
                parm = radtEmrManager.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.EC, "留观召回");
            }
            else
            {
                //变更类型:转住院召回
                parm = radtEmrManager.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.IC, "留观召回");
            }
            if (parm == -1)
            {

                this.Err = "留观召回失败！" + inPatienMgr.Err;//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "留观召回失败! 患者信息有变动,请刷新当前窗口";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }


            this.Err = "留观召回成功！";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
            return 1;
        }

        #endregion

        /// <summary>
        /// 接诊
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Bed"></param>
        /// <returns></returns>
        public int ArrivePatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed Bed)
        {
            int parm = 0;


            //判断选择的床位是否可用
            Bed = managerBed.GetBedInfo(Bed.ID);
            if (Bed.Status.ID.ToString() != "U" &&
                Bed.Status.ID.ToString() != "H")
            {
                this.Err = "您所选择的床位为" + Bed.Status.Name;
                return -1;
            }

            //{FA32C976-E003-4a10-9028-71F2CD154052} 固定费用时间
            DateTime saveDate = patientInfo.PVisit.RegistTime;
            //patientInfo.PVisit.RegistTime = inPatienMgr.GetDateTimeFromSysDateTime();// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}


            //接珍处理(1更新患者信息, 2插入接珍表)
            parm = inPatienMgr.RecievePatient(patientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.K, "接诊");

            //{FA32C976-E003-4a10-9028-71F2CD154052} 固定费用时间
            patientInfo.PVisit.RegistTime = saveDate;

            if (parm == -1)
            {

                this.Err = "接诊失败！" + inPatienMgr.Err;
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "接诊失败! 患者信息有变动,请刷新当前窗口";
                return -1;
            }

            patientInfo.PVisit.PatientLocation.Bed = Bed;
            if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.K) == -1)
            {
                Err = "处理床位日志错误！\r\n" + dayReportMgr.Err;
                return -1;
            }

            this.Err = "接诊成功！";

            return 1;
        }

        /// <summary>
        /// 急诊留观接诊
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="Bed"></param>
        /// <returns></returns>
        public int EmrArrivePatient(FS.HISFC.Models.Registration.Register outpatientInfo, FS.HISFC.Models.Base.Bed Bed)
        {
            int parm = 0;


            //判断选择的床位是否可用
            Bed = managerBed.GetBedInfo(Bed.ID);
            if (Bed.Status.ID.ToString() != "U" &&
                Bed.Status.ID.ToString() != "H")
            {
                this.Err = "您所选择的床位为" + Bed.Status.Name;
                return -1;
            }

            //接珍处理(1更新患者信息, 2插入接珍表)
            parm = radtEmrManager.RecievePatient(outpatientInfo, Bed, FS.HISFC.Models.Base.EnumShiftType.EK, "接诊");
            if (parm == -1)
            {
                this.Err = "留观接诊失败！" + inPatienMgr.Err;//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }
            else if (parm == 0)
            {

                this.Err = "留观接诊失败! 患者信息有变动,请刷新当前窗口";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}
                return -1;
            }

            this.Err = "留观接诊成功！";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}

            return 1;
        }

        /// <summary>
        /// 包床、挂床处理
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bed"></param>
        /// <param name="kind">0 </param>
        /// <returns></returns>
        public int WapBed(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bed, string kind)
        {
            if (this.inPatienMgr.SwapPatientBed(patientInfo, bed.ID, kind) == -1)
            {
                this.Err = "处理包床、挂床信息失败！\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.AddExtentBed(patientInfo, bed, true) == -1)
            {
                Err = "处理床位日志出错！\r\n" + dayReportMgr.Err;
                return -1;
            }

            if (this.InsertShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.ABD, "包床", new FS.FrameWork.Models.NeuObject(), bed) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 包床、挂床处理
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="bed"></param>
        /// <param name="kind">0 </param>
        /// <returns></returns>
        public int UnWapBed(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Base.Bed bed, string kind)
        {
            if (this.inPatienMgr.UnWrapPatientBed(patientInfo, bed.ID, kind) == -1)
            {
                this.Err = "处理包床、挂床信息失败！\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.AddExtentBed(patientInfo, bed, false) == -1)
            {
                Err = "处理床位日志出错！\r\n" + dayReportMgr.Err;
                return -1;
            }

            if (this.InsertShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.RBD, "解包床", bed, new FS.FrameWork.Models.NeuObject(" ", " ", " ")) == -1)
            {
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 更换医生
        /// </summary>
        /// <param name="PatientInfo"></param>
        /// <returns></returns>
        public int ChangeDoc(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            int parm = 0;
    
            //更新患者信息
            parm = inPatienMgr.RecievePatient(patientInfo, patientInfo.PVisit.InState);
            if (parm == -1)
            {

                this.Err = "更新出错！\n" + inPatienMgr.Err;
                return -1;
            }
            else if (parm == 0)
            {
              
                this.Err = "保存失败! 患者信息有变动,请刷新当前窗口";
                return -1;
            }

            this.Err = "保存成功！";
            return 1;
        }

       /// <summary>
       /// 转科确认
       /// </summary>
       /// <param name="PatientInfo"></param>
       /// <param name="nurseCell"></param>
       /// <param name="bedNo"></param>
       /// <returns></returns>
        public int ShiftIn(FS.HISFC.Models.RADT.PatientInfo PatientInfo,FS.FrameWork.Models.NeuObject nurseCell,string bedNo)
        {
            int parm = 0;

            FS.HISFC.Models.RADT.PatientInfo patientInfoOld=PatientInfo.Clone();

            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            newLocation = inPatienMgr.QueryShiftNewLocation(PatientInfo.ID, PatientInfo.PVisit.PatientLocation.Dept.ID);
            if (newLocation == null)
            {
                this.Err = "转科、转病区确认出错！\n患者信息有变动,请刷新当前窗口";
                return -1;
            }

            //如果没有找到数据,说明患者已经被确认,并发
            if (newLocation.Dept.ID == "" && newLocation.NurseCell.ID == "")
            {
                this.Err = "转科、转病区确认失败！\n" + inPatienMgr.Err;
                return -1;
            }
            if (newLocation.Dept.Name == "" && newLocation.Dept.ID != "")
            {
                newLocation.Dept = managerDepartment.GetDeptmentById(newLocation.Dept.ID);
                if (newLocation.Dept == null)
                {
                    this.Err = "转科确认失败！\n" + managerDepartment.Err;
                    return -1;
                }

            }
            #region {9A2D53D3-25BE-4630-A547-A121C71FB1C5}
            if (newLocation.NurseCell.Name == "" && newLocation.NurseCell.ID != "")
            {
                newLocation.NurseCell = managerDepartment.GetDeptmentById(newLocation.NurseCell.ID);
                if (newLocation.NurseCell == null)
                {
                    this.Err = "转病区确认失败！\n" + managerDepartment.Err;
                    return -1;
                }

            }
            #endregion
            newLocation.NurseCell = nurseCell.Clone();
            newLocation.Bed.ID = bedNo;	//新病床
            newLocation.Bed.Status.ID = "U";					//新床的状态
            newLocation.Bed.InpatientNO = "N";					//新床的患者住院流水号
            PatientInfo.User01 = newLocation.User01;
         
            try
            {
                //去系统时间
                DateTime sysDate = inPatienMgr.GetDateTimeFromSysDateTime();

                //接珍处理(1更新患者信息, 2插入接珍表), 注:只要有接珍操作,都进行此处理
                if (inPatienMgr.RecievePatient(PatientInfo, FS.HISFC.Models.Base.EnumInState.I) == -1)
                {

                    this.Err = "转科确认出错！\n" + inPatienMgr.Err;
                    return -1;
                }

                //转科处理
                parm = inPatienMgr.TransferPatient(PatientInfo, newLocation);
                if (parm == -1)
                {

                    this.Err = "转科确认出错！\n" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                   
                    this.Err = "保存失败! \n患者信息有变动,请刷新当前窗口";
                    return -1;
                }

                //释放包床和挂床
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(PatientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    //if (inPatienMgr.UnWrapPatientBed(PatientInfo, obj.ID, obj.Memo) < 0)
                    if (this.UnWapBed(PatientInfo, obj, obj.Memo) == -1)
                    {
                        this.Err = "释放床位失败！" + inPatienMgr.Err;
                        return -1;
                    }
                }


                //停止医嘱
                //System.Windows.Forms.DialogResult r = System.Windows.Forms.MessageBox.Show("是否停止以前的医嘱！", "转科确认", System.Windows.Forms.MessageBoxButtons.OKCancel);
                //if (r == System.Windows.Forms.DialogResult.OK)
                //{
                //    if (managerOrder.DcOrder(PatientInfo.ID, sysDate, "01", "转科停止") == -1)
                //    {

                //        this.Err = "停止医嘱失败！" + managerOrder.Err;
                //        return -1;
                //    }
                //}
                PatientInfo.PVisit.PatientLocation = newLocation;
                if (dayReportMgr.TransBed(patientInfoOld, PatientInfo) == -1)
                {
                    this.Err = "处理床位日志错误！\r\n" + dayReportMgr.Err;
                    return -1;
                }
              
                this.Err = "转科确认成功！";
            }
            catch (Exception ex)
            {
               
                this.Err = ex.Message;
                return -1;
            }

            return 1;
        }

        /// <summary>
        ///  转科申请，取消
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="newDept"></param>
        /// <param name="state">当前申请状态"1"</param>
        /// <param name="isCancel">是否取消</param>
        /// <returns></returns>
        public int ShiftOut(FS.HISFC.Models.RADT.PatientInfo patientInfo,
            FS.FrameWork.Models.NeuObject newDept,FS.FrameWork.Models.NeuObject newNurseCell,string state,bool isCancel)
        {

            //{DF72A3CF-38E6-4616-8287-DC989A4155F9} 婴儿转科
            //婴儿不允许转科
            //if (patientInfo.IsBaby) //
            //{
            //   this.Err = ("婴儿不可以单独转科,只能跟着母亲一同转科.");
            //    return -1;
            //}

            ////取母亲是否有在院的婴儿，如果有，就不允许转科
            //int baby = inPatienMgr.IsMotherHasBabiesInHos(patientInfo.ID);
            //if (baby == -1)
            //{
            //     this.Err = (inPatienMgr.Err);
            //    return -1;
            //}
            string isBringBaby = patientInfo.User01;// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}

            //取患者主表中最新的信息,用来判断并发
            FS.HISFC.Models.RADT.PatientInfo patient = inPatienMgr.QueryPatientInfoByInpatientNO(patientInfo.ID);
            if (patient == null)
            {
                 this.Err = (inPatienMgr.Err);
                return -1;
            }
            //如果患者已不在本科,则清空数据---当患者转科后,如果本窗口没有关闭,会出现此种情况
            if (patient.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            {
                 this.Err = ("患者已不在本病区,请刷新当前窗口");
                return -1;
            }
            //如果患者已不是在院状态,则不允许操作
            if (patient.PVisit.InState.ID.ToString() != patientInfo.PVisit.InState.ID.ToString())
            {
                this.Err = ("患者信息已发生变化,请刷新当前窗口");
                return -1;
            }


            
            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
            //{9A2D53D3-25BE-4630-A547-A121C71FB1C5}start
            FS.HISFC.Models.Base.Department tmpDept = new FS.HISFC.Models.Base.Department();
            tmpDept = managerDepartment.GetDeptmentById(newDept.ID);
            bool isShiftNurseCell = false;
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            //if (tmpDept.DeptType.ID.ToString() == "N")
            //{
            //    isShiftNurseCell = true;
            //    newLocation.NurseCell.ID = newDept.ID;
            //    newLocation.NurseCell.Name = newDept.Name;
            //    newLocation.NurseCell.Memo = newDept.Memo;

            //    if (patientInfo.PVisit.PatientLocation.NurseCell.ID == newLocation.NurseCell.ID)
            //    {
            //        this.Err = ("原病区不能与目标病区相同！");
            //        return -1;
            //    }
            //}
            ////{9A2D53D3-25BE-4630-A547-A121C71FB1C5}end
            //else
            //{
            //    //更新科室信息
            //    newLocation.Dept.ID = newDept.ID;
            //    newLocation.Dept.Name = newDept.Name;
            //    newLocation.Dept.Memo = newDept.Memo;

            //    if (patientInfo.PVisit.PatientLocation.Dept.ID == newLocation.Dept.ID)
            //    {
            //        this.Err = ("原科室不能与目标科室相同！");
            //        return -1;
            //    }
            //}
            //{F0BF027A-9C8A-4bb7-AA23-26A5F3539586}
            newLocation.Dept.ID = newDept.ID;
            newLocation.Dept.Name = newDept.Name;
            newLocation.Dept.Memo = newDept.Memo;
            newLocation.NurseCell.ID = newNurseCell.ID;
            newLocation.NurseCell.Name = newNurseCell.Name;
            newLocation.User01 = isBringBaby;
            

            if (patientInfo.PVisit.PatientLocation.NurseCell.ID == newLocation.NurseCell.ID && patientInfo.PVisit.PatientLocation.Dept.ID == newLocation.Dept.ID)
            {
                this.Err = ("原病区和原科室不能与目标病区和目标科室都相同！");
                return -1;
            }


            //转科申请/取消
            try
            {
                int parm;
                if (state == null || state == "") state = "1";
                parm = inPatienMgr.TransferPatientApply(patientInfo, newLocation,
                    isCancel, state);//状态，申请还是啥?
                if (parm == -1)
                {
                    this.Err = (inPatienMgr.Err);
                    return -1;
                }
                else if (parm == 0)
                {
                    //取消申请时,发生并发操作
                    if (isCancel)
                        this.Err = ("此转{0}申请已生效,不能取消.");
                    else
                        this.Err = ("此转{0}申请已被取消,不能确认");
                    return -1;
                }
                else
                {
                    if(isCancel)
                        this.Err = "取消转{0}申请成功！";
                    else
                        this.Err = "转{0}申请成功！";
                }
                
                if(this.Err.Contains("{0}"))
                {
                    if (isShiftNurseCell)
                    {
                        this.Err = string.Format(this.Err, "病区");
                    }
                    else
                    {
                        this.Err = string.Format(this.Err, "科");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        
            return 1;
        }
        /// <summary>
        /// CA对照
        /// </summary>
        /// <returns></returns>
        public int InsertCACompare(string emplcode, string cacode)
        {
            if (this.inPatienMgr.InsertCACompare(emplcode, cacode) != 1)
            {
                Err = "对照医生信息错误！\r\n" + inPatienMgr.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// CA对照作废
        /// </summary>
        /// <param name="cacode"></param>
        /// <returns></returns>
        public int UpdateCancelCACompare(string cacode)
        {
            if (this.inPatienMgr.UpdateCancelCACompare(cacode) != 1)
            {
                Err = "作废对照医生信息错误！\r\n" + inPatienMgr.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// CA对照判断
        /// </summary>
        /// <param name="cacode"></param>
        /// <returns></returns>
        public int QueryAllCompare(string emplcode, string cacode)
        {
            ArrayList al = new ArrayList();
            al = this.inPatienMgr.QueryAllCompare(emplcode,cacode);
            if (al.Count>0)
            {
                Err = "对照医生信息错误,该人员已经做了对照！\r\n" + inPatienMgr.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 登记新婴儿
        /// </summary>
        /// <param name="babyInfo"></param>
        /// <returns></returns>
        public int InsertNewBabyInfo(FS.HISFC.Models.RADT.PatientInfo babyInfo)
        {
            if (this.inPatienMgr.InsertNewBabyInfo(babyInfo) != 1)
            {
                Err = "登记新婴儿出错！\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.K) == -1)
            {
                Err = "处理床位日志错误！\r\n" + dayReportMgr.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 取消婴儿登记
        /// </summary>
        /// <param name="babyInfo"></param>
        /// <returns></returns>
        public int DiscardBabyRegister(FS.HISFC.Models.RADT.PatientInfo babyInfo)
        {
            if (this.inPatienMgr.DiscardBabyRegister(babyInfo.ID) != 1)
            {
                Err = "登记新婴儿出错！\r\n" + inPatienMgr.Err;
                return -1;
            }

            if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.OF) == -1)
            {
                Err = "处理床位日志错误！\r\n" + dayReportMgr.Err;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 出院登记德惠用
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <returns>-1 错误 0取消 1 成功</returns>
        public int OutPatientDH(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //停止医嘱
                //补收费（床费、医嘱收费）
                DialogResult r = MessageBox.Show("是否停止全部的医嘱！", "出院登记", MessageBoxButtons.YesNo);
                if (r == DialogResult.Yes)
                {
                    if (managerOrder.DcOrder(patientInfo.ID, managerOrder.GetDateTimeFromSysDateTime(), "01", "出院停止") == -1)
                    {
                        this.Err = "停止医嘱失败！" + managerOrder.Err;
                        return -1;
                    }
                }

                //更新患者状态、置空床位
                int parm = inPatienMgr.RegisterOutHospital(patientInfo);
                if (parm == -1)
                {

                    this.Err = "保存失败！" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    this.Err = "保存失败! 患者信息有变动,请刷新当前窗口";
                    return -1;
                }

                //释放包床和挂床
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (inPatienMgr.UnWrapPatientBed(patientInfo, obj.ID, obj.Memo) < 0)
                    {

                        this.Err = "释放床位失败！" + inPatienMgr.Err;
                        return -1;
                    }
                }

                this.Err = "出院登记成功！";

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }


        /// <summary>
        /// 出院登记
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <returns>-1 错误 0取消 1 成功</returns>
        public int OutPatient(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            try
            {
                //放在前面防止出院后查询不到婴儿
                ArrayList alBaby = inPatienMgr.QueryBabiesByMother(patientInfo.ID);
                if (alBaby == null)
                {
                    Err = inPatienMgr.Err;
                    return -1;
                }
                for (int i = 0; i < alBaby.Count; i++)
                {
                    FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[i] as FS.HISFC.Models.RADT.PatientInfo;
                    babyInfo = inPatienMgr.QueryPatientInfoByInpatientNO(babyInfo.ID);
                    if (babyInfo == null)
                    {
                        Err = inPatienMgr.Err;
                        return -1;
                    }
                    if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.O) == -1)
                    {
                        Err = dayReportMgr.Err;
                        return -1;
                    }
                }


                //更新患者状态、置空床位
                int parm = inPatienMgr.RegisterOutHospital(patientInfo);
                if (parm == -1)
                {
                    this.Err = "保存失败！" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    //查找不是婴儿标记，则提示错误 houwb 2011-5-16
                    if (inPatienMgr.QueryBabyMotherInpatientNO(patientInfo.ID) == "-1") //查找不到默认返回"-1"
                    {
                        this.Err = "保存失败! 患者信息有变动,请刷新当前窗口";
                        return -1;
                    }
                }


                if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.O) == -1)
                {
                    Err = dayReportMgr.Err;
                    return -1;
                }

                //释放包床和挂床
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (this.UnWapBed(patientInfo, obj, obj.Memo) < 0)
                    {
                        this.Err = "释放床位失败！" + inPatienMgr.Err;
                        return -1;
                    }
                }

                this.Err = "出院登记成功！";

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 急诊留观出院登记
        /// </summary>
        /// <param name="patientInfo">患者信息</param>
        /// <returns>-1 错误 0取消 1 成功</returns>
        public int OutPatient(FS.HISFC.Models.Registration.Register patientInfo)
        {
            try
            {
                //更新患者状态、置空床位
                int parm = radtEmrManager.RegisterOutHospital(patientInfo);
                
                if (parm == -1)
                {

                    this.Err = "保存失败！" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    this.Err = "保存失败! 患者信息有变动,请刷新当前窗口";
                    return -1;
                }

                //释放包床和挂床
                ArrayList al = new ArrayList();
                al = radtEmrManager.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (radtEmrManager.UnWrapPatientBed(patientInfo, obj.ID, obj.Memo) < 0)
                    {

                        this.Err = "释放床位失败！" + inPatienMgr.Err;
                        return -1;
                    }
                }

                this.Err = "留观出院成功！";//{1D08D511-B7E9-4e00-8A1D-87421815A4C4}

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 更新预约记录的状态
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdatePreInPatientState(string cardNo, string state)
        {
            SetDB(inPatienMgr);
            return inPatienMgr.UpdatePreInPatientState(cardNo,state);
        }
        /// <summary>
        /// 无费退院修改住院号
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <returns></returns>
        public int UnregisterChangePatientNO(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.SetDB(inPatienMgr);
            if (patientInfo == null)
            {
                this.Err = "患者信息不能为空！";
                return -1;
            }

            if (patientInfo.PID.PatientNO.StartsWith("N")
                || patientInfo.PID.PatientNO.StartsWith("B")
                || patientInfo.PID.PatientNO.StartsWith("F")
                || patientInfo.PID.PatientNO.StartsWith("L")
            || patientInfo.PID.PatientNO.StartsWith("C"))
            {
                //不需要进行回收住院号
                return 1;
            }

            string newPatientNO = "C" + patientInfo.PID.PatientNO.Substring(1);
            string oldPatientNO = patientInfo.PID.PatientNO;
            string newCardNO = patientInfo.PID.CardNO;
            if (patientInfo.PID.CardNO.StartsWith("T"))//已T开头的患者基本信息 需要修改患者基本信息
            {
                newCardNO = "C" + patientInfo.PID.CardNO.Substring(1);
            }

            //修改住院号
            if (this.inPatienMgr.UpdatePatientNO(patientInfo.ID, patientInfo.PID.CardNO, newPatientNO, newCardNO) == -1)
            {
                this.Err = "修改住院号错误" + this.inPatienMgr.Err;
                return -1;
            }

            //插入变更记录
            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "无费退院";
            if (inPatienMgr.SetShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.F, "回收住院号", obj1, obj2, patientInfo.IsBaby) < 0)
            {
                this.Err = "更新变更日志失败!" + inPatienMgr.Err;
                return -1;
            }

            //插入患者住院号回收表
            if (this.inPatienMgr.SetPatientNOShift(newPatientNO, oldPatientNO) == -1)
            {
                this.Err = "插入住院号回收表失败！" + radtEmrManager.Err;
                return -1;
            }

            return 1;
            
        }

        /// <summary>
        /// 无费退院
        /// </summary>
        /// <param name="patient">住院患者信息实体</param>
        /// <returns>1 成功 -1 失败</returns>
        public int UnregisterNoFee(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.SetDB(inPatienMgr);

            //如果在院状态是在院并且不是婴儿,则释放床位
            if (patientInfo.PVisit.InState.ID.ToString() == FS.HISFC.Models.Base.EnumInState.I.ToString()
                && !patientInfo.IsBaby)
            {
                //更新床位
                FS.HISFC.Models.Base.Bed newBed = patientInfo.PVisit.PatientLocation.Bed.Clone();
                newBed.InpatientNO = "N";	//床位无患者
                newBed.Status.ID = "U";	//床位状态是空床

                //更新床位状态
                int parm = inPatienMgr.UpdateBedStatus(newBed, patientInfo.PVisit.PatientLocation.Bed);
                if (parm == -1)
                {
                    this.Err = "释放床位失败" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    this.Err = "患者信息发生变动,请刷新当前窗口" + inPatienMgr.Err;
                    return -1;
                }

                //没有考虑有登记的婴儿而无费退院的问题 这种情况很少
                //ArrayList al = inPatienMgr.QueryBabiesByMother(patientInfo.ID);
                //if (al == null)
                //{
                //    Err = inPatienMgr.Err;
                //    return -1;
                //}
                //for (int i = 0; i < al.Count; i++)
                //{
                //    FS.HISFC.Models.RADT.PatientInfo babyInfo = al[i] as FS.HISFC.Models.RADT.PatientInfo;
                //    babyInfo = inPatienMgr.QueryPatientInfoByInpatientNO(babyInfo.ID);
                //    if (babyInfo == null)
                //    {
                //        Err = "处理床位日志错误！\r\n" + inPatienMgr.Err;
                //        return -1;
                //    }
                //    if (dayReportMgr.ArriveBed(babyInfo, FS.HISFC.Models.Base.EnumShiftType.OF) == -1)
                //    {
                //        Err = "处理床位日志错误！\r\n" + dayReportMgr.Err;
                //        return -1;
                //    }
                //}
                if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.OF) == -1)
                {
                    Err = dayReportMgr.Err;
                    return -1;
                }

                #region 释放包床

                //释放包床和挂床
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patientInfo.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    //if (inPatienMgr.UnWrapPatientBed(patientInfo, obj.ID, obj.Memo) < 0)
                    if (this.UnWapBed(patientInfo, obj, obj.Memo) < 0)
                    {
                        this.Err = "释放床位失败！" + inPatienMgr.Err;
                        return -1;
                    }
                }
                #endregion
            }

            //更新患者主表:住院状态改为无费退院N
            patientInfo.PVisit.InState.ID = FS.HISFC.Models.Base.EnumInState.N.ToString();
            // patientInfo.PVisit.OutTime = (DateTime)inPatienMgr.GetSysDateTime;

            if (inPatienMgr.UpdatePatient(patientInfo) != 1)
            {
                this.Err = "更新住院主表失败" + inPatienMgr.Err;
                return -1;
            }

            //处理变更日志

            FS.FrameWork.Models.NeuObject obj1 = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject obj2 = new FS.FrameWork.Models.NeuObject();
            obj2.ID = "无费退院";
            if (inPatienMgr.SetShiftData(patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.OF, "无费退院", obj1, obj2, patientInfo.IsBaby) < 0)
            {
                this.Err = "更新变更日志失败。" + inPatienMgr.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region 患者生命体征

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="lfch"></param>
        /// <returns></returns>
        public int InsertLifeCharacter(FS.HISFC.Models.RADT.LifeCharacter lfch)
        {
            this.SetDB(lfchManagement);
            return lfchManagement.InsertLifeCharacter(lfch);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="inPatientNO"></param>
        /// <param name="measureDate"></param>
        /// <returns></returns>
        public int DeleteLifeCharacter(string inPatientNO, DateTime measureDate)
        {
            this.SetDB(lfchManagement);
            return lfchManagement.DeleteLifeCharacter(inPatientNO, measureDate);
        }

        #endregion

        #region 住院处转科转床 by luzhp 2007-7-11
        /// <summary>
        /// 根据科室和在院状态查找患者
        /// </summary>
        /// <param name="dept_Code">科室编码</param>
        /// <param name="state">在院状态</param>
        /// <returns></returns>
        public ArrayList QueryPatientByDeptCode(string dept_Code,FS.HISFC.Models.RADT.InStateEnumService state)
        { 
            this.SetDB(inPatienMgr);
            return inPatienMgr.QueryPatientBasic(dept_Code, state);
        }

        public int ChangeDept(FS.HISFC.Models.RADT.PatientInfo PatientInfo, FS.HISFC.Models.RADT.Location newlocation)
        {
            try
            {
                #region 验证患者

                FS.HISFC.Models.RADT.PatientInfo patient = QueryPatientInfoByInpatientNO(PatientInfo.ID); //inPatienMgr.GetPatientInfoByPatientNO(PatientInfo.ID);
                if (patient.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                {
                    this.Err = "该患者未接诊！";
                    return -1;
                }
                #endregion

                if (patient.IsBaby)
                {
                    this.Err = "婴儿不可以单独转科、转床,\n只能跟着母亲一同转科";
                    return -1;
                }

                #region 验证床位
                string bedNo = newlocation.Bed.ID;
                FS.HISFC.Models.Base.Bed bed = managerBed.GetBedInfo(bedNo);
                if (bed == null)
                {
                    this.Err = "转科、床失败！";
                    return -1;
                }
                if (bed.Status.ID.ToString() == "W")
                {
                    MessageBox.Show("床号为 [" + bedNo + " ]的床位状态为包床，不能占用！", "提示：");
                    return -1;
                }
                else if (bed.Status.ID.ToString() == "C")
                {
                    MessageBox.Show("床号为 [" + bedNo + " ]的床位状态为关闭，不能占用！", "提示：");
                    return -1;
                }
                else if (bed.IsPrepay)
                {
                    MessageBox.Show("床号为 [" + bedNo + " ]的床位已经预约，不能占用！", "提示：");
                    return -1;
                }
                else if (!bed.IsValid)
                {
                    MessageBox.Show("床号为 [" + bedNo + " ]的床位已经停用，不能占用！", "提示：");
                    return -1;
                }
                #endregion

                //去系统时间
                DateTime sysDate = inPatienMgr.GetDateTimeFromSysDateTime();

                //接珍处理(1更新患者信息, 2插入接珍表), 注:只要有接珍操作,都进行此处理
                if (inPatienMgr.RecievePatient(patient, FS.HISFC.Models.Base.EnumInState.I) == -1)
                {

                    this.Err = "转科确认出错！\n" + inPatienMgr.Err;
                    return -1;
                }
                int parm;
                //转科处理
                parm = inPatienMgr.TransferPatientLocation(patient, newlocation);
                if (parm == -1)
                {

                    this.Err = "转科确认出错！\n" + inPatienMgr.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                   
                    this.Err = "保存失败! \n患者信息有变动,请刷新当前窗口";
                    return -1;
                }

                //释放包床和挂床
                ArrayList al = new ArrayList();
                al = inPatienMgr.GetSpecialBedInfo(patient.ID);
                for (int i = 0; i < al.Count; i++)
                {
                    FS.HISFC.Models.Base.Bed obj;
                    obj = (FS.HISFC.Models.Base.Bed)al[i];
                    if (inPatienMgr.UnWrapPatientBed(patient, obj.ID, obj.Memo) < 0)
                    {
                        this.Err = "释放床位失败！" + inPatienMgr.Err;
                        return -1;
                    }
                    
                }

                //停止医嘱
                System.Windows.Forms.DialogResult r = System.Windows.Forms.MessageBox.Show("是否停止以前的医嘱！", "转科确认", System.Windows.Forms.MessageBoxButtons.OKCancel);
                if (r == System.Windows.Forms.DialogResult.OK)
                {
                    if (managerOrder.DcOrder(PatientInfo.ID, sysDate, "01", "转科停止") == -1)
                    {

                        this.Err = "停止医嘱失败！" + managerOrder.Err;
                        return -1;
                    }
                }
              
                this.Err = "转科、床确认成功！";
                return parm;
            }
            catch (Exception ex)
            {
               
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// 变更身份
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="newobj"></param>
        /// <param name="oldobj"></param>
        /// <returns></returns>
        public int SetPactShiftData(FS.HISFC.Models.RADT.PatientInfo patient, FS.FrameWork.Models.NeuObject newobj, FS.FrameWork.Models.NeuObject oldobj)
        {
            this.SetDB(inPatienMgr);
            return inPatienMgr.SetPactShiftData(patient, newobj, oldobj);
        }

        #endregion

        #region 更新患者状态

        /// <summary>
        /// 更新患者在院状态
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="patientStatus"></param>
        /// <returns></returns>
        public int UpdatePatientState(FS.HISFC.Models.RADT.PatientInfo patientInfo,FS.HISFC.Models.RADT.InStateEnumService patientStatus)
        {
            this.SetDB(inPatienMgr);

            return inPatienMgr.UpdatePatientStatus(patientInfo, patientStatus);
        }

        #endregion

        #region 急诊留观
        public int RegisterObservePatient(FS.HISFC.Models.Registration.Register outPatient)
        { 
            this.SetDB(radtEmrManager);
            return radtEmrManager.RegisterObservePatient(outPatient);
        }

        //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
        /// <summary>
        /// 留观患者出关函数
        /// </summary>
        /// <returns></returns>
        public int OutObservePatientManager(FS.HISFC.Models.Registration.Register OutPatient, FS.HISFC.Models.Base.EnumShiftType type,string notes)
        {
            this.SetDB(radtEmrManager);
            return radtEmrManager.OutObservePatientManager(OutPatient, type,notes);
        }
        #endregion

        #region 更改患者病情{F0C48258-8EFB-4356-B730-E852EE4888A0}
        /// <summary>
        /// 更新患者病情状态（更新为病重）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdateBZ_Info(string id)
        {
            this.SetDB(inPatienMgr);
            return this.inPatienMgr.UpdateBZ_Info(id);
        }
        /// <summary>
        /// 更新患者病情状态（更新为普通）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int UpdatePT_Info(string id)
        {
            this.SetDB(inPatienMgr);
            return this.inPatienMgr.UpdatePT_Info(id);
        }

        public string SelectBQ_Info(string id)
        {
            this.SetDB(inPatienMgr);
            return this.inPatienMgr.SelectBQ_Info(id);
        }
        //{F0C48258-8EFB-4356-B730-E852EE4888A0}
        #endregion

        #region 取全院某一天的住院日报数据{A500A213-41EC-4d2f-87DA-4A2BB0D635A4}
        public ArrayList GetInpatientDayReportList(DateTime dateStat) 
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.GetInpatientDayReportList(dateStat);
        }
        #endregion

        #region 取全院某一天的住院日报数据{CB8DF724-12C6-47b9-A375-0F32167A6659}
        public ArrayList GetDayReportDetailList(DateTime dateBegin, DateTime dateEnd, string deptCode, string nurseCellCode) 
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.GetDayReportDetailList(dateBegin, dateEnd,deptCode,nurseCellCode);
        }
        #endregion

        #region 更新住院日报汇总表中一条记录{563EE3FB-8744-478a-8A63-B383DF637E94}
        public int UpdateInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport)
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.UpdateInpatientDayReport(dayReport);
        }
        #endregion

        #region 向住院日报汇总表中插入一条记录{C4275ACD-5523-4c15-903B-473527F0B43D}
        public int InsertInpatientDayReport(FS.HISFC.Models.HealthRecord.InpatientDayReport dayReport)
        {
            this.SetDB(dayReportMgr);
            return dayReportMgr.InsertInpatientDayReport(dayReport);
        }
        #endregion
    }

    ///// <summary>
    ///// 出院登记接口
    ///// </summary>
    //public interface IucOutPatient
    //{
    //    bool IsSelect
    //    {
    //        set;
    //    }
    //}
    ///// <summary>
    ///// 护士站出院召回接口
    ///// </summary>
    //public interface ICallBackPatient
    //{
    //    bool IsSelect
    //    {
    //        set;
    //    }
    //}

    ///// <summary>
    ///// 出院、出院召回等地方的判断,是否可以执行下一步操作
    ///// </summary>
    //public interface IPatientShiftValid
    //{
    //    /// <summary>
    //    /// 出院、出院召回等地方的判断,是否可以执行下一步操作
    //    /// </summary>
    //    /// <param name="p">患者信息</param>
    //    /// <param name="type">操作类型</param>
    //    /// <param name="err">错误</param>
    //    /// <returns>true判断成功 false错误返回错误err</returns>
    //    bool IsPatientShiftValid(FS.HISFC.Models.RADT.PatientInfo p, EnumPatientShiftValid type, ref string err);
    //}

}
