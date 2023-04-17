using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Order.OutPatient.Forms
{
    /// <summary>
    /// 门诊医生站主界面
    /// </summary>
    public partial class frmOutPatientOrder : FS.FrameWork.WinForms.Forms.frmBaseForm, FS.FrameWork.WinForms.Classes.IPreArrange, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public frmOutPatientOrder()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.iControlable = this.ucOutPatientOrder1 as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.ucOutPatientOrder1;
                this.panelToolBar.Visible = false;
                this.FormClosing += new FormClosingEventHandler(frmOutPatientOrder_FormClosing);

                //this.FontSize = 28;
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        void frmOutPatientOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            Classes.LogManager.Write("【开始退出界面】");
            this.ucOutPatientOrder1.QuitPass();
            this.ucOutPatientOrder1.ReleaseLisInterface();
            this.ucOutPatientOrder1.ReleasePacsInterface();

            Classes.LogManager.Write("【成功退出界面】");
            return;
        }

        #region 变量

        /// <summary>
        /// 是否采用账户流程？
        /// </summary>
        private bool isAccountMode = false;

        /// <summary>
        /// 门诊账户流程是否使用终端扣费流程 1终端收费 0门诊收费
        /// </summary>
        private bool isAccountTerimal = false;

        /// <summary>
        /// 传染病上报类
        /// </summary>
        private FS.HISFC.BizProcess.Interface.DCP.IDCP dcpInstance = null;

        /// <summary>
        /// 门诊医生开立扩展按钮实现接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderExtendModule IOrderExtendModule = null;

        /// <summary>
        /// 门诊医生站选择检验项目接口
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderChooseUL IOrderChooseUl = null;

        FS.HISFC.BizProcess.Interface.Common.ILis IResultPrint = null;
        /// <summary>
        /// 组套树列表
        /// </summary>
        FS.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;

        /// <summary>
        /// 是否编辑组套
        /// </summary>
        //bool isEditGroup = false;

        /// <summary>
        /// 开立与挂号之间的有效天数
        /// </summary>
        private decimal validDays = 1;

        /// <summary>
        /// 是否启用电子申请单功能
        /// </summary>
        private bool isPacsApplyEnable = false;

        /// <summary>
        /// 是否允许门诊医生站登录住院科室
        /// </summary>
        private bool isHosDeptCanLogin = false;

        /// <summary>
        /// 患者树已诊患者双击如存在医嘱是否提示
        /// </summary>
        private bool isDoubleClickedShowTips = false;

        /// <summary>
        /// 是否编辑组套
        /// </summary>
        //public bool IsEditGroup
        //{
        //    get
        //    {
        //        return isEditGroup;
        //    }
        //    set
        //    {
        //        isEditGroup = value;
        //        if (this.tvGroup != null)
        //        {
        //            this.tvGroup.IsEditGroup = value;
        //        }

        //        operMode = EnumOperMode.Group;
        //    }
        //}

        public ArrayList Diagnoses = null;

        /// <summary>
        /// 是否首次录入诊断，第一次需要加载诊断列表
        /// </summary>
        private bool isFirst = true;

        /// <summary>
        /// 是否首次录入门诊电子病历
        /// </summary>
        private bool isCaseFirst = true;

        /// <summary>
        /// 博济开立界面
        /// </summary>
        FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe ucOutPatientRecipe1 = null;

        /// <summary>
        /// 门诊医生菜单
        /// </summary>
        Dictionary<string,FS.HISFC.Models.Base.Const> dicOutMenu = null;

        #endregion

        #region 事件

        private void frmOutPatientOrder_Load(object sender, EventArgs e)
        {
            try
            {
                Classes.LogManager.Write("【初始化门诊医生from界面】");
                this.WindowState = FormWindowState.Maximized;

                this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

                this.InitMenu();
                OperMode = EnumOperMode.Query;

                //this.panelTree.Height = this.Height - 162;
                //this.panelTree.Visible = false;

                this.ucOutPatientTree1.TreeDoubleClick += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.TreeDoubleClickHandler(ucOutPatientTree1_TreeDoubleClick);

                this.isAccountTerimal = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, false, "0"));
                this.isAccountMode = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("MZ0300", false, "0"));
                //this.isAccountTerimal = controlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, false, false);
                //this.isAccountMode = controlIntegrate.GetControlParam<bool>("MZ0300", false, false);

                this.validDays = FS.FrameWork.Function.NConvert.ToDecimal(Classes.Function.GetBatchControlParam("200022", false, "0"));

                this.isDoubleClickedShowTips = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("HNMZ53", false, "0"));

                this.ucOutPatientOrder1.IsAccountTerimal = this.isAccountTerimal;
                this.ucOutPatientOrder1.IsAccountMode = this.isAccountMode;
                this.ucOutPatientTree1.IsAccountMode = this.isAccountMode;
                this.ucOutPatientOrder1.OnRefreshGroupTree += new EventHandler(ucOutPatientOrder1_OnRefreshGroupTree);

                foreach (TabPage tb in this.tabControl1.TabPages)
                {
                    tb.Text = FS.FrameWork.Management.Language.Msg(tb.Text);
                }

                Application.DoEvents();

                #region 初始化传染病上报接口

                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    this.dcpInstance.LoadNotice(this, FS.HISFC.Models.Base.ServiceTypes.C);
                }
                #endregion

                #region 初始化扩展按钮接口

                if (this.IOrderExtendModule == null)
                {
                    this.IOrderExtendModule = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderExtendModule)) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderExtendModule;
                }

                #endregion

                #region 初始化选择检验项目接口
                if (this.IOrderChooseUl == null)
                {
                    this.IOrderChooseUl = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderChooseUL)) as FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOrderChooseUL;
                }
                #endregion

                #region 获取人员自定义参数

                FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(CacheManager.LogEmpl.ID);

                if (setting == null)
                {
                    //MessageBoxShow(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.tbPass.Checked = FS.FrameWork.Function.NConvert.ToBoolean(setting.Setting4);
                    this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;
                }
                #endregion

                #region  add by lijp 2011-11-25 电子申请单添加 {102C4C01-8759-4b93-B4BA-1A2B4BB1380E}

                this.tblEditPacsApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S申请单);

                try
                {
                    isPacsApplyEnable = CacheManager.ContrlManager.GetControlParam<bool>("200212");
                }
                catch
                {
                    isPacsApplyEnable = false;
                }

                #endregion

                #region 初始化博济处方
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Text.Contains("博济"))
                    {
                        FS.FrameWork.Models.NeuObject neuObject = CacheManager.ConManager.GetConstant("DeptItem", CacheManager.LogEmpl.Dept.ID);

                        if (object.Equals(neuObject, null) || object.Equals(neuObject.ID, ""))
                        {
                            tabControl1.TabPages.Remove(tabPage);
                        }
                        else
                        {
                            tabControl1.SelectedTab = tabPage;
                            if (tabPage.Controls.Count > 0
                                && tabPage.Controls[0].GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                            {
                                ucOutPatientRecipe1 = tabPage.Controls[0] as FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe;
                            }
                            tabControl1.SelectedIndex = 0;
                        }
                    }
                }


                #endregion

                this.tbPass.CheckedChanged += new EventHandler(tbPass_CheckedChanged);

                Classes.LogManager.Write("【结束初始化门诊医生from界面】");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        void tbPass_CheckedChanged(object sender, EventArgs e)
        {
            this.ucOutPatientOrder1.SetInputItemVisible(false);

            this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;


            FS.HISFC.BizLogic.Manager.UserDefaultSetting userSetmgr = new FS.HISFC.BizLogic.Manager.UserDefaultSetting();
            FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(userSetmgr.Operator.ID);

            if (setting == null)
            {
                MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                setting.Setting4 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                if (userSetmgr.Update(setting) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                FS.FrameWork.Management.PublicTrans.Commit();
            }
        }

        void ucOutPatientOrder1_OnRefreshGroupTree(object sender, EventArgs e)
        {
            this.tvGroup.RefrshGroup();
        }

        /// <summary>
        /// 双击患者树节点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="isDoubleClick">是否双击树节点</param>
        private void ucOutPatientTree1_TreeDoubleClick(object sender, HISFC.Components.Order.OutPatient.Controls.ClickEventArgs e, bool isDoubleClick)
        {
            try
            {
                if (this.ucOutPatientTree1.neuTreeView1.Visible)
                {
                    this.tree = this.ucOutPatientTree1.neuTreeView1;
                    TreeViewEventArgs mye = new TreeViewEventArgs(this.ucOutPatientTree1.neuTreeView1.SelectedNode);

                    this.tree_AfterSelect(e.Message, mye);
                    if (this.CurrentControl.Name.Equals(this.ucOutPatientOrder1.Name) == false)
                    {
                        this.ucOutPatientOrder1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    #region add by liuww 博济处方 2013-05-13
                    if (!object.Equals(null, this.ucOutPatientRecipe1))
                    {
                        this.ucOutPatientRecipe1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    #endregion

                    this.Tag = this.ucOutPatientTree1.neuTreeView1.SelectedNode.Tag;
                }

                if (this.ucOutPatientTree1.neuTreeView2.Visible)
                {
                    this.tree = this.ucOutPatientTree1.neuTreeView2;
                    TreeViewEventArgs mye = new TreeViewEventArgs(this.ucOutPatientTree1.neuTreeView2.SelectedNode);
                    this.tree_AfterSelect(e.Message, mye);

                    if (this.CurrentControl.Name.Equals(this.ucOutPatientOrder1.Name) == false)
                    {
                        this.ucOutPatientOrder1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView2.SelectedNode);
                    }
                    #region add by liuww 博济处方 2013-05-13
                    if (!object.Equals(null, this.ucOutPatientRecipe1))
                    {
                        this.ucOutPatientRecipe1.SetValue(e.Message, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    #endregion

                    this.Tag = this.ucOutPatientTree1.neuTreeView2.SelectedNode.Tag;
                }

                if (this.Tag is FS.HISFC.Models.Registration.Register)
                {
                    FS.HISFC.Models.Registration.Register register = (FS.HISFC.Models.Registration.Register)Tag;
                    this.Text = "病历号：" + register.PID.CardNO.PadRight(12) + "姓名：" + register.Name.PadRight(8) + "性别：" + register.Sex.Name.PadRight(4) + "电话：" + register.PhoneHome + "住址：" + register.AddressHome;
                }

                if (this.ucOutPatientTree1.neuTreeView2.SelectedNode != null
                    && this.ucOutPatientTree1.neuTreeView2.SelectedNode.Nodes.Count > 0
                    && isDoubleClick && isDoubleClickedShowTips)
                {
                    //存在医嘱,则提示是否新开立医嘱
                    DialogResult dr = MessageBox.Show("已存在医嘱,是否新开立?", "提示", MessageBoxButtons.YesNo);
                    if (dr == DialogResult.Yes)
                    {
                        AddOrder(false);
                    }

                }
                else
                {
                    if (isDoubleClick)
                    {
                        AddOrder(false);
                    }
                }

                return;

                if (isAccountMode)
                {
                    #region 此处不再扣费，医嘱保存时才收费
                    if (this.Tag is FS.HISFC.Models.Registration.Register)
                    {
                        //判断账户流程的挂号收费情况
                        bool isAccount = false;
                        decimal vacancy = 0m;
                        FS.HISFC.Models.Registration.Register r = (FS.HISFC.Models.Registration.Register)Tag;

                        if (isAccountTerimal && r.IsAccount)
                        {

                            if (CacheManager.FeeIntegrate.GetAccountVacancy(r.PID.CardNO, ref vacancy) <= 0)
                            {
                                MessageBox.Show(CacheManager.FeeIntegrate.Err);
                                return;
                            }
                            isAccount = true;
                        }
                        if (isAccount && r.IsFee == false)
                        {
                            #region 账户扣取挂号费

                            if (!CacheManager.FeeIntegrate.CheckAccountPassWord(r))
                            {
                                this.ucOutPatientTree1.neuTreeView1.SelectedNode = null;
                                this.ucOutPatientTree1.PatientInfo = null;
                                return;
                            }

                            if (isAccount && !r.IsFee)
                            {
                                if (vacancy < r.OwnCost)
                                {
                                    MessageBox.Show("账户金额不足，请交费！");
                                    return;
                                }

                                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                                if (CacheManager.FeeIntegrate.AccountPay(r, r.OwnCost, "挂号收费", CacheManager.LogEmpl.Dept.ID, string.Empty) < 0)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("扣账户金额失败！" + CacheManager.FeeIntegrate.Err);
                                    return;
                                }
                                r.SeeDoct.ID = CacheManager.LogEmpl.ID;
                                r.SeeDoct.Dept.ID = CacheManager.LogEmpl.ID;

                                FS.HISFC.BizLogic.Admin.FunSetting funMgr = new FS.HISFC.BizLogic.Admin.FunSetting();
                                if (CacheManager.RegInterMgr.UpdateAccountFeeState(r.ID, r.SeeDoct.ID, r.SeeDoct.Dept.ID, funMgr.GetDateTimeFromSysDateTime()) == -1)
                                {
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新挂号表已收费状态出错\r\n" + CacheManager.RegInterMgr.Err);
                                    return;
                                }
                                FS.FrameWork.Management.PublicTrans.Commit();
                                r.IsFee = true;
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }
            catch { }
            finally { }
        }

        /// <summary>
        /// 选择组套
        /// </summary>
        /// <param name="alOrders"></param>
        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            this.ucOutPatientOrder1.AddGroupOrder(alOrders);
        }


        /// <summary>
        /// 单据打印按钮实现（治疗、处方、指引、检验）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmPrint_Click(object sender, EventArgs e)
        {
            bool IsPreview = false;

            if (!((ToolStripMenuItem)sender).Tag.Equals(null))
            {
                IsPreview = FS.FrameWork.Function.NConvert.ToBoolean(((ToolStripMenuItem)sender).Tag);

            }
            this.ucOutPatientOrder1.PrintAllBill(((ToolStripMenuItem)sender).Name, IsPreview);


            if (((ToolStripMenuItem)sender).Name.Equals("0") || ((ToolStripMenuItem)sender).Name.Equals("9"))
            {
                if (!object.Equals(ucOutPatientRecipe1, null))
                {
                    this.ucOutPatientRecipe1.PrintAgain(((ToolStripMenuItem)sender).Name, IsPreview);
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
            //if (object.Equals(this.Tag, null))
            //{
            //    MessageBox.Show("请先选择您要查看的患者！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //if (IResultPrint == null)
            //{

            //    IResultPrint = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.Components.Order.OutPatient.Forms.frmOutPatientOrder), typeof(FS.HISFC.BizProcess.Interface.Common.ILis)) as FS.HISFC.BizProcess.Interface.Common.ILis;

            //}

            //if (!((ToolStripMenuItem)sender).Name.Equals(null))
            //{
            //    IResultPrint.ResultType = ((ToolStripMenuItem)sender).Name.ToString();
            //    IResultPrint.SetPatient(this.Tag as FS.HISFC.Models.RADT.Patient);
            //    IResultPrint.ShowResultByPatient();

            //}

            FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType resultType = (FS.SOC.HISFC.BizProcess.OrderInterface.Common.EnumResultType)FS.FrameWork.Function.NConvert.ToInt32(((ToolStripMenuItem)sender).Name);

            ucOutPatientOrder1.QueryMedicalResult(resultType);
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

        #endregion

        #region 方法

        /// <summary>
        /// 当前操作类型
        /// </summary>
        private EnumOperMode operMode = EnumOperMode.Query;

        /// <summary>
        /// 当前操作类型
        /// </summary>
        private EnumOperMode OperMode
        {
            set
            {
                operMode = value;
                //if (operMode == EnumOperMode.Group)
                //{
                //    this.isEditGroup = true;
                //}
                this.SetButton();
            }
        }

        /// <summary>
        /// 操作模式
        /// </summary>
        private enum EnumOperMode
        {
            /// <summary>
            /// 开立
            /// </summary>
            Add,

            /// <summary>
            /// 组套
            /// </summary>
            Group,

            /// <summary>
            /// 查询界面
            /// </summary>
            Query
        }

        /// <summary>
        /// 设置菜单显示
        /// </summary>
        /// <param name="isDisign"></param>
        private void SetButton()
        {
            #region 设置菜单的可见性

            //组套
            this.tbGroup.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbGroup.Name);
            //打印
            this.tbPrintOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPrintOrder.Name);
            //预约入院
            this.tbOutPreIn.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbOutPreIn.Name);
            //补打
            this.tbPrintAgain.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPrintAgain.Name);
            //打印预览
            this.tbPrintOrderPreview.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPrintOrderPreview.Name);
            //开立处方
            this.tbAddOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbAddOrder.Name);
            //新开立
            this.tbAddNewOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbAddNewOrder.Name);

            //扩展1 只能在开立模式下使用
            tbExtend1.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbExtend1.Name);
            //扩展2 任何时候都可以使用
            tbExtend2.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbExtend2.Name);
            //扩展3 只能在非开立模式下使用
            tbExtend3.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbExtend3.Name);
            //检验项目开立
            tbChooseUL.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbChooseUL.Name);
            //树列表
            this.tbPatientTree.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPatientTree.Name);
            //组合
            this.tbComboOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbComboOrder.Name);
            //取消组合
            this.tbCancelOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbCancelOrder.Name);
            //计算附材
            this.tbCalculatSubl.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbCalculatSubl.Name);
            //添加检查、检验医嘱
            this.tbCheck.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbCheck.Name);
            //草药开立
            this.tbHerbal.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbHerbal.Name);
            //层级医嘱
            this.tbLevelOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbLevelOrder.Name);
            //手术申请
            this.tbOperation.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbOperation.Name);
            //删除组合
            this.tbDelOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbDelOrder.Name);
            //删除单个处方
            this.tbDeleteOne.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbDeleteOne.Name);
            //退出医嘱
            this.tbExitOrder.Visible = (operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbExitOrder.Name);
            //过滤
            this.tbFilter.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbFilter.Name);
            //查询处方
            this.tbQueryOrder.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbQueryOrder.Name);
            //编辑申请单
            this.tblEditPacsApply.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tblEditPacsApply.Name);
            //保存处方
            this.tbSaveOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbSaveOrder.Name);
            //诊出
            this.tbDiagOut.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbDiagOut.Name);
            //取消看诊
            tbBackNoSee.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbBackNoSee.Name);
            //LIS申请单打印
            this.tbLisApply.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbLisApply.Name);
            //PACS申请单打印
            this.tbPacsApply.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPacsApply.Name);
            //LIS结果查看
            this.tbLisResult.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbLisResult.Name);
            //PACS结果查看
            this.tbPacsResult.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add) && dicOutMenu.ContainsKey(tbPacsResult.Name);
            //叫号
            this.tbCall.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbCall.Name);
            //延迟叫号
            tbDelayCall.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbDelayCall.Name);
            //退出窗口
            tbClose.Visible = (operMode == EnumOperMode.Query || operMode == EnumOperMode.Add || operMode == EnumOperMode.Group) && dicOutMenu.ContainsKey(tbClose.Name);
            //留观
            tbRegEmerPatient.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbRegEmerPatient.Name);
            //出观
            tbOutEmerPatient.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbOutEmerPatient.Name);
            //转住院
            this.tbInEmerPatient.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbInEmerPatient.Name);

            //传染病报卡
            tbDiseaseReport.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbDiseaseReport.Name);
            //医技预约
            tbMedTechOrder.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbMedTechOrder.Name);
            //院感上报
            tbInfectionReport.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbInfectionReport.Name);

            //合理用药
            tbPass.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbPass.Name);
            //选择时间 查看历史病人列表
            tbChooseTime.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbChooseTime.Name);

            //医疗结果，目前把所有的医疗结果都放在一起查看了
            tbResultPrint.Visible = (operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbResultPrint.Name);
            //传染病报卡
            this.tbSwipCard.Visible = (operMode == EnumOperMode.Add || operMode == EnumOperMode.Query) && dicOutMenu.ContainsKey(tbSwipCard.Name);
            // {DD27333B-4CBF-4bb2-845D-8D28D616937E}
            #endregion

            #region 设置组套、患者列表的可见性

            if (operMode == EnumOperMode.Query)
            {
                pnPatientList.Visible = true;
                panelTree.Visible = false;
                //这是是什么都不知道...
                panel2.Visible = false;
            }
            else if (operMode == EnumOperMode.Add
                || operMode == EnumOperMode.Group)
            {
                pnPatientList.Visible = false;
                panelTree.Visible = true;
                //这是是什么都不知道...
                panel2.Visible = true;

                if (!(CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe)
                    || CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder)))
                {
                    this.panel2.Visible = false;
                }
            }

            if (operMode == EnumOperMode.Add
                || operMode == EnumOperMode.Group)
            {
                if (tvGroup == null)
                {
                    tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
                    //tvGroup.IsEditGroup = this.isEditGroup;
                    tvGroup.IsEditGroup = operMode == EnumOperMode.Group ? true : false;

                    tvGroup.Init();
                    tvGroup.SelectOrder -= new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                    tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);

                    this.panelTree.Controls.Add(tvGroup);
                    tvGroup.Dock = DockStyle.Fill;
                }
            }

            #endregion


            //下面的另说了

            //if (operMode == EnumOperMode.Add) //开立
            //{
            //    if (tvGroup == null)
            //    {
            //        tvGroup = new FS.HISFC.Components.Common.Controls.tvDoctorGroup();
            //        tvGroup.Type = FS.HISFC.Components.Common.Controls.enuType.Order;
            //        tvGroup.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
            //        tvGroup.IsEditGroup = this.isEditGroup;
            //        tvGroup.Init();
            //        tvGroup.SelectOrder += new FS.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
            //    }
            //    tvGroup.Dock = DockStyle.Fill;
            //    tvGroup.Visible = true;

            //    this.panelTree.Visible = true;
            //    this.panel2.Visible = true;
            //    if (this.btnShow.Visible != true)
            //    {
            //        this.panel2.Width = 250;
            //        this.panelTree.Width = 250;
            //    }
            //    this.panelTree.Controls.Add(tvGroup);
            //    this.pnPatientList.Visible = false;
            //}
            //else
            //{
            //    this.pnPatientList.Visible = true;
            //    this.panelTree.Visible = false;
            //    this.panel2.Visible = false;
            //    if (tvGroup != null)
            //    {
            //        tvGroup.Visible = false;
            //    }
            //}
        }

        /// <summary>
        /// 初始化菜单设置 加载自定义菜单及设置可见性
        /// </summary>
        private void InitMenu()
        {
            #region 设置菜单按钮

            this.tbAddReg.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T添加);

            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            this.tbPackage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T套餐);


            //{FBE92A1C-323E-405e-9F46-ACCA9700DF2A}
            this.tbAppoint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Y预约);

            this.tbMessage.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X信息);


            this.tbAddOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Y医嘱);
            this.tbAddNewOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X新建);
            this.tbMedTechOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.Y预约);
            this.tbInfectionReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FrameWork.WinForms.Classes.EnumImageList.S手动录入);
            //增加扩展按钮
            this.tbExtend1.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M默认);
            this.tbExtend2.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M默认);
            this.tbExtend3.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M默认);

            this.tbChooseUL.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M默认);

            this.tbComboOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H合并);
            this.tbCancelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Q取消);
            this.tbDelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S删除);

            this.tbDeleteOne.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S删除);

            this.tbOperation.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z诊断);
            this.tbSaveOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B保存);
            this.tbCheck.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H换单);
            this.tbClose.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbExitOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbGroup.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z组套);

            this.tbDiagOut.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X下一个);
            tbBackNoSee.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Q取消);

            this.tbRefreshPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S刷新);
            this.tbQueryOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C查询);
            this.tbPatientTree.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.G顾客);

            this.tbPrintOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbOutPreIn.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M默认);
            this.tbPrintAgain.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbPrintOrderPreview.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.D打印预览);

            this.tbRegEmerPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J接诊);
            this.tbOutEmerPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C出院登记);
            this.tbInEmerPatient.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z转科);
            this.tbDiseaseReport.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.J健康档案);
            this.tbHerbal.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.C草药);
            //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 层级形式开立医嘱 yangw 20101024
            this.tbLevelOrder.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z子级);
            this.tbLisResult.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H化验);
            this.tbPacsResult.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S设备);

            this.tbLisApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S申请单);
            this.tbPacsApply.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.S申请单);
            this.tbPass.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B报警);
            this.tbCalculatSubl.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.Z暂存);

            this.tbCall.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.X下一个);

            this.tbDelayCall.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.M默认);

            this.tbChooseTime.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.L历史信息);

            this.tbResultPrint.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.H化验);
            this.tbSwipCard.Image = FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.B报警);
            // {DD27333B-4CBF-4bb2-845D-8D28D616937E}


            this.tbOutPreIn.TextImageRelation = TextImageRelation.ImageAboveText;// {6BF1F99D-7307-4d05-B747-274D24174895}
            #endregion

            ArrayList alMenu = CacheManager.GetConList("OUTPATMENU");
            //foreach (FS.HISFC.Models.Base.Const con in alMenu)
            //{
            //    if (con.IsValid && !dicOutMenu.ContainsKey(con.ID))
            //    {
            //        dicOutMenu.Add(con.ID, con);
            //    }
            //}

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
                }
            }

            this.toolStrip1.Items.Clear();
            dicOutMenu = new Dictionary<string, FS.HISFC.Models.Base.Const>();
            foreach (FS.HISFC.Models.Base.Const conObj in alMenu)
            {
                if (!conObj.IsValid)
                {
                    continue;
                }

                if (!dicOutMenu.ContainsKey(conObj.ID))
                {
                    dicOutMenu.Add(conObj.ID, conObj);
                }

                if (hsTb.Contains(conObj.ID.Trim()))
                {
                    if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripButton))
                    {
                        tButton = new ToolStripButton();
                        tButton = hsTb[conObj.ID.Trim()] as ToolStripButton;

                        tButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(conObj.Memo))
                        {
                            tButton.Text = tButton.Text + "(&" + conObj.Memo + ")";
                        }
                        tButton.Visible = true;
                        this.toolStrip1.Items.Add(tButton);
                    }
                    else if (hsTb[conObj.ID.Trim()].GetType() == typeof(ToolStripDropDownButton))
                    {
                        tdButton = new ToolStripDropDownButton();
                        tdButton = hsTb[conObj.ID.Trim()] as ToolStripDropDownButton;

                        tdButton.Text = conObj.Name.Trim();
                        if (!string.IsNullOrEmpty(conObj.Memo))
                        {
                            tdButton.Text = tdButton.Text + "(&" + conObj.Memo + ")";
                        }
                        tdButton.Visible = true;

                        #region 自定义下拉按钮
                        //自定义下拉按钮1    OUTPATPRINT
                        if (!string.IsNullOrEmpty(conObj.UserCode) 
                            && (string.Equals(conObj.UserCode, "OUTPATPRINT") 
                            || string.Equals(conObj.UserCode, "OUTPATPREVIEW")))
                        {
                            ArrayList dropItemList = CacheManager.GetConList(conObj.UserCode);
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
                                        drop.Click += new System.EventHandler(this.tmPrint_Click);
                                        tdButton.DropDownItems.Add(drop);
                                    }
                                }
                            }
                        }

                        //自定义下拉按钮2
                        if (!string.IsNullOrEmpty(conObj.UserCode) 
                            && string.Equals(conObj.UserCode, "ResultPrint"))
                        {
                            ArrayList dropItemList = CacheManager.GetConList(conObj.UserCode);
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

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.tabControl1.SelectedTab.Controls.Count > 0)
            {
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];

                //传入PatienId流水号{17537415-C168-450d-BBCC-93CFFA19DB82}
                #region 诊断录入界面
                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
                {
                    if (isFirst)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag -= new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag += new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);

                        isFirst = false;
                    }
                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("您没有选择患者！");
                    }
                    else
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).RegInfo = ucOutPatientTree1.PatientInfo;
                    }
                }
                #endregion

                #region 门诊病历界面

                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase))
                {
                    if (isCaseFirst)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).transportAlDiag -= new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).transportAlDiag += new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).SaveClicked -= new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).SaveClicked += new FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase.SaveClickDelegate(frmOutPatientOrder_SaveClicked);

                        isCaseFirst = false;
                    }

                    //自己录入诊断，不需要外面传进来
                    //(this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).diagnoses = this.Diagnoses;
                    //if (this.Diagnoses != null)
                    //{
                    //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).SetDiagNose();
                    //}

                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("您没有选择患者！");
                    }
                    else
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientCase).RegObj = ucOutPatientTree1.PatientInfo;
                    }

                    //this.neuPanel1.Visible = false;
                }

                #endregion
                #region 历史医嘱界面
                //如果切换tab页时，没有点击数列表中的患者，历史医嘱不刷新 houwb {B9D1936C-4564-4e35-A158-40E8688267FF}
                else if (this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory))
                {
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).Patient = this.ucOutPatientTree1.PatientInfo;
                }
                #endregion

                #region 博济处方界面
                else if (this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                {
                    //if (ucOutPatientTree1.PatientInfo == null)
                    //{
                    //    MessageBox.Show("您没有选择患者！");
                    //}
                    //else
                    //{
                    //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe).Patient = ucOutPatientTree1.PatientInfo;
                    //}
                }
                #endregion

                #region 其他界面
                else
                {
                    if (this.ucOutPatientTree1.neuTreeView1.Visible)
                    {
                        (this.CurrentControl as FS.FrameWork.WinForms.Controls.ucBaseControl).SetValue(this.ucOutPatientTree1.PatientInfo, this.ucOutPatientTree1.neuTreeView1.SelectedNode);
                    }
                    else
                    {
                        (this.CurrentControl as FS.FrameWork.WinForms.Controls.ucBaseControl).SetValue(this.ucOutPatientTree1.PatientInfo, this.ucOutPatientTree1.neuTreeView2.SelectedNode);
                    }
                }
                #endregion

                this.SetButton();
            }
        }

        /// <summary>
        /// 诊断保存后调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOutPatientOrder_SaveClicked(object sender, ArrayList e)
        {
            if (e != null)
            {
                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    string msg = "";
                    Hashtable hs = new Hashtable();
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in e)
                    {
                        if (!hs.ContainsKey(diag.DiagInfo.ICD10.Name))
                        {
                            hs.Add(diag.DiagInfo.ICD10.Name, "");
                            this.dcpInstance.CheckDiseaseReport(this, this.ucOutPatientOrder1.Patient, FS.HISFC.Models.Base.ServiceTypes.C, diag.DiagInfo.ICD10.Name, out msg);
                        }
                    }
                }
            }


            if (!this.ucOutPatientOrder1.IsDesignMode)
            {
                return;
            }


            //开立后先显示诊断界面
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Text.Contains("医嘱"))
                {
                    tabControl1.SelectedTab = tabPage;
                    break;
                }
            }

            if (this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
            {
                this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;
                //{0BEB97B8-373D-45d0-A186-12502DD0AADE}
                if (this.ucOutPatientOrder1.Patient.ID.Trim() != this.ucOutPatientTree1.PatientInfo.ID.Trim())
                {
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                }

                if (this.ucOutPatientOrder1.Patient == null)
                {
                    MessageBox.Show("当前尚未选中患者！");
                    return;
                }

                this.statusBar1.Panels[1].Text = "(绿色：新开)(蓝色：收费)(红色：作废)";

                //如果未进诊的，开立时自动进诊{9881FD05-A55B-4fcc-80CB-705CB5F1B206}
                //if (!this.ucOutPatientOrder1.Patient.IsSee)
                //{
                //    this.ucOutPatientTree1.AutoTriage();
                //}
                //if (this.ucOutPatientOrder1.Add() == 0)
                //    this.initButton(true);

                this.ucOutPatientOrder1.SetInPutFocos();
            }
        }

        /// <summary>
        /// 用于传染病上报
        /// </summary>
        /// <param name="arrayDiagnoses"></param>
        void frmOutPatientOrder_transportAlDiag(ArrayList arrayDiagnoses)
        {
            if (arrayDiagnoses.Count > 0)
            {
                this.Diagnoses = arrayDiagnoses;
            }
            return;
        }

        /// <summary>
        /// 开立处方
        /// </summary>
        /// <param name="isNew">是否强制新开立</param>
        private void AddOrder(bool isNew)
        {
            if (this.ucOutPatientOrder1.Patient == null)
            {
                MessageBox.Show("当前尚未选中患者！");
                return;
            }
            //if (ucOutPatientOrder1.Patient.PID.CardNO.StartsWith("9")
            //    && !FS.FrameWork.WinForms.Classes.Function.IsManager())
            //{
            //    MessageBox.Show("当前患者是临时收费患者，不能开立，请使用手工方开立！");
            //    return;
            //}

            #region 有效时间内开立医嘱

            if (!FS.FrameWork.WinForms.Classes.Function.IsManager())
            {
                DateTime dtSys = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime(); //数据库系统时间
                DateTime dtSee = this.ucOutPatientOrder1.Patient.DoctorInfo.SeeDate;//看诊时间
                TimeSpan span = dtSys.Subtract(dtSee);  //时间差

                bool isEmergency = this.ucOutPatientOrder1.Patient.DoctorInfo.Templet.RegLevel.IsEmergency; //急诊

                if (isEmergency && span.Days > validDays)
                {
                    MessageBox.Show("急诊患者，已超过有效时间：" + validDays + " * 24小时，不能开立医嘱。");
                    return;
                }
                if (!isEmergency && span.Days > this.validDays)
                {
                    MessageBox.Show("已超过有效时间：" + validDays + "天，不能开立医嘱。");
                    return;
                }
            }
            #endregion

            #region 收费处方不允许再次开立

            if (!isNew)
            {
                if (!ucOutPatientOrder1.CheckCanAdd())
                {
                    return;
                }
            }

            #endregion

            this.statusBar1.Panels[1].Text = "(绿色：新开)(蓝色：收费)(红色：作废)";

            //如果未进诊的，开立时自动进诊{9881FD05-A55B-4fcc-80CB-705CB5F1B206}
            if (!this.ucOutPatientOrder1.Patient.IsSee)
            {
                this.ucOutPatientTree1.AutoTriage();
            }

            if (this.ucOutPatientOrder1.Add(isNew) == 0)
            {
                OperMode = EnumOperMode.Add;
            }
            else
            {
                return;
            }

            if (!this.ucOutPatientOrder1.isHaveDiag())
            {
                //开立后先显示诊断界面
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Text.Contains("门诊病历"))
                    {
                        tabControl1.SelectedTab = tabPage;
                        break;
                    }
                }
            }
            else
            {
                //{0FF7F0D0-E31A-474b-A2F6-DB1E2CF843C6}
                //开立后先显示诊断界面
                foreach (TabPage tabPage in tabControl1.TabPages)
                {
                    if (tabPage.Text.Contains("医嘱"))
                    {
                        tabControl1.SelectedTab = tabPage;
                        break;
                    }
                }
            }

            #region 开启博济处方

            this.AddOrderOutsource(isNew);

            #endregion
        }

        /// <summary>
        /// 博济处方开立
        /// </summary>
        /// <param name="isNew"></param>
        private void AddOrderOutsource(bool isNew)
        {
            if (this.tabControl1.SelectedTab.Text.Contains("博济"))
            {
                if (this.ucOutPatientRecipe1.Add(isNew) == 0)
                {
                    OperMode = EnumOperMode.Add;
                }
            }
            //if (!object.Equals(ucOutPatientRecipe1, null))
            //{
            //    if (this.ucOutPatientRecipe1.Add(isNew) == 0)
            //    {
            //        this.SetButton(true);
            //    }
            //}
        }

        /// <summary>
        /// 点击按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.ucOutPatientOrder1.SetInputItemVisible(false);

            #region 开立
            if (e.ClickedItem == this.tbAddOrder)//开立
            {
                //{82FC5ABE-B85B-4011-AE0F-8042A89CD327}
                //if (this.ucOutPatientOrder1.Patient == null)
                //{
                //    MessageBox.Show("当前尚未选中患者！");
                //    return;
                //}
                //if (this.ucOutPatientOrder1.DiagIn() < 0)
                //{
                //    MessageBox.Show("进诊失败！");
                //    return;
                //}
                AddOrder(false);
            }
            #endregion

            //{B7A4247B-9D29-48bd-ADE4-A097A8651861}
            #region 查看套餐
            else if (e.ClickedItem == this.tbPackage)
            {
                if (this.ucOutPatientOrder1.Patient == null || string.IsNullOrEmpty(this.ucOutPatientOrder1.Patient.PID.CardNO))
                {
                    MessageBox.Show("请先检索患者！");
                    return;
                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
               // FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                
                //frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);
                //frmpackage.detailVisible = true;
                //frmpackage.ShowDialog();

                FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
                frmpackage.DetailVisible = true;//{187A73EB-008A-4A25-A6CB-28CAE0E629A7}门诊医生站查看套餐详细
                frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);
                frmpackage.ShowDialog();
            }
            #endregion

            //{FBE92A1C-323E-405e-9F46-ACCA9700DF2A}

            #region 诊间预约
            else if (e.ClickedItem == this.tbAppoint)
            {
                if (this.ucOutPatientOrder1.Patient == null || string.IsNullOrEmpty(this.ucOutPatientOrder1.Patient.PID.CardNO))
                {

                }
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
                // FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();

                //frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);
                //frmpackage.detailVisible = true;
                //frmpackage.ShowDialog();

                FS.HISFC.Components.Order.OutPatient.Forms.frmAppointment frmappoint = new FS.HISFC.Components.Order.OutPatient.Forms.frmAppointment();

                if (this.ucOutPatientOrder1.Patient == null || string.IsNullOrEmpty(this.ucOutPatientOrder1.Patient.PID.CardNO))
                {
                    frmappoint.ShowDialog();
                }
                else
                {
                    frmappoint.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);

                    frmappoint.ShowDialog();
                }


                //Form form = new Form();
                //FS.HISFC.Components.Operation.ucAppointNew uc = new FS.HISFC.Components.Operation.ucAppointNew();
                //form.Size = uc.Size;
                //form.Controls.Add(uc);
                //form.StartPosition = FormStartPosition.CenterScreen;
                //form.ShowDialog();
                //MessageBox.Show("加载成功1111222！！");
            }
            #endregion

            //{93020DA1-B5F5-4a29-BBD7-D9BE76E4919A}

            #region 短信发送
            else if (e.ClickedItem == this.tbMessage)
            {
                FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();

                FS.HISFC.Models.RADT.PatientInfo selectedPatient = accountMgr.GetPatientInfoByCardNO(this.ucOutPatientOrder1.Patient.PID.CardNO);//.SelectedNode.Tag as FS.HISFC.Models.RADT.PatientInfo;
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

            #region 新开立
            else if (e.ClickedItem == this.tbAddNewOrder)//开立
            {
                if (this.ucOutPatientOrder1.Patient == null)
                {
                    MessageBox.Show("当前尚未选中患者！");
                    return;
                }
                //{82FC5ABE-B85B-4011-AE0F-8042A89CD327}
                //if (this.ucOutPatientOrder1.DiagIn() < 0)
                //{
                //    MessageBox.Show("进诊失败！");
                //    return;
                //}
                AddOrder(true);
            }
            #endregion

            #region 医技预约
            else if (e.ClickedItem == this.tbMedTechOrder)//开立
            {
                MTOrder.frmMTApply newfrm = new MTOrder.frmMTApply();
                newfrm.PatientInfo = this.Tag as FS.HISFC.Models.Registration.Register;
                newfrm.ShowDialog();
            }
            #endregion

            #region 院感上报
            else if (e.ClickedItem == this.tbInfectionReport)//院感上报
            {
                if (this.ucOutPatientOrder1.Patient == null)
                {
                    MessageBox.Show("请先选择您要上报的患者！");
                    return;
                }
                this.ucOutPatientOrder1.Patient.Memo = "2";//门诊标识
                tbInfectionReport.Tag = this.ucOutPatientOrder1.Patient;
                this.tbInfectionReport_Click(this.tbInfectionReport);
            }
            #endregion

            #region 组套
            else if (e.ClickedItem == this.tbGroup)//组套
            {
                if (this.tbGroup.CheckState == CheckState.Checked)
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;
                    OperMode = EnumOperMode.Query;

                    this.ucOutPatientOrder1.SetEditGroup(false);
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                }
                else
                {
                    this.tbGroup.CheckState = CheckState.Checked;
                    OperMode = EnumOperMode.Group;

                    this.ucOutPatientOrder1.SetEditGroup(true);
                    this.ucOutPatientOrder1.Patient = null;
                }
            }
            #endregion

            #region 草药开立
            else if (e.ClickedItem == this.tbHerbal)
            {
                this.ucOutPatientOrder1.HerbalOrder();
            }
            #endregion

            #region 层级形式开立医嘱
            else if (e.ClickedItem == this.tbLevelOrder)
            {
                //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 层级形式开立医嘱 yangw 20101024
                this.ucOutPatientOrder1.AddLevelOrders();
            }
            #endregion

            #region 添加检查、检验医嘱

            else if (e.ClickedItem == this.tbCheck)
            {
                this.ucOutPatientOrder1.AddTest();
            }
            #endregion

            #region 删除
            else if (e.ClickedItem == this.tbDelOrder)
            {
                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
                {
                    this.ucOutPatientOrder1.Del();
                }
                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                {
                    this.ucOutPatientRecipe1.Del();
                }
            }
            #endregion

            #region 删除
            else if (e.ClickedItem == this.tbDeleteOne)
            {
                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
                {
                    this.ucOutPatientOrder1.DeleteSingleOrder();
                }
                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucOutPatientRecipe))
                {
                    this.ucOutPatientRecipe1.DeleteSingleOrder();
                }
            }
            #endregion

            #region 查询
            else if (e.ClickedItem == this.tbQueryOrder)//查询
            {
                this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                this.ucOutPatientOrder1.Retrieve();
            }
            #endregion

            #region 打印
            else if (e.ClickedItem == this.tbPrintOrder)//打印
            {
                this.ucOutPatientOrder1.PrintRecipe();
            }
            #endregion



            #region 预约入院
            else if (e.ClickedItem == this.tbOutPreIn)// {6BF1F99D-7307-4d05-B747-274D24174895}
            {
                this.ucOutPatientOrder1.PrePayIn();
            }
            #endregion
            /*
            #region 补打
            else if (e.ClickedItem == this.tbPrintAgain)
            {
                this.ucOutPatientOrder1.PrintAgainAll();
            }
            #endregion
            */

            #region 组合
            else if (e.ClickedItem == this.tbComboOrder)//组合
            {
                this.ucOutPatientOrder1.ComboOrder();
            }
            #endregion

            #region 取消组合
            else if (e.ClickedItem == this.tbCancelOrder)//取消组合
            {
                this.ucOutPatientOrder1.CancelCombo();
            }
            #endregion

            #region 退出医嘱
            else if (e.ClickedItem == this.tbExitOrder)//退出医嘱
            {
                if (operMode == EnumOperMode.Group)
                {
                    this.tbGroup.CheckState = CheckState.Unchecked;

                    this.ucOutPatientOrder1.SetEditGroup(false);
                    //this.initButtonGroup(false);
                    OperMode = EnumOperMode.Query;

                    this.ucOutPatientOrder1.SetEditGroup(false);
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                }
                else
                {
                    if (this.ucOutPatientOrder1.ExitOrder() == 0)
                    {
                        OperMode = EnumOperMode.Query;

                        //退出时判断取消进诊标记
                        this.ucOutPatientTree1.CancelTriage();
                    }
                    if (!object.Equals(this.ucOutPatientRecipe1, null))
                    {
                        if (this.ucOutPatientRecipe1.ExitOrder() == 0)
                        {
                        }
                    }
                }
            }
            #endregion

            #region 留观

            else if (e.ClickedItem == this.tbRegEmerPatient)//留观
            {
                if (this.ucOutPatientOrder1.RegisterEmergencyPatient() < 0)
                {
                }
                else
                {
                    //MessageBox.Show("留观成功！");

                    ucOutPatientTree1.RefreshNoSeePatient();
                    ucOutPatientTree1.RefreshSeePatient();
                }
            }
            #endregion

            #region 出关
            else if (e.ClickedItem == this.tbOutEmerPatient) //出关
            {
                if (this.ucOutPatientOrder1.OutEmergencyPatient() > 0)
                {
                    ucOutPatientTree1.RefreshNoSeePatient();
                    ucOutPatientTree1.RefreshSeePatient();
                }
            }
            #endregion

            #region 留观转住院

            else if (e.ClickedItem == this.tbInEmerPatient) //转住院
            {
                if (this.ucOutPatientOrder1.InEmergencyPatient() > 0)
                {
                    ucOutPatientTree1.RefreshNoSeePatient();
                    //ucOutPatientTree1.RefreshTreePatientDone();
                }
            }
            #endregion

            #region 诊出
            else if (e.ClickedItem == this.tbDiagOut)//诊出
            {
                if (this.ucOutPatientOrder1.DiagOut() < 0)
                {
                }
                else
                {
                    ucOutPatientTree1.RefreshNoSeePatient();
                    //ucOutPatientTree1.RefreshTreePatientDone();
                }
            }
            #endregion

            #region 取消看诊
            else if (e.ClickedItem == this.tbBackNoSee)//诊出
            {
                if (this.ucOutPatientOrder1.CanCelDiag() < 0)
                {
                }
                else
                {
                    MessageBox.Show(this.ucOutPatientOrder1.Patient.Name + " 取消看诊成功！");

                    ucOutPatientTree1.RefreshNoSeePatient();
                    ucOutPatientTree1.RefreshSeePatient();
                }
            }
            #endregion

            #region 刷新人员列表
            else if (e.ClickedItem == this.tbRefreshPatient)//刷新
            {
                ucOutPatientTree1.RefreshNoSeePatient();
                ucOutPatientTree1.RefreshSeePatient();
            }
            #endregion

            #region 人员列表
            else if (e.ClickedItem == this.tbPatientTree)//列表
            {
                this.pnPatientList.Visible = !this.pnPatientList.Visible;
            }
            #endregion

            #region 保存处方
            else if (e.ClickedItem == this.tbSaveOrder)//保存
            {
                this.tbSaveOrder.Enabled = false;
                try
                {
                    if (operMode == EnumOperMode.Group)
                    {
                        SaveGroup();
                    }
                    else
                    {
                        if (this.tabControl1.SelectedTab.Text.Contains("博济"))
                        {
                            Classes.LogManager.Write("开始保存博济处方!");

                            if (this.ucOutPatientRecipe1.Save() == -1)
                            {
                                return;
                            }
                            else
                            {
                                Classes.LogManager.Write("结束保存博济处方!");
                            }

                        }

                        Classes.LogManager.Write("开始保存处方!");
                        if (this.ucOutPatientOrder1.Save() == -1)
                        {
                        }
                        else
                        {
                            Classes.LogManager.Write("结束保存处方!");

                            OperMode = EnumOperMode.Query;

                            Classes.LogManager.Write("开始刷新患者列表!");
                            ucOutPatientTree1.RefreshNoSeePatient();
                            //ucOutPatientTree1.RefreshTreePatientDone();
                            ucOutPatientTree1.FreshSeePatientNode();

                            Classes.LogManager.Write("结束刷新患者列表!");
                        }



                    }
                    this.statusBar1.Panels[1].Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("frmOutPatientOrder" + ex.Message);
                }
                finally
                {
                    this.tbSaveOrder.Enabled = true;
                }
            }
            #endregion

            #region 退出窗口
            else if (e.ClickedItem == this.tbClose)//退出
            {
                if (this.ucOutPatientOrder1 != null && this.ucOutPatientOrder1.IsDesignMode) //是在开立状态
                {
                    DialogResult result = MessageBox.Show(FS.FrameWork.Management.Language.Msg("医嘱目前处于开立模式，是否保存?"), "提示", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {
                        if (this.ucOutPatientOrder1.Save() == 0)
                        {
                            //退出时判断取消进诊标记
                            this.ucOutPatientTree1.CancelTriage();
                            this.Close();
                        }
                    }
                    else if (result == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        //退出时判断取消进诊标记
                        this.ucOutPatientTree1.CancelTriage();
                        this.Close();
                    }
                }
                else
                {
                    this.Close();
                }
            }
            #endregion

            #region 传染病报告卡
            else if (e.ClickedItem == this.tbDiseaseReport)     //  {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2} 传染病报告卡
            {
                if (this.dcpInstance == null)
                {
                    this.dcpInstance = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP)) as FS.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    FS.HISFC.Models.RADT.Patient patient = this.ucOutPatientTree1.PatientInfo as FS.HISFC.Models.RADT.Patient;

                    this.dcpInstance.RegisterDiseaseReport(this, patient, FS.HISFC.Models.Base.ServiceTypes.C);
                }
            }
            #endregion

            #region 刷卡
            else if (e.ClickedItem == this.tbSwipCard) // {DC68D3DF-1CB9-4d58-93E0-4F85B58E1647}  
            {
                string mCardNo = "";
                string error = "";

                if (Function.OperMCard(ref mCardNo, ref error) < 0)
                {
                    MessageBox.Show("读卡失败，请确认是否正确放置诊疗卡！\n" + error);
                    return;
                }
                mCardNo = ";" + mCardNo;
                this.ucOutPatientTree1.QueryPatientInfo(mCardNo);
            }
            #endregion
            #region LIS结果打印
            //LIS结果打印
            else if (e.ClickedItem == this.tbLisResult)
            {
                this.ucOutPatientOrder1.QueryLisResult();
            }
            #endregion

            #region PACS结果打印
            else if (e.ClickedItem == this.tbPacsResult)
            {
                ucOutPatientOrder1.QueryPacsReport();
            }
            #endregion

            #region 排队叫号
            else if (e.ClickedItem == this.tbCall)
            {
                this.ucOutPatientTree1.Call();
            }
            #endregion

            #region 过号
            else if (e.ClickedItem == this.tbDelayCall)
            {
                this.ucOutPatientTree1.DelayCall();
            }
            #endregion

            #region 申请单打印

            //LIS申请单打印
            else if (e.ClickedItem == this.tbLisApply)
            {
                this.ucOutPatientOrder1.LisReportPrint();
            }
            //PACS申请单打印
            else if (e.ClickedItem == this.tbPacsApply)
            {
                this.ucOutPatientOrder1.PacsReportPrint();
            }

            #endregion

            #region 电子申请单添加

            else if (e.ClickedItem == this.tblEditPacsApply)
            {
                this.ucOutPatientOrder1.EditPascApply();
            }
            #endregion

            #region 合理用药开关
            else if (e.ClickedItem == this.tbPass)
            {
                this.tbPass.Checked = !this.tbPass.Checked;

                this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;

                FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting userSetmgr = new FS.HISFC.BizProcess.Integrate.Common.UserDefaultSetting();
                FS.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(CacheManager.LogEmpl.ID);

                if (setting == null)
                {
                    setting = new FS.HISFC.Models.Base.UserDefaultSetting();
                    setting.Empl.ID = CacheManager.LogEmpl.ID;
                    setting.Oper.ID = CacheManager.LogEmpl.ID;
                    FS.HISFC.BizLogic.Admin.FunSetting funMgr = new FS.HISFC.BizLogic.Admin.FunSetting();
                    setting.Oper.OperTime = funMgr.GetDateTimeFromSysDateTime();
                    setting.Setting4 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

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
                    setting.Setting4 = FS.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

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

            #region 计算附材
            else if (e.ClickedItem == this.tbCalculatSubl)
            {
                this.ucOutPatientOrder1.CalculatSubl(false);
            }
            #endregion


            #region 按时间段查询看诊患者
            else if (e.ClickedItem == this.tbChooseTime)
            {
                this.QueryPatientByTimeSpan();
            }
            #endregion


            #region 开立界面显示
            ////开立界面显示
            //if (this.ucOutPatientOrder1.IsDesignMode)
            //{
            //    if (this.CurrentControl.GetType() != typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucOutPatientOrder))
            //    {
            //        this.panel2.Visible = false;
            //    }
            //    else
            //    {
            //        this.panel2.Visible = true;
            //    }
            //}
            #endregion

            #region 诊断保存（用于跳转到开立界面和传染病报告提示）
            //if (this.CurrentControl != null
            //    && this.CurrentControl.GetType() == typeof(FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
            //{
            //    //保存诊断后，自动跳转到开立界面
            //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
            //    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new FS.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
            //}
            #endregion


            #region 新版电子病历相关--门诊病历

            //因总部在整合，此处暂时不处理，考虑后续用接口挂tab页的方式实现

            //{4F2B8C3A-A728-4668-9879-37BF75DBE6E2}
            //else if (e.ClickedItem == this.tbEmergency)
            //{
            //    this.SetOutpatientCase(this.ucOutPatientTree1.PatientInfo, CaseType.Out_Emergency_Record);
            //}
            //else if (e.ClickedItem == this.tbFirst)
            //{
            //    this.SetOutpatientCase(this.ucOutPatientTree1.PatientInfo, CaseType.Out_First);
            //}
            //else if (e.ClickedItem == this.tbSecond)
            //{
            //    this.SetOutpatientCase(this.ucOutPatientTree1.PatientInfo, CaseType.Out_Second_Record);
            //}
            #endregion


            #region 扩展按钮1

            //扩展1 只能在开立模式下使用
            else if (e.ClickedItem == this.tbExtend1)
            {
                if (IOrderExtendModule != null)
                {
                    if (this.ucOutPatientOrder1.Patient == null)
                    {
                        MessageBox.Show("当前尚未选中患者！");
                        return;
                    }
                    if (IOrderExtendModule.DoOrderExtend1(this, this.ucOutPatientTree1.PatientInfo, null) <= 0)
                    {
                        if (string.IsNullOrEmpty(IOrderExtendModule.Err))
                        {
                            MessageBox.Show(IOrderExtendModule.Err, "错误", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
            #endregion
            #region 扩展按钮2
            //扩展2 任何时候都能使用
            else if (e.ClickedItem == this.tbExtend2)//开立
            {
                if (IOrderExtendModule != null)
                {
                    if (this.ucOutPatientOrder1.Patient == null)
                    {
                        MessageBox.Show("当前尚未选中患者！");
                        return;
                    }
                    if (IOrderExtendModule.DoOrderExtend2(this, this.ucOutPatientTree1.PatientInfo, null) <= 0)
                    {
                        if (string.IsNullOrEmpty(IOrderExtendModule.Err))
                        {
                            MessageBox.Show(IOrderExtendModule.Err, "错误", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
            #endregion
            #region 扩展按钮3
            //扩展3 只能在非开立模式下使用
            else if (e.ClickedItem == this.tbExtend3)//开立
            {
                if (IOrderExtendModule != null)
                {
                    //if (this.ucOutPatientOrder1.Patient == null)
                    //{
                    //    MessageBox.Show("当前尚未选中患者！");
                    //    return;
                    //}
                    if (IOrderExtendModule.DoOrderExtend3(this, this.ucOutPatientTree1.PatientInfo, null) <= 0)
                    {
                        if (string.IsNullOrEmpty(IOrderExtendModule.Err))
                        {
                            MessageBox.Show(IOrderExtendModule.Err, "错误", MessageBoxButtons.OK);
                            return;
                        }
                    }
                }
            }
            #endregion

            #region 检验项目开立界面
            else if (e.ClickedItem == this.tbChooseUL)
            {
                if (this.IOrderChooseUl != null)
                {
                    if (this.ucOutPatientOrder1.Patient == null)
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
                            this.ucOutPatientOrder1.AddGroupOrder(alOrders);
                        }
                    }
                }
            }
            #endregion

            #region 补挂号
            else if (e.ClickedItem == this.tbAddReg)
            {
                this.ucOutPatientTree1.AddNewReg();
                this.ucOutPatientTree1.RefreshNoSeePatient();
            }
            #endregion

            if (tvGroup != null)
            {
                if (ucOutPatientOrder1.Patient != null)
                {
                    tvGroup.Pact = ucOutPatientOrder1.Patient.Pact;
                }
                else
                {
                    tvGroup.Pact = null;
                }
            }
        }


        /// <summary>
        /// 根据时间段查询已诊患者
        /// </summary>
        private void QueryPatientByTimeSpan()
        {
            FS.FrameWork.WinForms.Forms.frmChooseDate chooseForm = new FS.FrameWork.WinForms.Forms.frmChooseDate();
            chooseForm.ShowDialog();
            //if (!this.ucOutPatientTree1.BAlreadyState)
            //{
            //    this.ucOutPatientTree1.BAlreadyState = true;
            //}
            this.ucOutPatientTree1.RefreshSeePatientByDate(new DateTime[] { chooseForm.DateBegin, chooseForm.DateEnd });

        }

        /// <summary>
        /// 保存组套
        /// </summary>
        private void SaveGroup()
        {
            FS.HISFC.Components.Common.Forms.frmOrderGroupManager group = new FS.HISFC.Components.Common.Forms.frmOrderGroupManager();
            group.InpatientType = FS.HISFC.Models.Base.ServiceTypes.C;
            try
            {
                group.IsManager = (FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            for (int i = 0; i < this.ucOutPatientOrder1.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                //if (this.ucOutPatientOrder1.neuSpread1.ActiveSheet.IsSelected(i, 0))
                //{
                FS.HISFC.Models.Order.OutPatient.Order order = this.ucOutPatientOrder1.GetObjectFromFarPoint(i, this.ucOutPatientOrder1.neuSpread1.ActiveSheetIndex).Clone();
                if (order == null)
                {
                    MessageBox.Show("获得医嘱出错！");
                }
                else
                {
                    string s = order.Item.Name;
                    string sno = order.Combo.ID;
                    //保存医嘱组套 默认开立时间为 零点
                    order.BeginTime = new DateTime(order.BeginTime.Year, order.BeginTime.Month, order.BeginTime.Day, 0, 0, 0);
                    al.Add(order);
                }
                //}
            }
            if (al.Count > 0)
            {
                group.alItems = al;
                group.ShowDialog();
                this.tvGroup.RefrshGroup();
                this.ucOutPatientOrder1.neuSpread1.ActiveSheet.RowCount = 0;
            }
        }

        #endregion

        #region IPreArrange 成员

        /// <summary>
        /// 登陆前的各种判断
        /// </summary>
        /// <returns></returns>
        public int PreArrange()
        {
            Classes.LogManager.Write("【开始门诊医生from界面初始化前 动作】");

            //增加取消功能 2011-1-3 houwb{DA7F7F3C-C9A6-4bcf-9181-93A6238B13F7}
            if (this.ucOutPatientTree1.InitControl() == -1)
            {
                return -1;
            }

            if (this.ucOutPatientTree1.RefreshNoSeePatient() == -1)
            {
                return -1;
            }

            FS.HISFC.Models.Base.Department currentDept = SOC.HISFC.BizProcess.Cache.Common.GetDeptInfo(CacheManager.LogEmpl.Dept.ID);

            if (currentDept == null || currentDept.ID.Length < 0)
            {
                return -1;
            }

            isHosDeptCanLogin = FS.FrameWork.Function.NConvert.ToBoolean(Classes.Function.GetBatchControlParam("LHMZ08", false, "0"));

            if (currentDept.DeptType.ID.ToString() == "I")
            {
                if (isHosDeptCanLogin)
                {
                    if (MessageBox.Show("您当前登陆的科室为住院科室，是否继续？",
                                         "询问",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question,
                                         MessageBoxDefaultButton.Button2) == DialogResult.No)
                    {
                        return -1;
                    }
                }
                else
                {
                    MessageBox.Show("您当前登陆的科室为住院科室，请重新选择。", "门诊医生站不允许登录住院科室");
                    return -1;
                }
            }

            //对于分诊流程，很多地方需要判断诊室，改为选择诊室后立即赋值到ucOutPatientOrder
            this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;

            Classes.LogManager.Write("【结束门诊医生from界面初始化前 动作】");
            return 1;
        }

        #endregion

        #region IInterfaceContainer 成员    {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2}

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] 
                { 
                    typeof(FS.HISFC.BizProcess.Interface.DCP.IDCP) 
                };
            }
        }

        #endregion

        #region EMR门诊病历接口  目前无用

        /// <summary>
        /// 调用门诊病历
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="type"></param>
        public void SetOutpatientCase(FS.HISFC.Models.Registration.Register reg, CaseType type)
        {
            #region 因总部在整合，此处暂时不处理，考虑后续用接口挂tab页的方式实现


            //if (reg == null || string.IsNullOrEmpty(reg.ID))
            //{
            //    MessageBox.Show("请选择患者信息");
            //    return;
            //}
            //FS.HISFC.BizProcess.Integrate.EMRService.EMRServiceOutpatient emrService = new FS.HISFC.BizProcess.Integrate.EMRService.EMRServiceOutpatient();
            //long emrID = 0;
            //if (emrService.GetEmrRegId(reg.ID, ref emrID) < 0)
            //{
            //    MessageBox.Show("获取EMR门诊流水号失败" + emrService.Err);
            //    return;
            //}
            //FS.Emr.HisInterface.Bll.Application.Facade.RecordInterface emrInterface = new FS.Emr.HisInterface.Bll.Application.Facade.RecordInterface();
            //FS.HISFC.Models.Base.Employee emp = CacheManager.OrderMgr.Operator as FS.HISFC.Models.Base.Employee;
            //string queryEmp = @"select e.ID from vemr_emp e where e.EMPL_CODE='{0}'";
            //string querydept = @"select d.id from vemr_department d where d.dept_code='{0}'";

            //queryEmp = string.Format(queryEmp, emp.ID);
            //querydept = string.Format(querydept, emp.Dept.ID);
            //string empid = CacheManager.OrderMgr.ExecSqlReturnOne(queryEmp);
            //string deptID = CacheManager.OrderMgr.ExecSqlReturnOne(querydept);
            //if (empid == "-1" || deptID == "-1")
            //{
            //    MessageBox.Show("操作员编号或者科室在EMR系统不存在");
            //    return;
            //}

            //emrInterface.CreatNewOutSetByPatient(emrID, Convert.ToInt64(empid), CacheManager.OrderMgr.GetDateTimeFromSysDateTime());
            //FS.Emr.HisInterface.UI.Internal.Facade.RecordUIFacde emrUIInterface = new FS.Emr.HisInterface.UI.Internal.Facade.RecordUIFacde();
            //System.Windows.Forms.Control con = emrUIInterface.CreatNewOutRecord(emrID, Convert.ToInt64(deptID), Convert.ToInt64(empid), type.ToString());
            //FS.FrameWork.WinForms.Classes.Function.PopShowControl(con);

            #endregion
        }

        /// <summary>
        /// 病历类型
        /// </summary>
        public enum CaseType
        {
            /// <summary>
            /// 急诊
            /// </summary>
            Out_Emergency_Record,

            /// <summary>
            /// 首诊
            /// </summary>
            Out_First,

            /// <summary>
            /// 复诊
            /// </summary>
            Out_Second_Record
        }
        #endregion
    }
}
