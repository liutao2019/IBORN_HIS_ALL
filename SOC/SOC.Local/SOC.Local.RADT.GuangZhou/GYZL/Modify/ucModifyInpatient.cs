using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using FS.SOC.HISFC.RADT.Components.Interface;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.RADT;
using FS.SOC.HISFC.BizProcess.CommonInterface.Attributes;
using System.Collections;
using FS.SOC.Local.RADT.GuangZhou.GYZL.Interface;


namespace FS.SOC.Local.RADT.GuangZhou.GYZL.Modify
{
    /// <summary>
    /// [功能描述: 住院患者基本信息修改]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public partial class ucModifyInpatient : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucModifyInpatient()
        {
            InitializeComponent();
        }

        #region 变量
        /// <summary>
        /// 弹出入院登记接口
        /// </summary>
        private FS.SOC.HISFC.RADT.Interface.Register.IInpatient IRegister = null;

        /// <summary>
        /// 患者信息实体
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new PatientInfo();

        /// <summary>
        /// 患者信息实体副本
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patientInfoOld; 
        
        /// <summary>
        /// 患者入出转转业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();

        private FS.SOC.HISFC.RADT.Interface.Patient.IQuery IQueryPatientInfo = null;
        private bool isAllowModifyInDate = false;
        private bool isAllowModifyPact = false;

        #endregion

        #region 属性

        [Category("参数设置"), Description("是否允许修改入院日期:true 允许 false 不允许"), DefaultValue(false)]
        [FSSetting()]
        public bool IsAllowModifyInDate
        {
            set
            {
                isAllowModifyInDate = value;
            }
            get
            {
                return isAllowModifyInDate;
            }
        }

        [Category("参数设置"), Description("住院号查询的状态设置")]
        [FSSetting()]
        public FS.HISFC.Components.Common.Controls.enuShowState ShowState
        {
            get
            {
                return this.ucQueryInpatientNo1.ShowState;
            }
            set
            {
                this.ucQueryInpatientNo1.ShowState = value;
            }
        }

        [Category("参数设置"),Description("是否允许修改合同单位"),DefaultValue(false)]
        [FSSetting()]
        public bool IsAllowModifyPact
        {
            get
            {
                return isAllowModifyPact;
            }
            set
            {
                isAllowModifyPact = value;
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 初始化
        /// </summary>
        public void initForms()
        {
            this.lblOldPact.Visible = this.isAllowModifyPact;
            this.txtOldPact.Visible = this.isAllowModifyPact;
            this.lblNewPact.Visible = this.isAllowModifyPact;
            this.cmbNewPact.Visible = this.isAllowModifyPact;

            if (this.isAllowModifyPact)
            {
                FS.SOC.HISFC.RADT.BizProcess.InpatientFee feeManager = new FS.SOC.HISFC.RADT.BizProcess.InpatientFee();
                ArrayList al = feeManager.QueryInpatientPact();
                if (al == null)
                {
                    this.myMessageBox("获取合同单位信息失败，原因："+feeManager.Err, MessageBoxIcon.Error);
                    return;
                }
                this.cmbNewPact.AddItems(al);
            }
        }

        public void initEvents()
        {
            this.ucQueryInpatientNo1.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo1_myEvent);
            
        }

        /// <summary>
        /// 初始化接口信息
        /// </summary>
        /// <returns></returns>
        private int initInterface()
        {
            this.IRegister = ModifyPatient.GetModifyIInpatient();

            this.IRegister.OnSavePatientInfo += new EventHandler(IRegisterInterface_OnSavePatientInfo);
            this.IRegister.IsArriveProcess = false;
            this.IRegister.IsAutoPatientNO = false;
            this.IRegister.IsCanModifyInTime = isAllowModifyInDate;
            this.IQueryPatientInfo = InterfaceManager.GetIInpatientQuery();
            this.IRegister.FileName = FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\SOC.Components.RADT.PatientInfo.ucModifyInpatient.xml";
            this.IRegister.OnQueryPatientInfo += new FS.SOC.HISFC.RADT.Interface.Register.SelectPatientInfo(IRegisterInterface_OnQueryPatientInfo);

            if (this.IRegister is Control)
            {
                Control c = (Control)this.IRegister;
                this.gbQuery.Controls.Clear();
                c.Dock = DockStyle.Fill;
                this.gbQuery.Height = this.IRegister.ControlSize.Height + 20;
                this.gbQuery.Controls.Add(c);
            }

            this.IRegister.Init();

            return 1;
        }

        /// <summary>
        /// 清空
        /// </summary>
        public void clear()
        {
            this.IRegister.Clear();
            this.ucQueryInpatientNo1.Text = string.Empty;
            this.ucQueryInpatientNo1.Select();
            this.ucQueryInpatientNo1.Focus();
            this.cmbNewPact.Tag = null;
            this.txtOldPact.Text = string.Empty;
        }

        #endregion
        
        #region 工具栏

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("清屏", "清除录入的信息", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q清空, true, false, null);
            toolBarService.AddToolButton("限额累计", "限额累计", (int)FS.FrameWork.WinForms.Classes.EnumImageList.L累计开始, true, false, null);
            return this.toolBarService;
        }


        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "清屏":
                    this.clear();
                    break;
                case "限额累计":
                    this.AlterDayLimitTot();
                    break;
                default:
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        #endregion

        #region 方法

        private void btnAltDlTot_Click(object sender, System.EventArgs e)
        {
            this.AlterDayLimitTot();
        }
        /// <summary>
        /// 更新日限额超标金额总数
        /// </summary>
        public void AlterDayLimitTot()
        {           
            FS.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient.ucDayLimitTot uc = new FS.SOC.Local.RADT.GuangZhou.GYSY.Base.Inpatient.ucDayLimitTot();
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(uc);
        }

        /// <summary>
        /// 提示方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="msgIcon"></param>
        private void myMessageBox(string msg, MessageBoxIcon msgIcon)
        {
            CommonController.CreateInstance().MessageBox(this, msg, MessageBoxButtons.OK, msgIcon);
        }

        private int save()
        {
            //验证有效性,如果有不符合录入,那么中止方法
            if (!this.IRegister.IsInputValid())
            {
                this.ucQueryInpatientNo1.Select();
                this.ucQueryInpatientNo1.Focus();
                return -1;
            }
            this.patientInfo=  this.IRegister.GetPatientInfo(this.patientInfo);
            if (this.patientInfo == null)
            {
                return -1;
            }

            //修改后的住院号
            string patientNO = this.patientInfo.PID.PatientNO;
            int inTimes = this.patientInfo.InTimes;

            //使用原来的卡号和次数
            this.patientInfo.PID.PatientNO = this.patientInfoOld.PID.PatientNO;
            this.patientInfo.InTimes = this.patientInfoOld.InTimes;
            this.patientInfo.PID.CardNO = this.patientInfoOld.PID.CardNO;
            this.patientInfo.ID = this.patientInfoOld.ID;
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在保存信息...");
            Application.DoEvents();

            FS.SOC.HISFC.RADT.BizProcess.Inpatient inpatientBizProcess = new FS.SOC.HISFC.RADT.BizProcess.Inpatient();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            inpatientBizProcess.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (inpatientBizProcess.ModifyPatientInfo(patientInfo, true, patientNO, true, inTimes) < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                this.myMessageBox("保存失败，原因："+inpatientBizProcess.Err, MessageBoxIcon.Error);
                return -1;
            }

            if (isAllowModifyPact)
            {
                if (this.cmbNewPact.Tag != null && !string.IsNullOrEmpty(this.cmbNewPact.Tag.ToString()))
                {
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在修改病人身份信息...");
                    Application.DoEvents();
                    if (inpatientBizProcess.ModifyPatientPactInfo(patientInfo.Clone(), (FS.HISFC.Models.Base.PactInfo)this.cmbNewPact.SelectedItem) <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                        this.myMessageBox("保存失败，原因：" + inpatientBizProcess.Err, MessageBoxIcon.Error);
                        return -1;
                    }

                    patientInfo.Pact = (FS.HISFC.Models.Base.PactInfo)this.cmbNewPact.SelectedItem;
                }
            }

            if (InterfaceManager.GetPatientInfoISave().SaveCommitting(FS.SOC.HISFC.BizProcess.CommonInterface.Common.EnumSaveType.Update, patientInfo) <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.myMessageBox(InterfaceManager.GetPatientInfoISave().Err, MessageBoxIcon.Warning);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            return 1;
        }

        private int queryPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo, bool isQueryShow)
        {
            this.IQueryPatientInfo.Clear();
            this.IQueryPatientInfo.PatientInfo = patientInfo;
            if (isQueryShow == false || this.IQueryPatientInfo.Query(FS.SOC.HISFC.RADT.Interface.Patient.EnumQueryType.NameSex) != -1)
            {
                this.IQueryPatientInfo.Show(this);
            }

            if (!this.IQueryPatientInfo.IsSelect)
            {
                return 0;
            }
            if (this.IQueryPatientInfo.IsOldSystem)
            {
                if (!string.IsNullOrEmpty(patientInfo.ID))
                {
                    patientInfo.PID.PatientNO = this.IQueryPatientInfo.PatientInfo.PID.PatientNO;
                    this.patientInfoOld = this.patientInfo.Clone();
                    this.txtOldPact.Text = this.patientInfo.Pact.Name;
                    this.cmbNewPact.Tag = null;
                    this.IRegister.ModifyPatientInfo(this.patientInfo);
                }
            }
            else
            {
                this.ucQueryInpatientNo1.Text = this.IQueryPatientInfo.PatientInfo.PID.PatientNO;
                this.ucQueryInpatientNo1.query();
            }
            return 1;
        }
        #endregion

        #region 事件

        protected override void OnLoad(EventArgs e)
        {
            this.initInterface();
            this.initEvents();
            this.initForms();
            this.ucQueryInpatientNo1.Select();
            this.ucQueryInpatientNo1.Focus();

            base.OnLoad(e);
        }

        private void IRegisterInterface_OnSavePatientInfo(object sender, EventArgs e)
        {
            this.OnSave(sender, e);
        }

        void ucQueryInpatientNo1_myEvent()
        {
            if (this.ucQueryInpatientNo1.InpatientNo == null || this.ucQueryInpatientNo1.InpatientNo.Length == 0)
            {
                this.myMessageBox("住院患者不存在或者已出院", MessageBoxIcon.Error);
                this.clear();
                return;
            }

            //获取住院号赋值给实体
            this.patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);
            this.patientInfo.Patient.Kin = this.patientInfo.Kin;
            if (this.patientInfo == null)
            {
                this.myMessageBox("查找患者失败，原因：" + this.radtIntegrate.Err, MessageBoxIcon.Error);
                this.ucQueryInpatientNo1.Focus();
                return;
            }

            this.patientInfoOld = this.patientInfo.Clone();
            this.txtOldPact.Text = this.patientInfo.Pact.Name;
            this.cmbNewPact.Tag = null;
            this.IRegister.ModifyPatientInfo(this.patientInfo);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            if (this.save() > 0)
            {
                this.myMessageBox("保存成功", MessageBoxIcon.Information);
                this.clear();
            }

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// 查询患者信息
        /// </summary>
        /// <param name="patientInfo"></param>
        private int IRegisterInterface_OnQueryPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (string.IsNullOrEmpty(patientInfo.Name))
            {
                return -1;
            }
            string numberCheck = "0123456789";
            if (!numberCheck.Contains(patientInfo.Name.Substring(0, 1)))
            {
                return this.queryPatientInfo(patientInfo, true);
            }

            return 0;
        }

        #endregion
    }
}
