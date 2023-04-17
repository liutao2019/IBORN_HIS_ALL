using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{28C63B3A-9C64-4010-891D-46F846EA093D}
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 出院登记组件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucPatientOut : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientOut()
        {
            InitializeComponent();

            this.AutoScroll = true;
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {

        }

        #region 变量

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 当前患者信息
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;

        /// <summary>
        /// 参数控制类{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 住院附材算法接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IDealSubjob iDealSubjob = null;

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// 药品业务层
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// 当前登陆人员（基本信息数据）
        /// </summary>
        private FS.HISFC.Models.Base.Employee Oper = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// 入出转业务层
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InPatient inPatienMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// 提示信息
        /// </summary>
        string Err = "";

        /// <summary>
        /// 出院登记调用接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.RADT.IPatientOut IPatientOut = null;

        /// <summary>
        /// 出院通知单打印接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintInHosNotice IPrintInHosNotice = null;

        /// <summary>
        /// 当前患者住院流水号
        /// </summary>
        string strInpatientNo;

        /// <summary>
        /// 是否保存后提示打印出院通知单
        /// </summary>
        private bool isPrintOutNote = false;

        /// <summary>
        /// 是否发送微信满意度问卷
        /// {97CCCEDA-BD81-447e-A104-F016BB61735B}停用点击发送
        /// </summary>
        private bool isSendWechat = false;

        /// <summary>
        /// 是否出院自动停止医嘱（控制参数HNZY20），当为false时，限制出院必须全停长嘱 
        /// </summary>
        private int isOutDCOrder = -1;

        /// <summary>
        /// 是否出院登记自动停止医嘱
        /// </summary>
        private bool isAutoDcOrder = false;

        /// <summary>
        /// 是否出院登记自动停止医嘱
        /// </summary>
        [Category("出院登记"), Description("是否出院登记自动停止医嘱 还需要参考控制参数HNZY20（出院是否自动停止医嘱）")]
        public bool IsAutoDcOrder
        {
            get
            {
                return isAutoDcOrder;
            }
            set
            {
                isAutoDcOrder = value;
            }
        }

        /// <summary>
        /// 是否医生填写转归，然后护士办理出院
        /// </summary>
        private bool isDoctZG = false;

        /// <summary>
        /// 是否医生填写转归，然后护士办理出院
        /// </summary>
        [Category("出院登记"), Description("是否医生填写转归，然后护士办理出院?")]
        public bool IsDoctZG
        {
            get
            {
                return isDoctZG;
            }
            set
            {
                isDoctZG = value;
            }
        }

        /// <summary>
        /// 出院自动停止长嘱的停止医生
        /// </summary>
        private AotuDcDoct autoDcDoct = AotuDcDoct.ExecutOutDoct;

        /// <summary>
        /// 出院自动停止长嘱的停止医生
        /// </summary>
        [Category("出院登记"), Description("出院自动停止长嘱的停止医生")]
        public AotuDcDoct AutoDcDoct
        {
            get
            {
                return autoDcDoct;
            }
            set
            {
                autoDcDoct = value;
            }
        }

        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;

        #endregion

        #region 属性

        #region 出院登记限制

        /// <summary>
        /// 存在退费申请是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenQuitFeeApplay = CheckState.No;

        /// <summary>
        /// 有退费申请是否允许出院登记
        /// </summary>
        [Category("出院登记"), Description("存在退费申请是否允许做出院登记")]
        public CheckState IsCanOutWhenQuitFeeApplay
        {
            get
            {
                return isCanOutWhenQuitFeeApplay;
            }
            set
            {
                isCanOutWhenQuitFeeApplay = value;
            }
        }

        /// <summary>
        /// 存在退药申请是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenQuitDrugApplay = CheckState.No;

        /// <summary>
        /// 存在退药申请是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("存在退药申请是否允许做出院登记")]
        public CheckState IsCanOutWhenQuitDrugApplay
        {
            get
            {
                return isCanOutWhenQuitDrugApplay;
            }
            set
            {
                isCanOutWhenQuitDrugApplay = value;
            }
        }

        /// <summary>
        /// 存在发药申请是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenDrugApplay = CheckState.Check;

        /// <summary>
        /// 存在发药申请是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("存在发药申请是否允许做出院登记")]
        public CheckState IsCanOutWhenDrugApplay
        {
            get
            {
                return this.isCanOutWhenDrugApplay;
            }
            set
            {
                this.isCanOutWhenDrugApplay = value;
            }
        }

        /// <summary>
        /// 存在未确认项目是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenUnConfirm = CheckState.Check;

        /// <summary>
        /// 存在未确认项目是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("存在未确认项目是否允许做出院登记")]
        public CheckState IsCanOutWhenUnConfirm
        {
            get
            {
                return this.isCanOutWhenUnConfirm;
            }
            set
            {
                this.isCanOutWhenUnConfirm = value;
            }
        }

        /// <summary>
        /// 未开立出院医嘱是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenNoOutOrder = CheckState.No;

        /// <summary>
        /// 未开立出院医嘱是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("未开立出院医嘱是否允许做出院登记")]
        public CheckState IsCanOutWhenNoOutOrder
        {
            get
            {
                return this.isCanOutWhenNoOutOrder;
            }
            set
            {
                this.isCanOutWhenNoOutOrder = value;
            }
        }

        /// <summary>
        /// 未全部停止长嘱是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenNoDcOrder = CheckState.No;

        /// <summary>
        /// 未全部停止长嘱是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("未全部停止长嘱是否允许做出院登记")]
        public CheckState IsCanOutWhenNoDcOrder
        {
            get
            {
                return this.isCanOutWhenNoDcOrder;
            }
            set
            {
                this.isCanOutWhenNoDcOrder = value;
            }
        }

        /// <summary>
        /// 存在未审核医嘱是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenUnConfirmOrder = CheckState.Check;

        /// <summary>
        /// 存在未审核医嘱是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("存在未审核医嘱是否允许做出院登记")]
        public CheckState IsCanOutWhenUnConfirmOrder
        {
            get
            {
                return this.isCanOutWhenUnConfirmOrder;
            }
            set
            {
                this.isCanOutWhenUnConfirmOrder = value;
            }
        }

        /// <summary>
        /// 存在未收费的非药品执行档是否允许做出院登记
        /// </summary>
        private CheckState isCanOutWhenNoFeeExecUndrugOrder = CheckState.Check;

        /// <summary>
        /// 存在未收费的非药品执行档是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("存在未收费的非药品执行档是否允许做出院登记")]
        public CheckState IsCanOutWhenNoFeeExecUndrugOrder
        {
            get
            {
                return this.isCanOutWhenNoFeeExecUndrugOrder;
            }
            set
            {
                isCanOutWhenNoFeeExecUndrugOrder = value;
            }
        }

        /// <summary>
        /// 存在未完成的会诊申请是否允许办理出院登记
        /// </summary>
        private CheckState isCanOutWhenUnConsultation = CheckState.No;

        /// <summary>
        /// 存在未完成的会诊申请是否允许办理出院登记
        /// </summary>
        [Category("出院登记"), Description("存在未完成的会诊申请是否允许办理出院登记")]
        public CheckState IsCanOutWhenUnConsultation
        {
            get
            {
                return this.isCanOutWhenUnConsultation;
            }
            set
            {
                isCanOutWhenUnConsultation = value;
            }
        }

        /// <summary>
        /// 存在未收费的手术申请单是否允许办理出院登记
        /// </summary>
        private CheckState isCanOutWhenUnFeeUOApply = CheckState.Check;

        /// <summary>
        /// 存在未收费的手术申请单是否允许办理出院登记
        /// </summary>
        [Category("出院登记"), Description("存在未收费的手术申请单是否允许办理出院登记")]
        public CheckState IsCanOutWhenUnFeeUOApply
        {
            get
            {
                return this.isCanOutWhenUnFeeUOApply;
            }
            set
            {
                isCanOutWhenUnFeeUOApply = value;
            }
        }

        /// <summary>
        /// 欠费是否允许办理出院手续
        /// </summary>
        private CheckState isCanOutWhenLackFee = CheckState.Check;

        /// <summary>
        /// 欠费是否允许办理出院手续
        /// </summary>
        [Category("出院登记"), Description("欠费是否允许办理出院手续")]
        public CheckState IsCanOutWhenLackFee
        {
            get
            {
                return this.isCanOutWhenLackFee;
            }
            set
            {
                isCanOutWhenLackFee = value;
            }
        }

        #endregion

        /// <summary>
        /// 是否保存后提示打印出院通知单
        /// </summary>
        [Category("出院登记"), Description("是否保存后提示打印出院通知单")]
        public bool IsPrintOutNote
        {
            get
            {
                return isPrintOutNote;
            }
            set
            {
                isPrintOutNote = value;
            }
        }

        /// <summary>
        /// 药品未执行确认是否允许办理出院（要有执行确认流程）
        /// </summary>
        private bool isCanOutWhenExecDrugLimit = true;

        /// <summary>
        /// 药品未执行确认是否允许办理出院（要有执行确认流程）
        /// </summary>
        [Category("出院登记"), Description("药品未执行确认是否允许办理出院（要有执行确认流程）")]
        public bool IsCanOutWhenExecDrugLimit
        {
            get
            {
                return isCanOutWhenExecDrugLimit;
            }
            set
            {
                isCanOutWhenExecDrugLimit = value;
            }
        }

        /// <summary>
        /// 存在一级护理计费时间为出院当天
        /// </summary>
        private CheckState isCanOutWhenYJHLFeeDate = CheckState.Check;

        /// <summary>
        /// 存在发药申请是否允许做出院登记
        /// </summary>
        [Category("出院登记"), Description("存在一级护理计费时间为出院当天")]
        public CheckState IsCanOutWhenYJHLFeeDate
        {
            get
            {
                return this.isCanOutWhenYJHLFeeDate;
            }
            set
            {
                this.isCanOutWhenYJHLFeeDate = value;
            }
        }

        #endregion

        #region 函数

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitControl()
        {
            try
            {
                this.cmbZg.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ZG));
                //[2011-6-2] zhaozf 转归默认选择第一个
                if (this.cmbZg.Items.Count > 0)
                {
                    this.cmbZg.SelectedIndex = 0;
                }

                //出院登记的时间默认为系统时间
                this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();

                this.Oper = manager.GetEmployeeInfo(this.inpatient.Operator.ID);
                if (this.Oper == null)
                {
                    MessageBox.Show("获取人员基本信息出错:" + manager.Err);
                }

                if (IPatientOut == null)
                {
                    IPatientOut = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IPatientOut)) as FS.HISFC.BizProcess.Interface.RADT.IPatientOut;
                }

                if (IPrintInHosNotice == null)
                {
                    IPrintInHosNotice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice)) as FS.HISFC.BizProcess.Interface.IPrintInHosNotice;
                }

                if (isOutDCOrder == -1)
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    isOutDCOrder = controlMgr.GetControlParam<int>("HNZY20", true, 0);
                }

                ////添加医保类型
                //System.Collections.ArrayList alHealthCareType = manager.GetConstantList("HealthCareType");
                //if (alHealthCareType == null)
                //{
                //    MessageBox.Show("查询医保类型出错！" + manager.Err);
                //    return;
                //}
                //else if (alHealthCareType.Count == 0)
                //{
                //    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "警告", "没有维护医保类型，可能导致医保患者办理出院出现问题！\r\n如有疑问请联系系统管理员！", ToolTipIcon.Warning);
                //    //return;
                //}

                //this.cbxHealthCareType.AddItems(alHealthCareType);

                //添加出院诊断
                //this.cmbDiag.AddItems(SOC.HISFC.BizProcess.Cache.Order.GetICD10());

                //添加血型
                this.cmbBloodType.AddItems(FS.HISFC.Models.RADT.BloodTypeEnumService.List());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private FS.HISFC.Models.HealthRecord.Diagnose GetOutDiag(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //ArrayList al = diagMgr.QueryDiagenoseByPateintID(patientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);

            FS.HISFC.Models.HealthRecord.Diagnose outDiag = null;
            //if (al != null && al.Count > 0)
            //{
            //    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            //    {
            //        if (outDiag == null)
            //        {
            //            //主诊断
            //            if (diag.DiagInfo.DiagType.ID == "1")
            //            {
            //                outDiag = diag;
            //            }
            //        }
            //        //出院诊断
            //        if (diag.DiagInfo.DiagType.ID == "14")
            //        {
            //            outDiag = diag;
            //        }
            //    }
            //}
            return outDiag;
        }
        /// <summary>
        /// 设置患者信息到控件
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtPatientNo.Text = patientInfo.PID.PatientNO;		//住院号
            this.txtCard.Text = patientInfo.PID.CardNO;	//门诊卡号
            this.txtPatientNo.Tag = patientInfo.ID;				//住院流水号
            this.txtName.Text = patientInfo.Name;						//姓名
            this.txtSex.Text = patientInfo.Sex.Name;					//性别
            this.cmbBloodType.Tag = myPatientInfo.BloodType.ID;  //血型
            this.txtIndate.Text = patientInfo.PVisit.InTime.ToString();	//入院日期
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;	//科室名称
            this.txtDept.Tag = patientInfo.PVisit.PatientLocation.Dept.ID;	//科室编码

            FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));
            this.txtBalKind.Text = helper.GetName(patientInfo.Pact.PayKind.ID);

            this.cmbDiag.Text = myPatientInfo.MainDiagnose; //出院诊断
            lblDiag.Text = myPatientInfo.MainDiagnose; //出院诊断

            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;	//床号
            this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//剩余预交金
            this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//总金额

            //[2011-6-2] zhaozf 转归默认选择第一个
            if (patientInfo.PVisit.ZG.ID != "0")
            {
                this.cmbZg.Text = "";
                this.cmbZg.Tag = patientInfo.PVisit.ZG.ID;						//转归    
            }

            //add by zhaorong 2013-7-24 医保类型
            //if (!string.IsNullOrEmpty(patientInfo.PVisit.HealthCareType))       //医保类型 0普通  1肿瘤 2恶性肿瘤  3ICU   4内镜   5眼科    6骨科指定手术  7生育保险
            //{
            //    this.cbxHealthCareType.Tag = Convert.ToInt32(patientInfo.PVisit.HealthCareType);
            //}

            this.dtpInDate.Value = patientInfo.PVisit.InTime; //入院时间
            if (this.myPatientInfo.PVisit.PreOutTime >= new DateTime(2000, 1, 1))
            {
                this.dtOutDate.Value = this.myPatientInfo.PVisit.PreOutTime;
            }
            else
            {
                this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();				//出院日期 －－－修改为系统时间
            }

            //出院登记修改时间处理 {28C63B3A-9C64-4010-891D-46F846EA093D}
            string rtn = this.ctlMgr.QueryControlerInfo("ZY0002");
            if (rtn == null || rtn == "-1" || rtn == "")
            {
                rtn = "0";
            }

            if (rtn == "1")//
            {
                ArrayList alShiftDataInfo = this.inpatient.QueryPatientShiftInfoNew(this.myPatientInfo.ID);

                if (alShiftDataInfo == null)
                {
                    MessageBox.Show("获取变更表记录信息出错");
                    return;
                }

                bool isExitInfo = false;

                foreach (FS.HISFC.Models.Invalid.CShiftData myCShiftDate in alShiftDataInfo)
                {
                    if (myCShiftDate.ShitType == "C" || myCShiftDate.ShitType == "O") //有结算召回或有办过出院登记
                    {
                        this.dtOutDate.Enabled = true;
                        isExitInfo = true;
                        break;
                    }
                }
                this.dtOutDate.Enabled = isExitInfo;
            }
            else if (rtn == "2")
            {
                // 不考虑，任何情况都允许修改
                this.dtOutDate.Enabled = true;
            }
            else
            {
                this.dtOutDate.Enabled = false;
            }
            this.panel1.AutoScrollPosition = new Point(0, 0);
            this.lblWarningInfo.Text = "";
            this.CheckOut(false);
        }

        /// <summary>
        /// 从控件获得出院登记信息
        /// </summary>
        private void GetOutInfo()
        {
            myPatientInfo.PVisit.ZG.ID = this.cmbZg.Tag.ToString();
            myPatientInfo.PVisit.ZG.Name = this.cmbZg.Text;
            myPatientInfo.PVisit.PreOutTime = this.dtOutDate.Value;
            myPatientInfo.PVisit.OutTime = this.dtOutDate.Value;
            //获取患者医保类型
            //myPatientInfo.PVisit.HealthCareType = this.cbxHealthCareType.Tag.ToString();
        }

        /// <summary>
        ///清屏
        /// </summary>
        private void ClearPatintInfo()
        {
            //[2011-6-2] zhaozf 转归默认选择第一个
            //this.cmbZg.Text = "";
            //this.cmbZg.Tag = "";
            if (this.cmbZg.Items.Count > 0)
            {
                this.cmbZg.SelectedIndex = 0;
            }
            this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();
        }

        /// <summary>
        /// 刷新患者信息
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void RefreshList(string inPatientNo)
        {
            try
            {
                myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(inPatientNo);

                //如果患者已不在本科,则清空数据
                if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
                {
                    MessageBox.Show("患者已不在本病区,请刷新当前窗口", "提示");
                    myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }

                if (IPatientOut != null)
                {
                    if (IPatientOut.BeforePatientOut(myPatientInfo, FS.FrameWork.Management.Connection.Operator) < 0)
                    {
                        MessageBox.Show(IPatientOut.ErrInfo, "提示");
                    }
                }

                this.SetPatientInfo(myPatientInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region 出院登记收取附材费用

        /// <summary>
        /// 附材接口
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="order"></param>
        /// <param name="alOrders"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int DealSubjobByInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            if (iDealSubjob == null)
            {
                iDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Order), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
            if (iDealSubjob != null)
            {
                //附材带出
                return iDealSubjob.DealSubjob(patientInfo, false, order, alOrders, ref alSubOrders, ref errInfo);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 附材收费项目
        /// </summary>
        FS.HISFC.Models.Fee.Item.Undrug subUndrug = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// 附材收费错误信息
        /// </summary>
        string errInfo = "";

        /// <summary>
        /// 创建费用信息
        /// </summary>
        /// <param name="item"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList CreateFeeItemList(FS.HISFC.Models.Base.Item item, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            subUndrug = new FS.HISFC.Models.Fee.Item.Undrug();
            subUndrug = feeIntegrate.GetItem(item.ID);
            if (subUndrug == null)
            {
                errInfo = "获取非药品失败！" + feeIntegrate.Err;
                return null;
            }
            else if (!subUndrug.IsValid)
            {
                errInfo = "非药品项目" + subUndrug.Name + "已经停用！";
                return null;
            }
            subUndrug.Qty = item.Qty;

            //实体赋值
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            feeItem.IsBaby = patient.IsBaby;
            feeItem.Item = subUndrug;
            feeItem.NoBackQty = subUndrug.Qty;
            feeItem.RecipeNO = feeIntegrate.GetUndrugRecipeNO();
            feeItem.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
            feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            feeItem.FeeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell.ID = patient.PVisit.PatientLocation.NurseCell.ID;
            feeItem.RecipeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            feeItem.ExecOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            if (patient.PVisit.AdmittingDoctor.ID == null || patient.PVisit.AdmittingDoctor.ID == "")
                patient.PVisit.AdmittingDoctor.ID = "日计费";

            feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
            feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
            feeItem.ChargeOper.ID = "日计费";

            decimal price = 0;
            decimal orgPrice = 0;
            if (feeIntegrate.GetPriceForInpatient(patient, feeItem.Item, ref price, ref orgPrice) != -1)
            {
                if (price > 0)
                {
                    feeItem.Item.Price = price;
                    feeItem.Item.DefPrice = orgPrice;
                }
            }

            //划价时间记录实际收取的项目的日期
            feeItem.ChargeOper.OperTime = this.ctlMgr.GetDateTimeFromSysDateTime();
            //feeItem.ChargeOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime();
            feeItem.FeeOper.ID = "日计费";
            feeItem.FeeOper.OperTime = this.ctlMgr.GetDateTimeFromSysDateTime();
            feeItem.SequenceNO = 0;
            feeItem.BalanceNO = 0;
            feeItem.BalanceState = "0";
            feeItem.FT.TotCost = subUndrug.Qty * subUndrug.Price;
            feeItem.FT.OwnCost = subUndrug.Qty * subUndrug.Price;
            feeItem.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("220");//出院补收，召回后不退费
            return feeItem;
        }

        /// <summary>
        /// 是否已收取过附材费用
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="feeDate"></param>
        /// <returns></returns>
        private bool CheckIsFeeed(string inPatientNo, DateTime feeDate)
        {
            string sql = @"select count(*) from com_job_log t
                                where t.job_code='{0}'
                                and t.exec_oper='{1}'
                                and t.exec_date=to_date('{2}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                sql = string.Format(sql, "Sub_Fee", inPatientNo, feeDate.Date);

                string rev = this.ctlMgr.ExecSqlReturnOne(sql);

                if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// 附材收取
        /// </summary>
        /// <returns>1 成功 －1 失败</returns>
        public int SubFee()
        {
            //先判断是否维护接口，没有接口的不处理，直接返回
            if (iDealSubjob == null)
            {
                iDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Order), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
            if (iDealSubjob == null)
            {
                errInfo = "附材收费接口未实现！不自动收取附材费用！";
                this.ctlMgr.WriteErr();
                return 1;
            }

            ArrayList alFeeItems = new ArrayList();

            ArrayList alExecOrders = new ArrayList();

            //避免修改固定收费时间 补收费用，此处判断已经收取的不再收费
            if (this.CheckIsFeeed(this.myPatientInfo.ID, this.ctlMgr.GetDateTimeFromSysDateTime().Date))
            {
                return 1;
            }

            ArrayList alOrders = new ArrayList();
            ArrayList alSubOrders = new ArrayList();

            alExecOrders = this.orderIntegrate.QueryExecOrder(myPatientInfo.ID, "1",
                this.ctlMgr.GetDateTimeFromSysDateTime().Date,
                this.ctlMgr.GetDateTimeFromSysDateTime().AddDays(1).Date);
            if (alExecOrders == null)
            {
                errInfo = orderIntegrate.Err;
                this.ctlMgr.WriteErr();
                return -1;
            }
            else if (alExecOrders.Count == 0)
            {
                return 1;
            }

            string strOrderID = "'";
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrders)
            {
                strOrderID += execOrder.Order.ID + "','";
            }
            strOrderID = strOrderID + "'";

            alOrders = this.orderIntegrate.QueryOrder(myPatientInfo.ID, "1", strOrderID);
            if (alOrders == null)
            {
                errInfo = this.orderIntegrate.Err;
                this.ctlMgr.WriteErr();
                return -1;
            }
            else if (alOrders.Count == 0)
            {
                return 1;
            }

            if (alOrders.Count > 0)
            {
                int rev = this.DealSubjobByInpatient(myPatientInfo, (FS.HISFC.Models.Order.Inpatient.Order)alOrders[0],
                    alOrders, ref alSubOrders, ref errInfo);
                if (rev == -1)
                {
                    return -1;
                }
                else if (rev == 0)
                {
                    errInfo = "附材收费接口未实现！";
                    this.ctlMgr.WriteErr();
                    return 1;
                }
                else
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = null;
                    foreach (FS.HISFC.Models.Base.Item item in alSubOrders)
                    {
                        feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                        feeItem = this.CreateFeeItemList(item, myPatientInfo);
                        if (feeItem == null)
                        {
                            this.ctlMgr.WriteErr();
                            return -1;
                        }
                        alFeeItems.Add(feeItem);
                    }
                }
            }

            #region 按人单个收费
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeItems)
            {
                if (this.feeIntegrate.FeeItem(myPatientInfo, feeItem) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = this.feeIntegrate.Err;
                    this.ctlMgr.WriteErr();
                    return -1;
                }
            }

            try
            {
                string sqlInsert = @"INSERT INTO com_job_log   --定时收费日志
                                          ( job_code,   --任务类型
                                            job_name,   --任务名称
                                            exec_date,   --执行时间
                                            exec_oper )  --执行者
                                     VALUES 
                                          ('{0}' ,   --任务类型
                                           '{1}' ,   --任务名称
                                            to_date('{2}','yyyy-mm-dd hh24:mi:ss'),   --执行时间
                                            '{3}') --执行者";
                sqlInsert = string.Format(sqlInsert, "Sub_Fee", "住院附材收费", this.ctlMgr.GetDateTimeFromSysDateTime().Date, myPatientInfo.ID);
                if (this.ctlMgr.ExecNoQuery(sqlInsert) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = orderIntegrate.Err;
                    this.ctlMgr.WriteErr();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = ex.Message;
                this.ctlMgr.WriteErr();
                return -1;
            }

            //FS.FrameWork.Management.PublicTrans.Commit();

            #endregion

            return 1;
        }

        /// <summary>
        /// 出院登记收取费用
        /// </summary>
        /// <returns></returns>
        private int OutFee()
        {
            if (this.SubFee() == -1)
            {
                return -1;
            }


            return 1;
        }

        #endregion

        /// <summary>
        /// 出院自动停止全部长嘱
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int AutoDcOrder(ref string errInfo)
        {
            //开立出院医嘱的医生
            if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                FS.HISFC.Models.Order.Inpatient.Order orderObj = null;
                int rev = this.IsAddOutOrder(ref orderObj, ref errInfo);
                if (rev == -1)
                {
                    return -1;
                }
                else if (rev == 0)
                {
                    //errInfo = "患者" + myPatientInfo.Name + "还未开立出院医嘱！";
                    return -1;
                }

                if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
                {
                    //errInfo = "患者" + myPatientInfo.Name + "还未开立出院医嘱！";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(myPatientInfo.ID, orderObj.ReciptDoctor, this.Oper, "", "出院登记自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //主任医生
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.myPatientInfo.PVisit.AttendingDirector == null ||
                    string.IsNullOrEmpty(myPatientInfo.PVisit.AttendingDirector.ID))
                {
                    errInfo = "患者" + myPatientInfo.Name + "没有维护主任医师，不能自动停止医嘱！";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(myPatientInfo.ID, myPatientInfo.PVisit.AttendingDirector, this.Oper, "", "出院登记自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //管床医生
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.orderIntegrate.AutoDcOrder(myPatientInfo.ID, myPatientInfo.PVisit.AdmittingDoctor, this.Oper, "", "出院登记自动停止") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            #region 临床路径患者

            if (this.iClinicPath == null)
            {   // {A196A50F-36E2-49b4-B530-DCC38D9D4464}
                //iClinicPath = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IClinicPath))
                //                as FS.HISFC.BizProcess.Interface.Common.IClinicPath;
            }
            if (iClinicPath != null)
            {
                if (iClinicPath.PatientOutByNurse(this.myPatientInfo.ID, this.dtOutDate.Value))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "未退出临床路径不能出院登记!";
                    return 0;
                }
            }
            #endregion

            //如果患者不是当天出院提示
            if (this.dtOutDate.Value.Date < this.myPatientInfo.PVisit.InTime.Date)
            {
                MessageBox.Show("出院日期不能小于入院日期！", "提示");
                return -1;
            }
            else
            {
                if (this.dtOutDate.Value.Date != this.inpatient.GetDateTimeFromSysDateTime().Date)
                {
                    DialogResult dr = MessageBox.Show("该患者的出院日期是： " +
                        this.dtOutDate.Value.ToString("yyyy年MM月dd日") + "  是否继续？\r\n\r\n出院日期比较重要，请填写正确！", "警告",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        this.Err = "";
                        return -1;
                    }
                }
            }
//{218B24CF-17BB-4ddf-8149-D2154A245D11}
            if (this.CheckOut(true) != 1)
            {
                return -1;
            }
             //放在前面防止出院后查询不到婴儿
            ArrayList alBaby = inPatienMgr.QueryBabiesByMother(myPatientInfo.ID);
                for (int i = 0; i < alBaby.Count; i++)
                {
                    FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[i] as FS.HISFC.Models.RADT.PatientInfo;
                    if (this.CheckOutbyPatient(true, babyInfo) != 1)
                    {
                       return -1;
                    }
                }
            //取患者最新的住院主表信息
            myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(this.myPatientInfo.ID);
            if (myPatientInfo == null)
            {
                this.Err = this.inpatient.Err;
                return -1;
            }
            this.Err = "";

            //如果患者已不在本科,则清空数据---当患者转科后,如果本窗口没有关闭,会出现此种情况
            if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            {
                this.Err = "患者已不在本病区,请刷新当前窗口";
                return -1;
            }

            //如果患者在院状态发生变化,则不允许操作
            if (myPatientInfo.PVisit.InState.ID.ToString() != "I")
            {
                this.Err = "患者信息已发生变化,请刷新当前窗口";
                return -1;
            }

            //取出院登记信息
            this.GetOutInfo();

            if (IPatientOut != null)
            {
                if (IPatientOut.BeforePatientOut(this.myPatientInfo.Clone(), this.inpatient.Operator) == -1)
                {
                    MessageBox.Show(IPatientOut.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (isDoctZG
                && this.Oper.EmployeeType.ID.ToString() == "D")
            {
                #region 医生保存转归情况

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {
                    //登记患者血型
                    myPatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                    if (this.inpatient.UpdateBloodType(myPatientInfo.ID, myPatientInfo.BloodType.ID.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新血型失败：" + inpatient.Err);
                        return -1;
                    }

                    //这里修改要注意！！！！！！！！！！！！
                    //医生只是保存转归而已，不要修改患者状态
                    if (this.inpatient.UpdateZGInfo(myPatientInfo, myPatientInfo.PVisit.InState.ID.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("保存信息失败：" + inpatient.Err);
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = ex.Message;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                Err = "保存信息成功，请通知护士办理出院手续！";
                #endregion
            }
            else
            {
                #region 护士办理出院

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {

                    //登记患者血型
                    myPatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                    if (this.inpatient.UpdateBloodType(myPatientInfo.ID, myPatientInfo.BloodType.ID.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("更新血型失败：" + inpatient.Err);
                        return -1;
                    }

                    //更新住院主诊断

                    //if (this.inpatient.UpdatePatientDiag(myPatientInfo.ID, this.cmbDiag.Text) < 0)
                    if (this.inpatient.UpdatePatientDiag(myPatientInfo.ID, this.lblDiag.Text) < 0)
                    {
                        this.Err = "更新患者诊断出错!";
                        return -1;
                    }

                    //出院登记

                    ///增加固定费用的收费
                    if (this.feeIntegrate.SupplementBedFee(myPatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = this.feeIntegrate.Err;
                        return -1;
                    }


                    int i = radt.OutPatient(myPatientInfo);
                    this.Err = radt.Err;
                    if (i == -1)　//失败
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    else if (i == 0)//取消
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "";
                        return 0;
                    }

                    if (this.isAutoDcOrder && isOutDCOrder == 1)
                    {
                        if (this.AutoDcOrder(ref Err) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }

                    if (this.OutFee() == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = errInfo;
                        return -1;
                    }

                    if (IPatientOut != null)
                    {
                        if (IPatientOut.OnPatientOut(this.myPatientInfo, this.inpatient.Operator) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.Err = IPatientOut.ErrInfo;
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = ex.Message;
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                if (IPatientOut != null)
                {
                    if (IPatientOut.AfterPatientOut(this.myPatientInfo, this.inpatient.Operator) == -1)
                    {
                        MessageBox.Show(IPatientOut.ErrInfo, "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                #endregion

                //***************打印出院带药单**************
                if (isPrintOutNote)
                {
                    if (MessageBox.Show("是否打印出院通知单？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.PrintOutNote();
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 是否开立出院医嘱
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int IsAddOutOrder(ref FS.HISFC.Models.Order.Inpatient.Order orderObj, ref string errInfo)
        {
            int rev = this.orderIntegrate.GetOutOrder(myPatientInfo.ID, ref orderObj);
            if (rev == -1)
            {
                errInfo = orderIntegrate.Err;
                return -1;
            }
            else if (rev == 0)
            {
                errInfo = "还未开立出院医嘱！";
                return 0;
            }

            if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
            {
                errInfo = "还未开立出院医嘱！";
                return 0;
            }
            return 1;
        }

        private int CheckOut(bool isSave)
        {
            //整理：把提示统一放到一起

            //需要提示选择的东东
            string checkMessage = "";

            //提示禁止的东东
            string stopMessage = "";

            this.Err = "";

            //实时获取最新的警戒线、余额等信息
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo pInfo = inpatientMgr.QueryPatientInfoByInpatientNO(myPatientInfo.ID);
            if (pInfo != null)
            {
                myPatientInfo = pInfo;
            }

            Classes.Function funMgr = new FS.HISFC.Components.RADT.Classes.Function();

            FS.HISFC.BizProcess.Interface.IPatientShiftValid obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                bool bl = obj.IsPatientShiftValid(pInfo, FS.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
                if (!bl)
                {
                    if (!string.IsNullOrEmpty(stopMessage))
                    {
                        MessageBox.Show(stopMessage);
                    }
                    return -1;
                }
            }


            //注意不要在业务层弹出MessageBox！！！

            /*
             * 一、费用
             *  1、存在退费申请，不允许办理出院登记
             * 二、药品
             *  1、存在退药申请，不允许办理出院登记
             *  2、存在发药申请，提示是否办理出院登记
             * 三、终端确认
             *  1、存在未确认项目，不允许或提示是否允许办理出院登记
             * 
             * 对于其他情况，采用接口本地化实现
             * 1、是否长嘱全停
             * 2、是否开立出院医嘱
             * 3、是否有未审核医嘱
             * 4、判断床位数和护理费的收取是否正确
             * */


            #region 1、存在退费申请，不允许办理出院登记

            if (isCanOutWhenQuitFeeApplay != CheckState.Yes)
            {
                string ReturnApplyItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckReturnApply(pInfo.ID);
                if (ReturnApplyItemInfo != null)
                {
                    string[] item = ReturnApplyItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        tip += item[i] + "\r";
                    }

                    if (isCanOutWhenQuitFeeApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n★存在未确认的退费申请！\r\n" + tip;

                        //if (MessageBox.Show("还有未确认的退费申请，是否继续办理出院？" + ReturnApplyItemInfo, "提示"
                        //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    return 0;
                        //}
                    }
                    else if (isCanOutWhenQuitFeeApplay == CheckState.No)
                    {
                        //存在退费申请不允许做出院登记
                        //this.Err += "还有未确认的退费申请,请确认或取消退费后再做出院登记!\n" + ReturnApplyItemInfo;
                        stopMessage += "\r\n★存在未确认的退费申请！\r\n" + tip;
                        //return -1;
                    }
                }
            }
            #endregion

            #region 2、存在退药申请，提示是否继续

            if (isCanOutWhenQuitDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckQuitDrugApplay(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenQuitDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未审核的退药申请信息！" + "\r" + msg;
                    }
                    else if (this.isCanOutWhenQuitDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★存在未审核的退药申请信息！" + "\r" + msg;
                    }
                }

            }

            #endregion

            #region 3、判断患者是存在未摆药的药品 提示是否继续

            if (isCanOutWhenDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplayWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未摆药的药品项目！\r\n" + msg;

                    }
                    else if (isCanOutWhenDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★存在未摆药的药品项目！\r\n" + msg;
                    }
                }
            }
            #endregion

            #region 4、存在未终端确认项目，提示是否继续

            if (isCanOutWhenUnConfirm != CheckState.Yes)
            {
                string UnConfirmItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(pInfo.ID);
                if (UnConfirmItemInfo != null)
                {
                    string[] item = UnConfirmItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {

                        tip += item[i] + "\r";
                    }

                    if (this.isCanOutWhenUnConfirm == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未确认收费的终端项目！\r\n" + tip;
                        //if (MessageBox.Show("存在以下未确认收费的终端项目，是否继续办理出院？\n" + UnConfirmItemInfo, "提示"
                        //           , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    this.Err = "请联系相关科室确认执行收费！";
                        //    return -1;
                        //}
                    }
                    else if (isCanOutWhenUnConfirm == CheckState.No)
                    {
                        //this.Err += "存在以下未确认收费的终端项目，请联系科室确认收费！\n" + UnConfirmItemInfo;

                        stopMessage += "\r\n\r\n★存在未确认收费的终端项目！\r\n" + tip;
                        //return -1;
                    }
                }
            }

            #endregion

            #region 5、判断是否开立出院医嘱

            if (isCanOutWhenNoOutOrder != CheckState.Yes)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

                int rev = IsAddOutOrder(ref inOrder, ref errInfo);

                if (rev < 0)
                {
                    MessageBox.Show("查询出院医嘱出错!\r\n" + errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return -1;
                }
                else if (rev == 0)
                {
                    if (isCanOutWhenNoOutOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★" + errInfo;
                    }
                    else if (isCanOutWhenNoOutOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★" + errInfo;
                    }
                }
            }

            #endregion

            #region 6、判断长嘱是否全停

            if (isCanOutWhenNoDcOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllLongOrderUnStop(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★长期医嘱没有全部停止\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★长期医嘱没有全部停止\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 7、判断是否有未审核医嘱


            if (isCanOutWhenUnConfirmOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 8、判断是否有未收费的非药品医嘱执行档

            if (isCanOutWhenNoFeeExecUndrugOrder != CheckState.Yes)
            {
                //////=============返回一个字符串 -- unfee|||countWarnings
                string returnStr = FS.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(pInfo.ID);
                if (returnStr != null)
                {
                    string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                    if (Convert.ToInt32(strArray[3]) > 0)
                    {
                        string[] item = strArray[0].Split('\r');
                        string tip = "";
                        for (int i = 0; i < item.Length; i++)
                        {
                            tip += item[i] + "\r";
                        }

                        if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未收费项目！\r\n" + tip;
                            //if (MessageBox.Show(strArray[0] + "\r\n尚未收费，是否继续办理出院！", "提示"
                            //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            //{
                            //    return -1;
                            //}
                        }
                        else if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★存在未收费项目！\r\n" + tip;
                            //this.Err += "以下项目尚未收费，请确认费用收取后再办理出院！\n" + strArray[0];
                            //return 0;
                        }
                    }
                }
            }

            #endregion


            #region 10、存在未收费手术申请单不允许办理出院

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select count(*) from met_ops_apply f
                                                                    where f.clinic_code='{0}'
                                                                    and f.ynvalid='1'
                                                                        and f.execstatus!='4'
                                                                        and f.execstatus!='5'";

                string rev = funMgr.ExecSqlReturnOne(string.Format(sql, pInfo.ID));
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未完成的手术申请单！";
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★存在未完成的手术申请单！";
                        }
                        //System.Windows.Forms.MessageBox.Show("患者【" + pInfo.Name + "】存在未完成的手术，不允许办理出院手续！\r\r\r\n如果有疑问请联系手术室！", "提示", System.Windows.Forms.MessageBoxButtons.OK);
                        //return false;
                    }
                }
                catch
                {
                }
            }

            #endregion


            #region 10、存在未收费手术申请单不允许办理出院

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select  f.operationno,f.apply_date,f.pre_date,a.item_name,f.apply_dpcd from met_ops_apply f,met_ops_operationitem a
                                                        where a.operationno=f.operationno
                                                        and f.clinic_code = a.clinic_code  
                                                        and f.clinic_code='{0}'
                                                        and f.ynvalid='1'
                                                        and a.main_flag = '1'
                                                        and f.execstatus!='4'
                                                        and f.execstatus!='5'
order by a.sort_no";
                try
                {
                    DataSet dsTemp = null;
                    string msg = null;
                    int rev = funMgr.ExecQuery(string.Format(sql, myPatientInfo.ID), ref dsTemp);

                    if (rev > 0)
                    {
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                        {
                            DataTable dtTemp = dsTemp.Tables[0];

                            string strapplyNo = string.Empty;
                            string strApplyDate = string.Empty;
                            string strItemName = string.Empty;
                            string strPreDate = string.Empty;
                            string applyDpcd = string.Empty;
                            ArrayList alDifOper = new ArrayList();
                            foreach (DataRow drRow in dtTemp.Rows)
                            {
                                strapplyNo = drRow["operationno"].ToString();
                                strApplyDate = drRow["apply_date"].ToString();
                                strItemName = drRow["item_name"].ToString();
                                strPreDate = drRow["pre_date"].ToString();
                                applyDpcd = drRow["apply_dpcd"].ToString();

                                if (!alDifOper.Contains(strapplyNo))
                                {
                                    msg = msg + "申请时间：" + strApplyDate + "    " + "预约时间：" + strPreDate + "\r\n" + "申请科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    手术名称：" + strItemName + "\r\n";
                                }
                                else
                                {
                                    msg = msg + "申请科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    手术名称：" + strItemName + "\r\n";
                                }

                                alDifOper.Add(strapplyNo);

                            }

                            if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                            {
                                checkMessage += "\r\n\r\n★存在未完成的手术申请单！\r\n" + msg;
                            }
                            else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                            {
                                stopMessage += "\r\n\r\n★存在未完成的手术申请单！\r\n" + msg;
                            }
                            //System.Windows.Forms.MessageBox.Show("患者【" + myPatientInfo.Name + "】存在未完成的手术，不允许办理出院手续！\r\r\r\n如果有疑问请联系手术室！", "提示", System.Windows.Forms.MessageBoxButtons.OK);
                            //return false;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("存在未完成的手术申请单");
                }
            }

            #endregion

            #region 11、存在未完成会诊，不允许出院

            if (isCanOutWhenUnConsultation != CheckState.Yes)
            {
                string strSQL = @"select --f.state,--0:开立未接收 1:已接收 2:已完成 3:已拒绝
                                   count(1)
                            from CONS_CONSULTATION f
                            where f.state in('0','1')
                            and f.inpatient_id=(
                            select g.id from 
                            bhemr.pt_inpatient_cure g
                            where g.inpatient_code='{0}'
                            )";
                strSQL = string.Format(strSQL, myPatientInfo.ID);

                string errMsg = null;
                if (this.ctlMgr.ExecSqlReturnOne(strSQL) != "0")
                {
                    errMsg = "\r\n\r\n★存在未完成的会诊申请！\n";
                }

                if (isCanOutWhenUnConsultation == CheckState.Check)
                {
                    checkMessage += errMsg;
                }
                else if (isCanOutWhenUnConsultation == CheckState.No)
                {
                    stopMessage += errMsg;
                }
            }
            #endregion

            #region 12、如果启用药品执行确认功能，限制存在未确认的药品项目不允许出院
            if (this.IsCanOutWhenExecDrugLimit)
            {
                string execMsg = FS.HISFC.Components.RADT.Classes.Function.CheckExecDrugWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(execMsg))
                {
                    checkMessage += "\r\n\r\n★存在未执行确认的药品项目！\r\n" + execMsg;
                    stopMessage += "\r\n\r\n★存在未执行确认的药品项目！\r\n" + execMsg;
                }
            }
            #endregion


            #region 欠费判断

            if (isCanOutWhenLackFee != CheckState.Yes)
            {
                try
                {
                    if (pInfo.PVisit.MoneyAlert != 0 && pInfo.FT.LeftCost < pInfo.PVisit.MoneyAlert)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★已经欠费，\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString();
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★已经欠费，\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString();
                        }

                        //if (System.Windows.Forms.MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 【" + pInfo.Name + "】 " + "已经欠费，\r\n\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n是否继续办理出院？", "询问", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        //{
                        //    return false;
                        //}
                    }
                }
                catch
                {
                }
            }
            #endregion

            #region 13、判断是否存在一级护理计费时间为出院当天

            if (isCanOutWhenYJHLFeeDate != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckYJHLFeeDate(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenYJHLFeeDate == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★出院当天存在一级护理的计费项目，请确认！\r\n" + msg;

                    }
                    else if (isCanOutWhenYJHLFeeDate == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★出院当天存在一级护理的计费项目，请确认！\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 需要把以上信息显示在界面上
            if (!isSave)
            {
                this.lblWarningInfo.Text = GetPaientInfo(pInfo) + "\r\n\r\n" + stopMessage;
                return -1;
            }
            #endregion

            #region 9、出院情况不允许为空

            if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            {
                stopMessage += "\r\n\r\n★病案室要求，出院情况不允许为空！\r\n";
                //this.Err = "根据病案室要求，出院情况不允许为空！";
                //return -1;
            }
            #endregion

            FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
            frmMessage.Clear();
            if (!string.IsNullOrEmpty(checkMessage))
            {
                frmMessage.SetPatientInfo(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + myPatientInfo.Name + "】");
                frmMessage.SetTipMessage("存在以下问题未处理,是否继续办理出院？");
                frmMessage.SetMessage(checkMessage);
                frmMessage.SetPerfectWidth();
                if (frmMessage.ShowDialog() == DialogResult.No)
                {
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                frmMessage.SetPatientInfo(myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + myPatientInfo.Name + "】");
                frmMessage.SetTipMessage("存在以下问题未处理,不能继续办理出院！");
                frmMessage.SetMessage(stopMessage);
                frmMessage.SetPerfectWidth();
                frmMessage.HideNoButton();
                frmMessage.ShowDialog();
                return -1;
            }

            if (!string.IsNullOrEmpty(this.Err))
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// 判断患者能否出院包括宝宝和母亲
        /// </summary>
        /// <param name="patientinfo"></param>
        /// <returns></returns>// {218B24CF-17BB-4ddf-8149-D2154A245D11}
        /// 
        private int CheckOutbyPatient(bool isSave, FS.HISFC.Models.RADT.PatientInfo babyinfo)
        {
            ArrayList alBaby = inPatienMgr.QueryBabiesByMother(babyinfo.ID);
            //整理：把提示统一放到一起

            //需要提示选择的东东
            string checkMessage = "";

            //提示禁止的东东
            string stopMessage = "";

            this.Err = "";

            //实时获取最新的警戒线、余额等信息
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo pInfo = inpatientMgr.QueryPatientInfoByInpatientNO(babyinfo.ID);
            if (pInfo != null)
            {
                babyinfo = pInfo;
            }

            Classes.Function funMgr = new FS.HISFC.Components.RADT.Classes.Function();

            FS.HISFC.BizProcess.Interface.IPatientShiftValid obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                bool bl = obj.IsPatientShiftValid(pInfo, FS.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
                if (!bl)
                {
                    if (!string.IsNullOrEmpty(stopMessage))
                    {
                        MessageBox.Show(stopMessage);
                    }
                    return -1;
                }
            }


            //注意不要在业务层弹出MessageBox！！！

            /*
             * 一、费用
             *  1、存在退费申请，不允许办理出院登记
             * 二、药品
             *  1、存在退药申请，不允许办理出院登记
             *  2、存在发药申请，提示是否办理出院登记
             * 三、终端确认
             *  1、存在未确认项目，不允许或提示是否允许办理出院登记
             * 
             * 对于其他情况，采用接口本地化实现
             * 1、是否长嘱全停
             * 2、是否开立出院医嘱
             * 3、是否有未审核医嘱
             * 4、判断床位数和护理费的收取是否正确
             * */


            #region 1、存在退费申请，不允许办理出院登记

            if (isCanOutWhenQuitFeeApplay != CheckState.Yes)
            {
                string ReturnApplyItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckReturnApply(pInfo.ID);
                if (ReturnApplyItemInfo != null)
                {
                    string[] item = ReturnApplyItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        tip += item[i] + "\r";
                    }

                    if (isCanOutWhenQuitFeeApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n★存在未确认的退费申请！\r\n" + tip;

                        //if (MessageBox.Show("还有未确认的退费申请，是否继续办理出院？" + ReturnApplyItemInfo, "提示"
                        //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    return 0;
                        //}
                    }
                    else if (isCanOutWhenQuitFeeApplay == CheckState.No)
                    {
                        //存在退费申请不允许做出院登记
                        //this.Err += "还有未确认的退费申请,请确认或取消退费后再做出院登记!\n" + ReturnApplyItemInfo;
                        stopMessage += "\r\n★存在未确认的退费申请！\r\n" + tip;
                        //return -1;
                    }
                }
            }
            #endregion

            #region 2、存在退药申请，提示是否继续

            if (isCanOutWhenQuitDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckQuitDrugApplay(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenQuitDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未审核的退药申请信息！" + "\r" + msg;
                    }
                    else if (this.isCanOutWhenQuitDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★存在未审核的退药申请信息！" + "\r" + msg;
                    }
                }

            }

            #endregion

            #region 3、判断患者是存在未摆药的药品 提示是否继续

            if (isCanOutWhenDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplayWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未摆药的药品项目！\r\n" + msg;

                    }
                    else if (isCanOutWhenDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★存在未摆药的药品项目！\r\n" + msg;
                    }
                }
            }
            #endregion

            #region 4、存在未终端确认项目，提示是否继续

            if (isCanOutWhenUnConfirm != CheckState.Yes)
            {
                string UnConfirmItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(pInfo.ID);
                if (UnConfirmItemInfo != null)
                {
                    string[] item = UnConfirmItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {

                        tip += item[i] + "\r";
                    }

                    if (this.isCanOutWhenUnConfirm == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未确认收费的终端项目！\r\n" + tip;
                        //if (MessageBox.Show("存在以下未确认收费的终端项目，是否继续办理出院？\n" + UnConfirmItemInfo, "提示"
                        //           , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    this.Err = "请联系相关科室确认执行收费！";
                        //    return -1;
                        //}
                    }
                    else if (isCanOutWhenUnConfirm == CheckState.No)
                    {
                        //this.Err += "存在以下未确认收费的终端项目，请联系科室确认收费！\n" + UnConfirmItemInfo;

                        stopMessage += "\r\n\r\n★存在未确认收费的终端项目！\r\n" + tip;
                        //return -1;
                    }
                }
            }

            #endregion

            #region 5、判断是否开立出院医嘱

            if (isCanOutWhenNoOutOrder != CheckState.Yes)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

                int rev = IsAddOutOrder(ref inOrder, ref errInfo);

                if (rev < 0)
                {
                    MessageBox.Show("查询出院医嘱出错!\r\n" + errInfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return -1;
                }
                else if (rev == 0)
                {
                    if (isCanOutWhenNoOutOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★" + errInfo;
                    }
                    else if (isCanOutWhenNoOutOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★" + errInfo;
                    }
                }
            }

            #endregion

            #region 6、判断长嘱是否全停

            if (isCanOutWhenNoDcOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllLongOrderUnStop(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★长期医嘱没有全部停止\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★长期医嘱没有全部停止\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 7、判断是否有未审核医嘱


            if (isCanOutWhenUnConfirmOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n★存在未审核医嘱\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 8、判断是否有未收费的非药品医嘱执行档

            if (isCanOutWhenNoFeeExecUndrugOrder != CheckState.Yes)
            {
                //////=============返回一个字符串 -- unfee|||countWarnings
                string returnStr = FS.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(pInfo.ID);
                if (returnStr != null)
                {
                    string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                    if (Convert.ToInt32(strArray[3]) > 0)
                    {
                        string[] item = strArray[0].Split('\r');
                        string tip = "";
                        for (int i = 0; i < item.Length; i++)
                        {
                            tip += item[i] + "\r";
                        }

                        if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未收费项目！\r\n" + tip;
                            //if (MessageBox.Show(strArray[0] + "\r\n尚未收费，是否继续办理出院！", "提示"
                            //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            //{
                            //    return -1;
                            //}
                        }
                        else if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★存在未收费项目！\r\n" + tip;
                            //this.Err += "以下项目尚未收费，请确认费用收取后再办理出院！\n" + strArray[0];
                            //return 0;
                        }
                    }
                }
            }

            #endregion


            #region 10、存在未收费手术申请单不允许办理出院

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select count(*) from met_ops_apply f
                                                                    where f.clinic_code='{0}'
                                                                    and f.ynvalid='1'
                                                                        and f.execstatus!='4'
                                                                        and f.execstatus!='5'";

                string rev = funMgr.ExecSqlReturnOne(string.Format(sql, pInfo.ID));
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★存在未完成的手术申请单！";
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★存在未完成的手术申请单！";
                        }
                        //System.Windows.Forms.MessageBox.Show("患者【" + pInfo.Name + "】存在未完成的手术，不允许办理出院手续！\r\r\r\n如果有疑问请联系手术室！", "提示", System.Windows.Forms.MessageBoxButtons.OK);
                        //return false;
                    }
                }
                catch
                {
                }
            }

            #endregion


            #region 10、存在未收费手术申请单不允许办理出院

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select  f.operationno,f.apply_date,f.pre_date,a.item_name,f.apply_dpcd from met_ops_apply f,met_ops_operationitem a
                                                        where a.operationno=f.operationno
                                                        and f.clinic_code = a.clinic_code  
                                                        and f.clinic_code='{0}'
                                                        and f.ynvalid='1'
                                                        and a.main_flag = '1'
                                                        and f.execstatus!='4'
                                                        and f.execstatus!='5'
order by a.sort_no";
                try
                {
                    DataSet dsTemp = null;
                    string msg = null;
                    int rev = funMgr.ExecQuery(string.Format(sql, babyinfo.ID), ref dsTemp);

                    if (rev > 0)
                    {
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                        {
                            DataTable dtTemp = dsTemp.Tables[0];

                            string strapplyNo = string.Empty;
                            string strApplyDate = string.Empty;
                            string strItemName = string.Empty;
                            string strPreDate = string.Empty;
                            string applyDpcd = string.Empty;
                            ArrayList alDifOper = new ArrayList();
                            foreach (DataRow drRow in dtTemp.Rows)
                            {
                                strapplyNo = drRow["operationno"].ToString();
                                strApplyDate = drRow["apply_date"].ToString();
                                strItemName = drRow["item_name"].ToString();
                                strPreDate = drRow["pre_date"].ToString();
                                applyDpcd = drRow["apply_dpcd"].ToString();

                                if (!alDifOper.Contains(strapplyNo))
                                {
                                    msg = msg + "申请时间：" + strApplyDate + "    " + "预约时间：" + strPreDate + "\r\n" + "申请科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    手术名称：" + strItemName + "\r\n";
                                }
                                else
                                {
                                    msg = msg + "申请科室：" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    手术名称：" + strItemName + "\r\n";
                                }

                                alDifOper.Add(strapplyNo);

                            }

                            if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                            {
                                checkMessage += "\r\n\r\n★存在未完成的手术申请单！\r\n" + msg;
                            }
                            else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                            {
                                stopMessage += "\r\n\r\n★存在未完成的手术申请单！\r\n" + msg;
                            }
                            //System.Windows.Forms.MessageBox.Show("患者【" + babyinfo.Name + "】存在未完成的手术，不允许办理出院手续！\r\r\r\n如果有疑问请联系手术室！", "提示", System.Windows.Forms.MessageBoxButtons.OK);
                            //return false;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("存在未完成的手术申请单");
                }
            }

            #endregion

            #region 11、存在未完成会诊，不允许出院

            if (isCanOutWhenUnConsultation != CheckState.Yes)
            {
                string strSQL = @"select --f.state,--0:开立未接收 1:已接收 2:已完成 3:已拒绝
                                   count(1)
                            from CONS_CONSULTATION f
                            where f.state in('0','1')
                            and f.inpatient_id=(
                            select g.id from 
                            bhemr.pt_inpatient_cure g
                            where g.inpatient_code='{0}'
                            )";
                strSQL = string.Format(strSQL, babyinfo.ID);

                string errMsg = null;
                if (this.ctlMgr.ExecSqlReturnOne(strSQL) != "0")
                {
                    errMsg = "\r\n\r\n★存在未完成的会诊申请！\n";
                }

                if (isCanOutWhenUnConsultation == CheckState.Check)
                {
                    checkMessage += errMsg;
                }
                else if (isCanOutWhenUnConsultation == CheckState.No)
                {
                    stopMessage += errMsg;
                }
            }
            #endregion

            #region 12、如果启用药品执行确认功能，限制存在未确认的药品项目不允许出院
            if (this.IsCanOutWhenExecDrugLimit)
            {
                string execMsg = FS.HISFC.Components.RADT.Classes.Function.CheckExecDrugWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(execMsg))
                {
                    checkMessage += "\r\n\r\n★存在未执行确认的药品项目！\r\n" + execMsg;
                    stopMessage += "\r\n\r\n★存在未执行确认的药品项目！\r\n" + execMsg;
                }
            }
            #endregion


            #region 欠费判断

            if (isCanOutWhenLackFee != CheckState.Yes)
            {
                try
                {
                    if (pInfo.PVisit.MoneyAlert != 0 && pInfo.FT.LeftCost < pInfo.PVisit.MoneyAlert)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n★已经欠费，\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString();
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n★已经欠费，\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString();
                        }

                        //if (System.Windows.Forms.MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 【" + pInfo.Name + "】 " + "已经欠费，\r\n\r\n余额： " + pInfo.FT.LeftCost.ToString() + "\r\n\r\n警戒线： " + pInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n是否继续办理出院？", "询问", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        //{
                        //    return false;
                        //}
                    }
                }
                catch
                {
                }
            }
            #endregion

            #region 需要把以上信息显示在界面上
            if (!isSave)
            {
                this.lblWarningInfo.Text = GetPaientInfo(pInfo) + "\r\n\r\n" + stopMessage;
                return -1;
            }
            #endregion

            #region 9、出院情况不允许为空

            if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            {
                stopMessage += "\r\n\r\n★病案室要求，出院情况不允许为空！\r\n";
                //this.Err = "根据病案室要求，出院情况不允许为空！";
                //return -1;
            }
            #endregion

            FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
            frmMessage.Clear();
            if (!string.IsNullOrEmpty(checkMessage))
            {
                frmMessage.SetPatientInfo(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + babyinfo.Name + "】");
                frmMessage.SetTipMessage("存在以下问题未处理,是否继续办理出院？");
                frmMessage.SetMessage(checkMessage);
                frmMessage.SetPerfectWidth();
                if (frmMessage.ShowDialog() == DialogResult.No)
                {
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                frmMessage.SetPatientInfo(babyinfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "床 患者【" + babyinfo.Name + "】");
                frmMessage.SetTipMessage("存在以下问题未处理,不能继续办理出院！");
                frmMessage.SetMessage(stopMessage);
                frmMessage.SetPerfectWidth();
                frmMessage.HideNoButton();
                frmMessage.ShowDialog();
                return -1;
            }

            if (!string.IsNullOrEmpty(this.Err))
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }



        private string GetPaientInfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            return pInfo.PID.PatientNO//住院号
                        + "  " + pInfo.Name //姓名
                        + "  " + pInfo.Sex.Name //性别
                        + "  " + this.inpatient.GetAge(pInfo.Birthday)//年龄
                        + "  " + pInfo.Pact.Name//合同单位
                        + "  住院日期：" + pInfo.PVisit.InTime.ToString("yyyy.MM.dd")//住院日期
                        ;
        }

        /// <summary>
        /// 打印出院通知单
        /// </summary>
        private void PrintOutNote()
        {
            if (myPatientInfo == null)
            {
                return;
            }
            this.GetOutInfo();

            if (IPrintInHosNotice != null)
            {
                IPrintInHosNotice.SetValue(myPatientInfo);
                if (((FS.HISFC.Models.Base.Employee)this.inpatient.Operator).IsManager)
                {
                    IPrintInHosNotice.PrintView();
                }
                else
                {
                    IPrintInHosNotice.Print();
                }
            }
            else
            {
                ucOutPrint print = new ucOutPrint();
                print.SetPatientInfo(myPatientInfo);
                //print.NameFlag = this.cmbOutpatientAim.Tag.ToString().Trim();
                //print.PrintPreview();
                print.Print();
            }
        }

        /// <summary>
        /// 请求满意度调查的his service接口
        /// {1E0B1D27-05E1-43a6-B3D1-C33BC970C1DD}
        /// </summary>
        /// <returns></returns>
        public void postHisSatisfiction(string patientid, string departmentid)
        {
            string url = @"http://192.168.34.9:8020/IbornMobileService.asmx";
            //string url = @"http://localhost:8080/IbornMobileService.asmx";
            #region 参数封装形式：req

            //<?xml version="1.0" encoding="UTF-8"?>
            //<req>　
            //  <patientId>his卡号</patientId>           是   his卡号
            //  <departmentId>科室部门id</departmentId>   是   科室部门id
            //  <patientName>姓名</patientName> 否   姓名
            //  <hospitalId>医院id</hospitalId> 否 医院编码
            //  <questionnaireId>问卷id</questionnaireId> 否 问卷id
            //</req>
            #endregion

            string requestStr = @"<?xml version='1.0' encoding='UTF-8'?>
                            <req>
                               <patientId>{0}</patientId>
                               <patientName>{1}</patientName> 
                               <healthCardNo>{2}</healthCardNo>
                               <departmentId>{3}</departmentId>
                               <questionnaireId>{4}</questionnaireId> 
                               <hospitalId>{5}</hospitalId>
                            </req>";

            string relay = string.Format(requestStr, patientid, "", patientid, departmentid/*部门id*/, "", "");

            //尽最大力度的发送短信，但不保证可传达
            string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "getCRMWechatApi", new string[] { relay }) as string;
        }

        #endregion

        #region 事件

        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//成功
            {
                MessageBox.Show(Err);

                string cardNo = myPatientInfo.PID.CardNO;
                //his service请求，满意度问卷
                //判断是否推送
                //{C2C33FC9-E95F-4484-B01B-E3148809FABB}
                if(this.isSendWechat)
                    this.postHisSatisfiction(cardNo, "住院收费");

                base.OnRefreshTree();
                ((Control)sender).Enabled = true;
                return;
            }
            else
            {
                if (Err != "")
                {
                    MessageBox.Show(Err, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            ((Control)sender).Enabled = true;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.strInpatientNo = (neuObject as FS.FrameWork.Models.NeuObject).ID;
            if (this.strInpatientNo != null || this.strInpatientNo != "")
            {
                try
                {
                    RefreshList(strInpatientNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                }
            }
            return 0;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        /// <summary>
        /// 出院通知单打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintOutNote();
            MessageBox.Show("已打印出院通知单，请及时办理出院登记！");
        }

        #endregion

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion

    }

    /// <summary>
    /// 选择状态
    /// </summary>
    public enum CheckState
    {
        /// <summary>
        /// 不允许
        /// </summary>
        No,

        /// <summary>
        /// 允许
        /// </summary>
        Yes,

        /// <summary>
        /// 提示选择
        /// </summary>
        Check
    }


    /// <summary>
    /// 出院自动停止医嘱的停止医生
    /// </summary>
    public enum AotuDcDoct
    {
        /// <summary>
        /// 住院医师（管床）
        /// </summary>
        InpatientDoct,

        /// <summary>
        /// 主任医生
        /// </summary>
        DirectorDoct,

        /// <summary>
        /// 开立出院医嘱的医生
        /// </summary>
        ExecutOutDoct
    }

}
