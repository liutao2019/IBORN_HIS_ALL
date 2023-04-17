using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Operation;
using System.Collections;
using FS.HISFC.Components.Common.Controls;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [功能描述: 手术收费]<br></br>
    /// [创 建 者: 王铁全]<br></br>
    /// [创建时间: 2006-12-20]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间='yyyy-mm-dd'
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucFeeForm :UserControl
    {
        public ucFeeForm()
        {
            InitializeComponent();
            if (!Environment.DesignMode)
                this.Clear();
        }
        #region 字段
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        
        FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
        //{F3C1935C-24E9-47a4-B7AE-4EA237A972C9} 
        FS.HISFC.Models.RADT.PatientInfo patientInfo = null;


        /// <summary>
        /// 是否显示比例项{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        private bool isShowFeeRate = false;

        /// <summary>
        /// 是否只显示手术医嘱  增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
        /// </summary>
        private bool isOnlyUO = false;

        /// <summary>
        /// 手术收费是否必须关联医嘱 增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
        /// </summary>
        private bool isNeedUOOrder = false;

        /// <summary>
        /// 手术收费时是否更新操作员信息
        /// </summary>
        private bool isUpdateOpsOper = false;


        //{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
        ucRegistrationTree.EnumListType listType;

        //[Category("控件设置"), Description("控件类型：麻醉还是收费")]
        public ucRegistrationTree.EnumListType ListType
        {
            get
            {
                return listType;
            }
            set
            {
                this.listType = value;
                this.InitControlName();
            }
        }

       

        #endregion
        #region 属性

        /// <summary>
        /// 不加载的药品性质// {4D67D981-6763-4ced-814E-430B518304E2}
        /// </summary>
        [Category("控件设置"), Description("不加载的药品性质，用“,”区分开")]
        public string NoAddDrugQuality
        {
            get
            {
                return this.ucInpatientCharge1.NoAddDrugQuality;
            }
            set
            {
                this.ucInpatientCharge1.NoAddDrugQuality = value;
            }
        }
        /// <summary>
        /// 手术收费时是否更新操作员信息
        /// </summary>
        [Category("控件设置"), Description("设置 手术收费时是否更新操作员信息 ")]
        public bool IsUpdateOpsOper
        {
            get
            {
                return isUpdateOpsOper;
            }
            set
            {
                isUpdateOpsOper = value;
            }
        }

        /// <summary>
        /// 手术收费是否必须关联医嘱 增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
        /// </summary>
        [Category("控件设置"), Description("设置 手术收费是否必须关联医嘱 ")]
        public bool IsNeedUOOrder
        {
            get
            {
                return
                    isNeedUOOrder;
            }
            set
            {
                isNeedUOOrder = value;
            }
        }

        /// <summary>
        /// 是否只显示手术医嘱 增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
        /// </summary>
        [Category("控件设置"), Description("设置 是否只显示手术医嘱 ")]
        public bool IsOnlyUO
        {
            get
            {
                return isOnlyUO;
            }
            set
            {
                isOnlyUO = value;
            }
        }

        [Category("控件设置"), Description("设置该控件加载的项目类别 药品:drug 非药品 undrug 所有: all")]
        public FS.HISFC.Components.Common.Controls.EnumShowItemType 加载项目类别
        {
            get
            {
                return ucInpatientCharge1.加载项目类别;
            }
            set
            {
                ucInpatientCharge1.加载项目类别 = value;
            }
        }
        /// <summary>
        /// 控件功能
        /// </summary>
        [Category("控件设置"), Description("获得或者设置该控件的主要功能"), DefaultValue(1)]
        public FS.HISFC.Components.Common.Controls.ucInpatientCharge.FeeTypes 控件功能
        {
            get
            {
                return this.ucInpatientCharge1.控件功能;
            }
            set
            {
                this.ucInpatientCharge1.控件功能 = value;
            }
        }

        /// <summary>
        /// 是否可以收费或者划价0单价的项目
        /// </summary>
        [Category("控件设置"), Description("获得或者设置是否可以收费或者划价"), DefaultValue(false)]
        public bool IsChargeZero
        {
            get
            {
                return this.ucInpatientCharge1.IsChargeZero;
            }
            set
            {
                this.ucInpatientCharge1.IsChargeZero = value;
            }
        }

        /// <summary>
        /// 是否显示比例项{2C7FCD3D-D9B4-44f5-A2EE-A7E8C6D85576}
        /// </summary>
        [Category("控件设置"), Description("是否显示比例项"), DefaultValue(false)]
        public bool IsShowFeeRate
        {
            get { return this.ucInpatientCharge1.IsShowFeeRate; }
            set
            {
                 
                this.ucInpatientCharge1.IsShowFeeRate = value;
            }
        }

        [Category("控件设置"), Description("是否判断欠费,Y：判断欠费，不允许继续收费,M：判断欠费，提示是否继续收费,N：不判断欠费")]
        public FS.HISFC.Models.Base.MessType MessageType
        {
            get
            {
                return this.ucInpatientCharge1.MessageType;
            }
            set
            {
                ucInpatientCharge1.MessageType = value;
            }
        }
        [Category("控件设置"), Description("数量为零是否提示和允许保存")]
        public bool IsJudgeQty
        {
            get
            {
                return this.ucInpatientCharge1.IsJudgeQty;
            }
            set
            {
                this.ucInpatientCharge1.IsJudgeQty = value;
            }
        }

        [Category("控件设置"), Description("执行科室是否默认为登陆科室")]
        public bool DefaultExeDeptIsDeptIn
        {
            get
            {
                return this.ucInpatientCharge1.DefaultExeDeptIsDeptIn;
            }
            set
            {
                this.ucInpatientCharge1.DefaultExeDeptIsDeptIn = value;
            }
        }
        // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
        public OperationAppllication operationAppllication = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public OperationAppllication OperationAppllication
        {
            set
            {
                if (value == null)
                {
                    this.Clear();
                    return;
                }

                //this.PatientInfo = value.PatientInfo;
                FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                patientInfo = radtManager.GetPatientInfomation(value.PatientInfo.ID);
                if (patientInfo == null)
                {
                    MessageBox.Show("找不到患者住院信息！");
                    this.Clear();
                    return;
                }
                PatientInfo = patientInfo;
                if (value.IsHeavy)
                    this.lbOwn.Text = "同意使用自费项目";
                else
                    this.lbOwn.Text = "不同意使用自费项目";
                OperationAppllication apply = value;
                this.cmbDoctor.Tag = apply.OperationDoctor.ID;//{F3C1935C-24E9-47a4-B7AE-4EA237A972C9};
                
                foreach(FS.HISFC.Models.Operation.ArrangeRole role in apply.RoleAl)
                {
                    if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse1.ToString())
                    {
                        this.ncmbScrubNurse.Tag = role.ID;
                        continue;
                    }
                    if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse1.ToString())
                    {
                        this.ncmbTourNurse.Tag = role.ID;
                        continue;
                    }
                    if (this.listType == ucRegistrationTree.EnumListType.Anaesthesia)
                    {
                        if (role.RoleType.ID.ToString() == EnumOperationRole.Anaesthetist.ToString())
                        {
                            this.cmbDoctor.Tag = role.ID;
                            continue;
                        }
                    }
                }
                //{7AC68FBC-6BB7-4c66-B24F-4E0E3474A531}              
                if (this.cmbDoctor.Tag != null && this.cmbDoctor.Tag!="")
                {
                    this.ucInpatientCharge1.RecipeDoctCode = this.cmbDoctor.Tag.ToString();
                }
                else
                {
                    this.ucInpatientCharge1.RecipeDoctCode = Environment.OperatorID;
                }
                // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
                operationAppllication = value;
                //显示暂存数据
                try
                {
                    this.SetValue();
                }
                catch
                {

                }
            }
            // 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
            get
            {
                return this.operationAppllication;
            }
        }

        ////{9B275235-0854-461f-8B7B-C4FE6EC6CC0B}
        private void InitControlName()
        {
            if (this.listType == ucRegistrationTree.EnumListType.Anaesthesia)
            {
                this.neuLabel3.Text = "麻醉医生";
                this.neuLabel1.Text = "麻醉收费";
            }
        }
      
        private FS.HISFC.Models.RADT.PatientInfo PatientInfo
        {
            set
            {
                try
                {
                    if (value.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                    {
                        MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者不是在院状态，不能进行收费操作"));
                        this.Clear();

                        return;
                    }
                }
                catch (Exception ee)
                {
                    MessageBox.Show(ee.Message.ToString());
                    return;
                }

                #region {2FA6F515-92FA-4251-8573-8DD5FC1E5E1C}
                this.lblBedNO.Text = value.PVisit.PatientLocation.Bed.ID.Substring(4);
                this.lblNurseCell.Text = value.PVisit.PatientLocation.NurseCell.Name;
                #endregion
                this.ucInpatientCharge1.Clear();
                this.lbName.Text = value.Name;
                this.lbAge.Text = value.Age;
                this.lbSex.Text = value.Sex.Name;
                this.lbPatient.Text = value.PID.PatientNO;
                this.lbDept.Text = value.PVisit.PatientLocation.Dept.Name;
                this.lbPayKind.Text = Environment.GetPayKind(value.Pact.PayKind.ID).Name;
                //this.ucInpatientCharge1.RecipeDoctCode = Environment.OperatorID;
                //为了调整手术室批费后的科室问题
                this.ucInpatientCharge1.RecipeDept = Environment.OperatorDept;
                this.cmbDept.Tag = Environment.OperatorDept.ID;
                //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                    this.ucInpatientCharge1.DefautStockDept = new FS.FrameWork.Models.NeuObject();
                    this.ucInpatientCharge1.DefautStockDept.ID = this.cmbStockDept.Tag.ToString();
                //增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱

                FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();
                ArrayList alOrder = orderIntegrate.QueryOrder(value.ID, FS.HISFC.Models.Order.EnumType.SHORT, 1);
                ArrayList alOrder2 = orderIntegrate.QueryOrder(value.ID, FS.HISFC.Models.Order.EnumType.SHORT, 2);
                alOrder.AddRange(alOrder2);

                ArrayList alUO = new ArrayList();

                foreach (FS.HISFC.Models.Order.Inpatient.Order ord in alOrder)
                {
                    FS.FrameWork.Models.NeuObject neuObj = new FS.FrameWork.Models.NeuObject(); 

                    neuObj.ID = ord.ID;
                    neuObj.Name = ord.Item.Name;

                    if (isOnlyUO)
                    {
                        if (ord.Item.SysClass.ID.ToString() == "UO")
                        {                           
                            alUO.Add(neuObj);                            
                        }
                    }
                    else
                    {
                        alUO.Add(neuObj);
                    }
                }

                this.cmbOrder.Tag = null;
                this.cmbOrder.Text = "";
                this.cmbOrder.AddItems(alUO);
                
                
                this.ucInpatientCharge1.PatientInfo = value;
              

                this.cmbDoctor.Focus();
            }
        }

        private string defaultStorageDept=string.Empty;
        public string DefaultStorageDept
        {
            get
            {
                return defaultStorageDept;
            }
            set
            {
                defaultStorageDept = value;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 清空
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            #region {2FA6F515-92FA-4251-8573-8DD5FC1E5E1C}
            this.lblBedNO.Text = "";
            this.lblNurseCell.Text = "";
            #endregion
            this.lbPayKind.Text = "";
            this.lbName.Text = "";
            this.lbPatient.Text = "";
            this.lbDept.Text = "";
            this.lbSex.Text = "";
            this.lbOwn.Text = "";
            this.lbAge.Text = string.Empty;
            this.lbDate.Text = Environment.OperationManager.GetDateTimeFromSysDateTime().ToString("yyyy-MM-dd");

            //appObj = new FS.HISFC.Object.Operator.OpsApplication();
            this.checkBox1.Checked = false;
            this.checkBox2.Checked = false;
            this.checkBox3.Checked = false;
            this.checkBox4.Checked = false;
            this.checkBox5.Checked = false;

            this.ucInpatientCharge1.Clear();
            this.cmbDoctor.Tag = "";
            this.ncmbScrubNurse.Tag = "";
            this.ncmbTourNurse.Tag = "";
            //增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
            this.cmbOrder.Tag = null;
            this.cmbOrder.Text = "";
            //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
            if (this.cmbStockDept.Items.Count > 0)
            {
                this.cmbStockDept.Tag = defaultStorageDept;
            }
        }
        /// <summary>
        /// 保存，进行收费
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            //// 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
            this.ucInpatientCharge1.OperationNO = this.operationAppllication.ID;
            //>>{74326E54-1315-4c07-A53E-7E515C364015}获取选择的取药科室20120730kjl
            this.ucInpatientCharge1.DefautStockDept.ID = this.cmbStockDept.Tag.ToString();
            //<<

            //增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
            if (this.isNeedUOOrder)
            {
                if ( this.cmbOrder == null || string.IsNullOrEmpty(this.cmbOrder.Tag.ToString()))
                {
                    MessageBox.Show("请选择本次收费关联的手术医嘱.");

                    return -1;
                }
            }
    
            this.ucInpatientCharge1.OrderID = this.cmbOrder.Tag.ToString();
            this.ucInpatientCharge1.IsPopSHowInvoice = true;
            this.ucInpatientCharge1.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("020");
            ucInpatientCharge1.RecipeDoctCode = cmbDoctor.Tag.ToString();
            if (this.ucInpatientCharge1.Save() < 0) return -1;
            #region 更新手术人员信息
            if (this.IsUpdateOpsOper)
            {
                if (this.OperationAppllication != null && !string.IsNullOrEmpty(this.OperationAppllication.ID))
                {
                    FS.HISFC.Models.Operation.OperationAppllication applicationInfo = Environment.OperationManager.GetOpsApp(this.OperationAppllication.ID);
                    if(this.cmbDoctor.SelectedIndex != -1)
                    {
                        applicationInfo.OperationDoctor.ID = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                        applicationInfo.OperationDoctor.Name = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                    }
                    bool iscontainWashingHandNurse1 = false;
                    bool iscontainItinerantNurse1 = false;
                    FS.HISFC.Models.Operation.ArrangeRole roleTmp = null;
                    foreach (FS.HISFC.Models.Operation.ArrangeRole role in applicationInfo.RoleAl)
                    {
                        if (role.RoleType.ID.ToString() == EnumOperationRole.Operator.ToString())
                        {
                            roleTmp = (FS.HISFC.Models.Operation.ArrangeRole)role.Clone();
                            if (this.cmbDoctor.SelectedIndex != -1)
                            {
                                role.ID = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                                role.Name = (this.cmbDoctor.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                            }
                        }
                        if (role.RoleType.ID.ToString() == EnumOperationRole.WashingHandNurse1.ToString())
                        {
                            iscontainWashingHandNurse1 = true;
                            if (this.ncmbScrubNurse.SelectedIndex != -1)
                            {
                                role.ID = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                                role.Name = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                            }
                        }
                        if (role.RoleType.ID.ToString() == EnumOperationRole.ItinerantNurse1.ToString())
                        {
                            iscontainItinerantNurse1 = true;
                            if (this.ncmbTourNurse.SelectedIndex != -1)
                            {
                                role.ID = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                                role.Name = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                            }
                        }
                    }
                    if (!iscontainItinerantNurse1 && this.ncmbTourNurse.SelectedIndex != -1)
                    {
                        FS.HISFC.Models.Operation.ArrangeRole arranyRole = (FS.HISFC.Models.Operation.ArrangeRole)roleTmp.Clone();
                        arranyRole.RoleType.ID = EnumOperationRole.ItinerantNurse1;
                        arranyRole.ID = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                        arranyRole.Name = (this.ncmbTourNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                        applicationInfo.RoleAl.Add(arranyRole);
                    }

                    if (!iscontainWashingHandNurse1 && this.ncmbScrubNurse.SelectedIndex != -1)
                    {
                        FS.HISFC.Models.Operation.ArrangeRole arranyRole = (FS.HISFC.Models.Operation.ArrangeRole)roleTmp.Clone();
                        arranyRole.RoleType.ID = EnumOperationRole.WashingHandNurse1;
                        arranyRole.ID = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).ID;
                        arranyRole.Name = (this.ncmbScrubNurse.SelectedItem as FS.FrameWork.Models.NeuObject).Name;
                        applicationInfo.RoleAl.Add(arranyRole);
                    }
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (Environment.OperationManager.UpdateApplicationByFee(applicationInfo) < 1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    FS.FrameWork.Management.PublicTrans.Commit();
                }
            }
            #endregion

            MessageBox.Show(this.ucInpatientCharge1.SucessMsg);



            this.Print();

            this.ucInpatientCharge1.Clear();
            this.Clear();
            return 1;
        }
        #region 暂存处理 2012-12-07 chengym
        /// <summary>
        /// 暂存
        /// </summary>
        /// <returns></returns>
        public int TempSave()
        {
            //// 手术编码{0604764A-3F55-428f-9064-FB4C53FD8136}
            this.ucInpatientCharge1.OperationNO = this.operationAppllication.ID;
            //>>{74326E54-1315-4c07-A53E-7E515C364015}获取选择的取药科室20120730kjl
            this.ucInpatientCharge1.DefautStockDept.ID = this.cmbStockDept.Tag.ToString();
            //<<

            //增加选择手术医嘱功能  {B9932D8E-ACFF-4a90-A252-B99136DD5285} 手术收费关联手术医嘱
            if (this.isNeedUOOrder)
            {
                if (this.cmbOrder == null || string.IsNullOrEmpty(this.cmbOrder.Tag.ToString()))
                {
                    MessageBox.Show("请选择本次收费关联的手术医嘱.");

                    return -1;
                }
            }

            this.ucInpatientCharge1.OrderID = this.cmbOrder.Tag.ToString();
            this.ucInpatientCharge1.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("020");
            if (this.ucInpatientCharge1.TemparorySave() < 0) return -1;
            //MessageBox.Show(this.ucInpatientCharge1.SucessMsg);
            this.ucInpatientCharge1.Clear();
            this.Clear();

            return 1;
        }
        /// <summary>
        /// 显示暂存
        /// </summary>
        /// <returns></returns>
        private int SetValue()
        {
            this.ucInpatientCharge1.OperationNO = this.operationAppllication.ID; 
            if (this.ucInpatientCharge1.SetValue() == -1)
            {
                return -1;
            }
            return 0;
        }
        #endregion
        /// <summary>
        /// 删除当前行
        /// </summary>
        /// <returns></returns>
        public int DelRow()
        {
            return this.ucInpatientCharge1.DelRow();
        }
        //{9F3CF1C0-AF96-4d17-96B1-6B34636A42A7}


        public void InsertGroup(string groupID)
        {
            frmChooseGroupDetails frm = new frmChooseGroupDetails();
            frm.GroupID = groupID;
          DialogResult dr =   frm.ShowDialog();
          if (dr == DialogResult.Cancel)
          {
              return;
          }
          else
          {
              FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载组套信息");
              Application.DoEvents();
              this.ucInpatientCharge1.AddGroupDetail(groupID,frm.AlReturnDetails); 



              FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
          }

            frm.Dispose();

            //FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载组套信息");
            //Application.DoEvents();
            ////this.ucInpatientCharge1.AddGroupDetail(groupID); 

            

            //FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        public int Print()
        {
            return this.print.PrintPreview(this);
        }
        #endregion

        #region 事件
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!Environment.DesignMode)
            {
                if (!this.IsUpdateOpsOper)
                {
                    this.neuLabel15.Visible = false;
                    this.neuLabel16.Visible = false;
                    this.ncmbScrubNurse.Visible = false;
                    this.ncmbTourNurse.Visible = false;
                }
                FS.HISFC.Models.Base.Employee em = (FS.HISFC.Models.Base.Employee)Environment.AnaeManager.Operator;
                this.ucInpatientCharge1.Init(em.Dept.ID);
                //添加手术医生 {F3C1935C-24E9-47a4-B7AE-4EA237A972C9}
                FS.HISFC.BizProcess.Integrate.Manager conMag = new FS.HISFC.BizProcess.Integrate.Manager();

                ArrayList aNurse = conMag.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N, ((FS.HISFC.Models.Base.Employee)Environment.AnaeManager.Operator).Dept.ID);
                if (aNurse != null)
                {
                    this.ncmbScrubNurse.AddItems(aNurse);
                    this.ncmbTourNurse.AddItems(aNurse);
                }
                ArrayList alDept = conMag.GetDepartment();
                if (alDept != null)
                {
                    this.cmbDept.AddItems(alDept);
                }
                //{CEA2C5D7-4816-4eec-9914-B4C20E6C998F}
                ArrayList alStockDept = conMag.GetDeptmentByType(FS.HISFC.Models.Base.EnumDepartmentType.P.ToString());
                if (alStockDept != null)
                {
                    FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = "default";
                    obj.Name = "默认药房";
                    alStockDept.Insert(0,obj);
                    this.cmbStockDept.AddItems(alStockDept);
                    this.cmbStockDept.Tag = defaultStorageDept;
                }
            }
        }

        public void InitDoctList()
        {
            FS.HISFC.BizProcess.Integrate.Manager conMag = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList aDoc = new ArrayList();
            if (this.listType == ucRegistrationTree.EnumListType.Anaesthesia)
            {
                aDoc = conMag.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            }
            else
            {
                aDoc = conMag.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            }

            if (aDoc != null)
            {
                this.cmbDoctor.AddItems(aDoc);
            }
        }

        #endregion
        //{F3C1935C-24E9-47a4-B7AE-4EA237A972C9} 
        private void cmbDoctor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.patientInfo != null)
            {
                this.patientInfo.PVisit.AdmittingDoctor.ID = this.cmbDoctor.Tag.ToString();
                this.patientInfo.PVisit.AdmittingDoctor.Name = this.cmbDoctor.Text;
                this.ucInpatientCharge1.RecipeDoctCode = this.cmbDoctor.Tag.ToString();
                this.ucInpatientCharge1.RecipeDept = ((FS.HISFC.Models.Base.Employee)this.cmbDoctor.SelectedItem).Dept;
            }
        }
        //{F3C1935C-24E9-47a4-B7AE-4EA237A972C9} 
        private void cmbDoctor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ucInpatientCharge1.Focus();
            }


        }

        private void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.FrameWork.Models.NeuObject obj = null;
            try
            {
                obj = this.cmbDept.SelectedItem as FS.FrameWork.Models.NeuObject;
            }
            catch (Exception)
            {

                return;
            }



            this.ucInpatientCharge1.RecipeDept = obj;

        }
    }
}
