using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Components.RADT.Controls
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
    public partial class ucRADT : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
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
        protected FS.HISFC.Models.RADT.PatientInfo patient = null;

        /// <summary>
        ///待分配的床号 
        /// </summary>
        protected string arrangeBedNO;

        /// <summary>
        /// 患者业务类{81987883-BFB0-42f7-8B99-CF44CA44BDDA}
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
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
            //FS.FrameWork.WinForms.Forms.IControlable ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
            //if (ic != null)
            //{
            //    ic.Init(this.tv, null, null);
            //    ic.SetValue(patient, this.tv.Nodes[0]);
            //    ic.RefreshTree += new EventHandler(ic_RefreshTree);
            //    ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
            //    ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

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


        /// <summary>
        /// 是否只显示婴儿登记// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}
        /// </summary>
        private bool tpOnlyBabyVisible = false;

        /// 终止妊娠登记是否显示
        /// </summary>
        private bool isStopPregnancyVisible = false;

        /// <summary>
        /// 床位等级信息是否显示
        /// </summary>
        private bool isShowPatientBed = false;

        /// <summary>
        /// 床位等级信息是否显示
        /// </summary>
        [Category("tab页设置"), Description("床位等级信息是否显示")]
        public bool IsShowPatientBed
        {
            get
            {
                return isShowPatientBed;
            }
            set
            {
                isShowPatientBed = value;
            }
        }

        /// <summary>
        /// 转病区管理是否显示
        /// </summary>
        private bool tpShiftNurseCellVisible = false;

        /// <summary>
        /// 允许婴儿登记的科室
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper babyDeptHelper = null;

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
        /// 是否允许修改接诊时间// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        /// </summary>
        private bool isCanEditArriveInTime = true;

        /// <summary>
        /// 是否允许修改接诊时间
        /// </summary>
        [Category("接诊设置"), Description("是否允许修改接诊时间")]
        public bool IsCanEditArriveInTime
        {
            get
            {
                return isCanEditArriveInTime;
            }
            set
            {
                isCanEditArriveInTime = value;
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
        /// 药品未执行确认是否允许办理出院（要有执行确认流程）
        /// </summary>
        private bool isCanOutWhenExecDrugLimit = false;

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
        /// 存在未完成的会诊申请是否允许办理出院登记
        /// </summary>
        private CheckState isCanOutWhenUnConsultation = CheckState.Check;

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

        #region 转科设置
        /// <summary>
        /// 转科是否可以更改病区
        /// </summary>
        [Category("转科设置"), Description("转科是否可以更改病区")]
        public bool IsAbleChangeInpatientArea
        {
            get;
            set;
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
        // {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}
        [Category("tab页设置"), Description("是否只显示婴儿登记")]
        public bool 是否只显示婴儿登记
        {
            get
            {
                return this.tpOnlyBabyVisible;
            }
            set
            {
                tpOnlyBabyVisible = value;
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

        #region 转病区维护设置

        /// <summary>
        /// 存在未审核的医嘱是否允许办理转病区// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        private CheckState isAllowWhenUnConfirmOrder = CheckState.Check;

        /// <summary>
        /// 存在未审核的医嘱是否允许办理转病区// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        [Category("转病区申请"), Description("存在未审核的医嘱是否允许办理转病区")]
        public CheckState IsAllowWhenUnConfirmOrder
        {
            get
            {
                return isAllowWhenUnConfirmOrder;
            }
            set
            {
                isAllowWhenUnConfirmOrder = value;
            }
        }
        #endregion
        #region 床位维护设置

        /// <summary>
        /// 是否允许加床
        /// </summary>
        private bool isAllowAddBed = true;

        /// <summary>
        /// 是否允许加床
        /// </summary>
        [Category("床位维护设置"), Description("是否允许加床")]
        public bool IsAllowAddBed
        {
            get
            {
                return isAllowAddBed;
            }
            set
            {
                isAllowAddBed = value;
            }
        }

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

        #region 转科申请限制


        /// <summary>
        /// 存在未收费的手术申请单是否允许办理
        /// </summary>
        /// {566f98c2-5ac4-49b3-940b-fdebad959229} 添加转科的手术限制控件属性。
        private CheckState isCanShiftWhenUnFeeUOApply = CheckState.Check;

        /// <summary>
        /// 存在未收费的手术申请单是否允许办理
        /// </summary>
        /// {566f98c2-5ac4-49b3-940b-fdebad959229} 添加转科的手术限制控件属性。
        [Category("转科"), Description("存在未收费的手术申请单是否允许办理，默认为校验。"), DefaultValue(CheckState.Check)]
        public CheckState IsCanShiftWhenUnFeeUOApply
        {
            get
            {
                return this.isCanShiftWhenUnFeeUOApply;
            }
            set
            {
                isCanShiftWhenUnFeeUOApply = value;
            }
        }

        /// <summary>
        /// 存在退费申请是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenQuitFeeApplay = CheckState.No;

        /// <summary>
        /// 有退费申请是否允许转科申请
        /// </summary>
        [Category("转科申请"), Description("存在退费申请是否允许做转科申请")]
        public CheckState IsCanShiftWhenQuitFeeApplay
        {
            get
            {
                return isCanShiftWhenQuitFeeApplay;
            }
            set
            {
                isCanShiftWhenQuitFeeApplay = value;
            }
        }

        /// <summary>
        /// 存在退药申请是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenQuitDrugApplay = CheckState.No;

        /// <summary>
        /// 存在退药申请是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在退药申请是否允许做转科申请")]
        public CheckState IsCanShiftWhenQuitDrugApplay
        {
            get
            {
                return isCanShiftWhenQuitDrugApplay;
            }
            set
            {
                isCanShiftWhenQuitDrugApplay = value;
            }
        }

        /// <summary>
        /// 存在发药申请是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenDrugApplay = CheckState.Check;

        /// <summary>
        /// 存在发药申请是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在发药申请是否允许做转科申请")]
        public CheckState IsCanShiftWhenDrugApplay
        {
            get
            {
                return this.isCanShiftWhenDrugApplay;
            }
            set
            {
                this.isCanShiftWhenDrugApplay = value;
            }
        }

        /// <summary>
        /// 存在未确认项目是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenUnConfirm = CheckState.Check;

        /// <summary>
        /// 存在未确认项目是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未确认项目是否允许做转科申请")]
        public CheckState IsCanShiftWhenUnConfirm
        {
            get
            {
                return this.isCanShiftWhenUnConfirm;
            }
            set
            {
                this.isCanShiftWhenUnConfirm = value;
            }
        }

        /// <summary>
        /// 未开立转科医嘱是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenNoOutOrder = CheckState.No;

        /// <summary>
        /// 未开立转科医嘱是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("未开立转科医嘱是否允许做转科申请")]
        public CheckState IsCanShiftWhenNoOutOrder
        {
            get
            {
                return this.isCanShiftWhenNoOutOrder;
            }
            set
            {
                this.isCanShiftWhenNoOutOrder = value;
            }
        }

        /// <summary>
        /// 未全部停止长嘱是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenNoDcOrder = CheckState.No;

        /// <summary>
        /// 未全部停止长嘱是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("未全部停止长嘱是否允许做转科申请")]
        public CheckState IsCanShiftWhenNoDcOrder
        {
            get
            {
                return this.isCanShiftWhenNoDcOrder;
            }
            set
            {
                this.isCanShiftWhenNoDcOrder = value;
            }
        }

        /// <summary>
        /// 存在未审核医嘱是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenUnConfirmOrder = CheckState.Check;

        /// <summary>
        /// 存在未审核医嘱是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未审核医嘱是否允许做转科申请")]
        public CheckState IsCanShiftWhenUnConfirmOrder
        {
            get
            {
                return this.isCanShiftWhenUnConfirmOrder;
            }
            set
            {
                this.isCanShiftWhenUnConfirmOrder = value;
            }
        }

        /// <summary>
        /// 存在未收费的非药品执行档是否允许做转科申请
        /// </summary>
        private CheckState isCanShiftWhenNoFeeExecUndrugOrder = CheckState.Check;

        /// <summary>
        /// 存在未收费的非药品执行档是否允许做转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未收费的非药品执行档是否允许做转科申请")]
        public CheckState IsCanShiftWhenNoFeeExecUndrugOrder
        {
            get
            {
                return this.isCanShiftWhenNoFeeExecUndrugOrder;
            }
            set
            {
                isCanShiftWhenNoFeeExecUndrugOrder = value;
            }
        }

        /// <summary>
        /// 存在未完成的会诊申请是否允许办理转科申请
        /// </summary>
        private CheckState isCanShiftWhenUnConsultation = CheckState.No;

        /// <summary>
        /// 存在未完成的会诊申请是否允许办理转科申请
        /// </summary>
        [Category("转科申请"), Description("存在未完成的会诊申请是否允许办理转科申请")]
        public CheckState IsCanShiftWhenUnConsultation
        {
            get
            {
                return this.isCanShiftWhenUnConsultation;
            }
            set
            {
                isCanShiftWhenUnConsultation = value;
            }
        }

        /// <summary>
        /// 欠费是否允许办理转科手续
        /// </summary>
        private CheckState isCanShiftWhenLackFee = CheckState.Check;

        /// <summary>
        /// 欠费是否允许办理转科手续
        /// </summary>
        [Category("转科申请"), Description("欠费是否允许办理转科手续")]
        public CheckState IsCanShiftWhenLackFee
        {
            get
            {
                return this.isCanShiftWhenLackFee;
            }
            set
            {
                isCanShiftWhenLackFee = value;
            }
        }

        #endregion

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

        private decimal autoCost = 0;
        /// <summary>
        /// 出院限制金额判断
        /// </summary>
        [Category("出院登记"), Description("出院限制金额判断")]
        public decimal AutoCost
        {
            get
            {
                return autoCost;
            }
            set
            {
                autoCost = value;
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
            FS.FrameWork.WinForms.Forms.IControlable ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
                ic.SetValue(patient, this.tv.Nodes[0]);
                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

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

            if (e.Tag.GetType() != typeof(FS.HISFC.Models.RADT.PatientInfo))
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
                patient = e.Tag as FS.HISFC.Models.RADT.PatientInfo;

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
                patient = e.Tag as FS.HISFC.Models.RADT.PatientInfo;
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
                    if (!tpOnlyBabyVisible)// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}
                    {
                        this.neuTabControl1.TabPages.Add(this.tpPatient);
                        this.neuTabControl1.TabPages.Add(this.tpDept);
                        this.neuTabControl1.TabPages.Add(this.tpChangeDoc);
                        this.neuTabControl1.TabPages.Add(this.tpOut);
                        this.neuTabControl1.TabPages.Add(this.tpCancelArrive);
                    }

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
                            FS.HISFC.BizProcess.Integrate.Manager constManager = new FS.HISFC.BizProcess.Integrate.Manager();
                            System.Collections.ArrayList al = constManager.GetConstantList("USEBABYDEPT");
                            babyDeptHelper = new FS.FrameWork.Public.ObjectHelper(al);
                        }

                        bool isShowBaby = false;
                        FS.HISFC.Models.Base.Employee myoper = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
                        if (babyDeptHelper.GetObjectFromID(myoper.Dept.ID) != null
                            || babyDeptHelper.GetObjectFromID(myoper.Nurse.ID) != null)
                        {
                            isShowBaby = true;
                        }

                        //foreach (FS.HISFC.Models.Base.Const obj in al)
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

                    if (isShowPatientBed)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpPatientBed);
                    }
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
                            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
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
            FS.FrameWork.WinForms.Forms.IControlable ic = null;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    uc.IsCanEditArriveInTime = this.isCanEditArriveInTime;// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
                    uc.IsAddAllNurse = this.isAddAllNurse;

                    if (this.node.Parent != null && this.node.Parent.Tag.ToString() == "ShiftIn")
                    {
                        uc.Arrivetype = EnumArriveType.ShiftIn;

                    }
                    else
                    {
                        uc.Arrivetype = EnumArriveType.Accepts;

                    }

                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);

                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                //this.neuTabControl1.TabPages.Add(this.tpcancelArrive);//////添加取消接诊标签到接诊页面？？
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCancelArrive)//取消接诊
            {
                #region 取消接诊
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucCancelArrive uc = new ucCancelArrive();

                    if (uc == null)
                    {
                        ucPatientOut ucDefault = new ucPatientOut();

                        ucDefault.Dock = DockStyle.Fill;
                        ucDefault.Visible = true;


                        ic = ucDefault as FS.FrameWork.WinForms.Forms.IControlable;
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
                        ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add((System.Windows.Forms.UserControl)uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCallBack)//找回
            {
                #region 召回
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    FS.HISFC.BizProcess.Interface.ICallBackPatient uc = null;
                    uc = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.ICallBackPatient)) as FS.HISFC.BizProcess.Interface.ICallBackPatient;
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

                        ic = defaultuc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add((FS.FrameWork.WinForms.Controls.ucBaseControl)defaultuc);
                    }
                    else
                    {
                        ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add((FS.FrameWork.WinForms.Controls.ucBaseControl)uc);
                    }
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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

                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpDept)//换科室
            {
                #region 换科室
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftOut ucPatientShiftOut = new ucPatientShiftOut();
                    ucPatientShiftOut.Dock = DockStyle.Fill;
                    ucPatientShiftOut.Visible = true;
                    ucPatientShiftOut.IsCancel = false;

                    ucPatientShiftOut.IsAbleChangeInpatientArea = this.IsAbleChangeInpatientArea;
                    ucPatientShiftOut.AutoDcDoct = this.autoDcDoct;
                    ucPatientShiftOut.IsAutoDcOrder = this.isShiftAutoDcOrder;
                    ucPatientShiftOut.IsUseShiftAutoDcOrder = this.isUseShiftAutoDcOrder;
                    // {566f98c2-5ac4-49b3-940b-fdebad959229} 添加转科的手术限制控件属性。
                    ucPatientShiftOut.IsCanShiftWhenUnFeeUOApply = this.isCanShiftWhenUnFeeUOApply;

                    ucPatientShiftOut.IsCanShiftWhenQuitFeeApplay = this.isCanShiftWhenQuitFeeApplay;
                    ucPatientShiftOut.IsCanShiftWhenQuitDrugApplay = this.isCanShiftWhenQuitDrugApplay;
                    ucPatientShiftOut.IsCanShiftWhenDrugApplay = this.isCanShiftWhenDrugApplay;
                    ucPatientShiftOut.IsCanShiftWhenUnConfirm = this.isCanShiftWhenUnConfirm;
                    ucPatientShiftOut.IsCanShiftWhenNoOutOrder = this.isCanShiftWhenNoOutOrder;
                    ucPatientShiftOut.IsCanShiftWhenNoDcOrder = this.isCanShiftWhenNoDcOrder;
                    ucPatientShiftOut.IsCanShiftWhenUnConfirmOrder = this.isCanShiftWhenUnConfirmOrder;
                    ucPatientShiftOut.IsCanShiftWhenNoFeeExecUndrugOrder = this.isCanShiftWhenNoFeeExecUndrugOrder;
                    ucPatientShiftOut.IsCanShiftWhenUnConsultation = this.isCanShiftWhenUnConsultation;
                    ucPatientShiftOut.IsCanShiftWhenLackFee = this.isCanShiftWhenLackFee;

                    ic = ucPatientShiftOut as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(ucPatientShiftOut);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpOut)//出院登记
            {
                #region 出院登记
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    //ucPatientOut uc = new ucPatientOut();

                    FS.HISFC.BizProcess.Interface.IucOutPatient uc = null;
                    uc = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IucOutPatient)) as FS.HISFC.BizProcess.Interface.IucOutPatient;
                    if (uc == null)
                    {
                        ucPatientOut ucPatientOut = new ucPatientOut();

                        ucPatientOut.Dock = DockStyle.Fill;
                        ucPatientOut.Visible = true;

                        ucPatientOut.IsPrintOutNote = this.isPrintOutNote;
                        ucPatientOut.AutoDcDoct = this.autoDcDoct;
                        ucPatientOut.IsAutoDcOrder = this.isAutoDcOrder;

                        ucPatientOut.IsCanOutWhenDrugApplay = this.isCanOutWhenDrugApplay;
                        ucPatientOut.IsCanOutWhenNoDcOrder = this.isCanOutWhenNoDcOrder;
                        ucPatientOut.IsCanOutWhenNoFeeExecUndrugOrder = this.isCanOutWhenNoFeeExecUndrugOrder;
                        ucPatientOut.IsCanOutWhenNoOutOrder = this.isCanOutWhenNoOutOrder;
                        ucPatientOut.IsCanOutWhenQuitDrugApplay = this.isCanOutWhenQuitDrugApplay;
                        ucPatientOut.IsCanOutWhenQuitFeeApplay = isCanOutWhenQuitFeeApplay;
                        ucPatientOut.IsCanOutWhenUnConfirm = isCanOutWhenUnConfirm;
                        ucPatientOut.IsCanOutWhenUnConfirmOrder = isCanOutWhenUnConfirmOrder;
                        ucPatientOut.IsCanOutWhenUnFeeUOApply = this.isCanOutWhenUnFeeUOApply;
                        ucPatientOut.IsCanOutWhenLackFee = this.isCanOutWhenLackFee;
                        ucPatientOut.IsCanOutWhenUnConsultation = this.isCanOutWhenUnConsultation;
                        ucPatientOut.IsCanOutWhenExecDrugLimit = this.isCanOutWhenExecDrugLimit;
                        ucPatientOut.IsDoctZG = this.isDoctZG;

                        ic = ucPatientOut as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add(ucPatientOut);
                    }
                    else
                    {
                        ((System.Windows.Forms.UserControl)uc).Dock = DockStyle.Fill;
                        ((System.Windows.Forms.UserControl)uc).Visible = true;
                        ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add((System.Windows.Forms.UserControl)uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    uc.IsCanWhenUnConfirmOrder = this.IsAllowWhenUnConfirmOrder;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
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
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }

            if (ic != null)
            {
                ic.SetValue(patient, node);
                ic.RefreshTree -= new EventHandler(ic_RefreshTree);
                ic.SendParamToControl -= new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo -= new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

            }
        }

        void uc_arrangeBed(string bedNO)
        {
            this.arrangeBedNO = bedNO;
        }

        void uc_ListViewItemChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            myPatientInfo = e.Item.Tag as FS.HISFC.Models.RADT.PatientInfo;
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
        public void AddTabpage(FS.FrameWork.WinForms.Controls.ucBaseControl control, string title, object tag)
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

            FS.FrameWork.WinForms.Forms.IControlable ic = control as FS.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
            }
            #region {5DF40042-300D-49b8-BB8D-4E4E906B7BAF}
            if (control.GetType() == typeof(FS.HISFC.Components.RADT.Controls.ucBedManager))
            {
                FS.HISFC.Components.RADT.Controls.ucBedManager uc = control as FS.HISFC.Components.RADT.Controls.ucBedManager;

                uc.IsAllBedWave = this.isAllBedWave;
                uc.IsAllowAddAllBedLevel = this.isAllowAddAllBedLevel;
                uc.IsAllowEditBedLevel = this.IsAllowEditBedLevel;
                uc.IsAllowEditBedNo = this.IsAllowEditBedNo;
                uc.IsAllowAddBed = this.isAllowAddBed;

                tp.Controls.Add(uc);
            }
            else if (control.GetType() == typeof(FS.HISFC.Components.RADT.Controls.ucPatientOut))
            {
                FS.HISFC.Components.RADT.Controls.ucPatientOut uc = control as FS.HISFC.Components.RADT.Controls.ucPatientOut;

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

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.IucOutPatient);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.ICallBackPatient);
                return type;
            }
        }

        #endregion
    }
}
