using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
//using FS.Emr.Path.HIS50;
using FS.HISFC.Models.Order;

namespace FS.HISFC.WinForms.WorkStation
{
    /// <summary>
    /// 医嘱管理主窗口
    /// </summary>
    public partial class frmOrder : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        #region 变量

        /// <summary>
        /// 当前控件
        /// </summary>
        private Control CurrentControl;

        /// <summary>
        /// 查询患者列表
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucQueryInpatient ucQuerypatient = new FS.HISFC.Components.Common.Controls.ucQueryInpatient();

        /// <summary>
        /// 传染病上报类
        /// </summary>
        private FS.HISFC.BizProcess.Interface.DCP.IDCP dcpInstance = null;

        /// <summary>
        /// 电子病历对外接口
        /// </summary>
        private FS.HISFC.BizProcess.Interface.EMR.IEMR emrInstance = null;

        /// <summary>
        /// 组套树列表
        /// </summary>
        FS.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;

        /// <summary>
        /// lis医嘱接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.ILis IResultPrint = null;

        /// <summary>
        /// 住院医生站选择检验项目接口
        /// </summary>
        private SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IOrderChooseUL IOrderChooseUl = null;

        /// <summary>
        /// 是否签入电子病历医嘱
        /// </summary>
        private bool isEMROrder = false;

        /// <summary>
        /// 是否签入电子病历医嘱
        /// </summary>
        public bool IsEMROrder
        {
            get
            {
                return isEMROrder;
            }
            set
            {
                isEMROrder = value;
                this.panel2.Visible = !isEMROrder;
            }
        }

        /// <summary>
        /// 临床路径
        /// </summary>
        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;

        /// <summary>
        /// 是否编辑组套模式
        /// </summary>
        bool isEditGroup = false;

        bool isInit = false;

        /// <summary>
        /// 是否启用电子申请单功能
        /// </summary>
        private bool isPacsApplyEnable = false;

        #endregion

        /// <summary>
        /// 医嘱管理
        /// </summary>
        public frmOrder()
        {

            InitializeComponent();

            if (!DesignMode)
            {
                this.SetTree(this.tvDoctorPatientList1);
                this.iQueryControlable = this.ucOrder1 as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.ucOrder1 as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.ucOrder1;
                this.tbMTOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.Y预约);
                this.tbInfectionReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.S手动录入);
                this.tbGroup.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z组套);
                this.tbEMR.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B病历);
                this.tbEMR.Text = "病历";
                this.tbQueryOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C查询);
                this.tbExitOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出);
                this.tbPrintOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
                this.tbRecipePrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);

                this.tbGroup.CheckState = CheckState.Unchecked;
                this.Resize += new EventHandler(frmOrder_Resize);
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                this.ucOrder1.refreshGroup += new FS.HISFC.Components.Order.Controls.RefreshGroupTree(ucOrder1_refreshGroup);
                this.neuTextBox1.MouseDoubleClick += new MouseEventHandler(neuTextBox1_MouseDoubleClick);

                this.panel2.Visible = !isEMROrder;
            }
        }

        /// <summary>
        /// 双击可以选择开立 用于手术室开立医嘱等
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void neuTextBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            TreeNode parent = new TreeNode();

            foreach (TreeNode node in this.tvDoctorPatientList1.Nodes)
            {
                if (node.Text.IndexOf("本科室") >= 0)
                {
                    parent = node;
                    break;
                }
            }

            if (FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucQuerypatient) == DialogResult.OK)
            {
                TreeNode node = new TreeNode();

                if (FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.PVisit.PatientLocation.Dept.ID
                    == (this.consultation.Operator as FS.HISFC.Models.Base.Employee).Dept.ID)
                {
                    //FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Patient.Memo = "科室";
                }
                else
                {
                    //FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Patient.Memo = "医技";
                }

                node.Tag = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient;
                node.Text = FS.HISFC.Components.Common.Controls.ucQueryInpatient.patient.Name;

                parent.Nodes.Add(node);
                parent.ExpandAll();
            }
        }

        /// <summary>
        /// 刷新组套树
        /// </summary>
        private void ucOrder1_refreshGroup()
        {
            this.tvGroup.RefrshGroup();
        }

        void frmOrder_Resize(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.panelTree.Height = this.Height - 162;
            }
        }

        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 初始化加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrder_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                this.WindowState = FormWindowState.Maximized;
                this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
                tbBatchDeal.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked_1);
                this.AddOrderHandle();
                this.initButton(false);

                //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
                this.tbPackage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T套餐);
                this.tbItem.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T套餐);
                this.tbAddOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱);
                this.tbComboOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H合并);
                this.tbCancelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Q取消);
                this.tbDelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S删除);
                this.tbOperation.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z诊断);
                this.tbSaveOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B保存);
                this.tbCheck.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H换单);
                this.tb1Exit.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出);
                this.tbEMR.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B病历);
                this.tsbHerbal.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M明细);
                /// 增加检验项目选择按钮
                this.tbChooseUL.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M默认);
                ///{FB86E7D8-A148-4147-B729-FD0348A3D670}  增加医嘱重整按钮
                this.tbRetidyOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱);

                this.tbLevelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z子级);

                this.tbAssayCure.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H护理);

                this.tbDiseaseReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J健康档案);
                this.tbFilter.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C查找);

                this.tbChooseDoct.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H换医师);

                this.tbLisResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H化验);
                this.tbResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H化验);

                this.tbPacsResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S设备);
                this.tbDcAllLongOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Q全退);
                this.tbPass.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B报警);
                this.tbPrintAgain.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印预览);
                this.tbMSG.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X信息); //{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
                this.panelTree.Height = this.Height - 162;
                this.toolStrip1.Focus();
                string treeSearchMode = "1";

                treeSearchMode = controlParamMgr.GetControlParam<string>("200303", true, "1");

                if (treeSearchMode == "2")
                {
                    this.ucQueryInpatientNo1.Visible = true;
                }
                else
                {
                    this.ucQueryInpatientNo1.Visible = false;
                }

                this.SetMenuVisible();


                this.IsDoubleSelectValue = false;

                this.ucOrder1.OnRefreshGroupTree += new EventHandler(ucOrder1_OnRefreshGroupTree);

                Application.DoEvents();

                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.IOrderChooseUl == null)
                {
                    this.IOrderChooseUl = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IOrderChooseUL)) as FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IOrderChooseUL;
                }

                if (this.dcpInstance != null)
                {
                    this.dcpInstance.LoadNotice(this, FS.HISFC.Models.Base.ServiceTypes.I);
                }

                #region  add by lijp 2011-11-25 电子申请单添加 {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

                this.tblEditPacsApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S申请单);

                try
                {
                    isPacsApplyEnable = controlParamMgr.GetControlParam<bool>("PACSZY");
                }
                catch
                {
                    isPacsApplyEnable = false;
                }

                #endregion

                try
                {
                    FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                    FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(consultation.Operator.ID);

                    if (setting == null)
                    {
                        //MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        this.tbPass.Checked = FS.FrameWork.Function.NConvert.ToBoolean(setting.Setting4);
                        this.ucOrder1.EnabledPass = this.tbPass.Checked;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        void ucOrder1_OnRefreshGroupTree(object sender, EventArgs e)
        {
            this.tvGroup.RefrshGroup();
        }

        #region 私有函数

        /// <summary>
        /// 菜单设置可见性
        /// </summary>
        private void SetMenuVisible()
        {
            FS.HISFC.BizProcess.Integrate.Manager dictionaryMgr = new FS.HISFC.BizProcess.Integrate.Manager();

            ArrayList alset = dictionaryMgr.GetConstantList("INPATMENU");

            FS.FrameWork.Public.ObjectHelper setHelper = new FS.FrameWork.Public.ObjectHelper();

            setHelper.ArrayObject = alset;

            ToolStripButton tButton = null;
            ToolStripDropDownButton tdButton = null;

            Hashtable hsTb = new Hashtable();
            foreach (object obj in toolStrip1.Items)
            {
                if (obj.GetType() == typeof(ToolStripButton))
                {
                    tButton = obj as ToolStripButton;
                    if (!hsTb.Contains(tButton.Name))
                    {
                        hsTb.Add(tButton.Name, tButton);
                    }
                }
                else if (obj.GetType() == typeof(ToolStripDropDownButton))
                {
                    tdButton = obj as ToolStripDropDownButton;
                    if (!hsTb.Contains(tdButton.Name))
                    {
                        hsTb.Add(tdButton.Name, tdButton);
                    }

                    if (tdButton.DropDownItems.Count > 0)
                    {
                        foreach (ToolStripDropDownItem o in tdButton.DropDownItems)
                        {
                            if (!hsTb.Contains(o.Name))
                            {
                                hsTb.Add(o.Name, o);
                            }
                        }
                    }
                }
            }

            this.toolStrip1.Items.Clear();
            foreach (FS.HISFC.Models.Base.Const conObj in alset)
            {
                if (conObj.IsValid && hsTb.Contains(conObj.ID.Trim()))
                {
                    if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripButton))
                    {
                        tButton = new ToolStripButton();
                        tButton = hsTb[conObj.ID.Trim()] as ToolStripButton;

                        tButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(setHelper.GetObjectFromID(tButton.Name).Memo))
                        {
                            tButton.Text = tButton.Text + "(&" + setHelper.GetObjectFromID(tButton.Name).Memo + ")";
                        }
                        tButton.Visible = true;
                        this.toolStrip1.Items.Add(tButton);
                    }
                    else if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripDropDownButton))
                    {
                        tdButton = new ToolStripDropDownButton();
                        tdButton = hsTb[conObj.ID.Trim()] as ToolStripDropDownButton;

                        tdButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(setHelper.GetObjectFromID(tdButton.Name).Memo))
                        {
                            tdButton.Text = tdButton.Text + "(&" + setHelper.GetObjectFromID(tdButton.Name).Memo + ")";
                        }
                        tdButton.Visible = true;

                        #region

                        //自定义下拉按钮
                        if (!string.IsNullOrEmpty(conObj.UserCode)
                            && string.Equals(conObj.UserCode, "ResultPrint"))
                        {
                            ArrayList dropItemList = dictionaryMgr.GetConstantList(conObj.UserCode);
                            if (dropItemList.Count > 0)
                            {
                                tdButton.DropDownItems.Clear();
                                System.Windows.Forms.ToolStripMenuItem drop;
                                foreach (FS.HISFC.Models.Base.Const item in dropItemList)
                                {
                                    if (item.IsValid)
                                    {
                                        drop = new ToolStripMenuItem();
                                        drop.Name = item.ID;
                                        drop.Text = item.Name;
                                        drop.Tag = item.Memo;
                                        drop.Click += new System.EventHandler(this.tmResultPrint_Click);
                                        tdButton.DropDownItems.Add(drop);
                                    }
                                }
                            }
                        }
                        else
                        {
                            ArrayList dropItemList = dictionaryMgr.GetConstantList(conObj.UserCode);
                            if (dropItemList.Count > 0)
                            {
                                tdButton.DropDownItems.Clear();
                                System.Windows.Forms.ToolStripMenuItem drop;
                                foreach (FS.HISFC.Models.Base.Const item in dropItemList)
                                {
                                    if (conObj.IsValid)
                                    {
                                        drop = new ToolStripMenuItem();
                                        drop.Text = item.Name;
                                        drop.Click += new System.EventHandler(this.tmPrint_Click);
                                        tdButton.DropDownItems.Add(drop);
                                    }
                                }
                            }
                        }
                        #endregion

                        this.toolStrip1.Items.Add(tdButton);
                    }

                    if (conObj.UserCode.Trim() == "间隔符")
                    {
                        this.toolStrip1.Items.Add(new ToolStripSeparator());
                    }
                }
            }
        }

        /// <summary>
        /// 医疗结果查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmResultPrint_Click(object sender, EventArgs e)
        {
            FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType resultType = (FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType)FS.FrameWork.Function.NConvert.ToInt32(((ToolStripMenuItem)sender).Name);

            ucOrder1.QueryMedicalResult(resultType);
        }

        private void initButton(bool isDisign)
        {
            this.tbGroup.Enabled = !isDisign;

            if (isEMROrder)
            {
                if (isDisign)
                {
                    panel2.Visible = true;
                }
                else if (tbGroup.Enabled && tbGroup.CheckState == CheckState.Checked)
                {
                    this.panel2.Visible = true;
                }
                else
                {
                    this.panel2.Visible = false;
                }
            }
            else
            {
                this.panel2.Visible = true;
            }       

            tbRefresh.Enabled = !isDisign;
            this.tbAddOrder.Enabled = !isDisign;
            this.tbPrintOrder.Enabled = !isDisign;
            this.tbRecipePrint.Enabled = !isDisign;
            this.tbResultPrint.Enabled = !isDisign;
            this.tbComboOrder.Enabled = isDisign;
            this.tbCancelOrder.Enabled = isDisign;
            this.tbCheck.Enabled = isDisign;
            //this.tbOperation.Enabled = false;
            this.tbOperation.Enabled = isDisign;
            this.tblEditPacsApply.Enabled = !isDisign;
            this.tbPrintAgain.Enabled = !isDisign;
            this.tbAssayCure.Enabled = isDisign;
            this.tbDelOrder.Enabled = isDisign;
            this.tbExitOrder.Enabled = isDisign;
            //this.tbFilter.Enabled = !isDisign;
            this.tbQueryOrder.Enabled = !isDisign;
            this.tbSaveOrder.Enabled = isDisign;
            this.tsbHerbal.Enabled = isDisign;
            this.tbChooseUL.Enabled = isDisign;
            this.tbLevelOrder.Enabled = isDisign;
            this.tbDcAllLongOrder.Enabled = !isDisign;
            //{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            this.tbChooseDoct.Enabled = isDisign;
            if (isDisign) //开立
            {
                if (tvGroup == null)
                {
                    tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.Init();

                    tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                if (ucOrder1.Patient != null)
                {
                    tvGroup.Pact = ucOrder1.Patient.Pact;
                }
                else
                {
                    tvGroup.Pact = null;
                }

                this.tvDoctorPatientList1.Visible = false;
                this.panelTree.Controls.Add(tvGroup);
                //{D5517722-7128-4d0c-BBC4-1A5558A39A03}
                //判断当前人员是否医生
                if (((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).EmployeeType.ID.ToString() == FS.HISFC.Models.Base.EnumEmployeeType.D.ToString())
                {
                    this.tbChooseDoct.Enabled = false;
                }
                else
                {
                    this.tbChooseDoct.Enabled = true;
                }
                if (this.ucOrder1.OrderType == FS.HISFC.Models.Order.EnumType.LONG)
                {
                    this.tsbHerbal.Enabled = false;
                    this.tbChooseUL.Enabled = false;
                    this.tbLevelOrder.Enabled = false;
                    this.tbOperation.Enabled = false;
                    this.tblEditPacsApply.Enabled = !isDisign;
                }
            }
            else
            {
                this.tvDoctorPatientList1.Visible = true;
                if (tvGroup != null)
                    tvGroup.Visible = false;
            } 
        }

        /// <summary>
        /// 组套添加
        /// </summary>
        /// <param name="alOrders"></param>
        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            this.ucOrder1.AddGroupOrder(alOrders);
        }

        /// <summary>
        /// 单据打印按钮实现（治疗、处方、指引、检验）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmPrint_Click(object sender, EventArgs e)
        {
           this.ucOrder1.PrintAgain(((ToolStripMenuItem)sender).Text);
        }

        /// <summary>
        /// 设置按钮
        /// </summary>
        /// <param name="isEdit">是否开立模式</param>
        private void InitButtonGroup(bool isEdit)
        {
            this.tbAddOrder.Enabled = !isEdit;
            this.tbPrintOrder.Enabled = !isEdit;
            this.tbRecipePrint.Enabled = !isEdit;
            this.tbResultPrint.Enabled = !isEdit;
            this.tbSaveOrder.Enabled = isEdit;
            this.tbRefresh.Enabled = !isEdit;
            this.isEditGroup = isEdit;
            //{EB959BC4-9120-478a-B527-74A1D7EF4C9E}
            this.tbComboOrder.Enabled = isEdit;
            this.tbCancelOrder.Enabled = isEdit;
            //{74E478F5-BDDD-4637-9F5A-E251AF9AA72F}
            this.tbRetidyOrder.Enabled = !isEdit;
            this.tbDcAllLongOrder.Enabled = !isEdit;
            this.tbDelOrder.Enabled = isEdit;

            if (isEdit) //开立
            {
                if (tvGroup == null)
                {
                    tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                this.tvDoctorPatientList1.Visible = false;
                this.panelTree.Controls.Add(tvGroup);
            }
            else
            {
                this.tvDoctorPatientList1.Visible = true;
                if (tvGroup != null)
                    tvGroup.Visible = false;
            }

            //{C1EA8590-F089-49cb-A045-F0D7A09D2C3E}
            //if (tbGroup.CheckState == CheckState.Checked)
            //{
            //    this.panel2.Visible = true;
            //}
            //else
            //{
            //    if (isEMROrder && isEdit)
            //    {
            //        panel2.Visible = true;
            //    }
            //    else
            //    {
            //        this.panel2.Visible = false;
            //    }
            //}      
        }

        /// <summary>
        /// 委托
        /// </summary>
        private void AddOrderHandle()
        {
            this.ucOrder1.OrderCanCancelComboChanged += new FS.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanCancelComboChanged);
            this.ucOrder1.OrderCanOperatorChanged += new FS.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanOperatorChanged);
            this.ucOrder1.OrderCanSetCheckChanged += new FS.HISFC.Components.Order.Controls.ucOrder.EventButtonHandler(ucOrder1_OrderCanSetCheckChanged);
        }

        void ucOrder1_OrderCanSetCheckChanged(bool b)
        {
            this.tbCheck.Enabled = b;
        }

        void ucOrder1_OrderCanOperatorChanged(bool b)
        {
            this.tbOperation.Enabled = b;
            this.tbAssayCure.Enabled = b;
            this.tsbHerbal.Enabled = b;
            this.tbChooseUL.Enabled = b;
            this.tbLevelOrder.Enabled = b;
        }

        void ucOrder1_OrderCanCancelComboChanged(bool b)
        {
            this.tbCancelOrder.Enabled = b;
        }
        #endregion

        /// <summary>
        /// 当前患者实体
        /// </summary>
        FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

        #region 封装公用，供外部调用（如电子病历）

        /// <summary>
        /// 当前患者
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return patient;
            }
            set
            {
                patient = value;
            }
        }

        /// <summary>
        /// 当前患者列表，只用于外部调用
        /// </summary>
        protected FS.HISFC.Components.Order.Controls.tvDoctorPatientList TvDoctorPatientList
        {
            get
            {
                return this.tvDoctorPatientList1;
            }
        }

        /// <summary>
        /// 点击患者列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TvDoctorPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.tvDoctorPatientList1_AfterSelect(sender, e);
        }

        #endregion

        FS.HISFC.BizProcess.Integrate.RADT inpatientManager = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.Order.Consultation consultation = new FS.HISFC.BizLogic.Order.Consultation();

        /// <summary>
        /// 获取开立患者类别
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Components.Order.Controls.ReciptPatientType GetPatientType()
        {
            if (tvDoctorPatientList1 != null && tvDoctorPatientList1.SelectedNode != null
                && tvDoctorPatientList1.SelectedNode.Tag != null)
            {
                if (this.tvDoctorPatientList1.SelectedNode.Parent != null)
                {
                    switch (tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString())
                    {
                        case "patient"://分管患者|patient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.PrivatePatient;
                            break;
                        case "DeptPatient"://本科室患者|DeptPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.DeptPatient;
                            break;
                        case "ConsultationPatient"://会诊患者|ConsultationPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.ConsultationPatient;
                            break;
                        case "PermissionPatient"://授权患者|PermissionPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.AuthorizedPatient;
                            break;
                        case "QueryPatient"://查找患者|QueryPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.FindedPatient;
                            break;
                        case "TeamPatient"://医疗组内患者|TeamPatient
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.MedicsPatient;
                            break;
                        default:
                            return FS.HISFC.Components.Order.Controls.ReciptPatientType.DeptPatient;
                            break;
                    }
                }
            }

            return FS.HISFC.Components.Order.Controls.ReciptPatientType.DeptPatient;
        }

        private void toolStrip1_ItemClicked_1(object sender, ToolStripItemClickedEventArgs e)
        {
            ucOrder1.PatientType = this.GetPatientType();

            #region 开立

            if (e.ClickedItem == this.tbAddOrder)
            {
                //选择子节点
                if (this.tvDoctorPatientList1.SelectedNode.Parent != null && this.tvDoctorPatientList1.SelectedNode.Parent.Tag != null)
                {
                    string inpatientNo;
                    ArrayList co = null;

                    int count = 0;
                    count = this.tvDoctorPatientList1.SelectedNode.Parent.GetNodeCount(false);
                    //判断所选节点父节点如果为会诊患者,则判断有无开立医嘱的权限/
                    //如果不是会诊患者则不需要进行判断,都可以进行开立医嘱
                    if (this.tvDoctorPatientList1.SelectedNode.Parent.Text == ("会诊患者" + "(" + count.ToString() + ")"))
                    {
                        patient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                        //处理床位号截位
                        string bedNO = patient.PVisit.PatientLocation.Bed.ID;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        //处理住院号显示
                        string patientNO = patient.PID.PatientNO;
                        if (string.IsNullOrEmpty(patientNO) == true)
                        {
                            patientNO = patient.ID;
                        }

                        inpatientNo = patient.ID;
                        co = consultation.QueryConsulation(inpatientNo);
                        bool iSCreateConsulationOrder = false;
                        if (co != null || co.Count != 0)
                        {
                            for (int i = 0; i < co.Count; i++)
                            {
                                FS.HISFC.Models.Order.Consultation obj = co[i] as FS.HISFC.Models.Order.Consultation;
                                //根据会诊患者有效的会诊单信息,判断医生是否有对该会诊患者开立医嘱权限
                                if ((FS.FrameWork.Management.Connection.Operator.ID == obj.DoctorConsultation.ID) &&
                                    (obj.EndTime >= consultation.GetDateTimeFromSysDateTime())
                                    && (obj.IsCreateOrder))
                                {
                                    if (this.ucOrder1.Add() == 0)
                                        this.initButton(true);
                                    iSCreateConsulationOrder = true;

                                    break;//{3541798B-AF9C-415c-AFAA-8BD22A34A808}
                                }
                                else if (string.IsNullOrEmpty(obj.DoctorConsultation.ID) &&
                                    (obj.EndTime >= consultation.GetDateTimeFromSysDateTime())
                                    && (obj.IsCreateOrder))
                                {
                                    if (this.ucOrder1.Add() == 0)
                                        this.initButton(true);
                                    iSCreateConsulationOrder = true;
                                }

                            }
                        }


                        if (iSCreateConsulationOrder.Equals(false))
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("对不起,您没有对该患者开立医嘱的权限!"), "提示");
                            return;
                        }
                    }
                    else
                    {
                        FS.HISFC.Models.RADT.PatientInfo patient1 = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                        //处理床位号截位
                        string bedNO = patient1.PVisit.PatientLocation.Bed.ID;
                        if (bedNO.Length > 4)
                        {
                            bedNO = bedNO.Substring(4);
                        }
                        //处理住院号显示
                        string patientNO = patient1.PID.PatientNO;
                        if (string.IsNullOrEmpty(patientNO) == true)
                        {
                            patientNO = patient1.ID;
                        }

                        //判断是否请假患者
                        if (patient1.PVisit.PatientLocation.Bed != null && patient1.PVisit.PatientLocation.Bed.Status.ID.ToString() == FS.HISFC.Models.Base.EnumBedStatus.R.ToString())
                        {
                            //请假患者不能开医嘱，须先销假，主要为防止请假后开立长嘱下次执行时间不对，这个bug有点2
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("患者请假中，如需开立医嘱请先销假"));
                            return;
                        }

                        if (this.ucOrder1.Add() == 0)
                            this.initButton(true);
                    }

                    //ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

                    //ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);


                    if (this.iClinicPath == null)
                    {
                       // iClinicPath = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IClinicPath))
                       //as FS.HISFC.BizProcess.Interface.Common.IClinicPath;
                    }

                    if (iClinicPath != null)
                    {
                        iClinicPath.ExecutePathOrder(this.Patient.ID);
                    }
                }
            }
            #endregion

            #region 套餐查看
            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            else if (e.ClickedItem == this.tbPackage)
            {
                if (this.ucOrder1.Patient == null || string.IsNullOrEmpty(this.ucOrder1.Patient.PID.CardNO))
                {
                    MessageBox.Show("请先检索患者！");
                    return;
                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                frmpackage.DetailVisible = true;//{e87b8fc3-e03c-43eb-be1a-97473bc93ebb}
                frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOrder1.Patient.PID.CardNO);
                frmpackage.ShowDialog();
            }

            #endregion 

            else if (e.ClickedItem == this.tbItem)  //{048e65b9-0b30-4049-9d66-e74fbe28c2fa}
            {
                if (this.ucOrder1.Patient == null || string.IsNullOrEmpty(this.ucOrder1.Patient.PID.CardNO))
                {
                    MessageBox.Show("请先检索患者！");
                    return;
                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                FS.HISFC.Components.Common.Forms.frmUnExecutedItem frmItem = new FS.HISFC.Components.Common.Forms.frmUnExecutedItem();
                frmItem.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOrder1.Patient.PID.CardNO);
                frmItem.ShowDialog();
            }

            #region 检查
            else if (e.ClickedItem == this.tbCheck)
            {
                this.ucOrder1.AddTest();
            }
            #endregion

            #region 刷新
            else if (e.ClickedItem == this.tbRefresh)
            {
                //刷新
                this.tvDoctorPatientList1.RefreshInfo();
            }
            #endregion

            #region 组套
            else if (e.ClickedItem == this.tbGroup)
            {
                if (this.tbGroup.CheckState == CheckState.Unchecked)
                {
                    this.tbGroup.CheckState = CheckState.Checked;
                    this.ucOrder1.SetEditGroup(true);
                    this.ucOrder1.SetPatient(null);
                    this.InitButtonGroup(true);
                }
                else
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;
                    this.ucOrder1.SetEditGroup(false);
                    this.InitButtonGroup(false);


                    if (this.tvDoctorPatientList1.SelectedNode != null &&
                        this.tvDoctorPatientList1.SelectedNode.Tag != null)
                    {
                        this.ucOrder1.Query(this.tvDoctorPatientList1.SelectedNode, this.tvDoctorPatientList1.SelectedNode.Tag);
                    }
                }
            }
            #endregion

            #region 手术
            else if (e.ClickedItem == this.tbOperation)
            {
                FS.HISFC.Models.RADT.PatientInfo pi = (FS.HISFC.Models.RADT.PatientInfo)this.tvDoctorPatientList1.SelectedNode.Tag;
                frmOperation frmOpt = new frmOperation(pi);
                frmOpt.ShowDialog();
            }
            #endregion

            #region
            else if (e.ClickedItem == this.tbAssayCure)
            {
                this.ucOrder1.AddAssayCure();
            }
            #endregion

            #region 删除
            else if (e.ClickedItem == this.tbDelOrder)
            {
                this.ucOrder1.Delete();
            }
            #endregion

            #region 查询
            else if (e.ClickedItem == this.tbQueryOrder)
            {
                try
                {
                    this.ucOrder1.Query(this.tvDoctorPatientList1.SelectedNode, this.tvDoctorPatientList1.SelectedNode.Tag);
                }
                catch { }
            }
            #endregion

            #region 打印
            else if (e.ClickedItem == this.tbPrintOrder)
            {
                this.ucOrder1.PrintOrder();
            }
            #endregion


            #region 处方打印
            else if (e.ClickedItem == this.tbRecipePrint)// {0045F3F6-1B1C-4d0a-A834-8BD07286E175}
            {
                this.ucOrder1.RecipePrint();
            }
            #endregion

            #region 组合
            else if (e.ClickedItem == this.tbComboOrder)
            {
                this.ucOrder1.ComboOrder();
            }
            #endregion

            #region 取消组合
            else if (e.ClickedItem == this.tbCancelOrder)
            {
                this.ucOrder1.CancelCombo();
            }
            #endregion

            #region 退出医嘱
            else if (e.ClickedItem == this.tbExitOrder)
            {
                if (this.isEditGroup)
                {
                    if (this.tbGroup.CheckState == CheckState.Checked)
                    {
                        this.tbGroup.CheckState = CheckState.Unchecked;
                    }
                    else
                    {
                        this.tbGroup.CheckState = CheckState.Checked;
                    }
                    this.ucOrder1.SetEditGroup(false);
                }
                else
                {
                    if (this.ucOrder1.ExitOrder() == 0)
                        this.initButton(false);

                    //避免失去患者焦点，此处不再刷新列表，如果想刷新，点击刷新按钮
                    //tvDoctorPatientList1.RefreshInfo();
                }
            }
            #endregion

            #region 过滤
            else if (e.ClickedItem == this.tbInValid)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.Invalid);
            }
            else if (e.ClickedItem == this.tbValid)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.Valid);
            }
            else if (e.ClickedItem == this.tbAll)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.All);
            }
            else if (e.ClickedItem == this.tbToday)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.Today);
            }
            else if (e.ClickedItem == this.tbNew)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.New);
            }
            else if (e.ClickedItem == this.tbUCULOrder)
            {
                this.ucOrder1.Filter(FS.HISFC.Components.Order.Controls.EnumFilterList.UC_ULOrder);
            }
            #endregion

            #region 保存
            else if (e.ClickedItem == this.tbSaveOrder)
            {
                if (isEditGroup)
                {
                    this.ucOrder1.SaveGroup();
                }
                else
                {
                    if (this.ucOrder1.Save() == -1)
                    {
                    }
                    else
                    {
                        this.initButton(false);
                    }
                }
            }
            #endregion

            #region 草药
            else if (e.ClickedItem == this.tsbHerbal)
            {
                this.ucOrder1.HerbalOrder();
            }
            #endregion

            #region 层级医嘱
            else if (e.ClickedItem == this.tbLevelOrder)
            {
                this.ucOrder1.AddLevelOrders();
            }
            #endregion

            #region 换医师
            else if (e.ClickedItem == this.tbChooseDoct)//{D5517722-7128-4d0c-BBC4-1A5558A39A03}
            {
                this.ucOrder1.ChooseDoctor();
            }
            #endregion

            #region 退出窗口
            else if (e.ClickedItem == this.tb1Exit)
            {
                if (this.ucOrder1.IsDesignMode) //是在开立状态
                {
                    DialogResult result = MessageBox.Show(FS.FrameWork.Management.Language.Msg("医嘱目前处于开立模式，是否保存?"), "询问", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    if (result == DialogResult.Yes)
                    {
                        if (this.ucOrder1.Save() == 0)
                            this.Close();

                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            #endregion

            #region 重整医嘱
            else if (e.ClickedItem == this.tbRetidyOrder)
            {
                if (this.ucOrder1.IsDesignMode == false)
                {
                    this.ucOrder1.ReTidyOrder();
                }
                else
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("非开立状态下才允许进行医嘱重整"), "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            #endregion

            #region 传染病报告
            else if (e.ClickedItem == this.tbDiseaseReport)
            {
                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                    this.dcpInstance.RegisterDiseaseReport(this, patientInfo, FS.HISFC.Models.Base.ServiceTypes.I);
                }
            }
            #endregion

            #region 医疗结果
            else if (e.ClickedItem == this.tbLisResultPrint)
            {
                this.ucOrder1.QueryLisResult();
            }

            else if (e.ClickedItem == this.tbResultPrint)
            {

            }
            #endregion

            #region PACS结果查看
            else if (e.ClickedItem == this.tbPacsResultPrint)
            {
                this.ucOrder1.QueryPacsReport();
            }
            #endregion

            #region 长嘱全停
            //停止全部长期医嘱 houwb 2011-3-11 {46E8908F-4248-4a40-89B1-530CA5796CD4}
            else if (e.ClickedItem == this.tbDcAllLongOrder)
            {
                this.ucOrder1.DcAllLongOrder("");
            }
            #endregion

            #region 电子申请单添加

            else if (e.ClickedItem == this.tblEditPacsApply)
            {
                this.ucOrder1.EditPascApply();
            }

            #endregion

            #region 合理用药

            else if (e.ClickedItem == this.tbPass)
            {
                this.tbPass.Checked = !this.tbPass.Checked;

                this.ucOrder1.EnabledPass = this.tbPass.Checked;

                FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(consultation.Operator.ID);

                if (setting == null)
                {
                    setting = new FS.HISFC.Models.Base.UserDefaultSetting();
                    setting.Empl.ID = consultation.Operator.ID;
                    setting.Oper.ID = consultation.Operator.ID;
                    setting.Oper.OperTime = consultation.GetDateTimeFromSysDateTime();
                    setting.Setting5 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                    if (userSetmgr.Insert(setting) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
                else
                {
                    setting.Setting5 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    if (userSetmgr.Update(setting) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
            }

            #endregion
            #region 消息发送{2E47C0BD-CD18-4ce8-B244-02DCE3B30DB6}
            else if (e.ClickedItem == this.tbMSG)
            {
                FS.HISFC.Models.RADT.PatientInfo selectedPatient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;//.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (selectedPatient == null || string.IsNullOrEmpty(selectedPatient.PID.CardNO))
                {
                    MessageBox.Show("请先检索患者！");
                    return;
                }
                FS.HISFC.Components.Order.Forms.frmAddMsgForm msg = new FS.HISFC.Components.Order.Forms.frmAddMsgForm();
                msg.patient = selectedPatient;
                msg.Init();
                msg.ShowDialog();
            }
            #endregion


            #region 术后医嘱

            else if (e.ClickedItem == this.tbPostOperat)
            {
                //1、出院医嘱；2、转科医嘱；3、死亡医嘱；4、术前医嘱；5、术后医嘱
                ucOrder1.AddDCOrder("5");
            }

            #endregion

            #region 转科医嘱

            else if (e.ClickedItem == this.tbSwitchDept)
            {
                //1、出院医嘱；2、转科医嘱；3、死亡医嘱；4、术前医嘱；5、术后医嘱
                ucOrder1.AddDCOrder("2");
            }

            #endregion

            #region 死亡医嘱

            else if (e.ClickedItem == this.tbDead)
            {
                //1、出院医嘱；2、转科医嘱；3、死亡医嘱；4、术前医嘱；5、术后医嘱
                ucOrder1.AddDCOrder("3");
            }

            #endregion

            #region 术前医嘱

            else if (e.ClickedItem == this.tbPreOperat)
            {
                //1、出院医嘱；2、转科医嘱；3、死亡医嘱；4、术前医嘱；5、术后医嘱
                ucOrder1.AddDCOrder("4");
            }

            #endregion

            #region 出院医嘱

            else if (e.ClickedItem == this.tbLeaveHos)
            {
                //1、出院医嘱；2、转科医嘱；3、死亡医嘱；4、术前医嘱；5、术后医嘱
                ucOrder1.AddDCOrder("1");
            }

            #endregion

            #region 修改待遇类型

            else if (e.ClickedItem == this.tbTreatmentType)
            {
                //1、出院医嘱；2、转科医嘱；3、死亡医嘱；4、术前医嘱；5、术后医嘱；6、修改待遇类型
                ucOrder1.AddDCOrder("6");
            }

            #endregion

            #region 电子病历接口
            else if (e.ClickedItem == this.tbEMR)
            {
                if (this.emrInstance == null)
                {
                    this.emrInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.EMR.IEMR)) as FS.HISFC.BizProcess.Interface.EMR.IEMR;
                }

                if (this.emrInstance != null)
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;

                    //{E3ED6553-0A5D-43e9-8D0C-DD24E42E8258}
                    if (this.emrInstance.EMRRegister(this, patientInfo, FS.HISFC.Models.Base.ServiceTypes.I) == -1)
                    {
                        MessageBox.Show(this.emrInstance.ErrMsg);
                    }
                }
            }
            #endregion

            #region 医技预约
            if (e.ClickedItem == this.tbMTOrder)//开立
            {
                FS.HISFC.Components.MTOrder.frmMTApply newfrm = new FS.HISFC.Components.MTOrder.frmMTApply();
                newfrm.PatientInfo = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                if (ucOrder1.SelectedOrder != null)
                    newfrm.SelectedOrderID = ucOrder1.SelectedOrder.ID;
                newfrm.ShowDialog();
            }
            #endregion

            #region 院感上报
            if (e.ClickedItem == this.tbInfectionReport)//院感上报
            {
                if (this.tvDoctorPatientList1.SelectedNode.Parent == null || this.tvDoctorPatientList1.SelectedNode.Parent.Tag == null)
                {
                    MessageBox.Show("请先选择您要上报的患者！");
                    return;
                }
                FS.HISFC.Models.RADT.PatientInfo patientInfo= this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                patientInfo.Memo = "1";//住院标识
                tbInfectionReport.Tag = patientInfo;
                this.tbInfectionReport_Click(this.tbInfectionReport);
            }
            #endregion

            #region 检验项目开立界面

            if (e.ClickedItem == this.tbChooseUL)
            {
                if (this.IOrderChooseUl != null)
                {
                    if (this.ucOrder1.Patient == null)
                    {
                        MessageBox.Show("当前尚未选中患者！");
                        return;
                    }
                    ArrayList alOrders = new ArrayList();
                    if (this.IOrderChooseUl.GetChooseUL(ref alOrders) < 0)
                    {
                        MessageBox.Show(IOrderChooseUl.Err, "错误", MessageBoxButtons.OK);
                        return;
                    }
                    else
                    {
                        if (alOrders != null && alOrders.Count > 0)
                        {
                            this.ucOrder1.AddGroupOrder(alOrders);
                        }
                    }
                }
            }

            #endregion


            if (tvGroup != null)
            {
                if (ucOrder1.Patient != null)
                {
                    tvGroup.Pact = patient.Pact;
                }
                else
                {
                    tvGroup.Pact = null;
                }
            }
        }

        /// <summary>
        /// 保存组套
        /// </summary>
        [Obsolete("作废,用HISFC.Components.Order.Controls.ucOrder的SaveGroup方法", true)]
        private void SaveGroup()
        {
            FS.HISFC.Components.Common.Forms.frmOrderGroupManager group = new FS.HISFC.Components.Common.Forms.frmOrderGroupManager();

            try
            {
                group.IsManager = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            #region 长期临时一起保存组套{11F97F55-F747-4ad9-A74F-086635D5EBD9}
            for (int i = 0; i < this.ucOrder1.fpOrder.Sheets[0].Rows.Count; i++)//长期医嘱
            {
                //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
                FS.HISFC.Models.Order.Inpatient.Order longorderTemp = this.ucOrder1.GetObjectFromFarPoint(i, 0);
                if (longorderTemp == null)
                {
                    continue;
                }

                //FS.HISFC.Models.Order.Inpatient.Order longorder = this.ucOrder1.GetObjectFromFarPoint(i, 0).Clone();
                FS.HISFC.Models.Order.Inpatient.Order longorder = longorderTemp.Clone();
                if (longorder == null)
                {
                    MessageBox.Show("获得医嘱出错！");
                }
                else
                {
                    string s = longorder.Item.Name;
                    string sno = longorder.Combo.ID;
                    //保存医嘱组套 默认开立时间为 零点
                    longorder.BeginTime = new DateTime(longorder.BeginTime.Year, longorder.BeginTime.Month, longorder.BeginTime.Day, 0, 0, 0);
                    al.Add(longorder);
                }
            }
            for (int i = 0; i < this.ucOrder1.fpOrder.Sheets[1].Rows.Count; i++)//临时医嘱
            {
                //{F4CA5CB3-0C23-4e0e-978D-5B72711A6C86}
                FS.HISFC.Models.Order.Inpatient.Order shortorderTemp = this.ucOrder1.GetObjectFromFarPoint(i, 1);
                if (shortorderTemp == null)
                {
                    continue;
                }
                //FS.HISFC.Models.Order.Inpatient.Order shortorder = this.ucOrder1.GetObjectFromFarPoint(i, 1).Clone();
                FS.HISFC.Models.Order.Inpatient.Order shortorder = shortorderTemp.Clone();
                if (shortorder == null)
                {
                    MessageBox.Show("获得医嘱出错！");
                }
                else
                {
                    string s = shortorder.Item.Name;
                    string sno = shortorder.Combo.ID;
                    //保存医嘱组套 默认开立时间为 零点
                    shortorder.BeginTime = new DateTime(shortorder.BeginTime.Year, shortorder.BeginTime.Month, shortorder.BeginTime.Day, 0, 0, 0);
                    al.Add(shortorder);
                }
            }
            #endregion
            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();
                this.tvGroup.RefrshGroup();
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Controls.Count > 0)
            {
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];

                //if (this.CurrentControl.GetType() == typeof(FS.Pathway.His50.ucHisEcp))
                //{
                //    if (this.ucOrder1.GetUnSavedOrders().Count > 0)
                //    {
                //        MessageBox.Show("还有未保存的医嘱！");
                //        this.tabControl1.SelectedIndex = 0;
                //        return;
                //    }

                //    //if (!isInit)
                //    //{
                //    //    this.isInit = true;

                //    //    (this.CurrentControl as FS.Pathway.His50.ucHisEcp).pathWayForm.ucPathwayDay.sendOrderList
                //    //          += new Ecp.WinForms.ucPathwayDay.SendOrderList(ucPathwayDay_sendOrderList);
                //    //}
                //}


                //if (this.CurrentControl.GetType() == typeof(ucEcp))
                //{
                //    ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

                //    ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);
                //}

                //住院临床路径 {CE9F7C07-A6B3-4ab4-BC47-2F88411BB541}
                //if (this.CurrentControl.GetType() == typeof(ucEcp))
                //{
                //    ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

                //    ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);
                //}
            }
        }

        void ucPathwayDay_sendOrderList(ArrayList alOrders)
        {
            //ToolStripItemClickedEventArgs e = new ToolStripItemClickedEventArgs(this.tbAddOrder);

            //if (this.ucOrder1.IsDesignMode == false)
            //{
            //    this.toolStrip1_ItemClicked_1(null, e);
            //}

            //this.ucOrder1.PasteOrder(list);

            //this.tabControl1.SelectedIndex = 0;

            alOrders.Reverse();
            this.tabControl1.SelectedIndex = 0;
            //this.ucOrder1.SetPatient(this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo);
            //this.patient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (!this.ucOrder1.IsDesignMode)
            {
                if (this.ucOrder1.Add() == 0)
                {
                    this.initButton(true);
                }
            }

            this.ucOrder1.AddPathwayOrders(alOrders);
            //foreach (FS.HISFC.Models.Order.Inpatient.Order order in alOrders)
            //{
            //    if (order.Item.SysClass.ID.ToString() == "PCC")
            //    {
            //        continue;
            //    }
            //    if (order.OrderType.Type == FS.HISFC.Models.Order.EnumType.LONG)
            //    {
            //        order.FirstUseNum = "1";
            //        this.ucOrder1.AddNewOrder(order, 0);
            //    }
            //    else
            //    {
            //        this.ucOrder1.AddNewOrder(order, 1);
            //    }
            //}
            //this.ucOrder1.RefreshCombo();
        }

        private void ucQueryInpatientNo1_myEvent()
        {
            if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo))
            {
                return;
            }

            if (this.ucOrder1.IsDesignMode)
            {
                MessageBox.Show("开立状态不能查询患者");
                return;
            }
            this.tvDoctorPatientList1.QueryPaitent(this.ucQueryInpatientNo1.InpatientNo, this.consultation.Operator as FS.HISFC.Models.Base.Employee);
        }

        private void tvDoctorPatientList1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (this.tvDoctorPatientList1.SelectedNode.Tag != null
               && this.tvDoctorPatientList1.SelectedNode.Tag.ToString() != ""
               && this.tvDoctorPatientList1.SelectedNode.Parent != null)
            {
                if (this.tvDoctorPatientList1.SelectedNode.Parent.Tag != null
                    && this.tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString() != "")
                {
                    if (this.tvDoctorPatientList1.SelectedNode.Parent.Tag.ToString() == "QueryPatient")
                    {
                        if (this.CurrentControl.GetType().FullName == "FS.HISFC.Components.Order.Controls.ucOrder")
                        {
                            this.tbAddOrder.Enabled = false;
                        }
                    }
                    else
                    {
                        if (this.CurrentControl.GetType().FullName == "FS.HISFC.Components.Order.Controls.ucOrder")
                        {
                            this.tbAddOrder.Enabled = true;

                            patient = this.tvDoctorPatientList1.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
                            string bedNO = patient.PVisit.PatientLocation.Bed.ID;
                            if (bedNO.Length > 4)
                            {
                                bedNO = bedNO.Substring(4);
                            }
                            this.Text = "您正在操作的患者为 住院号：" + patient.PID.PatientNO + " 姓名：" + patient.Name + " 性别：" + patient.Sex.Name + " 年龄：" + consultation.GetAge(patient.Birthday) + " 床号:" + bedNO;
                        }
                    }
                }
            }
            else
            {
                this.Text = "";
            }

            //住院临床路径 {CE9F7C07-A6B3-4ab4-BC47-2F88411BB541}
            //if (this.CurrentControl.GetType() == typeof(ucEcp))
            //{
            //    ucEcp.sendOrderList -= new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);

            //    ucEcp.sendOrderList += new ucEcp.SendOrderList(this.ucPathwayDay_sendOrderList);
            //    this.tabControl1.SelectedIndex = 0;
            //}
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmOrder_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!DesignMode)
            {
                this.ucOrder1.ReleaseLisInterface();
                this.ucOrder1.ReleasePacsInterface();
                this.ucOrder1.QuitPass();
            }
        }
        /// <summary>
        /// 院感上报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbInfectionReport_Click(object sender)
        {
            if (object.Equals(this.Tag, null))
            {
                MessageBox.Show("请先选择您要上报的患者！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (IResultPrint == null)
            {
                IResultPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Forms.frmOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;
            }

            if (!((ToolStripButton)sender).Name.Equals(null))
            {
                IResultPrint.ResultType = ((ToolStripButton)sender).Name.ToString();
                IResultPrint.SetPatient(((ToolStripButton)sender).Tag as FS.HISFC.Models.RADT.Patient);
                IResultPrint.ShowResultByPatient();
            }
        }

        #region IInterfaceContainer 成员

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { 
                    typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP) ,
                    typeof(FS.HISFC.BizProcess.Interface.Common.IPacs)
                };
            }
        }

        #endregion

    }
}