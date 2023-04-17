using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.Order.OutPatient.Forms.FoSi
{
    /// <summary>
    /// 门诊医生站主界面
    /// </summary>
    public partial class frmOutPatientOrder : Neusoft.FrameWork.WinForms.Forms.frmBaseForm, Neusoft.FrameWork.WinForms.Classes.IPreArrange, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public frmOutPatientOrder()
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.iControlable = this.ucOutPatientOrder1 as Neusoft.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.ucOutPatientOrder1;
                this.panelToolBar.Visible = false;
                this.FormClosing += new FormClosingEventHandler(frmOutPatientOrder_FormClosing);
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }

        void frmOutPatientOrder_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.ucOutPatientOrder1.QuitPass();
            this.ucOutPatientOrder1.ReleaseLisInterface();
            this.ucOutPatientOrder1.ReleasePacsInterface();

            return;
        }

        #region 变量

        /// <summary>
        /// 账户操作的业务层  {184209CF-569F-4355-896D-FB33FF6C506F} 
        /// </summary>
        private Neusoft.HISFC.BizProcess.Integrate.Fee feeMgr = new Neusoft.HISFC.BizProcess.Integrate.Fee();
        private Neusoft.HISFC.BizLogic.Order.Order orderManager = new Neusoft.HISFC.BizLogic.Order.Order();

        private HISFC.Components.Order.OutPatient.Classes.Function Function = new HISFC.Components.Order.OutPatient.Classes.Function();

        private Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();

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
        private Neusoft.HISFC.BizProcess.Interface.DCP.IDCP dcpInstance = null;

        /// <summary>
        /// 组套树列表
        /// </summary>
        Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup tvGroup = null;

        /// <summary>
        /// 是否编辑组套
        /// </summary>
        bool isEditGroup = false;

        public ArrayList Diagnoses = null;

        /// <summary>
        /// 是否首次录入诊断，第一次需要加载诊断列表
        /// </summary>
        private bool isFirst = true;

        #endregion

        #region 事件

        private void frmOutPatientOrder_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            this.tbFilter.DropDownItemClicked += new ToolStripItemClickedEventHandler(toolStrip1_ItemClicked);

            ////this.AddOrderHandle();
            this.initButton(false);

            this.tbAddOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Y医嘱);
            this.tbComboOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H合并);
            this.tbCancelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Q取消);
            this.tbDelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S删除);
            this.tbOperation.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z诊断);
            this.tbSaveOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.B保存);
            this.tbCheck.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H换单);
            this.tb1Exit.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbExitOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.T退出);
            this.tbGroup.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z组套);

            this.tbSeePatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.X下一个);
            this.tbRefreshPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S刷新);
            this.tbQueryOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C查询);
            this.tbPatientTree.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.G顾客);

            this.tbPrintOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.D打印);
            this.tbRegEmerPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.J接诊);
            this.tbOutEmerPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C出院登记);
            this.tbInEmerPatient.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z转科);
            this.tbDiseaseReport.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.J健康档案);
            this.tbHerbal.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.C草药);
            //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 层级形式开立医嘱 yangw 20101024
            this.tbLevelOrder.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z子级);
            this.tbLisResultPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.H化验);
            this.tbPacsResultPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S设备);

            this.tbLisReportPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S申请单);
            this.tbPacsReportPrint.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.S申请单);
            this.tbPass.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.B报警);
            this.tbCalculatSubl.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.Z暂存);

            this.tbStation.Image = Neusoft.FrameWork.WinForms.Classes.Function.GetImage(Neusoft.FrameWork.WinForms.Classes.EnumImageList.X下一个);
            this.SetMenuVisible();

            this.panelTree.Height = this.Height - 162;
            this.panelTree.Visible = false;

            this.ucOutPatientTree1.TreeDoubleClick += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.TreeDoubleClickHandler(ucOutPatientTree1_TreeDoubleClick);

            this.isAccountTerimal = controlIntegrate.GetControlParam<bool>(Neusoft.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, true, false);
            this.isAccountMode = controlIntegrate.GetControlParam<bool>("MZ0300", true, false);

            this.ucOutPatientOrder1.IsAccountTerimal = this.isAccountTerimal;
            this.ucOutPatientOrder1.IsAccountMode = this.isAccountMode;
            this.ucOutPatientTree1.IsAccountMode = this.isAccountMode;
            this.ucOutPatientOrder1.OnRefreshGroupTree += new EventHandler(ucOutPatientOrder1_OnRefreshGroupTree);
            this.ucOutPatientTree1.CopyRecipeByPatientTree += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.CopyRecipeByPatientTreeHandler(ucOutPatientTree1_CopyRecipeByPatientTree);
            this.ucOutPatientTree1.DeleteRecipeByPatientTree += new HISFC.Components.Order.OutPatient.Controls.ucOutPatientTree.DeleteRecipeByPatientTreeHandler(ucOutPatientTree1_DeleteRecipeByPatientTree);

            foreach (TabPage tb in this.tabControl1.TabPages)
            {
                tb.Text = Neusoft.FrameWork.Management.Language.Msg(tb.Text);
            }

            Application.DoEvents();

            if (this.dcpInstance == null)
            {
                this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
            }

            if (this.dcpInstance != null)
            {
                this.dcpInstance.LoadNotice(this, Neusoft.HISFC.Models.Base.ServiceTypes.C);
            }

            try
            {

                Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting userSetmgr = new Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting();
                Neusoft.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(userSetmgr.Operator.ID);

                if (setting == null)
                {
                    //MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    this.tbPass.Checked = Neusoft.FrameWork.Function.NConvert.ToBoolean(setting.Setting4);
                    this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            this.tbPass.CheckedChanged += new EventHandler(tbPass_CheckedChanged);
        }

        void tbPass_CheckedChanged(object sender, EventArgs e)
        {
            this.ucOutPatientOrder1.SetInputItemVisible(false);

            this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;            

        Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting userSetmgr = new Neusoft.HISFC.BizLogic.Manager.UserDefaultSetting();
            Neusoft.HISFC.Models.Base.UserDefaultSetting setting = userSetmgr.Query(userSetmgr.Operator.ID);

            if (setting == null)
            {
                MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                setting.Setting4 = Neusoft.FrameWork.Function.NConvert.ToInt32(this.tbPass.Checked).ToString();

                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                if (userSetmgr.Update(setting) == -1)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(userSetmgr.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();
            }
        }

        void ucOutPatientOrder1_OnRefreshGroupTree(object sender, EventArgs e)
        {
            this.tvGroup.RefrshGroup();
        }

        /// <summary>
        /// 点击患者数节点后查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    this.Tag = this.ucOutPatientTree1.neuTreeView2.SelectedNode.Tag;
                }

                return;

                if (isAccountMode)
                {
                    #region 此处不再扣费，医嘱保存时才收费
                    if (this.Tag is Neusoft.HISFC.Models.Registration.Register)
                    {
                        //判断账户流程的挂号收费情况
                        bool isAccount = false;
                        decimal vacancy = 0m;
                        Neusoft.HISFC.Models.Registration.Register r = (Neusoft.HISFC.Models.Registration.Register)Tag;

                        if (isAccountTerimal && r.IsAccount)
                        {

                            if (feeMgr.GetAccountVacancy(r.PID.CardNO, ref vacancy) <= 0)
                            {
                                MessageBox.Show(feeMgr.Err);
                                return;
                            }
                            isAccount = true;
                        }
                        if (isAccount && r.IsFee == false)
                        {
                            #region 账户扣取挂号费

                            if (!feeMgr.CheckAccountPassWord(r))
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

                                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                                if (feeMgr.AccountPay(r, r.OwnCost, "挂号收费", (orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID, string.Empty) < 0)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("扣账户金额失败！" + feeMgr.Err);
                                    return;
                                }
                                Neusoft.HISFC.BizProcess.Integrate.Registration.Registration registerManager = new Neusoft.HISFC.BizProcess.Integrate.Registration.Registration();
                                r.SeeDoct.ID = orderManager.Operator.ID;
                                r.SeeDoct.Dept.ID = (orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID;
                                if (registerManager.UpdateAccountFeeState(r.ID, r.SeeDoct.ID, r.SeeDoct.Dept.ID, orderManager.GetDateTimeFromSysDateTime()) == -1)
                                {
                                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                                    MessageBox.Show("更新挂号表已收费状态出错");
                                    return;
                                }
                                Neusoft.FrameWork.Management.PublicTrans.Commit();
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

        private void ucOutPatientTree1_CopyRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister, int copyNum)
        {
            this.ucOutPatientOrder1.CopyRecipeByPatientTree(selectRegister, copyNum);
        }

        private void ucOutPatientTree1_DeleteRecipeByPatientTree(Neusoft.HISFC.Models.Registration.Register selectRegister)
        {
            this.ucOutPatientOrder1.DeleteRecipeByPatientTree(selectRegister);
        }


        /// <summary>
        /// 选择组套
        /// </summary>
        /// <param name="alOrders"></param>
        void tvGroup_SelectOrder(System.Collections.ArrayList alOrders)
        {
            this.ucOutPatientOrder1.AddGroupOrder(alOrders);

            ////{D42BEEA5-1716-4be4-9F0A-4AF8AAF88988} //草药弹出草药开立界面
            //ArrayList alHerbal = new ArrayList(); //草药

            //Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在加载处方信息，请稍后", 10, false);
            //Application.DoEvents();
            //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();

            //int count = 1;
            //foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrders)
            //{
            //    //Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm(count);
            //    //Application.DoEvents();

            //    if (this.ucOutPatientOrder1.Patient != null && ucOutPatientOrder1.IsDesignMode)
            //    {
            //        #region 判断开立权限

            //        string error = "";

            //        int ret = 1;

            //        //处方权
            //        ret = Components.Order.Classes.Function.JudgeEmplPriv(order, orderManager.Operator,
            //            (orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept, Neusoft.HISFC.Models.Base.DoctorPrivType.RecipePriv, true, ref error);

            //        if (ret <= 0)
            //        {
            //            MessageBox.Show(error);
            //            continue;
            //        }

            //        //过敏史判断
            //        ret = Components.Order.Classes.Function.JudgePatientAllergy("1", this.ucOutPatientOrder1.Patient.PID, order, ref error);

            //        if (ret <= 0)
            //        {
            //            return;
            //        }
            //        #endregion
            //    }

            //    if (order.Item.SysClass.ID.ToString() == "PCC") //草药
            //    {
            //        Classes.Function.ReComputeQty(order);

            //        alHerbal.Add(order);
            //    }
            //    else
            //    {
            //        this.ucOutPatientOrder1.AddNewOrder(order, 0);
            //    }
            //    count += 1;
            //}
            //if (alHerbal.Count > 0)
            //{
            //    this.ucOutPatientOrder1.AddHerbalOrders(alHerbal);
            //}
            //this.ucOutPatientOrder1.RefreshOrderState();
            //this.ucOutPatientOrder1.RefreshCombo();

            //Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 点击开立
        /// </summary>
        /// <param name="isDisign"></param>
        private void initButton(bool isDisign)
        {
            this.tbGroup.Enabled = !isDisign;
            //开立界面不允许打印处方，避免打印内容和保存内容不一致
            this.tbPrintOrder.Enabled = !isDisign;
            this.tbAddOrder.Enabled = !isDisign;
            this.tbComboOrder.Enabled = isDisign;
            this.tbPatientTree.Enabled = !isDisign;
            this.tbCalculatSubl.Enabled = isDisign;
            this.tbCancelOrder.Enabled = isDisign;
            this.tbCheck.Enabled = isDisign;
            this.tbHerbal.Enabled = isDisign;
            this.tbLevelOrder.Enabled = isDisign;
            this.tbOperation.Enabled = false;
            this.tbDelOrder.Enabled = isDisign;
            this.tbDelOneOrder.Enabled = isDisign;
            this.tbExitOrder.Enabled = isDisign;
            this.tbFilter.Enabled = !isDisign;
            this.tbFilter.Visible = false;
            this.tbQueryOrder.Enabled = !isDisign;

            this.tbSaveOrder.Enabled = isDisign;
            this.tbSeePatient.Enabled = !isDisign;

            this.tbLisReportPrint.Enabled = !isDisign;
            this.tbPacsReportPrint.Enabled = !isDisign;

            this.tbStation.Enabled = !isDisign;
            if (isDisign) //开立
            {
                if (tvGroup == null)
                {
                    tvGroup = new Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = Neusoft.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.InpatientType = Neusoft.HISFC.Models.Base.ServiceTypes.C;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new Neusoft.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;

                this.panelTree.Visible = true;
                this.panel2.Visible = true;
                if (this.btnShow.Visible != true)
                {
                    this.panel2.Width = 170;
                    this.panelTree.Width = 170;
                }
                this.panelTree.Controls.Add(tvGroup);
                //this.SetTree(tvGroup);
                this.neuPanel1.Visible = false;
            }
            else
            {
                this.neuPanel1.Visible = true;
                this.panelTree.Visible = false;
                this.panel2.Visible = false;
                if (tvGroup != null) tvGroup.Visible = false;
                //this.ucOutPatientOrder1.Patient = new Neusoft.HISFC.Models.Registration.Register();
            }
        }

        /// <summary>
        /// 组套、开立显示菜单
        /// </summary>
        /// <param name="isEdit">是否组套模式</param>
        private void initButtonGroup(bool isEdit)
        {
            this.tbAddOrder.Enabled = !isEdit;
            this.tbPatientTree.Enabled = !isEdit;
            this.tbSaveOrder.Enabled = isEdit;
            //this.tbExitOrder.Enabled = !isEdit;
            this.isEditGroup = isEdit;
            this.tbQueryOrder.Enabled = !isEdit;
            this.tbLisReportPrint.Enabled = !isEdit;
            this.tbPacsReportPrint.Enabled = !isEdit;

            this.tbSeePatient.Enabled = !isEdit;
            //{CF7BCF69-95C3-4dcf-A61C-451E26C56161}
            this.tbComboOrder.Enabled = isEdit;
            //this.tbCalculatSubl.Enabled = isEdit;
            this.tbHerbal.Enabled = isEdit;
            this.tbCancelOrder.Enabled = isEdit;
            this.tbDelOrder.Enabled = isEdit;//{11F97F55-F747-4ad9-A74F-086635D5EBD9}
            this.tbDelOneOrder.Enabled = isEdit;
            if (isEdit) //开立
            {
                if (tvGroup == null)
                {
                    tvGroup = new Neusoft.HISFC.Components.Common.Controls.tvDoctorGroup();
                    tvGroup.Type = Neusoft.HISFC.Components.Common.Controls.enuType.Order;
                    tvGroup.InpatientType = Neusoft.HISFC.Models.Base.ServiceTypes.C;
                    tvGroup.Init();
                    tvGroup.SelectOrder += new Neusoft.HISFC.Components.Common.Controls.SelectOrderHandler(tvGroup_SelectOrder);
                }
                tvGroup.Dock = DockStyle.Fill;
                tvGroup.Visible = true;
                this.panelTree.Visible = true;
                this.panel2.Visible = true;
                if (this.btnShow.Visible != true)
                {
                    this.panel2.Width = 170;
                    this.panelTree.Width = 170;
                }
                this.panelTree.Controls.Add(tvGroup);
            }
            else
            {
                this.panelTree.Visible = false;
                this.panel2.Visible = false;
                if (tvGroup != null)
                {
                    tvGroup.Visible = false;
                }
            }
        }

        /// <summary>
        /// 菜单设置可见性
        /// </summary>
        private void SetMenuVisible()
        {
            Neusoft.HISFC.BizProcess.Integrate.Manager dictionaryMgr = new Neusoft.HISFC.BizProcess.Integrate.Manager();

            ArrayList alset = dictionaryMgr.GetConstantList("OUTPATMENU");

            Neusoft.FrameWork.Public.ObjectHelper setHelper = new Neusoft.FrameWork.Public.ObjectHelper();

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
                }
            }

            this.toolStrip1.Items.Clear();
            foreach (Neusoft.HISFC.Models.Base.Const conObj in alset)
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
                this.iQueryControlable = this.tabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IQueryControlable;
                this.iControlable = this.tabControl1.SelectedTab.Controls[0] as Neusoft.FrameWork.WinForms.Forms.IControlable;
                this.CurrentControl = this.tabControl1.SelectedTab.Controls[0];

                //传入PatienId流水号{17537415-C168-450d-BBCC-93CFFA19DB82}

                if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
                {
                    if (isFirst)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).transportAlDiag += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.TransportAlDiag(frmOutPatientOrder_transportAlDiag);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);

                        isFirst = false;
                    }
                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("您没有选择患者！");
                    }
                    else
                    {
                        //(this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).PatientId = ucOutPatientTree1.PatientInfo.ID;
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).RegInfo = ucOutPatientTree1.PatientInfo;
                    }
                    //this.neuPanel1.Visible = false;
                }
                else if (this.CurrentControl.GetType() == typeof(HISFC.Components.Order.OutPatient.Controls.ucPatientCase))
                {
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucPatientCase).diagnoses = this.Diagnoses;
                    if (this.Diagnoses != null)
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucPatientCase).SetDiagNose();
                    }
                    if (ucOutPatientTree1.PatientInfo == null)
                    {
                        MessageBox.Show("您没有选择患者！");
                    }
                    else
                    {
                        (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucPatientCase).Reg = ucOutPatientTree1.PatientInfo;
                    }
                    //this.neuPanel1.Visible = false;
                }
                //如果切换tab页时，没有点击数列表中的患者，历史医嘱不刷新 houwb {B9D1936C-4564-4e35-A158-40E8688267FF}
                else if (this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory))
                {
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).Patient = this.ucOutPatientTree1.PatientInfo;
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                }

                //开立界面显示
                if (this.ucOutPatientOrder1.IsDesignMode)
                {
                    if (this.CurrentControl.GetType() != typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
                    {
                        this.panel2.Visible = false;
                    }
                    else
                    {
                        this.panel2.Visible = true;
                    }
                }
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
                    this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
                }

                if (this.dcpInstance != null)
                {
                    string msg = "";
                    Hashtable hs = new Hashtable();
                    foreach (Neusoft.HISFC.Models.HealthRecord.Diagnose diag in e)
                    {
                        if (!hs.ContainsKey(diag.DiagInfo.ICD10.Name))
                        {
                            hs.Add(diag.DiagInfo.ICD10.Name, "");
                            this.dcpInstance.CheckDiseaseReport(this, this.ucOutPatientOrder1.Patient, Neusoft.HISFC.Models.Base.ServiceTypes.C, diag.DiagInfo.ICD10.Name, out msg);
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

            if (this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
            {
                this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;
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
        /// 复制所有医嘱后调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void frmOutPatientOrder_CopyHistoryClicked(Neusoft.HISFC.Models.Registration.Register register)
        {
            
            if (!this.ucOutPatientOrder1.IsDesignMode)
            {
                return;
            }
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                if (tabPage.Text.Contains("医嘱"))
                {
                    tabControl1.SelectedTab = tabPage;
                    break;
                }
            }

            if (this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
            {
                this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;
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
            this.ucOutPatientOrder1.PasteOrder();
        }

        void frmOutPatientOrder_transportAlDiag(ArrayList arrayDiagnoses)
        {
            if (arrayDiagnoses.Count > 0)
            {
                this.Diagnoses = arrayDiagnoses;
            }
            return;
        }

        /// <summary>
        /// 点击按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                this.ucOutPatientOrder1.SetInputItemVisible(false);
                if (e.ClickedItem == this.tbAddOrder)//开立
                {
                    //此处会多次查询患者医嘱，效率低
                    //this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                    //this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;

                    if (this.ucOutPatientOrder1.Patient == null)
                    {
                        MessageBox.Show("当前尚未选中患者！");
                        return;
                    }

                    #region 收费处方不允许再次开立

                    if (!ucOutPatientOrder1.CheckCanAdd())
                    {
                        return;
                    }

                    #endregion

                    this.statusBar1.Panels[1].Text = "(绿色：新开)(蓝色：收费)(红色：作废)";

                    //如果未进诊的，开立时自动进诊{9881FD05-A55B-4fcc-80CB-705CB5F1B206}
                    if (!this.ucOutPatientOrder1.Patient.IsSee)
                    {
                        this.ucOutPatientTree1.AutoTriage();
                    }
                    if (this.ucOutPatientOrder1.Add() == 0)
                        this.initButton(true);

                    if (!this.ucOutPatientOrder1.isHaveDiag())
                    {
                        //开立后先显示诊断界面
                        foreach (TabPage tabPage in tabControl1.TabPages)
                        {
                            if (tabPage.Text.Contains("诊断"))
                            {
                                tabControl1.SelectedTab = tabPage;
                                break;
                            }
                        }
                    }
                }
                else if (e.ClickedItem == this.tbGroup)//组套
                {
                    if (this.tbGroup.CheckState == CheckState.Checked)
                    {
                        this.tbGroup.CheckState = CheckState.Unchecked;
                        this.neuPanel1.Visible = true;
                    }
                    else
                    {
                        this.tbGroup.CheckState = CheckState.Checked;
                        this.neuPanel1.Visible = false;
                    }

                    if (this.tbGroup.CheckState == CheckState.Checked)
                    {
                        this.ucOutPatientOrder1.SetEditGroup(true);
                        this.ucOutPatientOrder1.Patient = null;
                        this.initButtonGroup(true);
                    }
                    else
                    {
                        this.ucOutPatientOrder1.SetEditGroup(false);
                        this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                        this.initButtonGroup(false);
                        this.panelTree.Visible = false;
                        this.panel2.Visible = false;
                    }

                }
                else if (e.ClickedItem == this.tbHerbal)
                {
                    this.ucOutPatientOrder1.HerbalOrder();
                }
                else if (e.ClickedItem == this.tbLevelOrder)
                {
                    //{1EB2DEC4-C309-441f-BCCE-516DB219FD0E} 层级形式开立医嘱 yangw 20101024
                    this.ucOutPatientOrder1.AddLevelOrders();
                }
                else if (e.ClickedItem == this.tbCheck)
                {
                    this.ucOutPatientOrder1.AddTest();
                }

                else if (e.ClickedItem == this.tbDelOrder)//删除
                {
                    this.ucOutPatientOrder1.Del();
                }
                else if (e.ClickedItem == this.tbDelOneOrder)//删除
                {
                    this.ucOutPatientOrder1.DeleteSingleOrder();
                }
                else if (e.ClickedItem == this.tbQueryOrder)//查询
                {
                    this.ucOutPatientOrder1.Patient = this.ucOutPatientTree1.PatientInfo;
                    this.ucOutPatientOrder1.Retrieve();
                }
                else if (e.ClickedItem == this.tbPrintOrder)//打印
                {
                    this.ucOutPatientOrder1.PrintRecipe(false);
                }
                else if (e.ClickedItem == this.tbComboOrder)//组合
                {
                    this.ucOutPatientOrder1.ComboOrder();
                }
                else if (e.ClickedItem == this.tbCancelOrder)//取消组合
                {
                    this.ucOutPatientOrder1.CancelCombo();
                }
                else if (e.ClickedItem == this.tbExitOrder)//退出医嘱
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
                        this.ucOutPatientOrder1.SetEditGroup(false);
                        this.initButtonGroup(false);
                    }
                    else
                    {
                        if (this.ucOutPatientOrder1.ExitOrder() == 0)
                        {
                            this.initButton(false);
                            //退出时判断取消进诊标记
                            this.ucOutPatientTree1.CancelTriage();
                        }
                    }
                }

                else if (e.ClickedItem == this.tbRegEmerPatient)//留观
                {
                    if (this.ucOutPatientOrder1.RegisterEmergencyPatient() < 0)
                    {
                    }
                    else
                    {
                        //MessageBox.Show("留观成功！");

                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }
                }
                //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
                else if (e.ClickedItem == this.tbOutEmerPatient) //出关
                {
                    if (this.ucOutPatientOrder1.OutEmergencyPatient() > 0)
                    {
                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }

                }
                //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
                else if (e.ClickedItem == this.tbInEmerPatient) //转住院
                {
                    if (this.ucOutPatientOrder1.InEmergencyPatient() > 0)
                    {
                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }
                }
                else if (e.ClickedItem == this.tbSeePatient)//诊出
                {
                    if (this.ucOutPatientOrder1.DiagOut() < 0)
                    {
                    }
                    else
                    {
                        MessageBox.Show(this.ucOutPatientOrder1.Patient.Name + " 诊出成功！");

                        ucOutPatientTree1.RefreshTreeView();
                        //ucOutPatientTree1.RefreshTreePatientDone();
                    }
                }
                else if (e.ClickedItem == this.tbRefreshPatient)//刷新
                {
                    ucOutPatientTree1.RefreshTreeView();
                    ucOutPatientTree1.RefreshTreePatientDone();
                }
                else if (e.ClickedItem == this.tbPatientTree)//列表
                {
                    this.neuPanel1.Visible = !this.neuPanel1.Visible;
                }
                else if (e.ClickedItem == this.tbSaveOrder)//保存
                {
                    try
                    {
                        this.tbSaveOrder.Enabled = false;

                        if (isEditGroup)
                        {
                            SaveGroup();
                        }
                        else
                        {
                            if (this.ucOutPatientOrder1.Save() == -1)
                            {
                            }
                            else
                            {
                                this.initButton(false);
                                ucOutPatientTree1.RefreshTreeView();

                                ucOutPatientTree1.FreshSeePatientNode();
                            }
                        }
                        this.statusBar1.Panels[1].Text = "";
                    }
                    catch
                    {
                    }
                    finally
                    {
                        this.tbSaveOrder.Enabled = true;
                    }
                }
                else if (e.ClickedItem == this.tb1Exit)//退出
                {
                    if (this.ucOutPatientOrder1 != null && this.ucOutPatientOrder1.IsDesignMode) //是在开立状态
                    {
                        DialogResult result = MessageBox.Show(this, Neusoft.FrameWork.Management.Language.Msg("医嘱目前处于开立模式，是否保存?"), "提示", MessageBoxButtons.YesNoCancel);
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
                else if (e.ClickedItem == this.tbDiseaseReport)     //  {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2} 传染病报告卡
                {
                    if (this.dcpInstance == null)
                    {
                        this.dcpInstance = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP)) as Neusoft.HISFC.BizProcess.Interface.DCP.IDCP;
                    }

                    if (this.dcpInstance != null)
                    {
                        Neusoft.HISFC.Models.RADT.Patient patient = this.ucOutPatientTree1.PatientInfo as Neusoft.HISFC.Models.RADT.Patient;

                        this.dcpInstance.RegisterDiseaseReport(this, patient, Neusoft.HISFC.Models.Base.ServiceTypes.C);
                    }
                }
                //LIS结果打印
                else if (e.ClickedItem == this.tbLisResultPrint)
                {
                    this.ucOutPatientOrder1.QueryLisResult();
                }
                //PACS结果打印
                else if (e.ClickedItem == this.tbPacsResultPrint)
                {
                    ucOutPatientOrder1.QueryPacsReport();
                }
                else if (e.ClickedItem == this.tbStation)
                {
                    this.ucOutPatientTree1.Call();
                }
                #region 申请单打印

                //LIS申请单打印
                else if (e.ClickedItem == this.tbLisReportPrint)
                {
                    this.ucOutPatientOrder1.LisReportPrint();
                }
                //PACS申请单打印
                else if (e.ClickedItem == this.tbPacsReportPrint)
                {
                    this.ucOutPatientOrder1.PacsReportPrint();
                }

                #endregion

                else if (e.ClickedItem == this.tbPass)
                {
                    this.tbPass.Checked = !this.tbPass.Checked;
                    //this.ucOutPatientOrder1.EnabledPass = this.tbPass.Checked;
                }
                else if (e.ClickedItem == this.tbCalculatSubl)
                {
                    this.ucOutPatientOrder1.CalculatSubl(false);
                }

                //开立界面显示
                if (this.ucOutPatientOrder1.IsDesignMode)
                {
                    if (this.CurrentControl.GetType() != typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.FoSi.ucOutPatientOrder))
                    {
                        this.panel2.Visible = false;
                    }
                    else
                    {
                        this.panel2.Visible = true;
                    }
                }

                if (this.CurrentControl != null && this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic))
                {
                    //保存诊断后，自动跳转到开立界面
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic).SaveClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucCaseInputForClinic.SaveClickDelegate(frmOutPatientOrder_SaveClicked);
                }
                if (this.CurrentControl != null && this.CurrentControl.GetType() == typeof(Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory))
                {
                    //复制处方后，自动跳转到开立界面
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked -= new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                    (this.CurrentControl as HISFC.Components.Order.OutPatient.Controls.ucOrderHistory).CopyAllClicked += new Neusoft.HISFC.Components.Order.OutPatient.Controls.ucOrderHistory.CopyAllClickDelegate(frmOutPatientOrder_CopyHistoryClicked);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 保存组套
        /// </summary>
        private void SaveGroup()
        {
            Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager group = new Neusoft.HISFC.Components.Common.Forms.frmOrderGroupManager();
            group.InpatientType = Neusoft.HISFC.Models.Base.ServiceTypes.C;
            try
            {
                group.IsManager = (Neusoft.FrameWork.Management.Connection.Operator as Neusoft.HISFC.Models.Base.Employee).IsManager;
            }
            catch
            { }

            ArrayList al = new ArrayList();
            for (int i = 0; i < this.ucOutPatientOrder1.neuSpread1.ActiveSheet.Rows.Count; i++)
            {
                //if (this.ucOutPatientOrder1.neuSpread1.ActiveSheet.IsSelected(i, 0))
                //{
                Neusoft.HISFC.Models.Order.OutPatient.Order order = this.ucOutPatientOrder1.GetObjectFromFarPoint(i, this.ucOutPatientOrder1.neuSpread1.ActiveSheetIndex).Clone();
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

        #region IPreArrange 成员   {B17077E6-7E65-45fb-BA25-F2883EB6BA27}

        public int PreArrange()
        {
            //增加取消功能 2011-1-3 houwb{DA7F7F3C-C9A6-4bcf-9181-93A6238B13F7}
            if (this.ucOutPatientTree1.InitControl() == -1)
            {
                return -1;
            }

            if (this.ucOutPatientTree1.RefreshTreeView() == -1)
            {
                return -1;
            }

            Neusoft.HISFC.BizProcess.Integrate.Manager managerInte = new Neusoft.HISFC.BizProcess.Integrate.Manager();

            Neusoft.HISFC.Models.Base.Department currentDept = managerInte.GetDepartment((orderManager.Operator as Neusoft.HISFC.Models.Base.Employee).Dept.ID);

            if (currentDept == null || currentDept.ID.Length < 0)
                return -1;

            if (currentDept.DeptType.ID.ToString() == "I")
            {
                if (MessageBox.Show("您当前登陆的科室为住院科室，是否继续？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                {
                    return -1;
                }
            }

            //对于分诊流程，很多地方需要判断诊室，改为选择诊室后立即赋值到ucOutPatientOrder
            this.ucOutPatientOrder1.CurrentRoom = this.ucOutPatientTree1.CurrRoom;

            return 1;
        }

        #endregion

        #region IInterfaceContainer 成员    {E53A21A7-2B74-4b48-A9F4-9E05F8FA11A2}

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(Neusoft.HISFC.BizProcess.Interface.DCP.IDCP) };
            }
        }

        #endregion
    }
}