using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using API.GZSI.UI;
using System.Collections;
using System.Windows.Forms;
using API.GZSI.Business;
using FS.HISFC.Models.Account;

namespace API.GZSI
{
    /// <summary>
    /// 广州医保接口 by chen.fch@20191023
    /// 1、本接口与创智平台实现https的Post交互，所有接口的入参出参以Json形式传输。
    /// 2、原则上接口Json都通过实体序列化和反序列化转换，本接口直接操作接口业务实体。
    /// 3、HIS数据获取通过HIS现有类库函数获取，没有数据的直接在本工程添加函数获取。
    /// 4、涉及业务SQL都需要配置com_sql调用，不要在源码中写sql。
    /// 5、业务常数、字典都需要配置到com_controlargument、com_dictionary调用。
    /// 6、业务交互需要弹窗支持时，直接在本工程添加窗体
    /// </summary>
    public class Process : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare
    {
        /// <summary>
        /// 用于rollbace记录正交易的标志
        /// </summary>
        //private string rollBackStatus = string.Empty;

        /// <summary>
        /// rollback挂号实体
        /// </summary>
        //private FS.HISFC.Models.Registration.Register rollbackRegister = null;

        /// <summary>
        /// rollback费用列表
        /// </summary>
        //private ArrayList rollBackFeeList = new ArrayList();

        /// <summary>
        /// 综合业务
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 门诊账户业务层
        ///
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();


        /// <summary>
        /// 费用业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constMgr = new FS.HISFC.BizLogic.Manager.Constant();

        private FS.HISFC.BizProcess.Integrate.Common.ControlParam ctlParamManage = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 健康管理类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diaMgr = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();

        /// <summary>
        /// 药品业务层
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemPharmacyManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 非药品业务层
        /// </summary>
        FS.HISFC.BizLogic.Fee.Item itemManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// 医保数据操作
        /// </summary>
        private LocalManager localMgr = new LocalManager();

        /// <summary>
        /// 业务回滚管理类
        /// </summary>
        private RollbackManager rollbackMgr = new RollbackManager();


        /// <summary>
        /// 账户实体
        /// </summary>
        private FS.HISFC.Models.Account.Account account = null;
        /// <summary>
        /// 账户明细实体
        /// </summary>
        private FS.HISFC.Models.Account.AccountDetail accountDetail = null;

        private FS.HISFC.Models.Account.AccountCard accountCard = null;

        int returnvalue = 0;
        string SerialNumber = string.Empty;//交易流水号
        string strTransVersion = string.Empty;//交易版本号
        string strVerifyCode = string.Empty;//交易验证码

        #region IMedcare 成员

        /// <summary>
        /// 设置事务
        /// </summary>
        /// <param name="t"></param>
        public void SetTrans(System.Data.IDbTransaction t)
        {
        }

        /// <summary>
        /// 事务句柄
        /// </summary>
        public System.Data.IDbTransaction Trans
        {
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// 说明
        /// </summary>
        public string Description
        {
            get { return "广州医保"; }
        }

        /// <summary>
        /// 错误信息代码
        /// </summary>
        public string errCode;

        /// <summary>
        /// 错误信息代码
        /// </summary>
        public string ErrCode
        {
            get { return this.errCode; }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        private string errMsg;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg
        {
            get { return this.errMsg; }
            set { this.errMsg = value; }
        }

        /// <summary>
        /// 查询黑名单列表
        /// </summary>
        /// <param name="blackLists"></param>
        /// <returns></returns>
        public int QueryBlackLists(ref System.Collections.ArrayList blackLists)
        {
            throw new NotImplementedException();
        }

        #region 住院相关

        /// <summary>
        /// 是否在黑名单
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            return false;
        }

        /// <summary>
        /// 获取患者医保基本信息
        /// 应用场景：住院登录点“医保”按钮设置合同单位，调用此函数获取患者医保基本信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns>返回医保卡号：patient.SSN及patient.SIMainInfo实体信息</returns>
        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.Models.RADT.Patient patientInfo = patient as FS.HISFC.Models.RADT.Patient;

            if (this.localMgr.GetSIInfo(ref patientInfo))
            {
                return 1;
            }
            else
            {
                this.errMsg = "未找到医保信息！";
                return -1;
            }
        }

        /// <summary>
        /// 住院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            frmPatientQuery frmSIPatient = new frmPatientQuery();
            frmSIPatient.Patient = patient;
            frmSIPatient.OperateType = "2";
            if (frmSIPatient.ShowDialog() != DialogResult.OK)
            {
                this.ErrMsg = "取消医保入院登记！";
                return -1;
            }

            #region 入院办理2401
            InPatient2401 inPatient2401 = new InPatient2401();
            Models.Request.RequestGzsiModel2401 requestGzsiModel2401 = new API.GZSI.Models.Request.RequestGzsiModel2401();
            Models.Response.ResponseGzsiModel2401 responseGzsiModel2401 = new API.GZSI.Models.Response.ResponseGzsiModel2401();

            #region 就诊信息mdtrtinfo
            Models.Request.RequestGzsiModel2401.Mdtrtinfo mdtrtinfo2401 = new API.GZSI.Models.Request.RequestGzsiModel2401.Mdtrtinfo();
            mdtrtinfo2401.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y
            mdtrtinfo2401.insutype = patient.SIMainInfo.Insutype;//险种类型 Y
            mdtrtinfo2401.coner_name = patient.Name;//联系人姓名 
            mdtrtinfo2401.tel = string.IsNullOrEmpty(patient.PhoneHome) ? "-" : patient.PhoneHome;//联系电话 
            mdtrtinfo2401.begntime = patient.PVisit.InTime.ToString(Common.Constants.FORMAT_DATE);//开始时间 Y
            mdtrtinfo2401.mdtrt_cert_type = patient.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y
            mdtrtinfo2401.mdtrt_cert_no = patient.SIMainInfo.Certno;//就诊凭证编号  
            mdtrtinfo2401.med_type = patient.SIMainInfo.Med_type;//医疗类别 Y
            mdtrtinfo2401.ipt_no = patient.ID + this.localMgr.GetNextBalanceNo(patient.ID, "2");//patient.PID.PatientNO;//住院/门诊号 Y
            mdtrtinfo2401.medrcdno = patient.PID.PatientNO;//patient.ID;//病历号 
            mdtrtinfo2401.atddr_no = this.localMgr.GetGDDoct(patient.DoctorReceiver.ID);//主治医生编码 Y
            mdtrtinfo2401.chfpdr_name = this.interMgr.GetEmployeeInfo(patient.DoctorReceiver.ID).Name;//主治医师姓名 Y
            mdtrtinfo2401.adm_diag_dscr = "";//入院诊断描述 Y
            mdtrtinfo2401.adm_dept_codg = patient.PVisit.PatientLocation.Dept.ID;//入院科室编码 Y
            mdtrtinfo2401.adm_dept_name = patient.PVisit.PatientLocation.Dept.Name;//入院科室名称 Y
            if (patient.PVisit.PatientLocation.Bed != null && !string.IsNullOrEmpty(patient.PVisit.PatientLocation.Bed.ID))
            {
                mdtrtinfo2401.adm_bed = patient.PVisit.PatientLocation.Bed.ID;//入院床位 Y
            }
            else
            {
                mdtrtinfo2401.adm_bed = "001";//床位号  
            }
            mdtrtinfo2401.dscg_maindiag_code = "";//住院主诊断代码 Y
            mdtrtinfo2401.dscg_maindiag_name = "";//住院主诊断名称 Y
            mdtrtinfo2401.dise_codg = patient.SIMainInfo.Dise_code;//病种编码 
            mdtrtinfo2401.dise_name = patient.SIMainInfo.Dise_name;//病种名称 
            mdtrtinfo2401.main_cond_desc = "";//主要病情描述 
            mdtrtinfo2401.oprn_oprt_code = "";//手术操作代码 
            mdtrtinfo2401.oprn_oprt_name = "";//手术操作名称 
            mdtrtinfo2401.fpsc_no = frmSIPatient.Fpsc_no;//计划生育服务证号            //生育
            mdtrtinfo2401.matn_type = frmSIPatient.Matn_type;//生育类别                //生育 Y 
            mdtrtinfo2401.birctrl_type = frmSIPatient.Birctrl_type;//计划生育手术类别  //生育 
            mdtrtinfo2401.latechb_flag = frmSIPatient.Latechb_flag;//晚育标志          //生育 Y
            mdtrtinfo2401.geso_val = frmSIPatient.Geso_val;//孕周数                    //生育
            mdtrtinfo2401.fetts = frmSIPatient.Fetts;//胎次                            //生育
            mdtrtinfo2401.fetus_cnt = frmSIPatient.Fetus_cnt;//胎儿数                  //生育
            mdtrtinfo2401.pret_flag = frmSIPatient.Pret_flag;//早产标志                //生育 Y
            mdtrtinfo2401.birctrl_matn_date = frmSIPatient.Birctrl_matn_date;  //计划生育手术或生育日期   //生育
            mdtrtinfo2401.dise_type_code = ""; //病种类型

            requestGzsiModel2401.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2401.Mdtrtinfo();
            requestGzsiModel2401.mdtrtinfo = mdtrtinfo2401;

            #endregion

            #endregion

            #region 诊断信息diseinfo
            //FS.HISFC.Models.RADT.Diagnose diag = patient.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose;
            FS.HISFC.Models.RADT.Diagnose diag = null;

            //ArrayList Diagnoses = this.localMgr.InpatientBaseDiagnoseList(patient.ID);
            //if (Diagnoses == null || Diagnoses.Count <= 0)
            //{
            //    Diagnoses = this.localMgr.InpatientEMRDiagnoseList(patient.ID, "1");
            //}

            ArrayList Diagnoses = this.localMgr.InpatientEMRDiagnoseList(patient.ID, "1");
            if (Diagnoses == null || Diagnoses.Count <= 0)
            {
                diag = patient.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose;
                foreach (FS.HISFC.Models.RADT.Diagnose d in patient.Diagnoses)
                {
                    if (d.ID != "MS999")  //描述诊断排除// {AF420A61-59D9-4123-97AB-21E93B137744}
                    {
                        diag = d;
                        break;
                    }
                }
            }
            else
            {
                if (Diagnoses.Count == 1)
                {
                    diag = Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose;
                }
                else
                {
                    foreach (FS.HISFC.Models.RADT.Diagnose d in Diagnoses)
                    {
                        if (d.IsMain) diag = d;
                    }

                    if (diag == null)
                    {
                        diag = Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose;
                    }
                }
            }

            Models.Request.RequestGzsiModel2401.Diseinfo diseinfo2401 = new API.GZSI.Models.Request.RequestGzsiModel2401.Diseinfo();
            diseinfo2401.psn_no = patient.SIMainInfo.Psn_no; //
            diseinfo2401.diag_type = "1"; //诊断类型 
            diseinfo2401.maindiag_flag = "1"; //是否主诊断
            diseinfo2401.diag_srt_no = "1"; //诊断排序号
            diseinfo2401.diag_code = this.localMgr.GetICDYB2(diag.ID); //诊断代码
            diseinfo2401.diag_name = diag.Name; //诊断名称
            diseinfo2401.adm_cond = "-"; //入院病情
            diseinfo2401.diag_dept = patient.PVisit.PatientLocation.Dept.ID; //诊断科室
            diseinfo2401.dise_dor_no = this.localMgr.GetGDDoct(patient.DoctorReceiver.ID); //诊断医师工号
            diseinfo2401.dise_dor_name = this.interMgr.GetEmployeeInfo(patient.DoctorReceiver.ID).Name;//诊断医师姓名
            diseinfo2401.diag_time = patient.PVisit.InTime.ToString(Common.Constants.FORMAT_DATE);//this.localMgr.GetDateTimeFromSysDateTime().ToString(Common.Constants.FORMAT_DATETIME); //诊断时间
            requestGzsiModel2401.mdtrtinfo.dscg_maindiag_code = this.localMgr.GetICDYB2(diag.ID);//住院主诊断代码 Y
            requestGzsiModel2401.mdtrtinfo.dscg_maindiag_name = diag.Name;//住院主诊断名称 Y

            requestGzsiModel2401.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel2401.Diseinfo>();
            requestGzsiModel2401.diseinfo.Add(diseinfo2401);

            #endregion

            returnvalue = inPatient2401.CallService(requestGzsiModel2401, ref responseGzsiModel2401, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                this.ErrMsg = inPatient2401.ErrorMsg;
                return -1;
            }

            //返回信息
            patient.SIMainInfo.Mdtrt_id = responseGzsiModel2401.output.result.mdtrt_id;

            if (this.localMgr.InsertInPatientReg(patient) < 0)
            {
                this.CancelRegInfoInpatient(patient);
                this.ErrMsg = this.localMgr.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 二类门特住院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int InpatientUploadRegMT(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            #region 二类门特

            #region 入院办理2401
            InPatient2401 inPatient2401 = new InPatient2401();
            Models.Request.RequestGzsiModel2401 requestGzsiModel2401 = new API.GZSI.Models.Request.RequestGzsiModel2401();
            Models.Response.ResponseGzsiModel2401 responseGzsiModel2401 = new API.GZSI.Models.Response.ResponseGzsiModel2401();

            #region 就诊信息mdtrtinfo
            Models.Request.RequestGzsiModel2401.Mdtrtinfo mdtrtinfo2401 = new API.GZSI.Models.Request.RequestGzsiModel2401.Mdtrtinfo();
            mdtrtinfo2401.psn_no = "1000753288";
            mdtrtinfo2401.insutype = "310";
            mdtrtinfo2401.begntime = "2020-11-02 21:42:39";
            mdtrtinfo2401.mdtrt_cert_type = "02";
            mdtrtinfo2401.med_type = "21";
            mdtrtinfo2401.ipt_no = "1023";
            mdtrtinfo2401.atddr_no = "009999";
            mdtrtinfo2401.chfpdr_name = "信息科";
            mdtrtinfo2401.adm_diag_dscr = "泌尿";
            mdtrtinfo2401.adm_dept_codg = "3105";
            mdtrtinfo2401.adm_dept_name = "内一科";
            mdtrtinfo2401.adm_bed = "4047";
            mdtrtinfo2401.dscg_maindiag_code = "OYBDM06.291";
            mdtrtinfo2401.dscg_maindiag_name = "并发症诊断";
            mdtrtinfo2401.matn_type = "1";
            mdtrtinfo2401.birctrl_type = "1";
            mdtrtinfo2401.latechb_flag = "0";
            mdtrtinfo2401.pret_flag = "1";
            mdtrtinfo2401.dise_type_code = "";
            requestGzsiModel2401.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2401.Mdtrtinfo();
            requestGzsiModel2401.mdtrtinfo = mdtrtinfo2401;

            #region 就诊信息mdtrtinfo
            //requestGzsiModel2401.psn_no = "";//人员编号 Y
            //requestGzsiModel2401.insutype = "";//险种类型 Y
            //requestGzsiModel2401.coner_name = "";//联系人姓名 
            //requestGzsiModel2401.tel = "";//联系电话 
            //requestGzsiModel2401.begntime = "";//开始时间 Y
            //requestGzsiModel2401.mdtrt_cert_type = "";//就诊凭证类型 Y
            //requestGzsiModel2401.mdtrt_cert_no = "";//就诊凭证编号 
            //requestGzsiModel2401.hcard_basinfo = "";//持卡就诊基本信息 
            //requestGzsiModel2401.hcard_chkinfo = "";//持卡就诊校验信息 
            //requestGzsiModel2401.med_type = "";//医疗类别 Y
            //requestGzsiModel2401.ipt_otp_no = "";//住院/门诊号 Y
            //requestGzsiModel2401.medrcdno = "";//病历号 
            //requestGzsiModel2401.atddr_no = patient.DoctorReceiver.ID;//主治医生编码 Y
            //requestGzsiModel2401.chfpdr_name = this.interMgr.GetEmployeeInfo(patient.DoctorReceiver.ID).Name;//主治医师姓名 Y
            //requestGzsiModel2401.adm_diag_dscr = "";//入院诊断描述 Y
            //requestGzsiModel2401.adm_dept_codg = "";//入院科室编码 Y
            //requestGzsiModel2401.adm_dept_name = "";//入院科室名称 Y
            //requestGzsiModel2401.adm_bed = "";//入院床位 Y
            //requestGzsiModel2401.dscg_maindiag_code = "";//住院主诊断代码 Y
            //requestGzsiModel2401.dscg_maindiag_name = "";//住院主诊断名称 Y
            //requestGzsiModel2401.main_cond_desc = "";//主要病情描述 
            //requestGzsiModel2401.clnc_flag = "";//临床试验标志 
            //requestGzsiModel2401.er_flag = "";//急诊标志 
            //requestGzsiModel2401.dise_codg = "";//病种编码 
            //requestGzsiModel2401.dise_name = "";//病种名称 
            //requestGzsiModel2401.oprn_oprt_code = "";//手术操作代码 
            //requestGzsiModel2401.oprn_oprt_name = "";//手术操作名称 
            //requestGzsiModel2401.fpsc_no = "";//计划生育服务证号 
            //requestGzsiModel2401.matn_type = "";//生育类别 
            //requestGzsiModel2401.birctrl_type = "";//计划生育手术类别 
            //requestGzsiModel2401.latechb_flag = "";//晚育标志 
            //requestGzsiModel2401.geso_val = "";//孕周数 
            //requestGzsiModel2401.fetts = "";//胎次 
            //requestGzsiModel2401.fetus_cnt = "";//胎儿数 
            //requestGzsiModel2401.pret_flag = "";//早产标志 
            //requestGzsiModel2401.birctrl_matn_date = "";//计划生育手术或生育日期 
            //requestGzsiModel2401.advpay = "";//预付款 
            #endregion

            #endregion

            #region 代办人信息agnterinfo
            //Models.Agnterinfo agnterinfo = new Models.Agnterinfo();
            //agnterinfo.agnter_name = "阿黄";
            //agnterinfo.agnter_rlts = "1";
            //agnterinfo.agnter_cert_type = "01";
            //agnterinfo.agnter_certno = "440125195909091731";
            //agnterinfo.agnter_tel = "111";
            //requestGzsiModel2401.agnterinfo = agnterinfo;
            #region 代办人信息agnterinfo
            //requestGzsiModel2401.agnter_name = "";//代办人姓名 Y
            //requestGzsiModel2401.agnter_rlts = "";//代办人关系 Y
            //requestGzsiModel2401.agnter_cert_type = "";//代办人证件类型 Y
            //requestGzsiModel2401.agnter_certno = "";//代办人证件号码 Y
            //requestGzsiModel2401.agnter_tel = "";//代办人联系电话 Y
            //requestGzsiModel2401.agnter_addr = "";//代办人联系地址 
            //requestGzsiModel2401.agnter_photo = "";//代办人照片 Base64文件流 
            #endregion
            #endregion

            #region 诊断信息diseinfo
            Models.Request.RequestGzsiModel2401.Diseinfo diseinfo2401 = new API.GZSI.Models.Request.RequestGzsiModel2401.Diseinfo();
            diseinfo2401.adm_cond = "入院病情描述";
            diseinfo2401.diag_code = "OYBDM06.291";
            diseinfo2401.diag_dept = "19";
            diseinfo2401.diag_name = "肺炎";
            diseinfo2401.diag_srt_no = "0";
            diseinfo2401.diag_time = "2020-10-14 14:40:54";
            diseinfo2401.diag_type = "03";
            diseinfo2401.dise_dor_name = "信息科";
            diseinfo2401.dise_dor_no = "009999";
            diseinfo2401.maindiag_flag = "1";
            diseinfo2401.psn_no = "1000753288";
            requestGzsiModel2401.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel2401.Diseinfo>();
            requestGzsiModel2401.diseinfo.Add(diseinfo2401);
            #endregion

            #endregion

            returnvalue = inPatient2401.CallService(requestGzsiModel2401, ref responseGzsiModel2401, SerialNumber, strTransVersion, strVerifyCode);
            //Models.Response.ResponseBase rb = null;
            //if (!requestGzsiModel2401.Call(out rb, out this.errMsg))
            //{
            //    return -1;
            //}
            //Models.Response.ResponseGzsiModel2401 responseGzsiModel2401 = rb as Models.Response.ResponseGzsiModel2401;

            //patient.SIMainInfo.InDiagnose.ID = responseGzsiModel2401.mdtrt_id;//就诊ID 作为后续流程对应医保业务的唯一标识。
            //patient.SIMainInfo.Aka130 = responseGzsiModel2401.mdtrt_id;//违规类型
            //patient.SIMainInfo.RegNo = responseGzsiModel2401.mdtrt_id;//违规说明
            if (this.localMgr.InsertInPatientReg(patient) < 0)
            {
                this.ErrMsg = this.localMgr.Err;
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 取消住院登记
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            //没有医保信息时，通过住院流水号查询医保信息
            if (string.IsNullOrEmpty(patient.SIMainInfo.Psn_no) || string.IsNullOrEmpty(patient.SIMainInfo.Mdtrt_id))
            {
                patientInfo = this.localMgr.GetSIPersonInfo(patient.ID, patient);
            }
            else
            {
                patientInfo = patient;
            }

            if (patientInfo == null)
            {
                this.ErrMsg = "获取患者医保登记信息失败。";
                return -1;
            }

            InPatient2404 inPatient2404 = new InPatient2404();
            Models.Request.RequestGzsiModel2404 requestGzsiModel2404 = new API.GZSI.Models.Request.RequestGzsiModel2404();
            Models.Response.ResponseGzsiModel2404 responseGzsiModel2404 = new API.GZSI.Models.Response.ResponseGzsiModel2404();

            requestGzsiModel2404.data = new API.GZSI.Models.Request.RequestGzsiModel2404.Data();
            requestGzsiModel2404.data.psn_no = patientInfo.SIMainInfo.Psn_no;
            requestGzsiModel2404.data.mdtrt_id = patientInfo.SIMainInfo.Mdtrt_id;

            returnvalue = inPatient2404.CallService(requestGzsiModel2404, ref responseGzsiModel2404, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                this.ErrMsg = inPatient2404.ErrorMsg;
                return -1;
            }

            if (this.localMgr.CancelInPatientReg(patientInfo.ID, patientInfo.SIMainInfo.BalNo) < 0)
            {
                this.ErrMsg = this.localMgr.Err;
                return -1;
            }

            return 1;

        }

        /// <summary>
        /// 住院登记召回
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            InPatient2405 inPatient2405 = new InPatient2405();
            Models.Request.RequestGzsiModel2405 requestGzsiModel2405 = new API.GZSI.Models.Request.RequestGzsiModel2405();
            Models.Response.ResponseGzsiModel2405 responseGzsiModel2405 = new API.GZSI.Models.Response.ResponseGzsiModel2405();

            requestGzsiModel2405.data = new API.GZSI.Models.Request.RequestGzsiModel2405.Data();
            requestGzsiModel2405.data.psn_no = patient.SIMainInfo.Psn_no;
            requestGzsiModel2405.data.mdtrt_id = patient.SIMainInfo.Mdtrt_id;

            returnvalue = inPatient2405.CallService(requestGzsiModel2405, ref responseGzsiModel2405, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                this.errMsg = inPatient2405.ErrorMsg;
                return -1;
            }

            returnvalue = this.DeleteUploadedFeeDetailsAllInpatient(patient);

            if (returnvalue < 0)
            {
                this.errMsg = "删除已上传费用失败";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 住院登出
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 住院预结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            ArrayList feeListForUpload = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeDetails.Cast<FS.HISFC.Models.Fee.Inpatient.FeeItemList>().OrderBy(m => m.FeeOper.OperTime))
            {
                if (this.localMgr.GetCompareSingleItem("4", f.Item.ID, ref f.Compare) == 0) //找到了对照维护
                {
                    //f.Compare.SpellCode.UserCode = "1-7041";
                    //f.Compare.CenterItem.ID = "86900407000576";
                    f.Item.UserCode = f.Compare.SpellCode.UserCode;
                    feeListForUpload.Add(f);
                }
            }

            //若没有医保项目，则不进行预结算
            if (feeListForUpload.Count == 0)
            {
                return 1;
            }

            //预结算弹出个人信息
            frmPatientQuery frmSIPatient = new frmPatientQuery();
            frmSIPatient.Patient = patient;
            frmSIPatient.OperateType = "2";
            frmSIPatient.IsMoreInfomationRequire = true;
            if (frmSIPatient.ShowDialog() == DialogResult.OK)
            {
                patient = frmSIPatient.Patient as FS.HISFC.Models.RADT.PatientInfo;
            }
            else
            {
                this.ErrMsg = "未选择参保人信息！";
                return -1;
            }

            string errorMsg = string.Empty;
            Dictionary<string, string> PhysicalDictionary = this.localMgr.GetPhysicalSort(patient.ID, out errorMsg);

            if (PhysicalDictionary == null)
            {
                this.ErrMsg = "获取参保人物理治疗项目出错:" + errorMsg;
                return -1;
            }

            #region 费用明细上传

            InPatient2301 inPatient2301 = new InPatient2301();
            Models.Request.RequestGzsiModel2301 requestGzsiModel2301 = new API.GZSI.Models.Request.RequestGzsiModel2301();
            Models.Response.ResponseGzsiModel2301 responseGzsiModel2301 = new API.GZSI.Models.Response.ResponseGzsiModel2301();

            Dictionary<string, List<Models.Request.RequestGzsiModel2301.FeedetailRow>> forQuit = new Dictionary<string, List<Models.Request.RequestGzsiModel2301.FeedetailRow>>();
            ArrayList itemback = new ArrayList();//退费费用
            Dictionary<string, API.GZSI.Models.Response.ResponseGzsiModel2301.Result> errResultList = new Dictionary<string, API.GZSI.Models.Response.ResponseGzsiModel2301.Result>();
            decimal TotalUploadFee = 0;//费用上传明细总额 要记录下来

            int uploadPer = 100;
            int uploadCnt = feeListForUpload.Count % uploadPer == 0 ? feeListForUpload.Count / uploadPer : (feeListForUpload.Count / uploadPer) + 1;

            try
            {
                #region 正交易 执行时间

                for (int i = 0; i < uploadCnt; i++)
                {
                    List<Models.Request.RequestGzsiModel2301.FeedetailRow> feeInfo = new List<Models.Request.RequestGzsiModel2301.FeedetailRow>();
                    for (int j = 0; j < uploadPer && (i * uploadPer + j) < feeListForUpload.Count; j++)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = feeListForUpload[(i * uploadPer) + j] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                        if (feeItem == null)
                        {
                            continue;
                        }

                        if (feeItem.Item.Qty == 0 && feeItem.FT.TotCost == 0)
                        {
                            continue;
                        }

                        if (feeItem.SplitFeeFlag)
                        {
                            continue;
                        }

                        decimal unitPrice = 0.0m;
                        decimal unitCout = 0.0m;
                        if (!this.GetPrice(feeItem, ref unitPrice))
                        {
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            this.errMsg = "上传费用时获取价格失败！";
                            return -1;
                        }

                        if (!this.GetCount(feeItem, ref unitCout))
                        {
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            this.errMsg = "上传费用时获取数量失败！";
                            return -1;
                        }
                        Models.Request.RequestGzsiModel2301.FeedetailRow feeInfoRow = new Models.Request.RequestGzsiModel2301.FeedetailRow();
                        string transtype = feeItem.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "1" : "0";
                        feeInfoRow.feedetl_sn = feeItem.RecipeNO + feeItem.SequenceNO.ToString().PadLeft(3, '0') + transtype;//费用明细流水号 Y 单次就诊内唯一
                        feeInfoRow.init_feedetl_sn = "";
                        feeInfoRow.mdtrt_id = patient.SIMainInfo.Mdtrt_id;//就诊ID Y 
                        feeInfoRow.drord_no = feeItem.Order.ID;//医嘱号  
                        feeInfoRow.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y 
                        feeInfoRow.med_type = patient.SIMainInfo.Med_type;//医疗类别 Y 见【4码表说明】
                        //feeItem.ExecOrder.ExecOper.OperTime.ToString("yyyyMMddHHmmss")费用发生时间

                        if (feeItem.FeeOper.OperTime.Date.ToShortDateString() == "2021-01-13")
                        {
                            feeItem.FeeOper.OperTime = feeItem.FeeOper.OperTime.AddDays(1);
                        }

                        feeInfoRow.fee_ocur_time = feeItem.FeeOper.OperTime.ToString();//费用发生时间 Y 格式：yyyy-MM-dd HH:mm:ss

                        feeInfoRow.cnt = unitCout.ToString();//feeItem.Item.Qty.ToString();//数量 Y 退单时数量填写负数
                        feeInfoRow.pric = unitPrice.ToString();//单价 Y 
                        feeInfoRow.det_item_fee_sumamt = feeItem.SIft.TotCost.ToString();//明细项目费用总额 Y 
                        feeInfoRow.med_list_codg = feeItem.Compare.CenterItem.ID;//医疗目录编码 Y 
                        feeInfoRow.medins_list_codg = feeItem.Item.UserCode;//医药机构目录编码 Y 
                        //feeInfoRow.medins_list_name = feeItem.Item.Name;//医疗机构目录名称 Y
                        feeInfoRow.bilg_dept_codg = feeItem.RecipeOper.Dept.ID;//开单科室编码 Y 
                        feeInfoRow.bilg_dept_name = this.localMgr.getDeptName(feeItem.RecipeOper.Dept.ID);//开单科室名称 Y 
                        feeInfoRow.bilg_dr_codg = this.localMgr.GetGDDoct(feeItem.RecipeOper.ID);//开单医生编码 Y 按照标准编码填写 //"000006"; 
                        feeInfoRow.bilg_dr_name = this.localMgr.getEmplName(feeItem.RecipeOper.ID);//开单医生姓名 Y  "Colina"; 
                        feeInfoRow.acord_dept_codg = "";//受单科室编码  
                        feeInfoRow.acord_dept_name = "";//受单科室名称  
                        feeInfoRow.orders_dr_code = "";//受单医生编码  同开单医生
                        feeInfoRow.orders_dr_name = "";//受单医生姓名  
                        feeInfoRow.hosp_appr_flag = feeItem.RangeFlag;//医院审批标志  见【4码表说明】

                        //限制用药判断
                        if (this.localMgr.IfRangeItem(feeItem.Item.UserCode) == "1" && feeItem.RangeFlag == "0")
                        {
                            feeInfoRow.hosp_appr_flag = "2";//不报销
                        }

                        //判断物理项目是否报销，消费最高的两个物理项目才能报销
                        if (PhysicalDictionary.Keys.Contains(feeItem.Item.ID))
                        {
                            decimal sort = decimal.Parse(PhysicalDictionary[feeItem.Item.ID].ToString());

                            if (sort <= 2)
                            {
                                feeInfoRow.hosp_appr_flag = "1";
                            }
                            else
                            {
                                feeInfoRow.hosp_appr_flag = "2";
                            }
                        }

                        feeInfoRow.tcmdrug_used_way = "1";//中药使用方式  见【4码表说明】
                        feeInfoRow.etip_flag = "0";//外检标志  1-是 0-否
                        feeInfoRow.etip_hosp_code = "";//外检医院编码  按照标准编码填写
                        feeInfoRow.dscg_tkdrug_flag = "0";//出院带药标志  1-是 0-否
                        feeInfoRow.matn_fee_flag = "";//生育费用标志  1-是 0-否
                        //feeInfoRow.frqu_dscr = "";//用药频次描述  
                        //feeInfoRow.prd_days = "";//用药周期天数  
                        //feeInfoRow.medc_way_dscr = "";//用药途径描述  
                        //feeInfoRow.sin_dos = feeItem.Order.DoseOnce.ToString();//单次剂量  
                        //feeInfoRow.sin_dosunt = "";//单次剂量单位  
                        //feeInfoRow.used_days = "";//使用天数  
                        //feeInfoRow.dismed_amt = "";//发药总量  
                        //feeInfoRow.dismed_unt = "";//发药总量单位  
                        //feeInfoRow.unchk_flag = "1";//不进行审核标志  2-医师确认不需审核
                        //feeInfoRow.unchk_memo = "";//不进行审核说明  unchk_flag=2时候，原因必填
                        feeInfoRow.memo = "";//备注  
                        if (feeItem.Item.Qty < 0)
                        {
                            itemback.Add(feeItem);//退费项目
                            feeInfoRow = new Models.Request.RequestGzsiModel2301.FeedetailRow();
                            continue;
                        }
                        TotalUploadFee += feeItem.SIft.TotCost;//记录一下上传的费用总额
                        feeInfo.Add(feeInfoRow);

                        if (forQuit.Keys.Contains(feeInfoRow.medins_list_codg))
                        {
                            List<Models.Request.RequestGzsiModel2301.FeedetailRow> itemChargeList = forQuit[feeInfoRow.medins_list_codg];
                            itemChargeList.Add(feeInfoRow);
                        }
                        else
                        {
                            List<Models.Request.RequestGzsiModel2301.FeedetailRow> itemChargeList = new List<Models.Request.RequestGzsiModel2301.FeedetailRow>();
                            itemChargeList.Add(feeInfoRow);
                            forQuit.Add(feeInfoRow.medins_list_codg, itemChargeList);
                        }
                    }

                    if (feeInfo == null || feeInfo.Count == 0)
                    {
                        continue;
                    }

                    requestGzsiModel2301.feedetail = feeInfo;

                    returnvalue = inPatient2301.CallService(requestGzsiModel2301, ref responseGzsiModel2301, SerialNumber, strTransVersion, strVerifyCode);

                    if (returnvalue == -1)
                    {
                        this.DeleteUploadedFeeDetailsAllInpatient(patient);
                        this.errMsg = inPatient2301.ErrorMsg;
                        return -1;
                    }

                    if (responseGzsiModel2301.output.result != null && responseGzsiModel2301.output.result.Count > 0)
                    {
                        int rtn = this.localMgr.InsertInBalanceSIFeeDetail(patient, responseGzsiModel2301.output.result);
                        if (rtn < 0)
                        {
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            this.errMsg = this.localMgr.Err;
                            return -1;
                        }

                        foreach (API.GZSI.Models.Response.ResponseGzsiModel2301.Result result in responseGzsiModel2301.output.result)
                        {
                            if (!string.IsNullOrEmpty(result.memo))
                            {
                                errResultList.Add(result.feedetl_sn, result);
                            }
                        }
                    }

                    //if (responseGzsiModel2301.output.voladetail != null && responseGzsiModel2301.output.voladetail.Count > 0)
                    //{
                    //    this.ErrMsg = "存在如下费用上传失败:\n";
                    //    for (int k = 0; k < responseGzsiModel2301.output.voladetail.Count; k++)
                    //    {
                    //        this.ErrMsg = this.ErrMsg
                    //            + "费用唯一标识号："
                    //            + responseGzsiModel2301.output.voladetail[k].medins_list_codg
                    //            + " 错误描述："
                    //            + responseGzsiModel2301.output.voladetail[k].vola_dscr + "\n";
                    //    }
                    //    this.DeleteUploadedFeeDetailsAllInpatient(patient);
                    //    return -1;
                    //}
                }

                #endregion

                #region 负记录处理
                //处理负记录

                uploadCnt = itemback.Count % uploadPer == 0 ? itemback.Count / uploadPer : (itemback.Count / uploadPer) + 1;

                for (int i = 0; i < uploadCnt; i++)
                {
                    List<Models.Request.RequestGzsiModel2301.FeedetailRow> feeInfo = new List<Models.Request.RequestGzsiModel2301.FeedetailRow>();
                    for (int j = 0; j < uploadPer && (i * uploadPer + j) < itemback.Count; j++)
                    {
                        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = itemback[(i * uploadPer) + j] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;

                        if (feeItem == null)
                        {
                            continue;
                        }

                        if (feeItem.Item.Qty == 0 && feeItem.FT.TotCost == 0)
                        {
                            continue;
                        }

                        if (feeItem.SplitFeeFlag)
                        {
                            continue;
                        }

                        decimal unitPrice = 0.0m;
                        decimal unitCout = 0.0m;
                        if (!this.GetPrice(feeItem, ref unitPrice))
                        {
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            this.errMsg = "上传费用时获取价格失败！";
                            return -1;
                        }

                        if (!this.GetCount(feeItem, ref unitCout))
                        {
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            this.errMsg = "上传费用时获取数量失败！";
                            return -1;
                        }

                        string init_feedetl_sn = string.Empty;
                        Models.Request.RequestGzsiModel2301.FeedetailRow feeInfoRow = new Models.Request.RequestGzsiModel2301.FeedetailRow();
                        List<Models.Request.RequestGzsiModel2301.FeedetailRow> itemChargeList = new List<Models.Request.RequestGzsiModel2301.FeedetailRow>();
                        if (forQuit.Keys.Contains(feeItem.Item.UserCode))
                        {
                            itemChargeList = forQuit[feeItem.Item.UserCode];
                        }
                        else
                        {
                            this.ErrMsg = "上传费用出现异常：" + "退费项目【" + feeItem.Item.Name + "】不在字典中！";
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            return -1;
                        }
                        //List<Models.Request.RequestGzsiModel2301.FeedetailRow> itemChargeList = forQuit[feeItem.Item.UserCode];
                        bool isUnuploadFee = false;
                        foreach (Models.Request.RequestGzsiModel2301.FeedetailRow row in itemChargeList.OrderBy(m => FS.FrameWork.Function.NConvert.ToDecimal(m.cnt)).ThenBy(m => FS.FrameWork.Function.NConvert.ToDateTime(m.fee_ocur_time)))
                        {

                            DateTime chargeTime = FS.FrameWork.Function.NConvert.ToDateTime(row.fee_ocur_time);
                            decimal qty = FS.FrameWork.Function.NConvert.ToDecimal(row.cnt);

                            if (chargeTime != DateTime.MinValue && chargeTime <= feeItem.FeeOper.OperTime && qty + feeItem.Item.Qty >= 0)
                            {
                                init_feedetl_sn = row.feedetl_sn;
                                decimal leftQty = FS.FrameWork.Function.NConvert.ToDecimal(qty + feeItem.Item.Qty);
                                if (leftQty == 0)
                                {
                                    itemChargeList.Remove(row);
                                }
                                else
                                {
                                    row.cnt = leftQty.ToString();
                                }

                                //正记录未上传成功，因此负记录不再上传
                                if (errResultList.Keys.Contains(init_feedetl_sn))
                                {
                                    isUnuploadFee = true;
                                }

                                break;
                            }
                        }

                        if (isUnuploadFee)
                        {
                            continue;
                        }

                        if (string.IsNullOrEmpty(init_feedetl_sn))
                        {
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            this.errMsg = "退单记录找不到合适的原记录单号，请联系信息部协助处理！";
                            return -1;
                        }

                        string transtype = feeItem.TransType == FS.HISFC.Models.Base.TransTypes.Positive ? "1" : "0";
                        feeInfoRow.feedetl_sn = feeItem.RecipeNO + feeItem.SequenceNO.ToString().PadLeft(3, '0') + transtype;//费用明细流水号 Y 单次就诊内唯一
                        feeInfoRow.init_feedetl_sn = init_feedetl_sn;//原费用流水号  退单时传入被退单的费用明细流水号
                        feeInfoRow.mdtrt_id = patient.SIMainInfo.Mdtrt_id;//就诊ID Y 
                        feeInfoRow.drord_no = feeItem.Order.ID;//医嘱号  
                        feeInfoRow.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y 
                        feeInfoRow.med_type = patient.SIMainInfo.Med_type;//医疗类别 Y 见【4码表说明】
                        //feeItem.ExecOrder.ExecOper.OperTime.ToString("yyyyMMddHHmmss")费用发生时间
                        feeInfoRow.fee_ocur_time = feeItem.FeeOper.OperTime.ToString();//费用发生时间 Y 格式：yyyy-MM-dd HH:mm:ss
                        feeInfoRow.cnt = feeItem.Item.Qty.ToString();//数量 Y 退单时数量填写负数
                        feeInfoRow.pric = unitPrice.ToString();//单价 Y 
                        feeInfoRow.det_item_fee_sumamt = feeItem.SIft.TotCost.ToString();//明细项目费用总额 Y 
                        feeInfoRow.med_list_codg = feeItem.Compare.CenterItem.ID;//医疗目录编码 Y 
                        feeInfoRow.medins_list_codg = feeItem.Item.UserCode;//医药机构目录编码 Y 
                        //feeInfoRow.medins_list_name = feeItem.Item.Name;//医疗机构目录名称 Y
                        feeInfoRow.bilg_dept_codg = feeItem.RecipeOper.Dept.ID;//开单科室编码 Y 
                        feeInfoRow.bilg_dept_name = this.localMgr.getDeptName(feeItem.RecipeOper.Dept.ID);//开单科室名称 Y 
                        feeInfoRow.bilg_dr_codg = this.localMgr.GetGDDoct(feeItem.RecipeOper.ID);//开单医生编码 Y 按照标准编码填写 //"000006"; 
                        feeInfoRow.bilg_dr_name = this.localMgr.getEmplName(feeItem.RecipeOper.ID);//开单医生姓名 Y  "Colina"; 
                        feeInfoRow.acord_dept_codg = "";//受单科室编码  
                        feeInfoRow.acord_dept_name = "";//受单科室名称  
                        feeInfoRow.orders_dr_code = "";//受单医生编码  同开单医生
                        feeInfoRow.orders_dr_name = "";//受单医生姓名  
                        feeInfoRow.hosp_appr_flag = feeItem.RangeFlag;//医院审批标志  见【4码表说明】

                        //限制用药判断
                        if (this.localMgr.IfRangeItem(feeItem.Item.UserCode) == "1" && feeItem.RangeFlag == "0")
                        {
                            feeInfoRow.hosp_appr_flag = "2";//不报销
                        }

                        //判断物理项目是否报销，消费最高的两个物理项目才能报销
                        if (PhysicalDictionary.Keys.Contains(feeItem.Item.ID))
                        {
                            decimal sort = decimal.Parse(PhysicalDictionary[feeItem.Item.ID].ToString());

                            if (sort <= 2)
                            {
                                feeInfoRow.hosp_appr_flag = "1";
                            }
                            else
                            {
                                feeInfoRow.hosp_appr_flag = "2";
                            }
                        }

                        feeInfoRow.tcmdrug_used_way = "1";//中药使用方式  见【4码表说明】
                        feeInfoRow.etip_flag = "0";//外检标志  1-是 0-否
                        feeInfoRow.etip_hosp_code = "";//外检医院编码  按照标准编码填写
                        feeInfoRow.dscg_tkdrug_flag = "0";//出院带药标志  1-是 0-否
                        feeInfoRow.matn_fee_flag = "";//生育费用标志  1-是 0-否
                        //feeInfoRow.frqu_dscr = "";//用药频次描述  
                        //feeInfoRow.prd_days = "";//用药周期天数  
                        //feeInfoRow.medc_way_dscr = "";//用药途径描述  
                        //feeInfoRow.sin_dos = feeItem.Order.DoseOnce.ToString();//单次剂量  
                        //feeInfoRow.sin_dosunt = "";//单次剂量单位  
                        //feeInfoRow.used_days = "";//使用天数  
                        //feeInfoRow.dismed_amt = "";//发药总量  
                        //feeInfoRow.dismed_unt = "";//发药总量单位  
                        //feeInfoRow.unchk_flag = "1";//不进行审核标志  2-医师确认不需审核
                        //feeInfoRow.unchk_memo = "";//不进行审核说明  unchk_flag=2时候，原因必填
                        feeInfoRow.memo = "";//备注
                        TotalUploadFee += feeItem.SIft.TotCost;//记录一下上传的费用总额
                        feeInfo.Add(feeInfoRow);
                    }

                    if (feeInfo == null || feeInfo.Count == 0)
                    {
                        continue;
                    }

                    requestGzsiModel2301.feedetail = feeInfo;

                    returnvalue = inPatient2301.CallService(requestGzsiModel2301, ref responseGzsiModel2301, SerialNumber, strTransVersion, strVerifyCode);

                    if (returnvalue == -1)
                    {
                        this.DeleteUploadedFeeDetailsAllInpatient(patient);
                        this.errMsg = inPatient2301.ErrorMsg;
                        return -1;
                    }

                    if (responseGzsiModel2301.output.result != null && responseGzsiModel2301.output.result.Count > 0)
                    {
                        int rtn = this.localMgr.InsertInBalanceSIFeeDetail(patient, responseGzsiModel2301.output.result);
                        if (rtn < 0)
                        {
                            this.DeleteUploadedFeeDetailsAllInpatient(patient);
                            this.errMsg = this.localMgr.Err;
                            return -1;
                        }
                    }

                    //if (responseGzsiModel2301.output.voladetail != null && responseGzsiModel2301.output.voladetail.Count > 0)
                    //{
                    //    this.ErrMsg = "存在如下费用上传失败:\n";
                    //    for (int k = 0; k < responseGzsiModel2301.output.voladetail.Count; k++)
                    //    {
                    //        this.ErrMsg = this.ErrMsg
                    //            + "费用唯一标识号："
                    //            + responseGzsiModel2301.output.voladetail[k].medins_list_codg
                    //            + " 错误描述："
                    //            + responseGzsiModel2301.output.voladetail[k].vola_dscr + "\n";
                    //    }
                    //    this.DeleteUploadedFeeDetailsAllInpatient(patient);
                    //    return -1;
                    //}
                }

                #endregion
            }
            catch (Exception ex)
            {
                this.ErrMsg = "上传费用出现异常：" + ex.Message;
                this.DeleteUploadedFeeDetailsAllInpatient(patient);
                return -1;
            }

            patient.ExtendFlag = TotalUploadFee.ToString("0.00");

            #endregion


            #region 获取最新诊断

            //patient.Diagnoses
            // {34204F38-134A-417A-ABBC-EBE2C0ED5A61}
            ArrayList Diagnoses = this.localMgr.InpatientBaseDiagnoseList(patient.ID);
            if (Diagnoses == null || Diagnoses.Count <= 0) {
                Diagnoses = this.localMgr.InpatientEMRDiagnoseList(patient.ID, "");
            }
            if (Diagnoses != null && Diagnoses.Count > 0)
            {
                patient.Diagnoses = Diagnoses;
            }
            #endregion

            #region 入院信息变更

            InPatient2403 inPatient2403 = new InPatient2403();
            Models.Request.RequestGzsiModel2403 requestGzsiModel2403 = new API.GZSI.Models.Request.RequestGzsiModel2403();
            Models.Response.ResponseGzsiModel2403 responseGzsiModel2403 = new API.GZSI.Models.Response.ResponseGzsiModel2403();

            try
            {
                #region 入院登记信息adminfo
                requestGzsiModel2403.adminfo = new API.GZSI.Models.Request.RequestGzsiModel2403.Mdtrtinfo();
                requestGzsiModel2403.adminfo.mdtrt_id = patient.SIMainInfo.Mdtrt_id;//就诊ID Y 
                requestGzsiModel2403.adminfo.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y 
                //requestGzsiModel2403.adminfo.insutype = patient.SIMainInfo.Insutype;//险种类型  见【4码表说明】
                requestGzsiModel2403.adminfo.begntime = patient.PVisit.InTime.ToString();//开始时间  
                requestGzsiModel2403.adminfo.endtime = "";//结束时间  
                requestGzsiModel2403.adminfo.coner_name = "";//联系人姓名  
                requestGzsiModel2403.adminfo.tel = "";//联系电话  
                requestGzsiModel2403.adminfo.mdtrt_cert_type = patient.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y 见【4码表说明】
                //requestGzsiModel2403.adminfo.mdtrt_cert_no = patient.SIMainInfo.Certno;//就诊凭证编号  就诊凭证类型为“02”时填写身份证号，为“03”时填写社会保障卡卡号
                requestGzsiModel2403.adminfo.med_type = patient.SIMainInfo.Med_type;//医疗类别 Y 见【4码表说明】
                requestGzsiModel2403.adminfo.ipt_otp_no = patient.ID + patient.SIMainInfo.BalNo;//住院/门诊号 Y 
                requestGzsiModel2403.adminfo.medrcdno = patient.PID.PatientNO;//病历号  
                requestGzsiModel2403.adminfo.atddr_no = this.localMgr.GetGDDoct(patient.PVisit.AdmittingDoctor.ID); //主治医生编码 Y 
                requestGzsiModel2403.adminfo.chfpdr_name = patient.PVisit.AdmittingDoctor.ID; //主治医师姓名 Y 
                requestGzsiModel2403.adminfo.adm_diag_dscr = patient.ClinicDiagnose;//入院诊断描述 Y 
                requestGzsiModel2403.adminfo.adm_dept_codg = patient.PVisit.PatientLocation.Dept.ID;//入院科室编码 Y 
                requestGzsiModel2403.adminfo.adm_dept_name = patient.PVisit.PatientLocation.Dept.Name;//入院科室名称 Y 
                requestGzsiModel2403.adminfo.adm_bed = patient.PVisit.PatientLocation.Bed.Name;//入院床位 Y 
                requestGzsiModel2403.adminfo.dscg_maindiag_code = this.localMgr.GetICDYB2((patient.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose).ID);// patient.ExtendFlag2;//住院主诊断代码 Y 
                requestGzsiModel2403.adminfo.dscg_maindiag_name = (patient.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose).Name;// patient.ClinicDiagnose;//住院主诊断名称 Y 
                requestGzsiModel2403.adminfo.main_cond_dscr = "";//主要病情描述  
                //requestGzsiModel2403.adminfo.clnc_flag = "0";//临床试验标志  1-是 0-否
                requestGzsiModel2403.adminfo.dise_codg = patient.SIMainInfo.Dise_code;//病种编码  按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_code)
                requestGzsiModel2403.adminfo.dise_name = patient.SIMainInfo.Dise_name;//病种名称  
                requestGzsiModel2403.adminfo.oprn_oprt_code = "";//手术操作代码  
                requestGzsiModel2403.adminfo.oprn_oprt_name = "";//手术操作名称  
                requestGzsiModel2403.adminfo.fpsc_no = frmSIPatient.Fpsc_no;//计划生育服务证号            //生育  
                requestGzsiModel2403.adminfo.matn_type = frmSIPatient.Matn_type;//生育类别                //生育 Y
                requestGzsiModel2403.adminfo.birctrl_type = frmSIPatient.Birctrl_type;//计划生育手术类别  //生育 Y
                requestGzsiModel2403.adminfo.latechb_flag = frmSIPatient.Latechb_flag;//晚育标志          //生育 Y
                requestGzsiModel2403.adminfo.geso_val = frmSIPatient.Geso_val;//孕周数                    //生育
                requestGzsiModel2403.adminfo.fetts = frmSIPatient.Fetts;//胎次                            //生育
                requestGzsiModel2403.adminfo.fetus_cnt = frmSIPatient.Fetus_cnt;//胎儿数                  //生育 Y
                requestGzsiModel2403.adminfo.pret_flag = frmSIPatient.Pret_flag;//早产标志                //生育 Y
                requestGzsiModel2403.adminfo.birctrl_matn_date = patient.SIMainInfo.Birctrl_matn_date;//计划生育手术或生育日期   //生育 Y
                requestGzsiModel2403.adminfo.dise_type_code = "";//病种编码

                #endregion

                #region 代办人信息agnterinfo

                //requestGzsiModel2403.agnterinfo = new API.GZSI.Models.Request.RequestGzsiModel2403.Agnterinfo();
                //requestGzsiModel2403.agnterinfo.agnter_name = patient.Name;//代办人姓名 Y
                //requestGzsiModel2403.agnterinfo.agnter_rlts = "1";//代办人关系 Y
                //requestGzsiModel2403.agnterinfo.agnter_cert_type = "01";//代办人证件类型 Y
                //requestGzsiModel2403.agnterinfo.agnter_certno = patient.IDCard;//代办人证件号码 Y
                //requestGzsiModel2403.agnterinfo.agnter_tel = string.IsNullOrEmpty(patient.PhoneHome) ? "-" : patient.PhoneHome; //代办人联系电话 Y
                //requestGzsiModel2403.agnterinfo.agnter_addr = "";//代办人联系地址 
                //requestGzsiModel2403.agnterinfo.agnter_photo = "";//代办人照片 Base64文件流

                #endregion

                #region 诊断信息diseinfo

                requestGzsiModel2403.diseinfo = new List<Models.Inpatient.Diseinfo>();
                //要取入院诊断,不能取病案诊断，需要取EMR的诊断
                ArrayList DiagnosesRY = this.localMgr.InpatientEMRDiagnoseList(patient.ID, "1");
                if (DiagnosesRY == null || DiagnosesRY.Count <= 0)
                {
                    DiagnosesRY = patient.Diagnoses;
                }

                if (DiagnosesRY != null && DiagnosesRY.Count > 0)
                {
                    for (int i = 0; i < DiagnosesRY.Count; i++)
                    {
                        // 转换诊断信息实体
                        FS.HISFC.Models.RADT.Diagnose diag = DiagnosesRY[i] as FS.HISFC.Models.RADT.Diagnose;
                        if (diag == null)
                            continue;
                        //if (diag.Type.ID != "1")
                        //    continue;

                        Models.Inpatient.Diseinfo row = new Models.Inpatient.Diseinfo();
                        row.mdtrt_id = patient.SIMainInfo.Mdtrt_id; //就诊ID
                        row.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y
                        row.diag_type = "1"; //诊断类别 1-西医诊断 Y
                        row.maindiag_flag = diag.IsMain ? "1" : "0";//主诊断标志 Y
                        row.diag_srt_no = diag.HappenNO.ToString();//诊断排序号 Y
                        row.diag_code = this.localMgr.GetICDYB2(diag.ID);//诊断代码 Y
                        row.diag_name = diag.Name;//诊断名称 Y
                        row.adm_cond = "";//入院病情 
                        row.diag_dept = patient.PVisit.PatientLocation.Dept.ID;//诊断科室 Y
                        row.dise_dor_no = this.localMgr.GetGDDoct(patient.PVisit.AdmittingDoctor.ID);//诊断医生编码 Y
                        row.dise_dor_name = patient.PVisit.AdmittingDoctor.Name;//诊断医生姓名 Y
                        row.diag_time = diag.DiagTime.ToString();//诊断时间 Y
                        row.vali_flag = "1";//有效标志 Y
                        requestGzsiModel2403.diseinfo.Add(row);
                    }
                }

                #endregion

                returnvalue = inPatient2403.CallService(requestGzsiModel2403, ref responseGzsiModel2403, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    this.DeleteUploadedFeeDetailsAllInpatient(patient);
                    this.errMsg = inPatient2403.ErrorMsg;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.ErrMsg = "入院信息变更异常：" + ex.Message;
                this.DeleteUploadedFeeDetailsAllInpatient(patient);
                return -1;
            }

            #endregion

            #region  调用出院办理接口(2402)

            InPatient2402 inPatient2402 = new InPatient2402();
            Models.Request.RequestGzsiModel2402 requestGzsiModel2402 = new API.GZSI.Models.Request.RequestGzsiModel2402();
            Models.Response.ResponseGzsiModel2402 responseGzsiModel2402 = new API.GZSI.Models.Response.ResponseGzsiModel2402();
            requestGzsiModel2402.dscginfo = new API.GZSI.Models.Request.RequestGzsiModel2402.Mdtrtinfo();

            try
            {
                #region 就诊信息mdtrtinfo

                requestGzsiModel2402.dscginfo.mdtrt_id = patient.SIMainInfo.Mdtrt_id;//就诊ID Y
                requestGzsiModel2402.dscginfo.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y
                requestGzsiModel2402.dscginfo.insutype = patient.SIMainInfo.Insutype;//险种类型 Y
                requestGzsiModel2402.dscginfo.endtime = patient.PVisit.OutTime.ToString();//结束时间 Y
                requestGzsiModel2402.dscginfo.dise_codg = patient.SIMainInfo.Dise_code;//病种编号 
                requestGzsiModel2402.dscginfo.dise_name = patient.SIMainInfo.Dise_name;//病种名称 
                requestGzsiModel2402.dscginfo.oprn_oprt_code = "";//手术操作代码 
                requestGzsiModel2402.dscginfo.oprn_oprt_name = "";//手术操作名称 
                requestGzsiModel2402.dscginfo.fpsc_no = frmSIPatient.Fpsc_no;//计划生育服务证号            //生育 
                requestGzsiModel2402.dscginfo.matn_type = frmSIPatient.Matn_type;//生育类别                //生育 Y 
                requestGzsiModel2402.dscginfo.birctrl_type = frmSIPatient.Birctrl_type;//计划生育手术类别  //生育 Y 
                requestGzsiModel2402.dscginfo.latechb_flag = frmSIPatient.Latechb_flag;//晚育标志          //生育 Y
                requestGzsiModel2402.dscginfo.geso_val = frmSIPatient.Geso_val;//孕周数                    //生育 
                requestGzsiModel2402.dscginfo.fetts = frmSIPatient.Fetts;//胎次                            //生育 
                requestGzsiModel2402.dscginfo.fetus_cnt = frmSIPatient.Fetus_cnt;//胎儿数                  //生育 Y
                requestGzsiModel2402.dscginfo.pret_flag = frmSIPatient.Pret_flag;//早产标志                //生育 Y 
                requestGzsiModel2402.dscginfo.birctrl_matn_date = patient.SIMainInfo.Birctrl_matn_date;//计划生育手术或生育日期   //生育 Y 
                requestGzsiModel2402.dscginfo.cop_flag = "";//伴有并发症标志 
                requestGzsiModel2402.dscginfo.dscg_dept_code = patient.PVisit.PatientLocation.Dept.ID;//出院科室编码 Y
                requestGzsiModel2402.dscginfo.dscg_dept_name = patient.PVisit.PatientLocation.Dept.Name;//出院科室名称 Y
                requestGzsiModel2402.dscginfo.dscg_bed = patient.PVisit.PatientLocation.Bed.Name;//出院床位 
                requestGzsiModel2402.dscginfo.dscg_way = "1";//离院方式 Y
                requestGzsiModel2402.dscginfo.die_date = "";//死亡日期 

                #endregion

                #region 诊断信息diseinfo

                requestGzsiModel2402.diseinfo = new List<Models.Inpatient.Diseinfo>();
                if (patient.Diagnoses != null && patient.Diagnoses.Count > 0)
                {
                    for (int i = 0; i < patient.Diagnoses.Count; i++)
                    {
                        // 转换诊断信息实体
                        FS.HISFC.Models.RADT.Diagnose diag = patient.Diagnoses[i] as FS.HISFC.Models.RADT.Diagnose;
                        if (diag == null)
                            continue;
                        if (diag.Type.ID != "2")
                            continue;

                        Models.Inpatient.Diseinfo row = new Models.Inpatient.Diseinfo();
                        row.mdtrt_id = patient.SIMainInfo.Mdtrt_id; //就诊ID
                        row.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y
                        row.diag_type = "1"; //诊断类别 1-西医诊断 Y
                        row.maindiag_flag = diag.IsMain ? "1" : "0";//主诊断标志 Y
                        row.diag_srt_no = diag.HappenNO.ToString();//诊断排序号 Y
                        row.diag_code = this.localMgr.GetICDYB2(diag.ID);//诊断代码 Y
                        row.diag_name = diag.Name;//诊断名称 Y
                        row.adm_cond = "";//入院病情 
                        row.diag_dept = patient.PVisit.PatientLocation.Dept.ID;//诊断科室 Y
                        row.dise_dor_no = this.localMgr.GetGDDoct(patient.PVisit.AdmittingDoctor.ID);//诊断医生编码 Y
                        row.dise_dor_name = patient.PVisit.AdmittingDoctor.Name;//诊断医生姓名 Y
                        row.diag_time = patient.PVisit.OutTime.ToString();//diag.DiagTime.ToString();//诊断时间 Y
                        row.vali_flag = "1";//有效标志 Y
                        requestGzsiModel2402.diseinfo.Add(row);
                    }
                }
                returnvalue = inPatient2402.CallService(requestGzsiModel2402, ref responseGzsiModel2402, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    this.DeleteUploadedFeeDetailsAllInpatient(patient);
                    this.errMsg = inPatient2402.ErrorMsg;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.ErrMsg = "出院办理出现异常：" + ex.Message;
                this.DeleteUploadedFeeDetailsAllInpatient(patient);
                return -1;
            }

                #endregion

            #endregion

            #region 预结算

            InPatient2303 inPatient2303 = new InPatient2303();
            Models.Request.RequestGzsiModel2303 requestGzsiModel2303 = new API.GZSI.Models.Request.RequestGzsiModel2303();
            Models.Response.ResponseGzsiModel2303 responseGzsiModel2303 = new API.GZSI.Models.Response.ResponseGzsiModel2303();
            Models.Request.RequestGzsiModel2303.Data requestGzsiModel2303Data = new API.GZSI.Models.Request.RequestGzsiModel2303.Data();

            try
            {
                requestGzsiModel2303Data.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y
                requestGzsiModel2303Data.mdtrt_cert_type = patient.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y
                requestGzsiModel2303Data.mdtrt_cert_no = patient.SIMainInfo.Certno;//就诊凭证编号 
                requestGzsiModel2303Data.medfee_sumamt = TotalUploadFee.ToString();//医疗费总额 Y
                requestGzsiModel2303Data.mdtrt_id = patient.SIMainInfo.Mdtrt_id.ToString();//就诊ID Y
                requestGzsiModel2303Data.insutype = patient.SIMainInfo.Insutype;//险种类型 
                requestGzsiModel2303Data.invono = ""; //发票号
                //requestGzsiModel2303Data.med_type = patient.SIMainInfo.Med_type;//医疗类别 Y
                requestGzsiModel2303Data.acct_used_flag = "1"; //个人账户使用标志 Y 
                requestGzsiModel2303Data.psn_setlway = patient.SIMainInfo.Psn_setlway;//个人结算方式  Y 
                requestGzsiModel2303Data.mid_setl_flag = "0";//中途结算标志  Y 
                requestGzsiModel2303Data.fulamt_ownpay_amt = ""; //全自费金额
                requestGzsiModel2303Data.overlmt_selfpay = ""; //超限价金额
                requestGzsiModel2303Data.preselfpay_amt = ""; //先行自付金额
                requestGzsiModel2303Data.inscp_scp_amt = ""; //符合政策范围金额
                //requestGzsiModel2303Data.order_no = patient.SIMainInfo.InvoiceNo;//医疗机构订单号或医疗机构就医序列号 
                //requestGzsiModel2303Data.mdtrt_mode = "0";//就诊方式 Y
                //requestGzsiModel2303Data.psn_setlway = "01"; //个人结算方式
                //requestGzsiModel2303Data.hcard_basinfo = patient.SIMainInfo.Hcard_basinfo;//持卡就诊基本信息 
                //requestGzsiModel2303Data.hcard_chkinfo = patient.SIMainInfo.Hcard_chkinfo;//持卡就诊校验信息 
                requestGzsiModel2303.data = requestGzsiModel2303Data;

                returnvalue = inPatient2303.CallService(requestGzsiModel2303, ref responseGzsiModel2303, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    this.RecallRegInfoInpatient(patient);
                    this.errMsg = inPatient2303.ErrorMsg;
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.ErrMsg = "出院办理出现异常：" + ex.Message;
                this.RecallRegInfoInpatient(patient);
                return -1;
            }

            decimal sumCost = 0m;
            decimal heightsumCost = 0m;//高收费费用
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeDetails)
            {
                sumCost += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.OwnCost.ToString("F2"));
                if (f.SplitFeeFlag)
                {
                    heightsumCost += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.OwnCost.ToString("F2"));
                }
            }

            decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.medfee_sumamt);
            totCost = FS.FrameWork.Public.String.FormatNumber(totCost, 2);
            decimal pubCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.fund_pay_sumamt);
            pubCost = FS.FrameWork.Public.String.FormatNumber(pubCost, 2);
            decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.psn_part_amt);
            ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);

            patient.SIMainInfo.TotCost = totCost;
            patient.SIMainInfo.PubCost = pubCost;
            patient.SIMainInfo.OwnCost = ownCost;

            //个人支付金额
            patient.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.psn_part_amt)
                - FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.acct_pay);
            //统筹支付金额
            patient.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.fund_pay_sumamt);
            //账户支付金额
            patient.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.acct_pay);

            //统筹信息
            patient.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.medfee_sumamt);  //医疗费总额
            patient.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.act_pay_dedc); //实际支付起付线
            patient.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.inscp_scp_amt);
            patient.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.fund_pay_sumamt);
            patient.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.psn_part_amt);
            patient.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.hosp_part_amt);
            //统筹基金明细
            patient.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.hifp_pay);
            patient.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.cvlserv_pay);
            patient.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.hifes_pay);
            patient.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.hifmi_pay);
            patient.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.hifob_pay);
            patient.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.maf_pay);
            //自负明细
            patient.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.fulamt_ownpay_amt);
            patient.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.overlmt_selfpay);
            patient.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.preselfpay_amt);
            patient.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.acct_pay);
            patient.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.acct_mulaid_pay);
            //patient.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.balc);
            patient.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2303.output.setlinfo.psn_cash_pay);
            //备注
            //patient.SIMainInfo.Memo = responseGzsiModel2303.output.setlinfo.memo;

            //if (patient.SIMainInfo.TotCost != patient.SIMainInfo.OwnCost + patient.SIMainInfo.PubCost + patient.SIMainInfo.PayCost)
            //{
            //    this.RecallRegInfoInpatient(patient);
            //    this.errMsg = "预结算失败，总金额与个人支付金额，统筹支付金额，账户支付金额总额不平！";
            //    return -1;
            //}

            frmBalanceShow balanceShow = new frmBalanceShow();
            balanceShow.PatientInfo = patient;
            balanceShow.DialogTitle = "住院医保结算";

            if (balanceShow.ShowDialog() != DialogResult.OK)
            {
                this.RecallRegInfoInpatient(patient);
                this.errMsg = "操作取消！";
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 二类门特
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        private int PreBalanceMT(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            #region 注释
            //#region 费用明细上传
            //Models.Request. requestBizh120332 = new API.GZSI.Models.Request.RequestBizh120332();
            //Models.Response.ResponseBizh120332 responseBizh120332 = new API.GZSI.Models.Response.ResponseBizh120332();
            //FS.HISFC.Models.RADT.PatientInfo patientObj = this.localMgr.GetSIPersonInfo(patient.ID, patient);
            //Models.Response.ResponseBase responseBase = new API.GZSI.Models.Response.ResponseBase();

            //if (patientObj == null)
            //{
            //    this.ErrMsg = "获取患者医保登记信息失败。";
            //    return -1;
            //}
            //if (FS.FrameWork.Management.Connection.Hospital.Memo == "001")
            //{
            //    if (!string.IsNullOrEmpty(patientObj.SIMainInfo.RegNo))
            //    {
            //        FS.FrameWork.Management.Connection.Hospital.Memo = patientObj.SIMainInfo.RegNo.Substring(0, 6);
            //    }
            //    else
            //    {
            //        this.ErrMsg = "获取医院ID错误。请联系工程师！！！";
            //        return -1;
            //    }
            //}
            ////查询患者费用明细（按执行时间）
            //if (this.localMgr.QueryfeeDetailsForUseTime(patient, ref feeDetails) < 1)
            //{
            //    this.ErrMsg = this.localMgr.Err;
            //    return -1;
            //}

            //ArrayList itemback = new ArrayList();//退费费用
            //decimal TotalUploadFee = 0;//费用上传明细总额，由于省集中可能出现上传总额和本地总额不相等的bug,要记录下来

            /////bizh120002
            //requestBizh120332.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;  //医疗机构编码
            //requestBizh120332.aaz217 = patientObj.SIMainInfo.RegNo;  //就医登记号

            //int uploadPer = 100;
            //int uploadCnt = feeDetails.Count % uploadPer == 0 ? feeDetails.Count / uploadPer : (feeDetails.Count / uploadPer) + 1;


            //#region 正交易 执行时间
            //for (int i = 0; i < uploadCnt; i++)
            //{
            //    //ArrayList feeLists = new ArrayList();
            //    Models.FeeInfo feeInfo = new API.GZSI.Models.FeeInfo();
            //    feeInfo.rows = new List<API.GZSI.Models.FeeInfoRow>();
            //    for (int j = 0; j < uploadPer && (i * uploadPer + j) < feeDetails.Count; j++)
            //    {
            //        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = feeDetails[(i * uploadPer) + j] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
            //        if (feeItem == null)
            //        {
            //            continue;
            //        }
            //        if (feeItem.Item.Qty == 0 && feeItem.FT.TotCost == 0)
            //        {
            //            continue;
            //        }
            //        if (feeItem.SplitFeeFlag)
            //        {
            //            continue;
            //        }

            //        decimal unitPrice = this.GetPrice(feeItem);
            //        decimal unitCout = this.GetCount(feeItem);
            //        Models.FeeInfoRow feeInfoRow = new Models.FeeInfoRow();
            //        //FS.HISFC.Models.Base.Employee doctor = this.interMgr.GetEmployeeInfo(feeItem.RecipeOper.ID);//获取开立医生信息
            //        feeInfoRow.ake005 = string.IsNullOrEmpty(feeItem.Compare.SpellCode.UserCode) ? feeItem.Item.UserCode : feeItem.Compare.SpellCode.UserCode;       //医药机构目录编码
            //        feeInfoRow.ake006 = feeItem.Item.Name;     //医药机构目录名称
            //        feeInfoRow.ake105 = String.Empty;          //药品本位码
            //        feeInfoRow.aka070 = String.Empty;          //剂型
            //        feeInfoRow.aka074 = string.Empty;          //规格
            //        feeInfoRow.bka073 = string.Empty;          //厂家
            //        feeInfoRow.ake007 = feeItem.ExecOrder.ExecOper.OperTime.ToString("yyyyMMddHHmmss");  //费用发生日期	
            //        feeInfoRow.aka067 = string.Empty;                           //计量单位
            //        feeInfoRow.bka040 = unitPrice.ToString("F4");               //单价
            //        feeInfoRow.akc226 = feeItem.Item.Qty.ToString("F2");        //数量
            //        feeInfoRow.aae019 = feeItem.FT.TotCost.ToString("F2");      //金额
            //        feeInfoRow.aaz213 = string.Empty;                           //费用序号
            //        feeInfoRow.bka070 = string.Empty;                           //处方号
            //        feeInfoRow.bka074 = feeItem.RecipeOper.ID;                  //处方医师编号
            //        feeInfoRow.bka075 = feeItem.RecipeOper.Name;                //处方医师姓名
            //        feeInfoRow.aae011 = this.localMgr.Operator.ID;              //操作员工号
            //        feeInfoRow.bka015 = this.localMgr.Operator.Name;            //操作员姓名
            //        feeInfoRow.bka071 = feeItem.RecipeNO;                       //医院费用的唯一标识
            //        feeInfoRow.bkr024 = string.Empty;                           //用药天数
            //        //feeInfoRow.aka036 = string.Empty;                           //限制使用标志
            //        //{F7CA9356-B603-49a2-8C26-8B58E3772C99} 市直传限制用药标识 chenw 2020-05-07
            //        feeInfoRow.aka036 = feeItem.RangeFlag == "1" ? "1" : "0";//限制使用标志

            //        TotalUploadFee += feeItem.FT.TotCost;//记录一下上传的费用总额
            //        if (feeItem.Item.Qty < 0)
            //        {
            //            itemback.Add(feeItem);//退费项目
            //            feeInfoRow = new Models.FeeInfoRow();
            //            continue;
            //        }

            //        feeInfo.rows.Add(feeInfoRow);
            //    }

            //    if (feeInfo.rows == null || feeInfo.rows.Count == 0)
            //    {
            //        continue;
            //    }

            //    requestBizh120332.feeinfo = feeInfo;

            //    string msg = string.Empty;
            //    if (!requestBizh120332.Call(out responseBase, out msg))
            //    {
            //        FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //        return -1;
            //    }
            //    responseBizh120332 = responseBase as Models.Response.ResponseBizh120332;

            //    if (responseBizh120332.feeinfo.rows != null && responseBizh120332.feeinfo.rows.Count > 0)
            //    {
            //        this.ErrMsg = "存在如下费用上传失败:\n";
            //        for (int k = 0; k < responseBizh120332.feeinfo.rows.Count; k++)
            //        {
            //            this.ErrMsg = this.ErrMsg + "费用唯一标识号：" + responseBizh120332.feeinfo.rows[k].bka071 + " 错误描述：" + responseBizh120332.feeinfo.rows[k].aae013 + "\n";
            //        }
            //        //如果有上传失败的费用明细，则取消所有上传明细
            //        Models.Request.RequestBizh120004 requestBizh120004 = new API.GZSI.Models.Request.RequestBizh120004();
            //        Models.Response.ResponseBizh120004 responseBizh120004 = new API.GZSI.Models.Response.ResponseBizh120004();

            //        ///bizh120109
            //        requestBizh120004.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;  //医疗机构编码
            //        requestBizh120004.aaz217 = patientObj.SIMainInfo.RegNo;  //就医登记号
            //        requestBizh120004.aae011 = this.localMgr.Operator.ID;    //操作员工号
            //        requestBizh120004.bka015 = this.localMgr.Operator.Name;  //操作员姓名

            //        if (!requestBizh120004.Call(out responseBase, out msg))
            //        {
            //            FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //            return -1;
            //        }
            //        return -1;
            //    }
            //}
            //#endregion

            //#region 负交易

            //int backuploadCnt = itemback.Count % uploadPer == 0 ? itemback.Count / uploadPer : (itemback.Count / uploadPer) + 1;
            //for (int i = 0; i < backuploadCnt; i++)
            //{
            //    //ArrayList feeLists = new ArrayList();
            //    Models.FeeInfo feeInfo = new API.GZSI.Models.FeeInfo();
            //    feeInfo.rows = new List<API.GZSI.Models.FeeInfoRow>();

            //    for (int j = 0; j < uploadPer && (i * uploadPer + j) < itemback.Count; j++)
            //    {
            //        FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = itemback[(i * uploadPer) + j] as FS.HISFC.Models.Fee.Inpatient.FeeItemList;
            //        if (feeItem == null)
            //        {
            //            continue;
            //        }
            //        if (feeItem.SplitFeeFlag)
            //        {
            //            continue;
            //        }

            //        decimal unitPrice = this.GetPrice(feeItem);
            //        decimal unitCout = this.GetCount(feeItem);
            //        Models.FeeInfoRow feeInfoRow = new Models.FeeInfoRow();

            //        feeInfoRow.ake005 = string.IsNullOrEmpty(feeItem.Compare.SpellCode.UserCode) ? feeItem.Item.UserCode : feeItem.Compare.SpellCode.UserCode;       //医药机构目录编码
            //        feeInfoRow.ake006 = feeItem.Item.Name;     //医药机构目录名称
            //        feeInfoRow.ake105 = String.Empty;          //药品本位码
            //        feeInfoRow.aka070 = String.Empty;          //剂型
            //        feeInfoRow.aka074 = string.Empty;          //规格
            //        feeInfoRow.bka073 = string.Empty;          //厂家
            //        feeInfoRow.ake007 = feeItem.ExecOrder.ExecOper.OperTime.ToString("yyyyMMddHHmmss");  //费用发生日期	
            //        feeInfoRow.aka067 = string.Empty;                           //计量单位
            //        feeInfoRow.bka040 = unitPrice.ToString("F4");               //单价
            //        feeInfoRow.akc226 = feeItem.Item.Qty.ToString("F2");        //数量
            //        feeInfoRow.aae019 = feeItem.FT.TotCost.ToString("F2");      //金额
            //        feeInfoRow.aaz213 = string.Empty;                           //费用序号
            //        feeInfoRow.bka070 = string.Empty;                           //处方号
            //        feeInfoRow.bka074 = feeItem.RecipeOper.ID;                  //处方医师编号
            //        feeInfoRow.bka075 = feeItem.RecipeOper.Name;                //处方医师姓名
            //        feeInfoRow.aae011 = this.localMgr.Operator.ID;              //操作员工号
            //        feeInfoRow.bka015 = this.localMgr.Operator.Name;            //操作员姓名
            //        feeInfoRow.bka071 = feeItem.RecipeNO;                       //医院费用的唯一标识
            //        feeInfoRow.bkr024 = string.Empty;                           //用药天数
            //        //feeInfoRow.aka036 = "0";                                    //限制使用标志
            //        //{F7CA9356-B603-49a2-8C26-8B58E3772C99} 市直传限制用药标识 chenw 2020-05-07
            //        feeInfoRow.aka036 = feeItem.RangeFlag == "1" ? "1" : "0";//限制使用标志

            //        feeInfo.rows.Add(feeInfoRow);
            //    }

            //    requestBizh120332.feeinfo = feeInfo;

            //    //调用费用明细交易
            //    string msg = string.Empty;
            //    if (!requestBizh120332.Call(out responseBase, out msg))
            //    {
            //        FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //        return -1;
            //    }

            //    responseBizh120332 = responseBase as Models.Response.ResponseBizh120332;

            //    if (responseBizh120332.feeinfo.rows != null && responseBizh120332.feeinfo.rows.Count > 0)
            //    {
            //        this.ErrMsg = "存在如下费用上传失败:\n";
            //        for (int k = 0; k < responseBizh120332.feeinfo.rows.Count; k++)
            //        {
            //            this.ErrMsg = this.ErrMsg + "费用唯一标识号：" + responseBizh120332.feeinfo.rows[k].bka071 + " 错误描述：" + responseBizh120332.feeinfo.rows[k].aae013;
            //        }
            //        //如果有上传失败的费用明细，则取消所有上传明细
            //        Models.Request.RequestBizh120334 requestBizh120334 = new API.GZSI.Models.Request.RequestBizh120334();
            //        Models.Response.ResponseBizh120334 responseBizh120334 = new API.GZSI.Models.Response.ResponseBizh120334();

            //        ///bizh120109
            //        requestBizh120334.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;  //医疗机构编码
            //        requestBizh120334.aaz217 = patientObj.SIMainInfo.RegNo;  //就医登记号
            //        requestBizh120334.aae011 = this.localMgr.Operator.ID;    //操作员工号
            //        requestBizh120334.bka015 = this.localMgr.Operator.Name;  //操作员姓名

            //        if (!requestBizh120334.Call(out responseBase, out msg))
            //        {
            //            FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //            return -1;
            //        }
            //        return -1;
            //    }
            //}
            //#endregion
            //patient.ExtendFlag = TotalUploadFee.ToString("0.00");

            //#endregion


            //#region 预结算
            //Models.Request.RequestBizh120333 requestBizh120333 = new API.GZSI.Models.Request.RequestBizh120333();
            //Models.Response.ResponseBizh120333 responseBizh120333 = new API.GZSI.Models.Response.ResponseBizh120333();

            /////bizh120003
            //requestBizh120333.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;       //医疗机构编码
            //requestBizh120333.aaz217 = patientObj.SIMainInfo.RegNo;           //就医登记号
            //requestBizh120333.aae031 = this.localMgr.GetSysDate("yyyyMMdd");  //费用计算日期

            //string msg1 = string.Empty;

            //if (!requestBizh120333.Call(out responseBase, out msg1))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg1, 111);
            //    return -1;
            //}

            //responseBizh120333 = responseBase as Models.Response.ResponseBizh120333;

            //decimal sumCost = 0m;
            //decimal heightsumCost = 0m;//高收费费用
            //foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeDetails)
            //{
            //    sumCost += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.OwnCost.ToString("F2"));
            //    if (f.SplitFeeFlag)
            //    {
            //        heightsumCost += FS.FrameWork.Function.NConvert.ToDecimal(f.FT.OwnCost.ToString("F2"));
            //    }
            //}

            //decimal totCost = FS.FrameWork.Function.NConvert.ToDecimal(responseBizh120333.akc264);
            //totCost = FS.FrameWork.Public.String.FormatNumber(totCost, 2);
            //decimal pubCost = FS.FrameWork.Function.NConvert.ToDecimal(responseBizh120333.bka832);
            //pubCost = FS.FrameWork.Public.String.FormatNumber(pubCost, 2);
            //decimal ownCost = FS.FrameWork.Function.NConvert.ToDecimal(responseBizh120333.bka831);
            //ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);

            //if (sumCost != totCost + heightsumCost)
            //{
            //    //要在这里强制把费用不符的项目给更新完
            //    if (this.localMgr.UpdateItemlistOwnCost(patient) < 0)
            //    {
            //        this.ErrMsg = "医保返回金额：" + totCost.ToString() + "和收费系统的总费用：" + sumCost.ToString("F2") + "不符合,请联系信息科！";
            //        return -1;
            //    }
            //    if (this.localMgr.UpdateMedicinelistOwnCost(patient) < 0)
            //    {
            //        this.ErrMsg = "医保返回金额：" + totCost.ToString() + "和收费系统的总费用：" + sumCost.ToString("F2") + "不符合,请联系信息科！";
            //        return -1;
            //    }
            //    if (this.localMgr.UpdateFeeinfoOwnCost(patient) < 0)
            //    {
            //        this.ErrMsg = "医保返回金额：" + totCost.ToString() + "和收费系统的总费用：" + sumCost.ToString("F2") + "不符合,请联系信息科！";
            //        return -1;
            //    }



            //}

            //patient.SIMainInfo.TotCost = totCost;
            //patient.SIMainInfo.PubCost = pubCost;
            //patient.SIMainInfo.OwnCost = ownCost;
            //#endregion
            #endregion

            return 1;
        }

        /// <summary>
        /// 出院结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            ArrayList feeListForUpload = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList f in feeDetails)
            {
                if (this.localMgr.GetCompareSingleItem("4", f.Item.ID, ref f.Compare) == 0) //找到了对照维护
                {
                    f.Item.UserCode = f.Compare.SpellCode.UserCode;
                    feeListForUpload.Add(f);
                }
            }

            //若没有医保项目，则不进行医保结算
            if (feeListForUpload.Count == 0)
            {
                return 1;
            }

            #region 电子凭证 // {C891E5B0-BE9A-47AC-8604-EDE14067FB60}
            if (patient.SIMainInfo.Mdtrt_cert_type == "01")
            {
                //电子凭证需要重新扫码
                frmPatientQuery frmSIPatient = new frmPatientQuery();
                frmSIPatient.Patient = patient;
                frmSIPatient.OperateType = "2";
                if (frmSIPatient.ShowDialog() != DialogResult.OK)
                {
                    this.ErrMsg = "取消医保结算！";
                    return -1;
                }
            }
            #endregion

            #region 屏蔽
            //FS.HISFC.Models.RADT.PatientInfo patientInfo = this.localMgr.GetSIPersonInfo(patient.ID, patient);

            //if (patientInfo == null)
            //{
            //    this.ErrMsg = "获取患者医保登记信息失败。";
            //    return -1;
            //}

            // 门特二类
            //if (patient.SIMainInfo.Aka130 == "66")
            //{
            //    return BalanceInpatientMT(patient);
            //}

            //if (FS.FrameWork.Management.Connection.Hospital.Memo == "001")
            //{
            //    if (!string.IsNullOrEmpty(patientInfo.SIMainInfo.RegNo))
            //    {
            //        FS.FrameWork.Management.Connection.Hospital.Memo = patientInfo.SIMainInfo.RegNo.Substring(0, 6);
            //    }
            //    else
            //    {
            //        this.ErrMsg = "获取医院ID错误。请联系工程师！！！";
            //        return -1;
            //    }
            //}
            #endregion

            #region 调用住院结算接口(2304)

            InPatient2304 inPatient2304 = new InPatient2304();
            Models.Request.RequestGzsiModel2304 requestGzsiModel2304 = new API.GZSI.Models.Request.RequestGzsiModel2304();
            Models.Response.ResponseGzsiModel2304 responseGzsiModel2304 = new API.GZSI.Models.Response.ResponseGzsiModel2304();

            string value = ctlParamManage.GetControlParam<string>("YBLF01");
            decimal paymony = 0.0m;

            try
            {
                #region 就医信息mdtrtinfo

                requestGzsiModel2304.data = new API.GZSI.Models.Request.RequestGzsiModel2304.Mdtrtinfo();
                requestGzsiModel2304.data.psn_no = patient.SIMainInfo.Psn_no;//人员编号 Y
                requestGzsiModel2304.data.mdtrt_cert_type = patient.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y
                requestGzsiModel2304.data.mdtrt_cert_no = patient.SIMainInfo.Certno;//就诊凭证编号 
                requestGzsiModel2304.data.medfee_sumamt = patient.ExtendFlag;//医疗费总额 Y
                requestGzsiModel2304.data.mdtrt_id = patient.SIMainInfo.Mdtrt_id;//就诊ID Y
                requestGzsiModel2304.data.insutype = patient.SIMainInfo.Insutype;//险种类型 Y
                //requestGzsiModel2304.data.med_type = patient.SIMainInfo.Med_type;//医疗类别 Y
                requestGzsiModel2304.data.acct_used_flag = "1";//个人账户使用标志 Y 
                requestGzsiModel2304.data.psn_setlway = patient.SIMainInfo.Psn_setlway;//个人结算方式  Y 
                requestGzsiModel2304.data.mid_setl_flag = "0";//中途结算标志  Y 
                requestGzsiModel2304.data.invono = "";//发票号 

                requestGzsiModel2304.data.fulamt_ownpay_amt = ""; //全自费金额
                requestGzsiModel2304.data.overlmt_selfpay = ""; //超限价金额
                requestGzsiModel2304.data.preselfpay_amt = ""; //先行自付金额
                requestGzsiModel2304.data.inscp_scp_amt = ""; //符合政策范围金额

                //requestGzsiModel2304.data.order_no = "";//医疗机构订单号或医疗机构就医序列号 
                //requestGzsiModel2304.data.mdtrt_mode = "0";//就诊方式 Y
                //requestGzsiModel2304.data.hcard_basinfo = patient.SIMainInfo.Hcard_basinfo;//持卡就诊基本信息 
                //requestGzsiModel2304.data.hcard_chkinfo = patient.SIMainInfo.Hcard_chkinfo;//持卡就诊校验信息 

                #endregion

                returnvalue = inPatient2304.CallService(requestGzsiModel2304, ref responseGzsiModel2304, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    this.RecallRegInfoInpatient(patient);
                    this.errMsg = inPatient2304.ErrorMsg;
                    return -1;
                }
            }
            catch
            {
                this.RecallRegInfoInpatient(patient);
                this.errMsg = inPatient2304.ErrorMsg;
                return -1;
            }

            try
            {
                //设置结算信息
                patient.SIMainInfo.Mdtrt_id = responseGzsiModel2304.output.setlinfo.mdtrt_id;  //就诊ID
                patient.SIMainInfo.Setl_id = responseGzsiModel2304.output.setlinfo.setl_id; //结算ID
                patient.SIMainInfo.Psn_no = responseGzsiModel2304.output.setlinfo.psn_no; //人员编号
                patient.SIMainInfo.Psn_name = responseGzsiModel2304.output.setlinfo.psn_name; //人员姓名
                patient.SIMainInfo.Psn_cert_type = responseGzsiModel2304.output.setlinfo.psn_cert_type; //人员证件类型
                patient.SIMainInfo.Certno = responseGzsiModel2304.output.setlinfo.certno; //证件号码
                patient.SIMainInfo.Gend = responseGzsiModel2304.output.setlinfo.gend; //性别
                patient.SIMainInfo.Naty = responseGzsiModel2304.output.setlinfo.naty; //民族
                patient.SIMainInfo.Brdy = FS.FrameWork.Function.NConvert.ToDateTime(responseGzsiModel2304.output.setlinfo.brdy); //出生日期
                patient.SIMainInfo.Age = responseGzsiModel2304.output.setlinfo.age; //年龄
                patient.SIMainInfo.Insutype = responseGzsiModel2304.output.setlinfo.insutype; //险种类型
                patient.SIMainInfo.Psn_type = responseGzsiModel2304.output.setlinfo.psn_type; //人员类别
                patient.SIMainInfo.Cvlserv_flag = responseGzsiModel2304.output.setlinfo.cvlserv_flag; //公务员标志
                patient.SIMainInfo.Setl_time = FS.FrameWork.Function.NConvert.ToDateTime(responseGzsiModel2304.output.setlinfo.setl_time); //结算时间
                //patient.SIMainInfo.Psn_setlway = responseGzsiModel2304.output.setlinfo.psn_setlway; //个人结算方式 
                patient.SIMainInfo.Mdtrt_cert_type = responseGzsiModel2304.output.setlinfo.mdtrt_cert_type; //就诊凭证类型
                patient.SIMainInfo.Med_type = responseGzsiModel2304.output.setlinfo.med_type; //医疗类别
                patient.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.medfee_sumamt); //医疗费总额
                patient.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.fulamt_ownpay_amt); //全自费金额
                patient.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.overlmt_selfpay); //超限价自费费用
                patient.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.preselfpay_amt); //先行自付金额
                patient.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.inscp_scp_amt); //符合范围金额
                patient.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.med_sumfee); //医保认可费用总额
                patient.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.act_pay_dedc); //实际支付起付线
                patient.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.hifp_pay); //基本医疗统筹基金支出
                patient.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.pool_prop_selfpay); //基本医疗统筹比例自付
                patient.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.cvlserv_pay); //公务员医疗补助基金支出
                patient.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.hifes_pay); //补充医疗保险基金支出
                patient.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.hifmi_pay); //大病补充医疗保险基金支出
                patient.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.hifob_pay); //大额医疗补助基金支出
                patient.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.hifdm_pay); //伤残人员医疗保障基金支出
                patient.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.maf_pay); //医疗救助基金支出
                patient.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.oth_pay); //其他基金支出
                patient.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.fund_pay_sumamt); //基金支付总额
                patient.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.hosp_part_amt); //医院负担金额 
                patient.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.psn_part_amt); //个人负担总金额
                patient.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.acct_pay); //个人账户支出
                patient.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.psn_cash_pay); //现金支付金额
                patient.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.acct_mulaid_pay); //账户共济支付金额
                patient.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2304.output.setlinfo.balc); //个人账户支出后余额
                patient.SIMainInfo.Clr_optins = responseGzsiModel2304.output.setlinfo.clr_optins; //清算经办机构
                patient.SIMainInfo.Clr_way = responseGzsiModel2304.output.setlinfo.clr_way; //清算方式
                patient.SIMainInfo.Clr_type = responseGzsiModel2304.output.setlinfo.clr_type; //清算类别
                patient.SIMainInfo.Medins_setl_id = responseGzsiModel2304.output.setlinfo.medins_setl_id; //医药机构结算ID
                patient.SIMainInfo.InvoiceNo = ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)feeDetails[0]).Invoice.ID;

                paymony = patient.SIMainInfo.Fund_pay_sumamt;
            }
            catch (Exception ex)
            {
                CancelBalanceInpatient(patient, ref feeListForUpload);
                this.errMsg = "更新医保结算信息失败,请联系信息科!";
                return -1;
            }

            if (this.localMgr.UpdateSiMainInfoBalanceInfo(patient) < 0)
            {
                CancelBalanceInpatient(patient, ref feeListForUpload);
                this.ErrMsg = "插入结算信息(fin_ipr_siinmaininfo)失败。";
                return -1;
            }

            if (this.localMgr.UpdateInBalanceSIFeeDetail(patient) < 0)
            {
                CancelBalanceInpatient(patient, ref feeListForUpload);
                this.ErrMsg = "更新医保费用明细信息(gzsi_feedetail)失败。";
                return -1;
            }

            returnvalue = this.localMgr.InsertInBalanceFeeDetail(patient, feeListForUpload);

            if (returnvalue <= 0)
            {
                CancelBalanceInpatient(patient, ref feeListForUpload);
                this.errMsg = "插入医保结算明细失败,请联系信息科!";
                return -1;
            }

            returnvalue = this.localMgr.InsertInBalanceSetldetail(patient, responseGzsiModel2304.output.setldetail);

            if (returnvalue <= 0)
            {
                CancelBalanceInpatient(patient, ref feeListForUpload);
                this.errMsg = "插入医保结算基金分项信息出错,请联系信息科!";
                return -1;
            }

            //returnvalue = this.localMgr.InsertInBalanceVoladetail(patient, responseGzsiModel2304.output.voladetail);

            //if (returnvalue <= 0)
            //{
            //    this.CancelBalanceInpatient(patient, ref feeListForUpload);
            //    this.errMsg = "插入医保结算违规费用明细出错,请联系信息科!";
            //    return -1;
            //}

            if (value == "1" && paymony > 0)
            {
                checkAccount(patient.PID.CardNO, paymony, ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)feeDetails[0]).Invoice.ID, "住院");
            }

            #endregion

            return 1;

        }

        /// <summary>
        /// 门特二类结算
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int BalanceInpatientMT(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            #region 入院信息变更(2403)
            Models.Request.RequestGzsiModel2403 requestGzsiModel2403 = new API.GZSI.Models.Request.RequestGzsiModel2403();
            Models.Response.ResponseGzsiModel2403 responseGzsiModel2403 = new API.GZSI.Models.Response.ResponseGzsiModel2403();
            // Models.Response.ResponseBase responseBase = new API.GZSI.Models.Response.ResponseBase();
            #region 入院登记信息adminfo
            requestGzsiModel2403.adminfo = new API.GZSI.Models.Request.RequestGzsiModel2403.Mdtrtinfo();
            requestGzsiModel2403.adminfo.mdtrt_id = "";//就诊ID Y 
            requestGzsiModel2403.adminfo.psn_no = "";//人员编号 Y 
            //requestGzsiModel2403.adminfo.insutype = "";//险种类型  见【4码表说明】
            requestGzsiModel2403.adminfo.coner_name = "";//联系人姓名  
            requestGzsiModel2403.adminfo.tel = "";//联系电话  
            requestGzsiModel2403.adminfo.mdtrt_cert_type = "";//就诊凭证类型 Y 见【4码表说明】
            //requestGzsiModel2403.adminfo.mdtrt_cert_no = "";//就诊凭证编号  就诊凭证类型为“02”时填写身份证号，为“03”时填写社会保障卡卡号
            requestGzsiModel2403.adminfo.med_type = "";//医疗类别 Y 见【4码表说明】
            requestGzsiModel2403.adminfo.ipt_otp_no = "";//住院/门诊号 Y 
            requestGzsiModel2403.adminfo.medrcdno = "";//病历号  
            requestGzsiModel2403.adminfo.atddr_no = "";//主治医生编码 Y 
            requestGzsiModel2403.adminfo.chfpdr_name = "";//主治医师姓名 Y 
            requestGzsiModel2403.adminfo.adm_diag_dscr = "";//入院诊断描述 Y 
            requestGzsiModel2403.adminfo.adm_dept_codg = "";//入院科室编码 Y 
            requestGzsiModel2403.adminfo.adm_dept_name = "";//入院科室名称 Y 
            requestGzsiModel2403.adminfo.adm_bed = "";//入院床位 Y 
            requestGzsiModel2403.adminfo.dscg_maindiag_code = "";//住院主诊断代码 Y 
            requestGzsiModel2403.adminfo.dscg_maindiag_name = "";//住院主诊断名称 Y 
            requestGzsiModel2403.adminfo.main_cond_dscr = "";//主要病情描述  
            //requestGzsiModel2403.adminfo.clnc_flag = "";//临床试验标志  1-是 0-否
            requestGzsiModel2403.adminfo.dise_codg = "";//病种编码  按照标准编码填写：按病种结算病种目录代码(bydise_setl_list_code)
            requestGzsiModel2403.adminfo.dise_name = "";//病种名称  
            requestGzsiModel2403.adminfo.oprn_oprt_code = "";//手术操作代码  
            requestGzsiModel2403.adminfo.oprn_oprt_name = "";//手术操作名称  
            requestGzsiModel2403.adminfo.fpsc_no = "";//计划生育服务证号  
            requestGzsiModel2403.adminfo.matn_type = "";//生育类别  见【4码表说明】
            requestGzsiModel2403.adminfo.birctrl_type = "";//计划生育手术类别  见【4码表说明】
            requestGzsiModel2403.adminfo.latechb_flag = "";//晚育标志  1-是 0-否
            requestGzsiModel2403.adminfo.geso_val = "";//孕周数  
            requestGzsiModel2403.adminfo.fetts = "";//胎次  
            requestGzsiModel2403.adminfo.fetus_cnt = "";//胎儿数  
            requestGzsiModel2403.adminfo.pret_flag = "";//早产标志  1-是 0-否
            requestGzsiModel2403.adminfo.birctrl_matn_date = "";//计划生育手术或生育日期  格式：yyyy-MM-dd
            //requestGzsiModel2403.adminfo.advpay = "";//预付款  变更会在原来的基础上累加
            #endregion

            #region 代办人信息agnterinfo
            //requestGzsiModel2403.agnterinfo = new API.GZSI.Models.Request.RequestGzsiModel2403.Agnterinfo();
            //requestGzsiModel2403.agnterinfo.agnter_name = "";//代办人姓名 Y
            //requestGzsiModel2403.agnterinfo.agnter_rlts = "";//代办人关系 Y
            //requestGzsiModel2403.agnterinfo.agnter_cert_type = "";//代办人证件类型 Y
            //requestGzsiModel2403.agnterinfo.agnter_certno = "";//代办人证件号码 Y
            //requestGzsiModel2403.agnterinfo.agnter_tel = "";//代办人联系电话 Y
            //requestGzsiModel2403.agnterinfo.agnter_addr = "";//代办人联系地址 
            //requestGzsiModel2403.agnterinfo.agnter_photo = "";//代办人照片 Base64文件流 
            #endregion

            #region 诊断信息diseinfo
            requestGzsiModel2403.diseinfo = new List<Models.Inpatient.Diseinfo>();
            if (patient.Diagnoses != null && patient.Diagnoses.Count > 0)
            {
                for (int i = 0; i < patient.Diagnoses.Count; i++)
                {
                    // 转换诊断信息实体
                    FS.HISFC.Models.RADT.Diagnose diag = patient.Diagnoses[i] as FS.HISFC.Models.RADT.Diagnose;
                    if (diag == null)
                        continue;
                    Models.Inpatient.Diseinfo row = new Models.Inpatient.Diseinfo();
                    row.psn_no = "";//人员编号 Y
                    row.diag_type = "";//诊断类别 Y
                    row.maindiag_flag = "";//主诊断标志 Y
                    row.diag_srt_no = "";//诊断排序号 Y
                    row.diag_code = "";//诊断代码 Y
                    row.diag_name = "";//诊断名称 Y
                    row.adm_cond = "";//入院病情 
                    row.diag_dept = "";//诊断科室 Y
                    row.dise_dor_no = "";//诊断医生编码 Y
                    row.dise_dor_name = "";//诊断医生姓名 Y
                    row.diag_time = "";//诊断时间 Y
                    row.vali_flag = "";//有效标志 Y
                    //row.aka120 = diag.ICD10.ID;
                    //row.aka210 = diag.ICD10.ID;
                    //row.bke558 = (i + 1).ToString();
                    //row.bke559 = "1"; // 入院诊断

                    requestGzsiModel2403.diseinfo.Add(row);
                }

                //11.		akc193	疾病诊断	字符	20		Y	疾病ICD编码，必须是医保业务系统中的疾病编码
                //reqBizh120103.akc193 = reqBizh120103.diagnoseinfo.rows.Count > 0 ?
                //    reqBizh120103.diagnoseinfo.rows[0].aka120 : string.Empty;
            }

            #endregion
            //requestBizh120104.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;                    //医药机构编码
            //requestBizh120104.aaz217 = patientObj.SIMainInfo.RegNo;                        //就医登记号
            //requestBizh120104.bka021 = patient.PVisit.PatientLocation.NurseCell.ID;        //病区编码
            //requestBizh120104.bka022 = LocalManager.ConvertDeptRomannum(patient.PVisit.PatientLocation.NurseCell.Name);//病区名称
            //requestBizh120104.akf001 = patient.PVisit.PatientLocation.Dept.ID;                                     //就诊科室
            //requestBizh120104.bka020 = LocalManager.ConvertDeptRomannum(interMgr.GetDepartment(patient.PVisit.PatientLocation.Dept.ID).Name);//就诊科室名称
            //requestBizh120104.akc190 = patient.PID.PatientNO;                    //住院号
            //requestBizh120104.ake020 = patient.PVisit.PatientLocation.Bed.Name;  //入院床位号
            //requestBizh120104.aae011 = this.localMgr.Operator.ID;  //登记人员工号
            //requestBizh120104.bka015 = this.localMgr.Operator.Name;  //登记人姓名

            string msg = string.Empty;
            //if (!requestGzsiModel2403.Call(out responseBase, out msg))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //    return -1;
            //}
            #endregion

            #region  调用出院办理接口(2402)
            Models.Request.RequestGzsiModel2402 requestGzsiModel2402 = new API.GZSI.Models.Request.RequestGzsiModel2402();
            Models.Response.ResponseGzsiModel2402 responseGzsiModel2402 = new API.GZSI.Models.Response.ResponseGzsiModel2402();
            #region 就诊信息mdtrtinfo
            requestGzsiModel2402.dscginfo.mdtrt_id = "";//就诊ID Y
            requestGzsiModel2402.dscginfo.psn_no = "";//人员编号 Y
            requestGzsiModel2402.dscginfo.insutype = "";//险种类型 Y
            requestGzsiModel2402.dscginfo.endtime = "";//结束时间 Y
            requestGzsiModel2402.dscginfo.dise_codg = "";//病种编号 
            requestGzsiModel2402.dscginfo.dise_name = "";//病种名称 
            requestGzsiModel2402.dscginfo.oprn_oprt_code = "";//手术操作代码 
            requestGzsiModel2402.dscginfo.oprn_oprt_name = "";//手术操作名称 
            requestGzsiModel2402.dscginfo.fpsc_no = "";//计划生育服务证号 
            requestGzsiModel2402.dscginfo.matn_type = "";//生育类别 
            requestGzsiModel2402.dscginfo.birctrl_type = "";//计划生育手术类别 
            requestGzsiModel2402.dscginfo.latechb_flag = "";//晚育标志 
            requestGzsiModel2402.dscginfo.geso_val = "";//孕周数 
            requestGzsiModel2402.dscginfo.fetts = "";//胎次 
            requestGzsiModel2402.dscginfo.fetus_cnt = "";//胎儿数 
            requestGzsiModel2402.dscginfo.pret_flag = "";//早产标志 
            requestGzsiModel2402.dscginfo.birctrl_matn_date = "";//计划生育手术或生育日期 
            requestGzsiModel2402.dscginfo.cop_flag = "";//伴有并发症标志 
            requestGzsiModel2402.dscginfo.dscg_dept_code = "";//出院科室编码 Y
            requestGzsiModel2402.dscginfo.dscg_dept_name = "";//出院科室名称 Y
            requestGzsiModel2402.dscginfo.dscg_bed = "";//出院床位 
            requestGzsiModel2402.dscginfo.dscg_way = "";//离院方式 Y
            requestGzsiModel2402.dscginfo.die_date = "";//死亡日期 
            #endregion

            #region 诊断信息diseinfo
            requestGzsiModel2402.diseinfo = requestGzsiModel2403.diseinfo;
            #endregion
            //if (!requestGzsiModel2402.Call(out responseBase, out msg))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //    return -1;
            //}

            //Models.Request.RequestBizh120105 requestBizh120105 = new API.GZSI.Models.Request.RequestBizh120105();
            //Models.Response.ResponseBizh120105 responseBizh120105 = new API.GZSI.Models.Response.ResponseBizh120105();

            //requestBizh120105.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;       //医疗机构编码
            //requestBizh120105.aaz217 = patientObj.SIMainInfo.RegNo;           //就医登记号
            //requestBizh120105.akc196 = (patient.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose).ID;   //出院诊断
            //requestBizh120105.aae031 = patient.PVisit.OutTime.ToString("yyyyMMdd");  //出院日期	格式：“yyyyMMdd”
            //requestBizh120105.bkf002 = patient.PVisit.InSource.ID=="2"?"1":"2";   //入院方式
            //if(patient.PVisit.Circs.ID=="1"){
            //    requestBizh120105.bkf003="3";
            //}
            //else if(patient.PVisit.Circs.ID=="2"){
            //    requestBizh120105.bkf003="2";
            //}
            //else{
            //    requestBizh120105.bkf003="1";                          //入院情况
            //}

            //switch (patient.PVisit.ZG.ID)                              //出院转归情况  
            //{
            //    case "1":
            //        requestBizh120105.bka066 = "01"; break;
            //    case "2":
            //        requestBizh120105.bka066 = "02"; break;
            //    case "3":
            //        requestBizh120105.bka066 = "04"; break;
            //    case "4":
            //        requestBizh120105.bka066 = "05"; break;
            //    default:
            //        requestBizh120105.bka066 = "99"; break; 
            //}

            //requestBizh120105.ake021 = patient.DoctorReceiver.ID;     //出院诊断医师编码
            //requestBizh120105.ake120 = this.interMgr.GetEmployeeInfo(patient.DoctorReceiver.ID).Name;   // //出院诊断医师姓名
            //requestBizh120105.aae011 = this.localMgr.Operator.ID;  //登记人员工号
            //requestBizh120105.bka015 = this.localMgr.Operator.Name;  //登记人姓名
            //requestBizh120105.ake024 = String.Empty;                 //主要病情描述
            //requestBizh120105.aae013 = String.Empty;                 //备注

            //if (!requestBizh120105.Call(out responseBase, out msg))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //    return -1;
            //}
            #endregion

            #region 调用住院结算接口(2304)
            Models.Request.RequestGzsiModel2304 requestGzsiModel2304 = new API.GZSI.Models.Request.RequestGzsiModel2304();
            Models.Response.ResponseGzsiModel2304 responseGzsiModel2304 = new API.GZSI.Models.Response.ResponseGzsiModel2304();
            #region 就医信息mdtrtinfo
            requestGzsiModel2304.data = new API.GZSI.Models.Request.RequestGzsiModel2304.Mdtrtinfo();
            requestGzsiModel2304.data.psn_no = "";//人员编号 Y
            requestGzsiModel2304.data.mdtrt_cert_type = "";//就诊凭证类型 Y
            requestGzsiModel2304.data.mdtrt_cert_no = "";//就诊凭证编号 
            requestGzsiModel2304.data.medfee_sumamt = "";//医疗费总额 Y
            requestGzsiModel2304.data.mdtrt_id = "";//就诊ID Y
            requestGzsiModel2304.data.insutype = "";//险种类型 Y
            //requestGzsiModel2304.data.med_type = "";//医疗类别 Y
            requestGzsiModel2304.data.acct_used_flag = "";//个人账户使用标志 Y 
            requestGzsiModel2304.data.psn_setlway = "";//个人结算方式  Y 
            requestGzsiModel2304.data.mid_setl_flag = "";//中途结算标志  Y 
            requestGzsiModel2304.data.invono = "";//发票号 
            //requestGzsiModel2304.data.order_no = "";//医疗机构订单号或医疗机构就医序列号 
            //requestGzsiModel2304.data.mdtrt_mode = "";//就诊方式 Y
            //requestGzsiModel2304.data.hcard_basinfo = "";//持卡就诊基本信息 
            //requestGzsiModel2304.data.hcard_chkinfo = "";//持卡就诊校验信息 
            #endregion
            //if (!requestGzsiModel2304.Call(out responseBase, out msg))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //    return -1;
            //}
            //responseGzsiModel2304 = responseBase as Models.Response.ResponseGzsiModel2304;

            //Models.Request.RequestBizh120106 requestBizh120106 = new API.GZSI.Models.Request.RequestBizh120106();
            //Models.Response.ResponseBizh120106 responseBizh120106 = new API.GZSI.Models.Response.ResponseBizh120106();

            /////bizh120106
            //requestBizh120106.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId; //医疗机构编码
            //requestBizh120106.aaz217 = patientObj.SIMainInfo.RegNo;  //就医登记号
            //requestBizh120106.aae011 = this.localMgr.Operator.ID;  //完成人工号
            //requestBizh120106.bka015 = this.localMgr.Operator.Name;  //完成人

            //if (!requestBizh120106.Call(out responseBase, out msg))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //    return -1;
            //}

            //responseBizh120106 = responseBase as Models.Response.ResponseBizh120106;

            //patient.SIMainInfo.TotCost = responseBizh120106.akc264;
            ////说明:由于省医保返回OwnCost(自付累计金额)里面包含了个账支付金额(扣医保卡的钱),所以个人自费=个人自付金额-个账支付金额
            //patient.SIMainInfo.OwnCost = responseBizh120106.bka831 - responseBizh120106.akb066;
            ////说明:由于省医保返回OwnCost(自付累计金额)里面包含了个账支付金额(扣医保卡的钱),所以基金支付=统筹基金+个账支付金额
            //patient.SIMainInfo.PubCost = responseBizh120106.bka832 + responseBizh120106.akb066;
            ////patient.SIMainInfo.PayCost = outParam120106.Payinfo.Akb066;
            //patient.SIMainInfo.Bka825 = responseBizh120106.bka825;
            //patient.SIMainInfo.Bka826 = responseBizh120106.bka826;
            //patient.SIMainInfo.Aka151 = responseBizh120106.aka151;
            //patient.SIMainInfo.Bka838 = responseBizh120106.bka838;
            //patient.SIMainInfo.Akb067 = responseBizh120106.akb067;
            //patient.SIMainInfo.Akb066 = responseBizh120106.akb066;
            //patient.SIMainInfo.Bka821 = responseBizh120106.bka821;
            //patient.SIMainInfo.Bka839 = responseBizh120106.bka839;
            //patient.SIMainInfo.Ake039 = responseBizh120106.ake039;
            //patient.SIMainInfo.Ake035 = responseBizh120106.ake035;
            //patient.SIMainInfo.Ake026 = responseBizh120106.ake026;
            //patient.SIMainInfo.Ake029 = responseBizh120106.ake029;
            //patient.SIMainInfo.Bka841 = responseBizh120106.bka841;
            //patient.SIMainInfo.Bka842 = responseBizh120106.bka842;
            //patient.SIMainInfo.Bka840 = responseBizh120106.bka840;
            ////chenw 出院诊断赋值
            //patient.SIMainInfo.OutDiagnose.ID = (patient.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose).ID;
            //patient.SIMainInfo.OutDiagnose.Name = (patient.Diagnoses[0] as FS.HISFC.Models.RADT.Diagnose).Name;

            if (this.localMgr.UpdateSiMainInfoBalanceInfo(patient) < 0)
            {
                this.ErrMsg = "插入结算信息(fin_ipr_siinmaininfo_gd)失败。";
                this.Rollback();
                return -1;
            }
            #endregion

            return 1;
        }

        /// <summary>
        /// 住院中途结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 取消出院结算
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {

            FS.HISFC.Models.RADT.PatientInfo patientInfo = this.localMgr.GetSIPersonInfo(patient.ID, patient);
            string value = ctlParamManage.GetControlParam<string>("YBLF01");

            if (patientInfo == null)
            {
                this.ErrMsg = "获取患者医保登记信息失败。";
                return -1;
            }

            if (patientInfo.SIMainInfo.BalanceState == "0")
            {
                return 1;
            }

            if (patient.SIMainInfo.IsCancelSIBanlance == true)
            {
                #region 住院结算撤销（2305）

                InPatient2305 inPatient2305 = new InPatient2305();
                Models.Request.RequestGzsiModel2305 requestGzsiModel2305 = new API.GZSI.Models.Request.RequestGzsiModel2305();
                Models.Response.ResponseGzsiModel2305 responseGzsiModel2305 = new API.GZSI.Models.Response.ResponseGzsiModel2305();
                requestGzsiModel2305.data = new API.GZSI.Models.Request.RequestGzsiModel2305.Mdtrtinfo();

                #region 就诊信息mdtrtinfo

                decimal paymony = 0.0m;
                requestGzsiModel2305.data.setl_id = patient.SIMainInfo.Setl_id;//结算ID
                requestGzsiModel2305.data.mdtrt_id = patient.SIMainInfo.Mdtrt_id;//就诊ID 
                requestGzsiModel2305.data.psn_no = patient.SIMainInfo.Psn_no;//人员编号

                #endregion

                returnvalue = inPatient2305.CallService(requestGzsiModel2305, ref responseGzsiModel2305, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    this.errMsg = inPatient2305.ErrorMsg;
                    return -1;
                }

                try
                {
                    FS.HISFC.Models.RADT.PatientInfo tmp = new FS.HISFC.Models.RADT.PatientInfo();
                    tmp.Name = patient.Name;
                    tmp.IDCard = patient.IDCard;
                    //统筹信息
                    tmp.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.medfee_sumamt);  //医疗费总额
                    tmp.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.act_pay_dedc); //实际支付起付线
                    tmp.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.inscp_scp_amt);
                    tmp.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.fund_pay_sumamt);
                    tmp.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.psn_part_amt);
                    tmp.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.hosp_part_amt);
                    //统筹基金明细
                    tmp.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.hifp_pay);
                    tmp.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.cvlserv_pay);
                    tmp.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.hifes_pay);
                    tmp.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.hifmi_pay);
                    tmp.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.hifob_pay);
                    tmp.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.maf_pay);
                    //自负明细
                    tmp.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.fulamt_ownpay_amt);
                    tmp.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.overlmt_selfpay);
                    tmp.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.preselfpay_amt);
                    tmp.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.acct_pay);
                    tmp.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.acct_mulaid_pay);
                    //tmp.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.balc);
                    tmp.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2305.output.setlinfo.psn_cash_pay);
                    //备注
                    //patient.SIMainInfo.Memo = responseGzsiModel2305.output.setlinfo.memo;
                    paymony = tmp.SIMainInfo.Fund_pay_sumamt > 0 ? tmp.SIMainInfo.Fund_pay_sumamt * -1 : tmp.SIMainInfo.Fund_pay_sumamt;
                    frmBalanceShow balanceShow = new frmBalanceShow();
                    balanceShow.PatientInfo = patient;
                    balanceShow.DialogTitle = "住院结算医保退费结算";

                    if (balanceShow.ShowDialog() != DialogResult.OK)
                    {
                        //this.RecallRegInfoInpatient(patient);
                        //this.errMsg = "操作取消！";
                        //return -1;
                    }
                }
                catch
                {
                }

                #endregion

                returnvalue = this.RecallRegInfoInpatient(patient);

                if (returnvalue < 0)
                {
                    this.errMsg = "出院召回失败";
                    return -1;
                }

                if (this.localMgr.CancelInPatientBalance(patientInfo.ID, patientInfo.SIMainInfo.BalNo) < 0)
                {
                    this.ErrMsg = this.localMgr.Err;
                    return -1;
                }

                if (value == "1" && paymony < 0)
                {
                    string invoice = "";
                    if (feeDetails.Count > 0)
                    {
                        invoice = ((FS.HISFC.Models.Fee.Inpatient.FeeItemList)feeDetails[0]).Invoice.ID;
                    }
                    checkAccount(patient.PID.CardNO, paymony, invoice, "住院");
                }
            }

            return 1;
        }

        /// <summary>
        /// 查询住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询药品列表
        /// </summary>
        /// <param name="drugLists"></param>
        /// <returns></returns>
        public int QueryDrugLists(ref System.Collections.ArrayList drugLists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 查询非药品列表
        /// </summary>
        /// <param name="undrugLists"></param>
        /// <returns></returns>
        public int QueryUndrugLists(ref System.Collections.ArrayList undrugLists)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 上传住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 上传住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新住院费用列表
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref System.Collections.ArrayList feeDetails)
        {
            #region 注释
            //Models.Request.RequestBizh120004 requestBizh120004 = new API.GZSI.Models.Request.RequestBizh120004();
            //Models.Response.ResponseBizh120004 responseBizh120004 = new API.GZSI.Models.Response.ResponseBizh120004();
            //Models.Response.ResponseBase responseBase = new API.GZSI.Models.Response.ResponseBase();

            //FS.HISFC.Models.RADT.PatientInfo patientObj = this.localMgr.GetSIPersonInfo(patient.ID, patient);

            //if (patientObj == null)
            //{
            //    this.ErrMsg = "获取患者医保登记信息失败。";
            //    return -1;
            //}
            //if (FS.FrameWork.Management.Connection.Hospital.Memo == "001")
            //{
            //    if (!string.IsNullOrEmpty(patientObj.SIMainInfo.RegNo))
            //    {
            //        FS.FrameWork.Management.Connection.Hospital.Memo = patientObj.SIMainInfo.RegNo.Substring(0, 6);
            //    }
            //    else
            //    {
            //        this.ErrMsg = "获取医院ID错误。请联系工程师！！！";
            //        return -1;
            //    }
            //}
            /////bizh120109
            //requestBizh120004.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;  //医疗机构编码
            //requestBizh120004.aaz217 = patientObj.SIMainInfo.RegNo;  //就医登记号
            //requestBizh120004.aae011 = this.localMgr.Operator.ID;    //操作员工号
            //requestBizh120004.bka015 = this.localMgr.Operator.Name;  //操作员姓名

            //string msg = string.Empty;
            //if (!requestBizh120004.Call(out responseBase, out msg))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //    return -1;
            //}
            #endregion 注释
            return 1;
        }

        /// <summary>
        /// 删除住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            InPatient2302 inPatient2302 = new InPatient2302();
            Models.Request.RequestGzsiModel2302 requestGzsiModel2302 = new API.GZSI.Models.Request.RequestGzsiModel2302();
            Models.Response.ResponseGzsiModel2302 responseGzsiModel2302 = new API.GZSI.Models.Response.ResponseGzsiModel2302();

            API.GZSI.Models.Request.RequestGzsiModel2302.FeeInfo feeinfo = new API.GZSI.Models.Request.RequestGzsiModel2302.FeeInfo();
            feeinfo.feedetl_sn = "0000";//费用流水号 传入“0000”删除全部
            feeinfo.mdtrt_id = patient.SIMainInfo.Mdtrt_id;//就诊ID
            feeinfo.psn_no = patient.SIMainInfo.Psn_no;//人员编号
            requestGzsiModel2302.data = feeinfo;

            returnvalue = inPatient2302.CallService(requestGzsiModel2302, ref responseGzsiModel2302, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(inPatient2302.ErrorMsg, 111);
                return -1;
            }

            returnvalue = this.localMgr.DeleteInBalanceSIFeeDetail(patient);

            if (returnvalue == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(this.localMgr.Err, 111);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 删除住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 重新计算住院费用
        /// </summary>
        /// <param name="patient"></param>
        /// <param name="feeItemList"></param>
        /// <returns></returns>
        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {
            return 1;
        }

        #endregion

        #region 门诊


        /// <summary>
        /// 扫码验证（电子社保码）
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public string GetCodeScanningVerification(FS.HISFC.Models.Registration.Register r, string codeNum)
        {

            //Patient1101 patient1101 = new Patient1101();
            //Models.Request.RequestGzsiModel1101 requestGzsiModel1101 = new API.GZSI.Models.Request.RequestGzsiModel1101();
            //Models.Response.ResponseGzsiModel1101 responseGzsiModel1101 = new Models.Response.ResponseGzsiModel1101();
            //Models.Request.RequestGzsiModel1101.Data data = new API.GZSI.Models.Request.RequestGzsiModel1101.Data();

            //data.mdtrt_cert_type = "01";//就诊凭证类型
            //data.mdtrt_cert_no = codeNum; //凭证号码
            ////卡识别码，凭证类型为03时必填
            //data.card_sn = "";
            //data.psn_cert_type = "";// this.cmbMdtrtCertType.Tag.ToString();//就诊类型
            //data.psn_name = r.Name;// this.Patient.Name;
            //data.certno = codeNum; //证件号

            //requestGzsiModel1101.data = data;

            //returnvalue = patient1101.CallService(requestGzsiModel1101, ref responseGzsiModel1101, SerialNumber, strTransVersion, strVerifyCode);

            //Models.Response.ResponseGzsiModel1101 rb = responseGzsiModel1101 as Models.Response.ResponseGzsiModel1101;

            //if (rb.output == null)
            //{
            //    return "";
            //}
            //else
            //{
            //    return rb.output.baseinfo.certno.Trim();
            //}

            return "";

        }



        /// <summary>
        /// 是否上传所有门诊费用
        /// </summary>
        public bool IsUploadAllFeeDetailsOutpatient
        {
            get { return true; }
        }

        /// <summary>
        /// 是否在黑名单
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            return false;
        }

        /// <summary>
        /// 查询是否可以使用医保
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 门诊挂号登记
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        /// <summary>
        /// 获取门诊挂号信息
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            int ret = this.localMgr.GetRegInfoOutpatient(ref r);
            return ret;
        }

        /// <summary>
        /// 撤销挂号
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            OutPatient2202 outPatient2202 = new OutPatient2202();
            Models.Request.RequestGzsiModel2202 requestGzsiModel2202 = new API.GZSI.Models.Request.RequestGzsiModel2202();
            Models.Response.ResponseGzsiModel2202 responseGzsiModel2202 = new API.GZSI.Models.Response.ResponseGzsiModel2202();
            Models.Request.RequestGzsiModel2202.Mdtrtinfo mdtrtinfo2202 = new API.GZSI.Models.Request.RequestGzsiModel2202.Mdtrtinfo();

            mdtrtinfo2202.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊ID 202011031615
            mdtrtinfo2202.psn_no = r.SIMainInfo.Psn_no;//人员编号 1000753288
            mdtrtinfo2202.ipt_otp_no = r.ID + r.SIMainInfo.BalNo;//住院/门诊号  0000735959
            requestGzsiModel2202.data = new API.GZSI.Models.Request.RequestGzsiModel2202.Mdtrtinfo();
            requestGzsiModel2202.data = mdtrtinfo2202;

            int ret = outPatient2202.CallService(requestGzsiModel2202, ref responseGzsiModel2202, SerialNumber, strTransVersion, strVerifyCode);

            if (ret == -1)
            {
                this.errMsg = outPatient2202.ErrorMsg;
                return -1;
            }

            if (this.localMgr.CancelOutPatientRegInfo(r.SIMainInfo.Mdtrt_id) < 0)
            {
                this.ErrMsg = this.localMgr.Err;
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 门特二类
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        private int CancelRegInfoInpatientMT(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            #region 注释
            //Models.Request.RequestBizh120309 requestBizh120309 = new API.GZSI.Models.Request.RequestBizh120309();
            //Models.Response.ResponseBizh120309 responseBizh120309 = new API.GZSI.Models.Response.ResponseBizh120309();
            //Models.Response.ResponseBase responseBase = new API.GZSI.Models.Response.ResponseBase();
            //if (FS.FrameWork.Management.Connection.Hospital.Memo == "001")
            //{
            //    if (!string.IsNullOrEmpty(patient.SIMainInfo.RegNo))
            //    {
            //        FS.FrameWork.Management.Connection.Hospital.Memo = patient.SIMainInfo.RegNo.Substring(0, 6);
            //    }
            //    else
            //    {
            //        this.ErrMsg = "获取医院ID错误。请联系工程师！！！";
            //        return -1;
            //    }
            //}

            /////bizh120309
            //requestBizh120309.akb020 = FS.FrameWork.Management.Connection.Hospital.Memo != "001" ? FS.FrameWork.Management.Connection.Hospital.Memo : Models.UserInfo.Instance.userId;  //医疗机构编码
            //requestBizh120309.aaz217 = patient.SIMainInfo.RegNo;  //就医登记号
            //requestBizh120309.aae011 = this.localMgr.Operator.ID;    //操作员工号
            //requestBizh120309.bka015 = this.localMgr.Operator.Name;  //操作员姓名

            //string msg = string.Empty;
            //if (!requestBizh120309.Call(out responseBase, out msg))
            //{
            //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
            //    return -1;
            //}

            //if (this.localMgr.UpdateSiMainInfoValidFlag(patient.ID, patient.SIMainInfo.BalNo) < 0)
            //{
            //    this.ErrMsg = this.localMgr.Err;
            //    return -1;
            //}
            #endregion 注释
            return 1;
        }

        /// <summary>
        /// 门诊预结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            ArrayList feeListForUpload = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                if (this.localMgr.GetCompareSingleItem("4", f.Item.ID, ref f.Compare) == 0) //找到了对照维护
                {
                    f.Item.UserCode = f.Compare.SpellCode.UserCode;
                    feeListForUpload.Add(f);
                }
            }

            //若没有医保项目，则不进行医保挂号
            if (feeListForUpload.Count == 0)
            {
                return 1;
            }

            frmPatientQuery frmSIPatient = new frmPatientQuery();
            frmSIPatient.Patient = r;
            frmSIPatient.OperateType = "1";
            if (frmSIPatient.ShowDialog() != DialogResult.OK)
            {
                this.ErrMsg = "取消医保结算！";
                return -1;
            }

            #region 诊断信息判断

            List<Models.Request.RequestGzsiModel2203.Diseinfo> diaList = localMgr.DiagnoseList(r.ID);

            if (r.SIMainInfo.Dise_code == "T01003" || r.SIMainInfo.Dise_code == "XGJC002")
            {
                if (diaList.Count == 0)
                {
                    Models.Request.RequestGzsiModel2203.Diseinfo diseinfo = new Models.Request.RequestGzsiModel2203.Diseinfo();
                    diseinfo.diag_type = "1";//诊断类别 
                    diseinfo.diag_srt_no = "1";//诊断排序号 
                    diseinfo.diag_code = "Z11.500";//诊断代码 
                    diseinfo.diag_name = "病毒性其他疾病的特殊筛查";//诊断名称 
                    diseinfo.diag_dept = r.DoctorInfo.Templet.Dept.ID;// this.localMgr.getDeptYBCode(r.DoctorInfo.Templet.Dept.ID);//诊断科室 
                    diseinfo.dise_dor_no =this.localMgr.GetGDDoct( r.DoctorInfo.Templet.Doct.ID);//诊断医生编码 
                    diseinfo.dise_dor_name = r.DoctorInfo.Templet.Doct.Name;//诊断医生姓名 
                    diseinfo.diag_time = r.DoctorInfo.SeeDate.ToString();//诊断时间 
                    diseinfo.maindiag_flag = "1";
                    diseinfo.vali_flag = "1";//有效标志 
                    diaList.Add(diseinfo);
                }
            }
            else if (diaList.Count <= 0)
            {
                this.ErrMsg = "该患者西医诊断为空，请让医生填写西医诊断！！！";
                return -1;
            }

            #endregion

            #region  门诊挂号登记2201

            OutPatient2201 outPatient2201 = new OutPatient2201();
            Models.Request.RequestGzsiModel2201 requestGzsiModel2201 = new API.GZSI.Models.Request.RequestGzsiModel2201();
            Models.Response.ResponseGzsiModel2201 responseGzsiModel2201 = new API.GZSI.Models.Response.ResponseGzsiModel2201();
            Models.Request.RequestGzsiModel2201.Mdtrtinfo mdtrtinfo2201 = new API.GZSI.Models.Request.RequestGzsiModel2201.Mdtrtinfo();

            //获取当前挂号记录在医保主表中的下一次结算序号
            r.SIMainInfo.BalNo = this.localMgr.GetNextBalanceNo(r.ID, "1").ToString();

            mdtrtinfo2201.psn_no = r.SIMainInfo.Psn_no;//人员编号
            mdtrtinfo2201.insutype = r.SIMainInfo.Insutype;//险种类型
            mdtrtinfo2201.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型
            mdtrtinfo2201.mdtrt_cert_no = r.SIMainInfo.Certno;//就诊凭证编号
            mdtrtinfo2201.begntime = r.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm:ss");//开始时间 
            mdtrtinfo2201.ipt_otp_no = r.ID + r.SIMainInfo.BalNo; // 住院/门诊 就诊流水号 + 结算序号
            mdtrtinfo2201.dept_code = r.DoctorInfo.Templet.Dept.ID;// this.localMgr.getDeptYBCode( r.DoctorInfo.Templet.Dept.ID); //科室编码  1000
            mdtrtinfo2201.dept_name = r.DoctorInfo.Templet.Dept.Name; //科室名称  普通外科门诊
            mdtrtinfo2201.atddr_no = this.localMgr.GetGDDoct(r.DoctorInfo.Templet.Doct.ID);//医师编码
            mdtrtinfo2201.dr_name = r.DoctorInfo.Templet.Doct.Name;//医师姓名
            mdtrtinfo2201.caty = this.localMgr.getDeptYBCode(r.DoctorInfo.Templet.Dept.ID);//科别
            // {C891E5B0-BE9A-47AC-8604-EDE14067FB60}
            mdtrtinfo2201.hcard_basinfo = r.SIMainInfo.Hcard_basinfo; //持卡就诊基本信息
            mdtrtinfo2201.hcard_chkinfo = r.SIMainInfo.Hcard_chkinfo; //持卡就诊校验信息

            Models.Agnterinfo agnterinfo = new Models.Agnterinfo();
            agnterinfo.agnter_name = r.Name;//代办人姓名
            agnterinfo.agnter_rlts = "1";//代办人关系
            agnterinfo.agnter_cert_type = "01";//代办人证件类型
            agnterinfo.agnter_certno = r.IDCard;//代办人证件号码
            agnterinfo.agnter_tel = r.PhoneHome;//代办人联系电话
            agnterinfo.agnter_addr = "";//代办人联系地址
            agnterinfo.agnter_photo = "";//代办人照片
            requestGzsiModel2201.data = mdtrtinfo2201;

            returnvalue = outPatient2201.CallService(requestGzsiModel2201, ref responseGzsiModel2201, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                this.errMsg = outPatient2201.ErrorMsg;
                return -1;
            }

            //设置医保就诊登记号
            r.SIMainInfo.Mdtrt_id = responseGzsiModel2201.output.data.mdtrt_id;

            if (this.localMgr.InsertOutPatientRegInfo(r) < 0)
            {
                CancelRegInfoOutpatient(r);
                this.ErrMsg = this.localMgr.Err;
                return -1;
            }

            #endregion

            #region 门诊就诊信息上传2203
            if (r.SIMainInfo.Med_type != "5301")
            {
                OutPatient2203 outPatient2203 = new OutPatient2203();
                Models.Request.RequestGzsiModel2203 requestGzsiModel2203 = new API.GZSI.Models.Request.RequestGzsiModel2203();
                Models.Response.ResponseGzsiModel2203 responseGzsiModel2203 = new API.GZSI.Models.Response.ResponseGzsiModel2203();
                Models.Request.RequestGzsiModel2203.Mdtrtinfo mdtrtinfo2203 = new API.GZSI.Models.Request.RequestGzsiModel2203.Mdtrtinfo();

                mdtrtinfo2203.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊 ID 
                mdtrtinfo2203.psn_no = r.SIMainInfo.Psn_no;//人员编号 
                mdtrtinfo2203.med_type = r.SIMainInfo.Med_type;//医疗类别 
                mdtrtinfo2203.begntime = r.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm:ss");//开始时间 
                mdtrtinfo2203.main_cond_dscr = "";//主要病情描述 
                mdtrtinfo2203.dise_codg = r.SIMainInfo.Dise_code;//病种编码 
                mdtrtinfo2203.dise_name = r.SIMainInfo.Dise_name;//病种名称 
                mdtrtinfo2203.matn_type = frmSIPatient.Matn_type;//生育类别                //生育 Y 
                mdtrtinfo2203.birctrl_type = frmSIPatient.Birctrl_type;//计划生育手术类别 
                mdtrtinfo2203.birctrl_matn_date = frmSIPatient.Birctrl_matn_date;//计划生育手术或生育日期
                mdtrtinfo2203.ttp_resp = "0"; //是否第三方责任申请 
                mdtrtinfo2203.expi_gestation_nub_of_m = ""; //终止妊娠月数

                requestGzsiModel2203.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2203.Mdtrtinfo();
                requestGzsiModel2203.mdtrtinfo = mdtrtinfo2203;
                requestGzsiModel2203.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel2203.Diseinfo>();
                requestGzsiModel2203.diseinfo = diaList;

                returnvalue = outPatient2203.CallService(requestGzsiModel2203, ref responseGzsiModel2203, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    CancelRegInfoOutpatient(r);
                    this.errMsg = outPatient2203.ErrorMsg;
                    return -1;
                }
            }
            else
            {
                OutPatient2203A outPatient2203A = new OutPatient2203A();
                Models.Request.RequestGzsiModel2203A requestGzsiModel2203A = new API.GZSI.Models.Request.RequestGzsiModel2203A();
                Models.Response.ResponseGzsiModel2203A responseGzsiModel2203A = new API.GZSI.Models.Response.ResponseGzsiModel2203A();
                Models.Request.RequestGzsiModel2203A.Mdtrtinfo mdtrtinfo2203A = new API.GZSI.Models.Request.RequestGzsiModel2203A.Mdtrtinfo();

                mdtrtinfo2203A.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊 ID 
                mdtrtinfo2203A.psn_no = r.SIMainInfo.Psn_no;//人员编号 
                mdtrtinfo2203A.med_type = r.SIMainInfo.Med_type;//医疗类别 
                mdtrtinfo2203A.begntime = r.DoctorInfo.Templet.Begin.ToString("yyyy-MM-dd HH:mm:ss");//开始时间 
                mdtrtinfo2203A.main_cond_dscr = "";//主要病情描述 
                mdtrtinfo2203A.dise_codg = r.SIMainInfo.Dise_code;//病种编码 
                mdtrtinfo2203A.dise_name = r.SIMainInfo.Dise_name;//病种名称 
                mdtrtinfo2203A.birctrl_type = frmSIPatient.Birctrl_type;//计划生育手术类别 
                mdtrtinfo2203A.birctrl_matn_date = frmSIPatient.Birctrl_matn_date;//计划生育手术或生育日期
                mdtrtinfo2203A.ttp_resp = "0"; //是否第三方责任申请 
                mdtrtinfo2203A.expi_gestation_nub_of_m = ""; //终止妊娠月数

                //frmSIPatient.Fpsc_no;//计划生育服务证号            //生育
                //frmSIPatient.Matn_type;//生育类别                //生育 Y 
                //frmSIPatient.Birctrl_type;//计划生育手术类别  //生育 
                //frmSIPatient.Latechb_flag;//晚育标志          //生育 Y
                //frmSIPatient.Geso_val;//孕周数                    //生育
                //frmSIPatient.Fetts;//胎次                            //生育
                //frmSIPatient.Fetus_cnt;//胎儿数                  //生育
                //frmSIPatient.Pret_flag;//早产标志                //生育 Y
                //frmSIPatient.Birctrl_type;  //计划生育手术或生育日期   //生育

                //2203特有
                mdtrtinfo2203A.matn_type = frmSIPatient.Matn_type;
                mdtrtinfo2203A.geso_val = frmSIPatient.Geso_val;

                requestGzsiModel2203A.mdtrtinfo = new API.GZSI.Models.Request.RequestGzsiModel2203A.Mdtrtinfo();
                requestGzsiModel2203A.mdtrtinfo = mdtrtinfo2203A;
                requestGzsiModel2203A.diseinfo = new List<API.GZSI.Models.Request.RequestGzsiModel2203A.Diseinfo>();
                foreach (API.GZSI.Models.Request.RequestGzsiModel2203.Diseinfo dise in diaList)
                {
                    API.GZSI.Models.Request.RequestGzsiModel2203A.Diseinfo diseinfo = new Models.Request.RequestGzsiModel2203A.Diseinfo();
                    diseinfo.diag_type = dise.diag_type;
                    diseinfo.diag_srt_no = dise.diag_srt_no;
                    diseinfo.diag_code = this.localMgr.GetICDYB2(dise.diag_code);
                    diseinfo.diag_name = dise.diag_name;
                    diseinfo.diag_dept = dise.diag_dept;
                    diseinfo.dise_dor_no = this.localMgr.GetGDDoct(dise.dise_dor_no);
                    diseinfo.dise_dor_name = dise.dise_dor_name;
                    diseinfo.diag_time = dise.diag_time;
                    diseinfo.vali_flag = dise.vali_flag;
                    requestGzsiModel2203A.diseinfo.Add(diseinfo);
                }

                returnvalue = outPatient2203A.CallService(requestGzsiModel2203A, ref responseGzsiModel2203A, SerialNumber, strTransVersion, strVerifyCode);

                if (returnvalue == -1)
                {
                    CancelRegInfoOutpatient(r);
                    this.errMsg = outPatient2203A.ErrorMsg;
                    return -1;
                }
            }


            #endregion

            #region 门诊费用明细信息上传2204

            OutPatient2204 outPatient2204 = new OutPatient2204();
            Models.Request.RequestGzsiModel2204 requestGzsiModel2204 = new API.GZSI.Models.Request.RequestGzsiModel2204();
            Models.Response.ResponseGzsiModel2204 responseGzsiModel2204 = new API.GZSI.Models.Response.ResponseGzsiModel2204();
            requestGzsiModel2204.feedetail = new List<API.GZSI.Models.Request.RequestGzsiModel2204.Feedetail>();

            decimal medfee_sumamt = 0.0m;

            for (int i = 0; i < feeListForUpload.Count; i++)
            {
                FS.HISFC.Models.Fee.Outpatient.FeeItemList feeItem = feeListForUpload[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (feeItem == null)
                {
                    continue;
                }

                decimal unitPrice = 0.0m;
                decimal unitCout = 0.0m;
                if (!this.GetPrice(feeItem, ref unitPrice))
                {
                    CancelRegInfoOutpatient(r);
                    this.errMsg = "上传费用时获取价格失败！";
                    return -1;
                }

                if (!this.GetCount(feeItem, ref unitCout))
                {
                    CancelRegInfoOutpatient(r);
                    this.errMsg = "上传费用时获取数量失败！";
                    return -1;
                }

                Models.Request.RequestGzsiModel2204.Feedetail feedetail2204 = new API.GZSI.Models.Request.RequestGzsiModel2204.Feedetail();

                FS.HISFC.Models.Base.Employee doctor = interMgr.GetEmployeeInfo(feeItem.RecipeOper.ID);//获取开立医生信息

                //符合项目的feeItem.RecipeNO为空

                feedetail2204.feedetl_sn = !string.IsNullOrEmpty(feeItem.RecipeNO) ? (feeItem.RecipeNO + feeItem.SequenceNO.ToString()) : i.ToString(); //r.ID;//费用明细流水号 Y 单次就诊内唯一
                feedetail2204.psn_no = r.SIMainInfo.Psn_no;//人员编号 Y
                feedetail2204.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊ID Y
                feedetail2204.chrg_bchno = "1";//收费批次号 
                feedetail2204.dise_codg = r.SIMainInfo.Dise_code;//病种编号 
                feedetail2204.rxno = feeItem.Order.ID;//处方号 
                feedetail2204.rx_circ_flag = "0";//外购处方标志 Y

                if (feeItem.ChargeOper.OperTime == DateTime.MinValue)
                {
                    //当场收费上传的患者收费日期为空，取系统当前时间
                    feedetail2204.fee_ocur_time = this.localMgr.GetSysDate("yyyy-MM-dd HH:mm:ss");//费用发生日期 Y
                }
                else
                {
                    //补上传的患者取当时的收费日期


                    feedetail2204.fee_ocur_time = feeItem.ChargeOper.OperTime.ToString("yyyy-MM-dd HH:mm:ss");//费用发生日期 Y
                }

                feedetail2204.cnt = unitCout.ToString("F4");//数量 Y
                feedetail2204.pric = unitPrice.ToString("F4");//单价 Y
                feedetail2204.det_item_fee_sumamt = feeItem.SIft.OwnCost.ToString("F2");//明细项目费用总额 Y
                feedetail2204.sin_dos_dscr = "";//单次剂量描述 
                feedetail2204.used_frqu_dscr = "";//使用频次描述 
                feedetail2204.prd_days = "";//用药周期天数 
                feedetail2204.medc_way_dscr = "";//用药途径描述 
                feedetail2204.med_list_codg = feeItem.Compare.CenterItem.ID;//医疗目录编码 Y
                feedetail2204.medins_list_codg = feeItem.Compare.SpellCode.UserCode;//医疗机构目录编码 Y
                feedetail2204.medins_list_name = feeItem.Item.Name;//医疗机构目录名称 Y

                //组合项目之前的历史数据此字段为空
                string deptCode = feeItem.DoctDeptInfo.ID;
                if (string.IsNullOrEmpty(deptCode))
                {
                    deptCode = ((FS.HISFC.Models.Registration.Register)feeItem.Patient).DoctorInfo.Templet.Dept.ID;
                }

                feedetail2204.bilg_dept_codg = deptCode;// this.localMgr.getDeptYBCode(deptCode);//开单科室编码 Y
                feedetail2204.bilg_dept_name = this.localMgr.getDeptName(deptCode);//开单科室名称 Y
                feedetail2204.bilg_dr_codg = this.localMgr.GetGDDoct(doctor.ID);//开单医生编码 Y
                feedetail2204.bilg_dr_name = doctor.Name;//开单医生姓名 Y
                feedetail2204.acord_dept_codg = "";//受单科室编码 
                feedetail2204.acord_dept_name = "";//受单科室名称 
                feedetail2204.orders_dr_code = "";//受单医生编码 
                feedetail2204.orders_dr_name = "";//受单医生姓名 

                //a)当目录限制使用标志为“是”时:
                //i) 医院审批标志为“0”或“2”时，明细按照自费处理； 
                //ii) 医院审批标志为“1”时，明细按纳入报销处理。
                //b). 当目录限制使用标志为“否”时: 
                //i) 医院审批标志为“0”或“1”时，明细按照实际情况处理； 
                //ii) 医院审批标志为“2”时，明细按照自费处理。
                feedetail2204.hosp_appr_flag = string.IsNullOrEmpty(feeItem.RangeFlag) ? "1" : feeItem.RangeFlag;//医院审批标志 

                //限制用药判断
                if (this.localMgr.IfRangeItem(feeItem.Item.UserCode) == "1" && feeItem.RangeFlag == "0")
                {
                    feedetail2204.hosp_appr_flag = "2";//不报销
                }

                feedetail2204.tcmdrug_used_way = "";//中药使用方式 
                feedetail2204.etip_flag = "";//外检标志 
                feedetail2204.etip_hosp_code = "";//外检医院编码 
                feedetail2204.dscg_tkdrug_flag = "";//出院带药标志 
                feedetail2204.matn_fee_flag = "";//生育费用标志 
                feedetail2204.unchk_flag = "1";//不进行审核标志 
                feedetail2204.unchk_memo = "";//不进行审核说明 

                requestGzsiModel2204.feedetail.Add(feedetail2204);

                medfee_sumamt += feeItem.SIft.OwnCost;
            }

            returnvalue = outPatient2204.CallService(requestGzsiModel2204, ref responseGzsiModel2204, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                CancelRegInfoOutpatient(r);
                this.errMsg = outPatient2204.ErrorMsg;
                return -1;
            }

            //插入医保费用返回结果
            if (responseGzsiModel2204.output.result != null && responseGzsiModel2204.output.result.Count > 0)
            {
                int rtn = this.localMgr.InsertOutBalanceSIFeeDetail(r, responseGzsiModel2204.output.result);
                if (rtn < 0)
                {
                    this.CancelRegInfoOutpatient(r);
                    this.errMsg = this.localMgr.Err;
                    return -1;
                }
            }

            #endregion

            #region 门诊预结算2206

            OutPatient2206 outPatient2206 = new OutPatient2206();
            Models.Request.RequestGzsiModel2206 requestGzsiModel2206 = new API.GZSI.Models.Request.RequestGzsiModel2206();
            Models.Response.ResponseGzsiModel2206 responseGzsiModel2206 = new API.GZSI.Models.Response.ResponseGzsiModel2206();
            Models.Request.RequestGzsiModel2206.Mdtrtinfo mdtrtinfo2206 = new API.GZSI.Models.Request.RequestGzsiModel2206.Mdtrtinfo();

            mdtrtinfo2206.psn_no = r.SIMainInfo.Psn_no;//人员编号 Y
            mdtrtinfo2206.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y
            mdtrtinfo2206.mdtrt_cert_no = r.SIMainInfo.Certno;//就诊凭证编号 
            mdtrtinfo2206.med_type = r.SIMainInfo.Med_type;//医疗类别 Y
            mdtrtinfo2206.medfee_sumamt = medfee_sumamt.ToString();//医疗费总额 Y
            mdtrtinfo2206.psn_setlway = r.SIMainInfo.Psn_setlway;//个人结算方式 Y
            mdtrtinfo2206.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊ID Y
            mdtrtinfo2206.chrg_bchno = "1";//收费批次号 Y （医保说取费用上传的费用明细流水号）
            mdtrtinfo2206.acct_used_flag = "1";//个人账户使用标志 Y
            mdtrtinfo2206.insutype = r.SIMainInfo.Insutype;//险种类型 Y

            requestGzsiModel2206.data = new API.GZSI.Models.Request.RequestGzsiModel2206.Mdtrtinfo();
            requestGzsiModel2206.data = mdtrtinfo2206;

            returnvalue = outPatient2206.CallService(requestGzsiModel2206, ref responseGzsiModel2206, SerialNumber, strTransVersion, strVerifyCode);

            if (returnvalue == -1)
            {
                CancelRegInfoOutpatient(r);
                this.errMsg = outPatient2206.ErrorMsg;
                return -1;
            }

            //总金额
            r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.medfee_sumamt);
            //个人支付金额
            r.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.psn_part_amt)
                - FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.acct_pay);
            //统筹支付金额
            r.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.fund_pay_sumamt);
            //账户支付金额
            r.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.acct_pay);

            //统筹信息
            r.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.medfee_sumamt);  //医疗费总额
            r.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.act_pay_dedc); //实际支付起付线
            r.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.inscp_scp_amt);
            r.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.fund_pay_sumamt);
            r.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.psn_part_amt);
            r.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.hosp_part_amt);
            //统筹基金明细
            r.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.hifp_pay);
            r.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.cvlserv_pay);
            r.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.hifes_pay);
            r.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.hifmi_pay);
            r.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.hifob_pay);
            r.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.maf_pay);
            //自负明细
            r.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.fulamt_ownpay_amt);
            r.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.overlmt_selfpay);
            r.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.preselfpay_amt);
            r.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.acct_pay);
            r.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.acct_mulaid_pay);
            r.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.balc);
            r.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2206.output.setlinfo.psn_cash_pay);
            //备注
            r.SIMainInfo.Memo = responseGzsiModel2206.output.setlinfo.memo;

            //if (r.SIMainInfo.TotCost != r.SIMainInfo.OwnCost + r.SIMainInfo.PubCost + r.SIMainInfo.PayCost)
            //{
            //    CancelRegInfoOutpatient(r);
            //    this.errMsg = "预结算失败，总金额与个人支付金额，统筹支付金额，账户支付金额总额不平！";
            //    return -1;
            //}

            frmBalanceShow balanceShow = new frmBalanceShow();
            balanceShow.PatientInfo = r;
            balanceShow.DialogTitle = "门诊医保结算";

            if (balanceShow.ShowDialog() != DialogResult.OK)
            {
                CancelRegInfoOutpatient(r);
                this.errMsg = "操作取消！";
                return -1;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 门诊结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {

            #region 电子凭证 // {C891E5B0-BE9A-47AC-8604-EDE14067FB60}
            if (r.SIMainInfo.Mdtrt_cert_type == "01")
            {
                //电子凭证需要重新扫码
                frmPatientQuery frmSIPatient = new frmPatientQuery();
                frmSIPatient.Patient = r;
                frmSIPatient.OperateType = "1";
                if (frmSIPatient.ShowDialog() != DialogResult.OK)
                {
                    this.ErrMsg = "取消医保结算！";
                    return -1;
                }
            }
            #endregion

            decimal medfee_sumamt = 0.0m;

            //只上传已对照的项目
            ArrayList feeListForUpload = new ArrayList();
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
            {
                if (this.localMgr.GetCompareSingleItem("4", f.Item.ID, ref f.Compare) == 0) //找到了对照维护
                {
                    medfee_sumamt += f.SIft.OwnCost;
                    feeListForUpload.Add(f);
                }
            }

            //计算结算总金额
            //medfee_sumamt = feeDetails.Cast<FS.HISFC.Models.Fee.Outpatient.FeeItemList>().Sum(l => l.SIft.OwnCost);
            OutPatient2207 outPatient2207 = new OutPatient2207();
            Models.Request.RequestGzsiModel2207 requestGzsiModel2207 = new API.GZSI.Models.Request.RequestGzsiModel2207();
            Models.Response.ResponseGzsiModel2207 responseGzsiModel2207 = new API.GZSI.Models.Response.ResponseGzsiModel2207();

            Models.Request.RequestGzsiModel2207.Mdtrtinfo mdtrtinfo2207 = new API.GZSI.Models.Request.RequestGzsiModel2207.Mdtrtinfo();
            mdtrtinfo2207.psn_no = r.SIMainInfo.Psn_no;//人员编号 Y
            mdtrtinfo2207.mdtrt_cert_type = r.SIMainInfo.Mdtrt_cert_type;//就诊凭证类型 Y
            mdtrtinfo2207.mdtrt_cert_no = r.SIMainInfo.Certno;//就诊凭证编号 
            mdtrtinfo2207.medfee_sumamt = medfee_sumamt.ToString();//医疗费总额 Y
            mdtrtinfo2207.mdtrt_id = r.SIMainInfo.Mdtrt_id;//就诊ID Y
            mdtrtinfo2207.chrg_bchno = "1";//收费批次号 Y
            mdtrtinfo2207.insutype = r.SIMainInfo.Insutype;//险种类型 
            mdtrtinfo2207.med_type = r.SIMainInfo.Med_type;//医疗类别 Y
            mdtrtinfo2207.acct_used_flag = "1";//个人账户使用标志 Y 
            mdtrtinfo2207.psn_setlway = r.SIMainInfo.Psn_setlway;//个人结算方式  Y 
            mdtrtinfo2207.invono = r.SIMainInfo.InvoiceNo;//发票号 
            mdtrtinfo2207.fulamt_ownpay_amt = r.SIMainInfo.Ownpay_amt.ToString(); //全自费金额
            mdtrtinfo2207.overlmt_selfpay = r.SIMainInfo.Overlmt_selfpay.ToString(); //超限价金额
            mdtrtinfo2207.preselfpay_amt = r.SIMainInfo.Preselfpay_amt.ToString(); //先行自付金额 
            mdtrtinfo2207.fulamt_ownpay_amt = r.SIMainInfo.Ownpay_amt.ToString(); //符合政策范围金额 
            requestGzsiModel2207.data = mdtrtinfo2207;

            int ret = outPatient2207.CallService(requestGzsiModel2207, ref responseGzsiModel2207, SerialNumber, strTransVersion, strVerifyCode);

            if (ret == -1)
            {
                CancelRegInfoOutpatient(r);
                this.errMsg = responseGzsiModel2207.err_msg;
                return -1;
            }

            try
            {
                //设置结算信息
                r.SIMainInfo.Mdtrt_id = responseGzsiModel2207.output.setlinfo.mdtrt_id;  //就诊ID
                r.SIMainInfo.Setl_id = responseGzsiModel2207.output.setlinfo.setl_id; //结算ID
                r.SIMainInfo.Psn_no = responseGzsiModel2207.output.setlinfo.psn_no; //人员编号
                r.SIMainInfo.Psn_name = responseGzsiModel2207.output.setlinfo.psn_name; //人员姓名
                r.SIMainInfo.Psn_cert_type = responseGzsiModel2207.output.setlinfo.psn_cert_type; //人员证件类型
                r.SIMainInfo.Certno = responseGzsiModel2207.output.setlinfo.certno; //证件号码
                r.SIMainInfo.Gend = responseGzsiModel2207.output.setlinfo.gend; //性别
                r.SIMainInfo.Naty = responseGzsiModel2207.output.setlinfo.naty; //民族
                r.SIMainInfo.Brdy = DateTime.Parse(responseGzsiModel2207.output.setlinfo.brdy); //出生日期
                r.SIMainInfo.Age = responseGzsiModel2207.output.setlinfo.age; //年龄
                r.SIMainInfo.Insutype = responseGzsiModel2207.output.setlinfo.insutype; //险种类型
                r.SIMainInfo.Psn_type = responseGzsiModel2207.output.setlinfo.psn_type; //人员类别
                r.SIMainInfo.Cvlserv_flag = responseGzsiModel2207.output.setlinfo.cvlserv_flag; //公务员标志
                r.SIMainInfo.Setl_time = DateTime.Parse(responseGzsiModel2207.output.setlinfo.setl_time); //结算时间
                //r.SIMainInfo.Psn_setlway = responseGzsiModel2207.output.setlinfo.psn_setlway; //个人结算方式 
                r.SIMainInfo.Mdtrt_cert_type = responseGzsiModel2207.output.setlinfo.mdtrt_cert_type; //就诊凭证类型
                r.SIMainInfo.Med_type = responseGzsiModel2207.output.setlinfo.med_type; //医疗类别
                r.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.medfee_sumamt); //医疗费总额
                r.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.fulamt_ownpay_amt); //全自费金额
                r.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.overlmt_selfpay); //超限价自费费用
                r.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.preselfpay_amt); //先行自付金额
                r.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.inscp_scp_amt); //符合范围金额
                r.SIMainInfo.Med_sumfee = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.med_sumfee); //医保认可费用总额
                r.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.act_pay_dedc); //实际支付起付线
                r.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.hifp_pay); //基本医疗统筹基金支出
                r.SIMainInfo.Pool_prop_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.pool_prop_selfpay); //基本医疗统筹比例自付
                r.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.cvlserv_pay); //公务员医疗补助基金支出
                r.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.hifes_pay); //补充医疗保险基金支出
                r.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.hifmi_pay); //大病补充医疗保险基金支出
                r.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.hifob_pay); //大额医疗补助基金支出
                r.SIMainInfo.Hifdm_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.hifdm_pay); //伤残人员医疗保障基金支出
                r.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.maf_pay); //医疗救助基金支出
                r.SIMainInfo.Oth_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.oth_pay); //其他基金支出
                r.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.fund_pay_sumamt); //基金支付总额
                r.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.hosp_part_amt); //医院负担金额 
                r.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.psn_part_amt); //个人负担总金额
                r.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.acct_pay); //个人账户支出
                r.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.psn_cash_pay); //现金支付金额
                r.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.acct_mulaid_pay); //账户共济支付金额
                r.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.balc); //个人账户支出后余额
                r.SIMainInfo.Clr_optins = responseGzsiModel2207.output.setlinfo.clr_optins; //清算经办机构
                r.SIMainInfo.Clr_way = responseGzsiModel2207.output.setlinfo.clr_way; //清算方式
                r.SIMainInfo.Clr_type = responseGzsiModel2207.output.setlinfo.clr_type; //清算类别
                r.SIMainInfo.Medins_setl_id = responseGzsiModel2207.output.setlinfo.medins_setl_id; //医药机构结算ID
                r.SIMainInfo.InvoiceNo = ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)feeDetails[0]).Invoice.ID;

                r.SIMainInfo.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.medfee_sumamt);
                //个人支付金额
                r.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.psn_part_amt)
                    - FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.acct_pay);
                //统筹支付金额
                r.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.fund_pay_sumamt);
                //账户支付金额
                r.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2207.output.setlinfo.acct_pay);

            }
            catch (Exception ex)
            {
                CancelBalanceOutpatient(r, ref feeListForUpload);
                this.errMsg = "更新医保结算信息失败,请联系信息科!";
                return -1;
            }

            returnvalue = this.localMgr.UpdateSiMainInfoOutBalanceInfo(r);

            if (returnvalue <= 0)
            {
                CancelBalanceOutpatient(r, ref feeListForUpload);
                this.errMsg = "更新医保结算信息失败,请联系信息科!";
                return -1;
            }

            //更新医保费用结算ID
            if (this.localMgr.UpdateOutBalanceSIFeeDetail(r) < 0)
            {
                CancelBalanceOutpatient(r, ref feeListForUpload);
                this.ErrMsg = "更新医保费用明细信息(gzsi_feedetail)失败。";
                return -1;
            }

            returnvalue = this.localMgr.InsertOutBalanceFeeDetail(r, feeListForUpload);

            if (returnvalue <= 0)
            {
                CancelBalanceOutpatient(r, ref feeListForUpload);
                this.errMsg = "插入医保结算明细失败,请联系信息科!";
                return -1;
            }

            returnvalue = this.localMgr.InsertOutBalanceSetldetail(r, responseGzsiModel2207.output.setldetail);

            if (returnvalue <= 0)
            {
                CancelBalanceOutpatient(r, ref feeListForUpload);
                this.errMsg = "插入医保结算基金分项信息出错,请联系信息科!";
                return -1;
            }

            //插入到门诊控费
            //ArrayList feeLimitItemList = new ArrayList();
            //feeLimitItemList = constMgr.GetAllList(Common.Constants.GZSI_CODE_PREFIX + "limit_item");
            string value = ctlParamManage.GetControlParam<string>("YBLF01");
            if (value == "1")
            {
                //生育门诊控费
                if (r.SIMainInfo.Med_type.ToString() == "51")  //51-生育门诊
                {
                    List<ExpItemMedical> explist = this.accountManager.QueryExpItemMedicalByCardNo(r.PID.CardNO);


                    //decimal total = 0.0m;
                    foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                    {
                        //FS.HISFC.Models.Base.Const item = new FS.HISFC.Models.Base.Const();

                        ExpItemMedical itemmedical = new ExpItemMedical();

                        if (f.UndrugComb != null && !string.IsNullOrEmpty(f.UndrugComb.ID))  //适配组合
                        {
                            itemmedical = explist.Where(t => t.ItemCode == f.UndrugComb.ID && t.ItemSubcode == f.Item.ID).SingleOrDefault();
                        }
                        else
                        {
                            itemmedical = explist.Where(t => t.ItemCode == f.Item.ID).SingleOrDefault();
                        }

                        if (itemmedical != null && !string.IsNullOrEmpty(itemmedical.ClinicCode))
                        {
                            decimal qty = f.Item.Qty;
                            returnvalue = this.localMgr.InsertLimitAccountFeeItem(f, itemmedical.ItemCode, itemmedical.ItemName, qty, itemmedical.ClinicCode, r);


                            if (returnvalue == 1)
                            {
                                int s = this.localMgr.UpdateExpItemDetailQty(r, itemmedical, qty);

                                if (s == 1)
                                {
                                    //total += itemmedical.UnitPrice * qty;
                                }
                            }
                        }
                    }

                    if (r.SIMainInfo.Fund_pay_sumamt > 0)
                    {
                        checkAccount(r.PID.CardNO, r.SIMainInfo.Fund_pay_sumamt, ((FS.HISFC.Models.Fee.Outpatient.FeeItemList)feeDetails[0]).Invoice.ID, "门诊");
                    }

                }

            }

            return 1;
        }


        /// <summary>
        /// 检查是否存在普通账户，不存在则创建,创建后进行账户返账
        /// </summary>
        private void checkAccount(string cardno, decimal tolmony, string invoince, string source)
        {
            try
            {
                if (string.IsNullOrEmpty(cardno))
                {
                    return;
                }

                this.account = this.accountManager.GetAccountByCardNoEX(cardno);//检查是否存在账户

                accountCard = new FS.HISFC.Models.Account.AccountCard();
                accountManager.GetCardByRule(cardno, ref accountCard);
                if (this.account == null || string.IsNullOrEmpty(this.account.ID))
                {
                    this.account = this.GetAccount();

                    if (this.account == null || accountManager.InsertAccount(this.account) < 0)
                    {
                        return;
                    }
                }

                List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new List<FS.HISFC.Models.Account.AccountDetail>();
                accountDetailList = this.accountManager.GetAccountDetail(this.account.ID, "1", "ALL");

                if (accountDetailList != null && accountDetailList.Count > 0)  //检查是否存在普通账户，存在则跳过，不存在则创建
                {

                }
                else
                {
                    this.accountDetail = this.GetAccountDetail(cardno);

                    if (this.accountDetail == null || accountManager.InsertAccountDetail(this.accountDetail) < 0)
                    {
                        return;
                    }

                    FS.HISFC.Models.Account.AccountRecord accountRecord = this.GetAccountRecord(FS.HISFC.Models.Account.OperTypes.NewAccount);
                    if (accountRecord == null || accountManager.InsertAccountRecordEX(accountRecord) < 0)
                    {
                        return;
                    }
                }

                FS.HISFC.Models.Account.PrePay prePay = GetPrePayEX(FS.HISFC.Models.Base.EnumValidState.Valid, 1, tolmony, cardno, invoince, source);//往账户充钱

                if (prePay == null)
                {
                    return;
                }

                if (!accountManager.AccountPrePayManagerEX(prePay, 1))
                {
                    return;
                }
            }
            catch
            {
                return;
            }

        }


        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="state"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Account.PrePay GetPrePayEX(FS.HISFC.Models.Base.EnumValidState state, int mode, decimal paynum, string cardno, string invoincenum, string source)
        {
            try
            {
                decimal pay = paynum;
                decimal donate = 0.0m;

                string invoiceNO = "";
                #region 获取发票号

                invoiceNO = this.feeIntegrate.GetNewInvoiceNO("A");
                if (string.IsNullOrEmpty(invoiceNO))
                {
                    invoiceNO = invoincenum;
                }

                #endregion

                //FS.HISFC.Models.Account.Account account1 = new FS.HISFC.Models.Account.Account();
                //account1 = accountManager.GetAccountVacancyEX(cardno);

                List<FS.HISFC.Models.Account.AccountDetail> accountDetailList = new List<FS.HISFC.Models.Account.AccountDetail>();
                accountDetailList = this.accountManager.GetAccountDetail(this.account.ID, "1", "ALL");
                FS.HISFC.Models.Account.AccountDetail accountDetail2 = new FS.HISFC.Models.Account.AccountDetail();
                accountDetail2 = accountDetailList[0] as FS.HISFC.Models.Account.AccountDetail;

                #region 预交金实体
                FS.HISFC.Models.Account.PrePay prePay = new FS.HISFC.Models.Account.PrePay();
                prePay.Patient = accountCard.Patient;//患者基本信息
                prePay.InvoiceNO = invoiceNO;
                if (source == "住院" && pay > 0)
                {
                    prePay.PayType.ID = "YFZ";//支付方式
                    prePay.PayType.Name = "医保住院返账";
                }
                else if (source == "住院" && pay < 0)
                {
                    prePay.PayType.ID = "YFZT";//支付方式
                    prePay.PayType.Name = "医保住院退账";
                }
                else if (source == "门诊" && pay > 0)
                {
                    prePay.PayType.ID = "YF";//支付方式
                    prePay.PayType.Name = "医保门诊返账";
                }
                else
                {
                    prePay.PayType.ID = "YFT";//支付方式
                    prePay.PayType.Name = "医保门诊退账";
                }


                //prePay.Bank = this.cmbPayType.bank.Clone();//开户银行
                //if (prePay.PayType.ID != "CA" && !string.IsNullOrEmpty(BankPayType))
                //{
                //    prePay.PayType.ID = BankPayType;//支付方式
                //    prePay.PayType.Name = "银行卡";
                //}

                prePay.PrePayOper.ID = accountManager.Operator.ID;//操作员编号
                prePay.PrePayOper.Name = accountManager.Operator.Name;//操作员姓名
                prePay.ValidState = state;
                prePay.BaseCost = pay;//预交金
                prePay.PrePayOper.OperTime = accountManager.GetDateTimeFromSysDateTime();//系统时间
                prePay.AccountNO = account.ID; //帐号
                prePay.IsHostory = false; //是否历史数据
                prePay.BaseVacancy = accountDetail2.BaseVacancy + pay;
                prePay.Memo = "医保返账" + source;
                prePay.DonateCost = donate;
                prePay.DonateVacancy = accountDetail2.DonateVacancy + donate;
                prePay.AccountType.ID = "1";//账户类型编码
                prePay.AccountType.Name = "普通账户";//账户类型编码
                #endregion
                return prePay;
            }
            catch
            {

                return null;
            }
        }


        /// <summary>
        /// 得到卡的交易实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.AccountRecord GetAccountRecord(FS.HISFC.Models.Account.OperTypes opertype)
        {
            try
            {
                //交易信息
                FS.HISFC.Models.Account.AccountRecord accountRecord = new FS.HISFC.Models.Account.AccountRecord();
                accountRecord.AccountNO = this.account.ID;//帐号
                accountRecord.Patient = accountCard.Patient;//门诊卡号
                accountRecord.FeeDept.ID = (accountManager.Operator as FS.HISFC.Models.Base.Employee).Dept.ID;//科室编码
                accountRecord.Oper.ID = accountManager.Operator.ID;//操作员
                accountRecord.OperTime = accountManager.GetDateTimeFromSysDateTime();//操作时间
                accountRecord.IsValid = true;//是否有效
                accountRecord.OperType.ID = (int)opertype;
                accountRecord.AccountType.ID = "1";

                return accountRecord;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 默认创建一个账户明细
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>

        private FS.HISFC.Models.Account.AccountDetail GetAccountDetail(string cardno)
        {
            try
            {
                this.accountDetail = new FS.HISFC.Models.Account.AccountDetail();
                accountDetail.ID = this.account.ID;
                this.accountDetail.AccountType.ID = "1";
                this.accountDetail.CardNO = cardno;
                this.accountDetail.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                this.accountDetail.CreateEnvironment.ID = accountManager.Operator.ID;
                this.accountDetail.CreateEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();
                this.accountDetail.OperEnvironment.ID = accountManager.Operator.ID;
                this.accountDetail.OperEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();
                return this.accountDetail;
            }
            catch
            {
                return null;
            }

        }


        /// <summary>
        /// 账户实体
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.Account.Account GetAccount()
        {

            try
            {
                //账户信息
                account = new FS.HISFC.Models.Account.Account();
                account.ID = accountManager.GetAccountNO();
                account.AccountCard = accountCard;
                //是否取默认密码，系统设置，上线初期一般默认密码。
                account.PassWord = "000000";
                //是否可用
                account.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;
                account.AccountLevel.ID = "1";
                account.CreateEnvironment.ID = accountManager.Operator.ID;
                account.CreateEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();
                account.OperEnvironment.ID = accountManager.Operator.ID;
                account.OperEnvironment.OperTime = accountManager.GetDateTimeFromSysDateTime();
                return account;
            }
            catch
            {
                return null;
            }
        }




        /// <summary>
        /// 上传门诊费用明细
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 上传门诊费用
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 修改门诊费用
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            return 1;
        }

        /// <summary>
        /// 修改门诊费用
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除门诊费用
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            return 1;
        }

        /// <summary>
        /// 删除门诊上传费用
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除门诊费用
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            return 1;
        }

        /// <summary>
        /// 查询门诊费用明细
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        #region 旧门诊取消结算
        /// <summary>
        /// 门诊取消结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        //public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        //{
        //    FS.HISFC.Models.Registration.Register patientOutBalanceInfo = null;
        //    patientOutBalanceInfo = this.localMgr.GetOutPatientBalanceInfo(r, "1");//获取c
        //    FS.HISFC.Models.Registration.Register siPatientInfo = null;
        //    if (patientOutBalanceInfo == null)
        //    {
        //        this.ErrMsg = "获取患者医保登记信息失败。";
        //        return -1;
        //    }
        //    if (FS.FrameWork.Management.Connection.Hospital.Memo == "001")
        //    {
        //        if (!string.IsNullOrEmpty(patientOutBalanceInfo.SIMainInfo.RegNo))
        //        {
        //            FS.FrameWork.Management.Connection.Hospital.Memo = patientOutBalanceInfo.SIMainInfo.RegNo.Substring(0, 6);
        //        }
        //        else
        //        {
        //            this.ErrMsg = "获取医院ID错误。请联系工程师！！！";
        //            return -1;
        //        }
        //    }
        //    if (r.Pact.ID == "113")
        //    {
        //        #region 退费
        //        Models.Request.RequestGzsiModel2261 requestGzsiModel2261 = new API.GZSI.Models.Request.RequestGzsiModel2261();
        //        Models.Response.ResponseGzsiModel2261 responseBizh110405 = new API.GZSI.Models.Response.ResponseGzsiModel2261();
        //        // Models.Response.ResponseBase responseBase = new API.GZSI.Models.Response.ResponseBase();
        //        requestGzsiModel2261.setl_id = "";//结算ID
        //        requestGzsiModel2261.mdtrt_id = "";//就诊ID
        //        requestGzsiModel2261.psn_no = this.localMgr.Operator.ID;               //操作员工号
        //        // requestBizh110405.aaz217 = patientOutBalanceInfo.SIMainInfo.RegNo;  //就诊登记号
        //        string msg = string.Empty;
        //        //if (!requestGzsiModel2261.Call(out responseBase, out msg))
        //        //{
        //        //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
        //        //    return -1;
        //        //}
        //        #endregion
        //    }
        //    else
        //    {
        //        #region 退费
        //        Models.Request.RequestGzsiModel2261 requestGzsiModel2261 = new API.GZSI.Models.Request.RequestGzsiModel2261();
        //        Models.Response.ResponseGzsiModel2261 responseBizh110405 = new API.GZSI.Models.Response.ResponseGzsiModel2261();
        //        //Models.Response.ResponseBase responseBase = new API.GZSI.Models.Response.ResponseBase();
        //        requestGzsiModel2261.setl_id = "";//结算ID
        //        requestGzsiModel2261.mdtrt_id = "";//就诊ID
        //        requestGzsiModel2261.psn_no = this.localMgr.Operator.ID;               //操作员工号
        //        // requestBizh110405.aaz217 = patientOutBalanceInfo.SIMainInfo.RegNo;  //就诊登记号
        //        string msg = string.Empty;
        //        //if (!requestGzsiModel2261.Call(out responseBase, out msg))
        //        //{
        //        //    FS.FrameWork.WinForms.Classes.Function.Msg(msg, 111);
        //        //    return -1;
        //        //}
        //        #endregion
        //    }

        //    if (this.localMgr.CancelOutPatientBalance(r) < 0)
        //    {
        //        this.ErrMsg = this.localMgr.Err;
        //        return -1;
        //    }

        //    return 1;
        //}
        #endregion

        /// <summary>
        /// 门诊取消结算
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            OutPatient2208 outPatient2208 = new OutPatient2208();
            Models.Request.RequestGzsiModel2208 requestGzsiModel2208 = new API.GZSI.Models.Request.RequestGzsiModel2208();
            Models.Response.ResponseGzsiModel2208 responseGzsiModel2208 = new API.GZSI.Models.Response.ResponseGzsiModel2208();
            Models.Request.RequestGzsiModel2208.Mdtrtinfo mdtrtinfo2208 = new API.GZSI.Models.Request.RequestGzsiModel2208.Mdtrtinfo();

            mdtrtinfo2208.setl_id = r.SIMainInfo.Setl_id; //结算ID
            mdtrtinfo2208.mdtrt_id = r.SIMainInfo.Mdtrt_id; //就诊ID 
            mdtrtinfo2208.psn_no = r.SIMainInfo.Psn_no; //人员编号
            requestGzsiModel2208.data = new API.GZSI.Models.Request.RequestGzsiModel2208.Mdtrtinfo();
            requestGzsiModel2208.data = mdtrtinfo2208;

            int ret = outPatient2208.CallService(requestGzsiModel2208, ref responseGzsiModel2208, SerialNumber, strTransVersion, strVerifyCode);

            if (ret == -1)
            {
                this.errMsg = outPatient2208.ErrorMsg;
                return -1;
            }

            FS.HISFC.Models.Registration.Register tmp = new FS.HISFC.Models.Registration.Register();
            tmp.Name = r.Name;
            tmp.IDCard = r.IDCard;
            //个人支付金额
            tmp.SIMainInfo.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.psn_part_amt)
                - FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.acct_pay);
            //统筹支付金额
            tmp.SIMainInfo.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.fund_pay_sumamt);
            //账户支付金额
            tmp.SIMainInfo.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.acct_pay);

            //统筹信息
            tmp.SIMainInfo.Medfee_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.medfee_sumamt);  //医疗费总额
            tmp.SIMainInfo.Act_pay_dedc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.act_pay_dedc); //实际支付起付线
            tmp.SIMainInfo.Inscp_scp_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.inscp_scp_amt);
            tmp.SIMainInfo.Fund_pay_sumamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.fund_pay_sumamt);
            tmp.SIMainInfo.Psn_part_am = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.psn_part_amt);
            tmp.SIMainInfo.Hosp_part_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.hosp_part_amt);
            //统筹基金明细
            tmp.SIMainInfo.Hifp_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.hifp_pay);
            tmp.SIMainInfo.Cvlserv_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.cvlserv_pay);
            tmp.SIMainInfo.Hifes_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.hifes_pay);
            tmp.SIMainInfo.Hifmi_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.hifmi_pay);
            tmp.SIMainInfo.Hifob_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.hifob_pay);
            tmp.SIMainInfo.Maf_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.maf_pay);
            //自负明细
            tmp.SIMainInfo.Ownpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.fulamt_ownpay_amt);
            tmp.SIMainInfo.Overlmt_selfpay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.overlmt_selfpay);
            tmp.SIMainInfo.Preselfpay_amt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.preselfpay_amt);
            tmp.SIMainInfo.Acct_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.acct_pay);
            tmp.SIMainInfo.Acct_mulaid_pay = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.acct_mulaid_pay);
            tmp.SIMainInfo.Balc = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.balc);
            tmp.SIMainInfo.Cash_payamt = FS.FrameWork.Function.NConvert.ToDecimal(responseGzsiModel2208.output.setlinfo.psn_cash_pay);

            //frmBalanceShow balanceShow = new frmBalanceShow();
            //balanceShow.PatientInfo = tmp;
            //balanceShow.DialogTitle = "门诊退费结算";

            //if (balanceShow.ShowDialog() != DialogResult.OK)
            //{
            //    //此处进行对冲后可以返回
            //    //this.errMsg = "操作取消！";
            //    //return -1;
            //}

            #region 屏蔽看不懂的
            ////if (this.localMgr.CancelOutPatientBalance(r.SIMainInfo.Mdtrt_id) < 0)
            //if (this.localMgr.CancelOutPatientRegInfo(r.SIMainInfo.Mdtrt_id) < 0)
            //{
            //    this.ErrMsg = this.localMgr.Err;
            //    return -1;
            //}

            //if (this.localMgr.CancelOutPatientBalance(r) < 0)
            //{
            //    this.ErrMsg = this.localMgr.Err;
            //    return -1;
            //}
            #endregion

            if (this.CancelRegInfoOutpatient(r) < 0)
            {
                return -1;
            }


            try
            {
                string value = ctlParamManage.GetControlParam<string>("YBLF01");
                if (value == "1" && feeDetails != null)
                {
                    //生育门诊控费
                    if (r.SIMainInfo.Med_type.ToString() == "51")  //51-生育门诊
                    {
                        List<ExpItemMedical> explist = this.accountManager.QueryExpItemMedicalByCardNo(r.PID.CardNO);
                      
                        decimal total = 0.0m;
                        foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in feeDetails)
                        {
                            ExpItemMedical itemmedical = new ExpItemMedical();

                            if (f.UndrugComb != null && !string.IsNullOrEmpty(f.UndrugComb.ID))  //适配组合
                            {
                                itemmedical = explist.Where(t => t.ItemCode == f.UndrugComb.ID && t.ItemSubcode == f.Item.ID).SingleOrDefault();
                            }
                            else
                            {
                                itemmedical = explist.Where(t => t.ItemCode == f.Item.ID).SingleOrDefault();
                            }

                            if (itemmedical != null && !string.IsNullOrEmpty(itemmedical.ClinicCode))
                            {
                                    decimal price = FS.FrameWork.Function.NConvert.ToDecimal(itemmedical.UnitPrice);
                                    decimal qty = f.Item.Qty > 0 ? f.Item.Qty * -1 : f.Item.Qty;

                                    //将生育门诊控费的项目插入到GZSI_HIS_ACCOUNTDETAIL  InsertLimitAccountFeeItem
                                    returnvalue = this.localMgr.InsertLimitAccountFeeItem(f, itemmedical.ItemCode, itemmedical.ItemName, qty,itemmedical.ClinicCode, r);
                                    if (returnvalue == 1)
                                    {
                                        int s = this.localMgr.UpdateExpItemDetailQty(r, itemmedical, qty);

                                        if (s == 1)
                                        {
                                            //total += price * qty;
                                        }
                                    }
                            }
                        }

                        total = tmp.SIMainInfo.Fund_pay_sumamt < 0 ? tmp.SIMainInfo.Fund_pay_sumamt : tmp.SIMainInfo.Fund_pay_sumamt * -1;

                        if (total < 0)
                        {
                            checkAccount(r.PID.CardNO, total , "", "门诊");
                        }

                    }
                }
            }
            catch
            {
                return 1;
            }


            return 1;
        }

        /// <summary>
        /// 门诊半退
        /// 不支持半退
        /// [整合至门诊预结算]
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref System.Collections.ArrayList feeDetails)
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion

        #region IMedcareTranscation 成员

        /// <summary>
        /// 事务开始
        /// </summary>
        public void BeginTranscation()
        {

        }

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <returns></returns>
        public long Commit()
        {
            return 1;
        }

        /// <summary>
        /// 连接
        /// </summary>
        /// <returns></returns>
        public long Connect()
        {
            return 1;
        }

        /// <summary>
        /// 断开
        /// </summary>
        /// <returns></returns>
        public long Disconnect()
        {
            return 1;
        }

        /// <summary>
        /// 回滚
        /// </summary>
        /// <returns></returns>
        public long Rollback()
        {
            return 1;
        }

        #endregion

        /// <summary>
        /// 获取项目价格
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetPrice(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal price)
        {
            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item phaItem = itemPharmacyManager.GetItem(f.Item.ID);
                if (phaItem == null)
                {
                    this.errMsg = "获得药品信息出错!";
                    return false;
                }
                f.Item.SpecialPrice = phaItem.SpecialPrice;
                f.Item.PackQty = phaItem.PackQty;
                f.Item.SysClass.ID = phaItem.SysClass.ID;
            }
            else if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                FS.HISFC.Models.Base.Item baseItem = new FS.HISFC.Models.Base.Item();
                baseItem = itemManager.GetItemByUndrugCode(f.Item.ID);
                if (baseItem == null)
                {
                    this.errMsg = "获得非药品信息出错!";
                    return false;
                }
                f.Item.SpecialPrice = baseItem.SpecialPrice;
                f.Item.PackQty = baseItem.PackQty;
                f.Item.SysClass.ID = baseItem.SysClass.ID;
            }

            //若没有维护医保价，则直接取普通价
            if (f.Item.SpecialPrice == 0)
            {
                f.Item.SpecialPrice = f.Item.Price;
            }

            //处理包装单位，部分项目没有包装单位
            if (f.Item.PackQty == 0)
            {
                f.Item.PackQty = 1;
            }

            if (f is FS.HISFC.Models.Fee.Outpatient.FeeItemList)
            {
                //参考门诊的价格计算规则
                //门诊价格计算规则代码路径：
                //FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.ITruncFee.ITruncFeeImplement
                f.SIft.TotCost = FS.FrameWork.Public.String.TruncateNumber(f.Item.SpecialPrice * f.Item.Qty / f.Item.PackQty, 2);
                f.SIft.RebateCost = FS.FrameWork.Public.String.TruncateNumber(f.FT.RebateCost * f.Item.Qty / f.Item.PackQty, 2);
                f.SIft.OwnCost = f.SIft.TotCost;
            }
            else if (f is FS.HISFC.Models.Fee.Inpatient.FeeItemList)
            {
                //参考门诊的价格计算规则
                //住院价格计算规则代码路径：
                //FS.HISFC.BizProcess.Integrate.Fee   ConvertOrderToFeeItemList函数
                f.SIft.TotCost = FS.FrameWork.Public.String.FormatNumber((f.Item.SpecialPrice / f.Item.PackQty), 2) * f.Item.Qty;
                f.SIft.TotCost = FS.FrameWork.Public.String.FormatNumber(f.SIft.TotCost, 2);
                f.SIft.RebateCost = FS.FrameWork.Public.String.FormatNumber((f.SIft.RebateCost / f.Item.PackQty), 2) * f.Item.Qty;
                f.SIft.RebateCost = FS.FrameWork.Public.String.FormatNumber(f.SIft.RebateCost, 2);
                f.SIft.OwnCost = f.SIft.TotCost;
            }
            else
            {
                return false;
            }

            //if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && f.Item.SysClass.ID.ToString() == "PCC" && !f.Item.Name.Contains("颗粒"))//中草药
            //{
            //    price = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost / f.Item.Qty), 4);
            //}
            //else
            //{
            //    price= FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost / f.Item.Qty), 4);
            //}

            price = FS.FrameWork.Public.String.FormatNumber(System.Math.Abs(f.SIft.TotCost / f.Item.Qty), 4);

            return true;
        }

        /// <summary>
        /// 获取项目数量
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private bool GetCount(FS.HISFC.Models.Fee.FeeItemBase f, ref decimal qty)
        {
            //数量大于300的，基本上就是中药了
            //if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug && f.Item.SysClass.ID.ToString() == "PCC" && !f.Item.Name.Contains("颗粒"))
            //{
            //    qty = FS.FrameWork.Public.String.FormatNumber(f.Item.Qty / 10, 4);
            //}
            //else
            //{
            //    qty =  f.Item.Qty;
            //}
            qty = f.Item.Qty;

            return true;
        }

        /// <summary>
        /// 清空rollback标志
        /// </summary>
        private void ClearStatus()
        {
            //this.rollBackStatus = string.Empty;
            //this.rollBackRegNo = string.Empty;
            //this.rollBackFeeList.Clear();
            //this.rollbackRegister = null;
        }

        /// <summary>
        /// 处理项目对照 - 处理国标码
        /// </summary>
        /// <param name="f">要处理的项目</param>
        /// <returns>成功返回1；失败返回-1</returns>
        public int DealFeeItemList(FS.HISFC.Models.Fee.FeeItemBase f)
        {
            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            this.itemPharmacyManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                #region 药品处理

                FS.HISFC.Models.Pharmacy.Item phaItem = this.itemPharmacyManager.GetItem(f.Item.ID);
                if (phaItem == null)
                {
                    this.ErrMsg = "获得药品信息出错!";
                    this.Disconnect();
                    return -1;
                }

                //自定义码和国标码全部赋值
                f.Item.GBCode = phaItem.GBCode;
                f.Item.UserCode = phaItem.UserCode;

                #endregion
            }
            else if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
            {
                #region 非药品处理

                FS.HISFC.Models.Base.Item baseItem = this.itemManager.GetUndrugByCode(f.Item.ID);
                if (baseItem == null)
                {
                    this.ErrMsg = "获得非药品信息出错!";
                    Disconnect();
                    return -1;
                }

                //自定义码和国标码全部赋值
                f.Item.GBCode = baseItem.GBCode;
                f.Item.UserCode = baseItem.UserCode;

                #endregion
            }

            return 1;

        }
    }
}
