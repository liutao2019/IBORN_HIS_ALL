using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Neusoft.HISFC.Models.RADT;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [功能描述: 护士站病房管理切换控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2006-11-30]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucRADT : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ucRADT()
        {
            InitializeComponent();

            this.wapType = 3;
        }

        #region 函数
        protected TreeView tv = null;
        protected TreeNode node = null;
        protected Neusoft.HISFC.Models.RADT.PatientInfo patient = null;

        /// <summary>
        ///待分配的床号 
        /// </summary>
        protected string arrangeBedNO;

        /// <summary>
        /// 患者业务类{81987883-BFB0-42f7-8B99-CF44CA44BDDA}
        /// </summary>
        Neusoft.HISFC.BizLogic.RADT.InPatient inpatientManager = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            try
            {
                tv = sender as TreeView;
            }
            catch { }
            //this.neuTabControl1.TabPages.Clear();
            //this.neuTabControl1.TabPages.Add(this.tbBedView);//默认显示病床
            //ucBedListView uc = new ucBedListView();
            //uc.WapType = wapType;
            //uc.arrangeBed += new ucBedListView.ArrangeBed(uc_arrangeBed);
            //uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
            //uc.Dock = DockStyle.Fill;
            //uc.Visible = true;
            //Neusoft.FrameWork.WinForms.Forms.IControlable ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
            //if (ic != null)
            //{
            //    ic.Init(this.tv, null, null);
            //    ic.SetValue(patient, this.tv.Nodes[0]);
            //    ic.RefreshTree += new EventHandler(ic_RefreshTree);
            //    ic.SendParamToControl += new Neusoft.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
            //    ic.StatusBarInfo += new Neusoft.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

            //}
            //this.tbBedView.Controls.Add(uc);

            return base.OnInit(sender, neuObject, param);
        }

        #region 私有变量
        private bool sexReadOnly = true;
        private bool birthdayReadOnly = true;
        private bool relationReadOnly = true;
        private bool heightReadOnly = true;
        private bool weightReadOnly = true;
        private bool iDReadOnly = true;
        private bool professionReadOnly = true;
        private bool marryReadOnly = true;
        private bool homeAddrReadOnly = true;
        private bool homeTelReadOnly = true;
        private bool workReadOnly = true;
        private bool linkManReadOnly = true;
        private bool kinAddressReadOnly = true;
        private bool linkTelReadOnly = true;
        private bool memoReadOnly = true;
        private bool tpLeaveVisible = false;

        /// <summary>
        /// 婴儿登记管理是否显示
        /// </summary>
        private bool tpNurseVisible = false;
        /// 终止妊娠登记是否显示
        /// </summary>
        private bool isStopPregnancyVisible = false;

        /// <summary>
        /// 转病区管理是否显示
        /// </summary>
        private bool tpShiftNurseCellVisible = false;

        /// <summary>
        /// 允许婴儿登记的科室
        /// </summary>
        private Neusoft.FrameWork.Public.ObjectHelper babyDeptHelper = null;

        #endregion

        #region  属性

        #region 接诊设置

        /// <summary>
        /// 接诊时是否自动加载收治医师或转科前医师
        /// </summary>
        private bool isAddDoctWhenArrive = true;

        /// <summary>
        /// 接诊时是否自动加载收治医师或转科前医师
        /// </summary>
        [Category("接诊设置"), Description("接诊时是否自动加载收治医师或转科前医师，否则为空")]
        public bool IsAddDoctWhenArrive
        {
            get
            {
                return isAddDoctWhenArrive;
            }
            set
            {
                isAddDoctWhenArrive = value;
            }
        }

        /// <summary>
        /// 是否默认加载全院医生
        /// </summary>
        private bool isAddAllDoct = true;

        /// <summary>
        /// 是否默认加载全院医生
        /// </summary>
        [Category("接诊设置"), Description("是否默认加载全院医生")]
        public bool IsAddAllDoct
        {
            get
            {
                return isAddAllDoct;
            }
            set
            {
                isAddAllDoct = value;
            }
        }

        /// <summary>
        /// 是否默认加载全院护士
        /// </summary>
        private bool isAddAllNurse = true;

        /// <summary>
        /// 是否默认加载全院护士
        /// </summary>
        [Category("接诊设置"), Description("是否默认加载全院护士")]
        public bool IsAddAllNurse
        {
            get
            {
                return isAddAllNurse;
            }
            set
            {
                isAddAllNurse = value;
            }
        }

        #endregion

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
        private CheckState isCanOutWhenDrugApplay = CheckState.No;

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
        private CheckState isCanOutWhenUnConfirm = CheckState.No;

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
        /// 存在未收费的手术申请单是否允许办理出院登记
        /// </summary>
        private CheckState isCanOutWhenUnFeeUOApply = CheckState.Yes;

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
        private CheckState isCanOutWhenLackFee = CheckState.Yes;

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

        #endregion

        /// <summary>
        /// 性别是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("性别是否允许修改")]
        public bool SexReadOnly
        {
            get
            {
                return sexReadOnly;
            }
            set
            {
                sexReadOnly = value;
            }
        }
        /// <summary>
        /// 生日是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("生日是否允许修改")]
        public bool BirthdayReadOnly
        {
            get
            {
                return birthdayReadOnly;
            }
            set
            {
                birthdayReadOnly = value;
            }
        }
        /// <summary>
        /// 身高是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("身高是否允许修改")]
        public bool HeightReadOnly
        {
            get
            {
                return heightReadOnly;
            }
            set
            {
                heightReadOnly = value;
            }
        }
        /// <summary>
        /// 体重是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("体重是否允许修改")]
        public bool WeightReadOnly
        {
            get
            {
                return weightReadOnly;
            }
            set
            {
                weightReadOnly = value;
            }
        }
        /// <summary>
        /// 身份证号是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("身份证号是否允许修改")]
        public bool IDReadOnly
        {
            get
            {
                return iDReadOnly;
            }
            set
            {
                iDReadOnly = value;
            }
        }
        /// <summary>
        /// 职业是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("职业是否允许修改")]
        public bool ProfessionReadOnly
        {
            get
            {
                return professionReadOnly;
            }
            set
            {
                professionReadOnly = value;
            }
        }
        /// <summary>
        /// 职业是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("婚姻是否允许修改")]
        public bool MarryReadOnly
        {
            get
            {
                return marryReadOnly;
            }
            set
            {
                marryReadOnly = value;
            }
        }
        /// <summary>
        /// 家庭住址是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("家庭住址是否允许修改")]
        public bool HomeAddrReadOnly
        {
            get
            {
                return homeAddrReadOnly;
            }
            set
            {
                homeAddrReadOnly = value;
            }
        }
        /// <summary>
        /// 家庭电话是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("家庭电话是否允许修改")]
        public bool HomeTelReadOnly
        {
            get
            {
                return homeTelReadOnly;
            }
            set
            {
                homeTelReadOnly = value;
            }
        }
        /// <summary>
        /// 工作单位是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("工作单位是否允许修改")]
        public bool WorkReadOnly
        {
            get
            {
                return workReadOnly;
            }
            set
            {
                workReadOnly = value;
            }
        }
        /// <summary>
        /// 联系人是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("联系人是否允许修改")]
        public bool LinkManReadOnly
        {
            get
            {
                return linkManReadOnly;
            }
            set
            {
                linkManReadOnly = value;
            }
        }
        /// <summary>
        /// 联系人地址是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("联系人地址是否允许修改")]
        public bool KinAddressReadOnly
        {
            get
            {
                return kinAddressReadOnly;
            }
            set
            {
                kinAddressReadOnly = value;
            }
        }
        /// <summary>
        /// 联系人电话是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("联系人电话是否允许修改")]
        public bool LinkTelReadOnly
        {
            get
            {
                return linkTelReadOnly;
            }
            set
            {
                linkTelReadOnly = value;
            }
        }
        /// <summary>
        /// 联系人关系是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("联系人关系是否允许修改")]
        public bool RelationReadOnly
        {
            get
            {
                return relationReadOnly;
            }
            set
            {
                relationReadOnly = value;
            }
        }
        /// <summary>
        /// 特注处理是否允许修改
        /// </summary>
        [Category("患者基本信息"), Description("特注处理是否允许修改")]
        public bool MemoReadOnly
        {
            get
            {
                return memoReadOnly;
            }
            set
            {
                memoReadOnly = value;
            }
        }

        #region 接诊设置

        /// <summary>
        /// 住院医师是否必填
        /// </summary>
        bool isAdmittingDoctorNeed = false;

        /// <summary>
        /// 住院医师是否必填
        /// </summary>
        [Category("接诊设置"), Description("住院医师是否必填")]   
        public bool IsAdmittingDoctorNeed
        {
            get
            {
                return isAdmittingDoctorNeed;
            }
            set
            {
                isAdmittingDoctorNeed = value;
            }
        }

        /// <summary>
        /// 主治医师是否必填
        /// </summary>
        bool isAttendingDoctorNeed = false;

        /// <summary>
        /// 主治医师是否必填
        /// </summary>
        [Category("接诊设置"), Description("主治医师是否必填")]   
        public bool IsAttendingDoctorNeed
        {
            get
            {
                return isAttendingDoctorNeed;
            }
            set
            {
                isAttendingDoctorNeed = value;
            }
        }

        /// <summary>
        /// 主任医师是否必填
        /// </summary>
        bool isConsultingDoctorNeed = false;

        /// <summary>
        /// 主任医师是否必填
        /// </summary>      
        [Category("接诊设置"), Description("主任医师是否必填")]     
        public bool IsConsultingDoctorNeed
        {
            get
            {
                return isConsultingDoctorNeed;
            }
            set
            {
                isConsultingDoctorNeed = value;
            }
        }

        /// <summary>
        /// 科主任是否必填
        /// </summary>
        bool isDirectorDoctorNeed = false;

        /// <summary>
        /// 科主任是否必填
        /// </summary>       
        [Category("接诊设置"), Description("科主任是否必填")]    
        public bool IsDirectorDoctorNeed
        {
            get
            {
                return isDirectorDoctorNeed;
            }
            set
            {
                isDirectorDoctorNeed = value;
            }
        }

        /// <summary>
        /// 责任护士是否必填
        /// </summary>
        bool isAdmittingNurseNeed = false;

        /// <summary>
        /// 责任护士是否必填
        /// </summary>    
        [Category("接诊设置"), Description("责任护士是否必填")]    
        public bool IsAdmittingNurseNeed
        {
            get
            {
                return isAdmittingNurseNeed;
            }
            set
            {
                isAdmittingNurseNeed = value;
            }
        }

        #endregion

        /// <summary>
        /// 是否允许修改床位等级
        /// </summary>
        private bool isAllowModifyBedGrad = false;

        /// <summary>
        /// 是否允许修改床位等级
        /// </summary>
        [Category("接诊设置"), Description("是否允许修改床位等级")]
        public bool IsAllowModifyBedGrad
        {
            get
            {
                return isAllowModifyBedGrad;
            }
            set
            {
                isAllowModifyBedGrad = value;
            }
        }

        [Category("tab页设置"), Description("tpLeave请假管理是否显示")]
        public bool 请假管理是否显示
        {
            get
            {
                return tpLeaveVisible;
            }
            set
            {
                tpLeaveVisible = value;
            }
        }

        [Category("tab页设置"), Description("tpNurse婴儿登记管理是否显示")]
        public bool 婴儿登记是否显示
        {
            get
            {
                return tpNurseVisible;
            }
            set
            {
                tpNurseVisible = value;
            }
        }

        /// 终止妊娠登记是否显示
        /// </summary>
        [Category("tab页设置"), Description("tpStopPregnancy终止妊娠登记是否显示")]
        public bool IsStopPregnancyVisible
        {
            get { return isStopPregnancyVisible; }
            set { isStopPregnancyVisible = value; }
        }

        #region {9A2D53D3-25BE-4630-A547-A121C71FB1C5}

        [Category("tab页设置"), Description("转病区管理是否显示")]
        public bool 转病区是否显示
        {
            get
            {
                return this.tpShiftNurseCellVisible;
            }
            set
            {
                this.tpShiftNurseCellVisible = value;
            }
        }

        #endregion

        #region 床位维护设置

        /// <summary>
        /// 已经存在的床，是否允许修改床号(只针对空床）
        /// </summary>
        private bool isAllowEditBedNo = false;

        /// <summary>
        /// 已经存在的床，是否允许修改床号
        /// </summary>
        [Category("床位维护设置"), Description("已经存在的床，是否允许修改床号(只针对空床）")]
        public bool IsAllowEditBedNo
        {
            get
            {
                return isAllowEditBedNo;
            }
            set
            {
                isAllowEditBedNo = value;
            }
        }


        /// <summary>
        /// 是否允许添加所有床位等级
        /// </summary>
        private bool isAllowAddAllBedLevel = false;

        /// <summary>
        /// 是否允许添加所有床位等级
        /// </summary>
        [Category("床位维护设置"), Description("是否允许添加所有床位等级")]
        public bool IsAllowAddAllBedLevel
        {
            get
            {
                return isAllowAddAllBedLevel;
            }
            set
            {
                isAllowAddAllBedLevel = value;
            }
        }

        /// <summary>
        /// 是否允许修改床位等级
        /// </summary>
        private bool isAllowEditBedLevel = false;

        /// <summary>
        /// 是否允许修改床位等级
        /// </summary>
        [Category("床位维护设置"), Description("是否允许修改床位等级")]
        public bool IsAllowEditBedLevel
        {
            get
            {
                return isAllowEditBedLevel;
            }
            set
            {
                isAllowEditBedLevel = value;
            }
        }

        /// <summary>
        /// 是否允许添加所有床位编制
        /// </summary>
        private bool isAllBedWave = false;

        /// <summary>
        /// 是否允许添加所有床位编制
        /// </summary>
        [Category("床位维护设置"), Description("是否可以添加所有床位编制病床")]
        public bool IsAllBedWave
        {
            get
            {
                return this.isAllBedWave;
            }
            set
            {
                this.isAllBedWave = value;
            }
        }

        /// <summary>
        /// 允许包床的类型：0 全都不允许包床；1 全都允许包床；2 医保、公费不允许包床；默认1
        /// </summary>
        private int wapType = 1;

        /// <summary>
        /// 允许包床的类型：0 全都不允许包床；1 全都允许包床；2 医保、公费不允许包床；默认1
        /// </summary>
        [Category("床位维护设置"), Description("允许包床的类型：0 全都不允许包床；1 全都允许包床；2 医保、公费不允许包床；默认1")]
        public int WapType
        {
            get
            {
                return wapType;
            }
            set
            {
                wapType = value;
            }
        }

        /// <summary>
        /// 是否只允许空床可接收
        /// </summary>
        private bool isOnlyEmptyBedCanReceive = false;

        /// <summary>
        /// 是否只允许空床可接收
        /// </summary>
        [Category("床位维护设置"), Description("是否只允许空床可接收")]
        public bool IsOnlyEmptyBedCanReceive
        {
            set { this.isOnlyEmptyBedCanReceive = value; }
            get { return this.isOnlyEmptyBedCanReceive; }
        }
        #endregion

        /// <summary>
        /// 是否出院登记保存后提示打印出院通知单
        /// </summary>
        private bool isPrintOutNote = false;

        /// <summary>
        /// 是否出院登记自动停止医嘱
        /// </summary>
        private bool isAutoDcOrder = false;

        /// <summary>
        /// 是否出院登记自动停止医嘱
        /// </summary>
        [Category("出院设置"), Description("是否出院登记自动停止医嘱")]
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
        /// 是否转科自动停止医嘱
        /// </summary>
        private bool isShiftAutoDcOrder = false;

        /// <summary>
        /// 是否转科自动停止医嘱
        /// </summary>
        [Category("出院设置"), Description("是否转科自动停止医嘱")]
        public bool IsShiftAutoDcOrder
        {
            get
            {
                return isShiftAutoDcOrder;
            }
            set
            {
                isShiftAutoDcOrder = value;
            }
        }

        /// <summary>
        /// 是否使用转科自动停止医嘱功能
        /// </summary>
        private bool isUseShiftAutoDcOrder = false;

        /// <summary>
        /// 是否使用转科自动停止医嘱功能
        /// </summary>
        [Category("出院设置"), Description("是否使用转科自动停止医嘱功能，使用后必须开立转科医嘱才能转科！")]
        public bool IsUseShiftAutoDcOrder
        {
            get
            {
                return isUseShiftAutoDcOrder;
            }
            set
            {
                isUseShiftAutoDcOrder = value;
            }
        }

        /// <summary>
        /// 是否出院登记保存后提示打印出院通知单
        /// </summary>
        [Category("出院设置"), Description("是否出院登记保存后提示打印出院通知单")]
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
        [Category("出院设置"), Description("出院自动停止长嘱的停止医生（InpatientDoct：住院医师、DirectorDoct：主任医师、ExecutOutDoct：开立出院医嘱的医生）")]
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

        /// <summary>
        /// 是否允许隔日召回
        /// </summary>
        private bool isAllowCallBackOtherDay = false;

        /// <summary>
        /// 是否允许隔日召回
        /// </summary>
        [Category("出院设置"), Description("是否允许隔日召回")]
        public bool IsAllowCallBackOtherDay
        {
            get
            {
                return isAllowCallBackOtherDay;
            }
            set
            {
                isAllowCallBackOtherDay = value;
            }
        }

        #region {F4EB69FA-F43E-4bdc-AC85-B53377604818}
        private bool isModifyBirthday = true;
        /// <summary>
        /// 是否可以修改婴儿出生时间
        /// </summary>
        [Category("婴儿登记设置"), Description("是否可以修改婴儿出生时间"), DefaultValue(true)]
        public bool IsModifyBirthday
        {
            get
            {
                return this.isModifyBirthday;
            }
            set
            {
                this.isModifyBirthday = value;
            }
        }
        #endregion

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            this.neuTabControl1.TabPages.Clear();
            this.neuTabControl1.TabPages.Add(this.tbBedView);//默认显示病床
            ucBedListView uc = new ucBedListView();
            uc.IsOnlyEmptyBedCanReceive = isOnlyEmptyBedCanReceive;
            uc.WapType = wapType;
            uc.arrangeBed += new ucBedListView.ArrangeBed(uc_arrangeBed);
            uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
            uc.Dock = DockStyle.Fill;
            uc.Visible = true;
            Neusoft.FrameWork.WinForms.Forms.IControlable ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
                ic.SetValue(patient, this.tv.Nodes[0]);
                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new Neusoft.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new Neusoft.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

            }
            this.tbBedView.Controls.Add(uc);
            base.OnLoad(e);
        }

        /// <summary>
        /// 获得患者
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            string txtNode = "";

            if (e.Tag.GetType() != typeof(Neusoft.HISFC.Models.RADT.PatientInfo))
            {
                //科室或护理组节点
                txtNode = e.Tag.ToString();
                //显示病区患者一览表
                type = EnumPatientType.DeptOrTend;
            }
            else
            {
                //二级(患者)节点
                txtNode = e.Parent.Tag.ToString();
                node = e;
                patient = e.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;

                //根据节点类型的不同,显示不同的内容
                if (txtNode == EnumPatientType.In.ToString())
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Arrive.ToString())
                {
                    type = EnumPatientType.Arrive;
                }
                else if (txtNode == EnumPatientType.ShiftIn.ToString())
                {
                    type = EnumPatientType.ShiftIn;
                }
                else if (txtNode == EnumPatientType.ShiftOut.ToString())
                {
                    type = EnumPatientType.ShiftOut;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Out.ToString())
                {
                    type = EnumPatientType.Out;
                    arrangeBedNO = null;
                }
                else
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
            }

            #region 旧的作废

            /*
            //根据选中的节点层次和类型不同,显示不同的内容
            if (e.Parent == null)
            {
                //一级节点
                txtNode = e.Tag.ToString();
                //显示病区患者一览表
                type = EnumPatientType.Dept;
            }
            else
            {
                //二级(患者)节点
                txtNode = e.Parent.Tag.ToString();

                //根据节点类型的不同,显示不同的内容
                if (txtNode == EnumPatientType.In.ToString())
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Arrive.ToString())
                {
                    type = EnumPatientType.Arrive;
                }
                else if (txtNode == EnumPatientType.ShiftIn.ToString())
                {
                    type = EnumPatientType.ShiftIn;
                }
                else if (txtNode == EnumPatientType.ShiftOut.ToString())
                {
                    type = EnumPatientType.ShiftOut;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Out.ToString())
                {
                    type = EnumPatientType.Out;
                    arrangeBedNO = null;
                }
                else
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
                node = e;
                patient = e.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
            }
             * */
            #endregion

            this.neuTabControl1_SelectedIndexChanged(null, null);
            return base.OnSetValue(neuObject, e);
        }

        private EnumPatientType mytype = EnumPatientType.DeptOrTend;
        /// <summary>
        /// 类型
        /// </summary>
        protected EnumPatientType type
        {
            get
            {
                return mytype;
            }
            set
            {
                if (mytype == value) 
                    return;
                mytype = value;
                try
                {
                    this.neuTabControl1.TabPages.Clear();
                }
                catch { };
                if (mytype == EnumPatientType.DeptOrTend)
                {

                    this.neuTabControl1.TabPages.Add(this.tbBedView);
                }
                else if (mytype == EnumPatientType.In)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpDept);
                    this.neuTabControl1.TabPages.Add(this.tpChangeDoc);
                    this.neuTabControl1.TabPages.Add(this.tpOut);

                    if (this.tpShiftNurseCellVisible)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpShiftNurseCell);
                    }                    

                    if (tpLeaveVisible)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpLeave);
                    }
                    if (tpNurseVisible)
                    {
                        #region 妇产科显示婴儿登记

                        if (babyDeptHelper == null)
                        {
                            Neusoft.HISFC.BizProcess.Integrate.Manager constManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
                            System.Collections.ArrayList al = constManager.GetConstantList("USEBABYDEPT");
                            babyDeptHelper = new Neusoft.FrameWork.Public.ObjectHelper(al);
                        }

                        bool isShowBaby = false;
                        Neusoft.HISFC.Models.Base.Employee myoper = (Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator;
                        if (babyDeptHelper.GetObjectFromID(myoper.Dept.ID) != null
                            || babyDeptHelper.GetObjectFromID(myoper.Nurse.ID) != null)
                        {
                            isShowBaby = true;
                        }

                        //foreach (Neusoft.HISFC.Models.Base.Const obj in al)
                        //{
                        //    if (obj.ID == myoper.Dept.ID || obj.ID == myoper.Nurse.ID)
                        //    {
                        //        isShowBaby = true;
                        //        break;
                        //    }
                        //}
                        if (isShowBaby)
                        {
                            this.neuTabControl1.TabPages.Add(this.tpNurse);
                        }
                        #endregion
                    }
                    if (isStopPregnancyVisible)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpStopPregnancy);
                    }

                    this.neuTabControl1.TabPages.Add(this.tpPatientBed);
                }
                else if (mytype == EnumPatientType.Out)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpCallBack);
                    //{5DDFB29F-29B7-4963-9E5F-3110B304307A} 默认选中功能菜单 20100915
                    this.neuTabControl1.SelectedTab = this.tpCallBack;
                }
                else if (mytype == EnumPatientType.Arrive)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpArrive);
                    //{5DDFB29F-29B7-4963-9E5F-3110B304307A} 默认选中功能菜单 20100915
                    this.neuTabControl1.SelectedTab = this.tpArrive;
                }
                else if (mytype == EnumPatientType.ShiftIn)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpArrive);
                    //{5DDFB29F-29B7-4963-9E5F-3110B304307A} 默认选中功能菜单 20100915
                    this.neuTabControl1.SelectedTab = this.tpArrive;
                }
                else if (mytype == EnumPatientType.ShiftOut)
                {
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    #region {81987883-BFB0-42f7-8B99-CF44CA44BDDA}
                    if (this.转病区是否显示)
                    {
                        if (patient != null)
                        {
                            Neusoft.HISFC.Models.RADT.Location newLocation = new Neusoft.HISFC.Models.RADT.Location();
                            newLocation = this.inpatientManager.QueryShiftNewLocation(this.patient.ID, this.patient.PVisit.PatientLocation.Dept.ID);


                            if (newLocation.Dept.ID == "")
                            {
                                if (newLocation.NurseCell.ID != "")
                                {
                                    this.neuTabControl1.TabPages.Add(this.tpCancelNurseCell);
                                    return;
                                }
                            }
                        }
                    }
                    this.neuTabControl1.TabPages.Add(this.tpCancelDept);

                    #endregion
                }
            }
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuTabControl1.SelectedTab == null)
            {
                return;
            }

            //tabControl Selected Changed
            Neusoft.FrameWork.WinForms.Forms.IControlable ic = null;
            if (this.neuTabControl1.SelectedTab == this.tbBedView)//床位一览
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBedListView uc = new ucBedListView();
                    uc.WapType = wapType;
                    uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
                    uc.arrangeBed += new ucBedListView.ArrangeBed(uc_arrangeBed);
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpArrive)//接珍
            {
                #region 接珍
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBasePatientArrive uc = new ucBasePatientArrive();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.ArrangeBedNO = this.arrangeBedNO;

                    uc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                    uc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                    uc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                    uc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                    uc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                    uc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                    uc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;

                    uc.IsAddAllDoct = this.isAddAllDoct;
                    uc.IsAddAllNurse = this.isAddAllNurse;

                    if (this.node.Parent != null && this.node.Parent.Tag.ToString() == "ShiftIn")
                    {
                        uc.Arrivetype = EnumArriveType.ShiftIn;

                    }
                    else
                    {
                        uc.Arrivetype = EnumArriveType.Accepts;

                    }

                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);

                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    ucBasePatientArrive uc = ic as ucBasePatientArrive;
                    uc.ArrangeBedNO = this.arrangeBedNO;

                    uc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                    uc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                    uc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                    uc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                    uc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                    uc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                    uc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;


                    uc.IsAddAllDoct = this.isAddAllDoct;
                    uc.IsAddAllNurse = this.isAddAllNurse;

                    if (this.node.Parent != null && this.node.Parent.Tag.ToString() == "ShiftIn")
                    {
                        uc.Arrivetype = EnumArriveType.ShiftIn;
                    }
                    else
                    {
                        uc.Arrivetype = EnumArriveType.Accepts;

                    }

                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCallBack)//找回
            {
                #region 召回
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    Neusoft.HISFC.BizProcess.Interface.ICallBackPatient uc = null;
                    uc = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.ICallBackPatient)) as Neusoft.HISFC.BizProcess.Interface.ICallBackPatient;
                    if (uc == null)
                    {
                        ucBasePatientArrive defaultuc = new ucBasePatientArrive();
                        defaultuc.Dock = DockStyle.Fill;
                        defaultuc.Visible = true;
                        defaultuc.Arrivetype = EnumArriveType.CallBack;
                        defaultuc.IsAllowCallBackOtherDay = this.isAllowCallBackOtherDay;

                        defaultuc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                        defaultuc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                        defaultuc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                        defaultuc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                        defaultuc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                        defaultuc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                        defaultuc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;


                        defaultuc.IsAddAllDoct = this.isAddAllDoct;
                        defaultuc.IsAddAllNurse = this.isAddAllNurse;

                        ic = defaultuc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add((Neusoft.FrameWork.WinForms.Controls.ucBaseControl)defaultuc);
                    }
                    else
                    {
                        ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add((Neusoft.FrameWork.WinForms.Controls.ucBaseControl)uc);
                    }
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCancelDept)//取消转科
            {
                #region 取消转科
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftOut uc = new ucPatientShiftOut();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = true;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion 
            }
            else if (this.neuTabControl1.SelectedTab == this.tpChangeDoc)//换医生
            {
                #region 换医生
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBasePatientArrive uc = new ucBasePatientArrive();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.Arrivetype = EnumArriveType.ChangeDoct;

                    uc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                    uc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                    uc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                    uc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                    uc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                    uc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                    uc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;

                    uc.IsAddAllDoct = this.isAddAllDoct;
                    uc.IsAddAllNurse = this.isAddAllNurse;

                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion  
            }
            else if (this.neuTabControl1.SelectedTab == this.tpDept)//换科室
            {
                #region 换科室
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftOut uc = new ucPatientShiftOut();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = false;

                    uc.AutoDcDoct = this.autoDcDoct;
                    uc.IsAutoDcOrder = this.isShiftAutoDcOrder;
                    uc.IsUseShiftAutoDcOrder = this.isUseShiftAutoDcOrder;

                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion 
            } 
            else if (this.neuTabControl1.SelectedTab == this.tpLeave)//请假
            {
                #region 请假
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientLeave uc = new ucPatientLeave();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion 
            }
            else if (this.neuTabControl1.SelectedTab == this.tpNurse)//婴儿登记
            {
                #region 婴儿登记
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBabyInfo uc = new ucBabyInfo();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    #region {F4EB69FA-F43E-4bdc-AC85-B53377604818}
                    uc.IsModifyBirthday = this.isModifyBirthday;
                    #endregion
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion 
            }
            else if (this.neuTabControl1.SelectedTab == this.tpOut)//出院登记
            {
                #region 出院登记
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    //ucPatientOut uc = new ucPatientOut();

                    Neusoft.HISFC.BizProcess.Interface.IucOutPatient uc = null;
                    uc = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IucOutPatient)) as Neusoft.HISFC.BizProcess.Interface.IucOutPatient;
                    if (uc == null)
                    {
                        ucPatientOut ucDefault = new ucPatientOut();
                        
                        ucDefault.Dock = DockStyle.Fill;
                        ucDefault.Visible = true;

                        ucDefault.IsPrintOutNote = this.isPrintOutNote;
                        ucDefault.AutoDcDoct = this.autoDcDoct;
                        ucDefault.IsAutoDcOrder = this.isAutoDcOrder;

                        ucDefault.IsCanOutWhenDrugApplay = this.isCanOutWhenDrugApplay;
                        ucDefault.IsCanOutWhenNoDcOrder = this.isCanOutWhenNoDcOrder;
                        ucDefault.IsCanOutWhenNoFeeExecUndrugOrder = this.isCanOutWhenNoFeeExecUndrugOrder;
                        ucDefault.IsCanOutWhenNoOutOrder = this.isCanOutWhenNoOutOrder;
                        ucDefault.IsCanOutWhenQuitDrugApplay = this.isCanOutWhenQuitDrugApplay;
                        ucDefault.IsCanOutWhenQuitFeeApplay = isCanOutWhenQuitFeeApplay;
                        ucDefault.IsCanOutWhenUnConfirm = isCanOutWhenUnConfirm;
                        ucDefault.IsCanOutWhenUnConfirmOrder = isCanOutWhenUnConfirmOrder;
                        ucDefault.IsCanOutWhenUnFeeUOApply = this.isCanOutWhenUnFeeUOApply;
                        ucDefault.IsCanOutWhenLackFee = this.isCanOutWhenLackFee;

                        ucDefault.IsDoctZG = this.isDoctZG;

                        ic = ucDefault as Neusoft.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add(ucDefault);
                    }
                    else
                    {
                        ((System.Windows.Forms.UserControl)uc).Dock = DockStyle.Fill;
                        ((System.Windows.Forms.UserControl)uc).Visible = true;
                        ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add((System.Windows.Forms.UserControl)uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPatient)//患者基本信息
            {
                #region 患者基本信息
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientInfo uc = new ucPatientInfo();
                    uc.SexReadOnly = sexReadOnly;
                    uc.BirthdayReadOnly = birthdayReadOnly;
                    uc.RelationReadOnly = relationReadOnly;
                    uc.HeightReadOnly = heightReadOnly;
                    uc.WeightReadOnly = weightReadOnly;
                    uc.IDReadOnly = iDReadOnly;
                    uc.ProfessionReadOnly = professionReadOnly;
                    uc.MarryReadOnly = marryReadOnly;
                    uc.HomeAddrReadOnly = homeAddrReadOnly;
                    uc.HomeTelReadOnly = homeTelReadOnly;
                    uc.WorkReadOnly = workReadOnly;
                    uc.LinkManReadOnly = linkManReadOnly;
                    uc.KinAddressReadOnly = kinAddressReadOnly;
                    uc.LinkTelReadOnly = linkTelReadOnly;
                    uc.MemoReadOnly = memoReadOnly;
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpShiftNurseCell)//{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
            {
                #region 转病区{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftNurseCell uc = new ucPatientShiftNurseCell();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = false;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion 
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCancelNurseCell)//{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
            {
                #region 取消转病区{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftNurseCell uc = new ucPatientShiftNurseCell();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = true;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion 
            }
            else if (this.neuTabControl1.SelectedTab == this.tpStopPregnancy)
            {
                #region 终止妊娠登记
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucStopPregnancy uc = new ucStopPregnancy();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPatientBed)
            {
                #region 床位等级信息
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientBedGrade uc = new ucPatientBedGrade();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as Neusoft.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    return;
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                }
            }

            if (ic != null)
            {
                ic.SetValue(patient, node);
                ic.RefreshTree -= new EventHandler(ic_RefreshTree);
                ic.SendParamToControl -= new Neusoft.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo -= new Neusoft.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new Neusoft.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new Neusoft.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

            }
        }

        void uc_arrangeBed(string bedNO)
        {
            this.arrangeBedNO = bedNO;
        }

        void uc_ListViewItemChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
            myPatientInfo = e.Item.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
            if (myPatientInfo != null)
            {
                string strBedInfo = myPatientInfo.PVisit.PatientLocation.Bed.ID;
                strBedInfo = strBedInfo.Length > 4 ? strBedInfo.Substring(4) : strBedInfo;
                e.Item.ToolTipText = myPatientInfo.Name + "-【" + strBedInfo + "床】-" + ((EnumBedState)e.Item.ImageIndex).ToString();
                base.OnStatusBarInfo(sender, myPatientInfo.Name + "-【" + strBedInfo + "床】-" + ((EnumBedState)e.Item.ImageIndex).ToString());
            }
            else
            {
                base.OnStatusBarInfo(sender, ((EnumBedState)e.Item.ImageIndex).ToString());
            }
        }

        void ic_StatusBarInfo(object sender, string msg)
        {
            this.OnStatusBarInfo(sender, msg);
        }

        void ic_SendParamToControl(object sender, string dllName, string controlName, object objParams)
        {
            this.OnSendParamToControl(sender, dllName, controlName, objParams);
        }

        void ic_SendMessage(object sender, string msg)
        {
            this.OnSendMessage(sender, msg);
        }

        /// <summary>
        /// {997A8EEC-A27E-492f-941A-CDEAA3CC4AE7}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ic_RefreshTree(object sender, EventArgs e)
        {
            this.OnRefreshTree();
            try
            {
                ucBedListView uc = this.tbBedView.Controls[0] as ucBedListView;
                uc.RefreshView();
                uc.WapType = wapType;
            }
            catch { }
        }

        #endregion

        #region 共有函数
        /// <summary>
        /// 开放的Tabpage
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        /// <param name="tag"></param>
        public void AddTabpage(Neusoft.FrameWork.WinForms.Controls.ucBaseControl control, string title, object tag)
        {

            foreach (TabPage tb in this.neuTabControl1.TabPages)
            {
                if (tb.Text == title)
                {
                    this.neuTabControl1.SelectedTab = tb;
                    return;
                }
            }
            TabPage tp = new TabPage(title);
            this.neuTabControl1.TabPages.Add(tp);

            control.Dock = DockStyle.Fill;
            control.Visible = true;

            Neusoft.FrameWork.WinForms.Forms.IControlable ic = control as Neusoft.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
            }
            #region {5DF40042-300D-49b8-BB8D-4E4E906B7BAF}
            if (control.GetType() == typeof(Neusoft.HISFC.Components.RADT.Controls.ucBedManager))
            {
                Neusoft.HISFC.Components.RADT.Controls.ucBedManager uc = control as Neusoft.HISFC.Components.RADT.Controls.ucBedManager;

                uc.IsAllBedWave = this.isAllBedWave;
                uc.IsAllowAddAllBedLevel = this.isAllowAddAllBedLevel;
                uc.IsAllowEditBedLevel = this.IsAllowEditBedLevel;
                uc.IsAllowEditBedNo = this.IsAllowEditBedNo;

                tp.Controls.Add(uc);
            }
            else if (control.GetType() == typeof(Neusoft.HISFC.Components.RADT.Controls.ucPatientOut))
            {
                Neusoft.HISFC.Components.RADT.Controls.ucPatientOut uc = control as Neusoft.HISFC.Components.RADT.Controls.ucPatientOut;

                uc.IsAutoDcOrder = this.isAutoDcOrder;
                uc.IsPrintOutNote = this.isPrintOutNote;
                uc.AutoDcDoct = this.autoDcDoct;

                tp.Controls.Add(uc);
            }
            else
            {
                tp.Controls.Add(control);
            }
            #endregion
            if (ic != null)
                ic.SetValue(patient, node);
            this.neuTabControl1.SelectedTab = tp;

            

        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public enum EnumBedState
        {
            /// <summary>
            /// 
            /// </summary>
            空床 = 0,
            /// <summary>
            /// 
            /// </summary>
            男 = 1,
            /// <summary>
            /// 
            /// </summary>
            女 = 2,
            /// <summary>
            /// 
            /// </summary>
            关闭 = 3,
            /// <summary>
            /// 
            /// </summary>
            三级护理 = 4,
            /// <summary>
            /// 
            /// </summary>
            二级护理 = 5,
            /// <summary>
            /// 
            /// </summary>
            一级护理 = 6,
            /// <summary>
            /// 
            /// </summary>
            病危 = 7,
            /// <summary>
            /// 
            /// </summary>
            重症 = 8,
            /// <summary>
            /// 
            /// </summary>
            包床 = 9,
            /// <summary>
            /// 
            /// </summary>
            放假 = 10,
            /// <summary>
            /// 
            /// </summary>
            挂床 = 11,
            /// <summary>
            /// 
            /// </summary>
            无 = 12,
            /// <summary>
            /// 
            /// </summary>
            没有 = 13
        }


        #region IInterfaceContainer 成员

        Type[] Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];
                type[0] = typeof(Neusoft.HISFC.BizProcess.Interface.IucOutPatient);
                type[1] = typeof(Neusoft.HISFC.BizProcess.Interface.ICallBackPatient);
                return type;
            }
        }

        #endregion
    }
}
